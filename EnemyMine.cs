using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameTools
{
	/// <summary>
	/// Summary description for EnemyMine.
	/// </summary>
	/// 


	public class EnemyMine : GameObject
	{

		protected Sprite sprite;
		protected bool exploding;
		protected bool detinated;
		protected Point startPosition;
		protected int maxLeft;
		protected int maxRight;

		protected bool hit;
		protected bool goLeft;
		protected bool goRight;
		protected bool goUp;
		protected bool goDown;

		public EnemyMine(Point startPoint) : base (startPoint.X, startPoint.Y, 25, 25)
		{
			sprite = new Sprite(startPoint.X, startPoint.Y, 25, 25, Image.FromFile("images\\mine.png"));
			speedX = 0;
			speedY = 6;
			goDown = false;
			active = false;
			visible = false;
			detinated = false;

			//
			// TODO: Add constructor logic here
			//
		}
		public EnemyMine(Point startPoint, bool turnedOn) : base (startPoint.X, startPoint.Y, 25, 25)
		{
			sprite = new Sprite(startPoint.X, startPoint.Y, 25, 25, Image.FromFile("images\\mine.png"));
			speedX = 0;
			speedY = 6;
			goDown = false;
			active = turnedOn;
			visible = turnedOn;
			detinated = false;
			

			//
			// TODO: Add constructor logic here
			//
		}

		public override void Animate ()
		{
			// move ship

			if (active)
			{
				if (goDown)
					setTop(getTop() + speedY);
			}

		}

		public virtual void DrawSprite (Graphics g)
		{
			if (this.isVisible())
				sprite.Draw(g, this.getLeft(), this.getTop(), sprite.getRow() , sprite.getColumn(), this.width, this.height);
		}

		public virtual void Draw (Graphics g)
		{
			this.DrawSprite(g);
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
		public void directionChange(string direction)
		{
			direction.ToLower();
			switch (direction)
			{
				case "north":
				case "up":
					goDown = false;
					goUp = true;
					break;
				case "south":
				case "down":
					goDown = true;
					goUp = false;
					break;
				case "west":
				case "left":
					goLeft = true;
					goRight = false;
					break;
				case "east":
				case "right":
					goRight = true;
					goLeft = false;
					break;
				default:
					break;
			}
		}

		public void directionStop(string direction)
		{
			direction.ToLower();
			switch (direction)
			{
				case "north":
				case "up":
					goUp = false;
					break;
				case "south":
				case "down":
					goDown = false;
					break;
				case "west":
				case "left":
					goLeft = false;
					break;
				case "east":
				case "right":
					goRight = false;
					break;
				default:
					break;
			}
		}

	}
}
