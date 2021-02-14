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
        Me.chkStructuredAppend = New System.Windows.Forms.CheckBox()
        Me.lblData = New System.Windows.Forms.Label()
        Me.cmbCharset = New System.Windows.Forms.ComboBox()
        Me.lblCharset = New System.Windows.Forms.Label()
        Me.nudModuleSize = New System.Windows.Forms.NumericUpDown()
        CType(Me.nudModuleSize,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'txtData
        '
        Me.txtData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtData.Location = New System.Drawing.Point(14, 447)
        Me.txtData.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtData.Multiline = true
        Me.txtData.Name = "txtData"
        Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtData.Size = New System.Drawing.Size(656, 130)
        Me.txtData.TabIndex = 0
        Me.txtData.WordWrap = false
        '
        'qrcodePanel
        '
        Me.qrcodePanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.qrcodePanel.AutoScroll = true
        Me.qrcodePanel.Location = New System.Drawing.Point(14, 17)
        Me.qrcodePanel.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.qrcodePanel.Name = "qrcodePanel"
        Me.qrcodePanel.Size = New System.Drawing.Size(656, 405)
        Me.qrcodePanel.TabIndex = 11
        '
        'lblErrorCorrectionLevel
        '
        Me.lblErrorCorrectionLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblErrorCorrectionLevel.AutoSize = true
        Me.lblErrorCorrectionLevel.Location = New System.Drawing.Point(10, 593)
        Me.lblErrorCorrectionLevel.Name = "lblErrorCorrectionLevel"
        Me.lblErrorCorrectionLevel.Size = New System.Drawing.Size(143, 17)
        Me.lblErrorCorrectionLevel.TabIndex = 1
        Me.lblErrorCorrectionLevel.Text = "Error Correction &Level :"
        '
        'lblMaxVersion
        '
        Me.lblMaxVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblMaxVersion.AutoSize = true
        Me.lblMaxVersion.Location = New System.Drawing.Point(10, 627)
        Me.lblMaxVersion.Name = "lblMaxVersion"
        Me.lblMaxVersion.Size = New System.Drawing.Size(87, 17)
        Me.lblMaxVersion.TabIndex = 5
        Me.lblMaxVersion.Text = "Max &Version :"
        '
        'lbModuleSize
        '
        Me.lbModuleSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lbModuleSize.AutoSize = true
        Me.lbModuleSize.Location = New System.Drawing.Point(391, 627)
        Me.lbModuleSize.Name = "lbModuleSize"
        Me.lbModuleSize.Size = New System.Drawing.Size(87, 17)
        Me.lbModuleSize.TabIndex = 8
        Me.lbModuleSize.Text = "&Module Size :"
        '
        'cmbErrorCorrectionLevel
        '
        Me.cmbErrorCorrectionLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbErrorCorrectionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbErrorCorrectionLevel.FormattingEnabled = true
        Me.cmbErrorCorrectionLevel.Location = New System.Drawing.Point(159, 589)
        Me.cmbErrorCorrectionLevel.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.cmbErrorCorrectionLevel.Name = "cmbErrorCorrectionLevel"
        Me.cmbErrorCorrectionLevel.Size = New System.Drawing.Size(55, 25)
        Me.cmbErrorCorrectionLevel.TabIndex = 2
        '
        'cmbMaxVersion
        '
        Me.cmbMaxVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbMaxVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaxVersion.FormattingEnabled = true
        Me.cmbMaxVersion.Location = New System.Drawing.Point(159, 623)
        Me.cmbMaxVersion.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.cmbMaxVersion.Name = "cmbMaxVersion"
        Me.cmbMaxVersion.Size = New System.Drawing.Size(55, 25)
        Me.cmbMaxVersion.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(552, 623)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(118, 25)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = true
        '
        'chkStructuredAppend
        '
        Me.chkStructuredAppend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.chkStructuredAppend.AutoSize = true
        Me.chkStructuredAppend.Location = New System.Drawing.Point(237, 626)
        Me.chkStructuredAppend.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.chkStructuredAppend.Name = "chkStructuredAppend"
        Me.chkStructuredAppend.Size = New System.Drawing.Size(137, 21)
        Me.chkStructuredAppend.TabIndex = 7
        Me.chkStructuredAppend.Text = "Structured &Append"
        Me.chkStructuredAppend.UseVisualStyleBackColor = true
        '
        'lblData
        '
        Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblData.AutoSize = true
        Me.lblData.Location = New System.Drawing.Point(10, 424)
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New System.Drawing.Size(42, 17)
        Me.lblData.TabIndex = 12
        Me.lblData.Text = "&Data :"
        '
        'cmbCharset
        '
        Me.cmbCharset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cmbCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCharset.FormattingEnabled = true
        Me.cmbCharset.Location = New System.Drawing.Point(302, 589)
        Me.cmbCharset.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.cmbCharset.Name = "cmbCharset"
        Me.cmbCharset.Size = New System.Drawing.Size(224, 25)
        Me.cmbCharset.TabIndex = 4
        '
        'lblCharset
        '
        Me.lblCharset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblCharset.AutoSize = true
        Me.lblCharset.Location = New System.Drawing.Point(237, 593)
        Me.lblCharset.Name = "lblCharset"
        Me.lblCharset.Size = New System.Drawing.Size(59, 17)
        Me.lblCharset.TabIndex = 3
        Me.lblCharset.Text = "&Charset :"
        '
        'nudModuleSize
        '
        Me.nudModuleSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.nudModuleSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.nudModuleSize.Location = New System.Drawing.Point(486, 623)
        Me.nudModuleSize.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.nudModuleSize.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nudModuleSize.Name = "nudModuleSize"
        Me.nudModuleSize.Size = New System.Drawing.Size(40, 25)
        Me.nudModuleSize.TabIndex = 9
        Me.nudModuleSize.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 17!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 661)
        Me.Controls.Add(Me.cmbCharset)
        Me.Controls.Add(Me.lblCharset)
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
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
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
    Friend WithEvents chkStructuredAppend As CheckBox
    Private WithEvents lblData As Label
    Private WithEvents txtData As TextBox
    Private WithEvents cmbCharset As ComboBox
    Private WithEvents lblCharset As Label
    Friend WithEvents nudModuleSize As NumericUpDown
End Class
