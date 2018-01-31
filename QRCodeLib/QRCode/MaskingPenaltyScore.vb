Imports System
Imports System.Collections.Generic

Imports Ys.TypeExtension

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
            Dim penalty As Integer = 0
            
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
            penalty += CalcAdjacentModulesInRowInSameColor(moduleMatrix.Rotate90())

            Return penalty
        End Function

        ''' <summary>
        ''' 行の同色隣接モジュールパターンの失点を計算します。
        ''' </summary>
        Private Function CalcAdjacentModulesInRowInSameColor(
            moduleMatrix As Integer()()) As Integer

            Dim penalty As Integer = 0

            For r As Integer = 0 To UBound(moduleMatrix)
                Dim columns As Integer() = moduleMatrix(r)
                Dim cnt As Integer = 1

                For c As Integer = 0 To UBound(columns) - 1
                    If (columns(c) > 0) = (columns(c + 1) > 0) Then
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
                    Dim isSameColor As Boolean = True

                    isSameColor = isSameColor And (moduleMatrix(r + 0)(c + 1) > 0 = temp)
                    isSameColor = isSameColor And (moduleMatrix(r + 1)(c + 0) > 0 = temp)
                    isSameColor = isSameColor And (moduleMatrix(r + 1)(c + 1) > 0 = temp)

                    If isSameColor Then
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
            penalty += CalcModuleRatioInRow(moduleMatrixTemp.Rotate90())
            
            Return penalty
        End Function

        ''' <summary>
        ''' 行の1 : 1 : 3 : 1 : 1 比率パターンの失点を計算します。
        ''' </summary>
        Private Function CalcModuleRatioInRow(moduleMatrix As Integer()()) As Integer
            Dim penalty As Integer = 0

            For r As Integer = 0 To UBound(moduleMatrix)
                Dim columns As Integer() = moduleMatrix(r)
                Dim startIndexes = New List(Of Integer)()

                startIndexes.Add(0)

                For c As Integer = 0 To UBound(columns) - 1
                    If columns(c) > 0 AndAlso columns(c + 1) <= 0 Then
                        startIndexes.Add(c + 1)
                    End If
                Next

                For i As Integer = 0 To startIndexes.Count - 1
                    Dim index As Integer = startIndexes(i)
                    Dim moduleRatio = New ModuleRatio()

                    Do While index <= UBound(columns) AndAlso columns(index) <= 0
                        moduleRatio.PreLightRatio4 += 1
                        index += 1
                    Loop

                    Do While index <= UBound(columns) AndAlso columns(index) > 0
                        moduleRatio.PreDarkRatio1 += 1
                        index += 1
                    Loop

                    Do While index <= UBound(columns) AndAlso columns(index) <= 0
                        moduleRatio.PreLightRatio1 += 1
                        index += 1
                    Loop

                    Do While index <= UBound(columns) AndAlso columns(index) > 0
                        moduleRatio.CenterDarkRatio3 += 1
                        index += 1
                    Loop

                    Do While index <= UBound(columns) AndAlso columns(index) <= 0
                        moduleRatio.FolLightRatio1 += 1
                        index += 1
                    Loop

                    Do While index <= UBound(columns) AndAlso columns(index) > 0
                        moduleRatio.FolDarkRatio1 += 1
                        index += 1
                    Loop

                    Do While index <= UBound(columns) AndAlso columns(index) <= 0
                        moduleRatio.FolLightRatio4 += 1
                        index += 1
                    Loop

                    If moduleRatio.PenaltyImposed() Then
                        penalty += 40
                    End If
                Next
            Next

            Return penalty
        End Function
        
        ''' <summary>
        ''' 全体に対する暗モジュールの占める割合について失点を計算します。
        ''' </summary>
        Private Function CalcProportionOfDarkModules(
            moduleMatrix As Integer()()) As Integer

            Dim darkCount As Integer = 0

            For Each columns As Integer() In moduleMatrix
                For Each value As Integer In columns
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
