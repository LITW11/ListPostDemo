using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListPostDemo.Data
{
    public class FurnitureItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
    }

    public class FurnitureManager
    {
        private string _connectionString;

        public FurnitureManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(FurnitureItem item)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Furniture (Name, Color, Price, QuantityInStock) " +
                "VALUES (@name, @color, @price, @qty)";
            cmd.Parameters.AddWithValue("@name", item.Name);
            cmd.Parameters.AddWithValue("@color", item.Color);
            cmd.Parameters.AddWithValue("@price", item.Price);
            cmd.Parameters.AddWithValue("@qty", item.QuantityInStock);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void AddMany(List<FurnitureItem> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public List<FurnitureItem> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Furniture";
            connection.Open();
            var list = new List<FurnitureItem>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new FurnitureItem
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Color = (string)reader["Color"],
                    Price = (decimal)reader["Price"],
                    QuantityInStock = (int)reader["QuantityInStock"],
                });
            }
            return list;
        }
    }

}
