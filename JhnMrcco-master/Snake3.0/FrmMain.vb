Public Class FrmMAin


#Region "Form Events"
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Determines what keys are pressed and what to do when each one is pressed

        Select Case e.KeyCode
            Case 37
                snake(0).facing = 1 'Left
            Case 38
                snake(0).facing = 2 'Up
            Case 39
                snake(0).facing = 3 'Right
            Case 40
                snake(0).facing = 4 'Down
            Case Keys.P
                'Pauses the game, it toggles the state of the timer's enabled value               
                'ie: if its set as true then it gets changed to not true and vice verse
                If GameOver = False Then
                    Timer1.Enabled = Not Timer1.Enabled

                End If
            Case Keys.R

                StartUp()

            Case Keys.Escape
                'exits the game
                End
        End Select

        Select Case e.KeyCode
            Case Keys.A
                snake(0).facing = 1 'Left
            Case Keys.W
                snake(0).facing = 2 'Up
            Case Keys.D
                snake(0).facing = 3 'Right
            Case Keys.S
                snake(0).facing = 4 'Down
        End Select

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'sets buffering and rendering settings to better optimize painting on the form
        PictureBox2.Visible = False
        Panel2.Visible = False

        cboBody.Items.Add("Blue")
        cboBody.Items.Add("Green")
        cboBody.Items.Add("Pink")
        cboBody.Items.Add("Red")
        cboBody.Items.Add("White")
        cboBody.Items.Add("Yellow")

        cboHead.Items.Add("Blue")
        cboHead.Items.Add("Green")
        cboHead.Items.Add("Pink")
        cboHead.Items.Add("Red")
        cboHead.Items.Add("White")
        cboHead.Items.Add("Yellow")

        cboFood.Items.Add("Blue")
        cboFood.Items.Add("Green")
        cboFood.Items.Add("Pink")
        cboFood.Items.Add("Red")
        cboFood.Items.Add("White")
        cboFood.Items.Add("Yellow")

        cboMode.Items.Add("Bordered")
        cboMode.Items.Add("Borderless")


        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.UpdateStyles()
        'My.Computer.Audio.Play("music\loop2.wav", AudioPlayMode.BackgroundLoop)
        'starts the game
        Call StartUp()
    End Sub
#End Region

#Region "Timer Loops for Snake and Moster Movement"
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        PictureBox1.Refresh()
        Call findfood()
        Call movesnake()
        Call MonsterEat()
        Call SpeedSnake()
        Label1.Text = score


    End Sub

    Private Sub SpeedSnake()
        speed = 0
        If speed = 0 Then
            Timer1.Interval = 50

        Else
            Timer1.Interval = speedcrawl
            Call StartUp()
        End If
    End Sub

#End Region

#Region "place things"
    Private Sub StartUp()

        For d As Integer = 0 To 99
            snake(d).x = 0
            snake(d).y = 0
            snake(d).facing = 3
        Next
        food.x = 0
        food.y = 0
        score = 0

        GameOver = False
        length = 3
        For a As Integer = 0 To 4
            snake(a).x = (120 - 10 * a)
            snake(a).y = 120
            Draw(snake(0).x, snake(0).y, Brushes.Blue)
            snake(a).facing = 3

        Next
        Randomize()

        PlaceObject(food.x, food.y)
        'Call findQuads()
        Timer1.Enabled = True

    End Sub

    Function PlaceObject(ByRef X As Integer, ByRef Y As Integer)
        Do
            X = CInt(Int(((PictureBox1.Width - 10) * Rnd()) + 10))
        Loop While (X Mod 10 <> 0)
        Do
            Y = CInt(Int(((PictureBox1.Height - 10) * Rnd()) + 10))
        Loop While (Y Mod 10 <> 0)
        Return Nothing
    End Function
#End Region

#Region "move things"

    Private Sub movesnake()

        If length > 1000 Then
            Call GameIsOver(True)
        Else

            For i As Integer = (length - 1) To 1 Step (-1)
                snake(i).x = snake(i - 1).x 'sets the snake.x to the snake.x in front of it
                snake(i).y = snake(i - 1).y 'sets the snake.y  to the snake.y in front of it
                snake(i).facing = snake(i + 1).facing 'sets the snake.facing to the snake.facing in front of it
                If i = 0 Then
                    Draw(snake(i).x, snake(i).y, Brushes.Green)
                Else
                    'Draw(snake(i).x, snake(i).y, Brushes.Red)
                    'Bodycolor()
                    If BodColor = "" Then
                        BodColor = ""
                    Else
                        BodColor = cboBody.Text
                    End If

                    Select Case BodColor
                        Case ""
                            Draw(snake(i).x, snake(i).y, Brushes.Red)
                        Case "Blue"
                            Draw(snake(i).x, snake(i).y, Brushes.Aqua)
                        Case "Green"
                            Draw(snake(i).x, snake(i).y, Brushes.LightGreen)
                        Case "Pink"
                            Draw(snake(i).x, snake(i).y, Brushes.LightPink)
                        Case "Red"
                            Draw(snake(i).x, snake(i).y, Brushes.Red)
                        Case "White"
                            Draw(snake(i).x, snake(i).y, Brushes.Snow)
                        Case "Yellow"
                            Draw(snake(i).x, snake(i).y, Brushes.Yellow)

                    End Select

                End If
                ' paints the snakepart
            Next i

            If snake(0).facing = 1 Then         'Moves left
                snake(0).x = snake(0).x - 10
            ElseIf snake(0).facing = 2 Then     'Moves up
                snake(0).y = snake(0).y - 10
            ElseIf snake(0).facing = 3 Then     'Moves right
                snake(0).x = snake(0).x + 10
            ElseIf snake(0).facing = 4 Then     'Moves down
                snake(0).y = snake(0).y + 10
            End If
            'Draw the head and redraw the food because i refreshed the picturebox1

            Headcolor()
            colorFood()

        End If
    End Sub
#End Region

#Region "Color"
    Private Sub Headcolor()
        If HedColor = "" Then
            HedColor = ""
        Else
            HedColor = cboHead.Text
        End If

        Select Case HedColor
            Case ""
                Draw(snake(0).x, snake(0).y, Brushes.Aqua)
            Case "Blue"
                Draw(food.x, food.y, Brushes.Aqua)
            Case "Green"
                Draw(snake(0).x, snake(0).y, Brushes.LightGreen)
            Case "Pink"
                Draw(snake(0).x, snake(0).y, Brushes.LightPink)
            Case "Red"
                Draw(snake(0).x, snake(0).y, Brushes.Red)
            Case "White"
                Draw(snake(0).x, snake(0).y, Brushes.Snow)
            Case "Yellow"
                Draw(snake(0).x, snake(0).y, Brushes.Yellow)

        End Select
    End Sub

    Private Sub colorFood()
        If FoodColor = "" Then
            FoodColor = ""
        Else
            FoodColor = cboFood.Text
        End If

        Select Case FoodColor
            Case ""
                Draw(food.x, food.y, Brushes.Yellow)
            Case "Blue"
                Draw(food.x, food.y, Brushes.Aqua)
            Case "Green"
                Draw(food.x, food.y, Brushes.LightGreen)
            Case "Pink"
                Draw(food.x, food.y, Brushes.LightPink)
            Case "Red"
                Draw(food.x, food.y, Brushes.Red)
            Case "White"
                Draw(food.x, food.y, Brushes.Snow)
            Case "Yellow"
                Draw(food.x, food.y, Brushes.Yellow)

        End Select
    End Sub
#End Region

#Region "Collision Detection"
    Public Sub MonsterEat()
        Mode = ""
        ' default Mode is Borderless
        Select Case Mode

            Case "Bordered"
                If snake(0).x >= 963 Then
                    snake(0).x = 0
                    GameIsOver(False)
                    PictureBox2.Visible = True
                ElseIf snake(0).x < 0 Then
                    snake(0).x = 963
                    GameIsOver(False)
                    PictureBox2.Visible = True
                End If
                If snake(0).y >= 470 Then
                    snake(0).y = 0
                    GameIsOver(False)
                    PictureBox2.Visible = True
                ElseIf snake(0).y <= 0 Then
                    snake(0).y = 470
                    GameIsOver(False)
                    PictureBox2.Visible = True
                End If

            Case "Borderless"
                If snake(0).x >= 963 Then
                    snake(0).x = 0
                ElseIf snake(0).x < 0 Then
                    snake(0).x = 963
                End If
                If snake(0).y >= 470 Then
                    snake(0).y = 0
                ElseIf snake(0).y <= 0 Then
                    snake(0).y = 470
                End If

            Case ""
                If snake(0).x >= 963 Then
                    snake(0).x = 0
                ElseIf snake(0).x < 0 Then
                    snake(0).x = 963
                End If
                If snake(0).y >= 470 Then
                    snake(0).y = 0
                ElseIf snake(0).y <= 0 Then
                    snake(0).y = 470
                End If

        End Select
        ' I have set the snake(0).x and snake(0).y to be set as snake(0).x = 0 and snake(0).y = 0 if they hit the wall
    End Sub

    Private Sub findfood()
        Dim timesUsed As Integer = 1
        '////////////
        ' NEW NEW NEW 12-11-10
        '////////////
        'change to select case statement and add a case for checking length to determin the end game

        If snake(0).x = food.x And snake(0).y = food.y Then
            If length > 100 Then
                'game over wow you played for a long time to get this far

                Call GameIsOver(True)
            Else

                Select Case snake(length - 1).facing
                    Case 1 'Looking left
                        snake(length).x = snake(length - 1).x - 10
                        snake(length).y = snake(length - 1).y
                    Case 2 'Looking up
                        snake(length).x = snake(length - 1).x
                        snake(length).y = snake(length - 1).y + 10
                    Case 3 'Looking right
                        snake(length).x = snake(length - 1).x - 10
                        snake(length).y = snake(length - 1).y
                    Case 4 'Looking down
                        snake(length).x = snake(length - 1).x
                        snake(length).y = snake(length - 1).y - 10

                End Select
            End If
            'If snake(length - 1).facing = 1 Then
            '    snake(length).x = snake(length - 1).x - 10
            '    snake(length).y = snake(length - 1).y
            'ElseIf snake(length - 1).facing = 2 Then
            '    snake(length).x = snake(length - 1).x
            '    snake(length).y = snake(length - 1).y + 10
            'ElseIf snake(length - 1).facing = 3 Then
            '    snake(length).x = snake(length - 1).x - 10
            '    snake(length).y = snake(length - 1).y
            'ElseIf snake(length - 1).facing = 4 Then
            '    snake(length).x = snake(length - 1).x
            '    snake(length).y = snake(length - 1).y - 10
            'End If
            If music = True Then
                My.Computer.Audio.Play(My.Resources.eat, AudioPlayMode.Background)
            End If
            Dim chance As Integer = CInt(Int((100 * Rnd()) + 1))
            If chance <= 50 Then
                powerup = CInt(Int((9 * Rnd()) + 1))
                If powerup <= 2 Then 'less than 2 (2/9 chance)
                    length += 5
                ElseIf powerup >= 3 And powerup <= 5 Then 'greater than 3 less than 5 (3/9 chance)
                    snakespeed = 90 - (timesUsed * 5)
                    timesUsed += 1
                ElseIf powerup >= 6 Then 'great than 6 less than 9 (4/9 chance)
                    length += 1
                End If


            End If
            'increase the length of the snake
            length += 1
            score += 100
            PlaceObject(food.x, food.y)
        End If
    End Sub
#End Region

#Region "MISC"

    Public Function Draw(ByVal x As Integer, ByVal y As Integer, ByVal color As System.Drawing.Brush)
        Dim g As Graphics
        g = PictureBox1.CreateGraphics
        g.FillRectangle(color, x, y, 8, 8)
        Return Nothing
    End Function

    Function GameIsOver(ByVal YouWon As Boolean)
        GameOver = True
        Timer1.Enabled = False

        If YouWon = True Then
            PictureBox2.Visible = True
            PictureBox2.Image = (My.Resources.win)
        Else
            PictureBox2.Visible = True
            PictureBox2.Image = (My.Resources.Gameover)
        End If
        Return Nothing
    End Function
#End Region

#Region "Settings"

    Private Sub picSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picSetting.Click
        Timer1.Stop()
        Panel2.Visible = True
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        If cboFood.Text = "" Then
            FoodColor = ""
            Timer1.Start()
            Panel2.Hide()
        ElseIf cboFood.Text = "Blue" Then
            FoodColor = "Blue"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboFood.Text = "Green" Then
            FoodColor = "Green"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboFood.Text = "Pink" Then
            FoodColor = "Pink"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboFood.Text = "Red" Then
            FoodColor = "Red"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboFood.Text = "White" Then
            FoodColor = "White"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboFood.Text = "Yellow" Then
            FoodColor = "Yellow"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        End If

        If cboHead.Text = "" Then
            HedColor = "Red"
            Timer1.Start()
            Panel2.Hide()
        ElseIf cboHead.Text = "Blue" Then
            HedColor = "Blue"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboHead.Text = "Green" Then
            HedColor = "Green"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboHead.Text = "Pink" Then
            HedColor = "Pink"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboHead.Text = "Red" Then
            HedColor = "Red"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboHead.Text = "White" Then
            HedColor = "White"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboHead.Text = "Yellow" Then
            HedColor = "Yellow"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        End If

        If cboBody.Text = "" Then
            BodColor = ""
            Timer1.Start()
            Panel2.Hide()
        ElseIf cboBody.Text = "Blue" Then
            BodColor = "Blue"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboBody.Text = "Green" Then
            BodColor = "Green"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboBody.Text = "Pink" Then
            BodColor = "Pink"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboBody.Text = "Red" Then
            BodColor = "Red"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboBody.Text = "White" Then
            BodColor = "White"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        ElseIf cboBody.Text = "Yellow" Then
            BodColor = "Yellow"
            Timer1.Start()
            Panel2.Hide()
            Call StartUp()
        End If

        If cboMode.Text = "" Then
            Mode = ""
            Timer1.Start()
            Panel2.Hide()
        ElseIf cboMode.Text = "Bordered" Then
            Mode = "Bordered"
            Timer1.Start()
            Panel2.Hide()
        ElseIf cboMode.Text = "Borderless" Then
            Mode = "Borderless"
            Timer1.Start()
            Panel2.Hide()
        End If

        If txtSpeed.Text = "" Then
            speed = 50
            Timer1.Start()
            Panel2.Hide()
        Else
            speedcrawl = Val(txtSpeed.Text)
            Timer1.Start()
            Panel2.Hide()
            'Call StartUp()
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Timer1.Start()
        Panel2.Hide()
    End Sub

#End Region
End Class
