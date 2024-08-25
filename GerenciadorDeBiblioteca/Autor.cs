using System;
using System.Data.SqlClient;

namespace GerenciadorDeBiblioteca
{
    internal class Autor
    {
        /*public string Nome { get; private set; }
        public string Nacionalidade { get; set; }
        public DateTime AnoDeNascimento { get; set; }*/

        public Autor() { }

        public void CadastrarAutor(string connectionString)
        {
            Console.WriteLine("Qual o nome do autor? (OBRIGATÓRIO)");
            string nome = Console.ReadLine();
            while (nome == "")
            {
                Console.WriteLine("Nome não pode ser vazio.\nInsira o nome do autor:");
                nome = Console.ReadLine();
            }
            Console.WriteLine("Qual a nacionalidade do autor? (OPCIONAL)");
            string nacionalidade = Console.ReadLine();
            Console.WriteLine("Qual o ano de nascimento do autor? (OPCIONAL)");
            string anoDeNascimento = Console.ReadLine();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "INSERT INTO autor (Nome, Nacionalidade, AnoDeNascimento)" +
                                "values(@nome, @nacionalidade, @anoDeNascimento)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@nacionalidade", nacionalidade);
                    cmd.Parameters.AddWithValue("@anoDeNascimento", anoDeNascimento);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Autor inserido com sucesso.");
                }
            }
        }

        public void ExcluirAutor(string connectionString)
        {
            Console.WriteLine("Qual o ID do autor a ser excluído? (Enter se quiser excluir pelo nome)");
            string idExcluir = Console.ReadLine();
            if (idExcluir == "")
            {
                Console.WriteLine("Qual o nome do autor a ser excluído?");
                string nome = Console.ReadLine();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "DELETE FROM autor WHERE Nome = @nome";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Autor excluído com sucesso.");
                    }
                }
            }
            else
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "DELETE FROM autor WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@id", idExcluir);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Autor excluído com sucesso.");
                    }
                }
            }
        }
    }
}
