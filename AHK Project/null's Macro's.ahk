#NoEnv
SetBatchLines, -1

; Compile Me!

; This Neutron script contains many separate web files and dependencies, but can
; still be compiled into a portable EXE that won't have to extract anything to
; work.

; Include the Neutron library
#Include ../../Neutron.ahk

; Create a new NeutronWindow and navigate to our HTML page
neutron := new NeutronWindow()
neutron.Load("Bootstrap.html")

; Instead of using neutron's built in Close method, make the window close action
; call our Func_ExitApp.
neutron.Close := Func("Func_ExitApp")

; Show the Neutron window
neutron.Show()
return

; FileInstall all your dependencies, but put the FileInstall lines somewhere
; they won't ever be reached. Right below your AutoExecute section is a great
; location!
FileInstall, Bootstrap.html, Bootstrap.html
FileInstall, bootstrap.min.css, bootstrap.min.css
FileInstall, bootstrap.min.js, bootstrap.min.js
FileInstall, jquery.min.js, jquery.min.js

Func_ExitApp()
{
	ExitApp
}

; Begin Discord
DiscordOpen()
{
SetWorkingDir C:\Users\%A_UserName%\AppData\Local\Discord\app-0.0.305

global Ejecutable := "Discord.exe"

IfWinExist, ahk_exe %Ejecutable%
{
    WinActivate, ahk_exe %Ejecutable%
}
else
{
    Run, C:\Users\%A_UserName%\AppData\Local\Discord\Update.exe --processStart Discord.exe
}
}

; Begin Discord
DiscordInject()
{
global Ejecutable := "AHK Libs.exe"

IfWinExist, ahk_exe %Ejecutable%
{
    WinActivate, ahk_exe %Ejecutable%
}
else
{
	Run, %A_ScriptDir%\Libraries\AHK Libs.exe --processStart
}
}
; END Discord