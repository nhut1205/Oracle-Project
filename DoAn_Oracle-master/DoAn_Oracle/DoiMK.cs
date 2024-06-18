using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DoAn_Net
{
    public partial class DoiMK : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";
        private string tenDangNhap;
        public DoiMK(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string newPassword = txt_MatKhauMoi.Text;
            string confirmPassword = txt_NhapLai.Text; // Lấy giá trị từ trường nhập liệu "Nhập lại mật khẩu"
            string oldPassword = txtMatKhauCu.Text; // Lấy giá trị từ trường nhập liệu "Mật khẩu cũ"

            // Kiểm tra xem mật khẩu mới và nhập lại mật khẩu có khớp nhau không
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string username = GetLoggedInUsername(); // Lấy tên tài khoản từ cơ sở dữ liệu
                if (!string.IsNullOrEmpty(username))
                {
                    // Kiểm tra xem mật khẩu cũ có đúng không
                    if (CheckOldPassword(username, oldPassword))
                    {
                        if (UpdatePassword(username, newPassword))
                        {
                            MessageBox.Show("Cập nhật mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi cập nhật thành công, bạn có thể thực hiện các hành động khác ở đây.
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật mật khẩu không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định tên tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Xóa các trường nhập liệu
            txt_MatKhauMoi.Clear();
            txt_NhapLai.Clear();
            txtMatKhauCu.Clear();
        }



        public bool UpdatePassword(string username, string newPassword)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu SQL Server
            string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Tạo câu lệnh SQL để cập nhật mật khẩu
                string updateQuery = "UPDATE Account SET Pass = :NewPassword WHERE Username = :Username";

                using (OracleCommand command = new OracleCommand(updateQuery, connection))
                {
                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.Add(new OracleParameter("NewPassword", newPassword));
                    command.Parameters.Add(new OracleParameter("Username", username));


                    // Thực thi câu lệnh SQL
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có dòng nào bị ảnh hưởng không
                    return rowsAffected > 0;
                }
            }
        }

        public string GetNameForCurrentUser(string tenDangNhap)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username FROM Account WHERE Username = :UserName";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Username", tenDangNhap));// Sử dụng tham số tenDangNhap
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Lấy tên đăng nhập từ cơ sở dữ liệu và gán cho biến tenTaiKhoan
                        tenDangNhap = reader["Username"].ToString();
                    }
                }

                connection.Close();
            }

            return tenDangNhap;
        }

        private string GetLoggedInUsername()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu SQL Server
            string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Điều kiện để xác định tên tài khoản đang đăng nhập.
                string userName = GetNameForCurrentUser(tenDangNhap);
                string selectQuery = "SELECT Username FROM Account WHERE Username = :Username";

                using (OracleCommand command = new OracleCommand(selectQuery, connection))
                {
                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.Add(new OracleParameter("Username", userName));


                    // Thực thi câu lệnh SQL và trả về tên tài khoản
                    return (string)command.ExecuteScalar();
                }
            }
        }

        //Kiểm tra mật khẩu cũ 
        private bool CheckOldPassword(string username, string oldPassword)
        {

            string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();


                string checkQuery = "SELECT COUNT(*) FROM Account WHERE Username = :Username AND Pass = :OldPassword";

                using (OracleCommand command = new OracleCommand(checkQuery, connection))
                {
                    command.Parameters.Add(new OracleParameter("Username", username));
                    command.Parameters.Add(new OracleParameter("OldPassword", oldPassword));

                    decimal countDecimal = (decimal)command.ExecuteScalar();
                    int result = Convert.ToInt32(countDecimal);

                    return result > 0;
                }
            }
        }
    }
}
