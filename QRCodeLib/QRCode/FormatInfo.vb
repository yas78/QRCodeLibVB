Imports System
Imports System.Diagnostics

Namespace Ys.QRCode

    ''' <summary>
    ''' 形式情報
    ''' </summary>
    Friend Module FormatInfo
        
        ''' <summary>
        ''' 形式情報を配置します｡
        ''' </summary>
        ''' <param name="moduleMatrix">シンボルの明暗パターン</param>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <param name="maskPatternReference">マスクパターン参照子</param>
        Public Sub Place(moduleMatrix As Integer()(), 
                         ecLevel As ErrorCorrectionLevel, 
                         maskPatternReference As Integer)

            Debug.Assert(maskPatternReference >= 0 AndAlso 
                         maskPatternReference <= 7)

            Dim formatInfoValue as Integer = GetFormatInfoValue(ecLevel, maskPatternReference)

            Dim r1 As Integer = 0
            Dim c1 As Integer = moduleMatrix.Length - 1

            For i As Integer = 0 To 7
                Dim temp As Integer = 
                    If((formatInfoValue And (1 << i)) > 0, 1, 0) Xor _formatInfoMaskArray(i)

                Dim v As Integer = If(temp > 0, 3, -3)

                moduleMatrix(r1)(8) = v
                moduleMatrix(8)(c1) = v

                r1 += 1
                c1 -= 1

                If r1 = 6 Then
                    r1 += 1
                End If

            Next

            Dim r2 As Integer = moduleMatrix.Length - 7
            Dim c2 As Integer = 7

            For i As Integer = 8 To 14
                Dim temp As Integer =
                    If((formatInfoValue And (1 << i)) > 0, 1, 0) Xor _formatInfoMaskArray(i)

                Dim v As Integer = If(temp > 0, 3, -3)

                moduleMatrix(r2)(8) = v
                moduleMatrix(8)(c2) = v

                r2 += 1
                c2 -= 1

                If c2 = 6 Then
                    c2 -= 1
                End If

            Next

        End Sub

        ''' <summary>
        ''' 形式情報の予約領域を配置します｡
        ''' </summary>
        Public Sub PlaceTempBlank(moduleMatrix As Integer()())

            Dim numModulesOneSide As Integer = moduleMatrix.Length

            For i As Integer = 0 To 8
                ' タイミグパターンの領域ではない場合
                If i <> 6 Then
                    moduleMatrix(8)(i) = -3
                    moduleMatrix(i)(8) = -3
                End If
            Next

            For i As Integer = numModulesOneSide - 8 To numModulesOneSide - 1
                moduleMatrix(8)(i) = -3
                moduleMatrix(i)(8) = -3
            Next

            ' 固定暗モジュールを配置(マスクの適用前に配置する)
            moduleMatrix(numModulesOneSide - 8)(8) = 2

        End Sub

        ''' <summary>
        ''' 形式情報の値を取得します。
        ''' </summary>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <param name="maskPatternReference">マスクパターン参照子</param>
        Public Function GetFormatInfoValue(
            ecLevel As ErrorCorrectionLevel, maskPatternReference As Integer) As Integer

            Debug.Assert(maskPatternReference >= 0 AndAlso maskPatternReference <= 7)

            Dim indicator As Integer

            Select Case ecLevel
                Case ErrorCorrectionLevel.L
                    indicator = 1

                Case ErrorCorrectionLevel.M
                    indicator = 0

                Case ErrorCorrectionLevel.Q
                    indicator = 3

                Case ErrorCorrectionLevel.H
                    indicator = 2

                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(ecLevel))

            End Select
        
            Return _formatInfoValues((indicator << 3) Or maskPatternReference)

        End Function

        ' 形式情報
        Private ReadOnly _formatInfoValues As Integer() = {
            &H0000, &H0537, &H0A6E, &H0F59, &H11EB, &H14DC, &H1B85, &H1EB2, &H23D6, &H26E1,
            &H29B8, &H2C8F, &H323D, &H370A, &H3853, &H3D64, &H429B, &H47AC, &H48F5, &H4DC2,
            &H5370, &H5647, &H591E, &H5C29, &H614D, &H647A, &H6B23, &H6E14, &H70A6, &H7591,
            &H7AC8, &H7FFF
        }

        ' 形式情報のマスクパターン
        Private _formatInfoMaskArray As Integer() = {
            0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1
        }

    End Module

End Namespace
