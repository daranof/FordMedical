Imports System.Data.SqlClient
Imports Janus.Windows.GridEX
Imports siCom40
Imports siCom40.Com

Public Class frmMeasure
    Dim IsAccept As Boolean = False
    Dim action As String = ESTR
    Dim dtPallet As New DataTable

    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Dim connectionString As String = "Data Source=localhost;Initial Catalog=FordMedical;User ID=sa;Password=Savent01$"
    Dim conn As New SqlConnection(connectionString)

    Private Sub frmPallet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtIdShipping.Text = cstr2(frmFordM.txtId.Text)
        txtLong.Clear()
        txtWidth.Clear()
        txtHight.Clear()
        txtWeight.Clear()
        cmbPallet.SelectedValue = -1
        cmbPallet.Text = ""

        Try
            txtLong.Enabled = True
            txtWidth.Enabled = True
            txtHight.Enabled = True
            txtWeight.Enabled = True
            cmbPallet.Enabled = True
            Dim r As Resizer = New Resizer(Me)
        Catch ex As Exception
            ErrHandler(ex)
        End Try

        dtPallet.Columns.Add("Id")
        dtPallet.Columns.Add("IdShipping")
        dtPallet.Columns.Add("IdPallet")
        dtPallet.Columns.Add("Long")
        dtPallet.Columns.Add("Width")
        dtPallet.Columns.Add("Hight")
        dtPallet.Columns.Add("Weight")

        GridPallet.DataSource = dtPallet

        action = ESTR

        FillcmbPallet()
        fillGridPallet()

        GridPallet.EditMode = EditMode.EditOn
        GridPallet.RootTable.AllowAddNew = InheritableBoolean.False
        GridPallet.RootTable.AllowEdit = InheritableBoolean.True
        GridPallet.RootTable.AllowDelete = InheritableBoolean.True

        txtHight.Focus()

    End Sub

    Private Sub fillGridPallet()

        Dim query As String = "SELECT m.Id, m.IdShipping, m.IdPallet, p.PalletNumber, m.Long, m.Width, m.Hight, m.Weight
                                       From Measure as m
                                       Left Join Pallet as p on m.IdPallet=p.id
                                       where m.IdShipping = @ShippingId
                                       ORDER BY m.IdPallet"

        query = query.Replace("@ShippingId", CInt2(txtIdShipping.Text))
        Dim Table As DataTable = DbFillDataTable(query)

        Dim sig As New siGrid18(GridPallet, Table)

        With GridPallet
            With .RootTable ' RootTable.
                With .Columns("Id")
                    .Visible = False
                End With
                With .Columns("IdShipping")
                    .Visible = False
                End With
                With .Columns("IdPallet")
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
            CMD.CommandText = "[dbo].[usp_Measure]"
            CMD.Parameters.Clear()
            If txtId.Text = "" Then
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbINSERT
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = CInt2(txtId.Text)
                CMD.Parameters.Add("@IdShipping", SqlDbType.Int).Value = CInt2(txtIdShipping.Text)
                CMD.Parameters.Add("@IdPallet", SqlDbType.Int).Value = CInt2(cmbPallet.SelectedValue)
                CMD.Parameters.Add("@Long", SqlDbType.Int).Value = CInt2(txtLong.Text)
                CMD.Parameters.Add("@Width", SqlDbType.Int).Value = CInt2(txtWidth.Text)
                CMD.Parameters.Add("@Hight", SqlDbType.Int).Value = CInt2(txtHight.Text)
                CMD.Parameters.Add("@Weight", SqlDbType.Int).Value = CInt2(txtWeight.Text)
                CMD.ExecuteNonQuery()

                txtId.Text = ""

            Else
                CMD.Parameters.Add("@MODE", SqlDbType.VarChar, 10).Value = dbUPDATE
                CMD.Parameters.Add("@Id", SqlDbType.Int).Value = GridPallet.GetValue("ID")
                CMD.Parameters.Add("@IdShipping", SqlDbType.Int).Value = GridPallet.GetValue("IdShipping")
                CMD.Parameters.Add("@IdPallet", SqlDbType.Int).Value = CInt2(cmbPallet.SelectedValue)
                CMD.Parameters.Add("@Long", SqlDbType.Int).Value = CInt2(txtLong.Text)
                CMD.Parameters.Add("@Width", SqlDbType.Int).Value = CInt2(txtWidth.Text)
                CMD.Parameters.Add("@Hight", SqlDbType.Int).Value = CInt2(txtHight.Text)
                CMD.Parameters.Add("@Weight", SqlDbType.Int).Value = CInt2(txtWeight.Text)

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

    Private Sub frmPallet_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'frmFordM.btnCancel.PerformClick()
        frmFordM.Enabled = True
        frmFordM.MdiParent = Me.MdiParent
        frmFordM.StartPosition = FormStartPosition.CenterScreen
        frmFordM.Show()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim IdPallet As Integer = 0

        If cmbPallet.Text = "" Then
            Mb("Could you select a Pallet Number")
            cmbPallet.Focus()
        Else
            dbSaveStatus(action)
            fillGridPallet()

            txtLong.Text = 40
            txtWidth.Text = 48
            txtHight.Text = ""
            txtWeight.Text = ""
            IdPallet = CInt2(DbExecuteScalar("Select top 1 IdPallet from Measure where Idshipping=" & txtIdShipping.Text & " order by Idpallet desc "))
            cmbPallet.SelectedValue = IdPallet + 1
            'cmbPallet.Text = ""
            txtHight.Focus()
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim checkedRows() As Janus.Windows.GridEX.GridEXRow
        checkedRows = GridPallet.GetCheckedRows()
        If checkedRows.Length = 0 Then
            MessageBox.Show("Select 1 Pallet to Delete.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim Message As String = "You are going to delete " & checkedRows.Length & " Pallet(s)." & vbCrLf & "¿Do you want to continue?"
            If MessageBox.Show(Message, AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = System.Windows.Forms.DialogResult.Yes Then
                Dim Row As Janus.Windows.GridEX.GridEXRow
                For Each Row In checkedRows
                    dbDeleteMeasure(Row.Cells("Id").Value)
                Next
                fillGridPallet()
                txtLong.Text = ""
                txtWidth.Text = ""
                txtHight.Text = ""
                txtWeight.Text = ""
                cmbPallet.SelectedValue = -1
                cmbPallet.Text = ""
                txtId.Text = ""
            End If
        End If
    End Sub

    Private Sub dbDeleteMeasure(ByVal Id As Integer)
        Dim CONN = New SqlConnection(ConnStr)
        Dim CMD = New SqlCommand
        Dim transaction As SqlTransaction = Nothing
        Try
            CONN.Open()
            transaction = CONN.BeginTransaction
            CMD.Connection = CONN
            CMD.Transaction = transaction
            CMD.CommandType = CommandType.Text
            CMD.CommandText = "DELETE FROM Measure WHERE Id=" & Id
            CMD.ExecuteNonQuery()
            transaction.Commit()
        Catch ex As Exception
            ErrHandler(ex)
        Finally
            CONN.Close()
            CONN.Dispose()
            CMD.Dispose()
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'frmFordM.FillcmbSKU()
        Me.Close()
    End Sub
    Private Sub GetData()
        If GridPallet.CurrentRow IsNot Nothing AndAlso GridPallet.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then

            txtId.Text = CStr2(GridPallet.GetValue("Id"))
            txtIdShipping.Text = CStr2(GridPallet.GetValue("IdShipping"))
            txtLong.Text = CStr2(GridPallet.GetValue("Long"))
            txtWidth.Text = CStr2(GridPallet.GetValue("Width"))
            txtHight.Text = CStr2(GridPallet.GetValue("Hight"))
            txtWeight.Text = CStr2(GridPallet.GetValue("Weight"))
            cmbPallet.SelectedValue = CInt2(GridPallet.GetValue("IdPallet"))
            cmbPallet.Text = CStr2(GridPallet.GetValue("PalletNumber"))

        End If
    End Sub

    Private Sub GridPallet_Click(sender As Object, e As EventArgs) Handles GridPallet.Click
        If action = ESTR Then GetData()
    End Sub

    Public Sub FillcmbPallet()
        Dim dtCmbPallet As New DataTable

        Try
            Dim query As String = qCmbPalletMeasures
            query = query.Replace("@ShippingId", CInt2(txtIdShipping.Text))
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

        txtLong.Text = 40
        txtWidth.Text = 48
        txtHight.Focus()
    End Sub

    Private Sub txtLong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLong.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        '    'e.KeyChar = IsNumeric(Keys.Back)
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub

    Private Sub txtWidth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWidth.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub

    Private Sub txtHight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHight.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub

    Private Sub txtWeight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWeight.KeyPress
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True
        'End If
        e.Handled = ("0123456789.-" & ControlChars.Back).IndexOf(e.KeyChar) = -1
    End Sub
End Class