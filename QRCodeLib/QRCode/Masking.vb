Imports System
Imports System.Diagnostics

Imports Ys.Misc

Namespace Ys.QRCode

    ''' <summary>
    ''' シンボルマスク
    ''' </summary>
    Friend Module Masking

        ''' <summary>
        ''' マスクを適用します。
        ''' </summary>
        ''' <param name="moduleMatrix">シンボルの明暗パターン</param>
        ''' <param name="version">型番</param>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <returns>適用されたマスクパターン参照子</returns>
        Public Function Apply(version As Integer,
                              ecLevel As ErrorCorrectionLevel,
                              ByRef moduleMatrix As Integer()()) As Integer
            Dim minPenalty As Integer = Int32.MaxValue
            Dim maskPatternReference As Integer = 0
            Dim maskedMatrix As Integer()() = Nothing

            For i As Integer = 0 To 7
                Dim temp As Integer()() = ArrayUtil.DeepCopy(moduleMatrix)

                Mask(i, temp)
                FormatInfo.Place(ecLevel, i, temp)

                If version >= 7 Then
                    VersionInfo.Place(version, temp)
                End If

                Dim penalty As Integer = MaskingPenaltyScore.CalcTotal(temp)

                If penalty < minPenalty Then
                    minPenalty = penalty
                    maskPatternReference = i
                    maskedMatrix = temp
                End If
            Next

            moduleMatrix = maskedMatrix
            Return maskPatternReference
        End Function

        ''' <summary>
        ''' マスクパターンを適用したシンボルデータを返します。
        ''' </summary>
        ''' <param name="moduleMatrix">シンボルの明暗パターン</param>
        ''' <param name="maskPatternReference">マスクパターン参照子</param>
        Private Sub Mask(maskPatternReference As Integer, moduleMatrix As Integer()())
            Dim condition = GetCondition(maskPatternReference)

            For r As Integer = 0 To UBound(moduleMatrix)
                For c As Integer = 0 To UBound(moduleMatrix(r))
                    If Math.Abs(moduleMatrix(r)(c)) = 1 Then
                        If condition(r, c) Then
                            moduleMatrix(r)(c) *= -1
                        End If
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' マスク条件を返します。
        ''' </summary>
        ''' <param name="maskPatternReference">マスクパターン参照子</param>
        Private Function GetCondition(
            maskPatternReference As Integer) As Func(Of Integer, Integer, Boolean)

            Select Case maskPatternReference
                Case 0
                    Return Function(r, c) (r + c) Mod 2 = 0
                Case 1
                    Return Function(r, c) r Mod 2 = 0
                Case 2
                    Return Function(r, c) c Mod 3 = 0
                Case 3
                    Return Function(r, c) (r + c) Mod 3 = 0
                Case 4
                    Return Function(r, c) ((r \ 2) + (c \ 3)) Mod 2 = 0
                Case 5
                    Return Function(r, c) ((r * c) Mod 2 + (r * c) Mod 3) = 0
                Case 6
                    Return Function(r, c) ((r * c) Mod 2 + (r * c) Mod 3) Mod 2 = 0
                Case 7
                    Return Function(r, c) ((r + c) Mod 2 + (r * c) Mod 3) Mod 2 = 0
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(maskPatternReference))
            End Select
        End Function

    End Module

End Namespace
