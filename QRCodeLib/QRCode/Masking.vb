Imports System
Imports System.Diagnostics

Imports Ys.TypeExtension

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
        Public Function Apply(moduleMatrix As Integer()(),
                              version As Integer,
                              ecLevel As ErrorCorrectionLevel) As Integer

            Debug.Assert(version >= Constants.MIN_VERSION AndAlso version <= Constants.MAX_VERSION)

            Dim maskPatternReference As Integer = SelectMaskPattern(moduleMatrix, version, ecLevel)
            Mask(moduleMatrix, maskPatternReference)

            Return maskPatternReference

        End Function

        ''' <summary>
        ''' マスクパターンを選択します。
        ''' </summary>
        ''' <param name="moduleMatrix">シンボルの明暗パターン</param>
        ''' <param name="version">型番</param>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <returns>マスクパターン参照子</returns>
        Private Function SelectMaskPattern(moduleMatrix As Integer()(),
                                           version As Integer,
                                           ecLevel As ErrorCorrectionLevel) As Integer

            Debug.Assert(version >= Constants.MIN_VERSION AndAlso version <= Constants.MAX_VERSION)

            Dim minPenalty As Integer = Int32.MaxValue

            Dim ret As Integer = 0

            For maskPatternReference As Integer = 0 To 7
                Dim temp As Integer()() = moduleMatrix.CloneDeep()

                Mask(temp, maskPatternReference) 

                FormatInfo.Place(temp, ecLevel, maskPatternReference)

                If version >= 7 Then
                    VersionInfo.Place(temp, version)
                End If

                Dim penalty As Integer = MaskingPenaltyScore.CalcTotal(temp)

                If penalty < minPenalty Then
                    minPenalty = penalty
                    ret = maskPatternReference
                End If
            Next

            Return ret

        End Function

        ''' <summary>
        ''' マスクパターンを適用したシンボルデータを返します。
        ''' </summary>
        ''' <param name="moduleMatrix">シンボルの明暗パターン</param>
        ''' <param name="maskPatternReference">マスクパターン参照子</param>
        Private Sub Mask(moduleMatrix As Integer()(), maskPatternReference As Integer)

            Debug.Assert(maskPatternReference >= 0 AndAlso 
                         maskPatternReference <= 7)

            Dim condition As Func(Of Integer, Integer, Boolean) = GetCondition(maskPatternReference)

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
        Private Function GetCondition(maskPatternReference As Integer) As Func(Of Integer, Integer, Boolean)

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
