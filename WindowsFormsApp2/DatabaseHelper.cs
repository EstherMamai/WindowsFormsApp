using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace WindowsFormsApp2
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WindowsFormsApp2.Properties.Settings.TutorialDBConnectionString"]?.ConnectionString
                               ?? throw new InvalidOperationException("Connection string 'SparePartsDB' not found.");
        }

        public DataTable GetSpareParts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM SpareParts";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new ApplicationException("An error occurred while retrieving spare parts.", ex);
            }
        }

        public void AddSparePart(string partName, string partNumber, decimal price)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO SpareParts (PartName, PartNumber, Price) VALUES (@PartName, @PartNumber, @Price)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PartName", partName);
                    cmd.Parameters.AddWithValue("@PartNumber", partNumber);
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new ApplicationException("An error occurred while adding the spare part.", ex);
            }
        }

        public string GetSparePartsAsString()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM SpareParts";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    StringBuilder sb = new StringBuilder();

                    while (reader.Read())
                    {
                        sb.AppendLine($"PartID: {reader["PartID"]}, PartName: {reader["PartName"]}, PartNumber: {reader["PartNumber"]}, Price: {reader["Price"]}");
                    }

                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new ApplicationException("An error occurred while retrieving spare parts as a string.", ex);
            }
        }

        public void UpdateSparePart(int partId, string partName, string partNumber, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE SpareParts SET PartName = @PartName, PartNumber = @PartNumber, Price = @Price WHERE PartID = @PartID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PartName", partName);
                cmd.Parameters.AddWithValue("@PartNumber", partNumber);
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                cmd.Parameters.AddWithValue("@PartID", partId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSparePart(int partId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM SpareParts WHERE PartID = @PartID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PartID", partId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
