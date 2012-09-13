Option Strict On
Imports System
Imports System.IO
Imports Microsoft.Win32.Registry


Public Class Form1
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '        Dim reg As Microsoft.Win32.RegistryKey
        '       reg = _
        '      Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\WoW6432Node\\Activision\\Call of Duty WAW", True)
        '     If reg Is Nothing Then
        'MsgBox("Das Spiel wurde nicht gefunden", MsgBoxStyle.Information, "nööö")
        'Me.Close()
        'Else
        'MsgBox("Das Spiel wurde gefunden", MsgBoxStyle.Information, "gefunden")
        'End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        MsgBox(ProductName & vbNewLine & "a Simple Tool for patching World At War to Version 1.7" & vbNewLine & "-built for testing purposes only", MsgBoxStyle.Information, "about")
    End Sub
End Class
