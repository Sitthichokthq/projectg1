using System;
// ถามว่าใช้ AI ไหม ใช้แต่ก็นิดหน่อยบางอันมันบัค
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
                    Console.WriteLine("No orders");
                }
            } while (InputHelper.ReadYesNo("\nNext customer?"));

            Console.WriteLine("Shutting down the system. Thank you.");
        }

        static void ShowWelcome()
        // medthod แสดงข้อความยินดีต้อนรับสามารถแก้ไขชื่อร้านได้ แต่คิดบ่ออก
        {
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine("   Shop name");
            Console.WriteLine("************************************");
        }

        static Member IdentifyMember()
        // เช็คระบบ สมาชิก
        {
            if (!InputHelper.ReadYesNo("Are you a member of the store?"))
            {
                if (InputHelper.ReadYesNo("Do you want to register as a member (get 5% discount + earn points)?"))
                {
                    string name = InputHelper.ReadText("Name: ");
                    string phone = InputHelper.ReadText("Phone number: ");
                    Member m = memberManager.Register(name, phone);
                    Console.WriteLine("Registration successful! Member ID: " + m.Id);
                    return m;
                }
                return null;
            }

            string p = InputHelper.ReadText("Enter member phone number: ");
            Member found = memberManager.FindByPhone(p);
            if (found == null)
            {
                Console.WriteLine("Member not found. Proceeding as a regular customer.");
                return null;
            }
            Console.WriteLine("Hello " + found.Name + " (Total points: " + found.Points + ")");
            return found;
        }

        static Order TakeOrder()
        {
            Order order = new Order();
            bool ordering = true;

            while (ordering)
            {
                MenuService.ShowCategories();
                int cat = InputHelper.ReadInt("Select category: ", 0, MenuService.Categories.Length);

                switch (cat)
        {
            case 0:
                ordering = false;
                break;

            default:
                
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
                break;
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
                Console.WriteLine("Member discount " + Member.DiscountPercent + "%: -" + discount + " Bath");
            }
            Console.WriteLine("Total amount to pay: " + total + " Bath");

            int paid = PaymentService.ReceiveMoney(total);
            PaymentService.ShowChange(paid - total);

            if (member != null)
            {
                int earned = memberManager.AddPoints(member, total);
                Console.WriteLine("Earned " + earned + " points (Total: " + member.Points + " points)");
            }
            Console.WriteLine("Thank you for using our service.");
        }
    }
}
