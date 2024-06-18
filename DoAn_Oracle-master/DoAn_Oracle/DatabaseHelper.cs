using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DoAn_Net
{
    public class DatabaseHelper
    {
        private string connectionString; // Chuỗi kết nối đến cơ sở dữ liệu

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetDataTable(string query)
        {
            // Tạo một đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
            OracleDataAdapter adapter;
            DataTable dataTable;
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Tạo một đối tượng SqlCommand để thực thi truy vấn
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    adapter = new OracleDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public object GetScalar(string sql, OracleParameter parameter)
        {
            object kq = new object();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Tạo một đối tượng SqlCommand để thực thi truy vấn
                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    // Thêm tham số vào câu truy vấn
                    command.Parameters.Add(parameter);

                    kq = command.ExecuteScalar();
                }
            }
            return kq;
        }


        public List<string> GetCategoryNames()
        {
            List<string> categoryNames = new List<string>();

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn SQL để lấy danh sách category names từ bảng Category
                    string query = "SELECT Name FROM Categories";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryNames.Add(reader["Name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi khi lấy danh sách category names: " + ex.Message);
            }

            return categoryNames;
        }

        // Hàm lấy danh sách tên của tất cả các Vendor từ cơ sở dữ liệu
        public List<string> GetVendorNames()
        {
            List<string> vendorNames = new List<string>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Name FROM Vendors";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string vendorName = reader["Name"].ToString();
                            vendorNames.Add(vendorName);
                        }
                    }
                }
            }

            return vendorNames;
        }

        //// Hàm lấy VendorId từ VendorName
        private int GetVendorIdByName(string vendorName)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT VendorId FROM Vendors WHERE Name = @VendorName";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@VendorName", vendorName);
                    command.Parameters.Add("@VendorName", vendorName);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            // Trả về -1 nếu không tìm thấy VendorName
            return -1;
        }

        public int GetProductIdByName(string productName)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ProductId FROM Products WHERE Name = @ProductName";
                    OracleCommand command = new OracleCommand(query, connection);
                    command.Parameters.Add(new OracleParameter("ProductName", productName));
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ném hoặc ghi vào log)
                Console.WriteLine("Lỗi khi lấy ProductId từ tên sản phẩm: " + ex.Message);
            }

            // Trả về giá trị mặc định (vd: -1) hoặc giá trị tùy chọn để chỉ ra rằng không tìm thấy sản phẩm.
            return -1;
        }


        public bool AddProduct(string productName, decimal productPrice, int productQuantity, string categoryName, string vendorName, string description, int guarantee, bool IsLatestProduct, bool IsTrendingProduct, bool IsSpecialProduct, int DiscountPercent)
        {
            try
            {
                // Lấy VendorId từ VendorName
                int vendorId = GetVendorIdByName(vendorName);

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn SQL để thêm sản phẩm vào bảng Products
                    string query = "INSERT INTO Products (Name, Price, Quantity, CategoryId, VendorId, Description, Guarantee, IsLatestProduct, IsTrendingProduct, IsSpecialProduct, DiscountPercent) " +
                                   "VALUES (@Name, @Price, @Quantity, (SELECT CategoryId FROM Categories WHERE Name = @CategoryName), @VendorId, @Description, @Guarantee, @IsLatestProduct, @IsTrendingProduct, @IsSpecialProduct, @DiscountPercent)";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("Name", productName));
                        command.Parameters.Add(new OracleParameter("Price", productPrice));
                        command.Parameters.Add(new OracleParameter("Quantily", productQuantity));
                        command.Parameters.Add(new OracleParameter("CategoryName", categoryName));
                        // Thêm các tham số cho các cột khác tương ứng

                        command.Parameters.Add(new OracleParameter("VendorId", vendorId));
                        command.Parameters.Add(new OracleParameter("Description", description));
                        command.Parameters.Add(new OracleParameter("Guarantee", guarantee));
                        command.Parameters.Add(new OracleParameter("IsLatestProduct", IsLatestProduct));

                        command.Parameters.Add(new OracleParameter("IsTendingProduct", IsTrendingProduct));
                        command.Parameters.Add(new OracleParameter("IsSpecialProduct", IsSpecialProduct));
                        command.Parameters.Add(new OracleParameter("DiscountPercent", DiscountPercent));
  

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi khi thêm sản phẩm: " + ex.Message);
                return false;
            }
        }

        public bool AddImageUpload(string ProductName, string FilePath)
        {
            try
            {
                int ProductId = GetProductIdByName(ProductName);

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn SQL để thêm sản phẩm vào bảng Products
                    string query = "INSERT INTO Images (ProductId, FilePath) " +
                                   "VALUES (@ProductId, @FilePath)";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {

                        command.Parameters.Add(new OracleParameter("ProductId", ProductId));
                        command.Parameters.Add(new OracleParameter("FilePath", FilePath));


                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi khi thêm đường dẫn hình ảnh: " + ex.Message);
                return false;
            }
        }

        public bool AddCategory(string categoryName)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn SQL để thêm category mới vào bảng Category
                    string query = "INSERT INTO Categories (Name) VALUES (@CategoryName)";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("CategoryName", categoryName));

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi khi thêm category: " + ex.Message);
                return false;
            }
        }

        // Hàm thêm Vendor mới vào cơ sở dữ liệu và trả về kết quả thành công hay không
        public bool AddVendor(string vendorName)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Vendors (Name) VALUES (@VendorName)";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("VendorName", vendorName));

                        command.ExecuteNonQuery();
                    }
                }

                // Nếu thêm Vendor thành công, trả về true
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi khi thêm Vendor: " + ex.Message);
                return false;
            }
        }

        public bool CategoryExists(string categoryName)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn SQL để kiểm tra xem category đã tồn tại hay chưa
                    string query = "SELECT COUNT(*) FROM Categories WHERE Name = @CategoryName";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter("CategoryName", categoryName));

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi khi kiểm tra category tồn tại: " + ex.Message);
                return false;
            }
        }

        // Hàm kiểm tra sự tồn tại của Vendor trong cơ sở dữ liệu
        public bool VendorExists(string vendorName)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Vendors WHERE Name = @VendorName";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("VendorName", vendorName));
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        // Hàm kiểm tra sự tồn tại của Discount trong cơ sở dữ liệu
        public bool DiscountExists(string discountPercent)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Discounts WHERE Percent = @DiscountPercent";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("DiscountPercent", discountPercent));
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public int UpdataDatabase(OracleDataAdapter da, DataTable ds)
        {
            // Cap nhat trong CSDL
            OracleCommandBuilder cB = new OracleCommandBuilder(da);

            return da.Update(ds);

        }

 


    }

}
