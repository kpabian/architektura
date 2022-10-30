using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dfbadsfvadfvdfav
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] registers = new string[8] { "01", "02", "03", "04", "05", "06", "07", "08" };
        static string[] registers_names = new string[8] { "AL", "AH", "BL", "BH", "CL", "CH", "DL", "DH" };

        void Reset()
        {
            label15.Text = "";
            label4.Text = "";
            label5.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";

        }

        string NameCheck(string x)
        {
            if (x.Length != 2 || (!(x[0] >= 'A' && x[0] <= 'D')) || (x[1] != 'H' && x[1] != 'L'))
            {
                DialogResult wynik2 = MessageBox.Show("Blednie podana nazwa. Podaj nazwe rejestru.", " ", MessageBoxButtons.OK);
                return "error";
            }
            else
                return x;
        }

        private void button2_Click(object sender, EventArgs e) //MOV
        {
            string first = NameCheck(textBox1.Text);
            string second = NameCheck(textBox2.Text);

            if (first != "error" && second != "error")
            {
                string x = "0";

                int i = 0, j = 0;
                while (first != registers_names[i])
                    i++;

                x = registers[i];

                while (second != registers_names[j])
                    j++;

                registers[j] = x;

                Reset();

                textBox1.Text = first;
                textBox2.Text = second;
                label15.Text = $"{registers_names[i]} = {registers[i]}, {registers_names[j]} = {registers[j]}";

                button1_Click(sender, e);
            }


        }

        private void button1_Click(object sender, EventArgs e) //aktualizuje rejestry
        {
            textBox18.Text = registers[0];
            textBox19.Text = registers[1];
            textBox20.Text = registers[2];
            textBox21.Text = registers[3];
            textBox22.Text = registers[4];
            textBox23.Text = registers[5];
            textBox24.Text = registers[6];
            textBox25.Text = registers[7];
        }

        private void button12_Click(object sender, EventArgs e) //wpisuje wartości do rejestrów
        {
            registers[0] = textBox18.Text;
            registers[1] = textBox19.Text;
            registers[2] = textBox20.Text;
            registers[3] = textBox21.Text;
            registers[4] = textBox22.Text;
            registers[5] = textBox23.Text;
            registers[6] = textBox24.Text;
            registers[7] = textBox25.Text;

            int i = 0;

            for (int znak = 0; znak < 8; znak++)
            {
                if ((registers[znak].Length != 2) || registers[znak][0] < '0' || (registers[znak][0] > '9' && registers[znak][0] < 'A') || registers[znak][0] > 'F' || (registers[znak][1] < '0' || (registers[znak][1] > '9' && registers[znak][1] < 'A') || registers[znak][1] > 'F'))
                {
                    DialogResult wynik = MessageBox.Show($"Niepopeawnie wprowadzona wartość rejestru {registers_names[znak]}. Podaj ponownie.", " ", MessageBoxButtons.OK);
                    i++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //ADD
        {
            string x = NameCheck(textBox3.Text);

            if (x != "error")
            {
                int a = 0;
                while (registers_names[a] != x)
                    a++;

                string first = registers[a];

                string y = NameCheck(textBox4.Text);
                int b = 0;
                while (registers_names[b] != y)
                    b++;

                string second = registers[b];

                int value1 = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                int value2 = Int32.Parse(second, System.Globalization.NumberStyles.HexNumber);

                int value = value1 + value2;

                if (value > 255)
                    value -= 255;

                string hex = value.ToString("X");

                if (hex.Length == 1)
                    hex = "0" + hex;

                registers[a] = hex;

                Reset();

                textBox3.Text = x;
                textBox4.Text = y;
                label4.Text = $"Po dodaniu rejestru {registers_names[a]} = {first} do rejestru {registers_names[b]} = {second} otrzymujemy {registers[a]}. ";
                button1_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e) //SUB
        {
            string x = NameCheck(textBox6.Text);
            string y = NameCheck(textBox5.Text);

            if (x != "error" && y != "error")
            {
                int a = 0;
                while (registers_names[a] != x)
                    a++;

                string first = registers[a];

                
                int b = 0;
                while (registers_names[b] != y)
                    b++;

                string second = registers[b];

                int value1 = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                int value2 = Int32.Parse(second, System.Globalization.NumberStyles.HexNumber);
                int value;

                if (value1 < value2)
                    value = value1 - value2 + 256;
                else
                    value = value1 - value2;

                string hex = value.ToString("X");
                registers[a] = hex;



                Reset();
                textBox5.Text = y;
                textBox6.Text = x;
                label5.Text = $"Odjemna ({registers_names[a]}) = {first}, odjemnik ({registers_names[b]}) = {second}, roznica ({registers_names[a]}) = {hex}.";
                button1_Click(sender, e);
            }

        }

        private void button5_Click(object sender, EventArgs e)  //XCHG
        {
            string x = NameCheck(textBox8.Text);
            string y = NameCheck(textBox7.Text);

            if (x != "error" && y != "error")
            {

                int a = 0;
                while (registers_names[a] != x)
                    a++;

                string first = registers[a];

                int b = 0;
                while (registers_names[b] != y)
                    b++;

                string second = registers[b];

                int value1 = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                int value2 = Int32.Parse(second, System.Globalization.NumberStyles.HexNumber);

                string val = registers[a];
                registers[a] = registers[b];
                registers[b] = val;

                Reset();
                textBox8.Text = x;
                textBox7.Text = y;
                label16.Text = $"Po zamianie wartości w rejestrach {registers_names[a]} = {registers[a]}, {registers_names[b]} = {registers[b]}";
                button1_Click(sender, e);
            }

        }

        private void button6_Click(object sender, EventArgs e) //INC
        {
            string register = NameCheck(textBox9.Text);

            if (register != "error")
            {

                int a = 0;
                while (registers_names[a] != register)
                    a++;

                string first = registers[a];

                if (registers[a] == "FF")
                    registers[a] = "00";
                else
                {
                    int value = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                    value++;
                    string hex = value.ToString("X");
                    registers[a] = hex;

                    if (hex.Length == 1)
                        registers[a] = "0" + hex;
                }
                Reset();
                textBox9.Text = register;
                label17.Text = $"Nowa wartość rejestru {registers_names[a]} = {registers[a]}";
                button1_Click(sender, e);
            }
        }

        private void button7_Click(object sender, EventArgs e) //DEC
        {
            string register = NameCheck(textBox10.Text);

            if (register != "error")
            {

                int a = 0;
                while (registers_names[a] != register)
                    a++;

                string first = registers[a];

                if (registers[a] == "00")
                    registers[a] = "FF";
                else
                {
                    int value = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                    value--;
                    string hex = value.ToString("X");
                    registers[a] = hex;

                    if (hex.Length == 1)
                        registers[a] = "0" + hex;
                }

                Reset();
                textBox10.Text = register;
                label18.Text = $"Nowa wartość rejestru {registers_names[a]} = {registers[a]}";
                button1_Click(sender, e);
            }
        }

        private void button8_Click(object sender, EventArgs e) //AND
        {
            string first = NameCheck(textBox12.Text);
            string second = NameCheck(textBox11.Text);

            if (first != "error" && second != "error")
            {

                int a = 0;
                while (registers_names[a] != first)
                    a++;

                first = registers[a];

                int b = 0;
                while (registers_names[b] != second)
                    b++;

                second = registers[b];

                int value1 = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                int value2 = Int32.Parse(second, System.Globalization.NumberStyles.HexNumber);

                int value = value1 & value2;
                string hex = value.ToString("X");

                if (hex.Length == 1)
                    hex = "0" + hex;

                registers[a] = hex;

                Reset();
                textBox12.Text = registers_names[a];
                textBox11.Text = registers_names[b];
                label19.Text = $"Po użyciu operatora AND wartość rejestru {registers_names[a]} = {registers[a]}";
                button1_Click(sender, e);
            }
        }

        private void button9_Click(object sender, EventArgs e) //OR
        {
            string first = NameCheck(textBox14.Text);
            string second = NameCheck(textBox13.Text);

            if (first != "error" && second != "error")
            {

                int a = 0;
                while (registers_names[a] != first)
                    a++;

                first = registers[a];

                int b = 0;
                while (registers_names[b] != second)
                    b++;

                second = registers[b];

                int value1 = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                int value2 = Int32.Parse(second, System.Globalization.NumberStyles.HexNumber);

                int value = value1 | value2;
                string hex = value.ToString("X");

                if (hex.Length == 1)
                    hex = "0" + hex;

                registers[a] = hex;

                Reset();
                textBox14.Text = registers_names[a];
                textBox13.Text = registers_names[b];
                label20.Text = $"Po użyciu operatora OR wartość rejestru {registers_names[a]} = {registers[a]}";
                button1_Click(sender, e);
            }
        }

        private void button10_Click(object sender, EventArgs e) //XOR
        {
            string first = NameCheck(textBox16.Text);
            string second = NameCheck(textBox15.Text);

            if (first != "error" && second != "error")
            {

                int a = 0;
                while (registers_names[a] != first)
                    a++;

                first = registers[a];

                int b = 0;
                while (registers_names[b] != second)
                    b++;

                second = registers[b];

                int value1 = Int32.Parse(first, System.Globalization.NumberStyles.HexNumber);
                int value2 = Int32.Parse(second, System.Globalization.NumberStyles.HexNumber);

                int value = value1 ^ value2;

                string hex = value.ToString("X");

                if (hex.Length == 1)
                    hex = "0" + hex;

                registers[a] = hex;

                Reset();
                textBox16.Text = registers_names[a];
                textBox15.Text = registers_names[b];
                label21.Text = $"Po użyciu operatora OR wartość rejestru {registers_names[a]} = {registers[a]}";
                button1_Click(sender, e);
            }
        }

        private void button11_Click(object sender, EventArgs e) //NOT
        {
            string register = NameCheck(textBox17.Text);

            string x = "-1";
            int i = -1;

            switch (register)
            {
                case "AL":
                    x = registers[0];
                    i = 0;
                    break;

                case "AH":
                    x = registers[1];
                    i = 1;
                    break;


                case "BL":
                    x = registers[2];
                    i = 2;
                    break;

                case "BH":
                    x = registers[3];
                    i = 3;
                    break;

                case "CL":
                    x = registers[4];
                    i = 4;
                    break;

                case "CH":
                    x = registers[5];
                    i = 5;
                    break;

                case "DL":
                    x = registers[6];
                    i = 6;
                    break;

                case "DH":
                    x = registers[7];
                    i = 7;
                    break;
            }

            int value = Int32.Parse(x, System.Globalization.NumberStyles.HexNumber);

            int pom = 128, number = 0;
            while (pom != 0)
            {
                if (value >= pom)
                    value -= pom;
                else
                    number += pom;
                pom /= 2;
            }

            string hex = number.ToString("X");
            registers[i] = hex;


                Reset();
                textBox17.Text = registers_names[i];
                label22.Text = $"Po użyciu operatora NOT wartość rejestru {registers_names[i]} = {registers[i]}";
                button1_Click(sender, e);
            }
        }

    }


