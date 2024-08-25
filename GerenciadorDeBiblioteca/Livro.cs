using System;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace GerenciadorDeBiblioteca
{
    public class Livro
    {
        /*public string Titulo { get; private set; }
        public string Editora { get; private set; }
        public short AnoDePublicacao { get; private set; }
        public short NumeroDePaginas { get; private set; }
        public bool LivroAlugado { get; set; }*/

        public Livro() { }

        public void CadastrarLivro(string connectionString)
        {
            Console.WriteLine("Qual o título do livro? (OBRIGATÓRIO)");
            string titulo = Console.ReadLine();
            Console.WriteLine("Qual a editora do livro? (OPCIONAL)");
            string editora = Console.ReadLine();
            Console.WriteLine("Qual o ano de publicação do livro? (OPCIONAL)");
            string anoDePublicacao = Console.ReadLine();
            Console.WriteLine("Qual o número de páginas do livro? (OPCIONAL)");
            string numeroDePaginas = Console.ReadLine();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "INSERT INTO Livro (Titulo, Editora, AnoPublicacao, NumeroPaginas, LivroAlugado)" +
                    "values (@titulo, @editora, @anoDePublicacao, @NumeroDePaginas, @LivroAlugado)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    cmd.Parameters.AddWithValue("@editora", editora);
                    cmd.Parameters.AddWithValue("@anoDePublicacao", anoDePublicacao);
                    cmd.Parameters.AddWithValue("@numeroDePaginas", numeroDePaginas);
                    cmd.Parameters.AddWithValue("@livroAlugado", false);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Livro inserido com sucesso.");
                }
            }
        }

        public void ExcluirLivro(string connectionString)
        {
            Console.WriteLine("Qual o ID do livro a ser excluído? (Enter se quiser excluir pelo título)");
            string idExcluir = Console.ReadLine();
            if (idExcluir == "")
            {
                Console.WriteLine("Qual o título do livro a ser excluído?");
                string titulo = Console.ReadLine();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "DELETE FROM livro WHERE titulo = @titulo";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@titulo", titulo);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Livro excluído com sucesso.");
                    }
                }
            }
            else
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "DELETE FROM livro WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@id", idExcluir);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Livro excluído com sucesso.");
                    }
                }
            }
        }

        public void ConsultarLivro(string connectionString)
        {
            Console.WriteLine("Para consultar pelo título, digite 1.\nPesquisar pelo autor, digite 2.");
            string escolhaConsulta = Console.ReadLine();
            
            if (escolhaConsulta == "1")
            {
                Console.WriteLine("Qual o título do livro a ser pesquisado?");
                string tituloPesquisado = Console.ReadLine();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "select * from livro where titulo = @titulo";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@titulo", tituloPesquisado);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                            while (reader.Read())
                            {
                                Console.WriteLine($"Titulo: {reader["Titulo"]}\n" +
                                    $"Editora: {reader["Editora"]}\n" +
                                    $"Ano de publicação: {reader["AnoPublicacao"]}\n" +
                                    $"Numero de páginas: {reader["NumeroPaginas"]}\n" +
                                    $"Livro alugado: {reader["LivroAlugado"]}");
                            }
                    }
                }
            }
            else if (escolhaConsulta == "2")
            {
                Console.WriteLine("Qual o nome do autor a ser pesquisado?");
                string autorPesquisado = Console.ReadLine();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "select * from livro where titulo = @titulo";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@titulo", autorPesquisado);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Livro excluído com sucesso.");
                    }
                }
            }
        }
    }
}
