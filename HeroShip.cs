using System;
using System.Drawing;

namespace GameTools
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class HeroShip : Ship
	{
		public Ship[] wingman = new Ship[2];
		public Sheild shield;


		/*public HeroShip() : base ()
		{
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Blue);
			wingman[0] = new Ship(getLeft() - 25, getMiddleY() - 10, 25, 25, getImage());
			wingman[1] = new Ship(getRight(), getMiddleY() - 10, 25, 25, getImage());
			active = true;
		}

		public HeroShip (int x, int y, int wide, int high) : base (x, y, wide, high)
		{
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Blue);
			wingman[0] = new Ship(getLeft() - 25, getMiddleY() - 10, 25, 25, getImage());
			wingman[1] = new Ship(getRight(), getMiddleY() - 10, 25, 25, getImage());
			active = true;
		}
		*/
		public HeroShip (int x, int y, int wide, int high, Image i) : base (x, y, wide, high, i)
		{
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Blue);
			wingman[0] = new Ship(getLeft() - 25, getMiddleY() - 10, 35, 35, getImage(), 50, 50);
			wingman[1] = new Ship(getRight(), getMiddleY() - 10, 35, 35, getImage(), 50, 50);
			shield = new Sheild(x - 10, y - 5, wide + 20, high + 20, 200);
			active = false;
			visible = false;
		}

		public HeroShip (int x, int y, int wide, int high, Image i, int shieldCharge) : base (x, y, wide, high, i)
		{
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Blue);
			wingman[0] = new Ship(getLeft() - 25, getMiddleY() - 10, 35, 35, getImage(), 50, 50);
			wingman[1] = new Ship(getRight(), getMiddleY() - 10, 35, 35, getImage(), 50, 50);
			shield = new Sheild(x - 10, y - 5, wide + 20, high + 20, shieldCharge);
			active = false;
			visible = false;
		}
			
		public override void DrawSprite(Graphics g)
		{
			base.DrawSprite(g);
			shield.Draw(g);
		}
		
	}
}
