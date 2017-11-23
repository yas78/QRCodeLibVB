Imports System
Imports System.Diagnostics

Namespace Ys.QRCode.Format

    ''' <summary>
    ''' RSブロック
    ''' </summary>
    Friend Module RSBlock
        
        ''' <summary>
        ''' RSブロック数を返します。
        ''' </summary>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <param name="version">型番</param>
        ''' <param name="preceding">RSブロック前半部分は True を指定します。</param>
        Public Function GetTotalNumber(
            ecLevel As ErrorCorrectionLevel, version As Integer, preceding As Boolean) As Integer

            Debug.Assert(version >= Constants.MIN_VERSION AndAlso 
                         version <= Constants.MAX_VERSION)

            Dim numDataCodewords As Integer = DataCodeword.GetTotalNumber(ecLevel, version)
            Dim numRSBlocks      As Integer = _totalNumbers(CInt(ecLevel))(version)

            Dim numFolBlocks As Integer = numDataCodewords Mod numRSBlocks

            If preceding Then
                Return numRSBlocks - numFolBlocks
            Else
                Return numFolBlocks
            End If
        End Function
    
        ''' <summary>
        ''' RSブロックのデータコード語数を返します。
        ''' </summary>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <param name="version">型番</param>
        ''' <param name="preceding">RSブロック前半部分は True を指定します。</param>
        Public Function GetNumberDataCodewords(
            ecLevel As ErrorCorrectionLevel, version As Integer, preceding As Boolean) As Integer

            Debug.Assert(version >= Constants.MIN_VERSION AndAlso 
                         version <= Constants.MAX_VERSION)

            Dim numDataCodewords As Integer = DataCodeword.GetTotalNumber(ecLevel, version)
            Dim numRSBlocks As Integer = _totalNumbers(CInt(ecLevel))(version)

            Dim numPreBlockCodewords As Integer = numDataCodewords \ numRSBlocks

            If preceding Then
                Return numPreBlockCodewords
            Else
                Dim numPreBlocks As Integer = GetTotalNumber(ecLevel, version, True)
                Dim numFolBlocks As Integer = GetTotalNumber(ecLevel, version, False)

                If numFolBlocks > 0 Then
                    Return (numDataCodewords - numPreBlockCodewords * numPreBlocks) \ numFolBlocks
                Else
                    Return 0
                End If
            End If
        End Function

        ''' <summary>
        ''' RSブロックの誤り訂正コード語数を返します。
        ''' </summary>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <param name="version">型番</param>
        Public Function GetNumberECCodewords(
            ecLevel As ErrorCorrectionLevel, version As Integer) As Integer

            Debug.Assert(version >= Constants.MIN_VERSION AndAlso 
                         version <= Constants.MAX_VERSION)

            Dim numDataCodewords As Integer = DataCodeword.GetTotalNumber(ecLevel, version)
            Dim numRSBlocks      As Integer = _totalNumbers(CInt(ecLevel))(version)

            Return (Codeword.GetTotalNumber(version) \ numRSBlocks) -
                   (numDataCodewords \ numRSBlocks)
        End Function

        ' RSブロック数
        Private ReadOnly _totalNumbers As Integer()() = {
            ({
                -1,
                 1,  1,  1,  1,  1,  2,  2,  2,  2,  4,
                 4,  4,  4,  4,  6,  6,  6,  6,  7,  8,
                 8,  9,  9, 10, 12, 12, 12, 13, 14, 15,
                16, 17, 18, 19, 19, 20, 21, 22, 24, 25
            }),
            ({
                -1,
                 1,  1,  1,  2,  2,  4,  4,  4,  5,  5,
                 5,  8,  9,  9, 10, 10, 11, 13, 14, 16,
                17, 17, 18, 20, 21, 23, 25, 26, 28, 29,
                31, 33, 35, 37, 38, 40, 43, 45, 47, 49
            }),
            ({
                -1,
                 1,  1,  2,  2,  4,  4,  6,  6,  8,  8,
                 8, 10, 12, 16, 12, 17, 16, 18, 21, 20,
                23, 23, 25, 27, 29, 34, 34, 35, 38, 40,
                43, 45, 48, 51, 53, 56, 59, 62, 65, 68
            }),
            ({
                -1,
                 1,  1,  2,  4,  4,  4,  5,  6,  8,  8,
                11, 11, 16, 16, 18, 16, 19, 21, 25, 25,
                25, 34, 30, 32, 35, 37, 40, 42, 45, 48,
                51, 54, 57, 60, 63, 66, 70, 74, 77, 81
            })
        }

    End Module

End Namespace
