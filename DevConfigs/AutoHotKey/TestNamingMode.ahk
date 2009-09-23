;=======================================================================================
;BDD Test Naming Mode AHK Script
;
;Description:
;  Replaces spaces with underscores while typing, to help with writing BDD test names.
;  Toggle on and off with Ctrl + Shift + U.
;=======================================================================================


;==========================
;Initialise
;==========================
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

enabledIcon := "testnamingmode_16.ico"
disabledIcon := "testnamingmode_disabled_16.ico"
IsInTestNamingMode := false
IsInWhenMode := false
SetTestNamingMode(false)

;==========================
;Functions
;==========================
SetTestNamingMode(toActive) {
  local iconFile := toActive ? enabledIcon : disabledIcon
  local state := toActive ? "ON" : "OFF"

  IsInTestNamingMode := toActive
  Menu, Tray, Icon, %iconFile%,
  Menu, Tray, Tip, Test naming mode is %state%  

  Send {Shift Up}
}

;==========================
;Test Mode toggle
;==========================
^+u::
  SetTestNamingMode(!IsInTestNamingMode)
return

;==========================
;then hot word
;==========================
::then::then^+u
return
 
;==========================
;return cancels toggle
;==========================
$Enter::
if (IsInTestNamingMode) {
  SetTestNamingMode(!IsInTestNamingMode)
  Send, {Enter}
} else {
  Send, {Enter}
}
return

$Escape::
if (IsInTestNamingMode) {
SetTestNamingMode(!IsInTestNamingMode)
}
Send, {Escape}
return

;==========================
;Handle SPACE press
;==========================
$Space::
  if (IsInTestNamingMode) {
    Send, _
  } else {
    Send, {Space}
  }

