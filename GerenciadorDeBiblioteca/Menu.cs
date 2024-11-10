using GerenciadorDeBiblioteca.Dados;
using System;

namespace GerenciadorDeBiblioteca
{
    internal class Menu
    {
        private readonly Conexao _conexao;
        private Livro _livro;
        private Autor _autor;

        public Menu(Conexao conexao)
        {
            _conexao = conexao;
            _livro = new Livro(_conexao);
            _autor = new Autor(_conexao);
        }

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
                    case 1: _livro.CadastrarLivro(); break;
                    case 2: _autor.CadastrarAutor(""); break;
                    case 3: _livro.ConsultarLivro(); break;
                    case 8: _livro.ExcluirLivro(); break;
                    case 9: _autor.ExcluirAutor(); break;
                    case 10: Console.WriteLine("Programa encerrado."); break;

                    default: Console.WriteLine("Escolha inválida. Programa encerrado.");break;
                }
                break;
            }
        }
    }
}
