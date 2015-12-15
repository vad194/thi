Public Class frmQLSP
    Private _DBAccess As New DataBaseAccess
    Private _isLoading As Boolean = False

    Private Sub LoadDataOnComBobox()
        Dim sqlQuery As String = "SELECT MaLSP, TenLSP FROM dbo.LoaiSanPham"
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.cmbLoaiSP.DataSource = dTable
        Me.cmbLoaiSP.ValueMember = "MaLSP"
        Me.cmbLoaiSP.DisplayMember = "TenLSP"
    End Sub

    Private Sub LoadDataOnGridView(MaLSP As String)
        Dim sqlQuery As String = _
            String.Format("SELECT MaSP, TenSP, SoLuong, MoTa FROM dbo.SanPham WHERE MaLSP = '{0}'", MaLSP)
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvSanPham.DataSource = dTable
        With Me.dgvSanPham
            .Columns(0).HeaderText = "MaSP"
            .Columns(1).HeaderText = "TenSP"
            .Columns(2).HeaderText = "SoLuong"
            .Columns(3).HeaderText = "MoTa"
            .Columns(3).Width = 200
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lblSearch_Click(sender As Object, e As EventArgs) Handles lblSearch.Click, Label1.Click

    End Sub

    Private Sub frmQLSP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isLoading = True


        LoadDataOnComBobox()
        LoadDataOnGridView(Me.cmbLoaiSP.SelectedValue)

        _isLoading = False
    End Sub

    Private Sub cmbLoaiSP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLoaiSP.SelectedIndexChanged
        If Not _isLoading Then
            LoadDataOnGridView(Me.cmbLoaiSP.SelectedValue)
        End If

    End Sub

    Private Sub SearchSanPham(MaLSP As String, value As String)
        Dim sqlQuery As String = _
            String.Format("SELECT MaSP, TenSP, SoLuong, MoTa FROM dbo.SanPham WHERE MaLSP = '{0}'", MaLSP)
        If Me.cmbSearch.SelectedIndex = 0 Then
            sqlQuery += String.Format(" AND MaSP LIKE '{0}'", value)
        ElseIf Me.cmbSearch.SelectedIndex = 1 Then
            sqlQuery += String.Format(" AND TenSP LIKE '{0}'", value)
        End If
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvSanPham.DataSource = dTable
        With Me.dgvSanPham
            .Columns(0).HeaderText = "MaSP"
            .Columns(1).HeaderText = "TenSP"
            .Columns(2).HeaderText = "SoLuong"
            .Columns(3).HeaderText = "MoTa"
            .Columns(3).Width = 200
        End With
    End Sub



    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SearchSanPham(Me.cmbLoaiSP.SelectedValue, Me.txtSearch.Text)
        If txtSearch.Text = "" Then
            LoadDataOnComBobox()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New frmSanPham(False)
        frm.txtMaLSP.Text = Me.cmbLoaiSP.SelectedValue
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            LoadDataOnGridView(Me.cmbLoaiSP.SelectedValue)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim frm As New frmSanPham(True)
        frm.txtMaLSP.Text = Me.cmbLoaiSP.SelectedValue
        frm.txtMaSP.ReadOnly = True
        With Me.dgvSanPham
            frm.txtMaSP.Text = .Rows(.CurrentCell.RowIndex).Cells("MaSP").Value
            frm.txtTenSP.Text = .Rows(.CurrentCell.RowIndex).Cells("TenSP").Value
            frm.txtSoLuong.Text = .Rows(.CurrentCell.RowIndex).Cells("SoLuong").Value
            frm.txtMoTa.Text = .Rows(.CurrentCell.RowIndex).Cells("MoTa").Value
        End With
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            LoadDataOnGridView(Me.cmbLoaiSP.SelectedValue)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim MaSP As String = Me.dgvSanPham.Rows(Me.dgvSanPham.CurrentCell.RowIndex).Cells("MaSP").Value
        Dim sqlQuery As String = String.Format("DELETE SanPham WHERE MaSP = '{0}'", MaSP)
        If _DBAccess.ExecuteNoneQuery(sqlQuery) Then
            MessageBox.Show("Xoa du lieu thanh cong", "infomation", MessageBoxButtons.OK)
            LoadDataOnGridView(Me.cmbLoaiSP.SelectedValue)
        Else
            MessageBox.Show("Xoa du lieu that bai", "Eroor", MessageBoxButtons.OK)
        End If
    End Sub
End Class
