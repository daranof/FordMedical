<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomer))
        Me.lblId = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAddressAdd = New System.Windows.Forms.TextBox()
        Me.lblNota = New System.Windows.Forms.Label()
        Me.txtNameAdd = New System.Windows.Forms.TextBox()
        Me.txtZipAdd = New System.Windows.Forms.TextBox()
        Me.txtStateAdd = New System.Windows.Forms.TextBox()
        Me.txtCountryAdd = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.GridCustomers = New Janus.Windows.GridEX.GridEX()
        Me.btnExit = New Janus.Windows.EditControls.UIButton()
        Me.btnDelete = New Janus.Windows.EditControls.UIButton()
        Me.btnAdd = New Janus.Windows.EditControls.UIButton()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.GridCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.BackColor = System.Drawing.Color.Transparent
        Me.lblId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblId.Location = New System.Drawing.Point(15, 25)
        Me.lblId.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(19, 16)
        Me.lblId.TabIndex = 883
        Me.lblId.Text = "Id"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.SystemColors.Window
        Me.txtId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtId.Enabled = False
        Me.txtId.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(90, 21)
        Me.txtId.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtId.MaxLength = 50
        Me.txtId.Name = "txtId"
        Me.txtId.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtId.Size = New System.Drawing.Size(71, 24)
        Me.txtId.TabIndex = 884
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 7.8!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(15, 97)
        Me.Label1.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 16)
        Me.Label1.TabIndex = 881
        Me.Label1.Text = "Address"
        '
        'txtAddressAdd
        '
        Me.txtAddressAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddressAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddressAdd.ForeColor = System.Drawing.Color.Black
        Me.txtAddressAdd.Location = New System.Drawing.Point(90, 93)
        Me.txtAddressAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtAddressAdd.MaxLength = 100
        Me.txtAddressAdd.Name = "txtAddressAdd"
        Me.txtAddressAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddressAdd.Size = New System.Drawing.Size(190, 24)
        Me.txtAddressAdd.TabIndex = 882
        '
        'lblNota
        '
        Me.lblNota.AutoSize = True
        Me.lblNota.BackColor = System.Drawing.Color.Transparent
        Me.lblNota.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNota.ForeColor = System.Drawing.Color.Black
        Me.lblNota.Location = New System.Drawing.Point(15, 61)
        Me.lblNota.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.lblNota.Name = "lblNota"
        Me.lblNota.Size = New System.Drawing.Size(47, 16)
        Me.lblNota.TabIndex = 879
        Me.lblNota.Text = "Name"
        '
        'txtNameAdd
        '
        Me.txtNameAdd.AcceptsTab = True
        Me.txtNameAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNameAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameAdd.ForeColor = System.Drawing.Color.Black
        Me.txtNameAdd.Location = New System.Drawing.Point(90, 57)
        Me.txtNameAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtNameAdd.MaxLength = 100
        Me.txtNameAdd.Name = "txtNameAdd"
        Me.txtNameAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNameAdd.Size = New System.Drawing.Size(190, 24)
        Me.txtNameAdd.TabIndex = 880
        '
        'txtZipAdd
        '
        Me.txtZipAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtZipAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZipAdd.ForeColor = System.Drawing.Color.Black
        Me.txtZipAdd.Location = New System.Drawing.Point(90, 129)
        Me.txtZipAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtZipAdd.MaxLength = 100
        Me.txtZipAdd.Name = "txtZipAdd"
        Me.txtZipAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtZipAdd.Size = New System.Drawing.Size(117, 24)
        Me.txtZipAdd.TabIndex = 885
        '
        'txtStateAdd
        '
        Me.txtStateAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtStateAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateAdd.ForeColor = System.Drawing.Color.Black
        Me.txtStateAdd.Location = New System.Drawing.Point(90, 163)
        Me.txtStateAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtStateAdd.MaxLength = 100
        Me.txtStateAdd.Name = "txtStateAdd"
        Me.txtStateAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtStateAdd.Size = New System.Drawing.Size(117, 24)
        Me.txtStateAdd.TabIndex = 886
        '
        'txtCountryAdd
        '
        Me.txtCountryAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountryAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountryAdd.ForeColor = System.Drawing.Color.Black
        Me.txtCountryAdd.Location = New System.Drawing.Point(90, 197)
        Me.txtCountryAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtCountryAdd.MaxLength = 100
        Me.txtCountryAdd.Name = "txtCountryAdd"
        Me.txtCountryAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCountryAdd.Size = New System.Drawing.Size(117, 24)
        Me.txtCountryAdd.TabIndex = 887
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 7.8!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(15, 133)
        Me.Label2.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 16)
        Me.Label2.TabIndex = 888
        Me.Label2.Text = "Zip"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 7.8!)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(15, 167)
        Me.Label3.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 16)
        Me.Label3.TabIndex = 889
        Me.Label3.Text = "State"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 7.8!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(15, 201)
        Me.Label4.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 890
        Me.Label4.Text = "Country"
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.GridCustomers)
        Me.UiGroupBox1.Location = New System.Drawing.Point(294, 12)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(204, 350)
        Me.UiGroupBox1.TabIndex = 891
        '
        'GridCustomers
        '
        Me.GridCustomers.Location = New System.Drawing.Point(6, 11)
        Me.GridCustomers.Name = "GridCustomers"
        Me.GridCustomers.Size = New System.Drawing.Size(191, 333)
        Me.GridCustomers.TabIndex = 868
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnExit.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnExit.Location = New System.Drawing.Point(518, 144)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.ShowFocusRectangle = False
        Me.btnExit.Size = New System.Drawing.Size(43, 39)
        Me.btnExit.TabIndex = 894
        Me.btnExit.ToolTipText = "Exit"
        Me.btnExit.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnDelete.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnDelete.Location = New System.Drawing.Point(518, 81)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.ShowFocusRectangle = False
        Me.btnDelete.Size = New System.Drawing.Size(43, 39)
        Me.btnDelete.TabIndex = 893
        Me.btnDelete.ToolTipText = "Delete"
        Me.btnDelete.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnAdd.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnAdd.Location = New System.Drawing.Point(518, 21)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.ShowFocusRectangle = False
        Me.btnAdd.Size = New System.Drawing.Size(43, 39)
        Me.btnAdd.TabIndex = 892
        Me.btnAdd.ToolTipText = "Add ( or Edit )"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'frmCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 382)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCountryAdd)
        Me.Controls.Add(Me.txtStateAdd)
        Me.Controls.Add(Me.txtZipAdd)
        Me.Controls.Add(Me.lblId)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAddressAdd)
        Me.Controls.Add(Me.lblNota)
        Me.Controls.Add(Me.txtNameAdd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.GridCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblId As Label
    Friend WithEvents txtId As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAddressAdd As TextBox
    Friend WithEvents lblNota As Label
    Friend WithEvents txtNameAdd As TextBox
    Friend WithEvents txtZipAdd As TextBox
    Friend WithEvents txtStateAdd As TextBox
    Friend WithEvents txtCountryAdd As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents GridCustomers As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnExit As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnDelete As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAdd As Janus.Windows.EditControls.UIButton
End Class
