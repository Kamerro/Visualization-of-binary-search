using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TabOfList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            inicializeGrid();
            this.Show();
                ContainerVals.Pole = RozpocznijDzialanie(0, this.Controls.Count - 1, (this.Controls.Count - 1) / 2);
                if (ContainerVals.Pole != -1)
                {
                    PictureBox pb = this.Controls[ContainerVals.Pole - 1] as PictureBox;
                    pb.BackColor = Color.Black;

                }
                this.Refresh();
                if (ContainerVals.Pole == 1 || ContainerVals.Pole == this.Controls.Count)
                {
                    MessageBox.Show("hehs");
                }
        }

        private int RozpocznijDzialanie(int leftBoundary, int rightBoundary, int numerRozpatrywanegoPola)
        {
            this.Refresh();
            (this.Controls[numerRozpatrywanegoPola - 1] as PictureBox).BackColor = Color.Blue;
            (this.Controls[numerRozpatrywanegoPola - 1] as PictureBox).Refresh();
            Thread.Sleep(500);
            (this.Controls[numerRozpatrywanegoPola - 1] as PictureBox).BackColor = Color.White;
            if (numerRozpatrywanegoPola == ContainerVals.numberOfTheChosen)
            {
                return numerRozpatrywanegoPola;
            }
            else if (numerRozpatrywanegoPola>ContainerVals.numberOfTheChosen)
            {
                //Wtedy idziemy w lewo. czyli przesuwamy prawą granicę do punktu gdzie byliśmy
                int newPoint = (numerRozpatrywanegoPola + leftBoundary) / 2;
                numerRozpatrywanegoPola=RozpocznijDzialanie(leftBoundary, numerRozpatrywanegoPola, newPoint);
            }
            else if (numerRozpatrywanegoPola < ContainerVals.numberOfTheChosen)
            {
                //Wtedy idziemy w prawo. czyli przesuwamy prawą granicę do punktu gdzie byliśmy
                int newPoint = (rightBoundary + numerRozpatrywanegoPola ) / 2;
                numerRozpatrywanegoPola = RozpocznijDzialanie(numerRozpatrywanegoPola, rightBoundary, newPoint);
            }
            return numerRozpatrywanegoPola;
        }

        private void WriteTextOnPictureBox(string text, PictureBox pictureBox,bool taknie)
        {
            using (Graphics graphics = Graphics.FromImage(pictureBox.Image))
            {
                // Ustawiamy czcionkę i kolor tekstu
                Font font = new Font("Arial", pictureBox.Width/5);
                Brush brush;
                if (!taknie)
                    brush = new SolidBrush(Color.Black);

                else
                    brush = new SolidBrush(Color.Pink);
                // Ustawiamy pozycję tekstu (możesz dostosować według potrzeb)
                float x = pictureBox.Width/2- font.Size;
                float y = pictureBox.Height/2- font.Size;

                // Rysujemy tekst na obrazie PictureBox
                graphics.DrawString(text, font, brush, x, y);
            }

            // Odświeżamy PictureBox, aby pokazać narysowany tekst
            pictureBox.Refresh();
        }
        private void pictureBoxHelperIn(object sender, EventArgs e)
        {
            var obj = sender as PictureBox;
            if (obj != null)
            {
                obj.BackColor = Color.Blue;
            }
        }
        private void pictureBoxHelperOut(object sender, EventArgs e)
        {
            var obj = sender as PictureBox;
            if (obj != null)
            {
                obj.BackColor = Color.White;
            }
        }

        private void inicializeGrid()
        {
            Random rand = new Random();
            this.Text = "Binary search";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.Width = 8*80;
            this.Height = this.Width;
            int sizeOfPB = this.Width / 8;
            int margin = 5;
            int iteracja = 1;
            int totalELements = 90;
            ContainerVals.numberOfTheChosen = rand.Next(1, totalELements + 1);
            for (int i = margin; i < this.Width; i += sizeOfPB + margin)
            {
                for (int j = margin; j < this.Height; j += sizeOfPB)
                {
                    try
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Size = new Size(sizeOfPB, sizeOfPB);
                        pictureBox.Image = new Bitmap(sizeOfPB, sizeOfPB);
                        pictureBox.BackColor = Color.White;
                        pictureBox.MouseEnter += pictureBoxHelperIn;
                        pictureBox.MouseLeave += pictureBoxHelperOut;
                        pictureBox.BorderStyle = BorderStyle.FixedSingle;
                        if (iteracja != ContainerVals.numberOfTheChosen)
                            WriteTextOnPictureBox(iteracja.ToString(), pictureBox, false);

                        else
                        {
                            WriteTextOnPictureBox(iteracja.ToString(), pictureBox, true);
                        }
                        this.Controls.Add(pictureBox);
                        pictureBox.Location = new Point(j, i);
                    }
                    catch
                    {

                    }
                    iteracja++;
                }


            }
            this.Width += sizeOfPB / 2 + margin;
            this.Height += sizeOfPB + margin;
        }
    }


    public static class ContainerVals
    {
       public static int numberOfTheChosen = default;

        public static int Pole { get; internal set; }
    }
}
