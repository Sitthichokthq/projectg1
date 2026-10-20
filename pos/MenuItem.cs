namespace CoffeeShop
{
    // ถ้างงตรงไหนทักมาได้ ง่วงนอน
    // Class: สินค้า 1 รายการ
    public class MenuItem
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public MenuItem(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    // Class: หมวดเมนู มี Array ของ MenuItem
    public class MenuCategory
    {
        public string Name { get; private set; }
        public MenuItem[] Items { get; private set; }

        public MenuCategory(string name, MenuItem[] items)
        {
            Name = name;
            Items = items;
        }
    }
}
