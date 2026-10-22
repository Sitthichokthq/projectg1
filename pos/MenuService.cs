using System;

namespace CoffeeShop
{
    // Method เมนู สร้างเมนู / แสดงหมวด / แสดงรายการ
    public static class MenuService
    {
        // ใช้ Array ของหมวดเมนู
        public static MenuCategory[] Categories = CreateMenu();

        private static MenuCategory[] CreateMenu()
        {
            MenuCategory[] cats = new MenuCategory[3];
            // แก้ทีหลัง
            cats[0] = new MenuCategory("Coffee", new MenuItem[]
            {
                new MenuItem("Americano", 50),
                new MenuItem("Latte", 60),
                new MenuItem("Cappuccino", 65)
            });

            cats[1] = new MenuCategory("Tea / Milk", new MenuItem[]
            {
                new MenuItem("Thai Tea", 45),
                new MenuItem("Green Tea", 50),
                new MenuItem("Fresh Milk", 40)
            });

            cats[2] = new MenuCategory("Bakery", new MenuItem[]
            {
                new MenuItem("Croissant", 55),
                new MenuItem("Brownie", 60),
                new MenuItem("Cheesecake", 85)
            });

            return cats;
        }

        public static void ShowCategories()
        {
            Console.WriteLine();
            Console.WriteLine("--- Menu Category ---");
            for (int i = 0; i < Categories.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + Categories[i].Name);
            }
            Console.WriteLine("0. Finish ordering / Proceed to payment");
        }

        public static void ShowItems(MenuCategory category)
        {
            Console.WriteLine();
            Console.WriteLine("--- " + category.Name + " ---");
            for (int i = 0; i < category.Items.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + category.Items[i].Name
                    + " (" + category.Items[i].Price + " Bath)");
            }
            Console.WriteLine("0. Go back");
        }
    }
}
