Imports System.IO

Public Class Main
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim reg As Microsoft.Win32.RegistryKey
        Dim U2L As Microsoft.Win32.RegistryKey
        U2L =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\U2L\\WAWPatcher\\3.0", True)
        If U2L Is Nothing Then
            U2L =
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\U2L\\WAWPatcher\\3.0")
            U2L.SetValue("Version", "3.0.2.2")
        End If
        U2L.SetValue("LastRunDir", Path.GetDirectoryName(Application.ExecutablePath))
        If Directory.Exists(Application.ExecutablePath) Then
            MsgBox("Der Ordner Existiert")
        End If
        If GetCpuMode() = "64Bit System" Then
            reg =
             Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\WoW6432Node\\Activision\\Call of Duty WAW", True)
            If reg Is Nothing Then
                MsgBox("Das Spiel wurde nicht gefunden oder nicht richtig installiert", MsgBoxStyle.Information, "Nicht Gefunden")
                Me.Close()
            Else
                VNr.Text = CStr(reg.GetValue("Version", True))
                If reg.GetValue("Language") Is "enu" Then
                    MsgBox("Es wurde die englische Version gefunden", MsgBoxStyle.Information, "Spiel wurde gefunden")
                ElseIf reg.GetValue("Language") = Nothing Then
                    MsgBox("Es wurde keine englische Version erkannt" & vbNewLine & "Damit das Programm richtig funktioniert, muss die englische Version installiert sein." & vbNewLine & vbNewLine & "Deshalb wird das Programm nun beendet.", MsgBoxStyle.Critical, "Falsche Spieleversion")
                    Me.Close()
                End If
            End If
        ElseIf GetCpuMode() = "32Bit System" Then
            reg =
     Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Activision\\Call of Duty WAW", True)
            If reg Is Nothing Then
                MsgBox("Das Spiel wurde nicht gefunden oder nicht richtig installiert", MsgBoxStyle.Information, "Spiel nicht gefunden")
                Me.Close()
            Else
                VNr.Text = CStr(reg.GetValue("Version", True))
                If reg.GetValue("Language") Is "enu" Then
                    MsgBox("Es wurde die englische Version gefunden", MsgBoxStyle.Information, "Spiel wurde gefunden")
                ElseIf reg.GetValue("Language") = Nothing Then
                    MsgBox("Es wurde keine englische Version erkannt" & vbNewLine & "Damit das Programm richtig funktioniert, muss die englische Version installiert sein." & vbNewLine & vbNewLine & "Deshalb wird das Programm nun beendet.", MsgBoxStyle.Critical, "Falsche Spieleversion")
                    Me.Close()
                End If
            End If
        End If
        Progress.Minimum = 0
        Progress.Maximum = 100
        Progress.Value = 0
        If VNr.Text = "1.7" Then
            action_patch.Enabled = False
            MsgBox("Das Spiel wurde bereits auf die Version 1.7 gepatcht", MsgBoxStyle.Information, "Spiel bereits gepatcht")
        End If
    End Sub
    Private Function GetCpuMode() As String
        '32Bit IntPtr.Size = 4 [ 4 * 8] = 32
        '64Bit IntPtr.Size = 8 [ 8 * 8] = 64
        Return If(IntPtr.Size = 8, "64Bit System", "32Bit System")
    End Function

    Private Sub Label1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles about_label.Click
        Dim gamename As String
        gamename = "'Call of Duty: World At War'"
        MsgBox(ProductName & vbNewLine & "Ein einfaches Tool um " & gamename & " auf die Version 1.7 zu patchen." & vbNewLine & vbNewLine & "Du verwendest ein " & GetCpuMode(), MsgBoxStyle.Information, "Info")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles action_patch.Click
        BGW1.RunWorkerAsync()
        If BGW1.IsBusy = True Then
            action_patch.Enabled = False
            action_patch.Text = "warten..."
        End If
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGW1.ProgressChanged
        Dim gamename As String
        gamename = "'Call of Duty: World At War'"
        Dim reg As Microsoft.Win32.RegistryKey
        If GetCpuMode() = "64Bit System" Then
            reg =
             Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\WoW6432Node\\Activision\\Call of Duty WAW", True)
        ElseIf GetCpuMode() = "32Bit System" Then
            reg =
             Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Activision\\Call of Duty WAW", True)
        End If
        Progress.Value = e.ProgressPercentage
        If Progress.Value = 100 Then
            MsgBox(gamename & " wurde auf die Version 1.7 gepatcht.", MsgBoxStyle.Information, "Erfolgreich")
            VNr.Text = reg.GetValue("Version", True)
            action_patch.Text = "Fertig"
        End If
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BGW1.DoWork
        Dim gamename As String
        gamename = "'Call of Duty: World At War'"
        Dim reg As Microsoft.Win32.RegistryKey
        Dim gamepath As String = ""
        If GetCpuMode() = "64Bit System" Then
            reg = _
             Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\WoW6432Node\\Activision\\Call of Duty WAW", True)
            gamepath = CStr(reg.GetValue("InstallPath", True))
        ElseIf GetCpuMode() = "32Bit System" Then
            reg = _
             Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Activision\\Call of Duty WAW", True)
            gamepath = CStr(reg.GetValue("InstallPath", True))
        End If
        File.Copy("res\version.u2l", gamepath & "version.inf", True)
        BGW1.ReportProgress((1 / 78) * 100)
        File.Copy("res\GameSP.u2l", gamepath & "CoDWaW.exe", True)
        BGW1.ReportProgress((2 / 78) * 100)
        File.Copy("res\GameMP.u2l", gamepath & "CoDWaWmp.exe", True)
        BGW1.ReportProgress((3 / 78) * 100)
        File.Copy("res\main\iw_21.u2l", gamepath & "main\iw_21.iwd", True)
        BGW1.ReportProgress((4 / 78) * 100)
        File.Copy("res\main\iw_22.u2l", gamepath & "main\iw_22.iwd", True)
        BGW1.ReportProgress((5 / 78) * 100)
        File.Copy("res\main\iw_23.u2l", gamepath & "main\iw_23.iwd", True)
        BGW1.ReportProgress((6 / 78) * 100)
        File.Copy("res\main\iw_24.u2l", gamepath & "main\iw_24.iwd", True)
        BGW1.ReportProgress((7 / 78) * 100)
        File.Copy("res\main\iw_25.u2l", gamepath & "main\iw_25.iwd", True)
        BGW1.ReportProgress((8 / 78) * 100)
        File.Copy("res\main\iw_26.u2l", gamepath & "main\iw_26.iwd", True)
        BGW1.ReportProgress((9 / 78) * 100)
        File.Copy("res\main\iw_27.u2l", gamepath & "main\iw_27.iwd", True)
        BGW1.ReportProgress((10 / 78) * 100)
        File.Copy("res\main\localized_english_iw04.u2l", gamepath & "main\localized_english_iw04.iwd", True)
        BGW1.ReportProgress((11 / 78) * 100)
        File.Copy("res\main\localized_english_iw05.u2l", gamepath & "main\localized_english_iw05.iwd", True)
        BGW1.ReportProgress((12 / 78) * 100)
        File.Copy("res\main\localized_english_iw06.u2l", gamepath & "main\localized_english_iw06.iwd", True)
        BGW1.ReportProgress((13 / 78) * 100)
        File.Copy("res\main\video\nazi_zombie_asylum_load.u2l", gamepath & "main\video\nazi_zombie_asylum_load.bik", True)
        BGW1.ReportProgress((14 / 78) * 100)
        File.Copy("res\main\video\nazi_zombie_factory_load.u2l", gamepath & "main\video\nazi_zombie_factory_load.bik", True)
        BGW1.ReportProgress((15 / 78) * 100)
        File.Copy("res\main\video\nazi_zombie_sumpf_load.u2l", gamepath & "main\video\nazi_zombie_sumpf_load.bik", True)
        BGW1.ReportProgress((16 / 78) * 100)
        File.Copy("res\zone\english\default.u2l", gamepath & "zone\english\default.ff", True)
        BGW1.ReportProgress((17 / 78) * 100)
        File.Copy("res\zone\english\patch.u2l", gamepath & "zone\english\patch.ff", True)
        BGW1.ReportProgress((18 / 78) * 100)
        File.Copy("res\zone\english\patch_mp.u2l", gamepath & "zone\english\patch_mp.ff", True)
        BGW1.ReportProgress((19 / 78) * 100)
        File.Copy("res\zone\english\ui_mp.u2l", gamepath & "zone\english\ui_mp.ff", True)
        BGW1.ReportProgress((20 / 78) * 100)
        File.Copy("res\zone\english\common_mp.u2l", gamepath & "zone\english\common_mp.ff", True)
        BGW1.ReportProgress((21 / 78) * 100)
        File.Copy("res\zone\english\code_post_gfx_mp.u2l", gamepath & "zone\english\code_post_gfx_mp.ff", True)
        BGW1.ReportProgress((22 / 78) * 100)
        File.Copy("res\zone\english\localized_code_post_gfx_mp.u2l", gamepath & "zone\english\localized_code_post_gfx_mp.ff", True)
        BGW1.ReportProgress((23 / 78) * 100)
        File.Copy("res\zone\english\localized_common_mp.u2l", gamepath & "zone\english\localized_common_mp.ff", True)
        BGW1.ReportProgress((24 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_bgate.u2l", gamepath & "zone\english\localized_mp_bgate.ff", True)
        BGW1.ReportProgress((25 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_docks.u2l", gamepath & "zone\english\localized_mp_docks.ff", True)
        BGW1.ReportProgress((26 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_drum.u2l", gamepath & "zone\english\localized_mp_drum.ff", True)
        BGW1.ReportProgress((27 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_kneedeep.u2l", gamepath & "zone\english\localized_mp_kneedeep.ff", True)
        BGW1.ReportProgress((28 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_kwai.u2l", gamepath & "zone\english\localized_mp_kwai.ff", True)
        BGW1.ReportProgress((29 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_makin_day.u2l", gamepath & "zone\english\localized_mp_makin_day.ff", True)
        BGW1.ReportProgress((30 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_nachtfeuer.u2l", gamepath & "zone\english\localized_mp_nachtfeuer.ff", True)
        BGW1.ReportProgress((31 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_stalingrad.u2l", gamepath & "zone\english\localized_mp_stalingrad.ff", True)
        BGW1.ReportProgress((32 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_subway.u2l", gamepath & "zone\english\localized_mp_subway.ff", True)
        BGW1.ReportProgress((33 / 78) * 100)
        File.Copy("res\zone\english\localized_mp_vodka.u2l", gamepath & "zone\english\localized_mp_vodka.ff", True)
        BGW1.ReportProgress((34 / 78) * 100)
        File.Copy("res\zone\english\localized_nazi_zombie_asylum.u2l", gamepath & "zone\english\localized_nazi_zombie_asylum.ff", True)
        BGW1.ReportProgress((35 / 78) * 100)
        File.Copy("res\zone\english\localized_nazi_zombie_factory.u2l", gamepath & "zone\english\localized_nazi_zombie_factory.ff", True)
        BGW1.ReportProgress((36 / 78) * 100)
        File.Copy("res\zone\english\localized_nazi_zombie_sumpf.u2l", gamepath & "zone\english\localized_nazi_zombie_sumpf.ff", True)
        BGW1.ReportProgress((37 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_asylum.u2l", gamepath & "zone\english\nazi_zombie_asylum.ff", True)
        BGW1.ReportProgress((38 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_asylum_load.u2l", gamepath & "zone\english\nazi_zombie_asylum_load.ff", True)
        BGW1.ReportProgress((39 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_asylum_patch.u2l", gamepath & "zone\english\nazi_zombie_asylum_patch.ff", True)
        BGW1.ReportProgress((40 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_factory.u2l", gamepath & "zone\english\nazi_zombie_factory.ff", True)
        BGW1.ReportProgress((41 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_factory_load.u2l", gamepath & "zone\english\nazi_zombie_factory_load.ff", True)
        BGW1.ReportProgress((42 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_factory_patch.u2l", gamepath & "zone\english\nazi_zombie_factory_patch.ff", True)
        BGW1.ReportProgress((43 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_sumpf.u2l", gamepath & "zone\english\nazi_zombie_sumpf.ff", True)
        BGW1.ReportProgress((44 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_sumpf_load.u2l", gamepath & "zone\english\nazi_zombie_sumpf_load.ff", True)
        BGW1.ReportProgress((45 / 78) * 100)
        File.Copy("res\zone\english\nazi_zombie_sumpf_patch.u2l", gamepath & "zone\english\nazi_zombie_sumpf_patch.ff", True)
        BGW1.ReportProgress((46 / 78) * 100)
        File.Copy("res\zone\english\mp_airfield.u2l", gamepath & "zone\english\mp_airfield.ff", True)
        BGW1.ReportProgress((47 / 78) * 100)
        File.Copy("res\zone\english\mp_asylum.u2l", gamepath & "zone\english\mp_asylum.ff", True)
        BGW1.ReportProgress((48 / 78) * 100)
        File.Copy("res\zone\english\mp_bgate.u2l", gamepath & "zone\english\mp_bgate.ff", True)
        BGW1.ReportProgress((49 / 78) * 100)
        File.Copy("res\zone\english\mp_bgate_load.u2l", gamepath & "zone\english\mp_bgate_load.ff", True)
        BGW1.ReportProgress((50 / 78) * 100)
        File.Copy("res\zone\english\mp_castle.u2l", gamepath & "zone\english\mp_castle.ff", True)
        BGW1.ReportProgress((51 / 78) * 100)
        File.Copy("res\zone\english\mp_courtyard.u2l", gamepath & "zone\english\mp_courtyard.ff", True)
        BGW1.ReportProgress((52 / 78) * 100)
        File.Copy("res\zone\english\mp_docks.u2l", gamepath & "zone\english\mp_docks.ff", True)
        BGW1.ReportProgress((53 / 78) * 100)
        File.Copy("res\zone\english\mp_docks_load.u2l", gamepath & "zone\english\mp_docks_load.ff", True)
        BGW1.ReportProgress((54 / 78) * 100)
        File.Copy("res\zone\english\mp_downfall.u2l", gamepath & "zone\english\mp_downfall.ff", True)
        BGW1.ReportProgress((55 / 78) * 100)
        File.Copy("res\zone\english\mp_drum.u2l", gamepath & "zone\english\mp_drum.ff", True)
        BGW1.ReportProgress((56 / 78) * 100)
        File.Copy("res\zone\english\mp_drum_load.u2l", gamepath & "zone\english\mp_drum_load.ff", True)
        BGW1.ReportProgress((57 / 78) * 100)
        File.Copy("res\zone\english\mp_hangar.u2l", gamepath & "zone\english\mp_hangar.ff", True)
        BGW1.ReportProgress((58 / 78) * 100)
        File.Copy("res\zone\english\mp_kneedeep.u2l", gamepath & "zone\english\mp_kneedeep.ff", True)
        BGW1.ReportProgress((59 / 78) * 100)
        File.Copy("res\zone\english\mp_kneedeep_load.u2l", gamepath & "zone\english\mp_kneedeep_load.ff", True)
        BGW1.ReportProgress((60 / 78) * 100)
        File.Copy("res\zone\english\mp_kwai.u2l", gamepath & "zone\english\mp_kwai.ff", True)
        BGW1.ReportProgress((61 / 78) * 100)
        File.Copy("res\zone\english\mp_kwai_load.u2l", gamepath & "zone\english\mp_kwai_load.ff", True)
        BGW1.ReportProgress((62 / 78) * 100)
        File.Copy("res\zone\english\mp_makin.u2l", gamepath & "zone\english\mp_makin.ff", True)
        BGW1.ReportProgress((63 / 78) * 100)
        File.Copy("res\zone\english\mp_makin_day.u2l", gamepath & "zone\english\mp_makin_day.ff", True)
        BGW1.ReportProgress((64 / 78) * 100)
        File.Copy("res\zone\english\mp_makin_day_load.u2l", gamepath & "zone\english\mp_makin_day_load.ff", True)
        BGW1.ReportProgress((65 / 78) * 100)
        File.Copy("res\zone\english\mp_nachtfeuer.u2l", gamepath & "zone\english\mp_nachtfeuer.ff", True)
        BGW1.ReportProgress((66 / 78) * 100)
        File.Copy("res\zone\english\mp_nachtfeuer_load.u2l", gamepath & "zone\english\mp_nachtfeuer_load.ff", True)
        BGW1.ReportProgress((67 / 78) * 100)
        File.Copy("res\zone\english\mp_outskirts.u2l", gamepath & "zone\english\mp_outskirts.ff", True)
        BGW1.ReportProgress((68 / 78) * 100)
        File.Copy("res\zone\english\mp_roundhouse.u2l", gamepath & "zone\english\mp_roundhouse.ff", True)
        BGW1.ReportProgress((69 / 78) * 100)
        File.Copy("res\zone\english\mp_seelow.u2l", gamepath & "zone\english\mp_seelow.ff", True)
        BGW1.ReportProgress((70 / 78) * 100)
        File.Copy("res\zone\english\mp_shrine.u2l", gamepath & "zone\english\mp_shrine.ff", True)
        BGW1.ReportProgress((71 / 78) * 100)
        File.Copy("res\zone\english\mp_stalingrad.u2l", gamepath & "zone\english\mp_stalingrad.ff", True)
        BGW1.ReportProgress((72 / 78) * 100)
        File.Copy("res\zone\english\mp_stalingrad_load.u2l", gamepath & "zone\english\mp_stalingrad_load.ff", True)
        BGW1.ReportProgress((73 / 78) * 100)
        File.Copy("res\zone\english\mp_suburban.u2l", gamepath & "zone\english\mp_suburban.ff", True)
        BGW1.ReportProgress((74 / 78) * 100)
        File.Copy("res\zone\english\mp_subway.u2l", gamepath & "zone\english\mp_subway.ff", True)
        BGW1.ReportProgress((75 / 78) * 100)
        File.Copy("res\zone\english\mp_subway_load.u2l", gamepath & "zone\english\mp_subway_load.ff", True)
        BGW1.ReportProgress((76 / 78) * 100)
        File.Copy("res\zone\english\mp_vodka.u2l", gamepath & "zone\english\mp_vodka.ff", True)
        BGW1.ReportProgress((77 / 78) * 100)
        File.Copy("res\zone\english\mp_vodka_load.u2l", gamepath & "zone\english\mp_vodka_load.ff", True)
        BGW1.ReportProgress((78 / 78) * 100)
        reg.SetValue("version", "1.7")
        BGW1.Dispose()
    End Sub
End Class
