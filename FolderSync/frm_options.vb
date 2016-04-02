﻿Imports System.Windows.Forms

Public Class frm_options

  Private Sub frm_options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    glob.readTuttiFrutti(Me)
    glob.readFormPos(Me, False)
    Me.Size = New Size(874, 406)
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
    save()
  End Sub

  Private Sub btnHelp1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim tx As String = "Um die Shortcuts aufzurufen, drücke ..." + vbNewLine + _
    "* [STRG] + [F-Taste] um das 'Lokal Upload'-Treeview zu navigieren oder" + vbNewLine + _
    "*   [ALT] + [F-Taste] um das 'Lokal Download'-Treeview zu navigieren"

    MessageBox.Show(tx, "Benutzung der Shortcut-Tasten", MessageBoxButtons.OK, MessageBoxIcon.Information)

  End Sub

  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    Me.Close()

  End Sub
  Sub save()
    glob.saveTuttiFrutti(Me)
    glob.saveFormPos(Me)
    frm_main.initShortcutButtons()
    frm_main.initSplitterDesign()
    FTP = New Utilities.FTP.FTPclient(glob.para("frm_options__ftp_host"), glob.para("frm_options__ftp_user"), glob.para("frm_options__ftp_pass"))

  End Sub
  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    save()
    Me.Close()

  End Sub

  Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    With fbd1
      .Description = "Bitte wähle den Backupordner aus..."
      .SelectedPath = backupFolder.Text
      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        backupFolder.Text = .SelectedPath
      End If
    End With
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    With OpenFileDialog1
      .Filter = "Wave-Sounddateien (.wav)|*.wav"
      .FileName = If(finish_soundfile.Text <> "", finish_soundfile.Text, "C:\windows\media\ding.wav")
      If .ShowDialog = Windows.Forms.DialogResult.OK Then
        finish_soundfile.Text = .FileName
      End If
    End With
  End Sub

  Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    Dim parts() As String = New String() {"[fsPwdList", dirlist_url.Text, twajax_url.Text, ftp_host.Text, ftp_dir.Text, ftp_user.Text, ftp_pass.Text, "eot]"}
    Clipboard.Clear()
    Dim out = Join(parts, "$fs$")

    Clipboard.SetText(out)

  End Sub

  Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
    Dim parts() = Split(Clipboard.GetText, "$fs$")
    If parts.Length <> 8 OrElse parts(0) <> "[fsPwdList" OrElse parts(7) <> "eot]" Then
      MsgBox("ungültiges Format")
      Exit Sub
    End If
    dirlist_url.Text = parts(1)
    twajax_url.Text = parts(2)
    ftp_host.Text = parts(3)
    ftp_dir.Text = parts(4)
    ftp_user.Text = parts(5)
    ftp_pass.Text = parts(6)


  End Sub
End Class
