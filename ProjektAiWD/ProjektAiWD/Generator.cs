using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAiWD
{
    public class Generator
    {
        public Generator()
        {

        }
        private string path;

        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        public void generate(bool ifSection)
        {
            Console.WriteLine("start");

            int groupAgeCount = 10;
            Random random = new Random();

            int[] ages = { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70 };
            int agesCount = ages.Length;
            int[] maxRange = { 200, 195, 190, 185, 180, 175, 170, 165, 160, 155, 150 };
            int[] lowerRange = { 100, 98, 95, 93, 90, 88, 85, 83, 80, 78, 75 };
            int[] higherRange = { 170, 166, 162, 157, 153, 149, 145, 140, 136, 132, 128 };

            int maxNumbers = 2;
            int offset = ages[1]-ages[0];
            Random rnd = new Random();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path + ".csv"))
            {
                if (ifSection)
                {
                    for (int idAge = 0; idAge < agesCount; idAge++)
                    {
                        for (int i = 1; i <= groupAgeCount; i++)
                        {
                            int age = rnd.Next(ages[idAge]- offset, ages[idAge]);
                            int pulse = rnd.Next(lowerRange[idAge], higherRange[idAge]);
                            file.WriteLine(age + "," + pulse);
                        }
                    }
                }
                else
                {
                    for (int idAge = 0; idAge < agesCount; idAge++)
                    {
                        for (int i = 1; i <= groupAgeCount; i++)
                        {
                            int age = ages[idAge];
                            int pulse = rnd.Next(lowerRange[idAge], higherRange[idAge]);
                            file.WriteLine(age + "," + pulse);
                        }
                    }
                }

                //bledne dane
                int wrongNumbers = 2;
                for (int i = 0; i <= wrongNumbers; i++)
                {

                    int wrongPulse = lowerRange[i] - 150;
                    file.WriteLine(ages[i] + "," + wrongPulse);

                    wrongPulse = higherRange[i] + 300;
                    file.WriteLine(ages[i] + "," + wrongPulse);

                }
                // pusty rekord
                file.WriteLine(ages[0] + "," + " ");

                // punkty oddalone na + 
                for (int i = 0; i <= maxNumbers; i++)
                {
                    file.WriteLine(ages[i] + "," + maxRange[i]);
                }

            }
            Console.WriteLine("wygenerowano pomyslnie");

        }

    }
}
/* * Age Maximum Heart Rate
20 years 200  beats per minute
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
20 years 100-170 beats per minute
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


