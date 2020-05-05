using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;


namespace test
{
    public partial class Form1 : Form
    {
        public string bot;
        public static string ConnectionString1 = @"Provider=Microsoft.Jet.OLEDB.4.0; " +
                                             "Data Source= 1.mdb";
        public static string ConnectionString2 = @"Provider=Microsoft.ACE.OLEDB.12.0; " +
                                            "Data Source= 1.mdb";

        private OleDbConnection connection;


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();
        public Form1()
        {
            InitializeComponent();

            try
            {
                connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString1;
                connection.Open();
            }
            catch(InvalidOperationException)
            {
                connection = new OleDbConnection();
                connection.ConnectionString = ConnectionString2;
                connection.Open();
            }
        }
      
        private volatile string Otvet;

        private volatile int p1;
        private volatile int p2;
        private volatile int p3;
        private volatile int p4;
        private volatile int timeDetector = 0;

        private volatile Boolean ViIzZP_1 = false;

        private volatile Boolean zanovoPO = false;
        private volatile Boolean vopros_4 = false;

        private volatile string[] oldOtvet = new string[15];

        private volatile string docCodeBol = " ";


        private volatile string rayenP;
        private volatile string nameBol;
        private volatile string nameDoc;

        private volatile string dataR;
        private volatile string timeR;

        private volatile string nameP;
        private volatile string surnameP;
        private volatile string numberP;
        private volatile int k_P;

        private volatile int animeVopros = 0;
        private volatile int pBol;
        private volatile int k_Doc;

        private volatile string[] names_doc = new string[10];
        private volatile string[] names_bol = new string[10];
        private volatile string[] name_rayena = new string[10];
        private volatile string[] kod_bol = new string[10];
        private volatile string[] zapisy = new string[20];
        private volatile string[] zapisy_2 = new string[200];
        private volatile int pZap;
        private volatile int pZap_2;

        private void button1_Click(object sender, EventArgs e)
        {
            Otvet = textBox1.Text;
            textBox1.Text = "";

            if (animeVopros < 0)
            {
                animeVopros = 0;
            }

            oldOtvet[animeVopros] = Otvet;

            if (animeVopros == 0)
            {
                Otvet = Otvet.ToLower();
                zpIF(Otvet);
            }
            else if (animeVopros == 1)
            {
                areaZP(Otvet);
            }
            else if (animeVopros == 2)
            {
                Otvet = Otvet.ToLower();
                hospitalZP(Otvet);
            }
            else if (animeVopros == 3)
            {
                doctorZP(Otvet);
            }
            else if (animeVopros == 4)
            {
                dataZP();
            }
            else if (animeVopros == 5)
            {
                button6.Enabled = false;
                timeZP(Otvet);
            }
            else if (animeVopros == 6)
            {
                nameYou(Otvet);
            }
            else if (animeVopros == 7)
            {
                textBox1.Text = "+380";
                textBox1.MaxLength = 13;
                surnameYou(Otvet);
            }
            else if (animeVopros == 8)
            {
                numberYou(Otvet);
            }
            else if (animeVopros == 9)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Ви вже записалися!";
            }
            textBox1.Focus();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
      
      
        public async void zpIF(string otvet)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;


            if (otvet == "так")
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: Так";              
                

                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "В якому районі ви проживаєте?";

                string sql = "SELECT Код, Назва FROM Areas";
                OleDbCommand myCommand = new OleDbCommand(sql, connection);
                OleDbDataReader reader = myCommand.ExecuteReader();
                p1 = 0;
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Для того, щоб обрати потрібний район введіть відповідний йому номер";
                while (reader.Read())
                {
                    richTextBox1.Text = richTextBox1.Text  + "\n" + reader[0].ToString() + " " + reader[1].ToString();
                    name_rayena[p1++] = reader[1].ToString();
                }
                reader.Close();
                p1--;
                ViIzZP_1 = true;
                animeVopros++;
            }
            else if (otvet == "ні")
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: Ні";
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Нажаль я можу працювати лише з лікарнями міста Запоріжжя";
            }
            else
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + otvet;
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Будь-ласка дайте відповідь так/ні";

            }

        }
        public async void areaZP(string otvet)
        {// 
            int a;
            try
            {             
                a = Int32.Parse(otvet) - 1;
                if (a >= 0 && a <= p1)
                {
                    rayenP = name_rayena[a];
                    richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + rayenP;
                    areaZP2(Int32.Parse(otvet));
                }
                else
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";
                }
            }
            catch (FormatException)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";
               
            }
            // 


        }

        public async void areaZP2(int bol)
        {

            string sql = "SELECT  Hospitals.Назва, Hospitals.Адреса,  Hospitals.Код  FROM Areas, Hospitals WHERE Areas.Код = Hospitals.Код_району AND " + "Areas.Код = " + bol;
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            OleDbDataReader reader = myCommand.ExecuteReader();
            int p = 1;
            p2 = 0;
            richTextBox1.Text = richTextBox1.Text + "\n" + bot;
            while (reader.Read())
            {
                richTextBox1.Text = richTextBox1.Text  + "\n" + p++ + " " + reader[0].ToString() + "\t" + reader[1].ToString();
                kod_bol[p - 2] = reader[2].ToString();
                names_bol[p2++] = reader[0].ToString();

            }
            reader.Close();
            pBol = p - 1;
            p2--;
            
            animeVopros++;
        }

        public async void hospitalZP(string otvet)
        {

            if (pBol == 1)
            {
             if(otvet == "так")
                {
                    nameBol = names_bol[0];
                    richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: Так";
                    docCodeBol = kod_bol[0];
                    hospitalZP_2(docCodeBol);
                }
             else if (otvet == "ні")
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: Ні";
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Інших лікарень в цьому районі немає";
                }
                else
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + otvet;
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Будь-ласка дайте відповідь так/ні";
                }
            }
            else
            {
                // 
                int a;
                try
                {
                    a = Int32.Parse(otvet) - 1;
                    if (a >= 0 && a <= p2)
                    {

                        nameBol = names_bol[a];
                        richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + nameBol;
                        int с = Int32.Parse(otvet) - 1;
                        docCodeBol = kod_bol[с];
                        hospitalZP_2(docCodeBol);

                    }
                    else
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";
                    }
                }
                catch (FormatException)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";

                }
                // 
            }

        }
    
        public async void hospitalZP_2(string bol)
        {
            label1.Text = nameBol;

            string sql = "SELECT  Doctors.Спеціальність, Doctors.Код FROM Doctors, Hospitals WHERE Doctors.Код_лікарні = Hospitals.Код AND " + "Doctors.Код_лікарні = " + bol;
            OleDbCommand myCommand = new OleDbCommand(sql, connection);
            OleDbDataReader reader = myCommand.ExecuteReader();
            int p = 1;
            p3 = 0;
            richTextBox1.Text = richTextBox1.Text + "\n" + bot;
            while (reader.Read())
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + p++ + " " + reader[0].ToString();
                kod_bol[p - 2] = reader[1].ToString();
                names_doc[p3++] = reader[0].ToString();
            }
            reader.Close();
            p3--;
            animeVopros++;
        }

        public async void doctorZP(string Otvet)
        {
            // 
            int a;
            try
            {
                a = Int32.Parse(Otvet) - 1;
                if (a >= 0 && a <= p3)
                {
                    nameDoc = names_doc[a];
                    richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + nameDoc;
                    int с = Int32.Parse(Otvet) - 1;
                    k_Doc = Int32.Parse(kod_bol[с]);
                    label2.Text = "" + nameDoc;
                    animeVopros++;
                    dataZP();

                }
                else
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";
                }
            }
            catch (FormatException)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";

            }

            // 

           
        }

        public async void dataZP()
        {
            richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Оберіть дату у полі нижче та натисніть кнопку Обрати";
            button6.Enabled = true;
            vopros_4 = true;
        }

        


        public async void timeZP(string otvet)
        {

            // 
            int a;
            try
            {
                a = Int32.Parse(otvet) - 1;
                if (a >= 0 && a <= p4)
                {
                    timeR = zapisy[a];
                    label6.Text = timeR;
                    richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + timeR;
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Для запису на: " + timeR + " введіть своє імя";
                    animeVopros++;

                }
                else
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";
                }
            }
            catch (FormatException)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Запис неможливий.";

            }
            
        }


        public async void nameYou(string otvet)
        {        
            nameP = otvet;
            richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + nameP;
            richTextBox1.Text = richTextBox1.Text + "\n" + bot +  "Тепер введіть свою фамілію";
            animeVopros++;

        }
        public async void surnameYou(string otvet)
        {
            surnameP = otvet;
            richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + surnameP;
            richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Введіть свій номер телефону";
            animeVopros++;

        }



        public async void numberYou(string otvet)
        {
         
            numberP = otvet;
            richTextBox1.Text = richTextBox1.Text + "\n" + "\t" + "Ви: " + numberP;
            label7.Text = numberP;
            
            dataSave();
            

        }

        public async void dataSave(){

            string sql = "INSERT INTO [User] ( [Імя] ,[Прізвище], [Телефон] ) VALUES ( '" + nameP + "' , '" + surnameP + "' , '" + numberP + "' )";
            OleDbCommand myCommand = new OleDbCommand(sql, connection);

            myCommand.ExecuteNonQuery();


            string sql3 = "SELECT Код FROM [User] WHERE [Імя] = '" + nameP + "' AND [Прізвище] = '" + surnameP + "' AND [Телефон] = '" + numberP + "'";
            myCommand = new OleDbCommand(sql3, connection);
            OleDbDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                k_P = Int32.Parse(reader[0].ToString());
            }
            reader.Close();


            string sql2 = "INSERT INTO [Registration] ( [Код_лікаря] , [Дата] , [Час], [Занято], [Код_користувача]) VALUES ( " + k_Doc + " , '" + dataR + "' , '" + timeR + "' ,  True  , " + k_P + " )";
            myCommand = new OleDbCommand(sql2, connection);
            myCommand.ExecuteNonQuery();

            //выводит данные о докторе
            string sql4 = "SELECT Спеціальність, ФІО, Кабінет FROM [Doctors] WHERE [Код] = " + 1;
            myCommand = new OleDbCommand(sql4, connection);
            OleDbDataReader reader2 = myCommand.ExecuteReader();

            Doctor doctor = new Doctor();
            
            while (reader2.Read())
            {
                doctor.Specialty = reader2[0].ToString();
                doctor.FullName = reader2[1].ToString();
                label3.Text = doctor.FullName;
                doctor.Cabinet = reader2[2].ToString();
                label4.Text = doctor.Cabinet;
            }
            reader2.Close();




            richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Ви успішно зараєструвалися!";
            animeVopros++;

        }

      

        public void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = bot + "Вітаю, ви з Запоріжжя?";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
             connection.Close();
            Application.Exit();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
             richTextBox1.Text = richTextBox1.Text + "\n" +"\t"+ "Ви: " + dateTimePicker1.Value.ToString("dd.MM.yyyy");
            dataR = dateTimePicker1.Value.ToString("dd.MM.yyyy");
            oldOtvet[4] = dataR;

            vopros_4 = false;

            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = DateTime.Today;
            int dateProverka = date1.CompareTo(date2);
            

            if (dateProverka >= 0)
            {
                if (timeDetector == 0)
                {
                    animeVopros++;
                    timeDetector++;
                }

                string sql = "SELECT  Time_reg.Час_запису FROM Time_reg   ";
                OleDbCommand myCommand = new OleDbCommand(sql, connection);
                OleDbDataReader reader = myCommand.ExecuteReader();

                pZap = 0;
                while (reader.Read())
                {
                    zapisy[pZap++] = reader[0].ToString();
                }
                reader.Close();

                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Оберіть час для реєстрації!";

                sql = "SELECT    Registration.Час FROM Doctors, Registration WHERE Doctors.Код = Registration.Код_лікаря  AND " + "Doctors.Код = " + k_Doc + " AND Registration.Дата = '" + dataR + "'";
                myCommand = new OleDbCommand(sql, connection);
                OleDbDataReader reader2 = myCommand.ExecuteReader();
                pZap_2 = 0;

                while (reader2.Read())
                {
                    zapisy_2[pZap_2++] = reader2[0].ToString();

                }
                reader2.Close();

                int z = 0;
                int k;

                while (z < pZap_2)
                {
                    k = 0;

                    while (k < pZap)
                    {

                        if (zapisy[k] == zapisy_2[z])
                        {

                            for (int i = k; i < pZap; ++i)
                            {

                                zapisy[i] = zapisy[i + 1];

                            }

                            pZap--;

                        }
                        k++;
                    }
                    z++;
                }

                z = 0;
                p4 = 0;
                while (z < pZap)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + ++z + "  час " + zapisy[z - 1];
                    p4++;
                }
                p4--;
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Оберіть час. Для цього впишіть номер часу, який вам підходить.";
                textBox1.Focus();

            }
            else
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + bot + "Запис на цей час неможливий.";
            }
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (animeVopros == 8)
            {
                char number = e.KeyChar;
                if (!Char.IsDigit(number))
                {
                    e.Handled = true;
                }
            }
            else if (animeVopros == 6)
            {
                char l = e.KeyChar;
                if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
                    e.Handled = true;
            }
            else if (animeVopros == 7)
            {
                char l = e.KeyChar;
                if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
                    e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.FullName = nameP+" "+surnameP;
            client.Telephone = numberP;

            //client.GetInfo();               
            DialogResult dialogResult = MessageBox.Show("Ваші данні введено вірно?" + "\n\n" + client.FullName +  "\n\n" + client.Telephone, " Підтвердження", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {              
                Form3 Zz = new Form3();   
                //likarnya
                Zz.label1.Text = Zz.label1.Text +" "+ label1.Text;
                //likar
                Zz.label2.Text = Zz.label2.Text + " " + label2.Text;
                //имя доктора
                Zz.label3.Text =  label3.Text;
                //кабинет
                Zz.label4.Text = Zz.label4.Text + label4.Text;
                //дата
                Zz.label6.Text = Zz.label6.Text+ dataR;
                //время
                Zz.label7.Text = Zz.label7.Text+ timeR;
                Zz.Show();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                //do something else
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                animeVopros = animeVopros - 2;


                if (animeVopros < -1)
                {
                    animeVopros = -1;
                }

             

                if (animeVopros == -1)
                {
                    richTextBox1.Text = bot + "Вітаю, ви з Запоріжжя?";
                  
                }
                if (animeVopros == 0)
                {
                    oldOtvet[animeVopros] = oldOtvet[animeVopros].ToLower();
                    zpIF(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 1)
                {
                    areaZP(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 2)
                {
                    oldOtvet[animeVopros] = oldOtvet[animeVopros].ToLower();
                    
                    hospitalZP(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 3)
                {
                  
                    doctorZP(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 4)
                {
                    dataZP();
                }
                else if (animeVopros == 5)
                {
                    button6.Enabled = false;
                    timeZP(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 6)
                {
                    nameYou(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 7)
                {
                    textBox1.Text = "+380";
                    textBox1.MaxLength = 13;
                    surnameYou(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 8)
                {
                    numberYou(oldOtvet[animeVopros]);
                }
                else if (animeVopros == 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + bot + " Ви вже записалися!";


                }
                textBox1.Focus();
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
            catch
            {
                richTextBox1.Text = "" + bot + " помилка :)";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ViIzZP_1 == true)
            {
                animeVopros = 0;
                zanovoPO = true;
                timeDetector = 0;
                richTextBox1.Text = "" + bot + "Вітаю, ви з Запоріжжя?";
                zpIF("так");
            }
            else
            {
                richTextBox1.Text = bot + "Вітаю, ви з Запоріжжя?";
            }
        }
    }
}
