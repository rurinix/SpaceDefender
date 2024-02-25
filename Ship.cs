using System;
using System.Drawing;


namespace GameTools
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Ship : GameObject
	{
		// create GameTools
		public Laser[] laser = new Laser[15];
		protected Sprite sprite;
		protected Sprite explodingSprite;

		// create local variables
		protected int exhaustSize;
		protected int cooldownCounter;
		protected bool exploding;
		public Sprite exhaust = new Sprite();

		public Ship (int x, int y, int wide, int high, Image i) : base (x, y, wide, high)
		{
			makeLasers();
			active = true;
			cooldownCounter = 15;
			exhaustSize = 5;
			exploding = false;

			sprite = new Sprite(x, y, width, height, i);
			explodingSprite = new Sprite(x, y, 100, 50, Image.FromFile("images\\exploding.png"));
			exhaust = new Sprite(this.getLeft(), this.getBottom(), 50, 50, Image.FromFile("images\\exhaust.png"));
		}

		public Ship (int x, int y, int wide, int high, Image i, int imageWidth, int imageHeight) : base (x, y, wide, high)
		{
			makeLasers();
			active = true;
			cooldownCounter = 15;
			exhaustSize = 5;
			exploding = false;

			sprite = new Sprite(x, y, imageWidth, imageHeight, i);
			explodingSprite = new Sprite(x, y, 100, 50, Image.FromFile("images\\exploding.png"));
			exhaust = new Sprite(this.getLeft(), this.getBottom(), 50, 50, Image.FromFile("images\\exhaust.png"));
		}

		public Ship (int x, int y, int wide, int high, Image i, int imageWidth, int imageHeight, Image ex) : base (x, y, wide, high)
		{
			makeLasers();
			active = true;
			cooldownCounter = 15;
			exhaustSize = 5;
			exploding = false;

			sprite = new Sprite(x, y, imageWidth, imageHeight, i);
			explodingSprite = new Sprite(x, y, 100, 50, ex);
			exhaust = new Sprite(this.getLeft(), this.getBottom(), 50, 50, Image.FromFile("images\\exhaust.png"));
		}

		public void tryToFire(bool trueFalse)
		{
			if (active && top > -100)
			{
				for (int count = 0; count < this.laser.Length; count ++)
				{
					if (!this.laser[count].isActive() && !this.laser[count].isVisible())
					{
						this.laser[count].setActive(true);
						this.laser[count].setVisible(true);
						this.laser[count].setTop(this.getTop() + this.laser[count].getHeight());
						this.laser[count].setLeft(this.getMiddleX());
						count = this.laser.Length;

					// Playing sound files slows down gameplay!!!!
						if (trueFalse)
						{
							Sound.FlushAudio();
							//ThreadedSound.PlayAudioAsync("sounds\\laser.wav");
							Sound.PlayAudioAsync("sounds\\laser.wav");
						}
					}
				}
			}
		}

		protected void makeLasers()
		{
			for (int count = 0; count < laser.Length; count++)
				laser[count] = new Laser(this.getMiddleX(), this.getMiddleY(), 3, 9, 6, Color.White);

		}
		protected void makeLasers(int X, int Y, int wide, int tall, int speed, Color laserColor)
		{
			for (int count = 0; count < laser.Length; count++)
				laser[count] = new Laser(X, Y, wide, tall, speed, laserColor);

		}

		public void moveLasers(int X, int Y)
		{
				for (int count = 0; count < laser.Length; count++)
				{
					laser[count].setLeft(X);
					laser[count].setTop(Y);
					laser[count].setActive(false);
					laser[count].setVisible(false);
				}
		}

		public void AnimateLasersUp(int max)
		{
			for (int count = 0; count < laser.Length ; count++)
			{
					
				if (laser[count].isActive() && laser[count].isVisible())
				{
					laser[count].setTop(laser[count].getTop() - laser[count].getSpeed());
					if (laser[count].getBottom() < max)
					{
						laser[count].setActive(false);
						laser[count].setVisible(false);
					}
				}
			}
		}

		public void AnimateLasersDown(int max)
		{
			for (int count = 0; count < laser.Length ; count++)
			{
					
				if (laser[count].isActive() && laser[count].isVisible())
				{
					laser[count].setTop(laser[count].getTop() + laser[count].getSpeed());
					if (laser[count].getTop() > max )
					{
						laser[count].setActive(false);
						laser[count].setVisible(false);
					}
				}
			}
		}

		public void drawLasers(Graphics g)
		{
			for (int count = 0; count < this.laser.Length; count++)
			{
				if (this.laser[count].isVisible()) // only draw the lasers if they are visible
					this.laser[count].Draw(g);						
			}
		}

		public void setLaserDelay (int laserDelay)
		{
			cooldownCounter = laserDelay;
		}

		public int getLaserDelay()
		{
			return cooldownCounter;
		}

		public void Draw (Graphics g)
		{ 
			if (this.isVisible())
			{
				DrawExhaust(g);
				DrawSprite(g);
			}
		}

		public void DrawExhaust(Graphics g)
		{
			/*if (exhaustSize < 10)
				exhaustSize++;
			else
				exhaustSize = 5;
		
			for (int count = this.getWidth()/4 + exhaustSize; count > 0; count--)
			{
				int trans = 255/count;
				g.DrawEllipse(new Pen(Color.FromArgb(trans, 240, 220, 85), 2), this.getMiddleX() - count/2, this.getBottom() - count/2, count, count);

			}
			for (int count = this.getWidth()/3 + exhaustSize; count > 0; count--)
			{
				int trans = 255/count;
				g.DrawEllipse(new Pen(Color.FromArgb(trans, 255, 150, 0), 2), this.getMiddleX() - count/2, this.getBottom()- count/2, count, count);

			} */
			exhaust.Draw(g, this.getLeft(), this.getBottom() - exhaust.getHeight()/4, exhaust.getRow() , exhaust.getColumn(), this.getWidth(), this.getHeight());
		//	g.DrawString("" + exhaust.getRow(), new Font("Arial", 12, FontStyle.Bold), Brushes.White, this.getLeft() + this.getWidth()/2, this.getBottom() + this.getHeight()/2);

		}

		public virtual void DrawSprite (Graphics g)
		{
			sprite.Draw(g, this.getLeft(), this.getTop(), sprite.getRow() , sprite.getColumn(), this.width, this.height);

		}


		public void setImage (Image i)
		{
			sprite.setImage(i);
		}

		public Image getImage ()
		{
			return sprite.getImage();
		}

		public void setSpriteRow (int index)
		{
			sprite.setRow(index);
		}

		public void setSpriteColumn (int index)
		{
			sprite.setColumn(index);
		}

		public void setExploding (bool trueFalse)
		{
			exploding = trueFalse;
		}

		public bool isExploding ()
		{
			return (exploding);
		}
	}
}
