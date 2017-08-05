using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAiWD
{
    public class Calculator
    {
        #region variables
        //rozmiar i tak sie potem zmieni
        private double[,] array2D = new double[500, 2];
        private double[,] newArray2D = new double[500, 2];
        private double[,] sortedArray2DbyPulse = new double[500, 2];
        private double[,] sortedArray2DbyAge = new double[500, 2];
        private double[,] individualPoints = new double[500, 2];
        private double[] estimatedY = new double[12];
        private double[] estimatedX = new double[12];

        private double minPulse;
        private double maxPulse;
        private double minAge;
        private double maxAge;

        private double expectedValuePulse;
        private double expectedValueAge;
        private double medianAge;
        private double medianPulse;
        private double standardDeviationAge;
        private double standardDeviationPulse;

        private double pearsonsR;

        private double a;
        private double b;

        private double q1;
        private double q3;
        private double iqr;

        private double valueMin;
        private double valueMax;

        private const int idAge = 0;
        private const int idPulse = 1;

        private string path;
        #endregion variables
      
        #region get; set;
        public double[,] Array2D
        {
            get { return array2D; }
            set { array2D = value; }
        }


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

        public double[,] NewArray2D1
        {
            get
            {
                return NewArray2D;
            }

            set
            {
                NewArray2D = value;
            }
        }

        public double[,] SortedArray2DbyPulse
        {
            get
            {
                return sortedArray2DbyPulse;
            }

            set
            {
                sortedArray2DbyPulse = value;
            }
        }

        public double[,] SortedArray2DbyAge
        {
            get
            {
                return sortedArray2DbyAge;
            }

            set
            {
                sortedArray2DbyAge = value;
            }
        }
        public double[] EstimatedX
        {
            get
            {
                return estimatedX;
            }

            set
            {
                estimatedX = value;
            }
        }

        public double[] EstimatedY
        {
            get
            {
                return estimatedY;
            }

            set
            {
                estimatedY = value;
            }
        }

        public double MinAge
        {
            get
            {
                return minAge;
            }

            set
            {
                minAge = value;
            }
        }

        public double MaxAge
        {
            get
            {
                return maxAge;
            }

            set
            {
                maxAge = value;
            }
        }

        public double MinPulse
        {
            get
            {
                return minPulse;
            }

            set
            {
                minPulse = value;

            }
        }

        public double MaxPulse
        {
            get
            {
                return maxPulse;
            }

            set
            {
                maxPulse = value;
            }
        }

        public double[,] IndividualPoints
        {
            get
            {
                return IndividualPoints1;
            }

            set
            {
                IndividualPoints1 = value;
            }
        }

        public double ExpectedValuePulse
        {
            get
            {
                return expectedValuePulse;
            }

            set
            {
                expectedValuePulse = value;
            }
        }

        public double ExpectedValueAge
        {
            get
            {
                return expectedValueAge;
            }

            set
            {
                expectedValueAge = value;
            }
        }

        public double MedianAge
        {
            get
            {
                return medianAge;
            }

            set
            {
                medianAge = value;
            }
        }

        public double MedianPulse
        {
            get
            {
                return medianPulse;
            }

            set
            {
                medianPulse = value;
            }
        }

        public double StandardDeviationAge
        {
            get
            {
                return standardDeviationAge;
            }

            set
            {
                standardDeviationAge = value;
            }
        }

        public double StandardDeviationPulse
        {
            get
            {
                return standardDeviationPulse;
            }

            set
            {
                standardDeviationPulse = value;
            }
        }

        public double PearsonsR
        {
            get
            {
                return pearsonsR;
            }

            set
            {
                pearsonsR = value;
            }
        }

        public double A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public double B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public double Q1
        {
            get
            {
                return q1;
            }

            set
            {
                q1 = value;
            }
        }

        public double Q3
        {
            get
            {
                return q3;
            }

            set
            {
                q3 = value;
            }
        }

        public double Iqr
        {
            get
            {
                return iqr;
            }

            set
            {
                iqr = value;
            }
        }

        public double[,] IndividualPoints1
        {
            get
            {
                return individualPoints;
            }

            set
            {
                individualPoints = value;
            }
        }

        public double ValueMin
        {
            get
            {
                return valueMin;
            }

            set
            {
                valueMin = value;
            }
        }

        public double ValueMax
        {
            get
            {
                return valueMax;
            }

            set
            {
                valueMax = value;
            }
        }

        public double[,] NewArray2D
        {
            get
            {
                return newArray2D;
            }

            set
            {
                newArray2D = value;
            }
        }


        #endregion get; set;
        public Calculator()
        {

        }

        private static Array ResizeArray(Array arr, int[] newSizes)
        {
            if (newSizes.Length != arr.Rank)
                throw new ArgumentException("arr must have the same number of dimensions " +
                                            "as there are elements in newSizes", "newSizes");

            var temp = Array.CreateInstance(arr.GetType().GetElementType(), newSizes);
            int length = arr.Length <= temp.Length ? arr.Length : temp.Length;
            Array.ConstrainedCopy(arr, 0, temp, 0, length);
            return temp;
        }

        public void readFile()
        {
            using (CsvFileReader reader = new CsvFileReader(Path))
            {
                CsvRow row = new CsvRow();
                int i = 0;
                Console.WriteLine("dlugosc przed: " + array2D.Length / 2);
                while (reader.ReadRow(row))
                {
                    int j = 0;
                    foreach (string s in row)
                    {

                        try
                        {
                            array2D[i, j] = Int32.Parse(s);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("blad formatu, nie wczytuje tej danej: "+ s+".");
                        }

                        Console.Write(s);
                        Console.Write(" ");
                        j++;

                    }
                    Console.WriteLine();
                    i++;

                }
                array2D = (double[,])ResizeArray(array2D, new int[] { i, 2 });
            }
        }


        public void show(double[,] array2D, string text)
        {
            Console.WriteLine("PRZED: " + text + " -------------------------------------");
            for (int i = 0; i < array2D.Length / 2; i++)
            {
                double s1 = array2D[i, 0];
                double s2 = array2D[i, 1];
                Console.WriteLine("[{0}, {1}]", s1, s2);
            }
            Console.WriteLine(text + " -------------------------------------");

        }

        public void checkValues()
        {
            int newI = 0;
            int maxReachedPulse = 250;
            int minReachedPulse = 50;
            int minAge = 10;
            int maxAge = 100;

            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                if (array2D[i, 0] > minAge & array2D[i, 0] < maxAge & array2D[i, 1] > minReachedPulse & array2D[i, 1] < maxReachedPulse)
                {
                    NewArray2D[newI, 0] = array2D[i, 0];
                    NewArray2D[newI, 1] = array2D[i, 1];
                    newI++;
                }
            }
            NewArray2D = (double[,])ResizeArray(NewArray2D, new int[] { newI, 2 });
            show(NewArray2D, "otrzymana newArray2D");
        }


        public void generateAdditionalData()
        {
            Random rnd = new Random();
            int min = Convert.ToInt32(minAge);
            int max = Convert.ToInt32(maxAge);

            for (int i = 0; i < EstimatedX.Length; i++)
            {
                EstimatedX[i] = rnd.Next(min, max);

            }

            for (int i = 0; i < EstimatedY.Length; i++)
            {
                EstimatedY[i] = linearRegression(EstimatedX[i]);
            }

        }
        #region Min, Max, Sum, Average, Expected Value
        double getMax(double[,] array2D, int targetParameter)
        {

            double max = array2D[0, targetParameter];
            for (int i = 0; i < array2D.Length / 2; i++)
            {
                if (array2D[i, targetParameter] > max)
                {
                    max = array2D[i, targetParameter];
                }
            }
            return max;
        }

        double getMin(double[,] array2D, int targetParameter)
        {
            double min = array2D[0, targetParameter];
            for (int i = 0; i < array2D.Length / 2; i++)
            {
                if (array2D[i, targetParameter] < min)
                {
                    min = array2D[i, targetParameter];
                }
            }
            return min;
        }

        double sum(double[,] array2D, int targetParameter)
        {
            double tmp = 0;
            for (int i = 0; i < array2D.Length / 2; i++)
            {
                tmp = tmp + array2D[i, targetParameter];

            }
            Console.WriteLine("suma: " + tmp);
            return tmp;
        }

        double arithmeticAverage(double[,] array2D, int targetParameter)
        {
            Console.WriteLine("srednia: " + sum(array2D, targetParameter) / array2D.GetLength(0));

            return sum(array2D, targetParameter) / array2D.GetLength(0);
        }

        double getExpectedValue(double[,] array2D, int targetParameter)
        {
            return Math.Round(sum(array2D, targetParameter) / array2D.GetLength(0), 2);
        }

        bool isEven(double array2D)
        {
            if (array2D % 2 == 0)
            {
                return true;
            }
            else
                return false;

        }
        #endregion Min, Max, Sum, Average, Expected Value
        #region Median,  Paracentile, Individual Points

        public void copyTab()
        {
            sortedArray2DbyPulse = (double[,])ResizeArray(sortedArray2DbyPulse, new int[] { NewArray2D.GetLength(0), 2 });
            Array.Copy(NewArray2D, sortedArray2DbyPulse, sortedArray2DbyPulse.Length);

            sortedArray2DbyAge = (double[,])ResizeArray(sortedArray2DbyAge, new int[] { NewArray2D.GetLength(0), 2 });
            Array.Copy(NewArray2D, sortedArray2DbyAge, sortedArray2DbyAge.Length);


        }

        void sort(double[,] array2D, int targetParameter)
        {
            //  Console.WriteLine("porownuje: " + array2D[0, 0] + "z" + array2D[1, 0]);

            int n = array2D.Length / 2;
            do
            {
                for (int i = 0; i < n - 1; i++)
                {

                    if (array2D[i, targetParameter] > array2D[i + 1, targetParameter])
                    {

                        double tmp0 = array2D[i, 0];
                        double tmp1 = array2D[i, 1];

                        array2D[i, 0] = array2D[i + 1, 0];
                        array2D[i, 1] = array2D[i + 1, 1];

                        array2D[i + 1, 0] = tmp0;
                        array2D[i + 1, 1] = tmp1;

                    }
                }
                n--;
            }
            while (n > 1);

            //show(array2D, "--super sort" + targetParameter);

        }


        double getMedian(double[,] array2D, int targetParameter)
        {

            sort(array2D, targetParameter);
            show(sortedArray2DbyAge, "DUPA");
            int maxIndex = array2D.GetLength(0) - 1;
            if (isEven(array2D.GetLength(0)))
            {
                int a = maxIndex / 2;
                int b = maxIndex / 2 + 1;
                Console.WriteLine("id a: " + a + ":" + array2D[a, targetParameter] + "id b: " + b + ":" + array2D[b, targetParameter]);
                Console.WriteLine("mediana bo patzyse: " + ((array2D[a, targetParameter] + array2D[b, targetParameter]) / 2));

                return ((array2D[a, targetParameter] + array2D[b, targetParameter]) / 2);
            }
            else
            {
                Console.WriteLine("media bo niepatrzyste: " + array2D[(maxIndex) / 2, targetParameter]);

                return array2D[(maxIndex) / 2, targetParameter];
            }

        }

        bool isInt(double d)
        {
            if ((d % 1) == 0)
            {
                Console.WriteLine("true - nie ma po przecnku - to jest srodek");
                return true;
            }

            else
                Console.WriteLine("fase - ma cos po przecinku, trzeba dzielic");
            return false;

        }


        double getQ1(double[,] array2D, int targetParameter)
        {
            sort(array2D, targetParameter);
            int maxIndex = (array2D.GetLength(0) - 1) / 2;
            //parzyscie elementow

            double check = (maxIndex) / 2.0;
            Console.WriteLine("check Q1: " + check);

            if (isInt(check))
            {
                return array2D[(maxIndex) / 2, targetParameter];

            }
            else
            {
                int a = maxIndex / 2;
                int b = maxIndex / 2 + 1;
                Console.WriteLine("id a: " + a + ":" + array2D[a, targetParameter] + "id b: " + b + ":" + array2D[b, targetParameter]);
                return ((array2D[a, targetParameter] + array2D[b, targetParameter]) / 2);

            }

        }

        double getQ3(double[,] array2D, int targetParameter)
        {
            sort(array2D, targetParameter);
            int maxIndex = (array2D.GetLength(0) - 1);

            int minIndex;
            if (isEven(array2D.GetLength(0)))
            {

                minIndex = ((array2D.GetLength(0) - 1) / 2) + 1;
            }
            else
            {
                minIndex = (array2D.GetLength(0) - 1) / 2;
            }

            double check = (maxIndex - minIndex) / 2.0;

            //parzyscie elementow - mamy 1 el 

            if (isInt(check))
            {
                int section = (maxIndex - minIndex) / 2;
                return array2D[section + minIndex, targetParameter];

            }
            else
            {
                int section = (maxIndex - minIndex) / 2;
                int a = section + minIndex;
                int b = section + minIndex + 1;
                Console.WriteLine("min: " + minIndex);
                Console.WriteLine("id a: " + a + ":" + array2D[a, targetParameter] + "id b: " + b + ":" + array2D[b, targetParameter]);
                return ((array2D[a, targetParameter] + array2D[b, targetParameter]) / 2);
            }
        }

        double getIQR(double[,] array2D, int targetParameter)
        {
            return getQ3(array2D, targetParameter) - getQ1(array2D, targetParameter);
        }

        void getindividualPoints(double[,] array2D, int targetParameter)
        {
            ValueMin = getQ1(array2D, targetParameter) - getIQR(array2D, targetParameter) - 0.5 * getIQR(array2D, targetParameter);
            ValueMax = getQ3(array2D, targetParameter) + getIQR(array2D, targetParameter) + 0.5 * getIQR(array2D, targetParameter);

            int ageMin = 0;
            Console.WriteLine("min Value" + ValueMin + "max Value" + ValueMax);

            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                if (array2D[i, targetParameter] < ValueMin || array2D[i, targetParameter] > ValueMax)
                {
                    Console.WriteLine("dodaje");
                    IndividualPoints[ageMin, 0] = array2D[i, 0];
                    IndividualPoints[ageMin, 1] = array2D[i, 1];
                    ageMin++;
                }

            }

            IndividualPoints = (double[,])ResizeArray(IndividualPoints, new int[] { ageMin, 2 });

        }

        #endregion Median and Paracentile

        double getVariance(double[,] array2D, int targetParameter)
        {
            double tmp = 0;
            double average = arithmeticAverage(array2D, targetParameter);

            for (int i = 0; i < array2D.Length / 2; i++)
            {
                tmp = tmp + Math.Pow((array2D[i, targetParameter] - average), 2);

            }
            tmp = tmp / (array2D.Length / 2);

            return tmp;
        }

        double getStandardDeviation(double[,] array2D, int targetParameter)
        {
            return Math.Round(Math.Sqrt(getVariance(array2D, targetParameter)), 2);
        }

        #region Pearson's r

        double sumOfProducts(double[,] array2D)
        {
            double tmp = 0;
            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                //  Console.WriteLine("tmp przed: " + tmp + "array2D[i, 0]: " + array2D[i, 0] + " * " + array2D[i, 1]);

                tmp = tmp + array2D[i, 0] * array2D[i, 1];
                //   Console.WriteLine("tmp po: " + tmp);


            }
            // Console.WriteLine("sumOfProducts: " + tmp);
            return tmp;
        }

        double sumOfTargetParameter(double[,] array2D, int targetParameter)
        {
            double tmp = 0;
            for (int i = 0; i < array2D.GetLength(0); i++)
            {

                tmp = tmp + array2D[i, targetParameter];
            }
            //  Console.WriteLine("sumOf[ " + targetParameter + "] = " + tmp);
            return tmp;
        }

        double sumOfPoweredTargetParameter(double[,] array2D, int targetParameter)
        {
            double tmp = 0;
            for (int i = 0; i < array2D.GetLength(0); i++)
            {

                tmp = tmp + Math.Pow(array2D[i, targetParameter], 2);
            }
            //  Console.WriteLine("sumOfPowered[ " + targetParameter + "] = " + tmp);
            return tmp;
        }

        double getPearsonsR(double[,] array2D)
        {
            double numerator;
            double denominator;

            numerator = (sumOfProducts(array2D) / array2D.GetLength(0)) - (arithmeticAverage(array2D, idAge) * arithmeticAverage(array2D, idPulse));
            denominator = getStandardDeviation(array2D, idAge) * getStandardDeviation(array2D, idPulse);
            Console.WriteLine("num = " + numerator + "denum" + denominator + " = " + numerator / denominator);
            if (denominator == 0)
            {
                return 0;
            }
            else
                return Math.Round(numerator / denominator, 2);

        }

        #endregion
        #region Linear Regression

        void setParameters(double[,] array2D)
        {
            setA(array2D);
            setB(array2D);
        }
        public double linearRegression(double X)
        {

            return Math.Round(A * X + B, 0);
        }


        void setA(double[,] array2D)
        {
            double numerator;
            double denominator;

            numerator = array2D.GetLength(0) * sumOfProducts(array2D) - sumOfTargetParameter(array2D, idAge) * sumOfTargetParameter(array2D, idPulse);
            denominator = array2D.GetLength(0) * sumOfPoweredTargetParameter(array2D, idAge) - Math.Pow(sumOfTargetParameter(array2D, idAge), 2);

            A = numerator / denominator;
        }

        void setB(double[,] array2D)
        {

            B = (sumOfTargetParameter(array2D, idPulse) - A * sumOfTargetParameter(array2D, idAge)) / array2D.GetLength(0);
        }

        #endregion Linear Regression
     

        public void calculateMinMax(double [,] NewArray2D)
        {
            maxPulse = getMax(NewArray2D, idPulse);
            minPulse = getMin(NewArray2D, idPulse);
            maxAge = getMax(NewArray2D, idAge);
            minAge = getMin(NewArray2D, idAge);
        }

        public void calculateValues()
        {
            maxPulse = getMax(NewArray2D, idPulse);
            minPulse = getMin(NewArray2D, idPulse);
            maxAge = getMax(NewArray2D, idAge);
            minAge = getMin(NewArray2D, idAge);

            expectedValuePulse = getExpectedValue(NewArray2D, idPulse);
            expectedValueAge = getExpectedValue(NewArray2D, idAge);

            copyTab();
            medianAge = getMedian(sortedArray2DbyAge, idAge);
            medianPulse = getMedian(sortedArray2DbyPulse, idPulse);
            standardDeviationAge = getStandardDeviation(NewArray2D, idAge);
            standardDeviationPulse = getStandardDeviation(NewArray2D, idPulse);

            pearsonsR = getPearsonsR(NewArray2D);
            setParameters(NewArray2D);
            q1 = getQ1(NewArray2D, idPulse);
            q3 = getQ3(NewArray2D, idPulse);
            iqr = getIQR(NewArray2D, idPulse);
            getindividualPoints(NewArray2D, idPulse);

        }
    }
}
