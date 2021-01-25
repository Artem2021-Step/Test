using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dovidnuk
{
    public partial class FormUser : Form
    {
        public FormUser()
        {
            InitializeComponent();
        }

        public void buttonOk_Click_Add(object sender, EventArgs e)
        {
            try
            {
                DataRow row = dov.table.NewRow();
                row["prizv"] =textBox1.Text.Trim();
                row["names"] = textBox2.Text.Trim();
                row["adresa"] =textBox3.Text.Trim();
                row["telefon"] =textBox4.Text.Trim();                  
                row["data"] = dateTimePicker1.Value.Date;
                row["Школа"] = comboBox1.Text.Trim();
                dov.table.Rows.Add(row);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додаванні нового запису.\r\n" + ex.Message, "Помилка при додаванні", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        public void buttonOk_Click_Edit(object sender, EventArgs e)
        {
            try
            {
                dov.table.Rows[dov.currentRow]["prizv"] = textBox1.Text.Trim();
                dov.table.Rows[dov.currentRow]["names"] = textBox2.Text.Trim();
                dov.table.Rows[dov.currentRow]["adresa"] = textBox3.Text.Trim();
                dov.table.Rows[dov.currentRow]["telefon"] = textBox4.Text.Trim(); 
                dov.table.Rows[dov.currentRow]["data"] = dateTimePicker1.Value.Date;
                dov.table.Rows[dov.currentRow]["Школа"] = comboBox1.Text.Trim();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при редагуванні даних.\r\n" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            if (dov.currentRow >= 0)
      {
        textBox1.Text =dov.table.Rows[dov.currentRow]["prizv"].ToString();
        textBox2.Text = dov.table.Rows[dov.currentRow]["names"].ToString();
        textBox3.Text = dov.table.Rows[dov.currentRow]["adresa"].ToString();
        textBox4.Text = dov.table.Rows[dov.currentRow]["telefon"].ToString();
        comboBox1.Text = dov.table.Rows[dov.currentRow]["Школа"].ToString();
         try
        {dateTimePicker1.Value = DateTime.Parse(dov.table.Rows[dov.currentRow]["data"].ToString());}
        catch (Exception)
        {
          //MessageBox.Show("Не удалось получить Срок. Заполните вручную.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

         }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Закрити форму
            Close();
        }
    }
}
