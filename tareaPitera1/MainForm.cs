/*
 * Created by SharpDevelop.
 * User: david
 * Date: 27/09/2020
 * Time: 10:50 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace tareaPitera1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form {
		OpenFileDialog openfile = new OpenFileDialog();
		Bitmap bmp;
		List<Circulo> circulos;
		public MainForm() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void cargarClick(object sender, EventArgs e) {
			if(openfile.ShowDialog() == DialogResult.OK) {
				//carga archivo de imagen
				//ponerla en el picturebox
				pictureBox1.ImageLocation = openfile.FileName;
				pictureBox2.ImageLocation = openfile.FileName;
				listBox.Items.Clear();
			} else {
				MessageBox.Show("archivo no cargado");
			}
		}
		
		void ejecutarClick(object sender, EventArgs e) {
			bmp = new Bitmap(pictureBox1.Image);
			circulos = new List<Circulo>();
			Circulo aux = new Circulo();
			listBox.Items.Clear();
			
			for(int y = 0; y < bmp.Height; y++) {
				for(int x = 0; x < bmp.Width; x++) {
					if(bmp.GetPixel(x, y).ToArgb().Equals(Color.Black.ToArgb())) {
						aux = buscarCirculo(x,y);
						if(aux != null) {
							circulos.Add(aux);
							listBox.Items.Add(aux.ToString());
						}
					}
				}
			}
			crearCirculos();
			pictureBox2.Image = bmp;
		}
		
		Circulo buscarCirculo(int x, int y) {
			int xi = x, yi = y, w, h;
			int cx, cy;
			int aux = 0;	
			
			//busca final x
			while(bmp.GetPixel(x, y).ToArgb().Equals(Color.Black.ToArgb())) { x++; }
			
			cx = (x+xi)/2;
			
			//mover abajo
			while(bmp.GetPixel(cx, y).ToArgb().Equals(Color.Black.ToArgb())) { y++; }
			
			cy = (y+yi)/2;
			
			h = y - yi;
			
			//mover izquierda
			while(bmp.GetPixel(cx - aux, cy).ToArgb().Equals(Color.Black.ToArgb())) { aux++; }
			
			w = aux;
			aux = 0;
			
			//mover derecha
			while(bmp.GetPixel(cx + aux, cy).ToArgb().Equals(Color.Black.ToArgb())) { aux++; }
			
			//recalcular el centro en x
			cx = (cx - w) + (w + aux)/2;
			
			w += aux-1;
			
			if(Math.Abs(w-h) > 10) {
				//pendejo no es un circulo, revisa bien
				//esto es un ovalo y lo debemos borrar
				rellenarCirculo(cx, cy, w+10, h+10, Color.White);
				return null;
			} else {
				//w = w > h ? w : h;
				if(h > w) {
					w = h;
				}
				rellenarCirculo(cx, cy, w+5, w+5, Color.White);
				return new Circulo(cx, cy, w+5);
			}
		}
		
		void rellenarCirculo(int cx, int cy, int w, int h, Color color) {
			Graphics g = Graphics.FromImage(bmp);
			Brush brush = new SolidBrush(color);
			
			g.FillEllipse(brush, cx-w/2, cy-h/2, w, h);
			g.Dispose();
		}
		
		void crearCirculos() {
			foreach(Circulo c in circulos) {
				rellenarCirculo(c.X, c.Y, c.R, c.R, Color.Black);
				rellenarCirculo(c.X, c.Y, 20, 20, Color.Red);
			}
		}
	}
}












