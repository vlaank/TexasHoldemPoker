using System;

namespace Intelligence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Пример взаимодействия интеллекта программы с игроком.
            var Player = new PlayerLib.PlayerClassExample();
            Player.Print();
            //Пример взаимодействия интеллекта программы с интерфейсом.
            var Interface = new InterfaceLib.InterfaceClassExample();
            Interface.Print();
        }
    }
}
