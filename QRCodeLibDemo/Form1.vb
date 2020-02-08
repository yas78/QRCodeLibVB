﻿Imports System
Imports System.IO
Imports System.Text

Imports Ys.QRCode

Public Class Form1

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UpdateQRCodePanel(sender As Object, e As EventArgs) _
        Handles txtData.TextChanged, 
                nudModuleSize.ValueChanged, 
                cmbMaxVersion.SelectedIndexChanged, 
                cmbErrorCorrectionLevel.SelectedIndexChanged, 
                chkStructuredAppend.CheckedChanged, 
                cmbEncoding.SelectedIndexChanged

        btnSave.Enabled = False
        qrcodePanel.Controls.Clear()

        If String.IsNullOrEmpty(txtData.Text) Then
            Return
        End If
        
        Dim ecLevel As ErrorCorrectionLevel = CType(cmbErrorCorrectionLevel.SelectedItem, ErrorCorrectionLevel)
        Dim version As Integer = CInt(cmbMaxVersion.SelectedItem)
        Dim allowStructuredAppend As Boolean = chkStructuredAppend.Checked
        Dim encoding As Encoding = CType(cmbEncoding.SelectedItem, EncodingInfo).GetEncoding()

        Dim symbols As Symbols = New Symbols(ecLevel, version, allowStructuredAppend, encoding.WebName)

        Try
            symbols.AppendText(txtData.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try

        For Each symbol As Symbol In symbols
            Dim image As Image = symbol.GetImage(CInt(nudModuleSize.Value), False)
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

        Using fd As SaveFileDialog = New SaveFileDialog()
            fd.Filter = "Monochrome Bitmap(*.bmp)|*.bmp|24-bit Bitmap(*.bmp)|*.bmp"

            If fd.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            isMonochrome = fd.FilterIndex = 1
            baseName = Path.Combine(
                Path.GetDirectoryName(fd.FileName), Path.GetFileNameWithoutExtension(fd.FileName))
        End Using
        
        Dim ecLevel As ErrorCorrectionLevel = CType(cmbErrorCorrectionLevel.SelectedItem, ErrorCorrectionLevel)
        Dim version As Integer = CInt(cmbMaxVersion.SelectedItem)
        Dim allowStructuredAppend As Boolean = chkStructuredAppend.Checked
        Dim encoding As Encoding = CType(cmbEncoding.SelectedItem, EncodingInfo).GetEncoding()

        Dim symbols As Symbols = New Symbols(ecLevel, version, allowStructuredAppend, encoding.WebName)

        Try
            symbols.AppendText(txtData.Text)
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

            symbols(i).SaveBitmap(filename, CInt(nudModuleSize.Value), isMonochrome)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbErrorCorrectionLevel.DataSource =
            [Enum].GetValues(GetType(ErrorCorrectionLevel))
        cmbErrorCorrectionLevel.SelectedItem = ErrorCorrectionLevel.M

        For i As Integer = 1 To 40
            cmbMaxVersion.Items.Add(i)
        Next

        cmbMaxVersion.SelectedIndex = cmbMaxVersion.Items.Count - 1

        cmbEncoding.DisplayMember = "DisplayName"
        cmbEncoding.ValueMember = "Name"
        cmbEncoding.DataSource =  Encoding.GetEncodings()
        cmbEncoding.Text = Encoding.Default.EncodingName

        nudModuleSize.Value = 4
        chkStructuredAppend.Checked = False
        btnSave.Enabled = False
    End Sub

End Class
