using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TheGameOfLIFE
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        
        private CalculationAutomata calculationAutomata;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timeDisplay.Enabled)
                return;
            //_currentGeneration = 0;

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            resolution = (int)nudResolution.Value;

            calculationAutomata = new CalculationAutomata(/*rows*/ fieldForDrawing.Height / resolution, /*cols*/fieldForDrawing.Width / resolution, (int)nudDensity.Value);

            Text = $"Generation: {calculationAutomata.CurrentGeneration}";


            fieldForDrawing.Image = new Bitmap(fieldForDrawing.Width, fieldForDrawing.Height);
            graphics = Graphics.FromImage(fieldForDrawing.Image);

            timeDisplay.Start();
        }
        private void DrawNewGeneration()
        {
            graphics.Clear(Color.Black);
            var field = calculationAutomata.CountingCurrentGeneration();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y])
                    {
                        graphics.FillEllipse(Brushes.Aquamarine, x * resolution, y * resolution, resolution, resolution);
                    }
                }
            }

            fieldForDrawing.Refresh();
            Text = $"Generation: {calculationAutomata.CurrentGeneration}";
            calculationAutomata.CalculateGeneration();
        }
        private void StopGame()
        {
            if (!timeDisplay.Enabled)
            {
                return;
            }
            timeDisplay.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNewGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
