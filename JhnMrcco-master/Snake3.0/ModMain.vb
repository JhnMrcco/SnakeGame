Module ModMain
#Region "Structure/Variables"

    Structure Parts
        Public x As Long
        Public y As Long
        Public facing As Integer
    End Structure
    Structure foodXY
        Public x As Long
        Public y As Long
    End Structure

    Public snake(99) As Parts
    Public length As Long
    Public food As foodXY
    Public i As Integer = 0

    Public score As Integer
    Public GameOver As Boolean = False
    Public music As Boolean = False
    Public powerup As Integer
    Public snakespeed As Integer = 100
    Public happyend As Boolean = False
    Public Mode As String
    Public FoodColor As String
    Public HedColor As String
    Public BodColor As String
    Public speed As Integer
    Public speedcrawl As Integer
    Public initspeed As Integer

#End Region

End Module
