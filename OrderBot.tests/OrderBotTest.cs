// TimBot Unit test code
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
            String confirmation = oSession.OnMessage("1")[0]; // pick up
                                                              //String confirmation = oSession.OnMessage("Waterloo")[0]; // Waterloo


            Assert.Contains("Iced Latte", confirmation);
            // Assert.Contains(" Your order Small Iced Capp = $2.39 has been placed in 384 King St N,Waterloo", confirmation);
            Assert.Contains("Medium", confirmation);

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
            Assert.True(nElapsed < 40000);
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
            String paymenttype = oSession.OnMessage("Credit")[0]; // COD
            String finalMessage = oSession.OnMessage("1234")[0]; // COD
            String ques = oSession.OnMessage("yes")[0]; //yes
            String hist = oSession.OnMessage("1")[0]; //history




            Assert.Contains("Tim Hortons Inc. is a Canadian fast food restaurant known for its coffee and doughnuts", hist);
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
            String paymenttype = oSession.OnMessage("Credit")[0]; // COD
            String finalMessage = oSession.OnMessage("1234")[0]; // COD
            String ques = oSession.OnMessage("yes")[0]; //yes
            String hist = oSession.OnMessage("1")[0]; //history


            Assert.Contains("Tim Hortons Inc. is a Canadian fast food restaurant known for its coffee and doughnuts", hist);

            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 100000);
        }

    }
}

