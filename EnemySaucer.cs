using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameTools
{
	/// <summary>
	/// Summary description for EnemySaucer.
	/// </summary>
	/// 


	public class EnemySaucer : EnemyShip
	{

		public EnemySaucer(Point startPoint, int leftBound, int rightBound) : base (startPoint.X, startPoint.Y, 50, 25, Image.FromFile("images\\saucer.png"))
		{

			maxLeft = leftBound;
			maxRight = rightBound;


			//
			// TODO: Add constructor logic here
			//
		}
		public EnemySaucer(Point startPoint, int leftBound, int rightBound, bool turnedOn) : base (startPoint.X, startPoint.Y, 50, 25, Image.FromFile("images\\saucer.png"), turnedOn, 50)
		{

			maxLeft = leftBound;
			maxRight = rightBound;


			

			//
			// TODO: Add constructor logic here
			//
		}

		public EnemySaucer(Point startPoint, int leftBound, int rightBound, bool turnedOn, int laserDelay) : base (startPoint.X, startPoint.Y, 50, 25, Image.FromFile("images\\saucer.png"), turnedOn, 50)
		{

			maxLeft = leftBound;
			maxRight = rightBound;
			cooldownCounter = laserDelay;


			//
			// TODO: Add constructor logic here
			//
		}

		public override void Animate ()
		{
			// move ship
			if (goUp)
				setTop(getTop() - speed);
			if (goDown)
				setTop(getTop() + speed);
			if (goLeft)
			{
				if (getLeft() - speed > maxLeft)
				{
					setLeft(getLeft() - speed);
				}
				else
				{
					goLeft = false;
					goRight = true;
				}
			}

			if (goRight)
			{
				if (getRight() + speed < maxRight)
				{
					setLeft(getLeft() + speed);
				}
				else
				{
					goRight = false;
					goLeft = true;
				}
			}

		}
	}
}
