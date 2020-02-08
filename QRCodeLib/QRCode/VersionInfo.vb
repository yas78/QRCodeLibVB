Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 型番情報
    ''' </summary>
    Friend Module VersionInfo
        ' 型番情報
        Private ReadOnly _versionInfoValues As Integer() = {
            -1, -1, -1, -1, -1, -1, -1,
            &H07C94, &H085BC, &H09A99, &H0A4D3, &H0BBF6, &H0C762, &H0D847, &H0E60D,
            &H0F928, &H10B78, &H1145D, &H12A17, &H13532, &H149A6, &H15683, &H168C9,
            &H177EC, &H18EC4, &H191E1, &H1AFAB, &H1B08E, &H1CC1A, &H1D33F, &H1ED75,
            &H1F250, &H209D5, &H216F0, &H228BA, &H2379F, &H24B0B, &H2542E, &H26A64,
            &H27541, &H28C69
        }

        ''' <summary>
        ''' 型番情報を配置します｡
        ''' </summary>
        ''' <param name="moduleMatrix">シンボルの明暗パターン</param>
        ''' <param name="version">型番</param>
        Public Sub Place(version As Integer, moduleMatrix As Integer()())
            Dim numModulesPerSide As Integer = moduleMatrix.Length

            Dim versionInfoValue As Integer = _versionInfoValues(version)

            Dim p1 As Integer = 0
            Dim p2 As Integer = numModulesPerSide - 11

            For i As Integer = 0 To 17
                Dim v As Integer = If((versionInfoValue And (1 << i)) > 0, 3, -3)

                moduleMatrix(p1)(p2) = v
                moduleMatrix(p2)(p1) = v

                p2 += 1

                If i Mod 3 = 2 Then
                    p1 += 1
                    p2 = numModulesPerSide - 11
                End If
            Next
        End Sub

        ''' <summary>
        ''' 型番情報の予約領域を配置します｡
        ''' </summary>
        Public Sub PlaceTempBlank(moduleMatrix As Integer()())
            Dim numModulesPerSide As Integer = moduleMatrix.Length

            For i As Integer = 0 To 5
                For j As Integer = numModulesPerSide - 11 To numModulesPerSide - 9
                    moduleMatrix(i)(j) = -3
                    moduleMatrix(j)(i) = -3
                Next
            Next
        End Sub

    End Module

End Namespace
