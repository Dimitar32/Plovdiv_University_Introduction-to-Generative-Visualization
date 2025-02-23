using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Button generateButton;
        private Button saveButton;
        private PictureBox canvas;
        private Bitmap bitmap;
        private Random random;
        private int num = 150; // Number of nodes

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Random Network Visualizer";
            this.Size = new Size(820, 860);

            generateButton = new Button { Text = "Generate", Location = new Point(10, 10) };
            saveButton = new Button { Text = "Save", Location = new Point(100, 10) };
            canvas = new PictureBox { Location = new Point(10, 50), Size = new Size(800, 800) };

            generateButton.Click += (s, e) => GenerateImage();
            saveButton.Click += (s, e) => SaveImage();

            this.Controls.Add(generateButton);
            this.Controls.Add(saveButton);
            this.Controls.Add(canvas);

            random = new Random();
            bitmap = new Bitmap(canvas.Width, canvas.Height);
            canvas.Image = bitmap;
        }

        private void GenerateImage()
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                Pen pen = new Pen(Color.FromArgb(10, 0, 0, 0));

                float[] angle1 = new float[num];
                float[] angle2 = new float[num];
                float[] x1 = new float[num];
                float[] x2 = new float[num];
                float[] y1 = new float[num];
                float[] y2 = new float[num];

                float centerX = canvas.Width / 2;
                float centerY = canvas.Height / 2;

                for (int i = 0; i < num; i++)
                {
                    float radius = (float)(random.NextDouble() * (canvas.Width / 2 - 10) + 88);
                    angle1[i] = (float)(random.NextDouble() * Math.PI * 2);
                    x1[i] = (float)(Math.Sin(angle1[i]) * radius + centerX);
                    y1[i] = (float)(Math.Cos(angle1[i]) * radius + centerY);
                    angle2[i] = (float)(random.NextDouble() * Math.PI * 2);
                    x2[i] = (float)(Math.Sin(angle2[i]) * radius + centerX);
                    y2[i] = (float)(Math.Cos(angle2[i]) * radius + centerY);
                }

                for (int i = 0; i < num; i++)
                {
                    for (int a = 1; a < num; a++)
                    {
                        pen.Width = (float)(random.NextDouble() * 1.5 + 0.5);
                        g.DrawLine(pen, x1[i], y1[i], x2[a], y2[a]);
                    }
                }
            }
            canvas.Refresh();
        }

        private void SaveImage()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), $"{DateTime.Now.Ticks}.png");
            bitmap.Save(path);
            MessageBox.Show($"Image saved: {path}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
