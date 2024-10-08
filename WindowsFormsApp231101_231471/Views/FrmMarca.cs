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
    public partial class FrmMarca : Form
    {
        Marca m;
        public FrmMarca()
        {
            InitializeComponent();
        }

        void LimpaControles()
        {
            txtID.Clear();
            txtMarca.Clear();
            txtPesquisa.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            m = new Marca()
            {
                marca = pesquisa
            };
            dgvMarca.DataSource = m.Consultar();
        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (txtMarca.Text == String.Empty) return;

            m = new Marca()
            {
                marca = txtMarca.Text,               
            };

            m.Incluir();

            LimpaControles();
            carregarGrid("");
        }

        private void dgvMarca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMarca.RowCount > 0)
            {
                txtID.Text = dgvMarca.CurrentRow.Cells[0].Value.ToString();
                txtMarca.Text = dgvMarca.CurrentRow.Cells[1].Value.ToString();              
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == String.Empty) return;

            m = new Marca()
            {
                id = int.Parse(txtID.Text),
                marca = txtMarca.Text,
              
            };
            m.Alterar();

            LimpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            if (MessageBox.Show("Deseja excluir a marca?", "Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                m = new Marca()
                {
                    id = int.Parse(txtID.Text),
                };

                m.Excluir();
                LimpaControles();
                carregarGrid("");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
