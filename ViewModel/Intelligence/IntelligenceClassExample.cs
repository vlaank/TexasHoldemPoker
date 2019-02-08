using System;

namespace ViewModelLib
{
    public class IntelligenceClassExample
    {
        public static string getInterfaceAndPlayerClassNames()
            => String.Join(
                "\n",
                "Название классов в Model:",
                InterfaceLib.InterfaceClassExample.Factory().GetType().Name,
                PlayerLib.PlayerClassExample.Factory().GetType().Name
                );
    }
}
