using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_Net
{
    public partial class AddCategoryForm : Form
    {
        string connectionString = @"Data Source=localhost:1521/orcl;User ID=minhtri;Password=123;";
        int ID = 0;

        DatabaseHelper dbHelper;
        OracleDataAdapter adapter;
        DataTable dataTable;
        OracleConnection connection;

        public AddCategoryForm()
        {
            InitializeComponent();
            this.dbHelper = new DatabaseHelper(connectionString);
            connection = new OracleConnection(connectionString);
        }

        void LoadDataToDataGridView()
        {
            string query = "SELECT * FROM Categories";

            try
            {
                adapter = new OracleDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataColumn[] key = new DataColumn[1];
                key[0] = dataTable.Columns[0];
                dataTable.PrimaryKey = key;

                dataGridView_Category.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        // Hàm tạo mã CategoryId ngẫu nhiên
        private string GenerateRandomCategoryId()
        {
            Random random = new Random();
            const string chars = "0123456789";
            string categoryId = "Cat" + new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return categoryId;
        }


        private void AddCategoryForm_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }

        private void dataGridView_Category_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxCategoryId.Text = dataGridView_Category.Rows[e.RowIndex].Cells["CategoryId"].Value.ToString();
            textBoxCategoryName.Text = dataGridView_Category.Rows[e.RowIndex].Cells["Name"].Value.ToString();
        }


        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }

        private void textBoxSearchProduct_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string searchText = textBoxSearchProduct.Text;

            string query = "SELECT * FROM Categories WHERE Name LIKE " + "'%" + searchText + "%'";
            DataTable dataTable_textBoxSearchProduct = dbHelper.GetDataTable(query);

            // Liên kết DataTable với DataGridView để hiển thị kết quả tìm kiếm
            dataGridView_Category.DataSource = dataTable_textBoxSearchProduct;
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox
            string searchText = textBoxSearchProduct.Text;

            string query = "SELECT * FROM Categories WHERE Name LIKE " + "'%" + searchText + "%'";
            DataTable dataTable_textBoxSearchProduct = dbHelper.GetDataTable(query);

            // Liên kết DataTable với DataGridView để hiển thị kết quả tìm kiếm
            dataGridView_Category.DataSource = dataTable_textBoxSearchProduct;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string categoryName = textBoxCategoryName.Text;
            // Tạo mã categoryId ngẫu nhiên
            string categoryID = GenerateRandomCategoryId();

            DataRow newrow = dataTable.NewRow();

            newrow["CategoryId"] = categoryID;
            newrow["Name"] = categoryName;

            dataTable.Rows.Add(newrow);
            int kq = dbHelper.UpdataDatabase(adapter, dataTable);

            if (kq > 0)
            {
                MessageBox.Show("Thêm loại sản phẩm thành công.");
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm loại sản phẩm.");
            }

            LoadDataToDataGridView();
        }

        //private void buttonXoa_Click(object sender, EventArgs e)
        //{
        //    string categoryId = textBoxCategoryId.Text;

        //    string checkQuery = "SELECT COUNT(*) FROM Products WHERE CategoryId = :CategoryId";
        //    OracleParameter parameter = new OracleParameter("@CategoryId", OracleDbType.VarChar);
        //    parameter.Value = categoryId;

        //    int kq_checkQuery = (int)dbHelper.GetScalar(checkQuery, parameter);


        //    if (kq_checkQuery > 0)
        //    {
        //        MessageBox.Show("Vi phạm ràng buộc khóa ngoại !");
        //    }
        //    else
        //    {
        //        DataRow dr = dataTable.Rows.Find(categoryId);
        //        // Xoa dong du lieu vua tim duoc
        //        if (dr != null)
        //        {
        //            dr.Delete();
        //        }

        //        int kq = dbHelper.UpdataDatabase(adapter, dataTable);

        //        if (kq > 0)
        //        {
        //            MessageBox.Show("Xóa thành công");
        //            textBoxCategoryId.Clear();
        //            textBoxCategoryName.Clear();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Xóa không thành công !");
        //        }
        //    }

        //    LoadDataToDataGridView();
        //}

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            string categoryId = textBoxCategoryId.Text;

            string checkQuery = "SELECT COUNT(*) FROM Products WHERE CategoryId = :CategoryId";
            OracleParameter parameter = new OracleParameter("@CategoryId", OracleDbType.Varchar2);
            parameter.Value = categoryId;

            decimal countDecimal = (decimal)dbHelper.GetScalar(checkQuery, parameter);
            int kq_checkQuery = Convert.ToInt32(countDecimal);

            if (kq_checkQuery > 0)
            {
                MessageBox.Show("Vi phạm ràng buộc khóa ngoại !");
            }
            else
            {
                DataRow dr = dataTable.Rows.Find(categoryId);
                // Xoa dong du lieu vua tim duoc
                if (dr != null)
                {
                    dr.Delete();
                }

                int kq = dbHelper.UpdataDatabase(adapter, dataTable);

                if (kq > 0)
                {
                    MessageBox.Show("Xóa thành công");
                    textBoxCategoryId.Clear();
                    textBoxCategoryName.Clear();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công !");
                }
            }

            LoadDataToDataGridView();


        }



        private void buttonSua_Click(object sender, EventArgs e)
        {
            DataRow dr = dataTable.Rows.Find(textBoxCategoryId.Text);
            // Hieu chinh thong tin dong tin moi vua tim duoc
            if (dr != null)
            {
                string CategoryName = textBoxCategoryName.Text;

                dr["Name"] = CategoryName;
            }

            int kq = dbHelper.UpdataDatabase(adapter, dataTable);

            if (kq > 0)
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa không thành công !");
            }
        }
    }
}
