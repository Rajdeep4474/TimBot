// TimBot session code 

using System;

namespace OrderBot
{
    public class Session
    {

        Dictionary<string, string> itemHash = new Dictionary<string, string>();
        Dictionary<string, string> sizeHash = new Dictionary<string, string>();

        Dictionary<string, string> priceHash = new Dictionary<string, string>();
        Dictionary<string, string> locHash = new Dictionary<string, string>();




        private enum State
        {
            WELCOMING, MENU, SIZE, WIPED_CREAM, DELIVERY, LOCATION, PAYMENT, CODE, FINAL_MESSAGE, CONTINUE, HISPRO
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            //this.oOrder.Phone = sPhone;

            this.itemHash.Add("1", "Iced Capp");
            this.itemHash.Add("2", "Iced Latte");
            this.itemHash.Add("3", "Hot Chocolate");
            this.itemHash.Add("4", "Espresso");

            this.sizeHash.Add("1", "Small");
            this.sizeHash.Add("2", "Medium");
            this.sizeHash.Add("3", "Large");

            this.priceHash.Add("1", "$2.39");
            this.priceHash.Add("2", "$3.29");
            this.priceHash.Add("3", "$4.13");

            this.locHash.Add("1", "384 King St N,Waterloo");
            this.locHash.Add("2", "340 Fairway Road S,Kitchener");
            this.locHash.Add("3", "355 Hespeler Road,Cambridge");
        }

        public List<String> OnMessage(String sInMessage)
        {
            if (sInMessage.Equals("1") && this.nCur == State.LOCATION)
            {
                this.nCur = State.PAYMENT;
            }
            if (sInMessage.ToUpper().Equals("COD") && this.nCur == State.CODE)
            {
                this.nCur = State.FINAL_MESSAGE;
            }

            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome To TimBot! Choose the location from where you want to order?");
                    aMessages.Add("1. 384 King St N, Waterloo \n2. 340 Fairway Road S,Kitchener \n3. 355 Hespeler Road,Cambridge");
                    this.nCur = State.MENU;
                    break;
                case State.MENU:
                    this.oOrder.CafeLocation = sInMessage;
                    aMessages.Add("What would you like to order?");
                    aMessages.Add("1. Iced Capp  \n2. Iced Latte \n3. Hot Chocolate \n4. Espresso");
                    this.nCur = State.SIZE;
                    break;
                case State.SIZE:
                    this.oOrder.Item = sInMessage;

                    aMessages.Add("What size would you like to have?");
                    aMessages.Add("1. Small \n2. Medium\n3. Large");
                    this.nCur = State.WIPED_CREAM;
                    break;
                case State.WIPED_CREAM:
                    this.oOrder.Size = sInMessage;
                    aMessages.Add("Would you like to add Wiped Cream? (Yes/No)");
                    this.nCur = State.DELIVERY;
                    break;
                case State.DELIVERY:
                    this.oOrder.WipedCream = sInMessage;
                    aMessages.Add("1. Pick up \n 2. Delivery");
                    this.nCur = State.LOCATION;

                    break;
                case State.LOCATION:
                    this.oOrder.Delivery = sInMessage;
                    aMessages.Add("Please enter your location for delivery");
                    this.nCur = State.PAYMENT;
                    break;
                case State.PAYMENT:
                    this.oOrder.Location = sInMessage;
                    aMessages.Add("Your order  " + this.sizeHash[this.oOrder.Size] + " " + this.itemHash[this.oOrder.Item]
                        + " = " + this.priceHash[this.oOrder.Size] + " has been placed in "
                        + this.locHash[this.oOrder.CafeLocation]);
                    //+ " \n How would you like to pay? (Credit/Debit)");
                    aMessages.Add("How would you like to pay? (Credit/Debit) ");
                    this.nCur = State.CODE;
                    break;
                case State.CODE:
                    this.oOrder.Payment = sInMessage;
                    aMessages.Add("Please enter 4-digit code received in your Tim's mobile app");
                    this.nCur = State.FINAL_MESSAGE;
                    break;
                case State.FINAL_MESSAGE:
                    this.oOrder.Code = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Your order has been confirmed. Now, you can see the order status in your Tim's mobile app");
                 //   aMessages.Add("Now, you can see the order status in your Tim's mobile app");
                    aMessages.Add("Do you want to know more about TimBot?");
                    this.nCur = State.CONTINUE;
                    break;
                case State.CONTINUE:
                    this.oOrder.Continue = sInMessage;
                    string msg = "1. History 2. Promotions";
                    if (sInMessage.ToLower().Equals("no"))
                    {
                        msg = "Thank you";
                    }
                    aMessages.Add(msg);

                    this.nCur = State.HISPRO;
                    break;

                case State.HISPRO:
                    string lei = " 50% festival off is going on all Tim stores on every weekend ";
                    if (sInMessage.ToLower().Equals("1"))
                    {
                        lei = " Tim Hortons Inc. is a Canadian fast food restaurant known for its coffee and doughnuts. It was started in 1964 in Hamilton, Ontario by Canadian hockey player Tim Horton. In 1967 Tim Horton joined with investor Ron Joyce, who quickly took over running the company and expanded the chain into a multi-million dollar franchise.";
                    }
                    aMessages.Add(lei);

                    this.nCur = State.HISPRO;
                    break;


                    //case State.MENU:
                    //    this.oOrder.Size = sInMessage;
                    //    this.oOrder.Save();
                    //    aMessages.Add("What protein would you like on this  " + this.oOrder.Size + " Shawarama?");
                    //    this.nCur = State.PROTEIN;
                    //    break;
                    //case State.PROTEIN:
                    //    string sProtein = sInMessage;
                    //    aMessages.Add("What toppings would you like on this (1. pickles 2. Tzaki) " + this.oOrder.Size + " " + sProtein + " Shawarama?");
                    //    break;



            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }


}

