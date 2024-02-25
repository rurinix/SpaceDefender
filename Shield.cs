using System;
using System.Drawing;

namespace GameTools
{
	/// <summary>
	/// Summary description for Sheild.
	/// </summary>
	public class Sheild : GameObject
	{
		protected int charge;
		protected int maxCharge;
		protected int delay;
		protected bool poweringUp;
		protected bool poweringDown;
		protected Sprite sprite;
		protected bool hit;
		protected int hitDelay;
	

		public Sheild()
		{
			maxCharge = 100;
			charge = maxCharge;
			//
			// TODO: Add constructor logic here
			//
		}

		public Sheild (int x, int y, int wide, int high) : base (x, y, wide, high)
		{
			maxCharge = 100;
			charge = maxCharge;
			delay = 5;
			poweringUp = false;
			poweringDown = false;
			sprite = new Sprite(x, y, wide, high, Image.FromFile("images\\shield.png"));
			hit = false;
			hitDelay = -1;
		}

		public Sheild (int x, int y, int wide, int high, int shieldCharge) : base (x, y, wide, high)
		{
			maxCharge = shieldCharge;
			charge = maxCharge;
			delay = 5;
			poweringUp = false;
			poweringDown = false;
			sprite = new Sprite(x, y, wide, high, Image.FromFile("images\\shield.png"));
			hit = false;
			hitDelay = -1;
		}

		public void adjustCharge (int offset)
		{
			if (charge + offset >= 0 && charge + offset <= maxCharge)
				charge += offset;
			else if (charge + offset > maxCharge)
				charge = maxCharge;
			else
			{
				charge = 0;
				this.ShutDown();
			}
		}

		public int getCharge ()
		{
			return charge;
		}

		public int getMaxCharge ()
		{
			return maxCharge;
		}

		public virtual void Draw (Graphics g)
		{
			if (this.isVisible())
			{
				if (delay > 1)
					sprite.Draw(g, this.getMiddleX() - (this.getWidth()/2)/delay, this.getMiddleY() - (this.getHeight()/2)/delay, sprite.getRow() , sprite.getColumn(), this.width/delay, this.height/delay);
				else
					sprite.Draw(g, this.getLeft(), this.getTop(), sprite.getRow() , sprite.getColumn() , this.width, this.height);
				//g.DrawString("" + sprite.getColumn(), new Font ("Arial", 12, FontStyle.Bold), Brushes.White, sprite.getX() + sprite.getWidth()/2, sprite.getY() + sprite.getHeight()/2);
			} 
		}

		public void StartUp()
		{
			sprite.setColumn(1);
			this.setVisible(true);
			this.poweringDown = false;
			this.poweringUp = true;
		}

		public void ShutDown()
		{
			this.setActive(false);
			this.poweringUp = false;
			this.poweringDown = true;
			hit = false;
		}

		public override void Animate()
		{
			if (this.poweringUp)
			{
				delay --;
				if (delay == 1)
				{
					poweringUp = false;
					this.setActive(true);
				}
			}
			else if (this.poweringDown)
			{
				delay ++;
				if (delay == 5)
				{
					poweringDown = false;
					this.setVisible(false);
				}
			}
			if (sprite.getColumn() > 1)
			{
				if (hit && sprite.getColumn() < 6)
					sprite.setColumn(sprite.getColumn() + 1);
				else if (!hit && sprite.getColumn() > 1)
					sprite.setColumn(sprite.getColumn() - 1);
			}
			if (hitDelay > 0)
				hitDelay --;
			if (hitDelay == 0 && hit)
			{
				//sprite.setColumn(2);
				hit = false;
			}
		}

		public void hitSomething()
		{
			if (!hit)
				sprite.setColumn(2);
			hit = true;
			hitDelay += 15;
			
		}

	}
}
