Imports System

Namespace Ys.QRCode.Format

    ''' <summary>
    ''' モジュール
    ''' </summary>
    Friend Module [Module]

        ''' <summary>
        ''' １辺のモジュール数を返します。
        ''' </summary>
        ''' <param name="version">型番</param>
        Public Function GetNumModulesPerSide(version As Integer) As Integer
            Return 17 + 4 * version
        End Function

    End Module

End Namespace
