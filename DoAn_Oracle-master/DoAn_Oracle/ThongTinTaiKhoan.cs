﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_Net
{
    public partial class ThongTinTaiKhoan : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";
        private string tenDangNhap;

        public ThongTinTaiKhoan(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;

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
                    //command.Parameters.Add("@UserName", tenDangNhap); // Sử dụng tham số tenDangNhap
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

        public string GetNameForCurrentUserDetails(string tenDangNhap)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username FROM UserDetails WHERE Username = @UserName";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("UserName", tenDangNhap));
                    //command.Parameters.Add("@UserName", tenDangNhap); // Sử dụng tham số tenDangNhap
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


        private void button1_Click(object sender, EventArgs e)
        {
            DoiMK mk = new DoiMK(tenDangNhap);
            mk.ShowDialog();
        }

        private void ThongTinTaiKhoan_Load_1(object sender, EventArgs e)
        {
            // Lấy thông tin người đăng nhập từ hàm GetNameForCurrentUser
            string userName = GetNameForCurrentUser(tenDangNhap);

            // Tạo một đối tượng DataHelper
            DataHelper dataHelper = new DataHelper();

            // Gọi hàm GetUserInfo để lấy thông tin người dùng
            Account user = dataHelper.GetUserInfo(userName);

            if (user != null)
            {
                // Hiển thị thông tin lên giao diện người dùng
                txt_TenDangNhap.Text = user.UserName;
                txt_TenHienThi.Text = user.DisPlayName;
                txt_Email.Text = user.Email;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng cho tên đăng nhập: " + userName);
            }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            CapNhatThongTin mk = new CapNhatThongTin(tenDangNhap);
            mk.ShowDialog();
        }

        private void txt_Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_TenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DoiMK mk = new DoiMK(tenDangNhap);
            mk.ShowDialog();
        }

        private void btn_CapNhat_Click_1(object sender, EventArgs e)
        {
            CapNhatThongTin mk = new CapNhatThongTin(tenDangNhap);
            mk.ShowDialog();
        }
    }
}
