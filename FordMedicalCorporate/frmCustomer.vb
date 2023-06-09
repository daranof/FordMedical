Imports System.Data.SqlClient
Imports Janus.Windows.GridEX
Imports siCom40
Imports siCom40.Com

Public Class frmCustomer
    Dim IsAccept As Boolean = False
    Dim action As String = ESTR
    Dim dtCustomer As New DataTable

    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Dim connectionString As String = "Data Source=localhost;Initial Catalog=FordMedical;User ID=sa;Password=Savent01$"
    Dim conn As New SqlConnection(connectionString)

    Private Sub frmCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtNameAdd.Clear()
        txtAddressAdd.Clear()
        txtZipAdd.Clear()
        txtStateAdd.Clear()
        txtCountryAdd.Clear()
        Try
            txtNameAdd.Enabled = True
            txtAddressAdd.Enabled = True
            txtZipAdd.Enabled = True
            txtStateAdd.Enabled = True
            txtCountryAdd.Enabled = True

            Dim r As Resizer = New Resizer(Me)
        Catch ex As Exception
            ErrHandler(ex)
        End Try


        dtCustomer.Columns.Add("Name")

        GridCustomers.DataSource = dtCustomer

        action = ESTR

        fillGridCustomers()

        GridCustomers.EditMode = EditMode.EditOn
        GridCustomers.RootTable.AllowAddNew = InheritableBoolean.False
        GridCustomers.RootTable.AllowEdit = InheritableBoolean.True
        GridCustomers.RootTable.AllowDelete = InheritableBoolean.True

    End Sub

    Private Sub fillGridCustomers()

        Dim query As String = "SELECT c.Name, c.Address, c.Zip, c.State, c.Country, c.Id
                                       From Customer as c									  
                                       ORDER BY c.Name"

        Dim Table As DataTable = DbFillDataTable(query)

        Dim sig As New siGrid18(GridCustomers, Table)

        With GridCustomers
            With .RootTable ' RootTable.
                With .Columns("Id")
                    .Visible = False
                End With
                With .Columns("Name")
                    .AutoSize()
                End With
                With .Columns("Address")
                    .Visible = False
                End With
                With .Columns("Zip")
                    .Visible = False
                End With
                With .Columns("State")
                    .Visible = False
                End With
                With .Columns("Country")
                    .Visible = False
                End With

            End With
        End With

        'GridSKU.RootTable.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
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
            CMD.CommandText = "[dbo].[usp_Customer]"
            CMD.Parameters.Clear()
            If txtId.Text = "" Then
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbINSERT
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = CStr2(txtNameAdd.Text)
                CMD.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = CStr2(txtAddressAdd.Text)
                CMD.Parameters.Add("@Zip", SqlDbType.NVarChar, 100).Value = CStr2(txtZipAdd.Text)
                CMD.Parameters.Add("@State", SqlDbType.NVarChar, 100).Value = CStr2(txtStateAdd.Text)
                CMD.Parameters.Add("@Country", SqlDbType.NVarChar, 100).Value = CStr2(txtCountryAdd.Text)

                CMD.ExecuteNonQuery()

                txtId.Text = ""

            Else
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbUPDATE
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = GridCustomers.GetValue("ID")
                CMD.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = CStr2(txtNameAdd.Text)
                CMD.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = CStr2(txtAddressAdd.Text)
                CMD.Parameters.Add("@Zip", SqlDbType.NVarChar, 100).Value = CStr2(txtZipAdd.Text)
                CMD.Parameters.Add("@State", SqlDbType.NVarChar, 100).Value = CStr2(txtStateAdd.Text)
                CMD.Parameters.Add("@Country", SqlDbType.NVarChar, 100).Value = CStr2(txtCountryAdd.Text)

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

    Private Sub frmCustomer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'frmFordM.btnCancel.PerformClick()
        frmFordM.Enabled = True
        frmFordM.MdiParent = Me.MdiParent
        frmFordM.StartPosition = FormStartPosition.CenterScreen
        frmFordM.Show()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtNameAdd.Text = "" Then
            Mb("Name is mandatory")

        Else
            dbSaveStatus(action)
            fillGridCustomers()

            txtNameAdd.Text = ""
            txtAddressAdd.Text = ""
            txtZipAdd.Text = ""
            txtStateAdd.Text = ""
            txtCountryAdd.Text = ""
        End If


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow
        checkedRows = GridCustomers.GetCheckedRows()
        'Dim query As String = qSKU
        'query = query.Replace("@SKUId", CInt2(GridCustomers.GetValue("Id")))

        'Dim command As New SqlCommand(query, conn)
        'Dim adapter As New SqlDataAdapter(command)
        'Dim table As New DataTable()

        'adapter.Fill(table)
        If checkedRows.Length = 0 Then
            MessageBox.Show("Select 1 Customer to Delete.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim Message As String = "You are going to delete " & checkedRows.Length & " Customer(s)." & vbCrLf & "¿Do you want to continue?"

            'If table.Rows.Count = 0 Then
            If MessageBox.Show(Message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = System.Windows.Forms.DialogResult.Yes Then
                Dim Row As Janus.Windows.GridEX.GridEXRow
                For Each Row In checkedRows
                    dbDeleteCustomer(Row.Cells("Id").Value)
                Next
                fillGridCustomers()
                txtNameAdd.Clear()
                txtAddressAdd.Clear()
                txtZipAdd.Clear()
                txtStateAdd.Clear()
                txtCountryAdd.Clear()
            End If
            'Else
            '    Mb("You have this SKU in an Invoice")

            'End If

        End If
    End Sub

    Private Sub dbDeleteCustomer(ByVal Id As Integer)
        Dim CONN = New SqlConnection(ConnStr)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try
            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction
            CMD.CommandType = CommandType.Text
            CMD.CommandText = "DELETE FROM Customer where Id=" & Id
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
        'frmFordM.FillcmbSKU()
        Me.Close()
    End Sub
    Private Sub GetData()
        If GridCustomers.CurrentRow IsNot Nothing AndAlso GridCustomers.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            txtId.Text = CStr2(GridCustomers.GetValue("Id"))
            txtNameAdd.Text = CStr2(GridCustomers.GetValue("Name"))
            txtAddressAdd.Text = CStr2(GridCustomers.GetValue("Address"))
            txtZipAdd.Text = CStr2(GridCustomers.GetValue("Zip"))
            txtStateAdd.Text = CStr2(GridCustomers.GetValue("State"))
            txtCountryAdd.Text = CStr2(GridCustomers.GetValue("Country"))


        End If
    End Sub

    Private Sub GridCustomers_Click(sender As Object, e As EventArgs) Handles GridCustomers.Click
        If action = ESTR Then GetData()
    End Sub
End Class