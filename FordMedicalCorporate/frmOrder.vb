Imports System.Data.SqlClient
Imports Janus.Windows.GridEX
Imports siCom40
Imports siCom40.Com

Public Class frmOrder
    Dim IsAccept As Boolean = False
    Dim action As String = ESTR
    Dim dtOrder As New DataTable
    Dim LastReg As String

    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Dim connectionString As String = "Data Source=localhost;Initial Catalog=FordMedical;User ID=sa;Password=Savent01$"
    Dim conn As New SqlConnection(connectionString)

    Private Sub frmOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtId.Text = LastReg
        txtOrderAdd.Clear()

        Try
            txtOrderAdd.Enabled = True
            Dim r As Resizer = New Resizer(Me)
        Catch ex As Exception
            ErrHandler(ex)
        End Try

        action = ESTR

        fillGridOrders()
        txtOrderAdd.Focus()
        GridOrder.EditMode = EditMode.EditOn
        GridOrder.RootTable.AllowAddNew = InheritableBoolean.False
        GridOrder.RootTable.AllowEdit = InheritableBoolean.True
        GridOrder.RootTable.AllowDelete = InheritableBoolean.True

    End Sub

    Private Sub fillGridOrders()

        Dim query As String = qCmbOrdersReport

        query = query.Replace("@CustomerId", CInt2(frmFordM.cmbCustomer.SelectedValue)).Replace("@ShippingId", CInt2(frmFordM.txtId.Text))

        Dim Table As DataTable = DbFillDataTable(query)

        Dim sig As New siGrid18(GridOrder, table)

        With GridOrder
            With .RootTable ' RootTable.
                With .Columns("Id")
                    .Visible = False
                End With

                With .Columns("OrderNumber")
                    .AutoSize()
                End With
            End With
            End With

        'GridOrder.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
    End Sub

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
            CMD.CommandText = "[dbo].[usp_Orders]"
            CMD.Parameters.Clear()
            If txtId.Text = "" Then
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbINSERT
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.Parameters.Add("@OrderNumber", SqlDbType.NVarChar, 50).Value = CInt2(txtOrderAdd.Text)
                CMD.Parameters.Add("@CustomerId", SqlDbType.Int).Value = CInt2(frmFordM.cmbCustomer.SelectedValue)
                CMD.Parameters.Add("@ShippingId", SqlDbType.Int).Value = CInt2(frmFordM.txtId.Text)
                CMD.Parameters.Add("@Date", SqlDbType.Date).Value = Now

                CMD.ExecuteNonQuery()

                '--------------------------------------------------------------------------------------------
                'Get the Identity of the Last Inserted Record.
                '--------------------------------------------------------------------------------------------
                'CMD.CommandType = CommandType.Text
                'CMD.CommandText = "SELECT @@IDENTITY FROM Orders"
                'Dim Identity As Integer = CInt2(CMD.ExecuteScalar())
                txtId.Text = ""

            Else
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbUPDATE
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = GridOrder.GetValue("ID")
                CMD.Parameters.Add("@OrderNumber", SqlDbType.NVarChar, 50).Value = CInt2(txtOrderAdd.Text)
                CMD.Parameters.Add("@CustomerId", SqlDbType.Int).Value = CInt2(frmFordM.cmbCustomer.SelectedValue)
                CMD.Parameters.Add("@ShippingId", SqlDbType.Int).Value = CInt2(frmFordM.txtId.Text)
                CMD.Parameters.Add("@Date", SqlDbType.Date).Value = Now

                CMD.ExecuteNonQuery()

                txtId.Text = ""
            End If

            transaction.Commit()

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

        IsAccept = True

    End Sub

    Private Sub frmOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'frmFordM.btnCancel.PerformClick()
        frmFordM.Enabled = True
        frmFordM.MdiParent = Me.MdiParent
        frmFordM.StartPosition = FormStartPosition.CenterScreen
        frmFordM.Show()
    End Sub

    Private Sub GridOrder_Click(sender As Object, e As EventArgs) Handles GridOrder.Click
        If action = ESTR Then GetData()
    End Sub

    Private Sub GetData()
        If GridOrder.CurrentRow IsNot Nothing AndAlso GridOrder.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            txtId.Text = CStr2(GridOrder.GetValue("Id"))
            txtOrderAdd.Text = CStr2(GridOrder.GetValue("OrderNumber"))

        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim Notcopy As Boolean = False

        If txtOrderAdd.Text = "" Then
            Mb("Order Number is mandatory")
        Else

            For Each grx As GridEXRow In GridOrder.GetDataRows

                If CStr2(txtOrderAdd.Text) = CStr2(grx.Cells("OrderNumber").Value) Then
                    Notcopy = True
                End If

            Next
            If Notcopy = False Then
                dbSaveStatus(action)
                fillGridOrders()
                txtOrderAdd.Text = ""
            Else
                Mb("This Order already exist")
            End If
        End If
        txtOrderAdd.Focus()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click


        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow
        checkedRows = GridOrder.GetCheckedRows()
        Dim query As String = qOrder
        query = query.Replace("@OrderId", CInt2(GridOrder.GetValue("Id")))

        Dim command As New SqlCommand(query, conn)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        If checkedRows.Length = 0 Then
            MessageBox.Show("Select 1 Order to Delete.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim Message As String = "You are going to delete " & checkedRows.Length & " Order(s)." & vbCrLf & "¿Do you want to continue?"
            If table.Rows.Count = 0 Then
                If MessageBox.Show(Message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = System.Windows.Forms.DialogResult.Yes Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow
                    For Each Row In checkedRows
                        dbDeleteOrder(Row.Cells("Id").Value)
                    Next
                    fillGridOrders()
                    txtOrderAdd.Clear()
                End If
            Else
                Mb("You have this Order in an Invoice")
            End If
        End If
    End Sub

    Private Sub dbDeleteOrder(ByVal Id As Integer)
        Dim CONN = New SqlConnection(ConnStr)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try
            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction
            CMD.CommandType = CommandType.Text
            CMD.CommandText = "DELETE FROM Orders WHERE Id=" & Id
            CMD.ExecuteNonQuery()
            transaction.Commit()
        Catch ex As Exception
            ErrHandler(ex)
        Finally
            CONN.Close()
            CONN.Dispose()
            CMD.Dispose()
        End Try
        txtId.Text = ""
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'frmFordM.FillcmbOrders()
        Me.Close()
    End Sub

    Private Sub txtOrderAdd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderAdd.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub
End Class