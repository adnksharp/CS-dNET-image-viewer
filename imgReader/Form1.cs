using System;
using System.Drawing;
using System.Windows.Forms;

namespace imgReader
{
	public partial class Form1 : Form
	{
		int[] y = { 0, 0 };
		bool zoom, hide, mode, move = true;
		public Form1()
		{
			InitializeComponent();
		}

		private void Setup(object sender, EventArgs e)
		{
			Text = "Visualizador de imagenes";
			button1.Text = "Abrir imagen";
			button2.Text = "Mover";
			button3.Text = "Girar";
			button3.Hide();
		}

		private void Mode(object sender, EventArgs e)
		{
			mode = !mode;
			zoom = false;
			move = false;
			if (mode)
			{
				button2.Text = "Ampliar";
				zoom = true;
			}
			else
			{
				button2.Text = "Mover";
				move = true;
			}
		}

		private void Rotate(object sender, EventArgs e)
		{
			Image rotate = pictureBox1.Image;
			if (zoom)
				rotate.RotateFlip(RotateFlipType.Rotate90FlipNone);
			else
				rotate.RotateFlip(RotateFlipType.Rotate270FlipNone);
			pictureBox1.Image = rotate;
		}

		private void Input(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				button3.Show();
				try
				{
					pictureBox1.Load(openFileDialog1.FileName);
					pictureBox1.Width = 525;
					pictureBox1.Height = 260;
					pictureBox1.Location = new Point(3, 3);
				}
				catch
				{
					button3.Hide();
				}
			}
		}

		private void Go(object sender, EventArgs e)
		{
			hide = !hide;
			if (hide)
				Cursor.Hide();
			else
				Cursor.Show();
			y[0] = Cursor.Position.X;
			y[1] = Cursor.Position.Y;
		}

		private void Run(object sender, MouseEventArgs e)
		{
			if (hide)
			{
				int[] x = { Cursor.Position.X, Cursor.Position.Y };
				if (zoom)
				{
					pictureBox1.Width -= (x[0] - y[0]);
					pictureBox1.Height -= (x[1] - y[1]);
					pictureBox1.Location = new Point(pictureBox1.Location.X + (x[0] - y[0]), pictureBox1.Location.Y);
				}
				else if (move)
				{
					pictureBox1.Location = new Point(pictureBox1.Location.X + (x[0] - y[0]), pictureBox1.Location.Y + (x[1] - y[1]));
				}
				Cursor.Position = new Point(y[0], y[1]);
			}
		}
	}
}
