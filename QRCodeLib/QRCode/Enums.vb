Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 誤り訂正レベル
    ''' </summary>
    Public Enum ErrorCorrectionLevel
        L
        M
        Q
        H
    End Enum

    ''' <summary>
    ''' 符号化モード
    ''' </summary>
    Friend Enum EncodingMode
        UNKNOWN
        NUMERIC
        ALPHA_NUMERIC
        EIGHT_BIT_BYTE
        KANJI
    End Enum

End Namespace

