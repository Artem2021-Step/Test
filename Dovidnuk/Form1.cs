using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Dovidnuk
{
    public partial class Form1 : Form
        {
           
        
        public Form1()
        {
            InitializeComponent();
        }
      //  BindingSource bind;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Відкриття форми
            
          //dov.connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=baza.mdb");
           dov.connection= new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=baza.mdb;Persist Security Info=False");      

          dov.adapter = new OleDbDataAdapter("SELECT * FROM Tab", dov.connection);
  
         // dov.adapter = new OleDbDataAdapter("SELECT id AS Номер,prizv AS Прізвище,[names] AS Імя, adresa AS Адреса,telefon AS Телефон ,data AS Датанародж FROM Tab", dov.connection);

         // Вивести шапку таблиці
            
           // Вставка
          dov.adapter.InsertCommand = new OleDbCommand("INSERT INTO Tab (id,prizv, [names], adresa, telefon, data, [Школа]) " + "VALUES (?,?,?,?,?,?,?)", dov.connection);
          dov.adapter.InsertCommand.Parameters.Add("@id", OleDbType.Integer, 255, "id");
          dov.adapter.InsertCommand.Parameters.Add("@prizv", OleDbType.VarChar, 255, "prizv");
          dov.adapter.InsertCommand.Parameters.Add("@names", OleDbType.VarChar, 255, "names");
          dov.adapter.InsertCommand.Parameters.Add("@adresa", OleDbType.VarChar, 255, "adresa");
          dov.adapter.InsertCommand.Parameters.Add("@telefon", OleDbType.VarChar, 255, "telefon");
          dov.adapter.InsertCommand.Parameters.Add("@data", OleDbType.VarChar, 255, "data");
          dov.adapter.InsertCommand.Parameters.Add("@Школа", OleDbType.VarChar, 255, "Школа");

          // Вилучення
          dov.adapter.DeleteCommand = new OleDbCommand("DELETE * FROM Tab WHERE id = ?", dov.connection);
          dov.adapter.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 255, "id");
            // Оновлення
          dov.adapter.UpdateCommand = new OleDbCommand("UPDATE Tab SET prizv = ?, [names] = ?, adresa = ?, telefon = ?, data = ?, [Школа] =?  WHERE id= ?", dov.connection);
          dov.adapter.UpdateCommand.Parameters.Add("@prizv", OleDbType.VarChar, 255, "prizv");
          dov.adapter.UpdateCommand.Parameters.Add("@names", OleDbType.VarChar, 255, "names");
          dov.adapter.UpdateCommand.Parameters.Add("@adresa", OleDbType.VarChar, 255, "adresa");
          dov.adapter.UpdateCommand.Parameters.Add("@telefon", OleDbType.VarChar, 255, "telefon");
          dov.adapter.UpdateCommand.Parameters.Add("@data", OleDbType.VarChar, 255, "data");
          dov.adapter.UpdateCommand.Parameters.Add("@Школа", OleDbType.VarChar, 255, "Школа");
          dov.adapter.UpdateCommand.Parameters.Add("@id", OleDbType.Integer, 255, "id");
         
            dov.table = new DataTable("Tab");
            dov.adapter.FillSchema(dov.table, SchemaType.Source);
            dov.adapter.Fill(dov.table);
            dov.dataView = new DataView(dov.table);
            dataGridView1.DataSource = dov.dataView;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {// Закривання форми
            if (dov.connection.State == ConnectionState.Open)
            {
                dov.connection.Close();
            }
        }

       
        private void buttonDelete_Click(object sender, EventArgs e)// Вилучення запису
        {
            if (dataGridView1.RowCount < 1)
            {
                return;
            }

            if (MessageBox.Show("Вилучити вибраний запис?", "Видалити", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.OK)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                dov.adapter.Update(dov.table);
                dov.table.AcceptChanges();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)//Редагування запису
        {
            if (dataGridView1.RowCount < 1 || dataGridView1.CurrentRow == null)
            {
                return;
            }

            FormUser frUser = new FormUser();
            frUser.buttonOk.Click += frUser.buttonOk_Click_Edit;
            frUser.Text = ((Button)sender).Text;

            dov.currentRow = dataGridView1.CurrentRow.Index;

            if (frUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dov.adapter.Update(dov.table);
                dov.table.AcceptChanges();
            }

            dov.currentRow = -1;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonEdit.PerformClick();
        }

        private void buttonSearch_Click(object sender, EventArgs e)  // Пошук
        {
            string query = "";
            foreach (DataColumn col in dov.table.Columns)
            {
                if (col.DataType == typeof(string))
                {
                    query += col.ColumnName + " LIKE '%" + textBoxSearch.Text + "%' OR ";
                }

            }
            query = query.Substring(0, query.Length - 4);

            dov.dataView.RowFilter = query;
        }

        private void button5_Click(object sender, EventArgs e)// Скасувати пошук, фiльтр
        {
            textBoxSearch.Text = "";
            buttonSearch.PerformClick();
        }

        
        private void buttonAdd_Click(object sender, EventArgs e)// Додати новий запис
        {
            FormUser frUser = new FormUser();
            frUser.buttonOk.Click += frUser.buttonOk_Click_Add;
            frUser.Text = ((Button)sender).Text;

            if (frUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dov.adapter.Update(dov.table);
                dov.table.AcceptChanges();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

       

       
    }
   
   
}
