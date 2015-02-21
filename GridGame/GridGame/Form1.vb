﻿Imports System.IO
Public Class Form1
    Public grid(9, 9) As Tile
    Dim hole(9, 9) As Tile
    Dim isRunning As Boolean = True
    Dim selectTile(3) As selectTile
    Dim mouseImg As String = "arrowRight.jpg"
    Dim countI As Integer
    Dim player As Ball
    Dim countC As Integer
    Dim key As String
    Dim reader As StreamReader = New StreamReader("selectTile.txt")

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.Space Then
            player.key = "space"
        End If
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Space Then
            'player.key = ""
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        player = New Ball(1, 1, "Ball_0.png")
        player.setup()
        Me.Controls.Add(player.ball)
        Dim tileReader As String

        For i = 0 To 9
            For c = 0 To 9
                If i = 1 And c = 7 Then
                    grid(i, c) = New Tile(i, c, "Hover.png")
                    grid(i, c).setup()
                    Me.Controls.Add(grid(i, c).tile)
                Else
                    grid(i, c) = New Tile(i, c, "Blank.png")
                    grid(i, c).setup()
                    Me.Controls.Add(grid(i, c).tile)
                End If
            Next
        Next

        For i = 0 To 4
            tileReader = reader.ReadLine
            selectTile(i) = New selectTile(11, i, tileReader)
            selectTile(i).setup()
            Me.Controls.Add(selectTile(i).tile)
        Next
    End Sub


    'Mouse Position and Clicks
    Public Sub mousePos()
        For i = 0 To 9
            For c = 0 To 9
                If grid(i, c).tile.Bounds.Contains(PointToClient(MousePosition)) And MouseButtons = Windows.Forms.MouseButtons.Left And grid(i, c).img = "Hover.png" Then
                    grid(i, c).img = mouseImg
                End If
            Next
        Next
        For i = 0 To 1
            For c = 0 To 2
                If selectTile(i).tile.Bounds.Contains(PointToClient(MousePosition)) And MouseButtons = Windows.Forms.MouseButtons.Left Then
                    mouseImg = selectTile(i).img
                    countC = c
                    countI = i
                End If
            Next
        Next
    End Sub


    'Main Loop
    Private Sub tmrLoop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoop.Tick
        For i = 0 To 9
            For c = 0 To 9
                grid(i, c).display()
            Next
        Next

        For i = 0 To 2
            selectTile(i).display()
        Next
        grid(1, 2).img = "arrowDown.jpg"
        grid(7, 7).img = "Check.jpg"
        mousePos()
        player.move()
        player.right = grid(player.locX + 1, player.locY).img
        player.left = grid(player.locX - 1, player.locY).img
        player.up = grid(player.locX, player.locY - 1).img
        player.down = grid(player.locX, player.locY + 1).img
        player.current = grid(player.locX, player.locY).img
        Debug.Print(player.right)
    End Sub

    Public Sub restart()
        player = New Ball(1, 1, "Ball_0.png")
        grid(1, 7).img = "Hover.png"
    End Sub

    Public Class Ball
        Public x As Integer
        Public y As Integer
        Public img As String
        Public ball As PictureBox
        Public count As Integer
        Public current As String
        Public locX As Integer
        Public locY As Integer
        Public left As String
        Public right As String
        Public up As String
        Public down As String
        Public key As String
        Public last As String

        Public Sub New(ByVal tempX As Integer, ByVal tempY As Integer, ByVal tempImg As String)
            x = tempX * 60
            y = tempY * 60
            x += 5
            y += 5
            img = tempImg
            locX = tempX
            locY = tempY
        End Sub
        Public Sub setup()
            ball = New Windows.Forms.PictureBox
            ball.Image = Image.FromFile(img)
            ball.Visible = True
            ball.Left = x
            ball.Top = y
            ball.Width = 48
            ball.Height = 48
            'Debug.Print(x & " " & y)
        End Sub
        Public Sub move()
            If key = "space" Then
                ball.Top = y - 5
                ball.Left = x + 3
                If down = "arrowDown.jpg" And locY + 1 < 9 Then
                    last = "arrowDown.jpg"
                    y += 69
                    locY += 1
                ElseIf right = "arrowRight.jpg" And locX + 1 < 9 Then
                    last = "arrowRight.jpg"
                    x += 60
                    locX += 1
                ElseIf down = "CurveUp_Right.jpg" And locY + 1 < 9 Then
                    last = "arrowRight.jpg"
                    y += 60
                    locY += 1
                ElseIf last = "arrowDown.jpg" And locY + 1 < 9 Then
                    y += 60
                    locY += 1
                ElseIf last = "arrowRight.jpg" And locX + 1 < 9 Then
                    x += 60
                    locX += 1
                End If
                If current = "Check.jpg" Then
                    key = ""
                    Form1.restart()
                End If
            End If
        End Sub
    End Class
End Class
