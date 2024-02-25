using System;
using System.Drawing;

namespace GameTools
{
	/// <summary>
	/// Summary description for Laser.
	/// </summary>
	public class Laser : GameObject
	{
		int frameCount;
		int speed;
		int hitX;
		int hitY;
		Color c;

		bool exploding;

		public Laser() : base ()
		{
			exploding = false;
			visible = false;
			active = false;
			setWidth(3);
			setHeight(10);
			speed = 6;
			c = Color.White;
			frameCount = 0;
		}

		public Laser(int X, int Y, Color color) : this ()
		{
			setTop(X);
			setLeft(Y);
			c = color;


		}
		public Laser (int X, int Y, int wide, int tall, int Speed, Color color) : this (X, Y, color)
		{
			setWidth(wide);
			setHeight(tall);
			speed = Speed;
		}

		public void setSpeed (int Speed)
		{
			speed = Speed;
		}

		public int getSpeed()
		{
			return speed;
		}

		public Color getColor ()
		{
			return c;
		}

		public void Draw (Graphics g)
		{
			if (!exploding)
			{
				if (c.Equals(Color.Blue))
				{
					g.DrawLine(new Pen(Color.FromArgb(115, 25, 25, 155), 2), this.getLeft() - 1, this.getTop() + 2, this.getLeft() - 1, this.getBottom() - 2);
					g.DrawLine(new Pen(Color.FromArgb(115, 20, 20, 135), 2), this.getLeft() + 1, this.getTop() + 2, this.getLeft() + 1, this.getBottom() - 2);
					g.DrawLine(new Pen(Color.FromArgb(255, 20, 20, 255), 2), this.getLeft(), this.getTop(), this.getLeft(), this.getBottom() );

				}
				else if (c.Equals(Color.Red))
				{
					g.DrawLine(new Pen(Color.FromArgb(115, 255, 110, 155), 2), this.getLeft() - 1, this.getTop() + 2, this.getLeft() - 1, this.getBottom() - 2);
					g.DrawLine(new Pen(Color.FromArgb(115, 225, 110, 130), 2), this.getLeft() + 1, this.getTop() + 2, this.getLeft() + 1, this.getBottom() - 2);
					g.DrawLine(new Pen(Color.FromArgb(255, 255, 25, 25), 2), this.getLeft(), this.getTop(), this.getLeft(), this.getBottom() );
				}
				else
				{
					g.DrawLine(new Pen(Color.FromArgb(115, 125, 125, 125), 2), this.getLeft() - 1, this.getTop() + 2, this.getLeft() - 1, this.getBottom() - 2);
					g.DrawLine(new Pen(Color.FromArgb(115, 180, 180, 180), 2), this.getLeft() + 1, this.getTop() + 2, this.getLeft() + 1, this.getBottom() - 2);
					g.DrawLine(new Pen(Color.FromArgb(255, 225, 225, 225), 2), this.getLeft(), this.getTop(), this.getLeft(), this.getBottom() );
				}	
			}
			else
			{
				if (c.Equals(Color.Blue))
				{
					g.FillEllipse(new SolidBrush(Color.FromArgb(115, 0, 0, 255)), hitX - (width - frameCount), hitY - (width - frameCount), (width * 3) - frameCount , (width * 3 - frameCount));
				}

				else if (c.Equals(Color.Red))
				{
					g.FillEllipse(new SolidBrush(Color.FromArgb(155, 225, 0, 0)), hitX - (width - frameCount), hitY - (width - frameCount), (width * 3) - frameCount , (width * 3 - frameCount));
				}

				else
				{
					g.FillEllipse(new SolidBrush(Color.FromArgb(125, 255, 255, 255)), hitX - (width - frameCount), hitY - (width - frameCount), (width * 3) - frameCount , (width * 3 - frameCount));
				}

				if (frameCount < width)
				{
					frameCount ++;
				}
				else
				{
					frameCount = 0;
					this.exploding = false;
					this.visible = false;
					this.active = false;
				}
			}
		}

		public void HitAnimation()
		{
			exploding = true;
			hitX = left;
			hitY = top;

			this.active = false;
		}
		public void HitAnimation (int x, int y)
		{
			exploding = true;
			hitX = x;
			hitY = y;

			this.active = false;
		}

		public bool isExploding ()
		{
			return exploding;
		}
	}
}
