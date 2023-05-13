using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Globalization;

namespace VideoRentalSystem
{
    public class DataAccess
    {
        readonly string dbName = "RentalDB";

        //CUSTOMERS

        public List<Customer> GetCustomers()
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                List<Customer> custs = connection.Query<Customer>($"SELECT * from Customer;").ToList();
                CheckOverdue(custs, connection);
                custs = connection.Query<Customer>($"SELECT * from Customer;").ToList(); //re-update customer list
                connection.Close();
                return custs;
            }
        }

        public string GetCustomerInfo(Customer cust)
        {
            List<string> rentedTitles = GetRentedTitles(cust);

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                string titlesRented;

                if (!rentedTitles.Any()) titlesRented = "None";
                else
                {
                    titlesRented = string.Join(", ", rentedTitles);
                }

                string info = String.Format("Customer Name: {0} {1}\n" +
                    "Video titles rented: {2}\n" +
                    "Overdue count: {3}", cust.FirstName, cust.LastName, titlesRented, cust.Overdue);

                connection.Close();

                return info;
            }
        }

        public List<RentedVideo> GetRentedVideos(Customer cust)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                return connection.Query<RentedVideo>($"SELECT * FROM Rented WHERE RenterID = {cust.ID}").ToList();
            }
        }

        public List<string> GetRentedTitles(Customer cust)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                List<RentedVideo> rentedVideos = GetRentedVideos(cust);
                List<string> rentedVideoTitles = new List<string>();

                foreach (RentedVideo rentedVid in rentedVideos) rentedVideoTitles.Add(rentedVid.VideoTitle);
                connection.Close();
                return rentedVideoTitles;
            }
        }

        public void AddCustomer(string FirstName, string LastName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                string query = $"INSERT INTO Customer (LastName, FirstName, Rented, Overdue, GrossBalance, Balance) " +
                $"VALUES ('{ FirstName }', '{LastName}', '0', '0', '0.00', '0.00');";
                connection.Query(query);
                connection.Close();
            }
        }

        public void EditCustomer(Customer cust, string newLastName, string newFirstName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                string query = $"UPDATE Customer SET LastName = '{newLastName}', FirstName = '{newFirstName}' WHERE ID = {cust.ID};";
                connection.Query(query);
                connection.Close();
            }
        }

        public void CustomerCheckout(int custID, List<RentedVideo> rentedVids, decimal totalPrice)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                int custRented = connection.Query<int>($"SELECT Rented FROM Customer WHERE ID = {custID};").ToList()[0];
                string todayString = String.Format("{0:yyyy-MM-dd}", DateTime.Today);

                foreach(RentedVideo rentVid in rentedVids)
                {
                    string query = $"INSERT INTO Rented (VideoID, VideoTitle, VideoCategory, VideoPrice, RenterID, DateRented, " +
                        $"DateDue, DaysOverdue, OverdueFee) " +
                        $"VALUES ({rentVid.VideoID}, '{rentVid.VideoTitle}', '{rentVid.VideoCategory}', {rentVid.VideoPrice}, " +
                        $"{custID}, '{todayString}', '{rentVid.DateDue}', 0, 0.00);";
                    connection.Query(query);

                    int numIn = connection.Query<int>($"SELECT NumIn FROM Video WHERE ID = {rentVid.VideoID};").ToList()[0];
                    int numOut = connection.Query<int>($"SELECT NumOut FROM Video WHERE ID = {rentVid.VideoID};").ToList()[0];
                    numIn--;
                    numOut++;
                    string query1 = $"UPDATE Video SET NumIn = {numIn}, NumOut = {numOut} WHERE ID = {rentVid.VideoID};";
                    connection.Query(query1);

                    custRented++;
                }
                totalPrice += connection.Query<decimal>($"SELECT Balance FROM Customer WHERE ID = {custID};").ToList()[0];
                string query2 = $"UPDATE Customer SET Rented = {custRented}, GrossBalance = {totalPrice}, Balance = {totalPrice} " +
                    $"WHERE ID = {custID};";
                connection.Query(query2);
            }
        }

        public void ReturnFromCustomer(Customer cust, List<RentedVideo> returnedVideos)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                int rented = returnedVideos.Count();
                int overdueDays = 0;
                decimal grossBalance = 0.00M;
                decimal netBalance = 0.00M;

                foreach(RentedVideo returnedVideo in returnedVideos)
                {
                    Video selectedVideo = connection.Query<Video>($"SELECT * FROM Video WHERE ID = {returnedVideo.VideoID}").ToList()[0];
                    selectedVideo.NumIn++;
                    selectedVideo.NumOut--;
                    connection.Query($"UPDATE Video SET NumIn = {selectedVideo.NumIn}, NumOut = {selectedVideo.NumOut} " +
                        $"WHERE ID = {selectedVideo.ID};");

                    overdueDays += ((int)returnedVideo.OverdueFee) / 5;
                    grossBalance += returnedVideo.VideoPrice;
                    netBalance += (returnedVideo.VideoPrice + returnedVideo.OverdueFee);

                    connection.Query($"DELETE FROM Rented WHERE RenterID = {cust.ID} AND VideoID = {returnedVideo.VideoID};");
                }

                cust.Rented -= rented;
                cust.Overdue -= overdueDays;
                cust.GrossBalance -= grossBalance;
                cust.Balance -= netBalance;

                string query = $"UPDATE Customer SET Rented = {cust.Rented}, Overdue = {cust.Overdue}, GrossBalance = {cust.GrossBalance}, Balance = {cust.Balance} " +
                    $"WHERE ID = {cust.ID};";
                connection.Query(query);
            }
        }

        public List<int> CheckRented(Customer cust)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                return connection.Query<int>($"SELECT VideoID FROM Rented WHERE RenterID = {cust.ID}").ToList();
            }
        }

        private void CheckOverdue(List<Customer> custs, IDbConnection conn)
        {
            DateTime today = DateTime.Today;

            foreach(Customer cust in custs)
            {
                int overdue = 0;
                List<RentedVideo> rentedVideos = conn.Query<RentedVideo>($"SELECT * FROM Rented WHERE RenterID = {cust.ID};").ToList();

                if (!rentedVideos.Any()) continue;

                foreach(RentedVideo rentedVideo in rentedVideos)
                {
                    DateTime dueDate = DateTime.ParseExact(rentedVideo.DateDue, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    if (DateTime.Compare(today, dueDate) > 0)
                    {
                        int daysOverdue = (today - dueDate).Days;
                        decimal overdueFee = 5 * daysOverdue;

                        conn.Query($"UPDATE Rented SET DaysOverdue = {daysOverdue}, OverdueFee = {overdueFee} " +
                            $"WHERE VideoID = {rentedVideo.VideoID} AND RenterID = {cust.ID};");
                        overdue += daysOverdue;
                    }
                }

                if (overdue >= 1)
                {
                    decimal netBalance = cust.GrossBalance + (overdue * 5);
                    conn.Query($"UPDATE Customer SET Overdue = {overdue}, Balance = {netBalance} WHERE ID = {cust.ID};");
                }
            }
        }

        //VIDEOS

        public List<Video> GetVideos()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                return connection.Query<Video>($"SELECT * from Video;").ToList();
            }
        }

        public string GetVideoName(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                return connection.Query<string>($"SELECT Title FROM Video WHERE ID = {id}").ToList()[0];
            }
        }

        public string GetVideoCategory(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                return connection.Query<string>($"SELECT Category FROM Video WHERE ID = {id}").ToList()[0];
            }
        }

        public void AddVideo(string title, string category, int stock, int rentDays)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                string query = $"INSERT INTO Video (Title, Category, NumIn, NumOut, RentDays) " +
                    $"VALUES ('{title}', '{category}', {stock}, 0, {rentDays});";
                connection.Query(query);
                connection.Close();
            }
        }

        public void EditVideo(Video vid, string newTitle, string newCategory)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                string query = $"UPDATE Video SET Title = '{newTitle}', Category = '{newCategory}' WHERE ID = {vid.ID};";
                connection.Query(query);
                connection.Close();
            }
        }

        public void DeleteVideo(Video vid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Connector.ConnVal(dbName)))
            {
                string query = $"DELETE FROM Video WHERE ID = {vid.ID};";
                connection.Query(query);
                connection.Close();
            }
        }
    }
}
