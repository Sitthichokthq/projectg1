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
                        Console.Write("Enter your money: ");
                        int money = int.Parse(Console.ReadLine());

                        if (money >= result)
                        {
                            Console.WriteLine("You pay " + money + ".");

                            int change = money - result;
                            Console.WriteLine("You get change for " + change + " Baht.");

                            int[] values = { 1000, 500, 100, 50, 20, 10, 5, 2, 1 };
                            string[] names = {
                                "1,000 Baht banknote",
                                "500 Baht banknote  ",
                                "100 Baht banknote  ",
                                "50 Baht banknote   ",
                                "20 Baht banknote   ",
                                "10 Baht coin       ",
                                "5 Baht coin        ",
                                "2 Baht coin        ",
                                "1 Baht coin        "
                            };

                            int[] counts = new int[values.Length];
                            int remainingChange = change;
                            switch (0)
                            {
                                case 0:
                                    if (remainingChange >= 1000)
                                    {
                                        counts[0] = remainingChange / 1000;
                                        remainingChange %= 1000;
                                    }
                                    goto case 1;

                                case 1:
                                    if (remainingChange >= 500)
                                    {
                                        counts[1] = remainingChange / 500;
                                        remainingChange %= 500;
                                    }
                                    goto case 2;

                                case 2:
                                    if (remainingChange >= 100)
                                    {
                                        counts[2] = remainingChange / 100;
                                        remainingChange %= 100;
                                    }
                                    goto case 3;

                                case 3:
                                    if (remainingChange >= 50)
                                    {
                                        counts[3] = remainingChange / 50;
                                        remainingChange %= 50;
                                    }
                                    goto case 4;

                                case 4:
                                    if (remainingChange >= 20)
                                    {
                                        counts[4] = remainingChange / 20;
                                        remainingChange %= 20;
                                    }
                                    goto case 5;

                                case 5:
                                    if (remainingChange >= 10)
                                    {
                                        counts[5] = remainingChange / 10;
                                        remainingChange %= 10;
                                    }
                                    goto case 6;

                                case 6:
                                    if (remainingChange >= 5)
                                    {
                                        counts[6] = remainingChange / 5;
                                        remainingChange %= 5;
                                    }
                                    goto case 7;

                                case 7:
                                    if (remainingChange >= 2)
                                    {
                                        counts[7] = remainingChange / 2;
                                        remainingChange %= 2;
                                    }
                                    goto case 8;

                                case 8:
                                    if (remainingChange >= 1)
                                    {
                                        counts[8] = remainingChange / 1;
                                        remainingChange %= 1;
                                    }
                                    break;
                            }

                            Console.WriteLine("Details of Change");
                            if (change == 0)
                            {
                                Console.WriteLine("No change.");
                            }
                            else
                            {
                                switch (0)
                                {
                                    case 0:
                                        if (counts[0] > 0) Console.WriteLine($"{names[0]}: {counts[0]}");
                                        goto case 1;

                                    case 1:
                                        if (counts[1] > 0) Console.WriteLine($"{names[1]}: {counts[1]}");
                                        goto case 2;

                                    case 2:
                                        if (counts[2] > 0) Console.WriteLine($"{names[2]}: {counts[2]}");
                                        goto case 3;

                                    case 3:
                                        if (counts[3] > 0) Console.WriteLine($"{names[3]}: {counts[3]}");
                                        goto case 4;

                                    case 4:
                                        if (counts[4] > 0) Console.WriteLine($"{names[4]}: {counts[4]}");
                                        goto case 5;

                                    case 5:
                                        if (counts[5] > 0) Console.WriteLine($"{names[5]}: {counts[5]}");
                                        goto case 6;

                                    case 6:
                                        if (counts[6] > 0) Console.WriteLine($"{names[6]}: {counts[6]}");
                                        goto case 7;

                                    case 7:
                                        if (counts[7] > 0) Console.WriteLine($"{names[7]}: {counts[7]}");
                                        goto case 8;

                                    case 8:
                                        if (counts[8] > 0) Console.WriteLine($"{names[8]}: {counts[8]}");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough money.");
                        }
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

