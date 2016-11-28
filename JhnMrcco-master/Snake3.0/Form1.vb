Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackgroundImage = (My.Resources.Main)
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click

        Me.Hide()
        FrmMAin.Show()
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Determines what keys are pressed and what to do when each one is pressed

        Select Case e.KeyCode
            Case Keys.Enter
                Call enterclick()
            Case Keys.Escape
                End
        End Select
    End Sub

    Private Sub enterclick()
        FrmMAin.Show()
        Me.Hide()
    End Sub

End Class