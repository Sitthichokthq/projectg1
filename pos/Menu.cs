using System;

namespace pos
{
    internal class Menuprogram
    {
        public static void DisplayMenu(int choice)
        {
            do
            {
                Console.WriteLine("Welcome to KFC");
                Console.WriteLine("Please select the menu");
                Console.WriteLine("1. Burger");
                Console.WriteLine("2. Fries");
                Console.WriteLine("3. Drink");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                int.TryParse(Console.ReadLine(), out choice);

                if (choice >= 1 && choice <= 3)
                {
                    string item;
                    int price;
                    switch (choice)
                    {
                        case 1:
                            item = "burger";
                            price = 50;
                            break;
                        case 2:
                            item = "fries";
                            price = 60;
                            break;
                        default:
                            item = "drink";
                            price = 30;
                            break;
                    }

                    Console.WriteLine($"You ordered {item}");
                    Console.Write("How many? ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        int result = quantity * price;
                        Console.WriteLine($"The cost of {quantity} {item}(s) is {result} baht");
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid quantity");
                    }

                    Console.WriteLine("**********************************");
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Goodbye");
                    Console.WriteLine("**********************************");
                }
                else
                {
                    Console.WriteLine("Please select a menu option from 1-4");
                }
            }
            while (choice != 4);
        }
    }
}

