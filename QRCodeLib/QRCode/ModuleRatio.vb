Imports System

Namespace Ys.QRCode

    Friend Class ModuleRatio
            
        Public PreLightRatio4   As Integer = 0
        Public PreDarkRatio1    As Integer = 0
        Public PreLightRatio1   As Integer = 0
        Public CenterDarkRatio3 As Integer = 0
        Public FolLightRatio1   As Integer = 0
        Public FolDarkRatio1    As Integer = 0
        Public FolLightRatio4   As Integer = 0

        Public Function PenaltyImposed() As Boolean
            If PreDarkRatio1 = 0 Then
                Return False
            End If

            If PreDarkRatio1     = PreLightRatio1   AndAlso
               PreDarkRatio1     = FolLightRatio1   AndAlso
               PreDarkRatio1     = FolDarkRatio1    AndAlso
               PreDarkRatio1 * 3 = CenterDarkRatio3 Then
                Return PreLightRatio4 >= PreDarkRatio1 * 4 OrElse
                       FolLightRatio4 >= PreDarkRatio1 * 4
            Else
                Return False
            End If
        End Function

    End Class

End Namespace
