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
        static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ,.;-!:?*()_=+/'|\"\\\n"; //Задаём строку с алфавитом
        static int specialCharMarker = 20; //Задаём число после которого заканчивается алфавит и начинаются спец символы
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text += EncDecVig(textBox2.Text.ToLower(), textBox1.Text.ToLower(), radioButton1.Checked == true) + Environment.NewLine; ; // Вывод в текстбокс
        }

        static string EncDecVig(string key, string inputStr, bool move) //Метод для шифровки\дешифроки текста
        {
            int[] keyPos = new int[key.Length]; //Массив который содержит числовое значение положения ключа в алфавите
            int[] inputStrPos = new int[inputStr.Length]; //Массив который содержит числовое значение положения текста в алфавите
            int index = 0;  //Число для перебора ключа и текста
            int currentStrPos; //Число которое в последствии будет содержать букву шифровоного сообщения
            string result = "";  //Место куда попадает зашифровонное сообщение
            int currentKeyPos; //Число хранящие значение во время шифровки относительно алфавита
            foreach (var pos in key) //Переводит буквы ключа в цифры относительно алфавита
            {
                keyPos[index++] +=alphabet.IndexOf(pos); 
            }
            index = 0;
            foreach (var pos in inputStr) //Переводит буквы введённого текста в цифры относительно алфавита
            {
                inputStrPos[index++] += alphabet.IndexOf(pos);
            }


            for(int i = 0; i<inputStrPos.Length; i++)
            {
                if(inputStrPos[i] >= alphabet.Length - specialCharMarker) //Заносит в зашифрованный текст специальные символы
                {
                    result += alphabet[inputStrPos[i]];
                    continue;
                }

                currentKeyPos = i % key.Length; //Числовое положения ключа относительно алфавита

                if (move == true) //Шифровка текста
                {
                    currentStrPos = (inputStrPos[i] + keyPos[currentKeyPos]) % (alphabet.Length - specialCharMarker);
                }
                else //Дешифровка текста
                {
                    currentStrPos = (inputStrPos[i] - keyPos[currentKeyPos]) % (alphabet.Length - specialCharMarker);
                    if(currentStrPos < 0)
                    {
                        currentStrPos += (alphabet.Length - specialCharMarker);
                    }
                }
                result += alphabet[currentStrPos];
            }
            return result;
        }
    }
}
