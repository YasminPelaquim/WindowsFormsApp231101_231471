
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp231101_231471.Models;

namespace WindowsFormsApp231101_231471.Views
{
    public partial class FrmClientes : Form
    {
        Cidade ci;
        Clientes cl;
        public FrmClientes()
        {
            InitializeComponent();
        }
        void LimpaControles()
        {
            txtID.Clear();
            txtNome.Clear();
            cboCidades.SelectedIndex = -1;
            txtUF.Clear();
            mskCPF.Clear();
            txtRenda.Clear();
            dtpDataNasc.Value = DateTime.Now;
            picFoto.ImageLocation = "";
            chkVenda.Checked = false;
        }

        void carregarGrid(string pesquisa)
        {
            cl = new Clientes()
            {
                nome = pesquisa
            };
            dgvClientes.DataSource = cl.Consultar();
        }

        
        private void FrmClientes_Load(object sender, EventArgs e)
        {
            //Cria um objeto do tipo cidade
            // e alimenta o combobox
            ci = new Cidade();
            cboCidades.DataSource = ci.Consultar();
            cboCidades.DisplayMember = "nome";
            cboCidades.ValueMember = "id";

            LimpaControles();
            carregarGrid("");

            //Deixa invisivel colunas do Grid
            dgvClientes.Columns["idCidade"].Visible = false;
            dgvClientes.Columns["foto"].Visible = false;

        }

        private void cboCidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidades.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboCidades.SelectedItem;
                txtUF.Text = reg["uf"].ToString();
            }
        }

        private void picFoto_Click(object sender, EventArgs e)
        {
            ofdArquivo.InitialDirectory = "D:/fotos/clientes/";
            ofdArquivo.FileName = "";
            ofdArquivo.ShowDialog();
            picFoto.ImageLocation = ofdArquivo.FileName;
        }

        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvClientes.CurrentRow.Cells["id"].Value.ToString();
            txtNome.Text = dgvClientes.CurrentRow.Cells["nome"].Value.ToString();
            cboCidades.Text = dgvClientes.CurrentRow.Cells["cidade"].Value.ToString();
            txtUF.Text = dgvClientes.CurrentRow.Cells["uf"].Value.ToString();
            chkVenda.Checked = (bool)dgvClientes.CurrentRow.Cells["venda"].Value;
            mskCPF.Text = dgvClientes.CurrentRow.Cells["cpf"].Value.ToString();
            txtRenda.Text = dgvClientes.CurrentRow.Cells["renda"].Value.ToString();
            picFoto.ImageLocation = dgvClientes.CurrentRow.Cells["foto"].Value.ToString();
        }


        private void btnIncluir_Click_1(object sender, EventArgs e)
        {
            if (txtNome.Text == "") return;

            cl = new Clientes()
            {
                nome = txtNome.Text,
                idCidade = (int)cboCidades.SelectedValue,
                dataNasc = dtpDataNasc.Value,
                renda = double.Parse(txtRenda.Text),
                cpf = mskCPF.Text,
                foto = picFoto.ImageLocation,
                venda = chkVenda.Checked
            };
            cl.Incluir();

            LimpaControles();
            carregarGrid("");
        }

        private void btnAlterar_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            cl = new Clientes()
            {
                id = int.Parse(txtID.Text),
                nome = txtNome.Text,
                idCidade = (int)cboCidades.SelectedValue,
                renda = double.Parse(txtRenda.Text),
                cpf = mskCPF.Text,
                foto = picFoto.ImageLocation,
                venda = chkVenda.Checked
            };
            cl.Alterar();

            LimpaControles();
            carregarGrid("");
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            LimpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;

            if (MessageBox.Show("Deseja excluir o cliente?", "Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cl = new Clientes()
                {
                    id = int.Parse(txtID.Text),
                };

                cl.Excluir();

                LimpaControles();
                carregarGrid("");
            }
        }


        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }
    }
}
    
