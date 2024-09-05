﻿using System;
using System.Data.SqlClient;

namespace GerenciadorDeBiblioteca
{
    public class Autor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        /*public string Nacionalidade { get; set; }
        public DateTime AnoDeNascimento { get; set; }*/

        public Autor() { }

        /*
        public Autor(string nomeDoAutor)
        {
            Nome = nomeDoAutor;
            CadastrarAutor(connectionString);
        }
        */
        public void CadastrarAutor(string connectionString, string nomeDoAutor)
        {
            Nome = nomeDoAutor;
            if (Nome == "")
            {
                Console.WriteLine("Qual o nome do autor? (OBRIGATÓRIO)");
                Nome = Console.ReadLine();
                while (Nome == "")
                {
                    Console.WriteLine("Nome não pode ser vazio.\nInsira o nome do autor:");
                    Nome = Console.ReadLine();
                }
            }
            Console.WriteLine("Qual a nacionalidade do autor? (OPCIONAL)");
            string nacionalidade = Console.ReadLine();
            Console.WriteLine("Qual o ano de nascimento do autor? (OPCIONAL)");
            string anoDeNascimento = Console.ReadLine();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "INSERT INTO autor (Nome, Nacionalidade, AnoDeNascimento) OUTPUT INSERTED.Id " +
                                "values(@nome, @nacionalidade, @anoDeNascimento)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@nome", Nome);
                    cmd.Parameters.AddWithValue("@nacionalidade", nacionalidade);
                    cmd.Parameters.AddWithValue("@anoDeNascimento", anoDeNascimento);
                    Id = (int)cmd.ExecuteScalar();
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
