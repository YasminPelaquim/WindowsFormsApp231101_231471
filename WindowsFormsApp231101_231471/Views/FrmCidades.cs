using System;
using System.Windows.Forms;
using WindowsFormsApp231101_231471.Models;

namespace WindowsFormsApp231101_231471.Views
{
    public partial class FrmCidades : Form
    {
        Cidade c;
        public FrmCidades()
        {
            InitializeComponent();
        }

        void LimpaControles()
        {
            txtID.Clear();
            txtNome.Clear();
            txtUF.Clear();
            txtPesquisa.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            c = new Cidade()
            {
                nome = pesquisa
            };
            dgvCidades.DataSource = c.Consultar();
        }        

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == String.Empty) return;
            
            c = new Cidade()
            {
                nome = txtNome.Text,
                uf = txtUF.Text,
            };

            c.Incluir();

            LimpaControles();
            carregarGrid("");
        }   

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == String.Empty) return;

            c = new Cidade()
            {
                id = int.Parse(txtID.Text),
                nome = txtNome.Text,
                uf = txtUF.Text
            };
            c.Alterar();

            LimpaControles();
            carregarGrid("");
        }

        private void FrmCidades_Load_1(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void dgvCidades_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCidades.RowCount > 0)
            {
                txtID.Text = dgvCidades.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgvCidades.CurrentRow.Cells[1].Value.ToString();
                txtUF.Text = dgvCidades.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            if (MessageBox.Show("Deseja excluir a cidade?", "Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                c = new Cidade()
                {
                    id = int.Parse(txtID.Text),
                };

                c.Excluir();
                LimpaControles();
                carregarGrid("");
            }
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

       
    }
}
