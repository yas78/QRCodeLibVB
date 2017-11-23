Imports System
Imports System.Diagnostics

Namespace Ys.QRCode.Format

    ''' <summary>
    ''' コード語
    ''' </summary>
    Friend Module Codeword

        ''' <summary>
        ''' コード語総数を返します。
        ''' </summary>
        ''' <param name="version">型番</param>
        Public Function GetTotalNumber(version As Integer) As Integer
            Debug.Assert(version >= Constants.MIN_VERSION AndAlso 
                         version <= Constants.MAX_VERSION)

            Return _totalNumbers(version)
        End Function

        ' コード語総数
        Private ReadOnly _totalNumbers As Integer() = {
              -1,
              26,   44,   70,  100,  134,  172,  196,  242,  292,  346,
             404,  466,  532,  581,  655,  733,  815,  901,  991, 1085,
            1156, 1258, 1364, 1474, 1588, 1706, 1828, 1921, 2051, 2185,
            2323, 2465, 2611, 2761, 2876, 3034, 3196, 3362, 3532, 3706
        }
        
    End Module

End Namespace
