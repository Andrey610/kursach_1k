using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Form1 C = new Form1();
                C.BackColor = Color.MistyRose;
                C.bot = "Лана: ";
                C.richTextBox1.BackColor = Color.Snow;
                C.textBox1.BackColor = Color.Snow;
                C.button1.BackColor = Color.MistyRose;
                C.button6.BackColor = Color.MistyRose;
                C.button2.BackColor = Color.MistyRose;
                Random rnd = new Random();
                int a = rnd.Next(1, 4);
                C.panel1.BackgroundImage = Image.FromFile(a + ".jpg");
                C.Show();
            }
            else if (checkBox2.Checked)
            {
                Form1 C = new Form1();               
                C.BackColor = Color.Lavender;
                C.bot = "Віктор: ";
                C.panel1.BackgroundImage = Image.FromFile("11.jpg");
                C.richTextBox1.BackColor = Color.AliceBlue;
                C.textBox1.BackColor = Color.AliceBlue;
                C.button1.BackColor = Color.Lavender;
                C.button6.BackColor = Color.Lavender;
                C.button2.BackColor = Color.Lavender;
                C.Show();
            }
            else
                label2.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox2.Enabled = false;
            if (checkBox1.Checked == false)
                checkBox2.Enabled = true;
            label2.Visible = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkBox1.Enabled = false;
            if (checkBox2.Checked == false)
                checkBox1.Enabled = true;
            label2.Visible = false;
        }

    }
}
