using System;
using System.Drawing;

namespace GameTools
{
	/// <summary>
	/// Summary description for EnemyShip.
	/// </summary>
	public class EnemyShip : Ship
	{
		protected int points;
		protected int hitPoints;
		protected int speed;
		protected int hitDelay;
		//int frame;

		protected bool hit;
		protected bool goLeft;
		protected bool goRight;
		protected bool goUp;
		protected bool goDown;

		protected Point startPosition;
		protected int maxLeft;
		protected int maxRight;



		/*public EnemyShip() : base ()
		{
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Red);
			goLeft = false;
			goRight = false;
			goUp = false;
			goDown = false;
			frame = 0;
		}

		public EnemyShip (int x, int y, int wide, int high) : base (x, y, wide, high)
		{
			cooldownCounter = 6;
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Red);
			goLeft = false;
			goRight = false;
			goUp = false;
			goDown = false;
			frame = 0;

		}*/

		public EnemyShip (int x, int y, int wide, int high, Image i) : base (x, y, wide, high, i)
		{

			cooldownCounter = 6;
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Red);	
			goLeft = false;
			goRight = false;
			goUp = false;
			goDown = false;
			visible = true;
			hitPoints = 1;
			speed = 1;
			hitDelay= 0;
			hit = false;
			startPosition = new Point(x, y);
			maxLeft = 0;
			maxRight = 600;
		}

		public EnemyShip (int x, int y, int wide, int high, Image i, bool turnedOn) : base (x, y, wide, high, i)
		{
			active = turnedOn;
			cooldownCounter = 6;
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Red);
			goLeft = false;
			goRight = false;
			goUp = false;
			goDown = false;
			visible = true;
			hitPoints = 1;
			speed = 1;
			hitDelay= 0;
			hit = false;
			startPosition = new Point(x, y);
			maxLeft = 0;
			maxRight = 600;
		}

		public EnemyShip (int x, int y, int wide, int high, Image i, bool turnedOn, int pointValue) : base (x, y, wide, high, i)
		{

			active = turnedOn;
			points = pointValue;
			cooldownCounter = 6;
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Red);
			goLeft = false;
			goRight = false;
			goUp = false;
			goDown = false;
			visible = true;
			hitPoints = 1;
			speed = 1;
			hitDelay= 0;
			hit = false;
			startPosition = new Point(x, y);
			maxLeft = 0;
			maxRight = 600;
		}

		public EnemyShip (int x, int y, int wide, int high, Image i, bool turnedOn, int pointValue, int laserDelay) : base (x, y, wide, high, i)
		{
			active = turnedOn;
			points = pointValue;
			cooldownCounter = laserDelay;
			makeLasers(getMiddleX(), getMiddleY(), 3, 9, 6, Color.Red);
			goLeft = false;
			goRight = false;
			goUp = false;
			goDown = false;
			visible = true;
			hitPoints = 1;
			speed = 1;
			hitDelay= 0;
			hit = false;
			startPosition = new Point(x, y);
			maxLeft = 0;
			maxRight = 600;
		}

		public int getPointValue ()
		{
			return points;
		}

		public int getHitPoints ()
		{
			return hitPoints;
		}

		public void setHitPoints (int Hpoints)
		{
			hitPoints = Hpoints;
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

		public override void Animate ()
		{
			// move ship
			if (goUp)
				setTop(getTop() - speed);
			if (goDown)
				setTop(getTop() + speed);
			if (goLeft)
				setLeft(getLeft() - speed);
			if (goRight)
				setLeft(getLeft() + speed);
		}

		public void setSpeed(int s)
		{
			speed = s;
		}

		public int getSpeed ()
		{
			return speed;
		}
		/*public void Animate (int shipType)
		{
			// move ship
			if (goUp)
				setTop(getTop() - 1);
			if (goDown)
				setTop(getTop() + 1);
			if (goLeft)
				setLeft(getLeft() - 1);
			if (goRight)
				setLeft(getLeft() + 1);

			//change image
			switch (shipType)
			{
				case 1:
					setImage(Image.FromFile("images\\saucer" + frame + ".png"));
					break;
				case 2:
					break;
				default:
					break;
			}

			if (frame < 5)
				frame++;
			else 
				frame = 0;
		}*/

		public void setHitDelay (int delay)
		{
			hitDelay = delay;
		}

		public int getHitDelay ()
		{
			return hitDelay;
		}

		public void setHitStatus (bool trueFalse)
		{
			hit = trueFalse;
		}

		public bool isHit ()
		{
			return hit;
		}

		public override void DrawSprite (Graphics g)
		{
			if (this.isVisible())
			{
				if (explodingSprite.getRow() < 8)
					sprite.Draw(g, this.getLeft(), this.getTop(), sprite.getRow(), sprite.getColumn(), this.width, this.height);
				if (exploding)
				{	
					explodingSprite.Draw(g, this.getMiddleX() - explodingSprite.getWidth()/2, this.getMiddleY() - explodingSprite.getHeight()/2, explodingSprite.getRow(), sprite.getColumn()); //explodingSprite.getWidth(), explodingSprite.getHeight());
				
					if (explodingSprite.getRow() >= explodingSprite.getMaxRow())
						this.setVisible(false);
				}	
			}

		}
	}
}
