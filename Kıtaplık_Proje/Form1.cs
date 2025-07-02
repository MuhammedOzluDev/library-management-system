using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kıtaplık_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection bgl= new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ozlum\\OneDrive\\Desktop\\Kitaplık.mdb");
        string durum;
        void Listele() { 
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Kitaplar",bgl);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bgl.Open();
            OleDbCommand cmd = new OleDbCommand("insert into Kitaplar(KitapAd,Yazar,Tür,SayfaSayısı,Durum) values(@p1,@p2,@p3,@p4,@p5)", bgl);
            cmd.Parameters.AddWithValue("@p1",txtName.Text);
            cmd.Parameters.AddWithValue("@p2", txtAuthor.Text);
            cmd.Parameters.AddWithValue("@p3", cmbGenre.Text);
            cmd.Parameters.AddWithValue("@p4", txtNumber.Text);
            cmd.Parameters.AddWithValue("@p4", durum);
            cmd.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Added");
            Listele();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtName.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAuthor.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtNumber.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbGenre.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bgl.Open();
            OleDbCommand cmd = new OleDbCommand("Delete from Kitaplar where KitapId=@p1",bgl);
            cmd.Parameters.AddWithValue("@p1",txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Deleted");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bgl.Open();
            OleDbCommand cmd = new OleDbCommand("Update Kitaplar set KitapAd=@p1,Yazar=@p2,Tür=@p3,SayfaSayısı=@p4,Durum=@p5 where KitapId=@p6", bgl);
            cmd.Parameters.AddWithValue("@p1", txtName.Text);
            cmd.Parameters.AddWithValue("@p2", txtAuthor.Text);
            cmd.Parameters.AddWithValue("@p3", cmbGenre.Text);
            cmd.Parameters.AddWithValue("@p4", txtNumber.Text);
            if (radioButton1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@p5", durum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@p5", durum);
            }
            cmd.Parameters.AddWithValue("@p6", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Updated");
            Listele();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bgl.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from Kitaplar where KitapAd=@p1",bgl);
            cmd.Parameters.AddWithValue("@p1",txtFind.Text);
            DataTable dt= new DataTable();
            OleDbDataAdapter da= new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.Close();
        }

        private void btnSeacrhv_Click(object sender, EventArgs e)
        {
            bgl.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from Kitaplar where KitapAd like'%"+txtFind.Text+ "%'", bgl);
            cmd.Parameters.AddWithValue("@p1", txtFind.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.Close();
        }
    }
}
