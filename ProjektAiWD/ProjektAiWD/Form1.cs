using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace ProjektAiWD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataPreprocessingToolStripMenuItem.Enabled = false;
            generateAdditionalDataToolStripMenuItem.Enabled = false;
            btnCalculate.Enabled = false;
            graphLoad();
        }

        private Calculator calculator = new Calculator();
        private Generator g = new Generator();


        #region Load And Check Data

        void dialogWindow()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fdlg.FileName);
            }
            calculator.Path = fdlg.FileName;
        }

        void saveDialogWindows()
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fdlg.FileName);
            }

            g.Path = fdlg.FileName;
        }

        #endregion Load And Check Data
        void calculateValues()
        {

        }
        void showValues()
        {
            calculator.calculateValues();

            label11.Text = calculator.MinAge.ToString();
            label12.Text = calculator.MaxAge.ToString();
            label13.Text = Math.Round(calculator.ExpectedValueAge, 0).ToString();
            label14.Text = calculator.MedianAge.ToString();
            label15.Text = calculator.StandardDeviationAge.ToString();

            label111.Text = calculator.MinPulse.ToString();
            label112.Text = calculator.MaxPulse.ToString();
            label113.Text = Math.Round(calculator.ExpectedValuePulse, 0).ToString();
            label114.Text = calculator.MedianPulse.ToString();
            label115.Text = calculator.StandardDeviationPulse.ToString();

            label16.Text = calculator.PearsonsR.ToString();
            label17.Text = "Y = " + Math.Round(calculator.A, 3) + " * X + " + Math.Round(calculator.B, 3);
            label18.Text = "" + calculator.Q1;
            label25.Text = "" + calculator.Q3;
            label26.Text = "" + calculator.Iqr;

            calculator.show(calculator.IndividualPoints, "individual tab");

            label19.Text = "puls < " + calculator.ValueMin + " || puls >" + calculator.ValueMax;
            setDataGridIndividualPoints();
            groupBox3.Refresh();

        }

        #region Graph
        double[] x = new double[500];
        double[] y = new double[500];


        void splitMainTable(double[,] newArray2D)
        {
            Array.Resize(ref x, newArray2D.GetLength(0));
            Array.Resize(ref y, newArray2D.GetLength(0));

            for (int i = 0; i < newArray2D.GetLength(0); i++)
            {

                x[i] = newArray2D[i, 0];
                y[i] = newArray2D[i, 1];
            }


            Console.WriteLine("done split");
        }

        void showPoints(double[,] newArray2D)
        {

            zgc.GraphPane.CurveList.Clear();
            splitMainTable(newArray2D);


            CurveItem linia = zgc.GraphPane.AddCurve("puls", x, y, Color.Red, SymbolType.Diamond);
            linia.Line.IsVisible = false;
            int offset = 5;
            Console.WriteLine("calculator.MinAge" + calculator.MinAge);

            zgc.GraphPane.XAxis.PickScale(calculator.MinAge - offset, calculator.MaxAge + offset);
            zgc.GraphPane.YAxis.PickScale(calculator.MinPulse - offset, calculator.MaxPulse + offset);
            zgc.Refresh();

        }

        void showAdditionalPoints(double[,] newArray2D, double[] x, double[] y)
        {
            zgc.GraphPane.CurveList.Clear();
            showPoints(newArray2D);
            showlinearRegression(newArray2D);
            CurveItem points = zgc.GraphPane.AddCurve("estymowana wartość", x, y, Color.DarkMagenta, SymbolType.Square);
            points.Symbol.Size = 5.0F;

            points.Line.IsVisible = false;

            zgc.Refresh();
        }
        void showlinearRegression(double[,] newArray2D)
        {
            GraphPane myPane = zgc.GraphPane;
            double[] y = new double[2];
            double[] x = new double[2];

            x[0] = -System.Math.Abs(2 * calculator.MinAge);
            x[1] = System.Math.Abs(2 * calculator.MaxAge);


            for (int i = 0; i < y.Length; i++)
            {
                y[i] = calculator.linearRegression(x[i]);
            }

            CurveItem reggresionLine = myPane.AddCurve("prosta regresji", x, y, Color.Blue, SymbolType.Circle);
            Console.WriteLine("done");
            zgc.Refresh();
        }

        private void graphLoad()
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.Title = "Wykres";
            myPane.XAxis.Title = "Wiek";
            myPane.YAxis.Title = "Puls";
        }
        #endregion Graph
        #region DataGrid


        void setDataGridAllPoints(double[,] newArray2D)
        {
            Person[] person = new Person[newArray2D.GetLength(0)];

            for (int i = 0; i < newArray2D.GetLength(0); i++)
            {
                person[i] = new Person();

                person[i].wiek = newArray2D[i, 0];
                person[i].puls = newArray2D[i, 1];

                Console.WriteLine(person[i].wiek + " " + person[i].puls);
            }

            dataGridView1.DataSource = person;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        void setDataGridIndividualPoints()
        {

            Person[] person = new Person[calculator.IndividualPoints.GetLength(0)];
            for (int i = 0; i < calculator.IndividualPoints.GetLength(0); i++)
            {
                person[i] = new Person();

                person[i].wiek = calculator.IndividualPoints[i, 0];
                person[i].puls = calculator.IndividualPoints[i, 1];
            }

            dataGridView3.DataSource = person;
            dataGridView3.RowHeadersVisible = false;
            dataGridView3.AutoResizeColumns();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        void setDataGridEstimated()
        {

            Person[] person = new Person[calculator.EstimatedX.Length];

            for (int i = 0; i < calculator.EstimatedX.Length; i++)
            {
                person[i] = new Person();

                person[i].wiek = calculator.EstimatedX[i];
                person[i].puls = calculator.EstimatedY[i];
            }

            dataGridView2.DataSource = person;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }


        #endregion DataGrid
        #region Buttons, Menu 

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogWindow();
            calculator.readFile();
            calculator.calculateMinMax(calculator.Array2D);
            setDataGridAllPoints(calculator.Array2D);
            showPoints(calculator.Array2D);
            dataPreprocessingToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = false;

        }

        private void dataPreprocessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculator.checkValues();
            calculator.calculateMinMax(calculator.NewArray2D);

            setDataGridAllPoints(calculator.NewArray2D);
            showPoints(calculator.NewArray2D);
            zgc.Refresh();
            btnCalculate.Enabled = true;
            dataPreprocessingToolStripMenuItem.Enabled = false;


        }

        private void generateAdditionalDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculator.generateAdditionalData();
            showAdditionalPoints(calculator.NewArray2D, calculator.EstimatedX, calculator.EstimatedY);
            setDataGridEstimated();

        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            calculateValues();
            showValues();
            showlinearRegression(calculator.NewArray2D);
            generateAdditionalDataToolStripMenuItem.Enabled = true;
            btnCalculate.Enabled = false;

        }
     

        #endregion Buttons, Menu 

        private void ageTightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDialogWindows();
            g.generate(false);

        }

        private void ageWidthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDialogWindows();
            g.generate(true);
        }
    }
}
