Imports System
Imports System.IO
Imports System.Text

Imports Ys.QRCode

Public Class Form1

    Public Sub New()

        InitializeComponent()

    End Sub

    Private Sub UpdateQRCodePanel(sender As Object, e As EventArgs) _
        Handles txtData.TextChanged, nudModuleSize.ValueChanged, cmbMaxVersion.SelectedIndexChanged, cmbErrorCorrectionLevel.SelectedIndexChanged, chkStructuredAppend.CheckedChanged, cmbEncoding.SelectedIndexChanged

        btnSave.Enabled = False
        qrcodePanel.Controls.Clear()

        If String.IsNullOrEmpty(txtData.Text) Then
            Return
        End If

        Dim version As Integer = CInt(cmbMaxVersion.SelectedItem)
        Dim ecLevel As ErrorCorrectionLevel = CType(cmbErrorCorrectionLevel.SelectedItem, ErrorCorrectionLevel)
        Dim allowStructuredAppend As Boolean = chkStructuredAppend.Checked
        Dim encoding As Encoding = CType(cmbEncoding.SelectedItem, EncodingInfo).GetEncoding()

        Dim symbols As Symbols = New Symbols(version, ecLevel, allowStructuredAppend, encoding)

        Try
            symbols.AppendString(txtData.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return

        End Try

        For Each symbol As Symbol In symbols
            Dim image As Image = symbol.Get24bppImage(CInt(nudModuleSize.Value))

            Dim pictureBox As PictureBox = New PictureBox()
            pictureBox.Size = image.Size
            pictureBox.Image = image

            qrcodePanel.Controls.Add(pictureBox)
        Next

        btnSave.Enabled = txtData.TextLength > 0

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim baseName As String
        Dim isMonochrome As Boolean

        Using fd As SaveFileDialog = New SaveFileDialog()
            fd.Filter = "Monochrome Bitmap(*.bmp)|*.bmp|24-bit Bitmap(*.bmp)|*.bmp"

            If fd.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            isMonochrome = fd.FilterIndex = 1
            baseName = Path.Combine(
                Path.GetDirectoryName(fd.FileName), Path.GetFileNameWithoutExtension(fd.FileName))
        End Using

        Dim version As Integer = CInt(cmbMaxVersion.SelectedItem)
        Dim ecLevel As ErrorCorrectionLevel = CType(cmbErrorCorrectionLevel.SelectedItem, ErrorCorrectionLevel)
        Dim allowStructuredAppend As Boolean = chkStructuredAppend.Checked
        Dim encoding As Encoding = CType(cmbEncoding.SelectedItem, EncodingInfo).GetEncoding()

        Dim symbols As Symbols = New Symbols(version, ecLevel, allowStructuredAppend, encoding)

        Try
            symbols.AppendString(txtData.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return

        End Try

        For i As Integer = 0 To symbols.Count - 1
            Dim filename As String

            If symbols.Count = 1 Then
                filename = baseName & ".bmp"
            Else
                filename = baseName & "_" & CStr(i + 1) & ".bmp"
            End If

            If isMonochrome Then
                symbols(i).Save1bppDIB(filename, CInt(nudModuleSize.Value))
            Else
                symbols(i).Save24bppDIB(filename, CInt(nudModuleSize.Value))
            End If
            
        Next

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For i As Integer = 1 To 40
            cmbMaxVersion.Items.Add(i)
        Next

        cmbErrorCorrectionLevel.DataSource =
            [Enum].GetValues(GetType(ErrorCorrectionLevel))

        cmbEncoding.DisplayMember = "DisplayName"
        cmbEncoding.ValueMember = "Name"
        cmbEncoding.DataSource =  Encoding.GetEncodings()

        cmbMaxVersion.SelectedItem = 40
        cmbErrorCorrectionLevel.SelectedItem = ErrorCorrectionLevel.M
        cmbEncoding.Text = Encoding.Default.EncodingName
        nudModuleSize.Value = 5
        chkStructuredAppend.Checked = False
        btnSave.Enabled = False

    End Sub
End Class
