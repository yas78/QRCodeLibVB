Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' エンコーダーの基本抽象クラス
    ''' </summary>
    Friend MustInherit Class QRCodeEncoder
        
        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New()
        End Sub

        Protected _codeWords    As New List(Of Integer)()
        Protected _charCounter  As Integer
        Protected _bitCounter   As Integer

        ''' <summary>
        ''' 符号化モードを取得します。
        ''' </summary>
        Public MustOverride ReadOnly Property EncodingMode() As EncodingMode

        ''' <summary>
        ''' モード指示子を取得します。
        ''' </summary>
        Public MustOverride ReadOnly Property ModeIndicator() As Integer
        
        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        ''' <returns>追加した文字のビット数</returns>
        Public MustOverride Function Append(c As Char) As Integer

        ''' <summary>
        ''' 指定の文字をエンコードしたコード語のビット数を返します。
        ''' </summary>
        Public MustOverride Function GetCodewordBitLength(c As Char) As Integer
    
        ''' <summary>
        ''' エンコードされたデータのバイト配列を返します。
        ''' </summary>
        Public MustOverride Function GetBytes() As Byte()

        ''' <summary>
        ''' 文字数を取得します。
        ''' </summary>
        Public ReadOnly Property CharCount() As Integer
            Get
                Return _charCounter
            End Get
        End Property

        ''' <summary>
        ''' データビット数を取得します。
        ''' </summary>
        Public ReadOnly Property BitCount() As Integer
            Get
                Return _bitCounter
            End Get
        End Property

        ''' <summary>
        ''' 指定したエンコーディングモードのエンコーダーを返します。
        ''' </summary>
        Public Shared Function CreateEncoder(
            encMode As EncodingMode, byteModeEncoding As Encoding) As QRCodeEncoder

            Select Case encMode
                Case EncodingMode.NUMERIC
                    Return New NumericEncoder()
                Case EncodingMode.ALPHA_NUMERIC
                    Return New AlphanumericEncoder()
                Case EncodingMode.EIGHT_BIT_BYTE
                    Return New ByteEncoder(byteModeEncoding)
                Case EncodingMode.KANJI
                    Return New KanjiEncoder()
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(encMode))
            End Select
        End Function

    End Class

End Namespace
