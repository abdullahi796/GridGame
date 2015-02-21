'Class for selecting ti
Public Class selectTile
    Public x As Integer
    Public y As Integer
    Public img As String
    Public tile As PictureBox
    Public count As Integer

    Public Sub New(ByVal tempX As Integer, ByVal tempY As Integer, ByVal tempImg As String)
        x = tempX
        y = tempY
        img = tempImg
    End Sub
    Public Sub setup()
        tile = New Windows.Forms.PictureBox
        tile.Visible = True
        tile.Left = x * 60
        tile.Top = y * 60
        tile.Width = 60
        tile.Height = 60
    End Sub
    Public Sub display()
        tile.Image = Image.FromFile(img)
    End Sub
End Class
