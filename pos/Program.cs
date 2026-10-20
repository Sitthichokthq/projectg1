using System;

namespace CoffeeShop
{
    internal class Program
    {
        static MemberManager memberManager = new MemberManager();

        static void Main(string[] args)
        // หน้าหลักของโปรแกรม จะวน loop จนกว่าจะไม่มีลูกค้าต่อ
        {
            do
            {
                ShowWelcome();
                Member member = IdentifyMember();
                Order order = TakeOrder();

                if (order.LineCount > 0)
                {
                    Checkout(order, member);
                }
                else
                {
                    Console.WriteLine("ไม่มีรายการสั่งซื้อ");
                }
            } while (InputHelper.ReadYesNo("\nลูกค้าคนถัดไป?"));

            Console.WriteLine("ปิดระบบ ขอบคุณครับ");
        }

        static void ShowWelcome()
        // medthod แสดงข้อความยินดีต้อนรับสามารถแก้ไขชื่อร้านได้ แต่คิดบ่ออก
        {
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine("   ชื่อร้าน");
            Console.WriteLine("************************************");
        }

        static Member IdentifyMember()
        // เช็คระบบ สมาชิก
        {
            if (!InputHelper.ReadYesNo("เป็นสมาชิกร้านหรือไม่?"))
            {
                if (InputHelper.ReadYesNo("ต้องการสมัครสมาชิก (ลด 5% + สะสมแต้ม)?"))
                {
                    string name = InputHelper.ReadText("ชื่อ: ");
                    string phone = InputHelper.ReadText("เบอร์โทร: ");
                    Member m = memberManager.Register(name, phone);
                    Console.WriteLine("สมัครสำเร็จ! สมาชิกหมายเลข " + m.Id);
                    return m;
                }
                return null;
            }

            string p = InputHelper.ReadText("กรอกเบอร์โทรสมาชิก: ");
            Member found = memberManager.FindByPhone(p);
            if (found == null)
            {
                Console.WriteLine("ไม่พบสมาชิก ดำเนินการเป็นลูกค้าทั่วไป");
                return null;
            }
            Console.WriteLine("สวัสดีคุณ " + found.Name + " (แต้มสะสม " + found.Points + ")");
            return found;
        }

        static Order TakeOrder()
        {
            Order order = new Order();
            bool ordering = true;

            while (ordering)
            {
                MenuService.ShowCategories();
                int cat = InputHelper.ReadInt("เลือกประเภท: ", 0, MenuService.Categories.Length);

                if (cat == 0)
                {
                    ordering = false;
                }
                else
                {
                    MenuCategory category = MenuService.Categories[cat - 1];
                    MenuService.ShowItems(category);
                    int pick = InputHelper.ReadInt("เลือกเมนู: ", 0, category.Items.Length);

                    if (pick != 0)
                    {
                        MenuItem item = category.Items[pick - 1];
                        int qty = InputHelper.ReadInt("จำนวนกี่ชิ้น/แก้ว: ", 1, 99);

                        if (order.AddItem(item, qty))
                        {
                            Console.WriteLine("เพิ่ม " + item.Name + " x " + qty + " แล้ว");
                        }
                        else
                        {
                            Console.WriteLine("ตะกร้าเต็ม");
                        }
                    }
                }
            }
            return order;
        }

        static void Checkout(Order order, Member member)
        {
            order.Show();

            int subtotal = order.GetSubtotal();
            int discount = PaymentService.CalculateDiscount(subtotal, member);
            int total = subtotal - discount;

            if (member != null)
            {
                Console.WriteLine("ส่วนลดสมาชิก " + Member.DiscountPercent + "%: -" + discount + " บาท");
            }
            Console.WriteLine("ยอดที่ต้องชำระ: " + total + " บาท");

            int paid = PaymentService.ReceiveMoney(total);
            PaymentService.ShowChange(paid - total);

            if (member != null)
            {
                int earned = member.AddPointsFromPurchase(total);
                Console.WriteLine("ได้รับ " + earned + " แต้ม (รวม " + member.Points + " แต้ม)");
            }
            Console.WriteLine("ขอบคุณที่ใช้บริการ");
        }
    }
}