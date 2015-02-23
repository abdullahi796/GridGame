Imports System.IO
Public Class Form1
    'Comment
    Public grid(9, 9) As Tile
    Dim hole(9, 9) As Tile
    Dim isRunning As Boolean = True
    Dim selectTile(3) As selectTile
    Dim mouseImg As String = "UpRight.png"
    Dim countI As Integer
    Dim player As Ball
    Dim countC As Integer
    Dim key As String
    Dim reader As StreamReader = New StreamReader("selectTile.txt")
    Dim pic1 As Boolean = False
    Dim pic2 As Boolean = False
    Dim pic3 As Boolean = False

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.Space Then
            If player.key = "space" Then
                player.key = ""
            Else
                player.key = "space"
            End If
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
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Panel1.Width = Me.Width
        Panel1.Height = Me.Height
        Panel1.Left = 0
        Panel1.Top = 0
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

        For i = 0 To 4
            tileReader = reader.ReadLine
            selectTile(i) = New selectTile(11, i, tileReader)
            selectTile(i).setup()
            Panel1.Controls.Add(selectTile(i).tile)
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
        For i = 0 To 2
            selectTile(i).tile.Visible = False
            If selectTile(i).tile.Bounds.Contains(PointToClient(MousePosition)) And MouseButtons = Windows.Forms.MouseButtons.Left Then
                mouseImg = selectTile(i).img
            End If
        Next
    End Sub


    'Main Loop
    Private Sub tmrLoop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoop.Tick
        Panel1.Width = Me.Width
        Panel1.Height = Me.Height
        Me.BackColor = Color.FromArgb(31, 218, 175)
        For i = 0 To 9
            For c = 0 To 9
                grid(i, c).display()
            Next
        Next

        For i = 0 To 2
            selectTile(i).display()
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
            Debug.Print(lastTile)
        End Sub
        Public Sub move()
            If key = "space" Then
                ball.Top = y - 5
                ball.Left = x + 3
                If down = "arrowDown.png" And locY + 1 < 9 Then
                    lastTile = "Up"
                    last = "Down"
                    y += 69
                    locY += 1
                ElseIf down = "UpRight.png" And lastTile <> "Down" And locY + 1 < 9 Then
                    lastTile = "Up"
                    last = "Right"
                    y += 60
                    locY += 1
                ElseIf down = "UpLeft.png" And lastTile <> "Down" And locY + 1 < 9 Then
                    lastTile = "Up"
                    last = "Left"
                    y += 60
                    locY += 1
                ElseIf up = "DownRight.png" And lastTile <> "Up" And locY - 1 < 9 Then
                    last = "Right"
                    lastTile = "Down"
                    y -= 60
                    locY -= 1
                ElseIf up = "DownLeft.png" And lastTile <> "Up" And locY - 1 < 9 Then
                    last = "Left"
                    lastTile = "Down"
                    y -= 60
                    locY -= 1
                ElseIf left = "UpRight.png" And lastTile <> "Left" And locX - 1 > 0 Then
                    last = "Up"
                    lastTile = "Right"
                    x -= 60
                    locX -= 1
                ElseIf left = "DownRight.png" And lastTile <> "Left" And locX - 1 > 0 Then
                    last = "Down"
                    lastTile = "Right"
                    x -= 60
                    locX -= 1
                ElseIf right = "DownLeft.png" And lastTile <> "Right" And locX + 1 < 9 Then
                    last = "Down"
                    lastTile = "Left"
                    x += 60
                    locX += 1
                ElseIf right = "UpLeft.png" And lastTile <> "Right" And locX + 1 < 9 Then
                    last = "Up"
                    lastTile = "Left"
                    x += 60
                    locX += 1
                ElseIf last = "Up" And locY - 1 > 0 Then
                    lastTile = "Down"
                    y -= 60
                    locY -= 1
                ElseIf last = "Down" And locY + 1 < 9 Then
                    lastTile = "Up"
                    y += 60
                    locY += 1
                ElseIf last = "Right" And locX + 1 < 9 Then
                    lastTile = "Left"
                    x += 60
                    locX += 1
                ElseIf last = "Left" And locX - 1 > 0 Then
                    lastTile = "Right"
                    x -= 60
                    locX -= 1
                End If
                If current = "Check.jpg" Then
                    key = ""
                    Form1.restart()
                End If
            End If
        End Sub
    End Class

    Private Sub tmrMove_Tick(sender As System.Object, e As System.EventArgs) Handles tmrMove.Tick
        player.move()
        player.right = grid(player.locX + 1, player.locY).img
        player.left = grid(player.locX - 1, player.locY).img
        player.up = grid(player.locX, player.locY - 1).img
        player.down = grid(player.locX, player.locY + 1).img
        player.current = grid(player.locX, player.locY).img
    End Sub

    Private Sub tmrUI_Tick(sender As System.Object, e As System.EventArgs) Handles tmrUI.Tick
        If Label1.Bounds.Contains(PointToClient(MousePosition)) Then
            showUI("Show")
        Else
            showUI("Hide")
        End If
        If Label2.Left < 15 Then
            Label2.Visible = False
        End If
        If Label3.Left < 15 Then
            Label3.Visible = False
        End If
    End Sub

    Public Sub showUI(ByVal value As String)
        If value = "Hide" Then
            If pic2 = True Then
                If Label2.Left > 12 Then
                    Label2.Left -= 20
                Else
                    pic2 = False
                End If
            ElseIf pic3 = True Then
                If Label3.Left > 12 Then
                    Label3.Left -= 20
                Else
                    pic3 = False
                End If
            End If
        ElseIf value = "Show" Then
            If pic2 = False Then
                If Label2.Left < 50 Then
                    Label2.Visible = True
                    Label2.Left += 20
                Else
                    pic2 = True
                End If
            ElseIf pic3 = False Then
                If Label3.Left < 50 Then
                    Label3.Visible = True
                    Label3.Left += 20
                Else
                    pic3 = True
                End If
            End If
        End If
    End Sub
    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
End Class

