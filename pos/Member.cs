namespace CoffeeShop
{
    // Class: สมาชิกร้าน
    // ระบบนี้เก็บแค่ครั้งเดียวต่อการรันโปรแกรม (ไม่เก็บลงไฟล์) ดังนั้นสมาชิกจะหายไปเมื่อปิดโปรแกรม
    public class Member
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public int Points { get; private set; }

        public const int DiscountPercent = 5; // สมาชิกลด 5%

        public Member(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Points = 0;
        }

        // ทุก 10 บาท = 1 แต้ม
        public int AddPointsFromPurchase(int amount)
        {
            int earned = amount / 10;
            Points += earned;
            return earned;
        }
    }
}
