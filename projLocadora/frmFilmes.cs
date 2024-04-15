using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projLocadora
{
    public partial class frmFilmes : Form
    {
        int registroAtual = 0;
        int totalRegistros = 0;
        DataTable dtProdutoras = new DataTable();
        String connectionString = @"Server=darnassus\motorhead;Database=db_230570; User Id=230570;Password=fodase123;";
        bool novo;
        DataTable dtFilmes = new DataTable();

        private void navegar()
        {
            carregaComboProdutoras();
            txtCodFilme.Text = dtFilmes.Rows[registroAtual][0].ToString();
            txtTituloFilme.Text = dtFilmes.Rows[registroAtual][1].ToString();
            txtAnoLancamento.Text = dtFilmes.Rows[registroAtual][2].ToString();
            //cmBoxProd.Text = dtFilmes.Rows[registroAtual][3].ToString();
            cmBoxGenFilme.Text = dtFilmes.Rows[registroAtual][4].ToString();
        }

        private void carregaTudoProdutoras()
        {
            dtProdutoras = new DataTable();
            string sql = "Select * from tblProdutora";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutoras.Load(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            cmBoxProd.DataSource = dtProdutoras;
            cmBoxProd.DisplayMember = "nomeProd";
            cmBoxProd.ValueMember = "codProd";
        }

        private void carregaComboProdutoras()
        {
            dtProdutoras = new DataTable();
            string sql = "Select * from tblProdutora Where codProd=" +
                dtFilmes.Rows[registroAtual][3].ToString();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutoras.Load(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            cmBoxProd.DataSource = dtProdutoras;
            cmBoxProd.DisplayMember = "nomeProd";
            cmBoxProd.ValueMember = "codProd";
        }
        public frmFilmes()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        

        private void frmFilmes_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodFilme.Enabled = false;
            txtTituloFilme.Enabled = false;
            txtAnoLancamento.Enabled = false;
            cmBoxProd.Enabled = false;
            cmBoxGenFilme.Enabled = false;

            string sql = "SELECT * FROM tblFilme";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtFilmes.Load(reader);
                    totalRegistros = dtFilmes.Rows.Count;
                    registroAtual = 0;
                    navegar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (registroAtual < totalRegistros - 1)
            {
                registroAtual++;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual--;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (registroAtual < totalRegistros - 1)
            {
                registroAtual = totalRegistros - 1;
                navegar();
            }
        }

        private void BtnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual = 0;
                navegar();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM tblFilme WHERE codFilme=" + txtCodFilme.Text;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Filme apagado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally { con.Close(); }
            dtFilmes = new DataTable();
            this.frmFilmes_Load(this, e);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            txtTituloFilme.Enabled = true;
            txtTituloFilme.Clear();
            txtAnoLancamento.Enabled = true;
            txtAnoLancamento.Clear();
            cmBoxGenFilme.Enabled = true;
            cmBoxGenFilme.SelectedIndex = 0;
            cmBoxProd.Enabled = true;

            txtTituloFilme.Enabled = true;
            txtAnoLancamento.Enabled = true;
            cmBoxGenFilme.Enabled = true;
            cmBoxProd.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            BtnPrimeiro.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;
            btnExcluir.Enabled = false;
            carregaTudoProdutoras();
            cmBoxProd.SelectedIndex = 0;
            btnAlterar.Enabled = false;
            txtCodFilme.Clear();

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            txtTituloFilme.Enabled = true;
            txtAnoLancamento.Enabled = true;
            cmBoxGenFilme.Enabled = true;
            cmBoxProd.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            BtnPrimeiro.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;
            btnExcluir.Enabled = false;
            carregaTudoProdutoras();
            btnAlterar.Enabled = false;
        }
       private void btnSalvar_Click(object sender, EventArgs e)
       {
            if (novo)
            {
                string sql = "insert into tblFilme (tituloFilme, " +
                    "anoFilme, codProd, generoFilme) values (' " +
                    txtTituloFilme.Text + " ' , " + txtAnoLancamento.Text +
                    ",  " + cmBoxProd.SelectedValue.ToString() +
                    " , ' " + cmBoxGenFilme.Text + " ')";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Filme cadastrado com sucesso");
                        this.frmFilmes_Load(this, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                txtCodFilme.Enabled = false;
                txtAnoLancamento.Enabled = false;
                cmBoxGenFilme.Enabled = false;
                cmBoxProd.Enabled = false;

                btnSalvar.Enabled = false;
                btnNovo.Enabled = true;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;

                BtnPrimeiro.Enabled = true;
                btnAnterior.Enabled = true;
                btnProximo.Enabled = true;
                btnUltimo.Enabled = true;
            }
            else
            {
                    string sql = "update tblFilme set tituloFilme='" + txtTituloFilme.Text + 
                        " ', anoFilme= " + txtAnoLancamento.Text + 
                        ", codProd=" + cmBoxProd.SelectedValue.ToString() + 
                        ", generoFilme='" + cmBoxGenFilme.Text + "' where codFilme=" + txtCodFilme.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Filme alterado com sucesso");
                        this.frmFilmes_Load(this, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                txtCodFilme.Enabled = false;
                txtAnoLancamento.Enabled = false;
                cmBoxGenFilme.Enabled = false;
                cmBoxProd.Enabled = false;
                
                btnSalvar.Enabled = false;
                btnNovo.Enabled = true;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                
                BtnPrimeiro.Enabled = true;
                btnAnterior.Enabled = true;
                btnProximo.Enabled = true;
                btnUltimo.Enabled = true;
                dtFilmes = new DataTable();
                frmFilmes_Load(this, e);
            }
       } 
    
    }
    
}
