using System;

namespace GerenciadorDeBiblioteca
{
    internal class Menu
    {
        Livro livro = new Livro();
        Autor autor = new Autor();

        string connectionString =
                "SERVER=DESKTOP-ROGER\\SQLEXPRESS;" +
                "DATABASE=GERENCIADORDEBIBLIOTECA;" +
                "TRUSTED_CONNECTION=TRUE;";

        public void MenuInicial()
        {
            byte escolhaMenu = 0;
            bool escolhaMenuCorreta = true;
            Console.WriteLine(
                "Digite a opção desejada:" +
                "\n1 - Cadastrar livro" +
                "\n2 - Cadastrar autor" +
                "\n3 - Consultar livro" +
                "\n4 - Consultar autor" +
                "\n5 - Alugar livro" +
                "\n6 - Consultar livros alugados" +
                "\n7 - Devolver livro alugado" +
                "\n8 - Excluir livro" +
                "\n9 - Excluir autor" +
                "\n10 - Sair\n");

            try
            {
                escolhaMenu = byte.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nEscolha novamente:");
                escolhaMenuCorreta = false;
                MenuInicial();
            }

            while (escolhaMenuCorreta)
            {
                switch(escolhaMenu)
                {
                    case 1: livro.CadastrarLivro(connectionString); break;
                    case 2: autor.CadastrarAutor(connectionString, ""); break;
                    case 3: livro.ConsultarLivro(connectionString); break;
                    case 8: livro.ExcluirLivro(connectionString); break;
                    case 9: autor.ExcluirAutor(connectionString); break;
                    case 10: Console.WriteLine("Programa encerrado."); break;

                    default: Console.WriteLine("Escolha inválida. Programa encerrado.");break;
                }
                break;
            }
        }
    }
}
