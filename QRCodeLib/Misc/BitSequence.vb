﻿Imports System
Imports System.Collections.Generic

Namespace Ys.Misc

    ''' <summary>
    ''' ビット列の生成機能を提供します。
    ''' </summary>
    Friend Class BitSequence

        Private _buffer As List(Of Byte)
        Private _bitCounter As Integer
        Private _space      As Integer

        ''' <summary>
        ''' インスタンスを初期化します。
        ''' </summary>
        Public Sub New()
            Clear()
        End Sub

        ''' <summary>
        ''' ビット数を取得します。
        ''' </summary>
        Public ReadOnly Property Length() As Integer
            Get
                Return _bitCounter
            End Get
        End Property

        Public sub Clear()
            _buffer = New List(Of Byte)()
            _bitCounter = 0
            _space = 0
        End sub

        ''' <summary>
        ''' 指定のビット数でデータを追加します。
        ''' </summary>
        ''' <param name="data">追加するデータ</param>
        ''' <param name="length">データのビット数</param>
        Public Sub Append(data As Integer, length As Integer)
            Dim remainingLength As Integer = length
            Dim remainingData   As Integer = data

            Do While remainingLength > 0
                If _space = 0 Then
                    _space = 8
                    _buffer.Add(&H0)
                End If

                Dim temp As Byte = _buffer(_buffer.Count - 1)

                If _space < remainingLength Then
                    temp = CByte(temp Or remainingData >> (remainingLength - _space))
                    remainingData = remainingData And ((1 << (remainingLength - _space)) - 1)
                    _bitCounter += _space
                    remainingLength -= _space
                    _space = 0
                Else
                    temp = CByte(temp Or remainingData << (_space - remainingLength))
                    _bitCounter += remainingLength
                    _space -= remainingLength
                    remainingLength = 0
                End If

                _buffer(_buffer.Count - 1) = temp
            Loop
        End Sub

        ''' <summary>
        ''' データのバイト配列を返します。
        ''' </summary>
        Public Function GetBytes() As Byte()
            Return _buffer.ToArray()
        End Function

    End Class

End Namespace
