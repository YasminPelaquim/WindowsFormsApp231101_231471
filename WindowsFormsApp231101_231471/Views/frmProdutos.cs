using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp231101_231471.Models;

namespace WindowsFormsApp231101_231471.Views
{
    public partial class frmProdutos : Form
    {
       
        Produto p;
        Marca m;
        Categoria c;
        public frmProdutos()
        {
            InitializeComponent();
        }

        void LimpaControles()
        {
            txtID.Clear();
            txtDescricao.Clear();
            txtEstoque.Clear();
            txtValor.Clear();
            txtPesquisa.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            p = new Produto()
            {
                descricao = pesquisa
            };
            dgvProdutos.DataSource = p.Pesquisar();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtDescricao.Text == String.Empty) return;

            p = new Produto()
            {               
                descricao = txtDescricao.Text,
                idCategoria = (int)cboCategoria.SelectedValue,
                idMarca = (int)cboMarca.SelectedValue,
                estoque = decimal.Parse(txtEstoque.Text),
                valorVenda = decimal.Parse(txtValor.Text),
            };

            p.Incluir();

            LimpaControles();
            carregarGrid("");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            p = new Produto()
            {
                id = int.Parse(txtID.Text),
                descricao = txtDescricao.Text,
                idCategoria = (int)cboCategoria.SelectedValue,
                idMarca = (int)cboMarca.SelectedValue,
                estoque = decimal.Parse(txtEstoque.Text),
                valorVenda = decimal.Parse(txtValor.Text),
            };
            p.Alterar();

            LimpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            if (MessageBox.Show("Deseja excluir o Produto?", "Exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                p = new Produto()
                {
                    id = int.Parse(txtID.Text),
                };
                p.Excluir();

                LimpaControles();
                carregarGrid("");
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProdutos.RowCount > 0)
            {
                txtID.Text = dgvProdutos.CurrentRow.Cells["id"].Value.ToString();
                txtDescricao.Text = dgvProdutos.CurrentRow.Cells["descricao"].Value.ToString();
                cboCategoria.Text = dgvProdutos.CurrentRow.Cells["categoria"].Value.ToString();
                cboMarca.Text = dgvProdutos.CurrentRow.Cells["marca"].Value.ToString();
                txtEstoque.Text = dgvProdutos.CurrentRow.Cells["estoque"].Value.ToString();
                txtValor.Text = dgvProdutos.CurrentRow.Cells["valorVenda"].Value.ToString();
            }
        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {
            m = new Marca();
            cboMarca.DataSource = m.Consultar();
            cboMarca.DisplayMember = "marca";
            cboMarca.ValueMember = "id";

            c = new Categoria();
            cboCategoria.DataSource = c.Consultar();
            cboCategoria.DisplayMember = "categoria";
            cboCategoria.ValueMember = "id";

            LimpaControles();
            carregarGrid("");

            dgvProdutos.Columns["idMarca"].Visible = false;
            dgvProdutos.Columns["idCategoria"].Visible = false;
        }
    }
}
