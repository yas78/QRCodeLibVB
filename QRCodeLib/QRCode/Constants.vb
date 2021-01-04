Imports System

Namespace Ys.QRCode

    Public Module Constants

        Public Const MIN_VERSION As Integer = 1
        Public Const MAX_VERSION As Integer = 40

    End Module

    Friend Module Values

        Public Const BLANK      As Integer = 0
        Public Const WORD       As Integer = 1
        Public Const ALIGNMENT  As Integer = 2
        Public Const FINDER     As Integer = 3
        Public Const FORMAT     As Integer = 4
        Public Const SEPARATOR  As Integer = 5
        Public Const TIMING     As Integer = 6
        Public Const VERSION    As Integer = 7

        Public Function IsDark(value As Integer) As Boolean
            Return value > BLANK
        End function

    End Module

End Namespace
