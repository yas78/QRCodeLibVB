<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtData = New System.Windows.Forms.TextBox()
        Me.qrcodePanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblErrorCorrectionLevel = New System.Windows.Forms.Label()
        Me.lblMaxVersion = New System.Windows.Forms.Label()
        Me.lbModuleSize = New System.Windows.Forms.Label()
        Me.cmbErrorCorrectionLevel = New System.Windows.Forms.ComboBox()
        Me.cmbMaxVersion = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.nudModuleSize = New System.Windows.Forms.NumericUpDown()
        Me.chkStructuredAppend = New System.Windows.Forms.CheckBox()
        Me.lblData = New System.Windows.Forms.Label()
        Me.cmbEncoding = New System.Windows.Forms.ComboBox()
        Me.lblEncoding = New System.Windows.Forms.Label()
        CType(Me.nudModuleSize,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'txtData
        '
        Me.txtData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtData.Location = New System.Drawing.Point(12, 355)
        Me.txtData.Multiline = true
        Me.txtData.Name = "txtData"
        Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtData.Size = New System.Drawing.Size(660, 85)
        Me.txtData.TabIndex = 0
        Me.txtData.WordWrap = false
        '
        'qrcodePanel
        '
        Me.qrcodePanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.qrcodePanel.AutoScroll = true
        Me.qrcodePanel.Location = New System.Drawing.Point(12, 12)
        Me.qrcodePanel.Name = "qrcodePanel"
        Me.qrcodePanel.Size = New System.Drawing.Size(660, 319)
        Me.qrcodePanel.TabIndex = 11
        '
        'lblErrorCorrectionLevel
        '
        Me.lblErrorCorrectionLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblErrorCorrectionLevel.AutoSize = true
        Me.lblErrorCorrectionLevel.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblErrorCorrectionLevel.Location = New System.Drawing.Point(9, 452)
        Me.lblErrorCorrectionLevel.Name = "lblErrorCorrectionLevel"
        Me.lblErrorCorrectionLevel.Size = New System.Drawing.Size(143, 13)
        Me.lblErrorCorrectionLevel.TabIndex = 1
        Me.lblErrorCorrectionLevel.Text = "Error Correction Level :"
        '
        'lblMaxVersion
        '
        Me.lblMaxVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblMaxVersion.AutoSize = true
        Me.lblMaxVersion.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblMaxVersion.Location = New System.Drawing.Point(9, 482)
        Me.lblMaxVersion.Name = "lblMaxVersion"
        Me.lblMaxVersion.Size = New System.Drawing.Size(83, 13)
        Me.lblMaxVersion.TabIndex = 5
        Me.lblMaxVersion.Text = "Max Version :"
        '
        'lbModuleSize
        '
        Me.lbModuleSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lbModuleSize.AutoSize = true
        Me.lbModuleSize.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lbModuleSize.Location = New System.Drawing.Point(384, 482)
        Me.lbModuleSize.Name = "lbModuleSize"
        Me.lbModuleSize.Size = New System.Drawing.Size(82, 13)
        Me.lbModuleSize.TabIndex = 8
        Me.lbModuleSize.Text = "Module Size :"
        '
        'cmbErrorCorrectionLevel
        '
        Me.cmbErrorCorrectionLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbErrorCorrectionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbErrorCorrectionLevel.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.cmbErrorCorrectionLevel.FormattingEnabled = true
        Me.cmbErrorCorrectionLevel.Location = New System.Drawing.Point(164, 448)
        Me.cmbErrorCorrectionLevel.Name = "cmbErrorCorrectionLevel"
        Me.cmbErrorCorrectionLevel.Size = New System.Drawing.Size(48, 21)
        Me.cmbErrorCorrectionLevel.TabIndex = 2
        '
        'cmbMaxVersion
        '
        Me.cmbMaxVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbMaxVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaxVersion.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.cmbMaxVersion.FormattingEnabled = true
        Me.cmbMaxVersion.Location = New System.Drawing.Point(164, 478)
        Me.cmbMaxVersion.Name = "cmbMaxVersion"
        Me.cmbMaxVersion.Size = New System.Drawing.Size(48, 21)
        Me.cmbMaxVersion.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.btnSave.Location = New System.Drawing.Point(548, 477)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(124, 23)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = true
        '
        'nudModuleSize
        '
        Me.nudModuleSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.nudModuleSize.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.nudModuleSize.Location = New System.Drawing.Point(472, 478)
        Me.nudModuleSize.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nudModuleSize.Name = "nudModuleSize"
        Me.nudModuleSize.Size = New System.Drawing.Size(46, 20)
        Me.nudModuleSize.TabIndex = 9
        Me.nudModuleSize.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'chkStructuredAppend
        '
        Me.chkStructuredAppend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.chkStructuredAppend.AutoSize = true
        Me.chkStructuredAppend.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.chkStructuredAppend.Location = New System.Drawing.Point(230, 480)
        Me.chkStructuredAppend.Name = "chkStructuredAppend"
        Me.chkStructuredAppend.Size = New System.Drawing.Size(132, 17)
        Me.chkStructuredAppend.TabIndex = 7
        Me.chkStructuredAppend.Text = "Structured Append"
        Me.chkStructuredAppend.UseVisualStyleBackColor = true
        '
        'lblData
        '
        Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblData.AutoSize = true
        Me.lblData.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblData.Location = New System.Drawing.Point(9, 339)
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New System.Drawing.Size(39, 13)
        Me.lblData.TabIndex = 12
        Me.lblData.Text = "Data :"
        '
        'cmbEncoding
        '
        Me.cmbEncoding.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEncoding.FormattingEnabled = true
        Me.cmbEncoding.Location = New System.Drawing.Point(352, 449)
        Me.cmbEncoding.Name = "cmbEncoding"
        Me.cmbEncoding.Size = New System.Drawing.Size(320, 20)
        Me.cmbEncoding.TabIndex = 4
        '
        'lblEncoding
        '
        Me.lblEncoding.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblEncoding.AutoSize = true
        Me.lblEncoding.Location = New System.Drawing.Point(230, 452)
        Me.lblEncoding.Name = "lblEncoding"
        Me.lblEncoding.Size = New System.Drawing.Size(116, 12)
        Me.lblEncoding.TabIndex = 3
        Me.lblEncoding.Text = "Byte mode Encoding :"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 511)
        Me.Controls.Add(Me.cmbEncoding)
        Me.Controls.Add(Me.lblEncoding)
        Me.Controls.Add(Me.lblData)
        Me.Controls.Add(Me.chkStructuredAppend)
        Me.Controls.Add(Me.nudModuleSize)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cmbMaxVersion)
        Me.Controls.Add(Me.cmbErrorCorrectionLevel)
        Me.Controls.Add(Me.lbModuleSize)
        Me.Controls.Add(Me.lblMaxVersion)
        Me.Controls.Add(Me.lblErrorCorrectionLevel)
        Me.Controls.Add(Me.qrcodePanel)
        Me.Controls.Add(Me.txtData)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QR Code"
        CType(Me.nudModuleSize,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Private WithEvents qrcodePanel As FlowLayoutPanel
    Friend WithEvents lblErrorCorrectionLevel As Label
    Friend WithEvents lblMaxVersion As Label
    Friend WithEvents lbModuleSize As Label
    Private WithEvents cmbErrorCorrectionLevel As ComboBox
    Friend WithEvents cmbMaxVersion As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents nudModuleSize As NumericUpDown
    Friend WithEvents chkStructuredAppend As CheckBox
    Private WithEvents lblData As Label
    Private WithEvents txtData As TextBox
    Private WithEvents cmbEncoding As ComboBox
    Private WithEvents lblEncoding As Label
End Class
