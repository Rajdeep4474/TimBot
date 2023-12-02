//Timbot Order File

using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string cafe_location = String.Empty;
        private string item = String.Empty;
        private string size = String.Empty;
        private string wiped_cream = String.Empty;
        private string delivery = String.Empty;
        private string location = String.Empty;
        private string payment = String.Empty;
        private string code = String.Empty;
        private string mcontinue = String.Empty;

        //cafe_location,item,size,wiped_cream,delivery,location,payment,code

        public string CafeLocation
        {
            get => cafe_location;
            set => cafe_location = value;
        }

        public string Item
        {
            get => item;
            set => item = value;
        }

        public string Size
        {
            get => size;
            set => size = value;
        }

        public string WipedCream
        {
            get => wiped_cream;
            set => wiped_cream = value;
        }
        public string Delivery
        {
            get => delivery;
            set => delivery = value;
        }
        public string Location
        {
            get => location;
            set => location = value;
        }
        public string Payment
        {
            get => payment;
            set => payment = value;
        }
        public string Code
        {
            get => code;
            set => code = value;
        }

        public string Continue
        {
            get => mcontinue;
            set => mcontinue = value;
        }

        Dictionary<string, string> itemHash = new Dictionary<string, string>();
        Dictionary<string, string> sizeHash = new Dictionary<string, string>();

        Dictionary<string, string> priceHash = new Dictionary<string, string>();
        Dictionary<string, string> locHash = new Dictionary<string, string>();

        public Order()
        {
            this.itemHash.Add("1", "Iced Cap");
            this.itemHash.Add("2", "Iced Latte");
            this.itemHash.Add("3", "Hot Chocolate");
            this.itemHash.Add("4", "Espresso");

            this.sizeHash.Add("1", "Small");
            this.sizeHash.Add("2", "Medium");
            this.sizeHash.Add("3", "Large");

            this.priceHash.Add("1", "2.39");
            this.priceHash.Add("2", "3.29");
            this.priceHash.Add("3", "4.13");

            this.locHash.Add("1", "Waterloo");
            this.locHash.Add("2", "Doon");
            this.locHash.Add("3", "Kitchener");
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
        SET 
        cafe_location = $cafe_location ,item = $item,size = $size,wiped_cream = $wiped_cream,delivery = $delivery,location = $location,payment = $payment,code = $code
        WHERE cafe_location = $cafe_location
    ";
                commandUpdate.Parameters.AddWithValue("$cafe_location", locHash[CafeLocation]);
                commandUpdate.Parameters.AddWithValue("$item", itemHash[Item]);
                commandUpdate.Parameters.AddWithValue("$size", sizeHash[Size]);
                commandUpdate.Parameters.AddWithValue("$wiped_cream", WipedCream);
                commandUpdate.Parameters.AddWithValue("$delivery", Delivery);
                commandUpdate.Parameters.AddWithValue("$location", Location);
                commandUpdate.Parameters.AddWithValue("$payment", Payment);

                commandUpdate.Parameters.AddWithValue("$code", Code);
                int nRows = commandUpdate.ExecuteNonQuery();
                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(cafe_location,item,size,wiped_cream,delivery,location,payment,code)
            VALUES($cafe_location,$item,$size,$wiped_cream,$delivery,$location,$payment,$code)
        ";
                    commandInsert.Parameters.AddWithValue("$cafe_location", locHash[CafeLocation]);
                    commandInsert.Parameters.AddWithValue("$item", itemHash[Item]);
                    commandInsert.Parameters.AddWithValue("$size", sizeHash[Size]);
                    commandInsert.Parameters.AddWithValue("$wiped_cream", WipedCream);
                    commandInsert.Parameters.AddWithValue("$delivery", Delivery);
                    commandInsert.Parameters.AddWithValue("$location", Location);
                    commandInsert.Parameters.AddWithValue("$payment", Payment);
                    commandInsert.Parameters.AddWithValue("$code", Code);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }
        }
    }
}

//INSERT INTO orders(size, phone)
//            VALUES($size, $phone)





