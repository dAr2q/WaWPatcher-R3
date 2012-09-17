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
    Private Function GetCpuMode() As String
        '32Bit IntPtr.Size = 4 [ 4 * 8] = 32
        '64Bit IntPtr.Size = 8 [ 8 * 8] = 64
        Return If(IntPtr.Size = 8, "64Bit System", "32Bit System")
    End Function

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        MsgBox(ProductName & vbNewLine & "a Simple Tool for patching World At War to Version 1.7" & vbNewLine & "-built for testing purposes only" & vbNewLine & "Es wird ein " & GetCpuMode() & " verwendet", MsgBoxStyle.Information, "about")
    End Sub
End Class
