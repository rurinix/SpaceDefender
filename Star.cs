using System;
using System.Drawing;

namespace SpaceDefender
{
	/// <summary>
	/// Summary description for Star.
	/// </summary>
	public class Star
	{
		protected int X;
		protected int Y;
		protected Color c;
		protected int size;
		protected int speed;
		protected int twinkleTime;
		protected bool twinkling;

		private Random randomizer = new Random();

		public Star()
		{

			X = 0;
			Y = 0;
			c = Color.Black;
			size = 1;
			speed = 0;
			twinkleTime = 0;
			twinkling = false;


			//
			// TODO: Add constructor logic here
			//
		}

		public Star (int x, int y, Color starColor, int diameter)
		{
			X = x; 
			Y = y;
			c = starColor;
			size = diameter;
			setSpeed();
			twinkleTime = 0;
			twinkling = false;
		}

		public Star (int x, int y, Color starColor, int diameter, int pixelsPerFrame)
		{
			X = x; 
			Y = y;
			c = starColor;
			size = diameter;
			speed = pixelsPerFrame;
			twinkleTime = 0;
			twinkling = false;
		}

		public Star (int x, int y, Color starColor, int diameter, int pixelsPerFrame, int twinkleDelay)
		{
			X = x; 
			Y = y;
			c = starColor;
			size = diameter;
			speed = pixelsPerFrame;
			twinkleTime = twinkleDelay;
			twinkling = false;
		}

		public int getX ()
		{
			return X;
		}

		public int getY ()
		{
				return Y;
		}

		public int getSpeed()
		{
			return speed;
		}

		public int getSize()
		{
			return size;
		}

		public Color getColor()
		{
			return c;
		}

		public int getTwinkleTime()
		{
			return twinkleTime;
		}
		
		public void setX (int x)
		{
			X = x;
		}

		public void setY (int y)
		{
			Y = y;
		}

		public void setColor (Color newColor)
		{
			c = newColor;
		}

		public void setSize (int diameter)
		{
			size = diameter;
		}

		public void setSpeed ()
		{
			speed = randomizer.Next(1, 7);
		}

		public void setSpeed (int pixelsPerFrame)
		{
			speed = pixelsPerFrame;
		}
		
		public void setTwinkleTime (int durration)
		{
			twinkleTime = durration;
		}

		public void setTwinkling (bool trueFalse)
		{
			twinkling = trueFalse;
		}

		public bool isTwinkling ()
		{
			return twinkling;
		}

		public void Draw (Graphics g)
		{
			for (int count = size; count > 0; count--)
			{
				int red = c.R;
				int blue = c.B;
				int green = c.G;
				int trans = 255/(count * 2);

				g.DrawEllipse(new Pen(Color.FromArgb(trans, red, green, blue), 2), this.getX() - count/2, this.getY() - count/2, count, count);
			}
		}

		public void Animate (int newTwinkleTime)
		{
			if (isTwinkling())
			{
				setSize(getSize()-2);
				setTwinkling(false);
			}
			if (getTwinkleTime() > 0)
				setTwinkleTime(getTwinkleTime() - 1);
			else
			{
				setTwinkleTime(newTwinkleTime);
				setTwinkling(true);
				setSize(getSize() +2);
			}
		}
	}
}
