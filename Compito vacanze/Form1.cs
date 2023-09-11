using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Elaborazione_dati_CSV
{
    public partial class listview : Form
    {
        public string nomeFile = "DridiAyoub.csv";
        public struct dato
        {
            public string anno;
            public string Causa_di_morte;
            public string Classe_eta;
            public string Decessi;
            public int miovalore;
            public bool cancellato;
        }


        public dato p;
        public string FileName;
        public string NomeTemp;
        public int recordLength;

        public listview()
        {
            InitializeComponent();
            p = new dato();
            FileName = "DridiAyoub.csv";
            NomeTemp = "temp.csv";
            recordLength = 62;
            Aggiungi_campi();
            visualizza();
        }


        public void visualizza()
        {
            listView1.Items.Clear();


            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);


            line = r.ReadLine();
            line = r.ReadLine();

            while (line != null)
            {
                p = FromString(line);

                if (p.cancellato == false)
                    listView1.Items.Add(p.anno + ";" + p.Causa_di_morte + ";" + p.Classe_eta + ";" + p.Decessi + ";");


                line = r.ReadLine();

            }

            r.Close();
        }



        //estraggo i valori di dato
        public static dato FromString(string datiStringa, string sep = ";")
        {
            dato p = new dato();
            String[] fields = datiStringa.Split(sep[0]);
            p.anno = fields[0];
            p.Causa_di_morte = fields[1];
            p.Classe_eta = fields[2];
            p.Decessi = fields[3];
            p.miovalore = int.Parse(fields[4]);
            p.cancellato = bool.Parse(fields[5]);

            return p;
        }


        //format stringa dato "dato"
        public string FileString(dato p)
        {
            return (p.anno + ";" + p.Causa_di_morte + ";" + p.Classe_eta + ";" + p.Decessi + ";" + p.miovalore + ";" + p.cancellato + ";").PadRight(60);
        }


        //scrittura in append
        public static void scriviAppend(string content, string filename)
        {
            var fStream = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Read);
            StreamWriter sw = new StreamWriter(fStream);
            sw.WriteLine(content);
            sw.Close();
        }

        public void Aggiungi_campi()
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);
            StreamWriter w = new StreamWriter(NomeTemp, true);

            char limite = ';';
            string[] fields;

            string[] words = new string[6];

            line = r.ReadLine();

            if (line[line.Length - 1] == 'e' && line[line.Length - 4] == 'l') 
            {
                w.WriteLine(line);
            }
            else
            {
                w.WriteLine(line + ";Miovalore");
            }

            line = r.ReadLine();

            while (line != null)
            {


                fields = line.Split(limite);
                p.anno = fields[0];
                p.Causa_di_morte = fields[1];
                p.Classe_eta = fields[2];
                p.Decessi = fields[3];
                p.miovalore = rand.Next(10, 21);
                p.cancellato = false;

                w.WriteLine(FileString(p));

                line = r.ReadLine();

            }

            w.Close();
            r.Close();

            File.Delete(FileName);
            File.Move(NomeTemp, FileName);
        }

        public int NumeroCampi()
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);


            line = r.ReadLine();
            line = r.ReadLine();

            int NumeroCampi = line.Split(';').Length - 1; 

            r.Close();

            return NumeroCampi;
        }


        public int ConteggioMassimo()
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);
            int Max;

            line = r.ReadLine();

            line = r.ReadLine();
            Max = line.Length;

            while (line != null)
            {

                if (Max < line.Length)
                {
                    Max = line.Length;
                }

                line = r.ReadLine();

            }


            r.Close();


            return Max;
        }


        public void Canc(string titolo)
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);
            StreamWriter w = new StreamWriter(NomeTemp, true);
            int lenghtMax;

            line = r.ReadLine();

            w.WriteLine(line);

            line = r.ReadLine();
            lenghtMax = line.Length;

            while (line != null)
            {
                p = FromString(line);

                if (titolo == p.anno)
                {
                    p.cancellato = true;
                }


                w.WriteLine(FileString(p));

                line = r.ReadLine();

            }

            w.Close();
            r.Close();

            File.Delete(FileName);
            File.Move(NomeTemp, FileName);

        }

        public void Mod(string titolo, string morte, string eta, string decessi, string miovalore)
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);
            StreamWriter w = new StreamWriter(NomeTemp, true);
            int lenghtMax;

            line = r.ReadLine();

            w.WriteLine(line);

            line = r.ReadLine();
            lenghtMax = line.Length;

            while (line != null)
            {
                p = FromString(line);

                if (titolo == p.anno)
                {
                    p.Causa_di_morte = morte;
                    p.Classe_eta = eta;
                    p.Decessi = decessi;
                    p.miovalore = int.Parse(miovalore);
                }


                w.WriteLine(FileString(p));

                line = r.ReadLine();

            }

            w.Close();
            r.Close();

            File.Delete(FileName);
            File.Move(NomeTemp, FileName);

        }

        public void Padding()
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);
            StreamWriter w = new StreamWriter(NomeTemp, true);

            line = r.ReadLine();

            w.WriteLine(line);

            line = r.ReadLine();

            while (line != null)
            {

                w.WriteLine(line.PadRight(130));

                line = r.ReadLine();
            }

            w.Close();
            r.Close();

            File.Delete(FileName);
            File.Move(NomeTemp, FileName);

        }


        public String Ricerca(string nome)
        {
            String line;

            Random rand = new Random();
            StreamReader r = new StreamReader(FileName);


            line = r.ReadLine();
            line = r.ReadLine();

            while (line != null)
            {


                p = FromString(line);
                if (p.anno == nome && p.cancellato == false)
                {
                    return line;
                }

                line = r.ReadLine();

            }

            r.Close();



            return "Not Found";
        }


        public void Aggiungi(string titolo, string link, string lat, string longi, string miovalore, bool canc = false)
        {
            string content = titolo + ";" + link + ";" + lat + ";" + longi + ";" + miovalore + ";" + canc + ";";
            scriviAppend(content, FileName);
        }

        private void LunghezzaMax_Click(object sender, EventArgs e)
        {
            MessageBox.Show("la lunghezza massima è: " + ConteggioMassimo());

        }

        private void Padding_button_Click(object sender, EventArgs e)
        {
            Padding();
            visualizza();
        }

        private void Cerca_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Ricerca(nome_textbox.Text));
        }

        private void CountFields_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Il num. di campi è: " + NumeroCampi());

        }

        private void Append_Click(object sender, EventArgs e)
        {
            Aggiungi(titolo_textbox.Text, link_textbox.Text, latitudine_textbox.Text, longitudine_textbox.Text, miovalore_textbox.Text);
            visualizza();
        }

        private void Modifica_Click(object sender, EventArgs e)
        {
            Mod(Titolo_mod_textbox.Text, link_mod_textbox.Text, latitudine_mod_textbox.Text, longitudine_mod_textbox.Text, miovalore_mod_textbox.Text);
            visualizza();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cancellazioneLog_Click(object sender, EventArgs e)
        {
            Canc(titolo_canc_textbox.Text);
            visualizza();
        }
    }
}


