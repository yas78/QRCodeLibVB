Imports System
Imports System.Diagnostics

Imports Ys.Util

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' 数字モードエンコーダー
    ''' </summary>
    Friend Class NumericEncoder
        Inherits QRCodeEncoder
        
        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New()
        End Sub
        
        ''' <summary>
        ''' 符号化モードを取得します。
        ''' </summary>
        Public Overrides ReadOnly Property EncodingMode As EncodingMode
            Get
                Return EncodingMode.NUMERIC
            End Get
        End Property

        ''' <summary>
        ''' モード指示子を取得します。
        ''' </summary>
        Public Overrides ReadOnly Property ModeIndicator As Integer
            Get
                Return Ys.QRCode.Format.ModeIndicator.NUMERIC_VALUE
            End Get
        End Property

        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        ''' <returns>追加した文字のビット数</returns>
        Public Overrides Function Append(c As Char) As Integer

            Debug.Assert(IsInSubset(c))

            Dim wd  As Integer = Int32.Parse(c.ToString())
            Dim ret As Integer

            If _charCounter Mod 3 = 0 Then
                _codeWords.Add(wd)
                ret = 4
            Else
                _codeWords(_codeWords.Count - 1) *= 10
                _codeWords(_codeWords.Count - 1) += wd
                ret = 3
            End If

            _charCounter += 1
            _bitCounter += ret

            Return ret

        End Function

        ''' <summary>
        ''' 指定の文字をエンコードしたコード語のビット数を返します。
        ''' </summary>
        Public Overrides Function GetCodewordBitLength(c As Char) As Integer

            Debug.Assert(IsInSubset(c))

            If _charCounter Mod 3 = 0 Then
                Return 4
            Else
                Return 3
            End If

        End Function

        ''' <summary>
        ''' エンコードされたデータのバイト配列を返します。
        ''' </summary>
        Public Overrides Function GetBytes() As Byte()

            Dim bb = New BitSequence()

            Dim bitLength As Integer = 10
            
            For i As Integer = 0 To (_codeWords.Count - 1) - 1
                bb.Append(_codeWords(i), 10)
            Next

            Select Case _charCounter Mod 3
                Case 1
                    bitLength = 4

                Case 2
                    bitLength = 7

                Case Else
                    bitLength = 10

            End Select

            bb.Append(_codeWords(_codeWords.Count - 1), bitLength)

            Return bb.GetBytes()

        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Shared Function IsInSubset(c As Char) As Boolean

            Return c >= "0"c And c <= "9"c

        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの排他的部分文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Shared Function IsInExclusiveSubset(c As Char) As Boolean

            Return IsInSubset(c)

        End Function
        
    End Class

End Namespace