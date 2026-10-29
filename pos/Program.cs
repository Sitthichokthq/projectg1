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
                Order order = TakeOrder();

                if (order == null)
                {
                    Console.WriteLine("Order cancelled.");
                }
                else if (order.LineCount == 0)
                {
                    Console.WriteLine("No orders");
                }
                else
                {
                    Member member = IdentifyMember();
                    Checkout(order, member);
                }
            } while (InputHelper.ReadYesNo("\nNext customer?"));

            Console.WriteLine("Shutting down the system. Thank you.");
        }

        static void ShowWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine("   Shop name");
            Console.WriteLine("************************************");
        }

        // คืนค่า null = ลูกค้ายกเลิกออเดอร์
        static Order TakeOrder()
        {
            Order order = new Order();
            int removeOption = MenuService.Categories.Length + 1;
            int cancelOption = MenuService.Categories.Length + 2;

            while (true)
            {
                MenuService.ShowCategories();
                int cat = InputHelper.ReadInt("Select category: ", 0, cancelOption);

                if (cat == 0)
                {
                    return order; // จบการสั่ง ไปจ่ายเงิน
                }
                else if (cat == removeOption)
                {
                    RemoveItems(order);
                }
                else if (cat == cancelOption)
                {
                    if (InputHelper.ReadYesNo("Cancel the whole order?"))
                    {
                        return null;
                    }
                }
                else
                {
                    MenuCategory category = MenuService.Categories[cat - 1];
                    MenuService.ShowItems(category);
                    int pick = InputHelper.ReadInt("Select item: ", 0, category.Items.Length);

                    if (pick != 0)
                    {
                        MenuItem item = category.Items[pick - 1];
                        int qty = InputHelper.ReadInt("Quantity: ", 1, 99);

                        if (order.AddItem(item, qty))
                        {
                            Console.WriteLine("Added " + item.Name + " x " + qty + " to the order.");
                        }
                        else
                        {
                            Console.WriteLine("Cart is full.");
                        }
                    }
                }
            }
        }

        // เอาสินค้าออกจากออเดอร์ (ลดจำนวน หรือเอาออกทั้งรายการ)
        static void RemoveItems(Order order)
        {
            if (order.LineCount == 0)
            {
                Console.WriteLine("The order is empty. Nothing to remove.");
                return;
            }

            order.Show();
            int n = InputHelper.ReadInt("Select item number to remove (0 = go back): ", 0, order.LineCount);
            if (n == 0) return;

            int index = n - 1;
            string name = order.GetLineName(index);
            int current = order.GetLineQuantity(index);
            int qty = InputHelper.ReadInt("Quantity to remove (1 - " + current + "): ", 1, current);

            order.RemoveItem(index, qty);
            Console.WriteLine("Removed " + name + " x " + qty + " from the order.");
        }

        static Member IdentifyMember()
        // เช็คระบบ สมาชิก
        {
            if (InputHelper.ReadYesNo("Are you a member of the store?"))
            {
                while (true)
                {
                    string p = InputHelper.ReadText("Enter member phone number: ");
                    Member found = memberManager.FindByPhone(p);
                    if (found != null)
                    {
                        Console.WriteLine("Hello " + found.Name + " (Total points: " + found.Points + ")");
                        return found;
                    }

                    Console.WriteLine("Member not found.");
                    if (!InputHelper.ReadYesNo("Re-enter phone number?"))
                    {
                        break; // ไม่กรอกใหม่ -> ไปต่อที่ตัวเลือกสมัครสมาชิก
                    }
                }
            }

            if (InputHelper.ReadYesNo("Do you want to register as a member (get 5% discount + earn points)?"))
            {
                string name = InputHelper.ReadText("Name: ");
                string phone;
                do
                {
                    phone = InputHelper.ReadText("Phone number: ");
                } while (!InputHelper.ReadYesNo("Phone number " + phone + " is correct?"));

                Member m = memberManager.Register(name, phone);
                if (m == null)
                {
                    Console.WriteLine("Member list is full. Proceeding as a regular customer.");
                    return null;
                }
                Console.WriteLine("Registration successful! Member ID: " + m.Id);
                return m;
            }

            Console.WriteLine("Proceeding as a regular customer.");
            return null;
        }

        static void Checkout(Order order, Member member)
        {
            int paid = 0; // เงินที่ลูกค้าให้มาแล้ว (สะสมถ้าลูกค้าเติมเงิน)
            int total;

            while (true)
            {
                if (order.LineCount == 0)
                {
                    Console.WriteLine("No items left. Order cancelled.");
                    RefundIfAny(paid);
                    return;
                }

                order.Show();

                int subtotal = order.GetSubtotal();
                int discount = PaymentService.CalculateDiscount(subtotal, member);
                total = subtotal - discount;

                if (member != null)
                {
                    Console.WriteLine("Member discount " + Member.DiscountPercent + "%: -" + discount + " Bath");
                }
                Console.WriteLine("Total amount to pay: " + total + " Bath");

                if (paid == 0)
                {
                    paid = PaymentService.ReceiveMoney("Receive payment from customers: ");
                }

                if (paid >= total)
                {
                    break; // เงินพอแล้ว
                }

                Console.WriteLine("Not enough money; there's still a shortfall. " + (total - paid) + " Bath");
                Console.WriteLine("1. Customer adds more money");
                Console.WriteLine("2. Remove some items");
                Console.WriteLine("3. Cancel the order");
                int choice = InputHelper.ReadInt("Select: ", 1, 3);

                if (choice == 1)
                {
                    paid += PaymentService.ReceiveMoney("Additional payment: ");
                }
                else if (choice == 2)
                {
                    RemoveItems(order);
                }
                else
                {
                    if (InputHelper.ReadYesNo("Cancel the whole order?"))
                    {
                        Console.WriteLine("Order cancelled.");
                        RefundIfAny(paid);
                        return;
                    }
                }
            }

            PaymentService.ShowChange(paid - total);

            if (member != null)
            {
                int earned = memberManager.AddPoints(member, total);
                Console.WriteLine("Earned " + earned + " points (Total: " + member.Points + " points)");
            }
            Console.WriteLine("Thank you for using our service.");
        }

        static void RefundIfAny(int paid)
        {
            if (paid > 0)
            {
                Console.WriteLine("Return money to customer: " + paid + " Bath");
            }
        }
    }
}