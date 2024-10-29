using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp231101_231471.Models;

namespace WindowsFormsApp231101_231471.Views
{
    public partial class FrmCategorias : Form
    {
        Categoria c;
        public FrmCategorias()
        {
            InitializeComponent();
        }

        void LimpaControles()
        {
            txtCodigo.Clear();
            txtCategoria.Clear();
            txtPesquisa.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            c = new Categoria()
            {
                categoria = pesquisa
            };
            dgvCategorias.DataSource = c.Consultar();
        }

        private void DgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.RowCount > 0)
            {
                txtCodigo.Text = dgvCategorias.CurrentRow.Cells[0].Value.ToString();
                txtCategoria.Text = dgvCategorias.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {

            LimpaControles();
            carregarGrid("");
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtCategoria.Text == String.Empty) return;

            c = new Categoria()
            {
                categoria = txtCategoria.Text,
            };

            c.Incluir();

            LimpaControles();
            carregarGrid("");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == String.Empty) return;

            c = new Categoria()
            {
                id = int.Parse(txtCodigo.Text),
                categoria = txtCategoria.Text,

            };
            c.Alterar();

            LimpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "") return;

            if (MessageBox.Show("Deseja excluir a categoria?", "Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                c = new Categoria()
                {
                    id = int.Parse(txtCodigo.Text),
                };

                c.Excluir();
                LimpaControles();
                carregarGrid("");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            carregarGrid(txtPesquisa.Text);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
