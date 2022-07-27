using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLServer
{
    class ConexaoBanco
    {
        public static SqlConnection ConectaDB()
        {
            try
            {
                //Realiza a conexão com o SQL Server
                string conexao = @"Data Source=Seu notebook;Initial Catalog=Recepcao; Integrated Security=true;";
                SqlConnection con;
                con = new SqlConnection(conexao);
                return con;
            }
            catch (SqlException ex)
            { 
                MessageBox.Show("Banco não conectado");
                return null;
            }
        }
    }
}
