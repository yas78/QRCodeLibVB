Imports System
Imports System.Text

Imports Ys.Misc

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' 漢字モードエンコーダー
    ''' </summary>
    Friend Class KanjiEncoder
        Inherits QRCodeEncoder

        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New(encoding As Encoding)
            MyBase.New(encoding)
        End Sub

        ''' <summary>
        ''' 符号化モードを取得します。
        ''' </summary>
        Public Overrides ReadOnly Property EncodingMode As EncodingMode
            Get
                Return EncodingMode.KANJI
            End Get
        End Property

        ''' <summary>
        ''' モード指示子を取得します。
        ''' </summary>
        Public Overrides ReadOnly Property ModeIndicator As Integer
            Get
                Return Format.ModeIndicator.KANJI_VALUE
            End Get
        End Property

        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        ''' <returns>追加した文字のビット数</returns>
        Public Overrides Function Append(c As Char) As Integer
            Dim charBytes As Byte() = _encoding.GetBytes(c.ToString())
            Dim wd As Integer = (CInt(charBytes(0)) << 8) Or CInt(charBytes(1))

            Select Case wd
                Case &H8140 To &H9FFC
                    wd -= &H8140
                Case &HE040 To &HEBBF
                    wd -= &HC140
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(c))
            End Select

            wd = ((wd >> 8) * &HC0) + (wd And &HFF)

            _codeWords.Add(wd)
            _charCounter += 1
            _bitCounter += 13

            Return 13
        End Function

        ''' <summary>
        ''' 指定の文字をエンコードしたコード語のビット数を返します。
        ''' </summary>
        Public Overrides Function GetCodewordBitLength(c As Char) As Integer
            Return 13
        End Function

        ''' <summary>
        ''' エンコードされたデータのバイト配列を返します。
        ''' </summary>
        Public Overrides Function GetBytes() As Byte()
            Dim bs = New BitSequence()

            For Each wd As Integer In _codeWords
                bs.Append(wd, 13)
            Next

            Return bs.GetBytes()
        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Overrides Function InSubset(c As Char) As Boolean
            Dim charBytes As Byte() = _encoding.GetBytes(c.ToString())

            If charBytes.Length <> 2 Then
                Return False
            End If

            Dim code As Integer = (CInt(charBytes(0)) << 8) Or CInt(charBytes(1))

            If &H8140 <= code AndAlso code <= &H9FFC OrElse
               &HE040 <= code AndAlso code <= &HEBBF Then
                Return &H40 <= charBytes(1) AndAlso charBytes(1) <= &HFC AndAlso
                       &H7F <> charBytes(1)
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの排他的部分文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Overrides Function InExclusiveSubset(c As Char) As Boolean
            Return InSubset(c)
        End Function

    End Class

End Namespace
