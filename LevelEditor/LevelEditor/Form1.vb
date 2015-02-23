Imports System.IO
Public Class Form1
    'Comment
    Public grid(9, 9) As Tile
    Dim hole(9, 9) As Tile
    Dim isRunning As Boolean = True
    Dim mouseImg As String = "UpRight.png"
    Dim countI As Integer
    Dim player As Ball
    Dim countC As Integer
    Dim key As String

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyValue = Keys.Space Then
            player.key = "space"
        End If
        If e.KeyValue = Keys.A Then
            key = "1"
        End If
        If e.KeyValue = Keys.S Then
            key = "2"
        End If
        If e.KeyValue = Keys.D Then
            key = "3"
        End If
        If e.KeyValue = Keys.F Then
            key = "4"
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Panel1.Width = Me.Width
        Panel1.Height = Me.Height
        player = New Ball(1, 1, "Ball_0.png")
        player.setup()
        Panel1.Controls.Add(player.ball)
        Dim tileReader As String


        For i = 0 To 9
            For c = 0 To 9
                'If i = 1 And c = 7 Then
                '    grid(i, c) = New Tile(i, c, "Hole.png")
                '    grid(i, c).setup()
                '    Panel1.Controls.Add(grid(i, c).tile)
                'Else
                If c = 9 Then
                    grid(i, c) = New Tile(i, c, "Tile_1.png")
                    grid(i, c).setup()
                    Panel1.Controls.Add(grid(i, c).tile)
                Else
                    grid(i, c) = New Tile(i, c, "Tile_0.png")
                    grid(i, c).setup()
                    Panel1.Controls.Add(grid(i, c).tile)
                End If
            Next
        Next
    End Sub


    'Mouse Position and Clicks
    Public Sub mousePos()
        If key = "1" Then
            mouseImg = "DownLeft.png"
        ElseIf key = "2" Then
            mouseImg = "DownRight.png"
        ElseIf key = "3" Then
            mouseImg = "UpLeft.png"
        ElseIf key = "4" Then
            mouseImg = "UpRight.png"
        End If
        For i = 0 To 9
            For c = 0 To 9
                If grid(i, c).tile.Bounds.Contains(PointToClient(MousePosition)) And MouseButtons = Windows.Forms.MouseButtons.Left Then
                    grid(i, c).img = mouseImg
                End If
            Next
        Next
    End Sub


    'Main Loop
    Private Sub tmrLoop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Panel1.Width = Me.Width
        Panel1.Height = Me.Height
        Me.BackColor = Color.FromArgb(31, 218, 175)
        For i = 0 To 9
            For c = 0 To 9
                grid(i, c).display()
            Next
        Next

        grid(1, 2).img = "arrowDown.png"
        grid(7, 7).img = "Check.jpg"
        Debug.Print(player.left)
        mousePos()
    End Sub

    Public Sub restart()
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
        Public lastTile As String
        Public lastCount As Integer

        Public Sub New(ByVal tempX As Integer, ByVal tempY As Integer, ByVal tempImg As String)
            x = tempX * 60
            y = tempY * 60
            x += 370
            y += 55
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
            ball.BackColor = Color.Transparent
            ball.SizeMode = PictureBoxSizeMode.AutoSize
            'Debug.Print(x & " " & y)
        End Sub
        Public Sub move()
            If key = "space" Then
                ball.Top = y - 5
                ball.Left = x + 3
                If down = "arrowDown.png" And locY + 1 < 9 Then
                    last = "Down"
                    y += 69
                    locY += 1
                    lastTile = current
                ElseIf down = "UpRight.png" And lastTile <> down And locY + 1 < 9 Then
                    last = "Right"
                    y += 60
                    locY += 1
                    lastTile = current
                ElseIf down = "UpLeft.png" And lastTile <> down And locY + 1 < 9 Then
                    last = "Left"
                    y += 60
                    locY += 1
                    lastTile = current
                ElseIf up = "DownRight.png" And lastTile <> up And locY - 1 < 9 Then
                    last = "Right"
                    y -= 60
                    locY -= 1
                    lastTile = current
                ElseIf up = "DownLeft.png" And lastTile <> up And locY - 1 < 9 Then
                    last = "Left"
                    y -= 60
                    locY -= 1
                    lastTile = current
                ElseIf left = "UpRight.png" And lastTile <> left And locX - 1 > 0 Then
                    last = "Up"
                    x -= 60
                    locX -= 1
                    lastTile = current
                ElseIf left = "DownRight.png" And lastTile <> left And locX - 1 > 0 Then
                    last = "Down"
                    x -= 60
                    locX -= 1
                    lastTile = current
                ElseIf right = "DownLeft.png" And lastTile <> right And locX + 1 < 9 Then
                    last = "Down"
                    x += 60
                    locX += 1
                    lastTile = current
                ElseIf right = "UpLeft.png" And lastTile <> right And locX + 1 < 9 Then
                    last = "Up"
                    x += 60
                    locX += 1
                    lastTile = current
                ElseIf last = "Up" And locY - 1 > 0 Then
                    y -= 60
                    locY -= 1
                    lastTile = current
                ElseIf last = "Down" And locY + 1 < 9 Then
                    y += 60
                    locY += 1
                    lastTile = current
                ElseIf last = "Right" And locX + 1 < 9 Then
                    x += 60
                    locX += 1
                    lastTile = current
                ElseIf last = "Left" And locX - 1 > 0 Then
                    x -= 60
                    locX -= 1
                    lastTile = current
                End If
                If current = "Check.jpg" Then
                    key = ""
                    Form1.restart()
                End If
            End If
        End Sub
    End Class
End Class
