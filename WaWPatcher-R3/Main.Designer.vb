<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.VNr = New System.Windows.Forms.TextBox()
        Me.about_label = New System.Windows.Forms.Label()
        Me.Version = New System.Windows.Forms.Label()
        Me.action_patch = New System.Windows.Forms.Button()
        Me.BGW1 = New System.ComponentModel.BackgroundWorker()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'VNr
        '
        Me.VNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VNr.Enabled = False
        Me.VNr.Location = New System.Drawing.Point(13, 63)
        Me.VNr.MaxLength = 3
        Me.VNr.Name = "VNr"
        Me.VNr.Size = New System.Drawing.Size(39, 20)
        Me.VNr.TabIndex = 0
        Me.VNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'about_label
        '
        Me.about_label.AutoSize = True
        Me.about_label.Location = New System.Drawing.Point(276, 89)
        Me.about_label.Name = "about_label"
        Me.about_label.Size = New System.Drawing.Size(34, 13)
        Me.about_label.TabIndex = 1
        Me.about_label.Text = "about"
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Location = New System.Drawing.Point(12, 47)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(42, 13)
        Me.Version.TabIndex = 2
        Me.Version.Text = "Version"
        '
        'action_patch
        '
        Me.action_patch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.action_patch.Location = New System.Drawing.Point(114, 68)
        Me.action_patch.Name = "action_patch"
        Me.action_patch.Size = New System.Drawing.Size(75, 23)
        Me.action_patch.TabIndex = 4
        Me.action_patch.Text = "Patch"
        Me.action_patch.UseVisualStyleBackColor = True
        '
        'BGW1
        '
        Me.BGW1.WorkerReportsProgress = True
        '
        'Progress
        '
        Me.Progress.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Progress.Location = New System.Drawing.Point(13, 13)
        Me.Progress.MarqueeAnimationSpeed = 50
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(285, 23)
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 5
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(310, 103)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.action_patch)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.about_label)
        Me.Controls.Add(Me.VNr)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "WaWPatcher-R3b"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VNr As System.Windows.Forms.TextBox
    Friend WithEvents about_label As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents action_patch As System.Windows.Forms.Button
    Friend WithEvents BGW1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar

End Class
