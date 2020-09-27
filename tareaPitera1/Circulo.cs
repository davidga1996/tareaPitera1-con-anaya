/*
 * Created by SharpDevelop.
 * User: david
 * Date: 27/09/2020
 * Time: 02:16 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace tareaPitera1
{
	/// <summary>
	/// Description of Circulo.
	/// </summary>
	public class Circulo {
		private int x;
		private int y;
		private int r;
		
		
		public Circulo() { }
		
		public Circulo(int x, int y, int r) {
			this.x = x;
			this.y = y;
			this.r = r;
		}
		
		public int X { get {return x;} }
		public int Y { get {return y;} }
		public int R { get {return r;} }
		
		public override String ToString() {
			return  String.Format("({0}, {1}) -> {2}", x, y, r) ;
		}
	}
}
