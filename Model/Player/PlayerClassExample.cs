using System;
using System.Threading.Tasks;

namespace PlayerLib
{
    public class ReactiveExample
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public async Task<string> GetFullName()
        {
            //Предположим что для проверки требуется некоторое время( < 5 сек);
            Task.Delay(5000);
            return string.Join(" ", Name, Surname);
        }
    }
    public class PlayerClassExample
    {
        public static PlayerClassExample Factory()
            => new PlayerClassExample();
    }
}
