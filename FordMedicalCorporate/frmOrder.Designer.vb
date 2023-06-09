<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrder))
        Me.GridOrder = New Janus.Windows.GridEX.GridEX()
        Me.lblNota = New System.Windows.Forms.Label()
        Me.txtOrderAdd = New System.Windows.Forms.TextBox()
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox()
        Me.lblId = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.btnDelete = New Janus.Windows.EditControls.UIButton()
        Me.btnAdd = New Janus.Windows.EditControls.UIButton()
        Me.btnExit = New Janus.Windows.EditControls.UIButton()
        CType(Me.GridOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridOrder
        '
        Me.GridOrder.Location = New System.Drawing.Point(6, 11)
        Me.GridOrder.Name = "GridOrder"
        Me.GridOrder.Size = New System.Drawing.Size(211, 314)
        Me.GridOrder.TabIndex = 868
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
        Me.lblNota.Size = New System.Drawing.Size(108, 16)
        Me.lblNota.TabIndex = 864
        Me.lblNota.Text = "Order Number"
        '
        'txtOrderAdd
        '
        Me.txtOrderAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtOrderAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOrderAdd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderAdd.ForeColor = System.Drawing.Color.Black
        Me.txtOrderAdd.Location = New System.Drawing.Point(139, 57)
        Me.txtOrderAdd.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtOrderAdd.MaxLength = 8
        Me.txtOrderAdd.Name = "txtOrderAdd"
        Me.txtOrderAdd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOrderAdd.Size = New System.Drawing.Size(174, 24)
        Me.txtOrderAdd.TabIndex = 865
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.GridOrder)
        Me.UiGroupBox1.Location = New System.Drawing.Point(18, 94)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(225, 331)
        Me.UiGroupBox1.TabIndex = 869
        '
        'lblId
        '
        Me.lblId.AutoSize = True
        Me.lblId.BackColor = System.Drawing.Color.Transparent
        Me.lblId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblId.Location = New System.Drawing.Point(15, 23)
        Me.lblId.Margin = New System.Windows.Forms.Padding(27, 25, 7, 0)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(19, 16)
        Me.lblId.TabIndex = 879
        Me.lblId.Text = "Id"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.SystemColors.Window
        Me.txtId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtId.Enabled = False
        Me.txtId.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(139, 19)
        Me.txtId.Margin = New System.Windows.Forms.Padding(11, 10, 11, 10)
        Me.txtId.MaxLength = 50
        Me.txtId.Name = "txtId"
        Me.txtId.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtId.Size = New System.Drawing.Size(71, 24)
        Me.txtId.TabIndex = 880
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnDelete.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnDelete.Location = New System.Drawing.Point(270, 160)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.ShowFocusRectangle = False
        Me.btnDelete.Size = New System.Drawing.Size(43, 39)
        Me.btnDelete.TabIndex = 882
        Me.btnDelete.ToolTipText = "Delete"
        Me.btnDelete.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnAdd.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnAdd.Location = New System.Drawing.Point(270, 100)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.ShowFocusRectangle = False
        Me.btnAdd.Size = New System.Drawing.Size(43, 39)
        Me.btnAdd.TabIndex = 881
        Me.btnAdd.ToolTipText = "Add (or Edit)"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center
        Me.btnExit.ImageSize = New System.Drawing.Size(24, 24)
        Me.btnExit.Location = New System.Drawing.Point(270, 223)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.ShowFocusRectangle = False
        Me.btnExit.Size = New System.Drawing.Size(43, 39)
        Me.btnExit.TabIndex = 883
        Me.btnExit.ToolTipText = "Exit"
        Me.btnExit.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP
        '
        'frmOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 432)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblId)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.lblNota)
        Me.Controls.Add(Me.txtOrderAdd)
        Me.Name = "frmOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order"
        CType(Me.GridOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GridOrder As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblNota As Label
    Friend WithEvents txtOrderAdd As TextBox
    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents lblId As Label
    Friend WithEvents txtId As TextBox
    Friend WithEvents btnDelete As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnAdd As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnExit As Janus.Windows.EditControls.UIButton
End Class
