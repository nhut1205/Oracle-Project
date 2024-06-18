using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;

namespace DoAn_Net
{
    public partial class CapNhatThongTin : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";
        private string tenDangNhap;

        public CapNhatThongTin(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        private string GetLoggedInUsername()
        {
            string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string userName = GetNameForCurrentUser(tenDangNhap);
                string selectQuery = "SELECT Username FROM Account WHERE Username = :Username";

                using (OracleCommand command = new OracleCommand(selectQuery, connection))
                {
                    command.Parameters.Add(new OracleParameter("Username", userName));
                    return (string)command.ExecuteScalar();
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
                    command.Parameters.Add(new OracleParameter("UserName", tenDangNhap));
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tenDangNhap = reader["Username"].ToString();
                    }
                }

                connection.Close();
            }

            return tenDangNhap;
        }

        private void CapNhatThongTin_Load(object sender, EventArgs e)
        {
            label1.Text = tenDangNhap;

            // Lấy thông tin người dùng từ bảng UserDetails
            UserDetails userDetails = GetUserDetails(tenDangNhap);

            // Hiển thị thông tin lên giao diện người dùng
            if (userDetails != null)
            {
                txt_Address.Text = userDetails.Address;
                textBoxCMND.Text = userDetails.CMND;
                textPhone.Text = userDetails.PhoneNumber;
            }
        }

        public UserDetails GetUserDetails(string username)
        {
            UserDetails userDetails = null;
            string query = "SELECT Address, CMND, PhoneNumber FROM UserDetails WHERE Username = :Username";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Username", username));
                    connection.Open();

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDetails = new UserDetails
                            {
                                Address = reader["Address"].ToString(),
                                CMND = reader["CMND"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            };
                        }
                    }
                }
            }

            return userDetails;
        }

        private void btn_XacNhan_Click_1(object sender, EventArgs e)
        {
            string diaChi = txt_Address.Text;
            string cmnd = textBoxCMND.Text;
            string phone = textPhone.Text;

            if (cmnd.Length != 12 || phone.Length != 10)
            {
                MessageBox.Show("Định dạng số CMND hoặc số điện thoại không đúng");
            }
            else
            {
                string username = GetLoggedInUsername(); // Lấy tên tài khoản từ cơ sở dữ liệu

                if (!string.IsNullOrEmpty(username))
                {
                    if (UpdateInfor(username, diaChi, cmnd, phone))
                    {
                        MessageBox.Show("Cập nhật thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Sau khi cập nhật thành công, bạn có thể thực hiện các hành động khác ở đây.
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool UpdateInfor(string username, string address, string cmnd, string phone)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string checkIfExistsQuery = "SELECT COUNT(*) FROM UserDetails WHERE Username = :Username";
                using (OracleCommand checkIfExistsCommand = new OracleCommand(checkIfExistsQuery, connection))
                {
                    checkIfExistsCommand.Parameters.Add(new OracleParameter("UserName", username));
                    decimal countDecimal = (decimal)checkIfExistsCommand.ExecuteScalar();
                    int count = Convert.ToInt32(countDecimal);

                    if (count > 0)
                    {
                        string updateQuery = "UPDATE UserDetails SET Address = :NewAddress, CMND = :NewCMND, PhoneNumber = :NewPhoneNumber WHERE Username = :Username";
                        using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.Add(new OracleParameter("NewAddress", address));
                            updateCommand.Parameters.Add(new OracleParameter("NewCMND", cmnd));
                            updateCommand.Parameters.Add(new OracleParameter("NewPhoneNumber", phone));
                            updateCommand.Parameters.Add(new OracleParameter("Username", username));

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO UserDetails (Username, Address, CMND, PhoneNumber) VALUES (:Username, :Address, :CMND, :PhoneNumber)";
                        using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.Add(new OracleParameter("Username", username));
                            insertCommand.Parameters.Add(new OracleParameter("Address", address));
                            insertCommand.Parameters.Add(new OracleParameter("CMND", cmnd));
                            insertCommand.Parameters.Add(new OracleParameter("PhoneNumber", phone));

                            int rowsAffected = insertCommand.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
            }
        }

        private void textBoxValidateNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
