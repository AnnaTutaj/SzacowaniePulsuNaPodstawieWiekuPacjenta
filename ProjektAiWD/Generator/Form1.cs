using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = "Nie rozpoczęto";

        }

        static void generate()
        {
            Console.WriteLine("start");

            int groupAgeCount = 10;
            int groupMaxAgeCount = 1;
            Random random = new Random();

            int[] ages = { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70 };
            int agesCount = ages.Length;
            int[] maxRange = { 200, 195, 190, 185, 180, 175, 170, 165, 160, 155, 150 };
            int maxDiffrence = 10;
            int[] lowerRange = { 100, 98, 95, 93, 90, 88, 85, 83, 80, 78, 75 };
            int[] higherRange = { 170, 166, 162, 157, 153, 149, 145, 140, 136, 132, 128 };

            int id = 1;
            int finishId = 0;
            int maxNumbers = 2;
            //11 razy, bo tylko jest grup wiekowych
            Console.WriteLine("start");

            Random rnd = new Random();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\2 sem\Analiza i wizualizacja danych\Projekt\ProjektAiWD\Data\data.csv"))
            {

                Console.WriteLine("start");
                for (int idAge = 0; idAge < agesCount; idAge++)
                {
                    for (int i = id; i <= groupAgeCount + finishId; i++)
                    {

                        int age = ages[idAge];

                        int pulse = rnd.Next(higherRange[idAge] + 1 - lowerRange[idAge]) + lowerRange[idAge];

                        file.WriteLine(age + "," + pulse);


                        pulse = random.Next(higherRange[idAge] + 1 - lowerRange[idAge]) / 2 + lowerRange[idAge];

                        file.WriteLine(age + "," + pulse);


                        id = i + 1;
                    }

                    finishId = id - 1;

                    finishId = id;

                }

                int wrongNumbers = 5;
                int currentId = 0;
                for (int i = id; i <= wrongNumbers + finishId; i++)
                {

                    int wrongPulse = lowerRange[currentId] - 150;
                    file.WriteLine(ages[currentId] + "," + wrongPulse);

                    id = i + 1;
                    currentId++;
                }

                // punkty oddalone na + 
                for (int i = 0; i <= maxNumbers ; i++)
                {
                    file.WriteLine(ages[i] + "," + maxRange[i]);
                    currentId++;
                }

                //punkt oddalone na - 
                file.WriteLine(ages[10] + "," + (lowerRange[10]-10));


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "Trwa generowanie danych";
            generate();
            label2.Text = "Wygenerowano pomyślnie";
        }

      
    }
}
/* * Age Maximum Heart Rate
20 years 200
25 years 195
30 years 190
35 years 185
40 years 180
45 years 175
50 years 170
55 years 165
60 years 160
65 years 155
70 years 150
Table 3: Target Heart Rate Zone Chart
Age Target Heart Rate Zone (50-85% Max HR)
20 years 100-170
25 years 98-166
30 years 95-162
35 years 93-157
40 years 90-153
45 years 88-149
50 years 85-145
55 years 83-140
60 years 80-136
65 years 78-132
70 years 75-128*/
