Imports System
Imports System.Diagnostics
Imports System.Text

Imports Ys.Util

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' 漢字モードエンコーダー
    ''' </summary>
    Friend Class KanjiEncoder
        Inherits QRCodeEncoder
        
        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New()
        End Sub

        Private Shared ReadOnly _textEncoding As Encoding = Encoding.GetEncoding("shift_jis")

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
                Return Ys.QRCode.Format.ModeIndicator.KANJI_VALUE
            End Get
        End Property

        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        ''' <returns>追加した文字のビット数</returns>
        Public Overrides Function Append(c As Char) As Integer

            Debug.Assert(IsInSubset(c))

            Dim charBytes As Byte() = _textEncoding.GetBytes(c.ToString())
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

            Debug.Assert(IsInSubset(c))

            Return 13

        End Function

        ''' <summary>
        ''' エンコードされたデータのバイト配列を返します。
        ''' </summary>
        Public Overrides Function GetBytes() As Byte()

            Dim bs = New BitSequence()

            For i As Integer = 0 To _codeWords.Count - 1
                bs.Append(_codeWords(i), 13)
            Next

            Return bs.GetBytes()

        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Shared Function IsInSubset(c As Char) As Boolean
            
            Dim charBytes As Byte() = _textEncoding.GetBytes(c.ToString())

            If charBytes.Length <> 2 Then
                Return False
            End If

            Dim code As Integer = (CInt(charBytes(0)) << 8) Or CInt(charBytes(1))

            If code >= &H8140 AndAlso code <= &H9FFC OrElse
               code >= &HE040 AndAlso code <= &HEBBF Then
                
                Return charBytes(1) >= &H40 AndAlso
                       charBytes(1) <= &HFC AndAlso
                       charBytes(1) <> &H7F
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの排他的部分文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Shared Function IsInExclusiveSubset(c As Char) As Boolean

            Return IsInSubset(c)

        End Function
        
    End Class

End Namespace
