Imports System.Data.SqlClient
Imports Janus.Windows.GridEX
Imports siCom40
Imports siCom40.Com

Public Class frmSKU
    Dim IsAccept As Boolean = False
    Dim action As String = ESTR
    Dim dtSKU As New DataTable

    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Dim connectionString As String = "Data Source=localhost;Initial Catalog=FordMedical;User ID=sa;Password=Savent01$"
    Dim conn As New SqlConnection(connectionString)

    Private Sub frmSKU_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtSKUAdd.Clear()
        txtNameAdd.Clear()

        Try
            txtSKUAdd.Enabled = True
            txtNameAdd.Enabled = True
            Dim r As Resizer = New Resizer(Me)
        Catch ex As Exception
            ErrHandler(ex)
        End Try

        dtSKU.Columns.Add("Id")
        dtSKU.Columns.Add("SKU")
        dtSKU.Columns.Add("Name")

        GridSKU.DataSource = dtSKU

        action = ESTR

        fillGridSKU()

        GridSKU.EditMode = EditMode.EditOn
        GridSKU.RootTable.AllowAddNew = InheritableBoolean.False
        GridSKU.RootTable.AllowEdit = InheritableBoolean.True
        GridSKU.RootTable.AllowDelete = InheritableBoolean.True

        txtSKUAdd.Focus()
    End Sub

    Private Sub fillGridSKU()

        Dim query As String = "SELECT s.SKU, s.Name, s.Id
                                       From SKU as s									  
                                       ORDER BY s.SKU"

        Dim Table As DataTable = DbFillDataTable(query)

        Dim sig As New siGrid18(GridSKU, Table)

        With GridSKU
            With .RootTable ' RootTable.
                With .Columns("Id")
                    .Visible = False
                End With
                With .Columns("SKU")
                    .AutoSize()
                End With
                With .Columns("Name")
                    .AutoSize()
                End With
            End With
        End With
        txtSKUAdd.Focus()
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
            CMD.CommandText = "[dbo].[usp_SKU]"
            CMD.Parameters.Clear()
            If txtId.Text = "" Then
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbINSERT
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.Parameters.Add("@SKU", SqlDbType.NVarChar, 50).Value = CStr2(txtSKUAdd.Text)
                CMD.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = CStr2(txtNameAdd.Text)
                CMD.ExecuteNonQuery()

            Else
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbUPDATE
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = GridSKU.GetValue("ID")
                CMD.Parameters.Add("@SKU", SqlDbType.NVarChar, 50).Value = CStr2(txtSKUAdd.Text)
                CMD.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = CStr2(txtNameAdd.Text)
                CMD.ExecuteNonQuery()

            End If
            txtId.Text = ""
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

    Private Sub frmSKU_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'frmFordM.btnCancel.PerformClick()
        frmFordM.Enabled = True
        frmFordM.MdiParent = Me.MdiParent
        frmFordM.StartPosition = FormStartPosition.CenterScreen
        frmFordM.Show()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim Notcopy As Boolean = False

        If txtSKUAdd.Text = "" Then
            Mb("SKU is mandatory")
        Else

            For Each grx As GridEXRow In GridSKU.GetDataRows

                If CStr2(txtSKUAdd.Text) = CStr2(grx.Cells("SKU").Value) Then
                    Notcopy = True
                End If

            Next
            If Notcopy = False Then
                dbSaveStatus(action)
                fillGridSKU()
                txtSKUAdd.Text = ""
                txtNameAdd.Text = ""
            Else
                Mb("This SKU already exist")

            End If


        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow
        checkedRows = GridSKU.GetCheckedRows()
        Dim query As String = qSKU
        query = query.Replace("@SKUId", CInt2(GridSKU.GetValue("Id")))

        Dim command As New SqlCommand(query, conn)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)
        If checkedRows.Length = 0 Then
            MessageBox.Show("Select 1 SKU to Delete.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim Message As String = "You are going to delete " & checkedRows.Length & " SKU(s)." & vbCrLf & "¿Do you want to continue?"

            If table.Rows.Count = 0 Then
                If MessageBox.Show(Message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = System.Windows.Forms.DialogResult.Yes Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow
                    For Each Row In checkedRows
                        dbDeleteSKU(Row.Cells("Id").Value)
                    Next
                    fillGridSKU()
                    txtSKUAdd.Clear()
                    txtNameAdd.Clear()
                End If
            Else
                Mb("You have this SKU in an Invoice")

            End If

        End If
    End Sub

    Private Sub dbDeleteSKU(ByVal Id As Integer)
        Dim CONN = New SqlConnection(ConnStr)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try
            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction
            CMD.CommandType = CommandType.Text
            CMD.CommandText = "DELETE FROM SKU WHERE Id=" & Id
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
        If GridSKU.CurrentRow IsNot Nothing AndAlso GridSKU.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            txtId.Text = CStr2(GridSKU.GetValue("Id"))
            txtSKUAdd.Text = CStr2(GridSKU.GetValue("SKU"))
            txtNameAdd.Text = CStr2(GridSKU.GetValue("Name"))

        End If
    End Sub

    Private Sub GridSKU_Click(sender As Object, e As EventArgs) Handles GridSKU.Click
        If action = ESTR Then GetData()
    End Sub

End Class