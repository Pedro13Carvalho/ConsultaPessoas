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
                string conexao = @"Data Source=LAPTOP-A3KIM76B\SQLEXPRESS;Initial Catalog=Recepcao; Integrated Security=true;";
                string criaBanco = 
                   "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DataBase')"+
                      "BEGIN"+
                        "CREATE DATABASE Recepcao"+
                        "CREATE TABLE USUARIOS("+
                        "id int primary key NOT NULL IDENTITY(1, 1),"+
                        "nome varchar(40),"+
                        "sobrenome varchar(50),"+
                        "idade int"+
                        ");"+
                   "END";

                SqlConnection con;
                con = new SqlConnection(conexao);
                return con;
                SqlCommand cmd = new SqlCommand(criaBanco, con);
            }
            catch (SqlException ex)
            { 
                MessageBox.Show("Banco não conectado");
                return null;
            }
        }
    }
}
