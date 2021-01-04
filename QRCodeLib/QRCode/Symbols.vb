Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text

Imports Ys.QRCode.Encoder

Namespace Ys.QRCode

    ''' <summary>
    ''' シンボルのコレクションを表します。
    ''' </summary>
    Public Class Symbols
        Implements IEnumerable(Of Symbol)

        Private ReadOnly _items As List(Of Symbol)

        Private _minVersion As Integer

        Private ReadOnly _maxVersion                As Integer
        Private ReadOnly _errorCorrectionLevel      As ErrorCorrectionLevel
        Private ReadOnly _structuredAppendAllowed   As Boolean
        Private ReadOnly _byteModeEncoding          As Encoding
        Private ReadOnly _shiftJISEncoding          As Encoding

        Private _parity As Integer
        Private _currSymbol As Symbol

        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        ''' <param name="ecLevel">誤り訂正レベル</param>
        ''' <param name="maxVersion">型番の上限</param>
        ''' <param name="allowStructuredAppend">複数シンボルへの分割を許可するには True を指定します。</param>
        ''' <param name="byteModeEncoding">バイトモードの文字エンコーディング</param>
        Public Sub New(Optional ecLevel As ErrorCorrectionLevel = ErrorCorrectionLevel.M,
                       Optional maxVersion As Integer = Constants.MAX_VERSION,
                       Optional allowStructuredAppend As Boolean = False,
                       Optional byteModeEncoding As String = "shift_jis")

            If Not (Constants.MIN_VERSION <= maxVersion AndAlso maxVersion <= Constants.MAX_VERSION) Then
                Throw New ArgumentOutOfRangeException(NameOf(maxVersion))
            End If

            _items = New List(Of Symbol)()

            _minVersion = Constants.MIN_VERSION

            _maxVersion                 = maxVersion
            _errorCorrectionLevel       = ecLevel
            _structuredAppendAllowed    = allowStructuredAppend
            _byteModeEncoding           = Encoding.GetEncoding(byteModeEncoding)
            _shiftJISEncoding           = Encoding.GetEncoding("shift_jis")

            _parity     = 0
            _currSymbol = New Symbol(Me)

            _items.Add(_currSymbol)
        End Sub

        ''' <summary>
        ''' インデックス番号を指定してSymbolオブジェクトを取得します。
        ''' </summary>
        Default Public ReadOnly Property Item(index As Integer) As Symbol
            Get
                Return _items(index)
            End Get
        End Property

        ''' <summary>
        ''' シンボル数を取得します。
        ''' </summary>
        Public ReadOnly Property Count() As Integer
            Get
                Return _items.Count
            End Get
        End Property

        ''' <summary>
        ''' 型番の下限を取得または設定します。
        ''' </summary>
        Friend Property MinVersion() As Integer
            Get
                Return _minVersion
            End Get
            Set
                _minVersion = value
            End Set
        End Property

        ''' <summary>
        ''' 型番の上限を取得します。
        ''' </summary>
        Friend ReadOnly Property MaxVersion() As Integer
            Get
                Return _maxVersion
            End Get
        End Property

        ''' <summary>
        ''' 誤り訂正レベルを取得します。
        ''' </summary>
        Friend ReadOnly Property ErrorCorrectionLevel() As ErrorCorrectionLevel
            Get
                Return _errorCorrectionLevel
            End Get
        End Property

        ''' <summary>
        ''' 構造的連接モードの使用可否を取得します。
        ''' </summary>
        Friend ReadOnly Property StructuredAppendAllowed() As Boolean
            Get
                Return _structuredAppendAllowed
            End Get
        End Property

        ''' <summary>
        ''' 構造的連接のパリティを取得します。
        ''' </summary>
        Friend ReadOnly Property Parity() As Integer
            Get
                Return _parity
            End Get
        End Property

        Public ReadOnly Property ByteModeEncoding() As Encoding
            Get
                Return _byteModeEncoding
            End Get
        End Property

        ''' <summary>
        ''' シンボルを追加します。
        ''' </summary>
        Private Function Add() As Symbol
            _currSymbol = New Symbol(Me)
            _items.Add(_currSymbol)

            Return _currSymbol
        End Function

        ''' <summary>
        ''' 文字列を追加します。
        ''' </summary>
        Public Sub AppendText(s As String)
            If String.IsNullOrEmpty(s) Then
                Throw New ArgumentNullException(NameOf(s))
            End If

            For i As Integer = 0 To s.Length - 1
                Dim oldMode As EncodingMode = _currSymbol.CurrentEncodingMode
                Dim newMode As EncodingMode

                Select Case oldMode
                    Case EncodingMode.UNKNOWN
                        newMode = SelectInitialMode(s, i)
                    Case EncodingMode.NUMERIC
                        newMode = SelectModeWhileInNumeric(s, i)
                    Case EncodingMode.ALPHA_NUMERIC
                        newMode = SelectModeWhileInAlphanumeric(s, i)
                    Case EncodingMode.EIGHT_BIT_BYTE
                        newMode = SelectModeWhileInByte(s, i)
                    Case EncodingMode.KANJI
                        newMode = SelectInitialMode(s, i)
                    Case Else
                        Throw New InvalidOperationException()
                End Select

                Dim c As Char = s(i)

                If newMode <> oldMode Then
                    If Not _currSymbol.TrySetEncodingMode(newMode, c) Then
                        If _structuredAppendAllowed = False OrElse _items.Count = 16 Then
                            Throw New ArgumentException("String too long", NameOf(s))
                        End If

                        Add()
                        newMode = SelectInitialMode(s, i)
                        _currSymbol.TrySetEncodingMode(newMode, c)
                    End If
                End If

                If Not _currSymbol.TryAppend(c) Then
                    If _structuredAppendAllowed = False OrElse _items.Count = 16 Then
                        Throw New ArgumentException("String too long", NameOf(s))
                    End If

                    Add()
                    newMode = SelectInitialMode(s, i)
                    _currSymbol.TrySetEncodingMode(newMode, c)
                    _currSymbol.TryAppend(c)
                End If
            Next
        End Sub

        ''' <summary>
        ''' 初期モードを決定します。
        ''' </summary>
        ''' <param name="s">対象文字列</param>
        ''' <param name="start">評価を開始する位置</param>
        Private Function SelectInitialMode(
            s As String, start As Integer) As EncodingMode

            If KanjiEncoder.InSubset(s(start)) Then
                Return EncodingMode.KANJI
            End If

            If ByteEncoder.InExclusiveSubset(s(start)) Then
                Return EncodingMode.EIGHT_BIT_BYTE
            End If

            If AlphanumericEncoder.InExclusiveSubset(s(start)) Then
                Return SelectModeWhenInitialDataAlphaNumeric(s, start)
            End If

            If NumericEncoder.InSubset(s(start)) Then
                Return SelectModeWhenInitialDataNumeric(s, start)
            End If

            Throw New InvalidOperationException()
        End Function

        Private Function SelectModeWhenInitialDataAlphaNumeric(
            s As String, start As Integer) As EncodingMode

            Dim cnt As Integer = 0

            For i As Integer = start To s.Length - 1
                If AlphanumericEncoder.InExclusiveSubset(s(i)) Then
                    cnt += 1
                Else
                    Exit For
                End If
            Next

            Dim flg As Boolean

            Select Case _currSymbol.Version
                Case 1 To 9
                    flg = cnt < 6
                Case 10 To 26
                    flg = cnt < 7
                Case 27 To 40
                    flg = cnt < 8
                Case Else
                    Throw New InvalidOperationException()
            End Select

            If flg Then
                If (start + cnt) < s.Length Then
                    If ByteEncoder.InSubset(s(start + cnt)) Then
                        Return EncodingMode.EIGHT_BIT_BYTE
                    End If
                End If
            End If

            Return EncodingMode.ALPHA_NUMERIC
        End Function

        Private Function SelectModeWhenInitialDataNumeric(
            s As String, start As Integer) As EncodingMode

            Dim cnt As Integer = 0

            For i As Integer = start To s.Length - 1
                If NumericEncoder.InSubset(s(i)) Then
                    cnt += 1
                Else
                    Exit For
                End If
            Next

            Dim flg As Boolean

            Select Case _currSymbol.Version
                Case 1 To 9
                    flg = cnt < 4
                Case 10 To 26
                    flg = cnt < 4
                Case 27 To 40
                    flg = cnt < 5
                Case Else
                    Throw New InvalidOperationException()
            End Select

            If flg Then
                If (start + cnt) < s.Length Then
                    If ByteEncoder.InExclusiveSubset(s(start + cnt)) Then
                        Return EncodingMode.EIGHT_BIT_BYTE
                    End If
                End If
            End If

            Select Case _currSymbol.Version
                Case 1 To 9
                    flg = cnt < 7
                Case 10 To 26
                    flg = cnt < 8
                Case 27 To 40
                    flg = cnt < 9
                Case Else
                    Throw New InvalidOperationException()
            End Select

            If flg Then
                If (start + cnt) < s.Length Then
                    If AlphanumericEncoder.InExclusiveSubset(s(start + cnt)) Then
                        Return EncodingMode.ALPHA_NUMERIC
                    End If
                End If
            End If

            Return EncodingMode.NUMERIC
        End Function

        ''' <summary>
        ''' 数字モードから切り替えるモードを決定します。
        ''' </summary>
        ''' <param name="s">対象文字列</param>
        ''' <param name="start">評価を開始する位置</param>
        Private Function SelectModeWhileInNumeric(
            s As String, start As Integer) As EncodingMode

            If KanjiEncoder.InSubset(s(start)) Then
                Return EncodingMode.KANJI
            End If

            If ByteEncoder.InExclusiveSubset(s(start)) Then
                Return EncodingMode.EIGHT_BIT_BYTE
            End If

            If AlphanumericEncoder.InExclusiveSubset(s(start)) Then
                Return EncodingMode.ALPHA_NUMERIC
            End If

            Return EncodingMode.NUMERIC
        End Function

        ''' <summary>
        ''' 英数字モードから切り替えるモードを決定します。
        ''' </summary>
        ''' <param name="s">対象文字列</param>
        ''' <param name="start">評価を開始する位置</param>
        Private Function SelectModeWhileInAlphanumeric(
            s As String, start As Integer) As EncodingMode

            If KanjiEncoder.InSubset(s(start)) Then
                Return EncodingMode.KANJI
            End If

            If ByteEncoder.InExclusiveSubset(s(start)) Then
                Return EncodingMode.EIGHT_BIT_BYTE
            End If

            If MustChangeModeAlphanumericToNumeric(s, start) Then
                Return EncodingMode.NUMERIC
            End if

            Return EncodingMode.ALPHA_NUMERIC
        End Function

        Private Function MustChangeModeAlphanumericToNumeric(
            s As String, start As Integer) As Boolean

            Dim ret As Boolean = False
            Dim cnt As Integer = 0

            For i As Integer = start To s.Length - 1
                If Not AlphanumericEncoder.InSubset(s(i)) Then
                    Exit For
                End If

                If NumericEncoder.InSubset(s(i)) Then
                    cnt += 1
                Else
                    ret = True
                    Exit For
                End If
            Next

            If ret Then
                Select Case _currSymbol.Version
                    Case 1 To 9
                        ret = cnt >= 13
                    Case 10 To 26
                        ret = cnt >= 15
                    Case 27 To 40
                        ret = cnt >= 17
                    Case Else
                        Throw New InvalidOperationException
                End Select
            End If

            Return ret
        End Function

        ''' <summary>
        ''' バイトモードから切り替えるモードを決定します。
        ''' </summary>
        ''' <param name="s">対象文字列</param>
        ''' <param name="start">評価を開始する位置</param>
        Private Function SelectModeWhileInByte(
            s As String, start As Integer) As EncodingMode

            If KanjiEncoder.InSubset(s(start)) Then
                Return EncodingMode.KANJI
            End If

            If MustChangeByteToNumeric(s, start) Then
                Return EncodingMode.NUMERIC
            End If

            If MustChangeByteToAlphanumeric(s, start) Then
                Return EncodingMode.ALPHA_NUMERIC
            End If

            Return EncodingMode.EIGHT_BIT_BYTE
        End Function

        Private Function MustChangeByteToNumeric(
            s As String, start As Integer) As Boolean

            Dim ret As Boolean = False
            Dim cnt As Integer = 0

            For i As Integer = start To s.Length - 1
                If Not ByteEncoder.InSubset(s(i)) Then
                    Exit For
                End If

                If NumericEncoder.InSubset(s(i)) Then
                    cnt += 1
                ElseIf ByteEncoder.InExclusiveSubset(s(i)) Then
                    ret = True
                    Exit For
                Else
                    Exit For
                End If
            Next

            If ret Then
                Select Case _currSymbol.Version
                    Case 1 To 9
                        ret = cnt >= 6
                    Case 10 To 26
                        ret = cnt >= 8
                    Case 27 To 40
                        ret = cnt >= 9
                    Case Else
                        Throw New InvalidOperationException()
                End Select
            End If

            Return ret
        End Function

        Private Function MustChangeByteToAlphanumeric(
            s As String, start As Integer) As Boolean

            Dim ret As Boolean = False
            Dim cnt As Integer = 0

            For i As Integer = start To s.Length - 1
                If Not ByteEncoder.InSubset(s(i)) Then
                    Exit For
                End If

                If AlphanumericEncoder.InExclusiveSubset(s(i)) Then
                    cnt += 1
                ElseIf ByteEncoder.InExclusiveSubset(s(i)) Then
                    ret = True
                    Exit For
                Else
                    Exit For
                End If
            Next

            If ret Then
                Select Case _currSymbol.Version
                    Case 1 To 9
                        ret = cnt >= 11
                    Case 10 To 26
                        ret = cnt >= 15
                    Case 27 To 40
                        ret = cnt >= 16
                    Case Else
                        Throw New InvalidOperationException()
                End Select
            End If

            Return ret
        End function

        ''' <summary>
        ''' 構造的連接のパリティを更新します。
        ''' </summary>
        ''' <param name="c">パリティ計算対象の文字</param>
        Friend Sub UpdateParity(c As Char)
            Dim charBytes As Byte()

            If KanjiEncoder.InSubset(c) Then
                charBytes = _shiftJISEncoding.GetBytes(c.ToString())
            Else
                charBytes = _byteModeEncoding.GetBytes(c.ToString())
            End If

            For Each value As Byte In charBytes
                _parity = _parity Xor value
            Next
        End Sub

#Region "IEnumerable<Symbols.Symbol> Implementation"
        Public Function GetEnumerator() As IEnumerator(Of Symbol) _
            Implements IEnumerable(Of Symbol).GetEnumerator

            Return _items.GetEnumerator()
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator _
            Implements IEnumerable.GetEnumerator

            Return GetEnumerator()
        End Function
#End Region

    End Class

End Namespace