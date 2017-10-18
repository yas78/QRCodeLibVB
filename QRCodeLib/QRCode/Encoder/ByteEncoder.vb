Imports System
Imports System.Diagnostics
Imports System.Text

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' バイトモードエンコーダー
    ''' </summary>
    Friend Class ByteEncoder
        Inherits QRCodeEncoder
        
        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New()

            MyClass.New(Encoding.GetEncoding("shift_jis"))

        End Sub

        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        ''' <param name="encoding">文字エンコーディング</param>
        Public Sub New(encoding As Encoding)

            _textEncoding = encoding

        End Sub
        
        Private ReadOnly _textEncoding As Encoding
        
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
                Return Ys.QRCode.Format.ModeIndicator.BYTE_VALUE
            End Get
        End Property
        
        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        ''' <returns>追加した文字のビット数</returns>
        Public Overrides Function Append(c As Char) As Integer

            Debug.Assert(IsInSubset(c))

            Dim charBytes As Byte() = _textEncoding.GetBytes(c.ToString())
            Dim ret       As Integer = 0

            For i As Integer = 0 To UBound(charBytes)
                _codeWords.Add(charBytes(i))
                _charCounter += 1
                _bitCounter += 8
                ret += 8
            Next

            Return ret

        End Function

        ''' <summary>
        ''' 指定の文字をエンコードしたコード語のビット数を返します。
        ''' </summary>
        Public Overrides Function GetCodewordBitLength(c As Char) As Integer

            Debug.Assert(IsInSubset(c))

            Dim charBytes As Byte() = _textEncoding.GetBytes(c.ToString())

            Return charBytes.Length * 8

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
        Public Shared Function IsInSubset(c As Char) As Boolean

            Return True

        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの排他的部分文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Shared Function IsInExclusiveSubset(c As Char) As Boolean

            If NumericEncoder.IsInSubset(c) Then
                Return False
            End If

            If AlphanumericEncoder.IsInSubset(c) Then
                Return False
            End If

            If KanjiEncoder.IsInSubset(c) Then
                Return False
            End If

            If IsInSubset(c) Then
                Return True
            End If

            Return False
            
        End Function
        
    End Class

End Namespace