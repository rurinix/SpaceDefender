using System;
using System.Drawing;

namespace GameTools 
{
	/// <summary>
	/// Summary description for PowerUp.
	/// </summary>
	public class PowerUp : GameObject
	{
		protected int type;
		protected int frame;
		//protected char typePrefix;

		protected Sprite sprite;
		protected Sprite overlay;
		public DisplayString displayString;

		/*public PowerUp() : base ()
		{
			type = 1;
			frame = 0;
			sprite.setImage(Image.FromFile("images\\powerup.png"));
			setType();

		}

		public PowerUp(int x, int y, int wide, int high) : base (x, y, wide, high)
		{
			type = 1;
			frame = 0;
			setType();
			sprite.setImage(Image.FromFile("images\\powerup.png"));

		}*/

		public PowerUp(int x, int y, int wide, int high, Image i) : base (x, y, wide, high)
		{
			sprite = new Sprite (x, y, width, height, i);
			overlay = new Sprite (x, y, width, height, Image.FromFile("images\\overlay.png"));
			displayString = new DisplayString (x, y, new Font("Arial", 12, FontStyle.Bold), " ", 255);
			type = 1;
			frame = 0;
			setType();
		}

		public PowerUp(int x, int y, int wide, int high, Image i, int index, int framecount) : base (x, y, wide, high)
		{
			sprite = new Sprite(x, y, width, high, i);
			overlay = new Sprite (x, y, width, height, Image.FromFile("images\\overlay.png"));
			displayString = new DisplayString (x, y, new Font("Arial", 12, FontStyle.Bold), " ", 255);
			type = index;
			frame = framecount;
			setType();
		}

		public PowerUp(int x, int y, int wide, int high, Image i, int index, int framecount, bool activated, bool canSee) : base (x, y, wide, high)
		{
			sprite = new Sprite(x, y, width, high, i);
			overlay = new Sprite (x, y, width, height, Image.FromFile("images\\overlay.png"));
			displayString = new DisplayString (x, y, new Font("Arial", 12, FontStyle.Bold), " ", 255);
			type = index;
			frame = framecount;
			setActive(activated);
			setVisible(canSee);
			setType();
		}

		public override void Animate ()
		{
			setTop(getTop() + 3);

			if (frame < 7)
				frame++;
			else
				frame = 0;

	
		}

		public void Animate (int speed)
		{
			setTop(getTop() + speed);

			if (frame < 7)
				frame++;
			else
				frame = 0;
			
		}

		public void setType (int typeCode)
		{
			type = typeCode;
			sprite.setColumn(type);
			setType();
		}

		public void setType ()
		{	
			sprite.setColumn(type);
			
		}

		public int getType ()
		{
			return type;
		}

		public Image getImage()
		{
			return sprite.getImage();
		}

		public void Draw (Graphics g)
		{
			if (this.isVisible())
			{
				if (this.isActive())
				{
					sprite.setColumn(type + 1);
					sprite.Draw(g, getLeft(), getTop());
				}
				else
				{
					if (overlay.getRow() <= 2)
						sprite.Draw(g, getLeft(), getTop());// sprite.getRow(), sprite.getColumn() + 1, getWidth()/3 * 2, this.getHeight()/3 * 2);
					overlay.Draw(g, getLeft(),getTop());
					if (overlay.getRow() >= overlay.getMaxRow())
					{
						this.setVisible(false);
						overlay.setRow(1);
					}
				}
			}
		}
		public void DrawOverlay (Graphics g)
		{
			this.displayString.Draw(g);
		}
	}
}
