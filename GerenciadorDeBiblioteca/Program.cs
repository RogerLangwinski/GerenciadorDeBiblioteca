using System;

namespace GerenciadorDeBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.MenuInicial();

            Console.ReadKey();
        }
    }
}
