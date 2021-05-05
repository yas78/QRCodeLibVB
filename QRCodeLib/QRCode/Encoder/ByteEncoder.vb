Imports System
Imports System.Text

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' バイトモードエンコーダー
    ''' </summary>
    Friend Class ByteEncoder
        Inherits QRCodeEncoder

        Private ReadOnly _encAlpha As AlphanumericEncoder
        Private ReadOnly _encKanji As KanjiEncoder

        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        ''' <param name="encoding">文字エンコーディング</param>
        Public Sub New(encoding As Encoding)
            MyBase.New(encoding)

            _encAlpha = New AlphanumericEncoder(encoding)

            If Charset.IsJP(encoding.WebName) Then
                _encKanji = New KanjiEncoder(encoding)
            End If
        End Sub

        ''' <summary>
        ''' 符号化モードを取得します。
        ''' </summary>
        Public Overrides ReadOnly Property EncodingMode As EncodingMode
            Get
                Return EncodingMode.EIGHT_BIT_BYTE
            End Get
        End Property

        ''' <summary>
        ''' モード指示子を取得します。
        ''' </summary>
        Public Overrides ReadOnly Property ModeIndicator As Integer
            Get
                Return Format.ModeIndicator.BYTE_VALUE
            End Get
        End Property

        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        Public Overrides Sub Append(c As Char)
            Dim charBytes As Byte() = _encoding.GetBytes(c.ToString())

            For Each value In charBytes
                _codeWords.Add(value)
            Next

            _bitCounter += GetCodewordBitLength(c)
            _charCounter += charBytes.Length            
        End Sub

        ''' <summary>
        ''' 指定の文字をエンコードしたコード語のビット数を返します。
        ''' </summary>
        Public Overrides Function GetCodewordBitLength(c As Char) As Integer
            Dim charBytes As Byte() = _encoding.GetBytes(c.ToString())

            Return 8 * charBytes.Length
        End Function

        ''' <summary>
        ''' エンコードされたデータのバイト配列を返します。
        ''' </summary>
        Public Overrides Function GetBytes() As Byte()
            Dim ret As Byte() = New Byte(_charCounter - 1) {}

            For i As Integer = 0 To _codeWords.Count - 1
                ret(i) = CByte(_codeWords(i))
            Next

            Return ret
        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Overrides Function InSubset(c As Char) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの排他的部分文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Overrides Function InExclusiveSubset(c As Char) As Boolean
            If _encAlpha.InSubset(c) Then
                Return False
            End If

            If _encKanji IsNot Nothing Then
                If _encKanji.InSubset(c) Then
                    Return False
                End If
            End If

            Return InSubset(c)
        End Function

    End Class

End Namespace
