Imports System

Namespace Ys.QRCode.Format

    ''' <summary>
    ''' 文字数指示子
    ''' </summary>
    Friend Module CharCountIndicator

        ''' <summary>
        ''' 文字数指示子のビット数を返します。
        ''' </summary>
        ''' <param name="version">型番</param>
        ''' <param name="encoding">符号化モード</param>
        Public Function GetLength(
            version As Integer, encoding As EncodingMode) As Integer

            Select Case version
                Case 1 To 9
                    Select Case encoding
                        Case EncodingMode.NUMERIC
                            Return 10
                        Case EncodingMode.ALPHA_NUMERIC
                            Return 9
                        Case EncodingMode.EIGHT_BIT_BYTE
                            Return 8
                        Case EncodingMode.KANJI
                            Return 8
                        Case Else
                            Throw New ArgumentOutOfRangeException(NameOf(encoding))
                    End Select

                Case 10 To 26
                    Select Case encoding
                        Case EncodingMode.NUMERIC
                            Return 12
                        Case EncodingMode.ALPHA_NUMERIC
                            Return 11
                        Case EncodingMode.EIGHT_BIT_BYTE
                            Return 16
                        Case EncodingMode.KANJI
                            Return 10
                        Case Else
                            Throw New ArgumentOutOfRangeException(NameOf(encoding))
                    End Select

                Case 27 To 40
                    Select Case encoding
                        Case EncodingMode.NUMERIC
                            Return 14
                        Case EncodingMode.ALPHA_NUMERIC
                            Return 13
                        Case EncodingMode.EIGHT_BIT_BYTE
                            Return 16
                        Case EncodingMode.KANJI
                            Return 12
                        Case Else
                            Throw New ArgumentOutOfRangeException(NameOf(encoding))
                    End Select

                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(version))
            End Select
        End Function

    End Module

End Namespace