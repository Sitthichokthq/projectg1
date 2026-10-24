using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CoffeeShop
{
    // จัดการสมาชิกด้วย Array
    public class MemberManager
    {
        private const string FilePath = "user_member.json";
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

        private Member[] members = new Member[100];
        private int count = 0;

        public MemberManager()
        {
            LoadMembers();
        }

        // โหลดสมาชิกจากไฟล์ user_member.json
        private void LoadMembers()
        {

        if (!File.Exists(FilePath)) return;
         try
            {
                string jsonString = File.ReadAllText(FilePath);
                Member[] loaded = JsonSerializer.Deserialize<Member[]>(jsonString) ?? Array.Empty<Member>();

                for (int i = 0; i < loaded.Length && i < members.Length; i++)
                {
                    members[i] = loaded[i];
                    count++;
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("user_member.json is invalid, starting with an empty member list.");
            }
        }

        // บันทึกสมาชิกลงไฟล์ user_member.json
        private void SaveMembers()
        {
            Member[] toSave = members.Take(count).ToArray();
            string jsonString = JsonSerializer.Serialize(toSave, JsonOptions);
            File.WriteAllText(FilePath, jsonString);
        }

        public Member Register(string name, string phone)
        {
            if (count >= members.Length) return null;

            // ใช้ ID ที่มากที่สุด + 1 เพื่อไม่ให้ ID ซ้ำ
            int newId = 1;
            for (int i = 0; i < count; i++)
            {
                if (members[i].Id >= newId) newId = members[i].Id + 1;
            }

            Member m = new Member(newId, name, phone);
            members[count] = m;
            count++;

            SaveMembers();
            return m;
        }

        public int AddPoints(Member member, int amount)
        {
            if (member == null || amount <= 0) return 0;
            int earned = member.AddPointsFromPurchase(amount);
            SaveMembers();
            return earned;
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


/*
using System.Text.Json;

namespace SerializeBasic
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot"
            };

            string jsonString = JsonSerializer.Serialize(weatherForecast);

            Console.WriteLine(jsonString);
        }
    }
}
// output:
//{"Date":"2019-08-01T00:00:00-07:00","TemperatureCelsius":25,"Summary":"Hot"}

*/