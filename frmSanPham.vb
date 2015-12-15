Public Class frmSanPham
    Private _DBAccess As New DataBaseAccess

    Private _isEdit As Boolean = False
    Public Sub New(IsEdit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _isEdit = IsEdit
    End Sub

    Private Function InsertSanPham() As Boolean
        Dim sqlQuery As String = "INSERT INTO SanPham (MaSP, TenSP, SoLuong, MoTa, MaLSP) "
        sqlQuery += String.Format("VALUES ('{0}','{1}','{2}','{3}','{4}')", _
                                  txtMaSP.Text, txtTenSP.Text, txtSoLuong.Text, txtMoTa.Text, txtMaLSP.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    Private Function UpdateSanPham() As Boolean
        Dim sqlQuery As String = String.Format("UPDATE SanPham SET TenSP = '{0}', SoLuong = '{1}', MoTa = '{2}' WHERE MaSP = '{3}'", _
                                               Me.txtTenSP.Text, Me.txtSoLuong.Text, Me.txtMoTa.Text, Me.txtMaSP.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    Private Function IsEmpty() As Boolean
        Return (String.IsNullOrEmpty(txtMaSP.Text) OrElse String.IsNullOrEmpty(txtTenSP.Text) OrElse _
            String.IsNullOrEmpty(txtSoLuong.Text) OrElse String.IsNullOrEmpty(txtMoTa.Text) OrElse _
            String.IsNullOrEmpty(txtMaLSP.Text))
    End Function

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles txtMaLSP.TextChanged

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If IsEmpty() Then
            MessageBox.Show("Nhap gia tri vao truoc khi ghi vao database", "Error", MessageBoxButtons.OK)
        Else
            If _isEdit Then
                If UpdateSanPham() Then
                    MessageBox.Show("SUA THANH CONG", "infomation", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    MessageBox.Show("Loi sua du lieu", "Eroor", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            Else
                If InsertSanPham() Then
                    MessageBox.Show("Them du lieu thanh cong", "Infomation", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Them du lieu that bai", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            End If

            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


End Class