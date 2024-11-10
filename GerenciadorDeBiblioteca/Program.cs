using GerenciadorDeBiblioteca.Dados;
using System;

namespace GerenciadorDeBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu(new Conexao());
            menu.MenuInicial();

            Console.ReadKey();
        }
    }
}
