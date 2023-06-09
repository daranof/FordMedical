Public Class Login
    Dim Access As Boolean

    Private Sub btnAcceptLogin_Click(sender As Object, e As EventArgs) Handles btnAcceptLogin.Click
        If txtpsw.Text = "860403" Then
            Access = True
        Else
            Access = False
        End If
        frmFordM.AccessL = Access
        Me.Close()
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtpsw.Clear()

    End Sub
    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'frmFordM.btnCancel.PerformClick()
        'Dim frmFordM As New frmFordM()
        frmFordM.Enabled = True
        frmFordM.MdiParent = Me.MdiParent
        frmFordM.StartPosition = FormStartPosition.CenterScreen
        frmFordM.Show()

    End Sub
End Class