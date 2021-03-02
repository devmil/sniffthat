; NOTE: this .NSI script is designed for NSIS v1.8+

!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"
!define MUI_WELCOMEPAGE_TITLE_3LINES
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_BITMAP "SniffThat\Images\SniffThatInstall.bmp"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!define MUI_LICENSEPAGE_RADIOBUTTONS
!insertmacro MUI_PAGE_LICENSE "SniffThat\InstallerFiles\lgpl.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "German"

; MUI end ------

!define PRODUCT_NAME "SniffThat"
!define PRODUCT_VERSION "1.0.3"
!define PRODUCT_PUBLISHER "LameSoft"
!define PRODUCT_WEB_SITE "http://codeplex.com/sniffthat"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
;Icon "YourApp.ico"
OutFile "SniffThatSetup.exe"
ShowInstDetails show
ShowUnInstDetails show

LicenseText "Sie müssen die Lizenzbedingungen akzeptieren, um mit dem Setup fortfahren zu können."

InstallDir "$PROGRAMFILES\LameSoft\SniffThat"
InstallDirRegKey HKEY_LOCAL_MACHINE "SOFTWARE\LameSoft\SniffThat" ""
;DirShow show ; (make this hide to not let the user change it)
DirText "Bitte wählen Sie das Verzeichnis, in das Sie SniffThat installieren wollen:"

InstType "Full"
;InstType "Base"
InstType /CUSTOMSTRING=Base
; The trick here is that we have only "Full" and "Base" types of installation
; NSIS knows the registered (with InsType) "Full" type. Any other combination
; of modules selected for installation is "Custom" - which we rename to "Base"
; because it's only one. (The main app is mandatory and the second is optional.)


;EnabledBitmap "check.bmp"
;DisabledBitmap "nocheck.bmp"
; if you don't use these bmps, a camel with a red check mark on it will appear
; instead of a classical checkbox (or a grayed camel)

ComponentText "Select what you wish to install." "The type of install:" "Components to install:"

;!execute 'SniffThat\InstallerFiles\CabRepair.exe "SniffThat\CabSetup\Release\SniffThat.inf"'

Section "SniffThat" ; (default, requried section)
SectionIn 1
SetOutPath "$INSTDIR"

File SniffThat\CabSetup\Release\Setup.ini
;File "SniffThat\CabSetup\Release\SniffThat.inf"
File "SniffThat\CabSetup\Release\SniffThat.cab"


; one-time initialization needed for InstallCAB subroutine 
ReadRegStr $1 HKEY_LOCAL_MACHINE "software\Microsoft\Windows\CurrentVersion\App Paths\CEAppMgr.exe" "" 
IfErrors Error 
Goto End
Error:
MessageBox MB_OK|MB_ICONEXCLAMATION \
"Unable to find Application Manager for PocketPC applications. \
Please install ActiveSync and reinstall OpenGeoDB Mobile."
End:

StrCpy $0 "$INSTDIR\Setup.ini"
Call InstallCAB

SectionEnd ; end of default section



; will insert a divider between the two sections with the "Optional" text on it
;SectionDivider "Optional" 



Section "DotNetCF 2.0"
SectionIn 1
SetOutPath "$INSTDIR"

File SniffThat\InstallerFiles\DotNetCF\DotNetCF.ini
File SniffThat\InstallerFiles\DotNetCF\NETCFv2.wce5.armv4i.cab
File SniffThat\InstallerFiles\DotNetCF\NETCFv2.ppc.armv4.cab
File SniffThat\InstallerFiles\DotNetCF\NETCFv2.wm.armv4i.cab

StrCpy $0 "$INSTDIR\DotNetCF.ini"
Call InstallCAB

SectionEnd ; end of section 'Optional'


Section "-post" ; (post install section, happens last after any optional sections) ; add any commands that need to happen after any optional sections here
WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\LameSoft\SniffThat" "" "$INSTDIR"
WriteRegStr HKEY_LOCAL_MACHINE "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "${PRODUCT_NAME}" "${PRODUCT_NAME} (remove only)"
WriteRegStr HKEY_LOCAL_MACHINE "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "${PRODUCT_NAME}" '"$INSTDIR\uninst.exe"'
  
; write out uninstaller
WriteUninstaller "$INSTDIR\uninst.exe"

;MessageBox MB_YESNO|MB_ICONQUESTION \
;  "Setup has completed. View readme file now?" \
;  IDNO NoReadme
    
;  ExecShell open '$INSTDIR\readme.html'

;  NoReadme:

  Quit
SectionEnd ; end of -post section



ShowInstDetails nevershow ;never show installation details

; begin uninstall settings/section
UninstallText "${PRODUCT_NAME} wird von Ihrem System entfernt"


Section Uninstall
; add delete commands to delete whatever files/registry keys/etc you installed here.
Delete "$INSTDIR\*.*"

DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\LameSoft\SniffThat"
DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"

RMDir "$INSTDIR"
SectionEnd ; end of uninstall section



; Installs a PocketPC cab-application
; It expects $0 to contain the absolute location of the ini file
; to be installed.
Function InstallCAB

  ExecWait '"$1" "$0"'

FunctionEnd


; eof
