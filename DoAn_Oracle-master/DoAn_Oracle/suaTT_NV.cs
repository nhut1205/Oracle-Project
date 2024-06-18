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
using Oracle.ManagedDataAccess.Client;

namespace DoAn_Net
{
    public partial class suaTT_NV : Form
    {
        public suaTT_NV(string tenDangNhap, string tenHienThi, string email, string quyen)
        {
            InitializeComponent();
            txt_TenDangNhap.Text = tenDangNhap;
            txt_TenHienThi.Text = tenHienThi;
            textBtxt_Email.Text = email;
            cbo_Quyen.SelectedItem = quyen;
        }

        private void suaTT_NV_Load(object sender, EventArgs e)
        {
            load_IsAdmin();
        }

        //private void btn_Luu_Click(object sender, EventArgs e)
        //{
        //    // Lấy giá trị đã chỉnh sửa từ các điều khiển trên form
        //    string tenDangNhapMoi = txt_TenDangNhap.Text;
        //    string tenHienThiMoi = txt_TenHienThi.Text;
        //    string emailMoi = textBtxt_Email.Text;
        //    string quyenMoi = cbo_Quyen.SelectedValue.ToString();

        //    // Chuỗi kết nối đến cơ sở dữ liệu
        //    string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

        //    using (OracleConnection connection = new OracleConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Chuẩn bị câu lệnh SQL UPDATE
        //        string updateQuery = "UPDATE Account SET Displayname = :Displayname, Email = :Email, IsAdminID = :IsAdminID WHERE Username = :Username";

        //        using (OracleCommand command = new OracleCommand(updateQuery, connection))
        //        {
        //            // Thay thế các tham số trong câu lệnh SQL với giá trị đã chỉnh sửa
        //            command.Parameters.Add(new OracleParameter("Username", tenDangNhapMoi));
        //            command.Parameters.Add(new OracleParameter("Displayname", tenHienThiMoi));
        //            command.Parameters.Add(new OracleParameter("Email", emailMoi));
        //            command.Parameters.Add(new OracleParameter("IsAdminID", quyenMoi));


        //            // Thực hiện câu lệnh SQL UPDATE
        //            /*ecimal countDecimal = (decimal)command.ExecuteScalar();*/
        //            int rowsAffected = Convert.ToInt32(command.ExecuteNonQuery());
        //            //int rowsAffected = command.ExecuteNonQuery();

        //            // Kiểm tra xem có bản ghi nào bị cập nhật không
        //            if (rowsAffected > 0)
        //            {
        //                MessageBox.Show("Thông tin đã được cập nhật.");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Không thể cập nhật thông tin.");
        //            }
        //        }
        //    }

        //    // Đóng form sau khi lưu thông tin
        //    this.Close();
        //}


        //private void btn_Luu_Click(object sender, EventArgs e)
        //{
        //    // Lấy giá trị đã chỉnh sửa từ các điều khiển trên form
        //    string tenDangNhapMoi = txt_TenDangNhap.Text;
        //    string tenHienThiMoi = txt_TenHienThi.Text;
        //    string emailMoi = textBtxt_Email.Text;
        //    string quyenMoi = cbo_Quyen.SelectedValue.ToString();
        //    //int quyenMoi = Convert.ToInt32(cbo_Quyen.SelectedValue); // Chuyển đổi giá trị quyền sang kiểu int

        //    // In ra các giá trị để kiểm tra
        //    Console.WriteLine($"Username: {tenDangNhapMoi}, Displayname: {tenHienThiMoi}, Email: {emailMoi}, IsAdminID: {quyenMoi}");

        //    // Chuỗi kết nối đến cơ sở dữ liệu
        //    string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

        //    try
        //    {
        //        using (OracleConnection connection = new OracleConnection(connectionString))
        //        {
        //            connection.Open();

        //            // Chuẩn bị câu lệnh SQL UPDATE
        //            string updateQuery = "UPDATE Account SET Displayname = :Displayname, Email = :Email, IsAdminID = :IsAdminID WHERE Username = :Username";

        //            using (OracleCommand command = new OracleCommand(updateQuery, connection))
        //            {
        //                // Thay thế các tham số trong câu lệnh SQL với giá trị đã chỉnh sửa
        //                command.Parameters.Add("Username", OracleDbType.Varchar2).Value = tenDangNhapMoi;
        //                command.Parameters.Add("Displayname", OracleDbType.Varchar2).Value = tenHienThiMoi;
        //                command.Parameters.Add("Email", OracleDbType.Varchar2).Value = emailMoi;
        //                command.Parameters.Add("IsAdminID", OracleDbType.Int32).Value = quyenMoi;

        //                // Thực hiện câu lệnh SQL UPDATE
        //                int rowsAffected = command.ExecuteNonQuery();

        //                // Kiểm tra xem có bản ghi nào bị cập nhật không
        //                if (rowsAffected > 0)
        //                {
        //                    MessageBox.Show("Thông tin đã được cập nhật.");
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Không thể cập nhật thông tin. Vui lòng kiểm tra lại tài khoản.");
        //                }
        //            }
        //        }
        //    }
        //    catch (OracleException ex)
        //    {
        //        MessageBox.Show("Lỗi xảy ra trong quá trình cập nhật: " + ex.Message + "\nSố lỗi: " + ex.Number);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi không xác định xảy ra: " + ex.Message);
        //    }

        //    // Đóng form sau khi lưu thông tin
        //    this.Close();
        //}


        private void btn_Luu_Click(object sender, EventArgs e)
        {
            // Lấy giá trị đã chỉnh sửa từ các điều khiển trên form
            string tenDangNhapMoi = txt_TenDangNhap.Text;
            string tenHienThiMoi = txt_TenHienThi.Text;
            string emailMoi = textBtxt_Email.Text;
            string quyenMoi = cbo_Quyen.SelectedValue.ToString(); // Assuming quyenMoi is already a string

            // In ra các giá trị để kiểm tra
            Console.WriteLine($"Username: {tenDangNhapMoi}, Displayname: {tenHienThiMoi}, Email: {emailMoi}, IsAdminID: {quyenMoi}");

            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Chuẩn bị câu lệnh SQL UPDATE
                    string updateQuery = "UPDATE Account SET Displayname = :Displayname, Email = :Email, IsAdminID = :IsAdminID WHERE Username = :Username";

                    using (OracleCommand command = new OracleCommand(updateQuery, connection))
                    {
                        // Thay thế các tham số trong câu lệnh SQL với giá trị đã chỉnh sửa
                        command.Parameters.Add("Username", OracleDbType.Varchar2).Value = tenDangNhapMoi;
                        command.Parameters.Add("Displayname", OracleDbType.Varchar2).Value = tenHienThiMoi;
                        command.Parameters.Add("Email", OracleDbType.Varchar2).Value = emailMoi;
                        command.Parameters.Add("IsAdminID", OracleDbType.Varchar2).Value = quyenMoi; // No need to convert quyenMoi

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị cập nhật không
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thông tin đã được cập nhật.");
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật thông tin. Vui lòng kiểm tra lại tài khoản.");
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi xảy ra trong quá trình cập nhật: " + ex.Message + "\nSố lỗi: " + ex.Number);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định xảy ra: " + ex.Message);
            }

            // Đóng form sau khi lưu thông tin
            this.Close();
        }



        OracleConnection conn;
        string str_Connection = "Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

        public DataTable getDatatable(string sql)
        {
            conn = new OracleConnection(str_Connection);
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(sql, str_Connection);
            da.Fill(dt);
            return dt;
        }

        public void load_IsAdmin()
        {
            string sql = "select * from IsAdmin";
            DataTable dt_sanPham = getDatatable(sql);
            cbo_Quyen.DataSource = dt_sanPham;
            cbo_Quyen.ValueMember = "IsAdminID";
            cbo_Quyen.DisplayMember = "NameA";
        }
    }
}
