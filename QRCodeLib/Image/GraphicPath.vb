Imports System
Imports System.Collections.Generic
Imports System.Drawing

Imports Ys.QRCode

Friend Class GraphicPath
    Private Enum Direction
        UP = 0
        DOWN
        LEFT
        RIGHT
    End Enum

    Public Shared Function FindContours(image As Integer()()) As Point()()
        Dim gps = New List(Of Point())()

        For y = 0 To UBound(image) - 1
            For x = 0 To UBound(image(y)) - 1
                If image(y)(x) = Integer.MaxValue Then
                    Continue For
                End If

                If Not (image(y)(x) > 0 AndAlso image(y)(x + 1) <= 0) Then
                    Continue For
                End If

                image(y)(x) = Integer.MaxValue
                Dim start = New Point(x, y)
                Dim gp = New List(Of Point) From {start}

                Dim dr As Direction = Direction.UP
                Dim p As Point = New Point(start.X, start.Y - 1)

                Do
                    Select Case dr
                        Case Direction.UP
                            If image(p.Y)(p.X) > 0 Then
                                image(p.Y)(p.X) = Integer.MaxValue

                                If image(p.Y)(p.X + 1) <= 0 Then
                                    p = New Point(p.X, p.Y - 1)
                                Else
                                    gp.Add(p)
                                    dr = Direction.RIGHT
                                    p = New Point(p.X + 1, p.Y)
                                End If
                            Else
                                p = New Point(p.X, p.Y + 1)
                                gp.Add(p)
                                dr = Direction.LEFT
                                p = New Point(p.X - 1, p.Y)
                            End If

                        Case Direction.DOWN
                            If image(p.Y)(p.X) > 0 Then
                                image(p.Y)(p.X) = Integer.MaxValue

                                If image(p.Y)(p.X - 1) <= 0 Then
                                    p = New Point(p.X, p.Y + 1)
                                Else
                                    gp.Add(p)
                                    dr = Direction.LEFT
                                    p = New Point(p.X - 1, p.Y)
                                End If
                            Else
                                p = New Point(p.X, p.Y - 1)
                                gp.Add(p)
                                dr = Direction.RIGHT
                                p = New Point(p.X + 1, p.Y)
                            End If

                        Case Direction.LEFT
                            If image(p.Y)(p.X) > 0 Then
                                image(p.Y)(p.X) = Integer.MaxValue

                                If image(p.Y - 1)(p.X) <= 0 Then
                                    p = New Point(p.X - 1, p.Y)
                                Else
                                    gp.Add(p)
                                    dr = Direction.UP
                                    p = New Point(p.X, p.Y - 1)
                                End If
                            Else
                                p = New Point(p.X + 1, p.Y)
                                gp.Add(p)
                                dr = Direction.DOWN
                                p = New Point(p.X, p.Y + 1)
                            End If

                        Case Direction.RIGHT
                            If image(p.Y)(p.X) > 0 Then
                                image(p.Y)(p.X) = Integer.MaxValue

                                If (image(p.Y + 1)(p.X) <= 0) Then
                                    p = New Point(p.X + 1, p.Y)
                                Else
                                    gp.Add(p)
                                    dr = Direction.DOWN
                                    p = New Point(p.X, p.Y + 1)
                                End If
                            Else
                                p = New Point(p.X - 1, p.Y)
                                gp.Add(p)
                                dr = Direction.UP
                                p = New Point(p.X, p.Y - 1)
                            End If

                        Case Else
                            Throw New InvalidOperationException()
                    End Select

                Loop While (p <> start)

                gps.Add(gp.ToArray())
            Next
        Next

        Return gps.ToArray()
    End Function

End Class
