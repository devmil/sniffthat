//==========================================================================================
//
//		LameSoft.Mobile.WirelessLan.AccessPoint
//		Copyright (c) 2006-2007, LameSoft (www.lamesoft.de)
//
//      This Code is provided under the LGPL. For more Information see 
//      http://www.gnu.org/licenses/lgpl.html
//
//      This library is free software; you can redistribute it and/or
//      modify it under the terms of the GNU Lesser General Public
//      License as published by the Free Software Foundation; either
//      version 2.1 of the License, or (at your option) any later version.
//
//      This library is distributed in the hope that it will be useful,
//      but WITHOUT ANY WARRANTY; without even the implied warranty of
//      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//      Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public
//      License along with this library; if not, write to the Free Software
//      Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
//
//      You are not allowed to remove or change this header!
//
//      If you change the source code in this file, you have to add a changelog entry.
//
//--------------- Changelog ----------------------------------------------------------------  
//
//      - 27.12.2006 (Michael Lamers, info@lamesoft.de): 
//          * added header
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LameSoft.Mobile.WirelessLan
{
    /// <summary>
    /// Represents an access point
    /// </summary>
    public class AccessPoint
    {
        #region private fields

        private string _Name;
        private Mode _Mode;
        private byte[] _MacAddress;
        private int _Privacy;
        private int _SignalStrengthInDecibels;
        private byte[] _SupportedRates;
        private Strength _Strength = Strength.NotAWirelessAdapter;

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessPoint"/> class.
        /// </summary>
        /// <param name="macAddress">The mac address.</param>
        /// <param name="name">The name.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="privacy">The privacy.</param>
        /// <param name="signalStrengthInDecibels">The signal strength in decibels.</param>
        /// <param name="strength">The strength.</param>
        /// <param name="supportedRates">The supported rates.</param>
        public AccessPoint(byte[] macAddress, string name, Mode mode, int privacy, int signalStrengthInDecibels, Strength strength, byte[] supportedRates)
        {
            _MacAddress = macAddress;
            _Name = name;
            _Mode = mode;
            _Privacy = privacy;
            _Strength = strength;
            _SignalStrengthInDecibels = signalStrengthInDecibels;
            _SupportedRates = supportedRates;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public Mode Mode
        {
            get { return _Mode; }
        }

        /// <summary>
        /// Gets the mac address.
        /// </summary>
        /// <value>The mac address.</value>
        public byte[] MacAddress
        {
            get { return _MacAddress; }
        }

        /// <summary>
        /// Gets the privacy.
        /// </summary>
        /// <value>The privacy.</value>
        public int Privacy
        {
            get { return _Privacy; }
        }

        /// <summary>
        /// Gets the signal strength in decibels.
        /// </summary>
        /// <value>The signal strength in decibels.</value>
        public int SignalStrengthInDecibels
        {
            get { return _SignalStrengthInDecibels; }
        }

        /// <summary>
        /// Gets the strength.
        /// </summary>
        /// <value>The strength.</value>
        public Strength Strength
        {
            get
            {
                return _Strength;
            }
        }

        /// <summary>
        /// Gets the supported rates.
        /// </summary>
        /// <value>The supported rates.</value>
        public byte[] SupportedRates
        {
            get { return _SupportedRates; }
        }

        #endregion
    }

    /// <summary>
    /// Access Point Network Mode
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Infrastructure Mode
        /// </summary>
        InfraStructure,
        /// <summary>
        /// AdHoc Mode
        /// </summary>
        AdHoc,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown
    }

    /// <summary>
    /// The StrengthType enumeration provides a list of relative RF Ethernet signal
    /// strength values that correspond to the strengths displayed by Windows CE
    /// itself.
    /// </summary>
    public enum Strength
    {
        /// <summary>
        /// The adapter for which signal strength was requested is not a wireless network
        /// adapter or does not report its signal strength in the standard way
        /// </summary>
        NotAWirelessAdapter = 0,
        /// <summary>
        /// The adapter is not receiving a network signal
        /// </summary>
        NoSignal = 1,
        /// <summary>
        /// The network signal has very low strength
        /// </summary>
        VeryLow = 2,
        /// <summary>
        /// The network signal has low strength
        /// </summary>
        Low = 3,
        /// <summary>
        /// The network signal is good
        /// </summary>
        Good = 4,
        /// <summary>
        /// The network signal is very good
        /// </summary>
        VeryGood = 5,
        /// <summary>
        /// The network signal is excellent
        /// </summary>
        Excellent = 6,
    }
}
