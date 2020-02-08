Imports System

Namespace Ys.QRCode

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

    ''' <summary>
    ''' 誤り訂正レベル
    ''' </summary>
    Public Enum ErrorCorrectionLevel
        L
        M
        Q
        H
    End Enum

    Friend Enum Constants
        MIN_VERSION = 1
        MAX_VERSION = 40
    End Enum

End Namespace

