<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMeasure
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMeasure))
        Me.btnExit = New Janus.Windows.EditControls.UIButton()
        Me.btnDelete = New Janus.Windows.EditControls.UIButton()
        Me.btnAdd = New Janus.Windows.EditControls.UIButton()
        Me.txtIdShipping = New System.Windows.Forms.TextBox()
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.GridPallet = New Janus.Windows.GridEX.GridEX()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtHight = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtLong = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbPallet = New System.Windows.Forms.ComboBox()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.GridPallet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnExit.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnExit.Location = New System.Drawing.Point(306, 218)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.ShowFocusRectangle = False
        Me.btnExit.Size = New System.Drawing.Size(43, 39)
        Me.btnExit.TabIndex = 907
        Me.btnExit.ToolTipText = "Exit"
        Me.btnExit.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnDelete.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnDelete.Location = New System.Drawing.Point(306, 155)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.ShowFocusRectangle = False
        Me.btnDelete.Size = New System.Drawing.Size(43, 39)
        Me.btnDelete.TabIndex = 906
        Me.btnDelete.ToolTipText = "Delete"
        Me.btnDelete.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnAdd.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnAdd.Location = New System.Drawing.Point(306, 95)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.ShowFocusRectangle = False
        Me.btnAdd.Size = New System.Drawing.Size(43, 39)
        Me.btnAdd.TabIndex = 905
        Me.btnAdd.ToolTipText = "Add ( or Edit )"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'txtIdShipping
        '
        Me.txtIdShipping.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdShipping.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdShipping.Enabled = False
        Me.txtIdShipping.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdShipping.ForeColor = System.Drawing.Color.Black
        Me.txtIdShipping.Location = New System.Drawing.Point(99, 9)
        Me.txtIdShipping.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtIdShipping.MaxLength = 50
        Me.txtIdShipping.Name = "txtIdShipping"
        Me.txtIdShipping.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtIdShipping.Size = New System.Drawing.Size(62, 24)
        Me.txtIdShipping.TabIndex = 893
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.GridPallet)
        Me.UiGroupBox1.Location = New System.Drawing.Point(363, 9)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(518, 437)
        Me.UiGroupBox1.TabIndex = 889
        '
        'GridPallet
        '
        Me.GridPallet.Location = New System.Drawing.Point(6, 11)
        Me.GridPallet.Name = "GridPallet"
        Me.GridPallet.Size = New System.Drawing.Size(506, 418)
        Me.GridPallet.TabIndex = 868
        '
        'txtWidth
        '
        Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWidth.Enabled = False
        Me.txtWidth.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWidth.ForeColor = System.Drawing.Color.Black
        Me.txtWidth.Location = New System.Drawing.Point(194, 50)
        Me.txtWidth.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtWidth.MaxLength = 100
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWidth.Size = New System.Drawing.Size(62, 24)
        Me.txtWidth.TabIndex = 899
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(174, 54)
        Me.Label19.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(21, 16)
        Me.Label19.TabIndex = 901
        Me.Label19.Text = "W"
        '
        'txtHight
        '
        Me.txtHight.BackColor = System.Drawing.SystemColors.Window
        Me.txtHight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHight.Enabled = False
        Me.txtHight.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHight.ForeColor = System.Drawing.Color.Black
        Me.txtHight.Location = New System.Drawing.Point(287, 50)
        Me.txtHight.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtHight.MaxLength = 100
        Me.txtHight.Name = "txtHight"
        Me.txtHight.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHight.Size = New System.Drawing.Size(62, 24)
        Me.txtHight.TabIndex = 900
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(267, 54)
        Me.Label17.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(17, 16)
        Me.Label17.TabIndex = 899
        Me.Label17.Text = "H"
        '
        'txtLong
        '
        Me.txtLong.BackColor = System.Drawing.SystemColors.Window
        Me.txtLong.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLong.Enabled = False
        Me.txtLong.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLong.ForeColor = System.Drawing.Color.Black
        Me.txtLong.Location = New System.Drawing.Point(99, 50)
        Me.txtLong.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtLong.MaxLength = 100
        Me.txtLong.Name = "txtLong"
        Me.txtLong.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLong.Size = New System.Drawing.Size(62, 24)
        Me.txtLong.TabIndex = 898
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(79, 54)
        Me.Label18.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(16, 16)
        Me.Label18.TabIndex = 897
        Me.Label18.Text = "L"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(17, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 16)
        Me.Label2.TabIndex = 903
        Me.Label2.Text = "Id Shipping"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(52, 145)
        Me.Label10.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 16)
        Me.Label10.TabIndex = 905
        Me.Label10.Text = "Pallet"
        '
        'cmbPallet
        '
        Me.cmbPallet.FormattingEnabled = True
        Me.cmbPallet.Location = New System.Drawing.Point(99, 142)
        Me.cmbPallet.Name = "cmbPallet"
        Me.cmbPallet.Size = New System.Drawing.Size(84, 24)
        Me.cmbPallet.TabIndex = 904
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.SystemColors.Window
        Me.txtId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtId.Enabled = False
        Me.txtId.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(194, 9)
        Me.txtId.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtId.MaxLength = 50
        Me.txtId.Name = "txtId"
        Me.txtId.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtId.Size = New System.Drawing.Size(62, 24)
        Me.txtId.TabIndex = 906
        Me.txtId.Visible = False
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWeight.Enabled = False
        Me.txtWeight.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(99, 94)
        Me.txtWeight.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtWeight.MaxLength = 100
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWeight.Size = New System.Drawing.Size(62, 24)
        Me.txtWeight.TabIndex = 901
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(43, 98)
        Me.Label1.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 16)
        Me.Label1.TabIndex = 907
        Me.Label1.Text = "Weight"
        '
        'frmMeasure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(925, 450)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmbPallet)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtWidth)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtHight)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtLong)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtIdShipping)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Name = "frmMeasure"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Measures"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.GridPallet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnDelete As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAdd As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtIdShipping As TextBox
    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents GridPallet As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtWidth As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtHight As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtLong As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbPallet As ComboBox
    Friend WithEvents txtId As TextBox
    Friend WithEvents txtWeight As TextBox
    Friend WithEvents Label1 As Label
End Class
