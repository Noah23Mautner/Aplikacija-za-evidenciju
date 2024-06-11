using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Aplikacija_za_evidenciju
{
    public partial class Form1 : Form
    {
        private List<Osoba> listaOsoba = new List<Osoba>();
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Učenik");
            comboBox1.Items.Add("Profesor");
            comboBox1.Items.Add("Tehničko osoblje");
            comboBox2.Items.Add("Učenik");
            comboBox2.Items.Add("Profesor");
            comboBox2.Items.Add("Tehničko osoblje");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Osoba osoba = new Osoba
            {
                Ime = textBox1.Text,
                Prezime = textBox2.Text,
                Uloga = comboBox1.SelectedItem.ToString()
            };
            listaOsoba.Add(osoba);
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            foreach (Osoba osoba in listaOsoba)
            {
                richTextBox1.AppendText(osoba.Ime + "\t" + osoba.Prezime + "\t" + osoba.Uloga + "\t" + osoba.Aktivnost + "\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Osoba osoba in listaOsoba)
            {
                switch (osoba.Uloga)
                {
                    case "Učenik":
                        osoba.Aktivnost = "Uči";
                        break;
                    case "Profesor":
                        osoba.Aktivnost = "Predaje";
                        break;
                    case "Tehničko osoblje":
                        osoba.Aktivnost = "Održava";
                        break;
                }
            }
            richTextBox1.Clear();
            foreach (Osoba osoba in listaOsoba)
            {
                richTextBox1.AppendText(osoba.Ime + "\t" + osoba.Prezime + "\t" + osoba.Uloga + "\t" + osoba.Aktivnost + "\n");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listaOsoba.Sort((x, y) => x.Ime.CompareTo(y.Ime));
            richTextBox1.Clear();
            foreach (Osoba osoba in listaOsoba)
            {
                richTextBox1.AppendText(osoba.Ime + "\t" + osoba.Prezime + "\t" + osoba.Uloga + "\t" + osoba.Aktivnost + "\n");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Nema selektirane osobe!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] selectedOsobaDetails = richTextBox1.Text.Split('\t');
            Osoba selektiranaOsoba = listaOsoba.FirstOrDefault(o => o.Ime == selectedOsobaDetails[0] && o.Prezime == selectedOsobaDetails[1]);

            if (selektiranaOsoba == null)
            {
                MessageBox.Show("Osoba nije pronađena!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Da li ste sigurni da želite obrisati osobu?", "Brisanje osobe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listaOsoba.Remove(selektiranaOsoba);
                richTextBox1.Clear();
                foreach (Osoba osoba in listaOsoba)
                {
                    richTextBox1.AppendText(osoba.Ime + "\t" + osoba.Prezime + "\t" + osoba.Uloga + "\t" + osoba.Aktivnost + "\n");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("osobe.csv"))
            {
                foreach (Osoba osoba in listaOsoba)
                {
                    writer.WriteLine(osoba.Ime + "," + osoba.Prezime + "," + osoba.Uloga + "," + osoba.Aktivnost);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listaOsoba.Clear();
            using (StreamReader reader = new StreamReader("osobe.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Osoba osoba = new Osoba
                    {
                        Ime = parts[0],
                        Prezime = parts[1],
                        Uloga = parts[2],
                        Aktivnost = parts[3]
                    };
                    listaOsoba.Add(osoba);
                }
            }
            richTextBox1.Clear();
            foreach (Osoba osoba in listaOsoba)
            {
                richTextBox1.AppendText(osoba.Ime + "\t" + osoba.Prezime + "\t" + osoba.Uloga + "\t" + osoba.Aktivnost + "\n");
            }
        }
        public class Osoba
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Uloga { get; set; }
            public string Aktivnost { get; set; }
        }
    }
}
