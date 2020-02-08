Imports System
Imports System.Collections.Generic

Imports Ys.Misc

Namespace Ys.QRCode

    ''' <summary>
    ''' マスクされたシンボルの失点評価
    ''' </summary>
    Friend Module MaskingPenaltyScore
       
        ''' <summary>
        ''' マスクパターン失点の合計を返します。
        ''' </summary>
        Public Function CalcTotal(moduleMatrix As Integer()()) As Integer
            Dim total   As Integer = 0
            Dim penalty As Integer

            penalty = CalcAdjacentModulesInSameColor(moduleMatrix)
            total += penalty

            penalty = CalcBlockOfModulesInSameColor(moduleMatrix)
            total += penalty

            penalty = CalcModuleRatio(moduleMatrix)
            total += penalty

            penalty = CalcProportionOfDarkModules(moduleMatrix)
            total += penalty

            Return total
        End Function

        ''' <summary>
        ''' 行／列の同色隣接モジュールパターンの失点を計算します。
        ''' </summary>
        Private Function CalcAdjacentModulesInSameColor(
            moduleMatrix As Integer()()) As Integer

            Dim penalty As Integer = 0

            penalty += CalcAdjacentModulesInRowInSameColor(moduleMatrix)
            penalty += CalcAdjacentModulesInRowInSameColor(ArrayUtil.Rotate90(moduleMatrix))

            Return penalty
        End Function

        ''' <summary>
        ''' 行の同色隣接モジュールパターンの失点を計算します。
        ''' </summary>
        Private Function CalcAdjacentModulesInRowInSameColor(
            moduleMatrix As Integer()()) As Integer

            Dim penalty As Integer = 0

            For Each row As Integer() In moduleMatrix
                Dim cnt As Integer = 1

                For i As Integer = 0 To UBound(row) - 1
                    If (row(i) > 0) = (row(i + 1) > 0) Then
                        cnt += 1
                    Else
                        If cnt >= 5 Then
                            penalty += 3 + (cnt - 5)
                        End If

                        cnt = 1
                    End If
                Next

                If cnt >= 5 Then
                    penalty += 3 + (cnt - 5)
                End If
            Next

            Return penalty
        End Function

        ''' <summary>
        ''' 2x2の同色モジュールパターンの失点を計算します。
        ''' </summary>
        Private Function CalcBlockOfModulesInSameColor(
            moduleMatrix As Integer()()) As Integer

            Dim penalty As Integer = 0

            For r As Integer = 0 To UBound(moduleMatrix) - 1
                For c As Integer = 0 To UBound(moduleMatrix(r)) - 1
                    Dim temp As Boolean = moduleMatrix(r)(c) > 0

                    If (moduleMatrix(r + 0)(c + 1) > 0 = temp) AndAlso
                       (moduleMatrix(r + 1)(c + 0) > 0 = temp) AndAlso
                       (moduleMatrix(r + 1)(c + 1) > 0 = temp) Then
                        penalty += 3
                    End If

                Next
            Next

            Return penalty
        End Function
        
        ''' <summary>
        ''' 行／列における1 : 1 : 3 : 1 : 1 比率パターンの失点を計算します。
        ''' </summary>
        Private Function CalcModuleRatio(moduleMatrix As Integer()()) As Integer
            Dim moduleMatrixTemp As Integer()() = QuietZone.Place(moduleMatrix)

            Dim penalty As Integer = 0

            penalty += CalcModuleRatioInRow(moduleMatrixTemp)
            penalty += CalcModuleRatioInRow(ArrayUtil.Rotate90(moduleMatrixTemp))

            Return penalty
        End Function

        ''' <summary>
        ''' 行の1 : 1 : 3 : 1 : 1 比率パターンの失点を計算します。
        ''' </summary>
        Private Function CalcModuleRatioInRow(moduleMatrix As Integer()()) As Integer
            Dim penalty As Integer = 0

            For Each row As Integer() In moduleMatrix
                Dim ratio3Ranges As Integer()() = GetRatio3Ranges(row)

                For Each rng As Integer() In ratio3Ranges
                    Dim ratio3 As Integer = rng(1) + 1 - rng(0)
                    Dim ratio1 As Integer = ratio3 \ 3
                    Dim ratio4 As Integer = ratio1 * 4
                    Dim impose As Boolean = False
                    Dim cnt As Integer

                    Dim i As Integer = rng(0) - 1

                    ' light ratio 1
                    cnt = 0
                    Do While i >= 0 AndAlso row(i) <= 0
                        cnt += 1
                        i -= 1
                    Loop

                    If cnt <> ratio1 Then Continue For

                    ' dark ratio 1
                    cnt = 0
                    Do While i >= 0 AndAlso row(i) > 0
                        cnt += 1
                        i -= 1
                    Loop

                    If cnt <> ratio1 Then Continue For

                    ' light ratio 4
                    cnt = 0
                    Do While i >= 0 AndAlso row(i) <= 0
                        cnt += 1
                        i -= 1
                    Loop

                    If cnt >= ratio4 Then
                        impose = True
                    End If

                    i = rng(1) + 1

                    ' light ratio 1
                    cnt = 0
                    Do While i <= UBound(row) AndAlso row(i) <= 0
                        cnt += 1
                        i += 1
                    Loop

                    If cnt <> ratio1 Then Continue For

                    ' dark ratio 1
                    cnt = 0
                    Do While i <= UBound(row) AndAlso row(i) > 0
                        cnt += 1
                        i += 1
                    Loop

                    If cnt <> ratio1 Then Continue For

                    ' light ratio 4
                    cnt = 0
                    Do While i <= UBound(row) AndAlso row(i) <= 0
                        cnt += 1
                        i += 1
                    Loop

                    If cnt >= ratio4 Then
                        impose = True
                    End If

                    If impose Then
                        penalty += 40
                    End If
                Next
            Next

            Return penalty
        End Function

        Private Function GetRatio3Ranges(arg As Integer()) As Integer()()
            Dim ret As New List(Of Integer())
            Dim s As Integer = 0

            For i As Integer = QuietZone.WIDTH To UBound(arg) - QuietZone.WIDTH
                If arg(i) > 0 AndAlso arg(i - 1) <= 0 Then
                    s = i
                End If

                If arg(i) > 0 AndAlso arg(i + 1) <= 0 Then
                    If (i + 1 - s) Mod 3 = 0 Then
                        ret.Add({s, i})
                    End If
                End If
            Next

            Return ret.ToArray()
        End Function

        ''' <summary>
        ''' 全体に対する暗モジュールの占める割合について失点を計算します。
        ''' </summary>
        Private Function CalcProportionOfDarkModules(
            moduleMatrix As Integer()()) As Integer

            Dim darkCount As Integer = 0

            For Each row As Integer() In moduleMatrix
                For Each value As Integer In row
                    If value > 0 Then
                        darkCount += 1
                    End If
                Next
            Next
            
            Dim numModules As Double = moduleMatrix.Length ^ 2
            Dim temp As Integer
            temp = CInt(Math.Ceiling(darkCount / numModules * 100))
            temp = Math.Abs(temp - 50)
            temp = (temp + 4) \ 5

            Return temp * 10
        End Function
        
    End Module

End Namespace
