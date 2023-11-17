using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _cafe_location = String.Empty;
        private string _item = String.Empty;
        private string _size = String.Empty;
        private string _wiped_cream = String.Empty;
        private string _delivery = String.Empty;
        private string _location = String.Empty;
        private string _payment = String.Empty;
        private string _code = String.Empty;

        public string CafeLocation
        {
            get => _cafe_location;
            set => _cafe_location = value;
        }

        public string Item
        {
            get => _item;
            set => _item = value;
        }

        public string Size
        {
            get => _size;
            set => _size = value;
        }

        public string WipedCream
        {
            get => _wiped_cream;
            set => _wiped_cream = value;
        }
        public string Delivery
        {
            get => _delivery;
            set => _delivery = value;
        }
        public string Location
        {
            get => _location;
            set => _location = value;
        }
        public string Payment
        {
            get => _payment;
            set => _payment = value;
        }
        public string Code
        {
            get => _code;
            set => _code = value;
        }


        public void Save()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET size = $size
        WHERE phone = $phone
    ";
                //commandUpdate.Parameters.AddWithValue("$size", Size);
                //commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    //commandInsert.Parameters.AddWithValue("$size", Size);
                    //commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}




