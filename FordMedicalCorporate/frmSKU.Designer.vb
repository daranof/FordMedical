<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSKU
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSKU))
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.GridSKU = New Janus.Windows.GridEX.GridEX()
        Me.lblNota = New System.Windows.Forms.Label()
        Me.txtSKUAdd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNameAdd = New System.Windows.Forms.TextBox()
        Me.lblId = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.btnExit = New Janus.Windows.EditControls.UIButton()
        Me.btnDelete = New Janus.Windows.EditControls.UIButton()
        Me.btnAdd = New Janus.Windows.EditControls.UIButton()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.GridSKU, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.GridSKU)
        Me.UiGroupBox1.Location = New System.Drawing.Point(20, 125)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(417, 350)
        Me.UiGroupBox1.TabIndex = 874
        '
        'GridSKU
        '
        Me.GridSKU.Location = New System.Drawing.Point(6, 11)
        Me.GridSKU.Name = "GridSKU"
        Me.GridSKU.Size = New System.Drawing.Size(405, 333)
        Me.GridSKU.TabIndex = 887
        '
        'lblNota
        '
        Me.lblNota.AutoSize = True
        Me.lblNota.BackColor = System.Drawing.Color.Transparent
        Me.lblNota.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNota.ForeColor = System.Drawing.Color.Black
        Me.lblNota.Location = New System.Drawing.Point(17, 55)
        Me.lblNota.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.lblNota.Name = "lblNota"
        Me.lblNota.Size = New System.Drawing.Size(37, 16)
        Me.lblNota.TabIndex = 870
        Me.lblNota.Text = "SKU"
        '
        'txtSKUAdd
        '
        Me.txtSKUAdd.AcceptsTab = True
        Me.txtSKUAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtSKUAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSKUAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSKUAdd.ForeColor = System.Drawing.Color.Black
        Me.txtSKUAdd.Location = New System.Drawing.Point(82, 55)
        Me.txtSKUAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtSKUAdd.MaxLength = 100
        Me.txtSKUAdd.Name = "txtSKUAdd"
        Me.txtSKUAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSKUAdd.Size = New System.Drawing.Size(244, 24)
        Me.txtSKUAdd.TabIndex = 700
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 7.8!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(17, 91)
        Me.Label1.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 875
        Me.Label1.Text = "Name"
        '
        'txtNameAdd
        '
        Me.txtNameAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameAdd.ForeColor = System.Drawing.Color.Black
        Me.txtNameAdd.Location = New System.Drawing.Point(82, 87)
        Me.txtNameAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtNameAdd.MaxLength = 100
        Me.txtNameAdd.Name = "txtNameAdd"
        Me.txtNameAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNameAdd.Size = New System.Drawing.Size(244, 24)
        Me.txtNameAdd.TabIndex = 872
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.BackColor = System.Drawing.Color.Transparent
        Me.lblId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblId.Location = New System.Drawing.Point(17, 23)
        Me.lblId.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(19, 16)
        Me.lblId.TabIndex = 877
        Me.lblId.Text = "Id"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.SystemColors.Window
        Me.txtId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtId.Enabled = False
        Me.txtId.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(82, 19)
        Me.txtId.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtId.MaxLength = 50
        Me.txtId.Name = "txtId"
        Me.txtId.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtId.Size = New System.Drawing.Size(71, 24)
        Me.txtId.TabIndex = 878
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnExit.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnExit.Location = New System.Drawing.Point(444, 255)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.ShowFocusRectangle = False
        Me.btnExit.Size = New System.Drawing.Size(43, 39)
        Me.btnExit.TabIndex = 886
        Me.btnExit.ToolTipText = "Exit"
        Me.btnExit.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnDelete.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnDelete.Location = New System.Drawing.Point(444, 192)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.ShowFocusRectangle = False
        Me.btnDelete.Size = New System.Drawing.Size(43, 39)
        Me.btnDelete.TabIndex = 885
        Me.btnDelete.ToolTipText = "Delete"
        Me.btnDelete.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnAdd.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnAdd.Location = New System.Drawing.Point(444, 132)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.ShowFocusRectangle = False
        Me.btnAdd.Size = New System.Drawing.Size(43, 39)
        Me.btnAdd.TabIndex = 873
        Me.btnAdd.ToolTipText = "Add (or Edit)"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'frmSKU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(500, 478)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblId)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNameAdd)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.lblNota)
        Me.Controls.Add(Me.txtSKUAdd)
        Me.Name = "frmSKU"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SKU"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.GridSKU, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents GridSKU As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblNota As Label
    Friend WithEvents txtSKUAdd As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNameAdd As TextBox
    Friend WithEvents lblId As Label
    Friend WithEvents txtId As TextBox
    Friend WithEvents btnExit As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnDelete As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAdd As Janus.Windows.EditControls.UIButton
End Class
