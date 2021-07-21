Imports System
Imports System.IO
Imports System.Text

Imports Ys.QRCode

Public Class Form1

    Const DEFAULT_MODULE_SIZE As Integer = 5

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UpdateQRCodePanel(sender As Object, e As EventArgs) _
        Handles txtData.TextChanged, 
                nudModuleSize.ValueChanged, 
                cmbMaxVersion.SelectedIndexChanged, 
                cmbErrorCorrectionLevel.SelectedIndexChanged, 
                chkStructuredAppend.CheckedChanged, 
                cmbCharset.SelectedIndexChanged

        btnSave.Enabled = False
        qrcodePanel.Controls.Clear()

        If String.IsNullOrEmpty(txtData.Text) Then
            Return
        End If
        
        Dim ecLevel As ErrorCorrectionLevel = CType(cmbErrorCorrectionLevel.SelectedItem, ErrorCorrectionLevel)
        Dim version As Integer = CInt(cmbMaxVersion.SelectedItem)
        Dim allowStructuredAppend As Boolean = chkStructuredAppend.Checked
        Dim charsetName As String = CType(cmbCharset.SelectedItem, String)
        Dim moduleSize As Integer = CInt(nudModuleSize.Value)

        Dim symbols As Symbols = New Symbols(ecLevel, version, allowStructuredAppend, charsetName)

        Try
            symbols.AppendText(txtData.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try

        For Each symbol As Symbol In symbols
            Dim image As Image = symbol.GetImage(moduleSize)
            Dim pictureBox = New PictureBox() With {
                .Size = image.Size,
                .Image = image
            }
            qrcodePanel.Controls.Add(pictureBox)
        Next

        btnSave.Enabled = txtData.TextLength > 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim baseName As String
        Dim isMonochrome As Boolean
        Dim ext As String

        Dim filters As String() = {
            "Monochrome Bitmap(*.bmp)|*.bmp", 
            "24-bit Bitmap(*.bmp)|*.bmp",
            "SVG(*.svg)|*.svg"
        }

        Using fd = New SaveFileDialog()
            fd.Filter = String.Join("|", filters)

            If fd.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            isMonochrome = fd.FilterIndex = 1
            baseName = Path.Combine(
                Path.GetDirectoryName(fd.FileName),
                Path.GetFileNameWithoutExtension(fd.FileName))

            Select Case fd.FilterIndex
                Case 1, 2
                    ext = FileExtension.BITMAP
                Case 3
                    ext = FileExtension.SVG
                Case Else
                    Throw New InvalidOperationException()
            End Select
        End Using
        
        Dim ecLevel As ErrorCorrectionLevel = CType(cmbErrorCorrectionLevel.SelectedItem, ErrorCorrectionLevel)
        Dim version As Integer = CInt(cmbMaxVersion.SelectedItem)
        Dim allowStructuredAppend As Boolean = chkStructuredAppend.Checked
        Dim charsetName As String = CType(cmbCharset.SelectedItem, String)
        Dim moduleSize As Integer = CInt(nudModuleSize.Value)

        Dim symbols As Symbols = New Symbols(ecLevel, version, allowStructuredAppend, charsetName)

        Try
            symbols.AppendText(txtData.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try

        For i As Integer = 0 To symbols.Count - 1
            Dim filename As String

            If symbols.Count = 1 Then
                filename = baseName & ext
            Else
                filename = baseName & "_" & CStr(i + 1) & ext
            End If

            Select Case ext
                Case FileExtension.BITMAP
                    symbols(i).SaveBitmap(filename, moduleSize, isMonochrome)
                Case FileExtension.SVG
                    symbols(i).SaveSvg(filename, moduleSize)
                Case Else
                    Throw New InvalidOperationException()
            End Select
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbErrorCorrectionLevel.DataSource =
            [Enum].GetValues(GetType(ErrorCorrectionLevel))
        cmbErrorCorrectionLevel.SelectedItem = ErrorCorrectionLevel.M

        For i As Integer = Constants.MIN_VERSION To Constants.MAX_VERSION
            cmbMaxVersion.Items.Add(i)
        Next

        cmbMaxVersion.SelectedIndex = cmbMaxVersion.Items.Count - 1
        cmbCharset.DataSource = {"Shift_JIS", "UTF-8"}
        nudModuleSize.Value = DEFAULT_MODULE_SIZE
        chkStructuredAppend.Checked = False
        btnSave.Enabled = False
    End Sub

End Class

Friend Module FileExtension
    Public Const BITMAP As String = ".bmp"
    Public Const SVG As String = ".svg"
End Module
