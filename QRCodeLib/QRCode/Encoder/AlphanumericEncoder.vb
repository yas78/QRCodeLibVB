Imports System
Imports System.Text

Imports Ys.Misc

Namespace Ys.QRCode.Encoder

    ''' <summary>
    ''' 英数字モードエンコーダー
    ''' </summary>
    Friend Class AlphanumericEncoder
        Inherits QRCodeEncoder

        Private ReadOnly _encNumeric As NumericEncoder

        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New(encoding As Encoding)
            MyBase.New(encoding)

            _encNumeric = New NumericEncoder(encoding)
        End Sub

        ''' <summary>
        ''' 符号化モードを取得します。
        ''' </summary>
        Public Overrides ReadOnly Property EncodingMode As EncodingMode
            Get
                Return EncodingMode.ALPHA_NUMERIC
            End Get
        End Property

        ''' <summary>
        ''' モード指示子を取得します。
        ''' </summary>
        Public Overrides ReadOnly Property ModeIndicator As Integer
            Get
                Return Format.ModeIndicator.ALPAHNUMERIC_VALUE
            End Get
        End Property

        ''' <summary>
        ''' 文字を追加します。
        ''' </summary>
        Public Overrides sub Append(c As Char)
            Dim wd As Integer = ConvertCharCode(c)
            Dim ret As Integer

            If _charCounter Mod 2 = 0 Then
                _codeWords.Add(wd)
                ret = 6
            Else
                _codeWords(_codeWords.Count - 1) *= 45
                _codeWords(_codeWords.Count - 1) += wd
                ret = 5
            End If

            _bitCounter += GetCodewordBitLength(c)
            _charCounter += 1
        End sub

        ''' <summary>
        ''' 指定の文字をエンコードしたコード語のビット数を返します。
        ''' </summary>
        Public Overrides Function GetCodewordBitLength(c As Char) As Integer
            If _charCounter Mod 2 = 0 Then
                Return 6
            Else
                Return 5
            End If
        End Function

        ''' <summary>
        ''' エンコードされたデータのバイト配列を返します。
        ''' </summary>
        Public Overrides Function GetBytes() As Byte()
            Dim bs = New BitSequence()
            Dim bitLength As Integer = 11 

            For i As Integer = 0 To (_codeWords.Count - 1) - 1
                bs.Append(_codeWords(i), bitLength)
            Next

            If _charCounter Mod 2 = 0 Then
                bitLength = 11
            Else
                bitLength = 6
            End If

            bs.Append(_codeWords(_codeWords.Count - 1), bitLength)

            Return bs.GetBytes()
        End Function

        ''' <summary>
        ''' 指定した文字の、英数字モードにおけるコード値を返します。
        ''' </summary>
        Private Shared Function ConvertCharCode(c As Char) As Integer
            Dim code = Asc(c)

            Select Case c
                Case " "c
                    Return 36
                Case "$"c, "%"c
                    Return code + 1
                Case "*"c, "+"c
                    Return code - 3
                Case "-"c, "."c
                    Return code - 4
                Case "/"c
                    Return 43
                Case "0"c To "9"c
                    Return code - 48
                Case ":"c
                    Return 44
                Case "A"c To "Z"c
                    Return code - 55
                Case Else
                    Return -1
            End Select
        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Overrides Function InSubset(c As Char) As Boolean
            Return ConvertCharCode(c) > -1
        End Function

        ''' <summary>
        ''' 指定した文字が、このモードの排他的部分文字集合に含まれる場合は True を返します。
        ''' </summary>
        Public Overrides Function InExclusiveSubset(c As Char) As Boolean
            If _encNumeric.InSubset(c) Then
                Return False
            End If

            Return InSubset(c)
        End Function

    End Class

End Namespace
