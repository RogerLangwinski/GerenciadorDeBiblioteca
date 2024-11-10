using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeBiblioteca.Dados
{
    public class Conexao
    {
        private readonly string connectionString =
                @"SERVER=DESKTOP-ROGER\SQLEXPRESS;
                DATABASE=GERENCIADORDEBIBLIOTECA;
                TRUSTED_CONNECTION=TRUE;";

        public Conexao() { }

        //método é sempre composto de nível de visibilidade (public, private, protected, internal), retorno dele
        public SqlConnection AbrirConexao()
        {
            SqlConnection conexao = new SqlConnection(connectionString);

            try
            {
                conexao.Open();
                Console.WriteLine("Conexão aberta com sucesso!");
                return conexao;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao abrir conexão: {e.Message}");
                Console.ReadKey();
                throw;
                //
                //throw;
            }
        }
    }
}
