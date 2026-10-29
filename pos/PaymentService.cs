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

        // รับเงินจากลูกค้า "ครั้งเดียว" (ไม่วนบังคับแล้ว)
        // ถ้าเงินไม่พอ Program.Checkout จะให้เลือก เติมเงิน / เอาสินค้าออก / ยกเลิก
        public static int ReceiveMoney(string prompt)
        {
            return InputHelper.ReadInt(prompt, 0, 1000000);
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