using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Vizhiner
{
    public partial class Form1 : Form
    {
        static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ,.;-!:?*()_=+/'|\"\\\n";
        static int specialCharMarker = 20;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text += EncDecVig(textBox2.Text.ToLower(), textBox1.Text.ToLower(), radioButton1.Checked == true) + Environment.NewLine;
        }

        static string EncDecVig(string key, string inputStr, bool move)
        {
            int[] keyPos = new int[key.Length];
            int[] inputStrPos = new int[inputStr.Length];
            int index = 0;
            int currentStrPos;
            string result = "";
            int currentKeyPos;
            foreach (var pos in key)
            {
                keyPos[index++] +=alphabet.IndexOf(pos);
            }
            index = 0;
            foreach (var pos in inputStr)
            {
                inputStrPos[index++] += alphabet.IndexOf(pos);
            }
            int special = 0;


            for(int i = 0; i<inputStrPos.Length; i++)
            {
                if(inputStrPos[i] >= alphabet.Length - specialCharMarker)
                {
                    result += alphabet[inputStrPos[i]];
                    special++;
                    continue;
                }

                currentKeyPos = i % key.Length - special % key.Length;
                if (currentKeyPos < 0)
                {
                    currentKeyPos += key.Length;
                }

                if (move == true)
                {
                    currentStrPos = (inputStrPos[i] + keyPos[currentKeyPos]) % (alphabet.Length - specialCharMarker);
                }
                else
                {
                    currentStrPos = (inputStrPos[i] - keyPos[currentKeyPos]) % (alphabet.Length - specialCharMarker);
                    if(currentStrPos < 0)
                    {
                        currentStrPos += (alphabet.Length - specialCharMarker);
                    }
                }
                if(currentKeyPos < 0)
                {
                    continue;
                }
                result += alphabet[currentStrPos];
            }
            return result;
        }
    }
}
