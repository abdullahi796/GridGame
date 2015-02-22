Imports System.IO
Public Class Form1
    Public grid(9, 9) As Tile
    Dim hole(9, 9) As Tile
    Dim isRunning As Boolean = True
    Dim mouseImg As String = "arrowRight.jpg"
    Dim countI As Integer
    Dim countC As Integer
    Dim key As String
    Dim picArray(12) As PictureBox
    Dim picString(12) As String
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i = 0 To 9
            For c = 0 To 9
                grid(i, c) = New Tile(i, c, "Blank.png")
                grid(i, c).setup()
                Me.Controls.Add(grid(i, c).tile)
            Next
        Next
        picArray(1) = Me.PictureBox2
        picArray(2) = Me.PictureBox3
        picArray(3) = Me.PictureBox4
        picArray(4) = Me.PictureBox5
        picArray(5) = Me.PictureBox6
        picArray(6) = Me.PictureBox7
        picArray(7) = Me.PictureBox8
        picArray(8) = Me.PictureBox9
        picArray(9) = Me.PictureBox10
        picArray(10) = Me.PictureBox11
        picArray(11) = Me.PictureBox12
        picArray(12) = Me.PictureBox13

        picString(1) = "arrowDown.jpg"
        picString(2) = "CurveUp_Right.jpg"
        picString(3) = "CurveUp_Left.jpg"
        picString(4) = "CurveLeft_Down.jpg"
        picString(5) = "CurveRight_Down.jpg"
        picString(6) = "CurveLeft_Up.jpg"
        picString(7) = "CurveRight_Up.jpg"
        picString(8) = "CurveDown_Right.jpg"
        picString(9) = "CurveDown_Left.jpg"
        picString(10) = "Blank.png"
        picString(11) = "Check.jpg"
        picString(12) = "Hover.png"
    End Sub
    Public Sub mousePos()
        For i = 0 To 9
            For c = 0 To 9
                If grid(i, c).tile.Bounds.Contains(PointToClient(MousePosition)) And MouseButtons = Windows.Forms.MouseButtons.Left Then
                    grid(i, c).img = mouseImg
                End If
            Next
        Next
        For i = 1 To 12
            If picArray(i).Bounds.Contains(PointToClient(MousePosition)) And MouseButtons = Windows.Forms.MouseButtons.Left Then
                mouseImg = picString(i)
            End If
        Next
    End Sub

    Private Sub tmrLoop_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoop.Tick
        For i = 0 To 9
            For c = 0 To 9
                grid(i, c).display()
            Next
        Next
        mousePos()
        ' Cursor = UseWaitCursor.GetTypeCode
    End Sub



    Public Class Tile
        Public x As Integer
        Public y As Integer
        Public img As String
        Public tile As PictureBox

        Public Sub New(ByVal tempX As Integer, ByVal tempY As Integer, ByVal tempImg As String)
            x = tempX
            y = tempY
            img = tempImg
        End Sub
        Public Sub setup()
            tile = New Windows.Forms.PictureBox
            tile.Visible = True
            tile.Image = Image.FromFile("Blank.png")
            tile.Left = x * 60
            tile.Top = y * 60
            tile.Width = 60
            tile.Height = 60
        End Sub
        Public Sub display()
            tile.Image = Image.FromFile(img)
        End Sub
    End Class

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub
End Class
