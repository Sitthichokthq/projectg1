using System;

namespace CoffeeShop
{
    // รับค่าจากผู้ใช้ (ใช้ while loop วนจนกว่าจะกรอกถูก)
    public static class InputHelper
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                int value;
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine("กรุณากรอกตัวเลข " + min + " - " + max + " เท่านั้น");
            }
        }

        public static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();
                if (text != null && text.Trim() != "")
                {
                    return text.Trim();
                }
                Console.WriteLine("ห้ามเว้นว่าง");
            }
        }

        public static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (Y/N): ");
                string ans = Console.ReadLine();
                if (ans == "Y" || ans == "y") return true;
                if (ans == "N" || ans == "n") return false;
                Console.WriteLine("กรุณาพิมพ์ Y หรือ N");
            }
        }
    }
}
