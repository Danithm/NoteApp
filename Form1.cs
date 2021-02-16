using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtMessage.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Connection String - REMOVE INFO BEFORE PUSH
                string mySqlConnectString = "Server=localhost;Database=note_app_schema;Uid=root;pwd=placeholder";

                //Insert Query
                string Query = "insert into note_app_schema.note_table(note_title, note_msg) values('" +this.txtTitle.Text+ "','" +this.txtMessage.Text+ "'); ";

                //MySql object to pass connection string
                MySqlConnection myConnection = new MySqlConnection(mySqlConnectString);

                //MySql command class to handle query and connection
                MySqlCommand myCommand = new MySqlCommand(Query, myConnection);

                //MySql data reader, to handle sql data
                MySqlDataReader myReader;

                //Open connection
                myConnection.Open();

                //Execute Query
                myReader = myCommand.ExecuteReader();
                MessageBox.Show("Saved Data");

                //Reads until done, then closes connection
                while (myReader.Read())
                {
                }
                //Clear boxes when saved
                txtTitle.Clear();
                txtMessage.Clear();

                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            //Ensures selection
            if( index > -1)
            {
                txtTitle.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                txtMessage.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Connection String - REMOVE INFO BEFORE PUSH
                string mySqlConnectString = "Server=localhost;Database=note_app_schema;Uid=root;Pwd=placeholder";

                //Delete Query - only deletes if note is loaded(Read) into title+msg boxes
                //TODO: Refactor to grab current cell and use note id
                string Query = "delete from note_app_schema.note_table where note_title='" +this.txtTitle.Text + "';";

                //MySql object to pass connection string
                MySqlConnection myConnection = new MySqlConnection(mySqlConnectString);

                //MySql command class to handle query and connection
                MySqlCommand myCommand = new MySqlCommand(Query, myConnection);

                //MySql data reader, to handle sql data
                MySqlDataReader myReader;

                //Open connection
                myConnection.Open();

                //Execute Query
                myReader = myCommand.ExecuteReader();
                MessageBox.Show("Data Deleted");

                //Reads until done, then closes connection
                while (myReader.Read())
                {
                }
                //Clear boxes when deleted
                txtTitle.Clear();
                txtMessage.Clear();

                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //Connection String - REMOVE INFO BEFORE PUSH
                string mySqlConnectString = "Server=localhost;Database=note_app_schema;Uid=root;Pwd=placeholder";

                //Read Query
                string Query = "select * from note_app_schema.note_table;";

                //MySql object to pass connection string
                MySqlConnection myConnection = new MySqlConnection(mySqlConnectString);

                //MySql command class to handle query and connection
                MySqlCommand myCommand = new MySqlCommand(Query, myConnection);

                //MySql data adapter, to handle/fill sql data
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();

                myAdapter.SelectCommand = myCommand;

                //Load into table
                DataTable noteTable = new DataTable();
                myAdapter.Fill(noteTable);
                dataGridView1.DataSource = noteTable;
                MessageBox.Show("Loaded Data");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
