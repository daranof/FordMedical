Imports System.Data.SqlClient
Imports Janus.Windows.GridEX
Imports siCom40
Imports siCom40.Com
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Net.Mail


Public Class frmFordM
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Dim Remaining As Integer = 0
    Dim PartialRemaining As Integer = 0
    Dim TotalOnOrders As Integer = 0
    Dim TotalOnShipment As Integer = 0
    Dim PartialOnPallet As Integer = 0
    Dim PartialQty As Integer = 0
    Dim ShippedDate As Date
    Dim PalletQty As Integer = 0
    Dim ordertemp As Integer


    Dim connectionString As String = "Data Source=localhost;Initial Catalog=FordMedical;User ID=sa;Password=Savent01$"
    Dim conn As New SqlConnection(connectionString)

    Dim IsAccept As Boolean = False
    Dim action As String = ESTR
    Dim DTable As New DataTable

    Dim WithEvents child As frmOrder
    'Dim apiClient As New SellerCloud.ApiClient("OR33shippingstation1@fordmed.com", "4@W5u!wNUq8IQ1A")

    Public AccessL As Boolean
    Public Property VAccessL As Boolean
        Get

            Return AccessL
        End Get
        Set(value As Boolean)
            AccessL = value
        End Set
    End Property

    Private Sub FordM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''Crear una instancia del formulario adicional
        'Dim frmLogin As New Login()

        ''Mostrar el formulario adicional y esperar hasta que se cierre
        'frmLogin.ShowDialog()

        ''Mostrar el formulario principal
        'Me.Show()

        Try
            Dim r As Resizer = New Resizer(Me)
            btnCancel.PerformClick()
        Catch ex As Exception
            ErrHandler(ex)
        End Try
        HyperV.Text = "Visit SellerCloud"
        HyperV.LinkBehavior = LinkBehavior.AlwaysUnderline
        HyperV.LinkColor = Color.Blue
        HyperV.ActiveLinkColor = Color.Red
        HyperV.VisitedLinkColor = Color.Green
        HyperV.LinkArea = New LinkArea(0, HyperV.Text.Length)
        HyperV.Enabled = False

    End Sub

    Private Sub EnableControls(ByVal Value As Boolean)

        txtId.Enabled = False
        cmbCustomer.Enabled = Value

        If (Value = False Or cmbCustomer.SelectedValue <> -1) Then
            txtOrder.Enabled = Value
        End If
        If (Value = False And txtOrder.Tag = -1) Then
            txtSKU.Enabled = Value
            GridInvoice.Enabled = Value
        End If
        If (Value = False And txtSKU.Tag = -1) Then
            txtTotalQty.Enabled = Value
        End If
        If (Value = False And txtSKU.Tag = -1) Then
            btnAddInvoice.Enabled = Value
        End If
        If (Value = False And txtSKUPallet.Text = "") Then
            txtQty.Enabled = False
            cmbPallet.Enabled = False
            btnAddPallet.Enabled = False
        End If

        dtpShippedDate.Enabled = False
        btnsend.Enabled = False
        txtBOL.Enabled = Value
        chkShipped.Enabled = Value

        'chkMeasure.Enabled = Value
        'If (Value = True) Then
        '    If chkMeasure.Checked = True Then
        '        btnAddMeasure.Enabled = Value
        '    Else
        '        btnAddMeasure.Enabled = False
        '    End If

        'Else
        '    btnAddMeasure.Enabled = Value
        'End If

        txtShippingLot.Enabled = False
        txtBOL.Enabled = Value
        btnAddMeasure.Enabled = Value
        btnAddOrder.Enabled = Value
        btnAddCustomer.Enabled = Value
        txtSKUFinder.Enabled = True
    End Sub

    Private Sub InitControls()
        txtId.Text = "0"
        cmbCustomer.SelectedValue = -1
        cmbCustomer.Text = ""
        cmbCustomer2.SelectedValue = -1
        cmbCustomer2.Text = ""
        txtOrder.Tag = -1
        txtSKU.Tag = -1
        'chkMeasure.Checked = False
        chkShipped.Checked = False
        txtShippingLot.Clear()

        txtTotalQty.Text = ""
        txtQty.Text = ""

        dtpShippedDate.Checked = False
        dtpShippedDate.Text = ESTR
        txtBOL.Text = ""

    End Sub

    Private Sub ToolbarButtons()
        If AccessL = True Then

            Select Case action
                Case ESTR
                    btnAdd.Enabled = True
                    Select Case GridMain.RowCount
                        Case 0
                            btnEdit.Enabled = False
                            btnDelete.Enabled = False
                            btnSave.Enabled = False
                            btnSave.Text = ESTR
                           ' btnPickForm.Enabled = False

                        Case Is > 0
                            btnEdit.Enabled = True
                            btnSave.Enabled = False
                            'btnSave.Text = ESTR
                            ' btnPickForm.Enabled = False

                    End Select
                Case Else
                    btnAdd.Enabled = False
                    btnEdit.Enabled = False
                    btnDelete.Enabled = False
                    btnSave.Enabled = True
                    ' btnPickForm.Enabled = False

            End Select
        Else
            btnAdd.Enabled = False
            btnEdit.Enabled = False
            btnDelete.Enabled = False
            btnSave.Enabled = False
        End If

        btnReport.Enabled = False
        btnPickForm.Enabled = False
        btnExportGrid.Enabled = False
    End Sub

    Private Function ValidateFields() As Boolean
        If cmbCustomer.SelectedValue = Nothing Then Mb("You must specify the client.") : cmbCustomer.Focus() : Return False
        Return True

    End Function

    Public Sub dbSaveStatus(ByVal mode As String)
        Dim CONN = New SqlConnection(connectionString)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try
            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction
            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandText = "usp_Shipping"
            CMD.Parameters.Clear()
            CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = action
            CMD.Parameters.Add("@ShippingId", SqlDbType.Int).Value = CInt2(txtId.Text)
            CMD.Parameters.Add("@CustomerId", SqlDbType.Int).Value = CInt2(cmbCustomer.SelectedValue)
            CMD.Parameters.Add("@Shipped", SqlDbType.Bit).Value = CBool2(chkShipped.Checked)
            'CMD.Parameters.Add("@Measures", SqlDbType.Bit).Value = CBool2(chkMeasure.Checked)
            CMD.Parameters.Add("@ShippingLot", SqlDbType.Int).Value = CInt2(txtShippingLot.Text)
            CMD.Parameters.Add("@BOL", SqlDbType.NVarChar, 50).Value = CStr2(txtBOL.Text)
            If mode = dbINSERT Then
                CMD.Parameters.Add("@Date", SqlDbType.Date).Value = Now
            Else
                CMD.Parameters.Add("@Date", SqlDbType.Date).Value = GridMain.GetValue("Date")
            End If

            If chkShipped.Checked = True Then
                CMD.Parameters.Add("@ShippedDate", SqlDbType.Date).Value = Format(CType(dtpShippedDate.Value, Date), "s")
            Else
                CMD.Parameters.Add("@ShippedDate", SqlDbType.Date).Value = DATE1900
            End If

            CMD.ExecuteNonQuery()

            Dim Identity As Integer
            If mode = "INSERT" Then
                CMD.CommandType = CommandType.Text
                CMD.CommandText = "SELECT @@IDENTITY FROM Shipping"
                Identity = CInt2(CMD.ExecuteScalar())
                txtId.Text = Identity
            Else
                Identity = CInt2(txtId.Text)
            End If

            CMD.CommandType = CommandType.Text
            CMD.CommandText = "update Orders
                               set CustomerId = " & CInt2(cmbCustomer.SelectedValue) & "
                               where CustomerId = " & CInt2(GridMain.GetValue("CustomerId")) & "and ShippingId=" & CInt2(txtId.Text)

            CMD.ExecuteNonQuery()

            CMD.CommandType = CommandType.Text
            CMD.CommandText = "update Orders
                               set ShippingId = " & Identity & "
                               where ShippingId=0"

            CMD.ExecuteNonQuery()

            CMD.CommandType = CommandType.Text
            CMD.CommandText = "update Palletizer
                               set ShippingId = " & Identity & "
                               where ShippingId=0"

            CMD.ExecuteNonQuery()

            CMD.CommandType = CommandType.Text
            CMD.CommandText = "update Invoice
                               set ShippingId = " & Identity & "
                               where ShippingId=0"

            CMD.ExecuteNonQuery()

            CMD.CommandType = CommandType.Text
            CMD.CommandText = "update Measure
                               set IdShipping = " & Identity & "
                               where IdShipping=0"

            CMD.ExecuteNonQuery()

            transaction.Commit()
            Mb("Succeed", 1)
            action = ESTR
        Catch ex As Exception
            transaction.Rollback()
            ErrHandler(ex)
        Finally
            CONN.Close()
            CONN.Dispose()
            CMD.Dispose()
            transaction.Dispose()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateFields() = True Then
            dbSaveStatus(action)
            btnCancel.PerformClick()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        action = dbINSERT
        EnableControls(True)
        ToolbarButtons()
        UiTab1.SelectedIndex = 1
        cmbCustomer.SelectedValue = -1
        txtOrder.Tag = -1
        txtSKU.Tag = -1
        txtId.Text = 0
        txtShippingLot.Text = ""
        txtTotalQty.Text = ""
        txtQty.Text = ""
        txtBOL.Text = ""

        '  chkMeasure.Checked = False
        chkShipped.Checked = False

        With GridMain
            .EditMode = EditMode.EditOn
            .RootTable.AllowAddNew = InheritableBoolean.False
            .RootTable.AllowEdit = InheritableBoolean.True
            .RootTable.AllowDelete = InheritableBoolean.True
        End With

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        If GridMain.CurrentRow IsNot Nothing AndAlso GridMain.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                action = dbUPDATE
                EnableControls(True)
                ToolbarButtons()
                If UiTab1.SelectedIndex = 0 Then UiTab1.SelectedIndex = 1
                TabMain.Enabled = False
                txtOrder.Tag = -1

                If txtOrder.Tag <> -1 Then
                    txtSKU.Enabled = True
                    btnAddSKU.Enabled = True
                    txtSKU.Enabled = True
                End If

                If chkShipped.Checked = True Then
                    dtpShippedDate.Enabled = True
                    txtBOL.Enabled = True
                    btnsend.Enabled = True
                End If

                With GridMain
                    .EditMode = EditMode.EditOn
                    .RootTable.AllowAddNew = InheritableBoolean.False
                    .RootTable.AllowEdit = InheritableBoolean.True
                    .RootTable.AllowDelete = InheritableBoolean.True
                End With

            End If

    End Sub

    Private Sub GridMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridMain.Click, GridMain.KeyDown, GridMain.KeyUp
        If action = ESTR Then GetData()
        EnableControls(False)

        btnPickForm.Enabled = True
        If AccessL = True Then
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        End If

    End Sub
    Private Sub GridMain_DoubleClick(sender As Object, e As EventArgs) Handles GridMain.DoubleClick
        If GridMain.CurrentRow IsNot Nothing AndAlso GridMain.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            UiTab1.SelectedIndex = 1
            EnableControls(False)
            If AccessL = True Then
                btnEdit.Enabled = True
            End If
            btnPickForm.Enabled = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If action <> ESTR Then
            Select Case MessageBox.Show("¿Do you want to save before exit?", AppTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                Case Windows.Forms.DialogResult.Yes
                    btnSave.PerformClick()
                Case Windows.Forms.DialogResult.No
                    'Do nothing
                Case Windows.Forms.DialogResult.Cancel
                    Exit Sub
            End Select
        End If
        UiTab1.SelectedIndex = 0
        TabMain.Enabled = True
        action = ESTR
        InitControls()
        EnableControls(False)

        FillGridMain()
        fillGridInvoice()
        fillGridPalletizer()
        FillGridCustomer()

        FillcmbCustomer()
        FillcmbCustomer2()
        FillcmbPallet()

        ToolbarButtons()

        txtSKUFinder.Tag = -1
        txtSKUFinder.Text = ""
        fillGridSKUFinder()

        txtShippingLot.Enabled = False
        btnDelete.Enabled = False

        If GridMain.RootTable.Groups.Count > 0 Then GridMain.RootTable.Groups.Clear() 'Clear Groups in Grid

    End Sub

    Private Sub GetData()
        If GridMain.CurrentRow IsNot Nothing AndAlso GridMain.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            txtId.Text = CStr2(GridMain.GetValue("ShippingId"))
            cmbCustomer.Text = CStr2(GridMain.GetValue("Customer"))
            txtShippingLot.Text = CStr2(GridMain.GetValue("ShipMent"))
            chkShipped.Checked = CBool2(GridMain.GetValue("Shipped"))
            ' chkMeasure.Checked = CBool2(GridMain.GetValue("Measures"))
            dtpShippedDate.Text = CStr2(GridMain.GetValue("ShippedDate"))
            txtBOL.Text = CStr2(GridMain.GetValue("BOL"))
            Select Case GridMain.GetValue("ShippedDate").ToString.Trim
                Case ESTR
                    dtpShippedDate.Checked = False
                    dtpShippedDate.Text = ESTR
                Case Else
                    dtpShippedDate.Checked = True
                    dtpShippedDate.Text = CStr2(GridMain.GetValue("ShippedDate"))
            End Select
        End If
    End Sub

    Public Sub FillGridMain()

        Try

            Dim query As String = qFillGridMain

            Dim table As DataTable = DbFillDataTable(query)

            Dim sig As siGrid18 = New siGrid18(GridMain, table)

            'grid Properties(Override the Layout settings)...
            With GridMain
                With .RootTable ' RootTable.
                    With .Columns("ShippingId")
                        .Visible = False
                    End With
                    With .Columns("CustomerId")
                        .Visible = False
                    End With
                    With .Columns("Measures")
                        .Visible = False
                    End With
                    With .Columns("BOL")
                        .Visible = False
                    End With
                    With .Columns("Customer")
                        .HeaderStyle.BackColor = Color.LemonChiffon
                    End With
                    With .Columns("Date")
                        .EditType = EditType.CalendarCombo
                        .FormatString = "MM/dd/yyyy"
                    End With
                    With .Columns("ShippedDate")
                        .EditType = EditType.CalendarCombo
                        .FormatString = "MM/dd/yyyy"
                    End With
                End With
                GridMain.AutoSizeColumns()
            End With

        Catch ex As Exception
            ErrHandler(ex)
        End Try
    End Sub

    Private Sub FillGridCustomer()
        Dim query As String
        Dim querytemp As String = "Select distinct cast (PalletNumber as varchar(300)) as Pallet from Pallet as t
                                   Left Join Palletizer as z on t.Id=z.PalletId
                                   Left Join Shipping as s on z.ShippingId=s.ShippingId
                                   WHERE s.ShippingLot = @ShippingLot And s.CustomerId = @CustomerId"
        querytemp = querytemp.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))
        Dim tabletemp As DataTable = DbFillDataTable(querytemp)

        Dim weight As String = qTotalWeight
        weight = weight.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))
        Dim TotalWeight As Integer = CInt2(DbExecuteScalar(weight))

        lblTotalWeight.Text = TotalWeight
        Try
            If tabletemp.Rows.Count = 0 Then
                query = qFillCustomer1
            Else
                query = qFillCustomer2
                query = query.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))
            End If

            Dim table As DataTable = DbFillDataTable(query)

            Dim sig As siGrid18 = New siGrid18(GridCustomer, table)


        Catch ex As Exception
            ErrHandler(ex)
        End Try
    End Sub

    Private Sub deleteDrFromGR(Grid As GridEX)
        Dim dt As DataTable = Grid.DataSource
        If dt Is Nothing Then
            Exit Sub
        End If

        DeleteDtRow(dt, Grid.CurrentRow.RowIndex)
        GridInvoice.Refresh()
        dbSaveInvoice(action)
    End Sub
    Private Sub deleteDrFromGRPallet(Grid As GridEX)
        Dim dt As DataTable = Grid.DataSource
        If dt Is Nothing Then
            Exit Sub
        End If

        DeleteDtRow(dt, Grid.CurrentRow.RowIndex)
        GridPalletizer.Refresh()
        dbSavePalletizer()
    End Sub

    Private Sub btnAddOrder_Click(sender As Object, e As EventArgs) Handles btnAddOrder.Click
        Me.Enabled = False
        frmOrder.MdiParent = Me.MdiParent
        frmOrder.StartPosition = FormStartPosition.CenterScreen
        frmOrder.Show()

    End Sub

    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        Me.Enabled = False
        frmCustomer.MdiParent = Me.MdiParent
        frmCustomer.StartPosition = FormStartPosition.CenterScreen
        frmCustomer.Show()

    End Sub

    Private Sub txtSKU_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSKU.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then

                Dim query As String = qFillSKU

                Dim filter As String =
                <SQL><![CDATA[
					A.SKU LIKE '%@TTF%'
					ORDER BY A.Id
				]]></SQL>.Value.Replace("@TTF", txtSKU.Text.Trim)

                Using f As frmFind13 = New frmFind13
                    With f
                        .Query = query
                        .Filter = filter
                        .SearchCol = "SKU"
                        .UseSelectorCol = True
                        .ShowDialog()

                        If sender.name.toupper = "TXTSKU" Then
                            If .Found Then
                                txtSKU.Text = CStr2(.DTSel.Rows(0).Item("SKU"))
                                txtSKU.Tag = CInt2(.DTSel.Rows(0).Item("Id"))
                            Else
                                txtSKU.Clear()
                                txtSKU.Tag = -1
                            End If
                        End If

                    End With
                End Using
                txtTotalQty.Focus()
            End If
        Catch ex As Exception
            ErrHandler(ex)
        End Try

    End Sub

    Private Sub btnAddInvoice_Click(sender As Object, e As EventArgs) Handles btnAddInvoice.Click
        If ValidateFieldsInvoice() = True Then

            GridInvoice.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True

            Dim dt As DataTable = GridInvoice.DataSource
            Dim dr As DataRow = dt.NewRow
            With dr
                dr("SKU") = CStr2(txtSKU.Text)
                dr("TotalQty") = CInt2(txtTotalQty.Text)
                dr("OrderId") = CInt2(txtOrder.Tag)
                dr("SKUId") = CInt2(txtSKU.Tag)
            End With

            dt.Rows.Add(dr)

            dt.AcceptChanges()

            txtSKU.Text = ""
            txtSKU.Tag = -1
            txtTotalQty.Text = ""

            With GridInvoice
                With .RootTable ' RootTable.
                    With .Columns("SKU")
                        .AutoSize()
                    End With
                End With
            End With

            GridInvoice.Refresh()
            dbSaveInvoice(action)
        End If

        Colored()
        txtSKU.Focus()
        btnImport.Enabled = False
        GridInvoice.EditMode = EditMode.EditOn
        GridInvoice.RootTable.AllowAddNew = InheritableBoolean.False
        GridInvoice.RootTable.AllowEdit = InheritableBoolean.True
        GridInvoice.RootTable.AllowDelete = InheritableBoolean.True
    End Sub

    Private Sub fillGridInvoice()

        Dim query As String = "SELECT s.SKU, i.TotalQty, i.OrderId, i.SKUId
                                       From Invoice as i	
                                       Inner Join SKU as s on i.SKUId=s.Id
                                       Where i.OrderId=@OrderId
                                       ORDER BY s.Id"
        query = query.Replace("@OrderId", CInt2(txtOrder.Tag))

        Dim command As New SqlCommand(query, conn)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        Dim sig As New siGrid18(GridInvoice, table)

        With GridInvoice
            With .RootTable ' RootTable.
                With .Columns("OrderId")
                    .Visible = False
                End With
                With .Columns("SKUId")
                    .Visible = False
                End With
                With .Columns("SKU")
                    .AutoSize()
                End With
            End With
        End With

        GridInvoice.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True

        If GridInvoice.RowCount > 0 Or txtOrder.Tag = -1 Then
            btnImport.Enabled = False
        Else
            btnImport.Enabled = True
        End If

        Colored()

    End Sub

    Public Sub FillcmbCustomer()
        Dim dtCmbCustomer As New DataTable

        Try
            Dim query As String = qCmbCustomer

            dtCmbCustomer = DbFillDataTable(query)

            With cmbCustomer
                .DataSource = dtCmbCustomer
                .DisplayMember = "Name"
                .ValueMember = "Id"

            End With
        Catch ex As Exception
            ErrHandler(ex)
        End Try
        cmbCustomer.SelectedValue = -1
        cmbCustomer.Text = ""
    End Sub

    Public Sub FillcmbCustomer2()
        Dim dtCmbCustomer2 As New DataTable

        Try
            Dim query As String = qCmbCustomer

            dtCmbCustomer2 = DbFillDataTable(query)

            With cmbCustomer2
                .DataSource = dtCmbCustomer2
                .DisplayMember = "Name"
                .ValueMember = "Id"

            End With
        Catch ex As Exception
            ErrHandler(ex)
        End Try
        cmbCustomer2.SelectedValue = -1
        cmbCustomer2.Text = ""

    End Sub

    Public Sub FillcmbShippingLot()
        Dim dtCmbShippingLot As New DataTable

        Try
            Dim query As String = qCmbShippingLot
            query = query.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue))

            dtCmbShippingLot = DbFillDataTable(query)

            With cmbShippingLot
                .DataSource = dtCmbShippingLot
                .DisplayMember = "ShippingLot"
                .ValueMember = "ShippingId"
            End With

        Catch ex As Exception
            ErrHandler(ex)
        End Try
        cmbShippingLot.SelectedValue = -1
        cmbShippingLot.Text = ""

    End Sub
    Public Sub FillcmbPallet()
        Dim dtCmbPallet As New DataTable

        Try
            Dim query As String = qCmbPallet

            dtCmbPallet = DbFillDataTable(query)

            With cmbPallet
                .DataSource = dtCmbPallet
                .DisplayMember = "PalletNumber"
                .ValueMember = "Id"

            End With
        Catch ex As Exception
            ErrHandler(ex)
        End Try
        cmbPallet.SelectedValue = -1
        cmbPallet.Text = ""

    End Sub

    Private Sub btnAddSKU_Click(sender As Object, e As EventArgs) Handles btnAddSKU.Click
        Me.Enabled = False
        frmSKU.MdiParent = Me.MdiParent
        frmSKU.StartPosition = FormStartPosition.CenterScreen
        frmSKU.Show()
        frmSKU.txtSKUAdd.Focus()
    End Sub

    Public Sub dbSaveInvoice(ByVal mode As String)
        Dim CONN = New SqlConnection(connectionString)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try
            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction

            CMD.CommandText = "DELETE FROM Invoice where OrderId=" & txtOrder.Tag
            CMD.CommandType = CommandType.Text
            CMD.Parameters.Clear()
            CMD.ExecuteNonQuery()

            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandText = "usp_Invoice"
            For Each grx As GridEXRow In GridInvoice.GetDataRows
                CMD.Parameters.Clear()
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbINSERT
                CMD.Parameters.Add("@OrderId", SqlDbType.Int).Value = CInt2(grx.Cells("OrderId").Value)
                CMD.Parameters.Add("@SKUId", SqlDbType.Int).Value = CInt2(grx.Cells("SKUId").Value)
                CMD.Parameters.Add("@TotalQty", SqlDbType.Int).Value = CInt2(grx.Cells("TotalQty").Value)
                CMD.Parameters.Add("@ShippingId", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.ExecuteNonQuery()
            Next

            transaction.Commit()

        Catch ex As Exception
            transaction.Rollback()
            ErrHandler(ex)
        Finally
            CONN.Close()
            CONN.Dispose()
            CMD.Dispose()
            transaction.Dispose()
        End Try
    End Sub

    Private Sub txtOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOrder.KeyDown
        If e.KeyCode <> Keys.Enter Then
            Exit Sub
        End If
        Try
            ' Query.
            Dim query As String = qCmbOrders

            query = query.Replace("@ShippingId", CInt2(txtId.Text)).Replace("@CustomerId", CInt2(cmbCustomer.SelectedValue))

            Dim filter As String =
                <SQL><![CDATA[
					o.OrderNumber LIKE '%@TTF%'
					ORDER BY o.Id
				]]></SQL>.Value.Replace("@TTF", txtOrder.Text.Trim)

            ' Find the text in the DB.
            Using f As frmFind13 = New frmFind13
                With f
                    .Query = query
                    .Filter = filter
                    .SearchCol = "OrderNumber"
                    .ShowDialog()

                    If .Found Then
                        txtOrder.Tag = CInt2(.DTSel.Rows(0)("Id"))
                        txtOrder.Text = CStr2(.DTSel.Rows(0)("OrderNumber"))
                    Else
                        txtOrder.Clear()
                        txtOrder.Tag = -1
                    End If

                End With
            End Using
            txtSKU.Focus()

        Catch ex As Exception
            ErrHandler(ex)
        End Try
    End Sub

    Private Sub txtOrder_TextChanged(sender As Object, e As EventArgs) Handles txtOrder.TextChanged
        'If CStr2(txtOrder.Text) <> "" Then
        If txtOrder.TextLength = 8 Then
            If CStr2(txtOrder.Tag) <> -1 Then
                txtSKU.Enabled = True
                btnAddSKU.Enabled = True
                fillGridInvoice()
                fillGridPalletizer()
                txtOrder.Focus()

                HyperV.Enabled = True
            End If
        Else
            If txtOrder.TextLength <= 7 Then
                txtSKU.Enabled = False
                txtSKU.Tag = -1
                txtSKU.Text = ""
                txtOrder.Tag = -1
                txtTotalQty.Enabled = False
                txtTotalQty.Text = ""
                btnAddSKU.Enabled = False
                txtSKUPallet.Enabled = False
                txtSKUPallet.Text = ""
                txtSKUPallet.Tag = -1
                txtQty.Enabled = False
                txtQty.Text = ""
                cmbPallet.Enabled = False
                cmbPallet.SelectedValue = -1
                btnAddPallet.Enabled = False
                txtQty.Text = ""
                txtQty.Enabled = False
                fillGridInvoice()
                fillGridPalletizer()
                txtOrder.Focus()
                HyperV.Enabled = False
            End If

        End If

    End Sub

    Private Sub txtSKU_TextChanged(sender As Object, e As EventArgs) Handles txtSKU.TextChanged
        If txtSKU.Text <> "" Then
            txtTotalQty.Enabled = True
            btnAddInvoice.Enabled = True
        Else
            txtTotalQty.Enabled = False
            btnAddInvoice.Enabled = False
        End If

    End Sub

    Private Sub cmbCustomer_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.SelectedValueChanged
        txtOrder.Clear()
        txtOrder.Tag = -1
        Dim ShipLot As Integer = CInt2(DbExecuteScalar("select max (shippinglot) from shipping where CustomerId=" & CInt2(cmbCustomer.SelectedValue)))
        txtShippingLot.Text = ShipLot + 1
    End Sub

    Private Sub GetDataInvoice()
        If GridInvoice.CurrentRow IsNot Nothing AndAlso GridInvoice.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            txtSKUPallet.Text = CStr2(GridInvoice.GetValue("SKU"))
            txtSKUPallet.Tag = CInt2(GridInvoice.GetValue("SKUId"))
            fillGridPalletizer()
        End If
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        If CInt2(txtQty.Text) >= 1 Then
            cmbPallet.Enabled = True
        Else
            cmbPallet.Enabled = False
        End If
    End Sub

    Private Sub cmbPallet_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPallet.SelectedValueChanged
        If CStr2(cmbPallet.Text) <> "" Then
            btnAddPallet.Enabled = True
        Else
            btnAddPallet.Enabled = False
        End If
    End Sub

    Private Sub btnAddPallet_Click(sender As Object, e As EventArgs) Handles btnAddPallet.Click


        If ValidateFieldsQty() = True Then

            With GridPalletizer
                With .RootTable ' RootTable.
                    'With .Columns("User")
                    '    .Visible = False
                    'End With
                End With
            End With

            GridPalletizer.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True

            dbSavePalletizer()

            txtQty.Clear()
            cmbPallet.SelectedValue = -1
            cmbPallet.Text = ""
            ordertemp = 0
            PartialQty = 0
            GridPalletizer.Refresh()

            UpdateQTY()
            fillGridPalletizer()

            Colored()

            GridPalletizer.EditMode = EditMode.EditOn
            GridPalletizer.RootTable.AllowAddNew = InheritableBoolean.False
            GridPalletizer.RootTable.AllowEdit = InheritableBoolean.True
            GridPalletizer.RootTable.AllowDelete = InheritableBoolean.True

            txtQty.Focus()
        End If
    End Sub

    Private Sub fillGridPalletizer()

        Dim query As String = qFillGridPalletizer
        Dim a As Integer = CInt2(GridInvoice.GetValue("SKUId"))
        Dim b As Integer = CInt2(txtId.Text)
        query = query.Replace("@SKUId", CInt2(GridInvoice.GetValue("SKUId"))).Replace("@ShippingId", CInt2(txtId.Text))

        Dim command As New SqlCommand(query, conn)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        Dim sig As New siGrid18(GridPalletizer, table)

        With GridPalletizer
            With .RootTable ' RootTable.
                With .Columns("Id")
                    .Visible = False
                End With
                With .Columns("SKUId")
                    .Visible = False
                End With
                With .Columns("PalletId")
                    .Visible = False
                End With
                With .Columns("ShippingId")
                    .Visible = False
                End With
                With .Columns("SKU")
                    .AutoSize()
                End With
                With .Columns("PalletizerId")
                    .Visible = False
                End With
                With .Columns("OrderId")
                    .Visible = False
                End With
            End With
        End With

        UpdateQTY()

        GridPalletizer.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
    End Sub

    Public Sub dbSavePalletizer()
        Dim CONN = New SqlConnection(connectionString)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try

            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction
            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandText = "usp_Palletizer"
            CMD.Parameters.Clear()
            If txtPalletizerId.Text = "" Then
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbINSERT
                CMD.Parameters.Add("@PalletId", SqlDbType.Int).Value = CInt2(cmbPallet.SelectedValue)
                CMD.Parameters.Add("@SKUId", SqlDbType.Int).Value = CInt2(txtSKUPallet.Tag)
                CMD.Parameters.Add("@ShippingId", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.Parameters.Add("@QTY", SqlDbType.Int).Value = CInt2(txtQty.Text)
                CMD.Parameters.Add("@PalletizerId", SqlDbType.Int).Value = CInt2(txtPalletizerId.Text)
                CMD.Parameters.Add("@OrderId", SqlDbType.Int).Value = CInt2(txtOrder.Tag)
                CMD.ExecuteNonQuery()

            Else
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbUPDATE
                CMD.Parameters.Add("@PalletId", SqlDbType.Int).Value = CInt2(cmbPallet.SelectedValue)
                CMD.Parameters.Add("@SKUId", SqlDbType.Int).Value = CInt2(txtSKUPallet.Tag)
                CMD.Parameters.Add("@ShippingId", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.Parameters.Add("@QTY", SqlDbType.Int).Value = CInt2(txtQty.Text)
                CMD.Parameters.Add("@PalletizerId", SqlDbType.Int).Value = CInt2(GridPalletizer.GetValue("PalletizerId"))
                CMD.Parameters.Add("@OrderId", SqlDbType.Int).Value = CInt2(GridPalletizer.GetValue("OrderId"))
                CMD.ExecuteNonQuery()

            End If
            txtPalletizerId.Text = ""

            transaction.Commit()
            'Mb("Succeed", 1)
            'action = ESTR
        Catch ex As Exception
            transaction.Rollback()
            ErrHandler(ex)
        Finally
            CONN.Close()
            CONN.Dispose()
            CMD.Dispose()
            transaction.Dispose()
        End Try
        IsAccept = True
    End Sub

    Private Sub GridInvoice_DoubleClick(sender As Object, e As EventArgs) Handles GridInvoice.DoubleClick
        GridInvoice.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
        GetDataInvoice()
        txtQty.Enabled = True
        txtQty.Clear()
        cmbPallet.Text = ""
        cmbPallet.Tag = -1
    End Sub

    Private Sub UpdateQTY()
        TotalOnOrders = CInt2(DbExecuteScalar("Select sum(i.TotalQty) As TotalOnOrders from Invoice As i
                             Left Join SKU as s on i.SKUId = s.Id
                             where i.SKUId = " & CInt2(GridInvoice.GetValue("SKUId")) & "and i.ShippingId=" & CInt2(txtId.Text)))
        lblTotalOnOrders.Text = TotalOnOrders

        TotalOnShipment = CInt2(DbExecuteScalar("Select sum(i.QTY) As TotalOnPallet from Palletizer As i
                             Left Join SKU as s on i.SKUId = s.Id
                             where i.SKUId = " & CInt2(GridInvoice.GetValue("SKUId")) & "and i.ShippingId=" & CInt2(txtId.Text)))
        lblTotalOnShipment.Text = TotalOnShipment
        lblRemaining.Text = CInt2(lblTotalOnOrders.Text) - CInt2(lblTotalOnShipment.Text)
        If TotalOnOrders < TotalOnShipment Then
            lblTotalOnShipment.ForeColor = Color.Red
        ElseIf TotalOnOrders > TotalOnShipment Then
            lblTotalOnShipment.ForeColor = Color.DarkOrange
        Else
            lblTotalOnShipment.ForeColor = Color.DarkGreen
        End If


    End Sub

    Private Function ValidateFieldsQty() As Boolean
        Dim VarOrderID As Integer = 0
        If GridPalletizer.GetValue("OrderId") = 0 Then
            VarOrderID = GridInvoice.GetValue("OrderId")
        Else
            VarOrderID = GridPalletizer.GetValue("OrderId")
        End If

        Dim TotalQty As Integer = CInt2(DbExecuteScalar("Select i.totalqty from Invoice As i
                                                  where i.SKUid=" & CInt2(GridInvoice.GetValue("SKUId")) & "and i.orderid =" & VarOrderID))
        Dim aas As Integer = CInt2(GridPalletizer.GetValue("Qty"))
        Dim Qty As Integer = CInt2(DbExecuteScalar("Select sum(p.qty) As Qty from Palletizer As p
                                             where p.SKUid=" & CInt2(GridInvoice.GetValue("SKUId")) & "and p.orderId = " & VarOrderID)) - aas

        Dim Qty2 As Integer = CInt2(DbExecuteScalar("Select sum(p.qty) As Qty from Palletizer As p
                                             where p.SKUid=" & CInt2(GridInvoice.GetValue("SKUId")) & "and p.orderId = " & VarOrderID))

        If TotalQty < (Qty + CInt2(txtQty.Text)) Then
            Mb("You have reach the quantity on this order")
            txtQty.Focus()
            Return False
        End If

        'Dim qtytemp As Integer = 0
        'If GridPalletizer.GetValue("OrderId") = 0 Then
        '    qtytemp = CInt2(GridInvoice.GetValue("TotalQTY"))
        'Else
        '    qtytemp = TotalQty
        'End If

        If CInt2(txtQty.Text) > TotalQty Or (CInt2(txtQty.Text) + CInt2(lblTotalOnShipment.Text)) > CInt2(lblTotalOnOrders.Text) Then
            Mb("You are setting an incorrect Qty")
            txtQty.Focus()
            Return False
        End If

        If CInt2(txtQty.Text) = 0 Then
            Mb("You must enter QTY")
            txtQty.Focus()
            Return False
        End If
        Return True

    End Function

    Private Function ValidateFieldsInvoice() As Boolean
        If txtSKU.Tag = -1 Then
            Mb("You must enter an SKU")
            txtSKU.Focus()
            Return False
        End If
        If CInt2(txtTotalQty.Text) = 0 Then
            Mb("You must enter QTY")
            txtTotalQty.Focus()
            Return False
        End If
        Return True

    End Function

    Private Sub GridInvoice_DeletingRecord(sender As Object, e As RowActionCancelEventArgs) Handles GridInvoice.DeletingRecord
        Dim Resp As Short = 0

        Dim query2 As String = qFillGridPalletizerOrder
        query2 = query2.Replace("@SKUId", CInt2(GridInvoice.GetValue("SKUId"))).Replace("@ShippingId", CInt2(txtId.Text)).Replace("@OrderId", CInt2(txtOrder.Tag))

        Dim command2 As New SqlCommand(query2, conn)
        Dim adapter2 As New SqlDataAdapter(command2)
        Dim table2 As New DataTable()

        adapter2.Fill(table2)

        'If table2.Rows.Count = 0 Then
        Resp = MessageBox.Show("Are you sure?", "Deleting Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Resp = 6 Then

            Try
                If GridInvoice.CurrentRow IsNot Nothing Then
                    If GridInvoice.CurrentRow.RowType = RowType.Record Then
                        Dim DTable As DataTable = GridInvoice.DataSource
                        DeleteSKUInvoicefromPallet(CInt2(GridInvoice.GetValue("SKUId")), CInt2(txtOrder.Tag))
                        deleteDrFromGR(GridInvoice)

                    End If
                End If
            Catch ex As Exception
                ErrHandler(ex)

            End Try
        End If
        'Else
        '    Mb("You have this SKU in a Pallet")
        'End If

        fillGridInvoice()
        fillGridPalletizer()
        UpdateQTY()
        Colored()
        txtSKUPallet.Clear()
        txtQty.Enabled = False
        txtQty.Clear()
        cmbPallet.Text = ""
        cmbPallet.Tag = -1

    End Sub

    Private Sub DeleteSKUInvoicefromPallet(SKUID As Integer, ORDERID As Integer)
        Dim cmd As SqlCommand = New SqlCommand
        Try
            With cmd
                .Connection = New SqlConnection(ConnStr)
                .Connection.Open()
                .Transaction = cmd.Connection.BeginTransaction
                .CommandType = CommandType.Text
                .CommandText = "DELETE FROM Palletizer WHERE SKUId = " & SKUID & " and OrderId= " & ORDERID

                .ExecuteNonQuery()
                .Transaction.Commit()
            End With
        Catch ex As Exception
            If cmd.Transaction IsNot Nothing Then cmd.Transaction.Rollback()
            ErrHandler(ex)
        Finally
            If cmd IsNot Nothing Then
                If cmd.Connection IsNot Nothing Then
                    If cmd.Transaction IsNot Nothing Then cmd.Transaction.Dispose()
                    If cmd.Connection.State <> ConnectionState.Closed Then cmd.Connection.Close()
                    cmd.Connection.Dispose()
                End If
                cmd.Dispose()
            End If
        End Try

    End Sub

    Private Sub cmbCustomer2_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer2.SelectedValueChanged
        FillGridCustomer()
        FillcmbShippingLot()
        btnReport.Enabled = False
        btnExportGrid.Enabled = False
    End Sub

    Private Sub cmbShippingLot_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbShippingLot.SelectedValueChanged
        FillGridCustomer()
        If CStr2(cmbShippingLot.Text) <> "" Then
            btnReport.Enabled = True
            btnExportGrid.Enabled = True
        Else
            btnReport.Enabled = False
            btnExportGrid.Enabled = False
        End If

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs)
        Try
            If GridCustomer.RowCount = 0 And CStr2(GridCustomer.GetValue("SKU")) = "" Then
                Mb("No data available")
            Else
                UiTab1.SelectedIndex = 2
                Export(GridCustomer, "Palletizer ",,, True)
                'Export(GridCustomer, "Palletizer " & CStr2(cmbCustomer2.Text) & " " & CInt2(cmbShippingLot.Text))
                'CursorDefault(Me)
            End If
        Catch ex As Exception
            ErrHandler(ex)
        End Try

    End Sub

    'Private Sub chkMeasure_CheckedChanged(sender As Object, e As EventArgs) Handles chkMeasure.CheckedChanged
    '    If chkMeasure.Checked = True Then
    '        btnAddMeasure.Enabled = True
    '    End If
    'End Sub

    Private Sub btnAddMeasure_Click(sender As Object, e As EventArgs) Handles btnAddMeasure.Click
        Me.Enabled = False

        frmMeasure.MdiParent = Me.MdiParent
        frmMeasure.StartPosition = FormStartPosition.CenterScreen
        frmMeasure.Show()
        frmMeasure.txtHight.Focus()
    End Sub

    'Private Sub chkMeasure_Click(sender As Object, e As EventArgs) Handles chkMeasure.Click
    '    If chkMeasure.Checked = True Then
    '        btnAddMeasure.Enabled = True
    '    Else
    '        btnAddMeasure.Enabled = False
    '    End If
    'End Sub

    Private Function Print(Format As String) As Boolean
        Dim queryReport As String = qFillCustomer2
        queryReport = queryReport.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))
        Dim a As Integer = CInt2(cmbShippingLot.Text)
        Dim querycolumnas As String = qFillColumnas
        querycolumnas = querycolumnas.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))
        Dim queryMeasures As String = qFillMeasureReport
        queryMeasures = queryMeasures.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))
        'Dim queryOrdersPalletizer As String = qOrdersPalletizer
        'queryOrdersPalletizer = queryOrdersPalletizer.Replace("@CustomerId", CInt2(cmbCustomer2.SelectedValue)).Replace("@ShippingLot", CInt2(cmbShippingLot.Text))

        Dim dtColumnas As DataTable = DbFillDataTable(querycolumnas)
        Dim dtReport As DataTable = DbFillDataTable(queryReport)
        Dim dtMeasures As DataTable = DbFillDataTable(queryMeasures)
        'Dim dtOrders As DataTable = DbFillDataTable(queryOrdersPalletizer)

        Dim dataTable As DataTable = DirectCast(GridCustomer.DataSource, DataTable)

        Try
            ' Aspose.
            Dim wb As Aspose.Cells.Workbook = Nothing
            Dim ws As Aspose.Cells.Worksheet = Nothing
            Dim LicenseFile As New Aspose.Cells.License()
            LicenseFile.SetLicense("Aspose.Cells.lic")

            ' wb and ws.
            wb = New Aspose.Cells.Workbook()
            ws = wb.Worksheets(0)

            ' Vars.
            Dim tfs As Integer = 30
            Dim ts As Integer = 20
            Dim fs As Integer = 13
            Dim row As Integer = 0
            Dim cells As Aspose.Cells.Cells = ws.Cells
            cells.StandardWidth = 10

            ' PageSetup.
            With ws.PageSetup

                .PaperSize = Aspose.Cells.PaperSizeType.PaperLetter
                .Orientation = Aspose.Cells.PageOrientationType.Landscape
                .HeaderMargin = 0
                .TopMargin = 0.5
                .FooterMargin = 0.8
                .BottomMargin = 2.0
                .LeftMargin = 1.5
                .RightMargin = 1.5

                ' LogoEmpresa.
                Dim logoEmpresa As MemoryStream = DbGetLogo()
                'Dim logoEmpresa As Image = Image.FromFile(AppPath & "Logos.jpg")
                If Format = "PDF" Then

                    ' Autosize Logo Emisor (max 187 x 207 pixels, original size is not important).
                    Dim NewWidth As Integer = 0
                    Dim NewHeigth As Integer = 0
                    Dim MaxSize As Integer = 74
                    Dim picNumber = ws.Pictures.Add(0, 20, DbGetLogo())
                    If ws.Pictures(picNumber).OriginalWidth > ws.Pictures(picNumber).OriginalHeight Then
                        NewWidth = MaxSize
                        NewHeigth = ws.Pictures(picNumber).OriginalHeight * MaxSize \ ws.Pictures(picNumber).OriginalWidth
                    Else
                        NewWidth = ws.Pictures(picNumber).OriginalWidth * MaxSize \ ws.Pictures(picNumber).OriginalHeight
                        NewHeigth = MaxSize
                    End If
                    ws.Pictures(picNumber).Width = NewWidth + 180
                    ws.Pictures(picNumber).Height = NewHeigth + 150

                End If

                ' Footer.
                .SetFooter(1, "&B&8&IDeveloped by Noryad")

                ' BottomLeftLogo.
                'Dim bottomLeftLogo As Image = ResizeImage(LogoSavent, 180, 46)

                .SetFooter(0, "&G")
                '.SetFooterPicture(0, ImageToBytes(bottomLeftLogo))

                .IsPercentScale = False
                .FitToPagesWide = 1
                .FitToPagesTall = 0
                .PrintTitleRows = "$1:$10"

            End With

            ' ComercialName.

            If Format = "PDF" Then
                SetCell(ws, "Ford Medical, LLC", 7, 0, 1, 6, , , HorAlignment.L, tfs + 5, , True)

                ' Tittle.
                SetCell(ws, "Shipment Report", 8, 10, 1, 6, , , HorAlignment.L, tfs - 5, , True)

                'Address.
                SetCell(ws, "Corporate 33 Drive", 10, 0, 1, 5, , Color.Black, HorAlignment.L, ts, , ,)
                SetCell(ws, "Orangeburg, NY 10962", 11, 0, 1, 5, , Color.Black, HorAlignment.L, ts, , ,)


                ' Customer.
                SetCell(ws, "Customer: ", 14, 0, 1, 2, , Color.White, HorAlignment.L, ts, , True, True, Color.SteelBlue)
                SetCell(ws, CStr2(cmbCustomer2.Text), 14, 2, 1, 3, , Color.Black, HorAlignment.L, ts, , , , )
                ' Shipment.
                SetCell(ws, "Shipment: ", 15, 0, 1, 2, , Color.White, HorAlignment.L, ts, , True, True, Color.SteelBlue)
                SetCell(ws, CInt2(cmbShippingLot.Text), 15, 2, 1, 3, , Color.Black, HorAlignment.L, ts, , , , )
                ' Products.
                row = 19
            Else
                row = 0
            End If

            If Format = "PDF" Then
                SetCell(ws, "SKU", row, 0, 1, 3, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)

                Dim k As Integer = 1
                For j As Integer = 3 To dtColumnas.Rows.Count + 2
                    For Each dr As DataRow In dtColumnas.Rows
                        For Each ds As DataRow In dtReport.Rows
                            SetCell(ws, CStr2(dr("Pallet")), row, j, 1, 1, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)
                            SetCell(ws, CStr2(ds(CStr2(dr("Pallet")))), row + k, j, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                            k += 1
                        Next
                        j += 1
                        k = 1
                    Next
                Next

                If Format = "PDF" Then
                    row = 20
                Else
                    row = 1
                End If

                For Each ds As DataRow In dtReport.Rows
                    SetCell(ws, CStr2(ds("SKU")), row, 0, 1, 3, , Color.Black, HorAlignment.L, fs, , , , , Color.Gainsboro)
                    row += 1
                Next

                row += 2

                SetCell(ws, "Total Weight", row + 1, 0, 1, 2, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, lblTotalWeight.Text, row + 2, 0, 1, 2, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)


                'Measures
                SetCell(ws, "Long", row + 1, 2, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Width", row + 2, 2, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Hight", row + 3, 2, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Weight", row + 4, 2, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)


                Dim pos As Integer = 3
                For Each dp As DataRow In dtMeasures.Rows
                    SetCell(ws, CStr2(dp("Long")), row + 1, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    SetCell(ws, CStr2(dp("Width")), row + 2, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    SetCell(ws, CStr2(dp("Hight")), row + 3, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    SetCell(ws, CStr2(dp("Weight")), row + 4, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    pos += 1
                Next
            Else
                row = 0

                '---------

                SetCell(ws, "SKU", row, 0, 1, 2, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Orders", row, 2, 1, 1, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Total", row, 3, 1, 1, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)

                Dim k As Integer = 1
                For j As Integer = 4 To dtColumnas.Rows.Count + 3
                    For Each dr As DataRow In dtColumnas.Rows
                        For Each ds As DataRow In dtReport.Rows
                            Dim temp As Integer = CInt2(ds(CStr2(dr("Pallet"))))
                            SetCell(ws, CStr2(dr("Pallet")), row, j, 1, 1, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)
                            SetCell(ws, If(temp = 0, "", temp), row + k, j, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                            k += 1
                        Next
                        j += 1
                        k = 1
                    Next
                Next

                If Format = "PDF" Then
                    row = 20
                Else
                    row = 1
                End If

                For Each ds As DataRow In dtReport.Rows
                    SetCell(ws, CStr2(ds("SKU")), row, 0, 1, 2, , Color.Black, HorAlignment.L, fs, , , , , Color.Gainsboro)
                    row += 1
                Next
                row = 0
                For i = 0 To dataTable.Rows.Count - 1
                    'Dim ord As String = dataTable.Rows(i)("Orders")
                    SetCell(ws, dataTable.Rows(i)("Orders"), row + 1 + i, 2, 1, 1, , Color.Black, HorAlignment.L, fs, , , , , Color.Gainsboro)
                    SetCell(ws, dataTable.Rows(i)("Total"), row + 1 + i, 3, 1, 1, , Color.Black, HorAlignment.C, fs, , , , Color.Beige, Color.Gainsboro)
                Next


                row += dataTable.Rows.Count + 2

                SetCell(ws, "Total Weight", row + 1, 0, 1, 2, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, lblTotalWeight.Text, row + 2, 0, 1, 2, , Color.SteelBlue, HorAlignment.C, fs, , True, True, Color.Gainsboro, Color.White)

                'Measures
                SetCell(ws, "Long", row + 1, 3, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Width", row + 2, 3, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Hight", row + 3, 3, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)
                SetCell(ws, "Weight", row + 4, 3, 1, 1, , Color.SteelBlue, HorAlignment.R, fs, , True, True, Color.Gainsboro, Color.White)


                Dim pos As Integer = 4
                For Each dp As DataRow In dtMeasures.Rows
                    SetCell(ws, CStr2(dp("Long")), row + 1, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    SetCell(ws, CStr2(dp("Width")), row + 2, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    SetCell(ws, CStr2(dp("Hight")), row + 3, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    SetCell(ws, CStr2(dp("Weight")), row + 4, pos, 1, 1, , Color.Black, HorAlignment.C, fs, , , , , Color.Gainsboro)
                    pos += 1
                Next
                '---------

            End If

            ' Aspose options.
            ws.HorizontalPageBreaks.Clear()
            ws.VerticalPageBreaks.Clear()

            Dim options As New Aspose.Cells.AutoFitterOptions()
            options.AutoFitMergedCells = True
            ws.AutoFitRows(options)

            ' Save.
            Dim pdfFile As String
            If Format = "PDF" Then
                pdfFile = XmlPath & "Palletizer " & CStr2(cmbCustomer2.Text) & " Lot. " & CInt2(cmbShippingLot.Text) & ".pdf"
                wb.Save(pdfFile, Aspose.Cells.SaveFormat.Pdf)
            Else
                pdfFile = XmlPath & "Palletizer " & CStr2(cmbCustomer2.Text) & " Lot. " & CInt2(cmbShippingLot.Text) & ".xlsx"
                wb.Save(pdfFile, Aspose.Cells.SaveFormat.Xlsx)
            End If
            ' Open file.
            Process.Start(pdfFile)

        Catch ex As Exception
            ErrHandler(ex)
        Finally

        End Try

    End Function

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        If GridCustomer.RowCount > 1 Then
            Print("PDF")
        ElseIf GridCustomer.GetValue("Pallet1") <> "" Then
            Print("PDF")
        Else
            Mb("There is not data to show")
        End If

    End Sub

    Public Function DbGetLogo(Optional ByVal IdSucursal = 1) As MemoryStream

        Dim ms As MemoryStream = Nothing

        Try
            Dim result = Nothing

            result = ImageToBytes(ResizeImage(Image.FromFile(AppPath & "Logos.jpg"), 450, 450))
            Dim img As Byte() = Nothing

            If result IsNot Nothing Then
                If result IsNot DBNull.Value Then
                    img = result
                    If img.Length > 0 Then
                        Return New MemoryStream(img, False)
                    Else
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ErrHandler(ex)
            Return Nothing
        Finally

        End Try

    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MessageBox.Show("You are going to delete a Shipment" & vbCrLf & "¿Do you want to continue?", AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = vbYes Then
            DbDeleteShipment(GridMain.GetValue("ShippingId"))
            btnCancel.PerformClick()
        End If
    End Sub

    Private Sub DbDeleteShipment(ByVal Id As Integer)
        Dim cmd As SqlCommand = New SqlCommand
        Try
            With cmd
                .Connection = New SqlConnection(ConnStr)
                .Connection.Open()
                .Transaction = cmd.Connection.BeginTransaction
                .CommandType = CommandType.Text
                .CommandText = "DELETE FROM Shipping WHERE ShippingId = " & Id & "
                                DELETE FROM Orders where ShippingId = " & Id & "
                                DELETE FROM Invoice where ShippingId = " & Id & "
                                DELETE FROM Palletizer where ShippingId = " & Id & "
                                DELETE FROM Measure where IdShipping= " & Id

                .ExecuteNonQuery()
                .Transaction.Commit()
            End With
        Catch ex As Exception
            If cmd.Transaction IsNot Nothing Then cmd.Transaction.Rollback()
            ErrHandler(ex)
        Finally
            If cmd IsNot Nothing Then
                If cmd.Connection IsNot Nothing Then
                    If cmd.Transaction IsNot Nothing Then cmd.Transaction.Dispose()
                    If cmd.Connection.State <> ConnectionState.Closed Then cmd.Connection.Close()
                    cmd.Connection.Dispose()
                End If
                cmd.Dispose()
            End If
        End Try
    End Sub

    Private Sub txtTotalQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalQty.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub

    Private Sub txtShippingLot_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShippingLot.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub

    Private Sub DbDeletePalletizer(ByVal Id As Integer)
        Dim cmd As SqlCommand = New SqlCommand
        Try
            With cmd
                .Connection = New SqlConnection(ConnStr)
                .Connection.Open()
                .Transaction = cmd.Connection.BeginTransaction
                .CommandType = CommandType.Text
                .CommandText = "DELETE FROM Palletizer WHERE PalletizerId = " & Id

                .ExecuteNonQuery()
                .Transaction.Commit()
            End With
        Catch ex As Exception
            If cmd.Transaction IsNot Nothing Then cmd.Transaction.Rollback()
            ErrHandler(ex)
        Finally
            If cmd IsNot Nothing Then
                If cmd.Connection IsNot Nothing Then
                    If cmd.Transaction IsNot Nothing Then cmd.Transaction.Dispose()
                    If cmd.Connection.State <> ConnectionState.Closed Then cmd.Connection.Close()
                    cmd.Connection.Dispose()
                End If
                cmd.Dispose()
            End If
        End Try
    End Sub

    Private Sub GridPalletizer_DoubleClick(sender As Object, e As EventArgs) Handles GridPalletizer.DoubleClick

        If (txtQty.Text <> CStr2(GridPalletizer.GetValue("Qty")) And cmbPallet.Text <> CStr2(GridPalletizer.GetValue("PalletNumber"))) Or ordertemp <> CInt2(GridPalletizer.GetValue("OrderNumber")) Then
            GetDataPalletizer()
        End If

    End Sub

    Private Sub GetDataPalletizer()
        If GridPalletizer.CurrentRow IsNot Nothing AndAlso GridPalletizer.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            txtSKUPallet.Text = CStr2(GridPalletizer.GetValue("SKU"))
            txtSKUPallet.Tag = CInt2(GridPalletizer.GetValue("SKUId"))
            txtQty.Text = CInt2(GridPalletizer.GetValue("Qty"))
            cmbPallet.SelectedValue = CInt2(GridPalletizer.GetValue("Id"))
            cmbPallet.Text = CStr2(GridPalletizer.GetValue("PalletNumber"))
            txtPalletizerId.Text = CInt2(GridPalletizer.GetValue("PalletizerId"))

            'If CInt2(GridPalletizer.GetValue("Qty")) <> PartialQty And CInt2(GridPalletizer.GetValue("OrderNumber")) <> ordertemp Then
            '    lblTotalOnShipment.Text = CStr2(CInt2(lblTotalOnShipment.Text) + PartialQty)
            '    PartialQty = CInt2(GridPalletizer.GetValue("Qty"))
            '    ordertemp = CInt2(GridPalletizer.GetValue("OrderNumber"))
            '    lblTotalOnShipment.Text = CStr2(CInt2(lblTotalOnShipment.Text) - PartialQty)
            'End If

            lblTotalOnShipment.Text = CStr2(CInt2(lblTotalOnShipment.Text) - CInt2(GridPalletizer.GetValue("Qty")))
            lblRemaining.Text = CInt2(lblTotalOnOrders.Text) - CInt2(lblTotalOnShipment.Text)
            TotalOnShipment = CInt2(lblTotalOnShipment.Text)
            If TotalOnOrders < TotalOnShipment Then
                lblTotalOnShipment.ForeColor = Color.Red
            ElseIf TotalOnOrders > TotalOnShipment Then
                lblTotalOnShipment.ForeColor = Color.DarkOrange
            Else
                lblTotalOnShipment.ForeColor = Color.DarkGreen
            End If

            GridPalletizer.Enabled = False

        End If
    End Sub

    Private Sub btnPickForm_Click(sender As Object, e As EventArgs) Handles btnPickForm.Click
        PrintPF()
    End Sub

    Private Function PrintPF() As Boolean
        Dim query As String = qCmbOrdersReport
        Dim query1 As String = qCmbPalletMeasures

        query = query.Replace("@CustomerId", CInt2(GridMain.GetValue("CustomerId"))).Replace("@ShippingId", CInt2(GridMain.GetValue("ShippingId")))
        Dim Table As DataTable = DbFillDataTable(query)

        query1 = query1.Replace("@ShippingId", CInt2(GridMain.GetValue("ShippingId")))
        Dim dtCmbPallet As DataTable = DbFillDataTable(query1)

        If Table.Rows.Count > 0 Then
            Try
                ' Aspose.
                Dim wb As Aspose.Cells.Workbook = Nothing
                Dim ws As Aspose.Cells.Worksheet = Nothing
                Dim LicenseFile As New Aspose.Cells.License()
                LicenseFile.SetLicense("Aspose.Cells.lic")

                ' wb and ws.
                wb = New Aspose.Cells.Workbook()
                ws = wb.Worksheets(0)

                ' Vars.
                Dim tfs As Integer = 40
                Dim ts As Integer = 30
                Dim fs As Integer = 20
                Dim row As Integer = 0
                Dim cells As Aspose.Cells.Cells = ws.Cells
                cells.StandardWidth = 10

                ' PageSetup.
                With ws.PageSetup

                    .PaperSize = Aspose.Cells.PaperSizeType.PaperLetter
                    .Orientation = Aspose.Cells.PageOrientationType.Portrait
                    .HeaderMargin = 0
                    .TopMargin = 0.5
                    .FooterMargin = 0.8
                    .BottomMargin = 2.0
                    .LeftMargin = 1.5
                    .RightMargin = 1.5

                    ' LogoEmpresa.
                    Dim logoEmpresa As MemoryStream = DbGetLogo()
                    'Dim logoEmpresa As Image = Image.FromFile(AppPath & "Logos.jpg")
                    If logoEmpresa IsNot Nothing Then

                        ' Autosize Logo Emisor (max 187 x 207 pixels, original size is not important).
                        Dim NewWidth As Integer = 0
                        Dim NewHeigth As Integer = 0
                        Dim MaxSize As Integer = 74
                        Dim picNumber = ws.Pictures.Add(0, 20, DbGetLogo())
                        If ws.Pictures(picNumber).OriginalWidth > ws.Pictures(picNumber).OriginalHeight Then
                            NewWidth = MaxSize
                            NewHeigth = ws.Pictures(picNumber).OriginalHeight * MaxSize \ ws.Pictures(picNumber).OriginalWidth
                        Else
                            NewWidth = ws.Pictures(picNumber).OriginalWidth * MaxSize \ ws.Pictures(picNumber).OriginalHeight
                            NewHeigth = MaxSize
                        End If
                        ws.Pictures(picNumber).Width = NewWidth + 220
                        ws.Pictures(picNumber).Height = NewHeigth + 190

                    End If

                    ' Footer.
                    .SetFooter(1, "&B&8&IDeveloped by Noryad")

                    .SetFooter(0, "&G")
                    '.SetFooterPicture(0, ImageToBytes(bottomLeftLogo))

                    .IsPercentScale = False
                    .FitToPagesWide = 1
                    .FitToPagesTall = 0
                    .PrintTitleRows = "$1:$10"

                    ' BottomRightLogo.

                    'Dim bottomRightLogo As Image = Nothing
                    'bottomRightLogo = Image.FromFile(AppPath & "Logos.jpg")
                    'bottomRightLogo = ResizeImage(bottomRightLogo, 180, 46)
                    '.SetFooter(2, "&G")
                    '.SetFooterPicture(2, ImageToBytes(bottomRightLogo))


                End With

                ' ReportName.
                SetCell(ws, "Picked Form", 7, 0, 1, 8, , , HorAlignment.L, tfs + 20, , True)

                ' Tittle.
                'SetCell(ws, "Picked Form", 8, 10, 1, 6, , , HorAlignment.L, tfs - 5, , True)

                ' Address.
                SetCell(ws, "Corporate 33 Drive", 11, 0, 1, 6, , Color.Black, HorAlignment.L, ts, , ,)
                SetCell(ws, "Orangeburg, NY 10962", 12, 0, 1, 6, , Color.Black, HorAlignment.L, ts, , ,)


                ' Customer.
                SetCell(ws, "Customer: ", 16, 0, 1, 3, , Color.White, HorAlignment.L, ts, , True, True, Color.SteelBlue)
                SetCell(ws, CStr2(GridMain.GetValue("Customer")), 16, 3, 1, 5, , Color.Black, HorAlignment.L, ts, , , , )
                ' Shipment.
                SetCell(ws, "Shipment: ", 17, 0, 1, 3, , Color.White, HorAlignment.L, ts, , True, True, Color.SteelBlue)
                SetCell(ws, CInt2(GridMain.GetValue("Shipment")), 17, 3, 1, 5, , Color.Black, HorAlignment.L, ts, , , , )
                ' Pallets.
                SetCell(ws, "Quantity:", 16, 20, 1, 3, , Color.White, HorAlignment.L, ts, , True, True, Color.SteelBlue)
                SetCell(ws, CInt2(dtCmbPallet.Rows.Count) & " Pallet(s)", 16, 23, 1, 5, , Color.Black, HorAlignment.L, ts, , , , )

                ' Orders.
                row = 21
                SetCell(ws, "Orders", row, 0, 1, 3, , Color.SteelBlue, HorAlignment.C, ts + 2, , True, True, Color.Gainsboro, Color.White)

                Dim k As Integer = 0
                Dim h As Integer = 0
                Dim j As Integer = 0

                Dim Columnas As Integer = Table.Rows.Count

                If Table.Rows.Count <= 20 Then
                    For i = 0 To Table.Rows.Count - 1
                        k = 3
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        'i += 1
                        h += 1
                    Next
                    SetCell(ws, "Date of Pick Up:", row + h + 10, 0, 1, 5, ,, HorAlignment.L, ts, , True)
                    SetCell(ws, "Sign:", row + h + 13, 0, 1, 4, ,, HorAlignment.L, ts, , True)
                End If
                If Table.Rows.Count >= 20 And Table.Rows.Count < 40 Then
                    For i = 0 To 19
                        k = 3
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        'i += 1
                        h += 1
                        j = h
                    Next
                    h = 0
                    For i = 20 To Table.Rows.Count - 1
                        k = 6
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        'i += 1
                        h += 1
                    Next
                    SetCell(ws, "Date of Pick Up:", row + j + 10, 0, 1, 5, ,, HorAlignment.L, ts, , True)
                    SetCell(ws, "Sign:", row + j + 13, 0, 1, 4, ,, HorAlignment.L, ts, , True)
                End If
                If Table.Rows.Count >= 40 And Table.Rows.Count < 60 Then
                    For i = 0 To 19
                        k = 3
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                        j = h
                    Next
                    h = 0
                    For i = 20 To 39
                        k = 6
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    h = 0
                    For i = 40 To Table.Rows.Count - 1
                        k = 9
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next

                    SetCell(ws, "Date of Pick Up:", row + j + 10, 0, 1, 5, ,, HorAlignment.L, ts, , True)
                    SetCell(ws, "Sign:", row + j + 13, 0, 1, 4, ,, HorAlignment.L, ts, , True)
                End If
                If Table.Rows.Count >= 60 And Table.Rows.Count < 80 Then
                    For i = 0 To 19
                        k = 3
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                        j = h
                    Next
                    h = 0
                    For i = 20 To 39
                        k = 6
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    h = 0
                    For i = 40 To 59
                        k = 9
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    h = 0
                    For i = 60 To Table.Rows.Count - 1
                        k = 12
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next

                    SetCell(ws, "Date of Pick Up:", row + j + 10, 0, 1, 5, ,, HorAlignment.L, ts, , True)
                    SetCell(ws, "Sign:", row + j + 13, 0, 1, 4, ,, HorAlignment.L, ts, , True)
                End If

                If Table.Rows.Count >= 80 Then
                    For i = 0 To 19
                        k = 3
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                        j = h
                    Next
                    h = 0
                    For i = 20 To 39
                        k = 6
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    h = 0
                    For i = 40 To 59
                        k = 9
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    h = 0
                    For i = 60 To 79
                        k = 12
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    h = 0
                    For i = 80 To Table.Rows.Count - 1
                        k = 15
                        SetCell(ws, CStr2(Table.Rows(i)("OrderNumber")), row + h, k, 1, 3, , Color.SteelBlue, HorAlignment.L, ts, , True, True, , Color.White)
                        h += 1
                    Next
                    SetCell(ws, "Date of Pick Up:", row + j + 10, 0, 1, 5, ,, HorAlignment.L, ts, , True)
                    SetCell(ws, "Sign:", row + j + 13, 0, 1, 4, ,, HorAlignment.L, ts, , True)
                End If

                ' Aspose options.
                ws.HorizontalPageBreaks.Clear()
                ws.VerticalPageBreaks.Clear()

                Dim options As New Aspose.Cells.AutoFitterOptions()
                options.AutoFitMergedCells = True
                ws.AutoFitRows(options)

                ' Save.
                Dim pdfFile As String = XmlPath & "Picked Form " & CStr2(GridMain.GetValue("Customer")) & " Lot. " & CInt2(GridMain.GetValue("Shipment")) & ".pdf"
                wb.Save(pdfFile, Aspose.Cells.SaveFormat.Pdf)

                ' Open file.
                Process.Start(pdfFile)
            Catch ex As Exception
                ErrHandler(ex)
            Finally

            End Try
        End If


    End Function

    Private Sub GridPalletizer_DeletingRecord(sender As Object, e As RowActionCancelEventArgs) Handles GridPalletizer.DeletingRecord
        If GridPalletizer.GetValue("OrderId") = txtOrder.Tag Then
            Dim Message As String = "You are going to delete this line" & vbCrLf & "¿Do you want to continue?"
            If MessageBox.Show(Message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = System.Windows.Forms.DialogResult.Yes Then
                DbDeletePalletizer(GridPalletizer.GetValue("PalletizerId"))

            End If
        Else
            Mb("Please select the correct Order")
        End If
        fillGridPalletizer()
        Colored()
        txtQty.Text = ""
        cmbPallet.SelectedValue = -1

    End Sub

    Private Sub chkShipped_CheckedChanged(sender As Object, e As EventArgs) Handles chkShipped.CheckedChanged
        If chkShipped.Checked = True Then
            dtpShippedDate.Enabled = True
            dtpShippedDate.Text = Now
            btnsend.Enabled = True

            'txtBOL.Enabled = True
        Else
            dtpShippedDate.Enabled = False
            dtpShippedDate.Text = ""
            btnsend.Enabled = False
            'txtBOL.Enabled = False
        End If

    End Sub

    Private Sub dtpShippedDate_ValueChanged(sender As Object, e As EventArgs)
        sender.Checked = True
    End Sub

    Private Sub dtpShippedDate_CheckedChanged(sender As Object, e As EventArgs)
        If action <> ESTR Then
            Select Case dtpShippedDate.Checked
                Case True
                    If action = "INSERT" Or action = "UPDATE" And sender.Checked = True Then sender.Value = Now
                Case False
                    dtpShippedDate.Text = ESTR
            End Select
        End If
    End Sub

    Private Sub btnExportGrid_Click(sender As Object, e As EventArgs) Handles btnExportGrid.Click

        CursorWait(Me)
        Select Case UiTab1.SelectedTab.Index
            'Case 0
            '    GridAExcel(dgv)
            Case 2
                Print("XLSX")
        End Select
        CursorDefault(Me)
    End Sub

    Private Sub HyperV_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles HyperV.LinkClicked
        'HyperV.LinkClicked += New LinkLabelLinkClickedEventHandler(AddressOf HyperV_LinkClicked)
        Dim H As String = "https://fml.cwa.sellercloud.com/Orders/Orders_details.aspx?id=" & txtOrder.Text
        Process.Start(H)
    End Sub

    Sub Colored()
        For Each grx As GridEXRow In GridInvoice.GetDataRows
            Dim QtyPalletizer As Integer = CInt2(DbExecuteScalar("Select sum(i.QTY) As TotalOnPallet from Palletizer As i
                             Left Join SKU as s on i.SKUId = s.Id
                             where i.SKUId = " & CInt2(grx.Cells("SKUId").Value) & "and i.OrderId=" & CInt2(txtOrder.Tag)))

            If CInt2(grx.Cells("TotalQty").Value) = QtyPalletizer Then
                Dim newStyle As New GridEXFormatStyle ' Crear un nuevo estilo de fila
                newStyle.BackColor = Color.LightGreen ' Establecer el color de fondo del nuevo estilo de fila
                grx.RowStyle = newStyle ' Asignar el nuevo estilo de fila a la fila correspondiente

            ElseIf CInt2(grx.Cells("TotalQty").Value) > QtyPalletizer And QtyPalletizer > 0 Then
                Dim newStyle As New GridEXFormatStyle ' Crear un nuevo estilo de fila
                newStyle.BackColor = Color.LightGoldenrodYellow ' Establecer el color de fondo del nuevo estilo de fila
                grx.RowStyle = newStyle ' Asignar el nuevo estilo de fila a la fila correspondiente
            Else
                Dim newStyle As New GridEXFormatStyle ' Crear un nuevo estilo de fila
                grx.RowStyle = newStyle ' Asignar el nuevo estilo de fila a la fila correspondiente
            End If
        Next
    End Sub

    Private Sub txtOrder2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOrder2.KeyDown
        If e.KeyCode <> Keys.Enter Then
            Exit Sub
        End If
        Try
            ' Query.
            Dim query As String = qCmbOrders2

            Dim filter As String =
                <SQL><![CDATA[
					Where o.OrderNumber LIKE '%@TTF%'
					ORDER BY o.Id
				]]></SQL>.Value.Replace("@TTF", txtOrder2.Text.Trim)

            ' Find the text in the DB.
            Using f As frmFind13 = New frmFind13
                With f
                    .Query = query
                    .Filter = filter
                    .SearchCol = "OrderNumber"
                    .ShowDialog()

                    If .Found Then
                        txtOrder2.Tag = CInt2(.DTSel.Rows(0)("Id"))
                        txtOrder2.Text = CStr2(.DTSel.Rows(0)("OrderNumber"))
                    Else
                        txtOrder2.Clear()
                        txtOrder2.Tag = -1
                    End If

                End With
            End Using

        Catch ex As Exception
            ErrHandler(ex)
        End Try
    End Sub

    Private Sub txtOrder2_TextChanged(sender As Object, e As EventArgs) Handles txtOrder2.TextChanged
        Dim Customer As String = CStr2(DbExecuteScalar("Select c.Name from Customer as c Left Join Orders as o on c.Id=o.CustomerId where o.Id=" & CInt2(txtOrder2.Tag)))
        Dim Shipment As String = CStr2(DbExecuteScalar("Select s.ShippingLot from Shipping as s Left Join Orders as o on s.ShippingId=o.ShippingId where o.Id=" & CInt2(txtOrder2.Tag)))
        Dim Status As String = CInt2(DbExecuteScalar("Select sum(p.Qty) from Palletizer as p Left Join Orders as o on p.OrderId=o.Id where o.Id=" & CInt2(txtOrder2.Tag)))
        Dim TotalO As String = CInt2(DbExecuteScalar("Select sum(i.TotalQty) from Invoice as i Left Join Orders as o on i.OrderId=o.Id where o.Id=" & CInt2(txtOrder2.Tag)))

        If txtOrder2.Tag <> -1 Then
            lblCustomer.Text = Customer
            lblShipment.Text = Shipment
            If Status = TotalO Then
                If Status = 0 Then
                    lblStatus.Text = ""
                Else
                    lblStatus.Text = "Complete"
                End If
            Else
                lblStatus.Text = "Partial"
            End If
        Else
            lblCustomer.Text = ""
            lblShipment.Text = ""
            lblStatus.Text = ""
        End If
        txtOrder2.Tag = -1
    End Sub
    Public Sub SendEmail()
        Try
            ' Email Server Instance
            Dim SmtpServer As New SmtpClient()
            SmtpServer.Credentials = New Net.NetworkCredential("OR33shippingstation1@fordmed.com", "pdKN7v9Gw8")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.office365.com"
            SmtpServer.EnableSsl = True

            ' Email Details
            Dim mail As New MailMessage()
            mail.From = New MailAddress("OR33shippingstation1@fordmed.com")
            mail.To.Add("HillelO@fordmed.com")
            mail.Subject = "Corporate Shipment sent"
            mail.Body = "Customer:" & cmbCustomer.Text & "  Shipment:" & txtShippingLot.Text & "  BOL:" & txtBOL.Text


            'Send
            SmtpServer.Send(mail)
            MsgBox("Succesfully")
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Sub Main()

        '' La URL de la solicitud
        'Dim url As String = "https://fml.api.sellercloud.com/rest/api/token"

        '' Crear una solicitud al servidor
        'Dim request As WebRequest = WebRequest.Create(url)

        '' Configurar la solicitud
        'request.Method = "POST"
        'request.ContentType = "application/json"
        'request.Headers.Add("X-Auth-Token", access_token)


        '' Crear el cuerpo de la solicitud 
        'Dim json As String = "OrderID: " & txtOrder.Text

        '' Escribir los datos en la solicitud
        'Dim data As Byte() = System.Text.Encoding.UTF8.GetBytes(json)
        'request.ContentLength = data.Length

        'Using stream As Stream = request.GetRequestStream()
        '    stream.Write(data, 0, data.Length)
        'End Using

        '' Enviar la solicitud y obtener la respuesta
        'Dim response As WebResponse = request.GetResponse()

        '' Leer la respuesta
        'Using reader As New StreamReader(response.GetResponseStream())
        '    Dim result As String = reader.ReadToEnd()

        '    ' Mostrar los resultados
        '    Console.WriteLine(result)
        'End Using



        Dim access_token As String = Nothing
        Dim request1 As HttpWebRequest = CType(WebRequest.Create("https://fml.api.sellercloud.com/rest/api/token"), HttpWebRequest)
        request1.Method = "POST"
        request1.ContentType = "application/json"
        Dim body As String = "{""username"":""OR33shippingstation1@fordmed.com"", ""password"":""4@W5u!wNUq8IQ1A""}"

        Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(body)
        request1.ContentLength = byteArray.Length
        Using dataStream As Stream = request1.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
        End Using
        Using response1 As HttpWebResponse = CType(request1.GetResponse(), HttpWebResponse)
            Dim statusCode As String = response1.StatusCode.ToString()

            If statusCode = "OK" Then
                Dim streamReader As New StreamReader(response1.GetResponseStream())
                Dim jsonResponse As String = streamReader.ReadToEnd()
                Dim tokenResponse As JObject = JsonConvert.DeserializeObject(jsonResponse)
                access_token = tokenResponse("access_token").ToString()
            End If
        End Using

        Dim url = "https://fml.api.sellercloud.com/rest/api/orders/" & txtOrder.Text
        Dim httpRequest = CType(WebRequest.Create(url), HttpWebRequest)
        httpRequest.ContentType = "application/json"
        httpRequest.Headers("Authorization") = "Bearer " & access_token
        Dim httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
        Using streamReader = New StreamReader(httpResponse.GetResponseStream())
            Dim result = streamReader.ReadToEnd()
            Dim order As JObject = JsonConvert.DeserializeObject(result)

        End Using


        'Dim client As HttpClient()
        'Dim request1 As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Get, "https://fml.cwa.sellercloud.com/Orders/Orders_details.aspx?id=12089780")
        'request1.Headers.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "token")
        'Dim response1 As HttpResponseMessage = client.SendAsync(request1).Result
        'If response1.IsSuccessStatusCode Then
        '    Dim json1 As String = response.Content.ReadAsStringAsync().Result
        '    Dim order As Order = JsonConvert.DeserializeObject(Of Order)(json)

        '    Dim dt As DataTable = New DataTable()
        '    dt.Columns.Add("SKU", GetType(String))
        '    dt.Columns.Add("QTY", GetType(Integer))

        '    For Each item As OrderItem In order.OrderItems
        '        dt.Rows.Add(item.ProductId, item.Qty)
        '    Next
        'End If

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Main()
    End Sub

    Private Sub btnsend_Click(sender As Object, e As EventArgs) Handles btnsend.Click
        Dim Message As String = "You are going to send an email" & vbCrLf & "¿Do you want to continue?"
        If MessageBox.Show(Message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = System.Windows.Forms.DialogResult.Yes Then
            SendEmail()
        End If
    End Sub

    Private Sub GridInvoice_Scroll(sender As Object, e As EventArgs) Handles GridInvoice.Scroll
        Colored()
    End Sub

    Private Sub fillGridSKUFinder()

        Dim query As String = qFillSKUFinder
        query = query.Replace("@SKUFinder", CStr2(txtSKUFinder.Text))

        Dim command As New SqlCommand(query, conn)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        Dim sig As New siGrid18(GridSKUFinder, table)

        With GridSKUFinder
            With .RootTable ' RootTable.
                With .Columns("Id")
                    .Visible = False
                End With
                With .Columns("OrderId")
                    .Visible = False
                End With
                With .Columns("SKU")
                    .AutoSize()
                End With

            End With
        End With

        GridSKUFinder.RootTable.Groups.Clear()
        GridSKUFinder.RootTable.GroupHeaderTotals.Clear()
        GridSKUFinder.HideColumnsWhenGrouped = InheritableBoolean.True

        Dim column1 As GridEXColumn = GridSKUFinder.RootTable.Columns("Customer")
        Dim column2 As GridEXColumn = GridSKUFinder.RootTable.Columns("OrderNumber")

        Dim group1 As GridEXGroup = New GridEXGroup(column1)
        Dim group2 As GridEXGroup = New GridEXGroup(column2)

        GridSKUFinder.RootTable.Groups.Add(group1)
        GridSKUFinder.RootTable.Groups.Add(group2)

        GridSKUFinder.ExpandGroups()
        txtSKUFinder.Focus()
        txtSKUFinder.SelectionStart = txtSKUFinder.Text.Length
    End Sub

    Private Sub txtSKUFinder_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSKUFinder.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                Dim query As String = qFillSKU

                Dim filter As String =
                <SQL><![CDATA[
					A.SKU LIKE '%@TTF%'
					ORDER BY A.Id
				]]></SQL>.Value.Replace("@TTF", txtSKUFinder.Text.Trim)

                Using f As frmFind13 = New frmFind13
                    With f
                        .Query = query
                        .Filter = filter
                        .SearchCol = "SKU"
                        .UseSelectorCol = True
                        .ShowDialog()

                        If sender.name.toupper = "TXTSKUFINDER" Then
                            If .Found Then
                                txtSKUFinder.Text = CStr2(.DTSel.Rows(0).Item("SKU"))
                                txtSKUFinder.Tag = .DTSel.Rows(0).Item("Id")
                            Else
                                txtSKUFinder.Clear()
                                txtSKUFinder.Tag = -1
                            End If
                        End If

                    End With
                End Using

            End If
        Catch ex As Exception
            ErrHandler(ex)
        End Try
    End Sub

    Private Sub txtSKUFinder_TextChanged(sender As Object, e As EventArgs) Handles txtSKUFinder.TextChanged
        If txtSKUFinder.Text <> "" Then
            fillGridSKUFinder()
        End If

        'If txtSKUFinder.TextLength > 0 Then
        '    If CStr2(txtSKUFinder.Tag) <> -1 Then
        '        fillGridSKUFinder()
        '        txtSKUFinder.Focus()
        '    End If
        'Else
        '    If txtSKUFinder.TextLength <= 7 Then
        '        txtSKUFinder.Tag = -1
        '        fillGridSKUFinder()
        '        txtSKUFinder.Focus()
        '    End If

        'End If





    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'Crear una instancia del formulario adicional
        Dim frmLogin As New Login()

        'Mostrar el formulario adicional y esperar hasta que se cierre
        frmLogin.ShowDialog()

        'Mostrar el formulario principal
        Me.Show()
        btnCancel.PerformClick()
    End Sub
End Class
