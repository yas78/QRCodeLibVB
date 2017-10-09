Imports System

Namespace Ys.QRCode.Format

    ''' <summary>
    ''' モード指示子
    ''' </summary>
    Friend Module ModeIndicator

        Public Const LENGTH As Integer = 4

        Public Const TERMINATOR_VALUE           As Integer = &H0
        Public Const NUMERIC_VALUE              As Integer = &H1
        Public Const ALPAHNUMERIC_VALUE         As Integer = &H2
        Public Const STRUCTURED_APPEND_VALUE    As Integer = &H3
        Public Const BYTE_VALUE                 As Integer = &H4
        Public Const KANJI_VALUE                As Integer = &H8
        
    End Module

End Namespace
