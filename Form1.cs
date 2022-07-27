using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SqlConnection conexao = ConexaoBanco.ConectaDB();

        //Realiza a conexão com o banco de dados
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Ativa todos as funcionalidades da aplicação e desativa a si mesmo para não abrir a mesma conexão mais de uma vez
                conexao.Open();
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                txtId.Enabled = true;
                txtNome.Enabled = true;
                txtSobrenome.Enabled = true;
                txtIdade.Enabled = true;
                txtPesquisa.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro ao se conectar com o banco de dados.");
            }
        }

        //Fecha a conexão com o banco de dados
        private void button2_Click(object sender, EventArgs e)
        {
            try 
            {
                //Desativa todas as funcionalidades da aplicação exceto o botão de abrir conexão
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                txtId.Enabled = false;
                txtNome.Enabled = false;
                txtSobrenome.Enabled = false;
                txtIdade.Enabled = false;
                txtPesquisa.Enabled = false;
                dgvPessoas.DataSource = null;
            }
            catch 
            {
                MessageBox.Show("Houve um erro ao desconectar com o banco.");
            }
            finally
            {
                conexao.Close();
            }
        }

        //Lista os usuarios presentes no banco
        private void button3_Click(object sender, EventArgs e)
        {
            //Busca todos os usuarios cadastrados no banco
            string sqlQuery = "USE Recepcao;" +
                "SELECT * FROM USUARIOS;";
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new(sqlQuery,conexao);
            
            dataAdapter.Fill(dataTable);
            dgvPessoas.DataSource = dataTable;
        }

        //Insere um novo usuario no banco
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //Registra os dados a partir das informações inseridas nas caixas de texto com os respectivos nomes
                string sqlQuery = "USE Recepcao;" +
                    "INSERT INTO USUARIOS (nome,sobrenome,idade)" +
                    "VALUES('" + txtNome.Text + "','" + txtSobrenome.Text + "'," + Convert.ToInt32(txtIdade.Text) + ");";
                SqlCommand cmd = new SqlCommand(sqlQuery, conexao);

                cmd.ExecuteNonQuery();
                txtId.Clear();
                txtNome.Clear();
                txtSobrenome.Clear();
                txtIdade.Clear();
            }
            catch 
            {
                MessageBox.Show("Dados inseridos incorretamente, favor conferir.");
            }
        }

        //Atualiza um usuario novo a partir de seu ID
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //Substitui as informações do usuario de acordo com o ID inserido.
                string sqlQuery = "USE Recepcao;" +
                    "UPDATE USUARIOS SET nome='" + txtNome.Text + "',sobrenome='" + txtSobrenome.Text + "',idade=" + Convert.ToInt32(txtIdade.Text) +
                    "WHERE id=" + Convert.ToInt32(txtId.Text);
                SqlCommand cmd = new SqlCommand(sqlQuery, conexao);
                
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario atualizado com sucesso!");
            }
            catch 
            {
                MessageBox.Show("Usuario inexistente.");
            }
        }

        //Deleta o usuario do banco
        private void button6_Click(object sender, EventArgs e)
        {
           //Deleta o usuaro com o id inserido.
            string sqlQuery = "DELETE FROM USUARIOS "+
                "WHERE id=" + Convert.ToInt32(txtId.Text)+";";
            SqlCommand cmd = new SqlCommand(sqlQuery, conexao);
            
            cmd.ExecuteNonQuery();
            MessageBox.Show("Usuario deletado com sucesso!");
            
        }

        //Pesquisar pelo nome
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //Realiza a procura por todos os usuarios com o nome inserido
                string sqlQuery = "USE Recepcao;" +
                "SELECT * FROM USUARIOS WHERE nome='" + txtPesquisa.Text + "';";
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new(sqlQuery, conexao);

                //Preenche o Data Grid View com os dados pesquisados
                dataAdapter.Fill(dataTable);
                dgvPessoas.DataSource = dataTable;
            }
            catch 
            {
                MessageBox.Show("Usuario não encontrado.");
            }
        }
    }
}
