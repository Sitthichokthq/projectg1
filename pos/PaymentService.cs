using System;

namespace CoffeeShop
{
    // Method เกี่ยวกับการคิดเงิน รับเงิน ทอนเงิน
    public static class PaymentService
    {
        private static int[] denominations = { 1000, 500, 100, 50, 20, 10, 5, 2, 1 };
        // เก็บข้อความแบบ array แล้วเรียกใช้ตามเลขเข้าใจนะโบรววววว
        private static string[] names =
        {
            "Banknote 1000", "Banknote 500", "Banknote 100", "Banknote 50", "Banknote 20",
            "Coin 10", "Coin 5", "Coin 2", "Coin 1"
        };

        // คำนวณส่วนลด (เฉพาะสมาชิก)
        public static int CalculateDiscount(int subtotal, Member member)
        {
            if (member == null) return 0;
            return subtotal * Member.DiscountPercent / 100;
        }

        // รับเงินจากลูกค้า วนจนกว่าจะพอ
        public static int ReceiveMoney(int total)
        {
            int paid;
            do
            {
                paid = InputHelper.ReadInt("รับเงินจากลูกค้า: ", 0, 1000000);
                if (paid < total)
                {
                    Console.WriteLine("Not enough money; there’s still a shortfall. " + (total - paid) + " Bath");
                }
            } while (paid < total);
            return paid;
        }

        // ทอนเงิน ให้แบงก์/เหรียญใหญ่ก่อนเสมอ
        public static void ShowChange(int change)
        {
            Console.WriteLine("Change: " + change + " Bath");
            int remain = change;
            for (int i = 0; i < denominations.Length; i++)
            {
                int n = remain / denominations[i];
                remain = remain % denominations[i];
                if (n > 0)
                {
                    Console.WriteLine("  " + names[i] + " quantity " + n);
                }
            }
        }
    }
}
