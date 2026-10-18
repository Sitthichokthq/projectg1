using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poscaculator
{
    internal class Calprogram
    {
        public static void Calculator(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("You selected Burger");
                    break;
                case 2:
                    Console.WriteLine("You selected Fries");
                    break;
                case 3:
                    Console.WriteLine("You selected Drink");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        public static void CalculateTotalPrice(int quantity)
        {
            int totalPrice = quantity * 10;
            Console.WriteLine("Total price is: " + totalPrice);
        }
    }
}

