Imports System
Imports System.Diagnostics

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
            Debug.Assert(version >= Constants.MIN_VERSION AndAlso 
                         version <= Constants.MAX_VERSION)

            Return 17 + version * 4
        End Function

    End Module

End Namespace
