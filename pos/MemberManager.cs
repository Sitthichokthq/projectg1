using System;

namespace CoffeeShop
{
    // จัดการสมาชิกด้วย ใช้ Array นะโว้ยยยยยยยยยยยยยยยยยยยย
    public class MemberManager
    {
        private Member[] members = new Member[100];
        private int count = 0;

        public MemberManager()
        {
            // สมาชิกตัวอย่างไว้ทดสอบบบบบ
            Register("Somchai", "0812345678");
        }

        public Member Register(string name, string phone)
        {
            if (count >= members.Length) return null;
            Member m = new Member(count + 1, name, phone);
            members[count] = m;
            count++;
            return m;
        }

        public Member FindByPhone(string phone)
        {
            for (int i = 0; i < count; i++)
            {
                if (members[i].Phone == phone)
                {
                    return members[i];
                }
            }
            return null;
        }
    }
}
