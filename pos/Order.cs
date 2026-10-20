using System;

namespace CoffeeShop
{
    // Class: 1 บรรทัดในใบสั่งซื้อ
    public class OrderLine
    {
        public MenuItem Item { get; private set; }
        public int Quantity { get; set; }
        public int Total { get { return Item.Price * Quantity; } }

        public OrderLine(MenuItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }

    // Class: ใบสั่งซื้อ เก็บรายการด้วย Array
    public class Order
    {
        private OrderLine[] lines = new OrderLine[20];
        private int count = 0;

        public int LineCount { get { return count; } }

        public bool AddItem(MenuItem item, int quantity)
        {
            // ถ้าสั่งเมนูเดิมซ้ำ ให้บวกจำนวนเพิ่ม
            for (int i = 0; i < count; i++)
            {
                if (lines[i].Item == item)
                {
                    lines[i].Quantity += quantity;
                    return true;
                }
            }

            if (count >= lines.Length)
            {
                return false; // ตะกร้าเต็ม
            }

            lines[count] = new OrderLine(item, quantity);
            count++;
            return true;
        }

        public int GetSubtotal()
        {
            int sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += lines[i].Total;
            }
            return sum;
        }

        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine("========== รายการสั่งซื้อ ==========");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine((i + 1) + ". " + lines[i].Item.Name
                    + " x " + lines[i].Quantity
                    + " = " + lines[i].Total + " บาท");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("ราคารวม: " + GetSubtotal() + " บาท");
        }
    }
}
