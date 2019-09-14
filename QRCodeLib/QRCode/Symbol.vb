﻿Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO

Imports Ys.Image
Imports Ys.Misc
Imports Ys.QRCode.Encoder
Imports Ys.QRCode.Format

Namespace Ys.QRCode
        
    ''' <summary>
    ''' シンボルを表します。
    ''' </summary>
    Public Class Symbol

        Const WHITE As String = "#FFFFFF"
        Const BLACK As String = "#000000"

        Const DEFAULT_MODULE_SIZE As Integer = 4

        Private ReadOnly _parent As Symbols

        Private ReadOnly _position As Integer

        Private _currEncoder      As QRCodeEncoder
        Private _currEncodingMode As EncodingMode
        Private _currVersion      As Integer

        Private _dataBitCapacity As Integer
        Private _dataBitCounter  As Integer

        Private ReadOnly _segments As List(Of QRCodeEncoder)
        Private ReadOnly _segmentCounter As Dictionary(Of EncodingMode, Integer)

        ''' <summary>
        ''' インスタンスを初期化します
        ''' </summary>
        ''' <param name="parent">親オブジェクト</param>
        Friend Sub New(parent As Symbols)
            _parent = parent

            _position = parent.Count

            _currEncoder      = Nothing
            _currEncodingMode = EncodingMode.UNKNOWN
            _currVersion      = parent.MinVersion

            _dataBitCapacity = 8 * DataCodeword.GetTotalNumber(
                parent.ErrorCorrectionLevel, parent.MinVersion)
            _dataBitCounter  = 0

            _segments = New List(Of QRCodeEncoder)()
            _segmentCounter = New Dictionary(Of EncodingMode, Integer) From {
                {EncodingMode.NUMERIC,        0},
                {EncodingMode.ALPHA_NUMERIC,  0},
                {EncodingMode.EIGHT_BIT_BYTE, 0},
                {EncodingMode.KANJI,          0}
            }

            If parent.StructuredAppendAllowed Then
                _dataBitCapacity -= StructuredAppend.HEADER_LENGTH 
            End If
        End Sub
            
        ''' <summary>
        ''' 親オブジェクトを取得します。
        ''' </summary>
        Public ReadOnly Property Parent() As Symbols
            Get
                Return _parent
            End Get
        End Property

        ''' <summary>
        ''' 型番を取得します。
        ''' </summary>
        Public ReadOnly Property Version() As Integer
            Get
                Return _currVersion
            End Get
        End Property

        ''' <summary>
        ''' 現在の符号化モードを取得します。
        ''' </summary>
        Friend ReadOnly Property CurrentEncodingMode() As EncodingMode
            Get
                Return _currEncodingMode
            End Get
        End Property

        ''' <summary>
        ''' シンボルに文字を追加します。
        ''' </summary>
        ''' <param name="c">対象文字</param>
        ''' <returns>シンボル容量が不足している場合は False を返します。</returns>
        Friend Function TryAppend(c As Char) As Boolean
            Dim bitLength As Integer = _currEncoder.GetCodewordBitLength(c)

            Do While _dataBitCapacity < _dataBitCounter + bitLength
                If _currVersion >= _parent.MaxVersion Then
                    Return False
                End If

                SelectVersion()
            Loop

            _currEncoder.Append(c)
            _dataBitCounter += bitLength
            _parent.UpdateParity(c)

            Return True
        End Function

        ''' <summary>
        ''' 符号化モードを設定します。
        ''' </summary>
        ''' <param name="encMode">符号化モード</param>
        ''' <param name="c">符号化する最初の文字。この文字はシンボルに追加されません。</param>
        ''' <returns>シンボル容量が不足している場合は False を返します。</returns>
        Friend Function TrySetEncodingMode(encMode As EncodingMode, c As Char) As Boolean
            Dim encoder As QRCodeEncoder = 
                QRCodeEncoder.CreateEncoder(encMode, _parent.ByteModeEncoding)
            Dim bitLength As Integer = encoder.GetCodewordBitLength(c)

            Do While _dataBitCapacity < _dataBitCounter +
                                        ModeIndicator.LENGTH +
                                        CharCountIndicator.GetLength(_currVersion, encMode) +
                                        bitLength
                If _currVersion >= _parent.MaxVersion Then
                    Return False
                End If

                SelectVersion()
            Loop

            _dataBitCounter += ModeIndicator.LENGTH +
                               CharCountIndicator.GetLength(_currVersion, encMode)

            _currEncoder = encoder
            _segments.Add(_currEncoder)
            _segmentCounter(encMode) += 1
            _currEncodingMode = encMode
                
            Return True
        End Function

        ''' <summary>
        ''' 型番を決定します。
        ''' </summary>
        Private Sub SelectVersion()
            For Each encMode As EncodingMode In _segmentCounter.Keys
                Dim num As Integer = _segmentCounter(encMode)

                _dataBitCounter += 
                    num * CharCountIndicator.GetLength(_currVersion + 1, encMode) -
                    num * CharCountIndicator.GetLength(_currVersion + 0, encMode)
            Next

            _currVersion += 1
            _dataBitCapacity = 8 * DataCodeword.GetTotalNumber(
                _parent.ErrorCorrectionLevel, _currVersion)
            _parent.MinVersion = _currVersion

            If _parent.StructuredAppendAllowed Then
                _dataBitCapacity -= StructuredAppend.HEADER_LENGTH
            End If
        End Sub
            
        ''' <summary>
        ''' データブロックを返します。
        ''' </summary>
        Private Function BuildDataBlock() As Byte()()
            Dim dataBytes As Byte() = GetMessageBytes()

            Dim numPreBlocks As Integer = RSBlock.GetTotalNumber(
                    _parent.ErrorCorrectionLevel, _currVersion, True)
            Dim numFolBlocks As Integer = RSBlock.GetTotalNumber(
                    _parent.ErrorCorrectionLevel, _currVersion, False)

            Dim ret As Byte()() = New Byte(numPreBlocks + numFolBlocks - 1)() {}
            
            Dim numPreBlockDataCodewords As Integer = RSBlock.GetNumberDataCodewords(
                    _parent.ErrorCorrectionLevel, _currVersion, True)
            Dim index As Integer = 0

            For i As Integer = 0 To numPreBlocks - 1
                Dim data As Byte() = New Byte(numPreBlockDataCodewords - 1) {}

                Array.Copy(dataBytes, index, data, 0, data.Length)
                index += data.Length

                ret(i) = data
            Next
                
            Dim numFolBlockDataCodewords As Integer = RSBlock.GetNumberDataCodewords(
                    _parent.ErrorCorrectionLevel, _currVersion, False)

            For i As Integer = numPreBlocks To numPreBlocks + numFolBlocks - 1
                Dim data As Byte() = New Byte(numFolBlockDataCodewords - 1) {}

                Array.Copy(dataBytes, index, data, 0, data.Length)
                index += data.Length

                ret(i) = data
            Next
                
            Return ret
        End Function

        ''' <summary>
        ''' 誤り訂正データ領域のブロックを生成します。
        ''' </summary>
        ''' <param name="dataBlock">データ領域のブロック</param>
        Private Function BuildErrorCorrectionBlock(dataBlock()() As Byte) As Byte()()
            Dim numECCodewords As Integer = RSBlock.GetNumberECCodewords(
                    _parent.ErrorCorrectionLevel, _currVersion)
            Dim numPreBlocks As Integer = RSBlock.GetTotalNumber(
                    _parent.ErrorCorrectionLevel, _currVersion, True)
            Dim numFolBlocks As Integer = RSBlock.GetTotalNumber(
                    _parent.ErrorCorrectionLevel, _currVersion, False)

            Dim ret As Byte()() = New Byte(numPreBlocks + numFolBlocks - 1)() {}

            For i As Integer = 0 To UBound(ret) 
                ret(i) = New Byte(numECCodewords - 1) {}
            Next

            Dim gp As Integer() = GeneratorPolynomials.Item(numECCodewords)

            For blockIndex As Integer = 0 To UBound(dataBlock)
                Dim size As Integer = UBound(dataBlock(blockIndex)) + UBound(ret(blockIndex)) + 1
                Dim data As Integer() = New Integer(size) {}
                Dim eccIndex As Integer = UBound(data) 

                For i As Integer = 0 To UBound(dataBlock(blockIndex))
                    data(eccIndex) = dataBlock(blockIndex)(i)
                    eccIndex -= 1
                Next

                For i As Integer = UBound(data) To numECCodewords Step -1
                    If data(i) > 0 Then
                        Dim exp As Integer = GaloisField256.ToExp(data(i))
                        eccIndex = i

                        For j As Integer = UBound(gp) To 0 Step -1
                            data(eccIndex) = data(eccIndex) Xor 
                                             GaloisField256.ToInt((gp(j) + exp) Mod 255)
                            eccIndex -= 1
                        Next
                    End If
                Next

                eccIndex = numECCodewords - 1

                For i As Integer = 0 To UBound(ret(blockIndex))
                    ret(blockIndex)(i) = CByte(data(eccIndex))
                    eccIndex -= 1
                Next
            Next

            Return ret
        End Function

        ''' <summary>
        ''' 符号化領域のバイトデータを返します。
        ''' </summary>
        Private Function GetEncodingRegionBytes() As Byte()
            Dim dataBlock   As Byte()() = BuildDataBlock()
            Dim ecBlock     As Byte()() = BuildErrorCorrectionBlock(dataBlock)
                
            Dim numCodewords As Integer = Codeword.GetTotalNumber(_currVersion)

            Dim numDataCodewords As Integer = DataCodeword.GetTotalNumber(
                    _parent.ErrorCorrectionLevel, _currVersion)
                
            Dim ret As Byte() = New Byte(numCodewords - 1) {}

            Dim index   As Integer = 0
            Dim n       As Integer

            n = 0
            Do While index < numDataCodewords
                Dim r As Integer = n Mod dataBlock.Length
                Dim c As Integer = n \ dataBlock.Length

                If c <= UBound(dataBlock(r)) Then 
                    ret(index) = dataBlock(r)(c)
                    index += 1
                End If

                n += 1
            Loop

            n = 0
            Do While index < numCodewords
                Dim r As Integer = n Mod ecBlock.Length
                Dim c As Integer = n \ ecBlock.Length

                If c <= UBound(ecBlock(r)) Then 
                    ret(index) = ecBlock(r)(c)
                    index += 1
                End If

                n += 1
            Loop
                
            Return ret
        End Function

        ''' <summary>
        ''' コード語に変換するメッセージビット列を返します。
        ''' </summary>
        Private Function GetMessageBytes() As Byte()
            Dim bs = New BitSequence()

            If _parent.Count > 1 Then
                WriteStructuredAppendHeader(bs)
            End If

            WriteSegments(bs)
            WriteTerminator(bs)
            WritePaddingBits(bs)
            WritePadCodewords(bs)
                
            Return bs.GetBytes()
        End Function

        Private Sub WriteStructuredAppendHeader(bs As BitSequence)
            bs.Append(ModeIndicator.STRUCTURED_APPEND_VALUE, ModeIndicator.LENGTH)
            bs.Append(_position, SymbolSequenceIndicator.POSITION_LENGTH)
            bs.Append(_parent.Count - 1, SymbolSequenceIndicator.TOTAL_NUMBER_LENGTH)
            bs.Append(_parent.StructuredAppendParity, StructuredAppend.PARITY_DATA_LENGTH)
        End Sub

        Private Sub WriteSegments(bs As BitSequence)
            For Each segment As QRCodeEncoder In _segments
                bs.Append(segment.ModeIndicator, ModeIndicator.LENGTH)
                bs.Append(segment.CharCount,
                          CharCountIndicator.GetLength(_currVersion, 
                                                       segment.EncodingMode)
                )

                Dim data As Byte() = segment.GetBytes()

                For i As Integer = 0 To UBound(data) - 1
                    bs.Append(data(i), 8)
                Next

                Dim codewordBitLength As Integer = segment.BitCount Mod 8

                If codewordBitLength = 0 Then
                    codewordBitLength = 8
                End If

                bs.Append(data(UBound(data)) >> (8 - codewordBitLength), 
                          codewordBitLength)
            Next
        End Sub

        Private Sub WriteTerminator(bs As BitSequence)
            Dim terminatorLength As Integer = _dataBitCapacity - _dataBitCounter

            If terminatorLength > ModeIndicator.LENGTH Then
                terminatorLength = ModeIndicator.LENGTH
            End If

            bs.Append(ModeIndicator.TERMINATOR_VALUE, terminatorLength)
        End Sub

        Private Sub WritePaddingBits(bs As BitSequence)
            If bs.Length Mod 8 > 0 Then
                bs.Append(&H0, 8 - (bs.Length Mod 8))
            End If
        End Sub

        Private Sub WritePadCodewords(bs As BitSequence)
            Dim numDataCodewords As Integer = DataCodeword.GetTotalNumber(
                _parent.ErrorCorrectionLevel, _currVersion)

            Dim flag As Boolean = True

            Do While bs.Length < 8 * numDataCodewords
                bs.Append(If(flag, 236, 17), 8)
                flag = Not flag
            Loop
        End Sub

        ''' <summary>
        ''' シンボルの明暗パターンを返します。
        ''' </summary>
        Private Function GetModuleMatrix() As Integer()()
            Dim numModulesPerSide As Integer = [Module].GetNumModulesPerSide(_currVersion)

            Dim moduleMatrix As Integer()() = New Integer(numModulesPerSide - 1)() {}

            For i As Integer = 0 To UBound(moduleMatrix)
                moduleMatrix(i) = New Integer(UBound(moduleMatrix)) {}
            Next

            FinderPattern.Place(moduleMatrix)
            Separator.Place(moduleMatrix)
            TimingPattern.Place(moduleMatrix)

            If _currVersion >= 2 Then
                AlignmentPattern.Place(_currVersion, moduleMatrix)
            End If

            FormatInfo.PlaceTempBlank(moduleMatrix)

            If _currVersion >= 7 Then
                VersionInfo.PlaceTempBlank(moduleMatrix)
            End If

            PlaceSymbolChar(moduleMatrix)
            RemainderBit.Place(moduleMatrix)

            Masking.Apply(_currVersion, _parent.ErrorCorrectionLevel, moduleMatrix)

            Return moduleMatrix
        End Function

        ''' <summary>
        ''' シンボルキャラクタを配置します。
        ''' </summary>
        Private Sub PlaceSymbolChar(moduleMatrix As Integer()())
            Dim data As Byte() = GetEncodingRegionBytes()

            Dim r As Integer = UBound(moduleMatrix)
            Dim c As Integer = UBound(moduleMatrix(0))

            Dim toLeft As Boolean = True
            Dim rowDirection As Integer = -1

            For Each value As Byte In data
                Dim bitPos As Integer = 7

                Do While bitPos >= 0
                    If moduleMatrix(r)(c) = 0 Then
                        moduleMatrix(r)(c) = If((value And (1 << bitPos)) > 0, 1, -1)
                        bitPos -= 1
                    End If

                    If toLeft Then
                        c -= 1
                    Else
                        If (r + rowDirection) < 0 Then
                            r = 0
                            rowDirection = 1
                            c -= 1

                            If c = 6 Then
                                c = 5
                            End If
                        ElseIf ((r + rowDirection) > UBound(moduleMatrix)) Then
                            r = UBound(moduleMatrix)
                            rowDirection = -1
                            c -= 1

                            If c = 6 Then
                                c = 5
                            End If
                        Else
                            r += rowDirection
                            c += 1
                        End If
                    End If

                    toLeft = Not toLeft
                Loop
            Next
        End Sub

        ''' <summary>
        ''' 1bppビットマップファイルのバイトデータを返します。
        ''' </summary>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        Public Function Get1bppDIB(Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                                   Optional foreRgb As String = BLACK,
                                   Optional backRgb As String = WHITE) As Byte()
            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim foreColor As Color = ColorTranslator.FromHtml(foreRgb)
            Dim backColor As Color = ColorTranslator.FromHtml(backRgb)

            Dim moduleMatrix As Integer()() = QuietZone.Place(GetModuleMatrix())

            Dim width As Integer = moduleSize * moduleMatrix.Length
            Dim height As Integer = width

            Dim rowBytesLen As Integer = (width + 7) \ 8

            Dim pack8bit As Integer = 0
            If width Mod 8 > 0 Then
                pack8bit = 8 - (width Mod 8)
            End If

            Dim pack32bit As Integer = 0
            If rowBytesLen Mod 4 > 0 Then
                pack32bit = 8 * (4 - (rowBytesLen Mod 4))
            End If

            Dim rowSize As Integer = (width + pack8bit + pack32bit) \ 8
            Dim bitmapData As Byte() = New Byte(rowSize * height - 1) {}
            Dim offset As Integer = 0

            For r As Integer = UBound(moduleMatrix) To 0 Step -1
                Dim bs = New BitSequence()

                For Each value In moduleMatrix(r)
                    Dim color As Integer = If(value > 0, 0, 1)

                    For j As Integer = 1 To moduleSize
                        bs.Append(color, 1)
                    Next
                Next
                bs.Append(0, pack8bit)
                bs.Append(0, pack32bit)

                Dim bitmapRow As Byte() = bs.GetBytes()

                For i As Integer = 1 To moduleSize
                    Array.Copy(bitmapRow, 0, bitmapData, offset, rowSize)
                    offset += rowSize
                Next
            Next

            Dim ret As Byte() = DIB.Build1bppDIB(bitmapData, width, height, foreColor, backColor)
            Return ret
        End Function

        ''' <summary>
        ''' 24bppビットマップファイルのバイトデータを返します。
        ''' </summary>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        Public Function Get24bppDIB(Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                                    Optional foreRgb As String = BLACK,
                                    Optional backRgb As String = WHITE) As Byte()
            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim foreColor As Color = ColorTranslator.FromHtml(foreRgb)
            Dim backColor As Color = ColorTranslator.FromHtml(backRgb)

            Dim moduleMatrix As Integer()() = QuietZone.Place(GetModuleMatrix())

            Dim width As Integer = moduleSize * moduleMatrix.Length
            Dim height As Integer = width

            Dim rowBytesLen As Integer = 3 * width

            Dim pack4byte As Integer = 0
            If rowBytesLen Mod 4 > 0 Then
                pack4byte = 4 - (rowBytesLen Mod 4)
            End If

            Dim rowSize As Integer = rowBytesLen + pack4byte
            Dim bitmapData As Byte() = New Byte(rowSize * height - 1) {}
            Dim offset As Integer = 0

            For r As Integer = UBound(moduleMatrix) To 0 Step -1
                Dim bitmapRow As Byte() = New Byte(rowSize - 1) {}
                Dim index As Integer = 0

                For Each value In moduleMatrix(r)
                    Dim color As Color = If(value > 0, foreColor, backColor)

                    For j As Integer = 1 To moduleSize
                        bitmapRow(index + 0) = color.B
                        bitmapRow(index + 1) = color.G
                        bitmapRow(index + 2) = color.R
                        index += 3
                    Next
                Next

                For i As Integer = 1 To moduleSize
                    Array.Copy(bitmapRow, 0, bitmapData, offset, rowSize)
                    offset += rowSize
                Next
            Next

            Dim ret As Byte() = DIB.Build24bppDIB(bitmapData, width, height)
            Return ret
        End Function

        ''' <summary>
        ''' Base64エンコードされたビットマップデータを返します。
        ''' </summary>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="colorDepth"></param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        ''' <returns></returns>
        Public Function GetBase64DIB(Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                                     Optional colorDepth As Integer = 24,
                                     Optional foreRgb As String = BLACK,
                                     Optional backRgb As String = WHITE) As String
            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim dib As Byte()

            Select Case colorDepth
                Case 1
                    dib = Get1bppDIB(moduleSize, foreRgb, backRgb)
                Case 24
                    dib = Get24bppDIB(moduleSize, foreRgb, backRgb)
                Case Else
                    Throw New InvalidOperationException()
            End Select

            Dim ret As String = Convert.ToBase64String(dib)
            Return ret
        End Function

        ''' <summary>
        ''' 1bppのシンボル画像を返します。
        ''' </summary>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        Public Function Get1bppImage(Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                                     Optional foreRgb As String = BLACK,
                                     Optional backRgb As String = WHITE) As System.Drawing.Image
            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim dib As Byte() = Get1bppDIB(moduleSize, foreRgb, backRgb)

            Dim converter As ImageConverter = New ImageConverter()
            Dim ret As System.Drawing.Image = DirectCast(converter.ConvertFrom(dib), System.Drawing.Image)

            Return ret
        End Function

        ''' <summary>
        ''' 24bppのシンボル画像を返します。
        ''' </summary>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        Public Function Get24bppImage(Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                                      Optional foreRgb As String = BLACK,
                                      Optional backRgb As String = WHITE) As System.Drawing.Image
            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim dib As Byte() = Get24bppDIB(moduleSize, foreRgb, backRgb)

            Dim converter As ImageConverter = New ImageConverter()
            Dim ret As System.Drawing.Image = DirectCast(converter.ConvertFrom(dib), System.Drawing.Image)

            Return ret
        End Function

        ''' <summary>
        ''' 1bppシンボル画像をファイルに保存します
        ''' </summary>
        ''' <param name="fileName">ファイル名</param>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        Public Sub Save1bppDIB(fileName As String,
                               Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                               Optional foreRgb As String = BLACK,
                               Optional backRgb As String = WHITE)
            If String.IsNullOrEmpty(fileName) Then
                Throw New ArgumentNullException(NameOf(fileName))
            End If

            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim dib As Byte() = Get1bppDIB(moduleSize, foreRgb, backRgb)
            File.WriteAllBytes(fileName, dib)
        End Sub

        ''' <summary>
        ''' 24bppシンボル画像をファイルに保存します
        ''' </summary>
        ''' <param name="fileName">ファイル名</param>
        ''' <param name="moduleSize">モジュールサイズ(px)</param>
        ''' <param name="foreRgb">前景色</param>
        ''' <param name="backRgb">背景色</param>
        Public Sub Save24bppDIB(fileName As String,
                                Optional moduleSize As Integer = DEFAULT_MODULE_SIZE,
                                Optional foreRgb As String = BLACK,
                                Optional backRgb As String = WHITE)
            If String.IsNullOrEmpty(fileName) Then
                Throw New ArgumentNullException(NameOf(fileName))
            End If

            If moduleSize < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(moduleSize))
            End If

            Dim dib As Byte() = Get24bppDIB(moduleSize, foreRgb, backRgb)
            File.WriteAllBytes(fileName, dib)
        End Sub

    End Class
        
End Namespace