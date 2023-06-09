<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.lblId = New System.Windows.Forms.Label()
        Me.txtpsw = New System.Windows.Forms.TextBox()
        Me.btnAcceptLogin = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.BackColor = System.Drawing.Color.Transparent
        Me.lblId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblId.Location = New System.Drawing.Point(27, 34)
        Me.lblId.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(69, 16)
        Me.lblId.TabIndex = 121
        Me.lblId.Text = "Password"
        '
        'txtpsw
        '
        Me.txtpsw.BackColor = System.Drawing.SystemColors.Window
        Me.txtpsw.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtpsw.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpsw.ForeColor = System.Drawing.Color.Black
        Me.txtpsw.Location = New System.Drawing.Point(114, 30)
        Me.txtpsw.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtpsw.MaxLength = 50
        Me.txtpsw.Name = "txtpsw"
        Me.txtpsw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtpsw.Size = New System.Drawing.Size(159, 24)
        Me.txtpsw.TabIndex = 122
        Me.txtpsw.UseSystemPasswordChar = True
        '
        'btnAcceptLogin
        '
        Me.btnAcceptLogin.Location = New System.Drawing.Point(198, 94)
        Me.btnAcceptLogin.Name = "btnAcceptLogin"
        Me.btnAcceptLogin.Size = New System.Drawing.Size(75, 28)
        Me.btnAcceptLogin.TabIndex = 894
        Me.btnAcceptLogin.Text = "Accept"
        Me.btnAcceptLogin.UseVisualStyleBackColor = True
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 159)
        Me.Controls.Add(Me.btnAcceptLogin)
        Me.Controls.Add(Me.lblId)
        Me.Controls.Add(Me.txtpsw)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblId As Label
    Friend WithEvents txtpsw As TextBox
    Friend WithEvents btnAcceptLogin As Button
End Class
