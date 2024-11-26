using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp231101_231471.Models
{
    public class Produto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int idCategoria { get; set; }
        public int idMarca { get; set; }
        public decimal estoque { get; set; }
        public decimal valorVenda { get; set; }

        public void Incluir()
        {
            try
            {
                Banco.AbrirConexao();
                Banco.Comando = new MySqlCommand("INSERT INTO produtos(descricao,idCategoria,idMarca, estoque,valorVenda) " +
                    " VALUES (@descricao, @idCategoria, @idMarca, @estoque, @valorVenda)", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.Comando.Parameters.AddWithValue("@idCategoria", idCategoria);
                Banco.Comando.Parameters.AddWithValue("@idMarca", idMarca);
                Banco.Comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.Comando.Parameters.AddWithValue("@valorVenda", valorVenda);
                Banco.Comando.ExecuteNonQuery();
                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Alterar()
        {
            try
            {
                Banco.AbrirConexao();
                Banco.Comando = new MySqlCommand("Update produtos set descricao = @descricao, idCategoria= @idCategoria, idMarca = @idMarca, estoque = @estoque, valorVenda = @valorVenda where id = @id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.Comando.Parameters.AddWithValue("@idCategoria", idCategoria);
                Banco.Comando.Parameters.AddWithValue("@idMarca", idMarca);
                Banco.Comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.Comando.Parameters.AddWithValue("@valorVenda", valorVenda);
                Banco.Comando.Parameters.AddWithValue("@id", id);
                Banco.Comando.ExecuteNonQuery();
                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Excluir()
        {
            try
            {
                Banco.AbrirConexao();
                Banco.Comando = new MySqlCommand("delete from produtos where id = @id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@id", id);
                Banco.Comando.ExecuteNonQuery();
                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable Pesquisar()
        {
            try
            {
                Banco.AbrirConexao();
                Banco.Comando = new MySqlCommand("SELECT p.*, m.marca marca, c.categoria FROM " +
                                                 " Produtos p inner join Marcas m on (m.id = p.idMarca) " + "inner join Categorias c on (c.id = p.idCategoria) " + "where p.descricao like @descricao order by p.descricao", Banco.Conexao);                                               
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao + "%");
                Banco.Adaptador = new MySqlDataAdapter(Banco.Comando);
                Banco.datTabela = new DataTable();
                Banco.Adaptador.Fill(Banco.datTabela);
                return Banco.datTabela;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
