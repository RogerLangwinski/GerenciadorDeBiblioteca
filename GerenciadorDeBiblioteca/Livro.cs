using GerenciadorDeBiblioteca.Dados;
using System;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace GerenciadorDeBiblioteca
{
    public class Livro
    {
        /*
        public string Titulo { get; private set; }
        public Autor IdAutor { get; private set; }
        public string Editora { get; private set; }
        public short AnoDePublicacao { get; private set; }
        public short NumeroDePaginas { get; private set; }
        public bool LivroAlugado { get; set; }
        public byte NumeroDeExemplares {  get; private set; }
        */
        private readonly Conexao _conexao;

        public Livro() { }

        public Livro(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void CadastrarLivro()
        {
            Console.WriteLine("Qual o título do livro? (OBRIGATÓRIO)");
            string titulo = Console.ReadLine();
            bool livroJaExiste = false;
            int? idAutor = 0;
            string autorDoLivro = "";

            using (var conexaoAberta = _conexao.AbrirConexao())
            {
                string queryContar = "SELECT COUNT(*) FROM LIVRO WHERE TITULO = @Titulo";
                SqlCommand commandContar = new SqlCommand(queryContar, conexaoAberta);
                commandContar.Parameters.AddWithValue("@Titulo", titulo);

                int? qtdeDeLivros = (int?)commandContar.ExecuteScalar();
                if (qtdeDeLivros != 0)
                {
                    livroJaExiste = true;
                    Console.WriteLine("Livro já cadastrado!");
                }

                if (livroJaExiste == false)
                {
                    Console.WriteLine("Qual o autor do livro? (OBRIGATÓRIO)");
                    autorDoLivro = Console.ReadLine();

                    string queryExiste = "SELECT Id FROM AUTOR WHERE NOME = @Nome";
                    SqlCommand commandExiste = new SqlCommand(queryExiste, conexaoAberta);
                    commandExiste.Parameters.AddWithValue("@Nome", autorDoLivro);

                    idAutor = (int?)commandExiste.ExecuteScalar();
                }
                if (idAutor == null)
                {
                    Autor autor = new Autor(_conexao);
                    autor.CadastrarAutor(autorDoLivro);
                    idAutor = autor.Id;
                }

                Console.WriteLine("Cadastrar quantos exemplares desse livro? (OBRIGATÓRIO)");
                string numeroDeExemplaresString = Console.ReadLine();
                byte numeroDeExemplares = 0;

                while (numeroDeExemplares == 0)
                {
                    try
                    {
                        numeroDeExemplares = byte.Parse(numeroDeExemplaresString);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Não foi possível obter o número de exemplares. " + ex.Message + "\n");
                        Console.WriteLine("Cadastrar quantos exemplares desse livro? (OBRIGATÓRIO)");
                        numeroDeExemplaresString = Console.ReadLine();
                    }
                }

                Console.WriteLine("Qual a editora do livro? (OPCIONAL)");
                string editora = Console.ReadLine();
                Console.WriteLine("Qual o ano de publicação do livro? (OPCIONAL)");
                string anoDePublicacao = Console.ReadLine();
                Console.WriteLine("Qual o número de páginas do livro? (OPCIONAL)");
                string numeroDePaginas = Console.ReadLine();

                string query = "INSERT INTO Livro (Titulo, IdAutor, Editora, AnoPublicacao, NumeroPaginas, LivroAlugado, numeroDeExemplares)" +
                    "values (@titulo, @idAutor, @editora, @anoDePublicacao, @NumeroDePaginas, @LivroAlugado, @numeroDeExemplares)";
                using (SqlCommand cmd = new SqlCommand(query, conexaoAberta))
                {
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    cmd.Parameters.AddWithValue("@idAutor", idAutor);
                    cmd.Parameters.AddWithValue("@editora", editora);
                    cmd.Parameters.AddWithValue("@anoDePublicacao", anoDePublicacao);
                    cmd.Parameters.AddWithValue("@numeroDePaginas", numeroDePaginas);
                    cmd.Parameters.AddWithValue("@livroAlugado", false);
                    cmd.Parameters.AddWithValue("@numeroDeExemplares", numeroDeExemplares);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Livro inserido com sucesso.");
                }

            }
        }




        public void ExcluirLivro()
        {
            Console.WriteLine("Qual o ID do livro a ser excluído? (Enter se quiser excluir pelo título)");
            string idExcluir = Console.ReadLine();

            using (var conexaoAberta = _conexao.AbrirConexao())
            {
                if (idExcluir == "")
                {
                    Console.WriteLine("Qual o título do livro a ser excluído?");
                    string titulo = Console.ReadLine();
                    string query = "DELETE FROM livro WHERE titulo = @titulo";
                    using (SqlCommand cmd = new SqlCommand(query, conexaoAberta))
                    {
                        cmd.Parameters.AddWithValue("@titulo", titulo);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Livro excluído com sucesso.");
                    }
                }
                else
                {
                    string query = "DELETE FROM livro WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conexaoAberta))
                    {
                        cmd.Parameters.AddWithValue("@id", idExcluir);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Livro excluído com sucesso.");
                    }
                }
            }
        }


        public void ConsultarLivro()
        {
            Console.WriteLine("Consultar pelo título, digite 1.\nConsultar pelo autor, digite 2.");
            string escolhaConsulta = Console.ReadLine();

            using (var conexaoAberta = _conexao.AbrirConexao())
            {
                if (escolhaConsulta == "1")
                {
                    Console.WriteLine("Qual o título do livro a ser pesquisado?");
                    string tituloPesquisado = Console.ReadLine();

                    string query = "select * from livro where titulo = @titulo";
                    using (SqlCommand cmd = new SqlCommand(query, conexaoAberta))
                    {
                        cmd.Parameters.AddWithValue("@titulo", tituloPesquisado);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                            while (reader.Read())
                            {
                                Console.WriteLine($"\n\nTitulo: {reader["Titulo"]}" +
                                    $"\nIdAutor: {reader["IdAutor"]}" +
                                    $"\nEditora: {reader["Editora"]}" +
                                    $"\nAno de publicação: {reader["AnoPublicacao"]}" +
                                    $"\nNumero de páginas: {reader["NumeroPaginas"]}" +
                                    $"\nLivro alugado: {reader["LivroAlugado"]}");
                            }
                    }
                }

                else if (escolhaConsulta == "2")
                {
                    Console.WriteLine("Qual o nome do autor a ser pesquisado?");
                    string autorPesquisado = Console.ReadLine();

                    string query = "select * from livro where IdAutor = (select Id from autor where nome = @autorPesquisado)";
                    using (SqlCommand cmd = new SqlCommand(query, conexaoAberta))
                    {
                        cmd.Parameters.AddWithValue("@autorPesquisado", autorPesquisado);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                            while (reader.Read())
                            {
                                Console.WriteLine($"\n\nTitulo: {reader["Titulo"]}" +
                                    $"\nIdAutor: {reader["IdAutor"]}" +
                                    $"\nEditora: {reader["Editora"]}" +
                                    $"\nAno de publicação: {reader["AnoPublicacao"]}" +
                                    $"\nNumero de páginas: {reader["NumeroPaginas"]}" +
                                    $"\nLivro alugado: {reader["LivroAlugado"]}");
                            }
                    }
                }
            }
        }
    }
}

