Imports System
Imports System.IO
Imports Ys.QRCode

Public Class Form1

    Public Sub New()

        InitializeComponent()
        InitializeComponent2()

    End Sub

    Public Sub InitializeComponent2()

        For i As Integer = 1 To 40
            cmbMaxVersion.Items.Add(i)
        Next

        cmbErrorCorrectionLevel.DataSource =
            [Enum].GetValues(GetType(ErrorCorrectionLevel))

        cmbMaxVersion.Text = "20"
        cmbErrorCorrectionLevel.Text = "M"
        nudSize.Value = 5
        chkStructuredAppend.Checked = True
        btnSave.Enabled = False

    End Sub

    Private Sub Update_qrcodePanel(sender As Object, e As EventArgs) _
        Handles txtData.TextChanged, nudSize.ValueChanged, cmbMaxVersion.SelectedIndexChanged, cmbErrorCorrectionLevel.SelectedIndexChanged, chkStructuredAppend.CheckedChanged

        btnSave.Enabled = False
        qrcodePanel.Controls.Clear()
        
        If String.IsNullOrEmpty(txtData.Text) Then
            Return
        End If

        Dim symbols As Symbols

        Try
            symbols = New Symbols(
                CInt(cmbMaxVersion.Text),
                CType(cmbErrorCorrectionLevel.SelectedValue, ErrorCorrectionLevel),
                chkStructuredAppend.Checked)

            symbols.AppendString(txtData.Text)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return

        End Try

        For Each symbol As Symbol In symbols
            Dim image As Image = symbol.Get24bppImage(CInt(nudSize.Value))

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

        Dim symbols As Symbols

        Try
            symbols = New Symbols(
                CInt(cmbMaxVersion.Text),
                CType(cmbErrorCorrectionLevel.SelectedValue, ErrorCorrectionLevel),
                chkStructuredAppend.Checked)

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
                symbols(i).Save1bppDIB(filename, CInt(nudSize.Value))
            Else
                symbols(i).Save24bppDIB(filename, CInt(nudSize.Value))
            End If
            
        Next

    End Sub
End Class
