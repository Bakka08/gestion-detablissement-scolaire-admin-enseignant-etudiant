﻿using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace School
{
    public partial class Gteacher : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=school; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='school';username=root;password=");

        MySqlDataAdapter adapter, adapter2, adapter3;

        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        string nom;
        string prenom;
        string currentindex;

        string id;
        public Gteacher(string id )
        {
            this.id = id;
            InitializeComponent();
          
            loadsession(id);
            loadteacher();
            guna2Button8.Enabled = false;
            guna2Button9.Enabled = false;
        }
        private void loadsession(string d)
        {
            adapter = new MySqlDataAdapter("SELECT * FROM `admin` WHERE `id` = '" + d + "'", connection); ;
            adapter.Fill(table);


            int i;
            String[] myArray = new String[8];
            foreach (DataRow dataRow in table.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                nom = myArray[1];
                prenom = myArray[2];


                session.Text = "Bonjour " + nom + " " + prenom;
                guna2HtmlLabel2.Text = "admin";




            }

        }
        private void loadteacher()
        {

            studentlist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from teacher;";

            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            int i;
            String[] myArray = new String[10];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }






                studentlist.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4], myArray[6]) ;

            }






        }

        private void studentlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = this.studentlist.Rows[e.RowIndex];
            currentindex = row.Cells[0].Value.ToString();
            nomtxt.Text = row.Cells[1].Value.ToString();
            prenomtxt.Text = row.Cells[2].Value.ToString();
            telephonetxt.Text = row.Cells[3].Value.ToString();
            emailtxt.Text = row.Cells[4].Value.ToString();
            matierecombo.Text = row.Cells[5].Value.ToString();
            
            guna2Button8.Enabled = true;
            guna2Button9.Enabled = true;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier les informations  ", "Modifier une appartement", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (emailtxt.Text == "" || nomtxt.Text == "" || prenomtxt.Text == "" || mdptxt.Text == "" || matierecombo.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();
                    MySqlCommand cmd2 = maconnexion.CreateCommand();
                    cmd2.CommandText = "UPDATE teacher SET nom = @nom,prenom = @prenom ,telephone=@telephone, email = @email ,mdp = @mdp ,matiere = @matiere WHERE id=" + currentindex;
                    cmd2.Parameters.AddWithValue("@nom", nomtxt.Text);
                    cmd2.Parameters.AddWithValue("@prenom", prenomtxt.Text);
                    cmd2.Parameters.AddWithValue("@telephone", telephonetxt.Text);
                    cmd2.Parameters.AddWithValue("@email", emailtxt.Text);
                    cmd2.Parameters.AddWithValue("@mdp", mdptxt.Text);
                    cmd2.Parameters.AddWithValue("@groupe_id", matierecombo);

                    cmd2.ExecuteNonQuery();
                    maconnexion.Close();

                    AdminDash a = new AdminDash(id);
                    a.Show();
                    this.Hide();

                }
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            guna2Button9.Enabled = false;
            guna2Button8.Enabled = false;

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            MySqlCommand cmd = maconnexion.CreateCommand();
            cmd.CommandText = "DELETE FROM teacher WHERE id=" + currentindex;
            cmd.ExecuteNonQuery();
            maconnexion.Close();
            AdminDash a = new AdminDash(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AdminDash a = new AdminDash(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Gteacher a =new Gteacher(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Ggroupe a =new Ggroupe(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Gseance a = new Gseance(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Gabsence a = new Gabsence(id);
            a.Show();
            this.Hide();
        }

        private void X_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }
            else if (dialogClose == DialogResult.Cancel)
            {
                //do something else
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (nomtxt.Text == "" || prenomtxt.Text == "" || emailtxt.Text == "" || mdptxt.Text == "" || matierecombo.Text == ""||telephonetxt.Text == "")
            {
                MessageBox.Show("remplir tout les case");
            }
            else
            {



   
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "INSERT INTO teacher (id ,nom , prenom,telephone,email,mdp,matiere) VALUES (@id,@nom , @prenom ,@telephone, @email,@mdp , @matiere)";
                cmd2.Parameters.AddWithValue("@id", "null");
                cmd2.Parameters.AddWithValue("@nom", nomtxt.Text);
                cmd2.Parameters.AddWithValue("@prenom", prenomtxt.Text);
                cmd2.Parameters.AddWithValue("@telephone", telephonetxt.Text);
                cmd2.Parameters.AddWithValue("@email", emailtxt.Text);
                cmd2.Parameters.AddWithValue("@mdp", mdptxt.Text);
                cmd2.Parameters.AddWithValue("@matiere", matierecombo.Text);

                cmd2.ExecuteNonQuery();
                maconnexion.Close();

                Gteacher a = new Gteacher(id);
                a.Show();
                this.Hide();
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
