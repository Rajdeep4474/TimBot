using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, MENU, SIZE, WIPED_CREAM, DELIVERY, LOCATION, PAYMENT, CODE, FINAL_MESSAGE
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
         //   this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome To TimBot! Choose the location from where you want to order?");
                    aMessages.Add("1. Waterloo\n2. Doon \n3. Kitchener");
                    this.nCur = State.MENU;
                    break;
                case State.MENU:
                    aMessages.Add("What would you like to order?");
                    aMessages.Add("1. Iced Cap\n2. Iced Latte\n3. Hot Chocolate\n4. Espresso");
                    this.nCur = State.SIZE;
                    break;
                case State.SIZE:
                    aMessages.Add("What size would you like to have?");
                    aMessages.Add("1. Small \n2. Medium\n3. Large");
                    this.nCur = State.WIPED_CREAM;
                    break;
                case State.WIPED_CREAM:
                    aMessages.Add("Would you like to add Wiped Cream? (Yes/No)");
                    this.nCur = State.DELIVERY;
                    break;
                case State.DELIVERY:
                    aMessages.Add("Pick up / Delivery?");
                    this.nCur = State.LOCATION;
                    break;
                case State.LOCATION:
                    aMessages.Add("Please Enter Your location.");
                    this.nCur = State.PAYMENT;
                    break;
                case State.PAYMENT:
                    aMessages.Add("Your Order is Large Iced Cap = 5.59$\nHow do you like to pay? (COD/Onlie)");
                    this.nCur = State.CODE;
                    break;
                case State.CODE:
                    aMessages.Add("Please Enter your 4-digit code shown in your Tim mobile app.");
                    this.nCur = State.FINAL_MESSAGE;
                    break;
                case State.FINAL_MESSAGE:
                    aMessages.Add("Thanks For ordering at Tim's. We will deduct the bill amount from your card. Now, you can see the status in the app.");
                    aMessages.Add("We have a Black Friday 40% off going on all Friday. Please check in the nearby Tim store.");
                    this.nCur = State.SIZE;
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

