using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }

        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.Contains("Welcome To TimBot! Choose", sInput);
        }
        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }


        [Fact]
        public void TestWipedCreamStatus()
        {
            Session oSession = new Session("12345");
            String welcomeMessage = oSession.OnMessage("hello")[0];
            String restLoca = oSession.OnMessage("1")[0]; // Wateloo
            String item = oSession.OnMessage("2")[0]; // Iced Latte
            String size = oSession.OnMessage("2")[0]; // Medium

            Assert.Contains("(Yes/No)", size);
        }


        [Fact]
        public void TestOrderConfirmation()
        {
            Session oSession = new Session("12345");
            String welcomeMessage = oSession.OnMessage("hello")[0];
            String restLoca = oSession.OnMessage("1")[0]; // Wateloo
            String item = oSession.OnMessage("2")[0]; // Iced Latte
            String size = oSession.OnMessage("2")[0]; // Medium
            String wipedCream = oSession.OnMessage("yes")[0]; // Yes
            String pickup = oSession.OnMessage("1")[0]; // pick up
            String confirmation = oSession.OnMessage("Waterloo")[0]; // Waterloo

            Assert.Contains("Iced Cap", confirmation);
            Assert.Contains("Large", confirmation);
        }

        [Fact]
        public void TestOrderConfirmationPerfomance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String welcomeMessage = oSession.OnMessage("hello")[0];
            String restLoca = oSession.OnMessage("1")[0]; // Wateloo
            String item = oSession.OnMessage("2")[0]; // Iced Latte
            String size = oSession.OnMessage("2")[0]; // Medium
            String wipedCream = oSession.OnMessage("yes")[0]; // Yes
            String pickup = oSession.OnMessage("1")[0]; // pick up
            String confirmation = oSession.OnMessage("Waterloo")[0]; // Waterloo

            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 20000);
        }


        [Fact]
        public void TestFullFlow()
        {
            Session oSession = new Session("12345");
            String welcomeMessage = oSession.OnMessage("hello")[0];
            String restLoca = oSession.OnMessage("1")[0]; // Wateloo
            String item = oSession.OnMessage("2")[0]; // Iced Latte
            String size = oSession.OnMessage("2")[0]; // Medium
            String wipedCream = oSession.OnMessage("yes")[0]; // Yes
            String pickup = oSession.OnMessage("1")[0]; // pick up
            String confirmation = oSession.OnMessage("Waterloo")[0]; // Waterloo
            String paymenttype = oSession.OnMessage("COD")[0]; // COD
            String finalMessage = oSession.OnMessage("1234")[0]; // COD

            Assert.Contains("Thanks For ordering", finalMessage);
        }



        [Fact]
        public void TestFullFlowPerfomance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String welcomeMessage = oSession.OnMessage("hello")[0];
            String restLoca = oSession.OnMessage("1")[0]; // Wateloo
            String item = oSession.OnMessage("2")[0]; // Iced Latte
            String size = oSession.OnMessage("2")[0]; // Medium
            String wipedCream = oSession.OnMessage("yes")[0]; // Yes
            String pickup = oSession.OnMessage("1")[0]; // pick up
            String confirmation = oSession.OnMessage("Waterloo")[0]; // Waterloo
            String paymenttype = oSession.OnMessage("COD")[0]; // COD
            String finalMessage = oSession.OnMessage("1234")[0]; // COD

            //Assert.Contains("Thanks For ordering", finalMessage);

            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }

    }
}

