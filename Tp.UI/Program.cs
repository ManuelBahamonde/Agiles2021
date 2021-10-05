using System;

namespace Tp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = InputName();
        }

        static string InputName()
        {
            Console.WriteLine("Ingrese su nombre");
            var name = Console.ReadLine();

            // Utilities.ValidateName(name); TODO

            return name;
        }
    }
}
