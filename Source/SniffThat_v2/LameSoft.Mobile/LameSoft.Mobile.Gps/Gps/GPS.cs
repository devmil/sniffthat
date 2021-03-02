//==========================================================================================
//
//		OpenNETCF.IO.Serial.GPS
//		Copyright (c) 2003-2006, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//==========================================================================================
//
//      Modified by Michael Lamers:
//       - Namespace
//       - usage of the .NET 2.0 serial port class
//
using System;
using System.Threading;
using System.Text;
using System.Collections;
using System.IO;
using System.Globalization;
using Microsoft.Win32;
using System.IO.Ports;

namespace LameSoft.Mobile.Gps
{
	public class GPS
	{
		#region private members
		SerialPort cp=null;
		string comport="COM1:";
		int baudrate = 4800;

		string strreceived="";
		int sentencecount=0;

		bool InitDistance = false;

		private States state=States.Stopped;

        string lasterror="";
		
		private Position waypoint=null;
		decimal distancetowaypoint=0;
		decimal bearingtowaypoint=0;
		decimal coursecorrection=0;

		// Position dilution of precision
		// The best value is near to 0
		decimal pdop=50; // The max if it's not present

		// Horizontal dilution of precision
		// The best value is near to 0
		decimal hdop=50; // The max if it's not present

		// Vertical dilution of precision
		// The best value is near to 0
		decimal vdop=50; // The max if it's not present
		
		// Maximum Allowable HDOP Error
		// The best value is near to 0
		decimal hdopmaxerror=6;

		//number of satellites in view
		int nbsatinview=0;

		//number of satellites used
		int nbsatused=0;

		// Fix mode Auto,Manual
		Fix_Mode fixmode=Fix_Mode.Auto;
		// Fix indicator NotSet,_2D,_3D
		Fix_Indicator fixindicator=Fix_Indicator.NotSet;
		// Fix type NotSet,	NoAltitude,	WithAltitude
		Fix_Type fixtype=Fix_Type.NotSet;
		// Fix typemode NotSet, SPS, DSPS, PPS, RTK
		Fix_TypeMode fixtypemode=Fix_TypeMode.NotSet;
		
    #endregion

		#region public members

        private Timer _TimeoutTimer = null;

        public void Start()
		{
			if (state!=States.Stopped) return;

            setstate = States.Opening;
            this.reset_gps_vars();

            DataReceived += new DataReceivedEventHandler(GPS_DataReceived);
            if (!isconnecteddevice())
            {
                OnError(null, "Com Port " + comport + " Is Not On Device", "");
                setstate = States.Stopped;
                return;
            }

            try
            {
                cp = new SerialPort(comport, (int)this.baudrate);
                cp.ReadTimeout = 5000;
                cp.WriteTimeout = 5000;
                cp.ReceivedBytesThreshold = 64;

                cp.Open();
            }
            catch (Exception e)
            {
                Thread.Sleep(1000);

                if ((cp != null) && cp.IsOpen)
                    StartPollingThread();
                else
                {
                    //OnError(e, "Could Not Open Com Port " + comport, "");
                    CancleGps();
                    throw e;
                }
            }
            cp.ErrorReceived += new SerialErrorReceivedEventHandler(cp_ErrorReceived);
            cp.DataReceived += new SerialDataReceivedEventHandler(cp_DataReceived);

            if (_TimeoutTimer != null)
                _TimeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);

            _TimeoutTimer = new Timer(new TimerCallback(TimeoutOccured), this, _Timeout, Timeout.Infinite);

            state = States.Running;
		}

        private int _Timeout = 10000;

        private void TimeoutOccured(object state)
        {
            CancleGps();
        }

        private void CancleGps()
        {
            if (_TimeoutTimer != null)
                _TimeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);
            try
            {
                if ((cp != null) && cp.IsOpen)
                    cp.Close();
            }
            catch
            { }
            cp = null;
            setstate = States.Stopped;
        }

        private Thread _PollingThread;

        private void StartPollingThread()
        {
            if (_PollingThread != null)
            {
                _PollingThread.Abort();
            }
            _PollingThread = new Thread(new ThreadStart(RunPolling));
            _PollingThread.Start();
        }

        private void RunPolling()
        {
            try
            {
                while ((cp != null) && (cp.IsOpen) && (state == States.Running))
                {
                    if (cp.BytesToRead > cp.ReceivedBytesThreshold)
                        cp_DataReceived();
                    Thread.Sleep(10);
                }
            }
            catch
            { 
            }
        }

		public void Stop()
		{
			if (state!=States.Running) return;
		
			// signal for the thread to exit
			setstate=States.Stopping;
            CancleGps();
            setstate = States.Stopped;

            if (_PollingThread != null)
            {
                _PollingThread.Join(1000);
                try
                {
                    _PollingThread.Abort();
                }
                catch
                { }
            }
		}

		/// <summary>
		/// calculate distance between a position and another position (in meters)
		/// </summary>
		/// <param name="pos1">Position 1</param>
		/// <param name="pos2">Position 2</param>
		/// <param name="unit">Units of measure</param>
		/// <returns></returns>
		public decimal CalculateDistance(Position pos1, Position pos2, Units unit)
		{
			double lat1 = (double)pos1.Latitude_Decimal;
			double lat2 = (double)pos2.Latitude_Decimal;
			double lon1 = (double)pos1.Longitude_Decimal;
			double lon2 = (double)pos2.Longitude_Decimal;
			double distance;
	
			if ((lat1 == lat2) && (lon1 == lon2)) 
				return 0;

			double DEG2RAD = Math.PI/180;
			lat1 *= DEG2RAD;
			lat2 *= DEG2RAD;
			lon1 *= DEG2RAD;
			lon2 *= DEG2RAD;
			distance = (60.0 * ((Math.Acos((Math.Sin(lat1) * Math.Sin(lat2)) + (Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1)))) / DEG2RAD));			
			
			switch (unit)
			{
				case Units.Kilometers:
					return Misc.NmToMeters((decimal)distance)/1000;
				case Units.Knots:
					return Misc.ToDecimal((decimal)distance)/1000;
				case Units.Miles:
					return Misc.NmToMiles((decimal)distance)/1000;
				default:
					return 0;
			}
		}
    
		/// <summary>
		/// calculate distance between last position and new position (in km)
		/// </summary>
		/// <returns></returns>
		/// 
		public decimal CalculateDistance(Units unit)
		{
			// if no move, no distance to add
			if (mov.SpeedKnots==0)
				return 0;

			double lat1mem=(double)pos.Latitude_Decimal_Mem;
			double lat2=(double)pos.Latitude_Decimal;
			double lon1mem=(double)pos.Longitude_Decimal_Mem;
			double lon2=(double)pos.Longitude_Decimal;

			// if no move, no distance to add
			if ((lat2 == lat1mem) && (lon2 == lon1mem)) 
				return 0;

			double lat1;
			double lon1;

			// the first time 
			if (InitDistance==true) 
				lat1 = lat1mem;
			else
				lat1 = lat2;
			
			pos.Latitude_Decimal_Mem = pos.Latitude_Decimal;
			
			if (InitDistance==true)
				lon1 = lon1mem;
			else
				lon1 = lon2;

			pos.Longitude_Decimal_Mem = pos.Longitude_Decimal;

			double distance;

			if (InitDistance==false) InitDistance=true;

			if ((lat1 == lat2) && (lon1 == lon2)) 
				return 0;

			double DEG2RAD = Math.PI/180;
			lat1 *= DEG2RAD;
			lat2 *= DEG2RAD;
			lon1 *= DEG2RAD;
			lon2 *= DEG2RAD;
			distance = (60.0 * ((Math.Acos((Math.Sin(lat1) * Math.Sin(lat2)) + (Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1)))) / DEG2RAD));			

			switch (unit)
			{
				case Units.Kilometers:
					return Misc.NmToMeters((decimal)distance)/1000;
				case Units.Knots:
					return Misc.ToDecimal((decimal)distance)/1000;
				case Units.Miles:
					return Misc.NmToMiles((decimal)distance)/1000;
				default:
					return 0;
			}
		}
		/// <summary>
		/// calculate distance with time and average speed (in km)
		/// </summary>
		/// <param name="TimePC"></param>
		/// <param name="TimeSAT"></param>
		/// <param name="unit"></param>
		/// <returns></returns>
		public decimal CalculateDistance(double TimePC,double TimeSAT, Units unit)
		{
			if (mov.SpeedKnotsAverage==0) return 0;

			double TimeInterval;

			if (pos.SatTime.TimeOfDay.TotalSeconds!=0)
				TimeInterval = pos.SatTime.TimeOfDay.TotalSeconds - TimeSAT;
			else
				TimeInterval = DateTime.Now.TimeOfDay.TotalSeconds - TimePC;
			switch (unit)
			{
				case Units.Kilometers:
					return (Misc.ToDecimal(TimeInterval) * mov.SpeedKphAverage)/3600;
				case Units.Knots:
					return (Misc.ToDecimal(TimeInterval) * mov.SpeedKnotsAverage)/3600;
				case Units.Miles:
					return (Misc.ToDecimal(TimeInterval) * mov.SpeedMphAverage)/3600;
				default:
					return 0;
			}
		}

		/// <summary>
		/// calculate bearing between a position and another position (in degrees)
		/// </summary>
		/// <param name="pos1"></param>
		/// <param name="pos2"></param>
		/// <returns></returns>
		public decimal CalculateBearing(Position pos1,Position pos2)
		{
			double DEG2RAD = Math.PI/180;
			double lat1 = (double)pos1.Latitude_Decimal * DEG2RAD;
			double lat2 = (double)pos2.Latitude_Decimal * DEG2RAD;
			double lon1 = (double)pos1.Longitude_Decimal * DEG2RAD;
			double lon2 = (double)pos2.Longitude_Decimal * DEG2RAD;
			double bearing;
	
			if ((lat1 == lat2) && (lon1 == lon2)) 
				return 0;

			bearing = (Math.Atan2(Math.Sin(lon2 - lon1) * Math.Cos(lat2),
				Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1))) / DEG2RAD;
			// returns a value between -180 and 180.
			if (bearing < 0.0)
				bearing += 360.0;

			return (decimal)bearing;
		}
    
		#endregion

		#region protected methods
		protected virtual void OnGpsSentence(GpsSentenceEventArgs e)
		{
			if (GpsSentence != null) GpsSentence(this, e);
		}

		protected virtual void OnGpsCommState(GpsCommStateEventArgs e)
		{
			if (GpsCommState!=null) GpsCommState(this, e);
		}

		protected virtual void OnPosition(Position pos)
		{
			if (Position!=null) Position(this,pos);
		}

		protected virtual void OnMovement(Movement mov)
		{
			if (Movement!=null) Movement(this,mov);
		}

		protected virtual void OnSatellites(Satellite[] satellites)
		{
			if (Satellite!=null) Satellite(this,satellites);
		}

		protected virtual void OnError(Exception exception,string message,string gps_data)
		{
			lasterror=message;
			if (Error!=null) Error(this,exception,message,gps_data);
		}

		protected virtual void OnDataReceived(string data)
		{
			if (DataReceived!=null) DataReceived(this,data);
		}

		#endregion

		#region events
		public event GpsSentenceEventHandler GpsSentence;
		public event GpsCommStateEventHandler GpsCommState;
		public event PositionEventHandler Position;
		public event MovementEventHandler Movement;
		public event SatelliteEventHandler Satellite;
		public event ErrorEventHandler Error;
		private event DataReceivedEventHandler DataReceived;
		#endregion

		#region delegates
		public delegate void GpsSentenceEventHandler(object sender, GpsSentenceEventArgs e);
		public delegate void GpsCommStateEventHandler(object sender, GpsCommStateEventArgs e);
		public delegate void GpsStatusEventHandler(object sender, StatusType GpsStatus);
		public delegate void PositionEventHandler(object sender,Position pos);
		public delegate void MovementEventHandler(object sender,Movement mov);
		public delegate void SatelliteEventHandler(object sender,Satellite[] satellites);
		public delegate void ErrorEventHandler(object sender,Exception exception,string message,string gps_data);
		private delegate void DataReceivedEventHandler(object sender, string data);
		#endregion

		#region properties
		public Satellite[] Satellites
		{
			get
			{
				return satellites;
			}
		}

		public Position Pos
		{
			get
			{
				return pos;
			}
		}

		private Movement Mov
		{
			get
			{
				return mov;
			}
		}

		public States State 
		{
			get
			{
				return state;
			}
		}

		public StatusType GpsState
		{
			get
			{
				return gpsstatus;
			}
		}

		private States setstate
		{
			set
			{
				state=value;
				this.OnGpsCommState(new GpsCommStateEventArgs(value));
			}
		}

		public string ComPort
		{
			set
			{
				comport=value;
			}
		}

		public int BaudRate
		{
			set
			{
				baudrate=value;
			}
		}

		public string LastError
		{
			get
			{
				return lasterror;
			}
		}

		public Decimal HdopMaxError
		{
			set
			{
				hdopmaxerror=value;
			}
			get
			{
				return hdopmaxerror;
			}
		}

		public Decimal Hdop
		{
		get
			{
				return hdop;
			}
		}

		public Decimal Vdop
		{
			get
			{
				return vdop;
			}
		}

		public Decimal Pdop
		{
			get
			{
				return pdop;
			}
		}

		public int SatInView
		{
			get	
			{
				return nbsatinview;
			}
		}
		public Fix_Mode FixMode
		{
			get	
			{
				return fixmode;
			}
		}
		public Fix_Indicator FixIndicator
		{
			get	
			{
				return fixindicator;
			}
		}
		public Fix_Type FixType
		{
			get	
			{
				return fixtype;
			}
		}
		public Fix_TypeMode FixTypeMode
		{
			get	
			{
				return fixtypemode;
			}
		}

		// TODO : waypoint stuff - not yet fully implemented
		#region waypoint stuff - not yet fully implemented
		public Position WayPoint
		{
			get
			{
				return waypoint;
			}
			set
			{
				waypoint=value;
			}
		}

		public decimal DistanceToWayPoint
		{
			get
			{
				// HELP REQUIRED HERE - to work out distance to waypoint
				calculate_waypoint();
				return distancetowaypoint;
			}
		}
		public decimal BearingToWayPoint
		{
			
			get
			{
				// HELP REQUIRED HERE - to work out distance to waypoint
				calculate_waypoint();
				return bearingtowaypoint;
			}
		}

		public decimal CourseCorrection
		{
			
			get
			{
				// HELP REQUIRED HERE - to work out what course correction to apply to get to waypoint
				calculate_waypoint();
				return coursecorrection;
			}
		}

		#endregion
		
		#endregion

		#region private methods
		private void reset_gps_vars()
		{
			strreceived="";
			sentencecount=0;

			pos = null;
			pos = new Position();
			mov = null;
			mov = new Movement();
			satellites = null;
			lasterror="";

			distancetowaypoint=0;
			bearingtowaypoint=0;
			coursecorrection=0;
			
			InitDistance = false;

			// Position dilution of precision
			pdop=50;

			// Horizontal dilution of precision
			hdop=50;

			// Vertical dilution of precision
			hdop=50;

		}

		private void calculate_waypoint()
		{
			if (pos==null || waypoint==null) return;
	
			distancetowaypoint = CalculateDistance(pos,waypoint,Units.Kilometers);
			bearingtowaypoint = CalculateBearing(pos,waypoint);
			coursecorrection = bearingtowaypoint - mov.Track;
			if (coursecorrection < -180m)
				coursecorrection += 360m;
			if (coursecorrection > 180m)
				coursecorrection -= 360m;
		}

        void cp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            cp_DataReceived();
        }

        void cp_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            cp_OnError(e.ToString());
            CancleGps();
        }

		/// <summary>
		/// this is fired when the comport receives data
		/// </summary>
		private void cp_DataReceived()
		{
			try
			{
				if (state!=States.Running) return;

                if (_TimeoutTimer != null)
                {
                    _TimeoutTimer.Change(_Timeout, Timeout.Infinite);
                }
			
				string strret="";
				string strdata="";
				
				byte[] inputData = new byte[1];
			
				while (cp.BytesToRead > 0)
				{

					inputData[0] = (byte)cp.ReadByte();
					strret = Encoding.ASCII.GetString(inputData, 0,1);
					if (strret=="\n") // If newline
					{
						strdata=this.strreceived.Substring(0,strreceived.Length-1);
						//nmea(strdata);
						OnDataReceived(strdata);

						strdata="";
						strreceived="";
					}					
					else
						strreceived+=strret;	
				}
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in cp_DataReceived","");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="data"></param>
		private void GPS_DataReceived(object sender, string data)
		{
			nmea(data);
		}
		/// <summary>
		/// processes our string of nmea data
		/// </summary>
		/// <param name="gps_data">string of input data</param>
		/// <returns>Boolean to indicate if we were able to process string of data ok</returns>
		private bool nmea(string gps_data) 
		{
			try
			{

			// GPS data can't be zero length
			if (gps_data.Length==0) return false;

			// first character must be a $
			if (gps_data[0]!='$') return false;

			// GPS data can't be longer than 82 character
			if (gps_data.Length>82) return false;

			// remove our leading character
			string strdata=gps_data.Substring(1);

			// see if the last block contains a * used to see if we have a checksum
			int intstarpos=strdata.IndexOf('*');
			if (intstarpos>=0)
			{
				// we have a checksum so check it...
				string strchecksum=strdata.Substring(intstarpos+1);

				// remove checksum from end of string
				strdata=strdata.Substring(0,strdata.Length-strchecksum.Length-1);

				if (!checksum(strdata,strchecksum))
				{
					OnError(null,"Checksum failed on: '"+gps_data+"'",gps_data);
					return false;
				}
			}

			String[] strrecarray = strdata.Split(',');

			// get the first block which is the sentence id
			string strsentence=strrecarray[0];

			ArrayList sbdata = new ArrayList();

			// get the data block minus the first block
			for (int i=1;i<strrecarray.Length;i++)
				sbdata.Add(strrecarray[i]);

			// if all is well raise our main GPS event
			string[] arrydata = (string[]) sbdata.ToArray(typeof(string));

			sbdata = null;

			// increment our counter
			if (sentencecount==int.MaxValue) sentencecount=0;

			sentencecount++;
			OnGpsSentence(new GpsSentenceEventArgs(gps_data,sentencecount));

			// simple checksums to see if we need to fire and event
			//decimal poscheck = pos.Latitude_Fractional + pos.Longitude_Fractional;
			//decimal movcheck = mov.SpeedKnots+mov.Track;
			StatusType oldgpsstatus = this.gpsstatus;
			switch (strsentence)
			{
				case "GPRMC": // Recommended minimum specific GPS/Transit data 
					//if (storedgprmc!=gps_data)
					//{
						//storedgprmc=gps_data;
						process_gprmc(arrydata,gps_data);
					//}
					break;
				case "GPGGA": //Global positioning system fixed data
					//if (storedgpgga!=gps_data)
					//{
						//storedgpgga=gps_data;
						process_gpgga(arrydata,gps_data);
					//}
					break;

				case "GPGSA": // GPS DOP and active satellites

					//if (storedgpgsa!=gps_data)
					//{

						//storedgpgsa=gps_data;
						process_gpgsa(arrydata,gps_data);
					//}
					break;

				case "GPGSV": // Satellites in view
					//if (storedgpgsv!=gps_data)
					//{
						//storedgpgsv=gps_data;
						if (process_gpgsv(arrydata,gps_data))
							OnSatellites(this.satellites);
					//}
					break;
				case "GPGLL": // Location Fix
					//if (storedgpgll!=gps_data)
					//{
						//storedgpgll=gps_data;
						process_gpgll(arrydata,gps_data);
					//}
					break;
				case "GPVTG": // 
					//if (storedgpvtg!=gps_data)
					//{
						//storedgpvtg=gps_data;
						process_gpvtg(arrydata,gps_data);
					//}
					break;
				default: 

					return true;

			}
			// TODO poscheck not a good solution
			// see if our long and lat have changed
			//if (poscheck != (pos.Latitude_Fractional  + pos.Longitude_Fractional ))
			//{
				// only fire if we have under the HDOP maxium error
				//if (hdop<=hdopmaxerror)
					//OnFix(pos,fixtype);
				OnPosition(pos);
				//}
			// TODO movcheck not a good solution
			//if (movcheck!=mov.SpeedKnots+mov.Track)
			//{
				// only fire if we have under the HDOP maxium error
				//if (hdop<=hdopmaxerror)
				OnMovement(mov);
			//}

//			if (oldgpsstatus!=gpsstatus)
//			{
//				OnGpsStatus(new GpsStatusEventArgs(gpsstatus));
//			}
			return true;

			}
			catch(Exception ex)
			{
				OnError(ex,"Error in nmea",gps_data);
				return false;
			}
		}

		/// <summary>
		/// this checksums all nmea sequences
		/// </summary>
		/// <param name="strtocheck">string to check</param>
		/// <param name="strchecksum">checksum</param>
		/// <returns>true if checksum computes</returns>
		private bool checksum(string strtocheck,string strchecksum)
		{
			int intor=0;
			// go from first character upto last *
			for(int i=0;(i<strtocheck.Length);i++)
				intor=intor^ (int) (strtocheck[i]);

			int y = 0;

			try 
			{
				y = Convert.ToInt32(strchecksum,16);
			}
			catch
			{
				return false;
			}
			if (intor != y)
			{
				// debug for checksum failures
				intor+=0;
			}
			return (intor == y);
		}

		private void cp_OnError(string Description)
		{
			if (state!=States.Running) return;
			OnError(null,"Com Port Error Received "+Description,"");
			this.setstate=States.Stopping;
		}

		

		private bool isconnecteddevice()
		{
            return true;
/*			foreach (string port in this.devices())
			{
			
				if (port.ToUpper()==this.comport.ToUpper())
				{
					return true;
				}
			}
			return false;*/
		}

		private string[] devices()
		{
			// lists all modems on the device
			ArrayList al = new ArrayList();
			RegistryKey defkey;
			string[] keyNames;
			string keyvalue="";
			string strport="";
		
			// now get the active
			defkey=Registry.LocalMachine.CreateSubKey(@"Drivers\Active");
			
			keyNames =defkey.GetSubKeyNames();
			foreach (string x in keyNames)
			{
				
				try	
				{
					keyvalue=defkey.CreateSubKey(x).GetValue("Key").ToString();
					strport=defkey.CreateSubKey(x).GetValue("Name").ToString();
					
					// fudge to remove rubbish off the end of the string
					strport = strport.Substring(0,strport.IndexOf("'"));

					RegistryKey thekey=Registry.LocalMachine.CreateSubKey(keyvalue);
					
					if (strport.StartsWith("COM"))
						al.Add(strport);
				}
				catch
				{

				}
			}
			return (string[]) al.ToArray(typeof(string));
		}

		#endregion

		#region gps stored data
		private StatusType gpsstatus=StatusType.Warning; 
		private Position pos=null;
		private Movement mov=null;
		private Satellite[] satellites = null;
		#endregion 

		#region gpsmessage handlers
		private bool process_gpgsv(string []strdata, string gpsdata)
		{	
			try
			{
				
				int numberofmessages=0;
				int messagenumber=0;
				
				//int numsat=0;
				// 0 Number of messages 3 Number of messages in complete message (1-3)
				if (strdata[0].Length>0)
				{
					numberofmessages=Convert.ToInt32(strdata[0]);
				}

				// 1 Sequence number 1 -Sequence number of this entry (1-3)
				if (strdata[1].Length>0)
				{
					messagenumber=Convert.ToInt32(strdata[1]);
				}
				// 2 Satellites in view - 10
				if (strdata[2].Length>0)
				{
					nbsatinview=Convert.ToInt32(strdata[2]);
					if (satellites==null)
					{
						setupsats(nbsatinview);
					}
				}

				if (messagenumber==0||nbsatinview==0) return false;

				int whichsat=(nbsatinview-((messagenumber-1)*4)-1);
				int sats;
				int intcount=0;


				if (whichsat >= 4)
					sats = 4;
				else
					sats = nbsatinview-((messagenumber-1)*4);


				for (int i=1;i<=sats;i++) 
				{
					// 3 Satellite ID 1 - 20- Range is 1-32
					if (strdata[intcount+3].Length>0)
						satellites[whichsat].ID=Convert.ToInt32(strdata[intcount+3]);

					// 4 Elevation 1- 78- Elevation in degrees (0-90)
					if (strdata[intcount+4].Length>0)
						satellites[whichsat].Elevation=Convert.ToInt32(strdata[intcount+4]); 

					// 5 Azimuth 1 - 331- Azimuth in degrees (0-359)
					if (strdata[intcount+5].Length>0)
						satellites[whichsat].Azimuth=Convert.ToInt32(strdata[intcount+5]);

					// 6 SNR 1 -45 - Signal to noise ration in dBHZ (0-99)
					if (strdata[intcount+6].Length>0)
						satellites[whichsat].SNR=Convert.ToInt32(strdata[intcount+6]);
					else
						satellites[whichsat].SNR=0;

					intcount += 4;
					whichsat--;	
				}
				return (numberofmessages==messagenumber);
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in process_gpgsv",gpsdata);
				return false;
			}
		}
		private void process_gpgsa(string []strdata,string gpsdata)
		{
			try
			{
				//0 Mode 1 - A - A = Auto 2D/3D, M = Forced 2D/3D
				if (strdata[0].Length>0)
				{
					if (strdata[0][0]=='A')
						fixmode = Fix_Mode.Auto;
					else
						fixmode = Fix_Mode.Manual;
				}

				//1 Mode 1 - 3 - 1 = No fix, 2 = 2D, 3 = 3D
				if (strdata[1].Length>0)
				{
					switch (Convert.ToInt32(strdata[1]))
					{
						case 1:
							fixindicator=Fix_Indicator.NotSet;
							fixtype=Fix_Type.NotSet;
							break;
						case 2:
							fixindicator=Fix_Indicator.Mode2D;
							fixtype=Fix_Type.NoAltitude;
							break;
						case 3:
							fixindicator=Fix_Indicator.Mode3D;
							fixtype=Fix_Type.WithAltitude;
							break;
						default:
							fixindicator=Fix_Indicator.NotSet;
							fixtype=Fix_Type.NotSet;
							break;
					}
				}

				// if satellites is null because GSV sentence not arrived or not present
				if (satellites==null)
				{
					nbsatused=0;
					for (int intcount=0;intcount<12;intcount++)
					{
						string strid=strdata[2+intcount];
						if (strid!="")
						{
							nbsatused++;
						}
					}
					setupsats(nbsatused);
				}


				if (satellites!=null)
				{
					foreach (Satellite s in this.satellites)
					{
						s.Active=false;
						s.Channel=0;
					}

					//  2 Satellite used 1  - 01 - Satellite used on channel  1
					//  3 Satellite used 2  - 20 - Satellite used on channel  2
					//  4 Satellite used 3  - 19 - Satellite used on channel  3
					//  5 Satellite used 4  - 13 - Satellite used on channel  4
					//  6 Satellite used 5  -    - Satellite used on channel  5
					//  7 Satellite used 6  -    - Satellite used on channel  6
					//  8 Satellite used 7  -    - Satellite used on channel  7
					//  9 Satellite used 8  -    - Satellite used on channel  8
					// 10 Satellite used 9  -    - Satellite used on channel  9
					// 11 Satellite used 10 -    - Satellite used on channel 10
					// 12 Satellite used 11 -    - Satellite used on channel 11
					// 13 Satellite used 12 -    - Satellite used on channel 12

					nbsatused=0;
					for (int intcount=0;intcount<12;intcount++)
					{
						string strid=strdata[2+intcount];
						if (strid!="")
						{
							nbsatused++;
							setsat(Convert.ToInt32(strid),intcount+1);
						}
					}
				}

				// 14 PDOP 40.4 Position dilution of precision
				if (strdata[14].Length>0)
					pdop=Misc.ToDecimal(strdata[14]);
		
				// 15 HDOP 24.4 Horizontal dilution of precision
				if (strdata[15].Length>0)
					hdop=Misc.ToDecimal(strdata[15]);
					
				
				// 16 VDOP 32.2 Vertical dilution of precision
				if (strdata[16].Length>0)
					vdop=Misc.ToDecimal(strdata[16]);
				
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in process_gpgsa",gpsdata);
			}
		}

		/// <summary>
		/// sets the id's in our list of satellites
		/// </summary>
		/// <param name="id"></param>
		/// <param name="channel"></param>
		private void setsat(int id,int channel)
		{
			if (id==0) return;
			bool satfound=false;

			foreach (Satellite s in this.satellites)
			{
				if (s.ID==id)
				{
					s.Active=true;
					s.Channel=channel;
					satfound=true;
					break;
				}
			}
			if (satfound==false)
			{
				foreach (Satellite s in this.satellites)
				{
					if (s.ID==0)
					{
						s.ID=id;
						s.Active=true;
						s.Channel=channel;
						satfound=true;
						break;
					}
				}
			}

		}
		/// <summary>
		/// sets up our array of sats
		/// </summary>
		/// <param name="numsats"></param>
		private void setupsats(int numsats)
		{
			if (numsats==0) return;
			// TODO it will be better to have an arraylist
			satellites = new Satellite[30];

			for (int intcount=0;intcount<satellites.Length;intcount++)
			{
					satellites[intcount]= new Satellite();
			}
		}

		private void process_gpgga(string []strdata,string gpsdata)
		{
			try
			{
				//0 UTC Time
				if (strdata[0].Length>0)
				{
					settime(strdata[0]);
				}

				//1 Latitude
				if (strdata[1].Length>0)
				{
					pos.Latitude_Fractional=Misc.ToDecimal(strdata[1]);
				}
			
				//2 N/S Indicator
				if (strdata[2].Length>0)
				{
					if (strdata[2][0]=='N')
						pos.DirectionLatitude=CardinalDirection.North;
					else
						pos.DirectionLatitude=CardinalDirection.South;
				}

				//3 Longitude
				if (strdata[3].Length>0)
				{
					pos.Longitude_Fractional=Misc.ToDecimal(strdata[3]);
				}

				//4 E/W Indicator
				if (strdata[4].Length>0)
				{
					if (strdata[4][0]=='W')
						pos.DirectionLongitude=CardinalDirection.West;
					else
						pos.DirectionLongitude=CardinalDirection.East;
				}

				//5 Position Fix - 0 = Invalid, 1 = Valid SPS, 2 = Valid DGPS, 3 = Valid PPS, 4 = Valid RTK
				if (strdata[5].Length>0)
				{
					switch (Convert.ToInt32(strdata[5]))
					{
						case 0:
							fixtypemode=Fix_TypeMode.NotSet;
							//fixtype=Fix_Type.NotSet;
							break;
						case 1:
							fixtypemode=Fix_TypeMode.SPS;
							//fixtype=Fix_Type.WithAltitude;
							break;
						case 2:
							fixtypemode=Fix_TypeMode.DSPS;
							//fixtype=Fix_Type.WithAltitude;
							break;
						case 3:
							fixtypemode=Fix_TypeMode.PPS;
							//fixtype=Fix_Type.WithAltitude;
							break;
						case 4:
							fixtypemode=Fix_TypeMode.RTK;
							//fixtype=Fix_Type.WithAltitude;
							break;
						default:
							fixtypemode=Fix_TypeMode.NotSet;
							//fixtype=Fix_Type.NotSet;
							break;
					}
				}

				//6 Satellites Used (0-12)
				if (strdata[6].Length>0)
				{
					nbsatused=Convert.ToInt32(strdata[6]);
				}

				//7 HDOP- Horizontal dilution of precision
				if (strdata[7].Length>0)
					hdop=Misc.ToDecimal(strdata[7]);

				//8 Altitude- Altitude in meters according to WGS-84 ellipsoid
				if (strdata[8].Length>0)
				{
						pos.Altitude=Misc.ToDecimal(strdata[8]);
				}

				//9 Altitude Units - M = Meters
				//if (strdata[9].Length>0)
				//{
				// Not necessary
				//}

				//10 Geoid Seperation- Geoid seperation in meters according to WGS-84 ellipsoid
				if (strdata[10].Length>0)
				{
					pos.GeoidSeparation = Misc.ToDecimal(strdata[10]);
				}

				//11 Geoid Seperation Units - M = Meters
				//if (strdata[11].Length>0)
				//{
				// Not necessary
				//}

				//12 DGPS Age - Age of DGPS data in seconds
				//if (strdata[12].Length>0)
				//{
				// To be implemented
				//}

				//13 DGPS Station ID-0000
				//if (strdata[13].Length>0)
				//{
				// To be implemented
				//}
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in process_gpgga",gpsdata);
			}
		}

		private void process_gpgll(string []strdata,string gpsdata)
		{
			try
			{
				// 0 Latitude 4250.5589 ddmm.mmmm 
				if (strdata[0].Length>0)
				{
					pos.Latitude_Fractional=Misc.ToDecimal(strdata[0]);
				}

				// 1 N/S Indicator S N = North, S = South 
				if (strdata[1].Length>0)
				{
					if (strdata[1][0]=='N')
						pos.DirectionLatitude=CardinalDirection.North;
					else
						pos.DirectionLatitude=CardinalDirection.South;
				}

				// 2 Longitude 14718.5084 dddmm.mmmm 
				if (strdata[2].Length>0)
				{
					pos.Longitude_Fractional=Misc.ToDecimal(strdata[2]);
				}

				// 3 E/W Indicator E E = East, W = West
				if (strdata[3].Length>0)
				{
					if (strdata[3][0]=='W')
						pos.DirectionLongitude=CardinalDirection.West;
					else
						pos.DirectionLongitude=CardinalDirection.East;
				}


				// 4 UTC Time HHMMSS 
				if (strdata[4].Length>0)
				{
					settime(strdata[4]);
				}
				
				// 5 Status A A = Valid, V = Invalid 
				gpsstatus=(strdata[5]=="A")?StatusType.OK:StatusType.Warning;
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in process_gpgll",gpsdata);
			}
		}

		private void process_gprmc(string []strdata,string gpsdata)
		{
			try
			{
				// 0 180432 UTC of position fix in hhmmss format (18 hours, 4 minutes and 32 seconds)
				if (strdata[0].Length>0)
				{
					settime(strdata[0]);
				}

				// 1 A Status (A - data is valid, V - warning) 
				gpsstatus=(strdata[1]=="A")?StatusType.OK:StatusType.Warning;
				/*
				switch (strdata[1])
				{
					case "A":
						fixtype=Fix_Type.WithAltitude;
						break;
					case "V":
						fixtype=Fix_Type.NotSet;
						break;
				}
				*/

				//2 4027.027912 Geographic latitude in ddmm.mmmmmm format (40 degrees and 27.027912 minutes) 
				if (strdata[2].Length>0)
				{
					pos.Latitude_Fractional=Misc.ToDecimal(strdata[2]);
				}		
			

				// 3 N Direction of latitude (N - North, S - South) 
				if (strdata[3].Length>0)
				{
					if (strdata[3][0]=='N')
						pos.DirectionLatitude=CardinalDirection.North;
					else
						pos.DirectionLatitude=CardinalDirection.South;
				}

				// 4 08704.857070 Geographic longitude in dddmm.mmmmmm format (87 degrees and 4.85707 minutes) 
				if (strdata[4].Length>0)
				{
					pos.Longitude_Fractional=Misc.ToDecimal(strdata[4]);
				}

				// 5 W Direction of longitude (E - East, W - West) 

				if (strdata[5].Length>0)
				{
					if (strdata[5][0]=='W')
						pos.DirectionLongitude=CardinalDirection.West;
					else
						pos.DirectionLongitude=CardinalDirection.East;
				}

				//6 000.04 Speed over ground (0.04 knots) 
				if (strdata[6].Length>0)
				{
					mov.SpeedKnots=Misc.ToDecimal(strdata[6]);
					mov.NbSpeedValues = mov.NbSpeedValues + 1;
				}


				// 7 181.9 Track made good (heading) (181.9º) 
				if (strdata[7].Length>0)
					mov.Track=Misc.ToDecimal(strdata[7]);

				//8 131000 Date in ddmmyy format (October 13, 2000) 
				if (strdata[8].Length>0)
				{
					setdate(strdata[8], strdata[0]);
				}

				// 9 1.8 Magnetic variation (1.8º) 
				if (strdata[9].Length>0)
					mov.MagneticVariation=Misc.ToDecimal(strdata[9]);

				//10 W Direction of magnetic variation (E - East, W - West) 
				if (strdata[10].Length>0)
				{
					if (strdata[10][0]=='W')
						mov.DirectionMagnetic=CardinalDirection.West;
					else
						mov.DirectionMagnetic=CardinalDirection.East;
				}

			}
			catch(Exception ex)
			{
				OnError(ex,"Error in process_gprmc",gpsdata);
			}
		}

		private void process_gpvtg(string []strdata,string gpsdata)
		{
			try
			{
				// 0 309.62 degrees Course over ground - Measured heading
				if (strdata[0].Length>0)
				{
					mov.Track=Misc.ToDecimal(strdata[0]);
				}

				// 1 T Course Reference - True
				//if (strdata[1].Length>0)
				//{
					// Not necessary
				//}
 
				//2 xxx.xx degrees Course over ground - Measured heading 
				//if (strdata[2].Length>0)
				//  Not necessary as information is available in GPRMC
				//	mov.MagneticVariation=Misc.ToDecimal(strdata[2]);
			
				// 3 T Course Reference - Magnetic
				//if (strdata[3].Length>0)
				//{
					// Not necessary
				//}

				// 4 0.13 Measured horinzontal speed in knots 
				if (strdata[4].Length>0)
				{
					mov.SpeedKnots=Misc.ToDecimal(strdata[4]);
					mov.NbSpeedValues = mov.NbSpeedValues + 1;
				}

				// 5 N for knots 
				//if (strdata[5].Length>0)
				//{
					// Not necessary
				//}

				// 6 0.2 Measured horinzontal speed in kilometers per hours 
				//if (strdata[6].Length>0)
				//{
					// Not necessary, use mov.SpeedKph
				//}

				// 7 K for kilometers per hours 
				//if (strdata[7].Length>0)
				//{
					// Not necessary
				//}

			}
			catch(Exception ex)
			{
				OnError(ex,"Error in process_gpvtg",gpsdata);
			}
		}

		private void settime(string strtime)
		{
			try
			{

				int day = DateTime.Now.Day;
				int month = DateTime.Now.Month;
				int year = DateTime.Now.Year;

				int utchours = Convert.ToInt32(strtime.Substring(0, 2));
				int utcminutes = Convert.ToInt32(strtime.Substring(2, 2));
				int utcseconds = Convert.ToInt32(strtime.Substring(4, 2));
				int utcmilliseconds =0;

				// extract milliseconds if it is available
				if (strtime.Length > 7)
					utcmilliseconds = Convert.ToInt32(strtime.Substring(7));

				// now build a datetime object with all values
				pos.SatTime= new DateTime(year, month, day, utchours, utcminutes, utcseconds, utcmilliseconds);
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in settime",strtime);
			}
		}

		private void setdate(string strdate, string strtime)
		{
			try
			{
				// now build a datetime object with all values
				int day = Convert.ToInt32(strdate.Substring(0, 2));
				int month = Convert.ToInt32(strdate.Substring(2, 2));
				// available for this century
				int year = Convert.ToInt32(strdate.Substring(4, 2)) + 2000;

				int utchours = Convert.ToInt32(strtime.Substring(0, 2));
				int utcminutes = Convert.ToInt32(strtime.Substring(2, 2));
				int utcseconds = Convert.ToInt32(strtime.Substring(4, 2));
				int utcmilliseconds =0;

				// extract milliseconds if it is available
				if (strtime.Length > 7)
					utcmilliseconds = Convert.ToInt32(strtime.Substring(7));

				pos.SatDate= new DateTime(year, month, day, utchours, utcminutes, utcseconds, utcmilliseconds);
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in setdate",strdate + " / " + strtime);
			}
		}
		/// <summary>
		/// Send to GPS receiver a sentence like $PSRF103(enable/disable sentences) or $PSRF100(change serial gps speed)
		/// </summary>
		public bool SendGpsMessage(string GPSSentence)
		{
			try
			{
				// Port open
				if (state==States.Running)
				{
                    byte[] buf = Encoding.ASCII.GetBytes(GPSSentence);
                    cp.Write(buf, 0, buf.Length); 
					return true;
				}
				else
					// Port closed
				{
					return false;
				}
			}
			catch(Exception ex)
			{
				OnError(ex,"Error in SendRate ",GPSSentence);
				return false;
			}
		}

		#endregion
    }

    #region Enums
    public enum States
    {
        /// <summary>
        /// Opening
        /// </summary>
        Opening,
        /// <summary>
        /// Running
        /// </summary>
        Running,
        /// <summary>
        /// Stopping
        /// </summary>
        Stopping,
        /// <summary>
        /// Stopped
        /// </summary>
        Stopped
    }

    public enum Fix_Mode
    {
        Auto,
        Manual
    }
    public enum Fix_Indicator
    {
        NotSet,
        Mode2D,
        Mode3D
    }
    public enum Fix_Type
    {
        NotSet,
        NoAltitude,
        WithAltitude
    }
    public enum Fix_TypeMode
    {
        NotSet,
        SPS,
        DSPS,
        PPS,
        RTK
    }
    public enum Units
    {
        Kilometers,
        Miles,
        Knots
    }

    public enum StatusType
    {
        NotSet,
        OK, //A
        Warning //V
    }

    public enum CardinalDirection
    {
        /// <summary>
        /// North
        /// </summary>
        North = 0,
        /// <summary>
        /// East
        /// </summary>
        East = 1,
        /// <summary>
        /// South
        /// </summary>
        South = 2,
        /// <summary>
        /// West
        /// </summary>
        West = 4,
        /// <summary>
        /// Northwest
        /// </summary>
        NorthWest = 5,
        /// <summary>
        /// Northeast
        /// </summary>
        NorthEast = 6,
        /// <summary>
        /// Southwest
        /// </summary>
        SouthWest = 7,
        /// <summary>
        /// Southeast
        /// </summary>
        SouthEast = 8,
        /// <summary>
        /// Stationary
        /// </summary>
        Stationary = 9
    }
    #endregion
}
