using System;

namespace GameTools
{
	/// <summary>
	/// Summary description for CollisionDetection.
	/// </summary>
	public class CollisionDetection
	{
		public CollisionDetection()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static bool LaserHitSquare(Laser laser, Ship ship)
		{
			bool returnVal = false;
			if (laser.isActive() && ship.isActive())
			{
				if (laser.getLeft() <= ship.getRight() && laser.getRight() >= ship.getLeft())
				{
					if (laser.getBottom() >= ship.getTop() && laser.getTop() <= ship.getBottom())
					{
						returnVal = true;
					}
				}
			}

			return returnVal;
		}

		public static bool LaserHitPyramid(Laser laser, Ship ship)
		{
			double slope = (ship.getRight()-ship.getMiddleX())/(ship.getBottom()-ship.getMiddleY());
			double distanceX;
			double distanceY = laser.getBottom() - ship.getTop();
			bool returnVal = false;
			if (laser.isActive() && ship.isActive())
			{
				if (laser.getLeft() <= ship.getRight() && laser.getRight() >= ship.getLeft())
				{
					if (laser.getBottom() >= ship.getTop() && laser.getTop() <= ship.getBottom())
					{
						if (laser.getLeft() > ship.getMiddleX())
							distanceX = laser.getLeft() - ship.getMiddleX();
					
						else if (laser.getRight() < ship.getMiddleX())
							distanceX = ship.getMiddleX() - laser.getRight();

						else
							distanceX = 0;

						if (distanceY > distanceX * (2 * slope))
							returnVal = true;
					}
				}
			}

			return returnVal;
		}

		public static bool LaserHitPentagon(Laser laser, Ship ship)
		{
			int distanceX;
			int distanceY = laser.getBottom() - ship.getTop();
			bool returnVal = false;
			if (laser.isActive() && ship.isActive())
			{
				if (laser.getLeft() <= ship.getRight() && laser.getRight() >= ship.getLeft())
				{
					if (laser.getBottom() >= ship.getTop() && laser.getTop() <= ship.getBottom())
					{
						if (laser.getLeft() > ship.getMiddleX())
							distanceX = laser.getLeft() - ship.getMiddleX();
					
						else if (laser.getRight() < ship.getMiddleX())
							distanceX = ship.getMiddleX() - laser.getRight();

						else
							distanceX = 0;

						if (distanceY > distanceX)
							returnVal = true;
					}
				}
			}

			return returnVal;
		}

		public static bool SquareHitSquare(GameObject object1, GameObject object2)
		{
			bool returnVal = false;
			if (object1.isActive() && object2.isActive())
			{
				if (object1.getLeft() <= object2.getRight() && object1.getRight() >= object2.getLeft())
				{
					if (object1.getBottom() >= object2.getTop() && object1.getTop() <= object2.getBottom())
					{
						returnVal = true;
					}
				}
			}

			return returnVal;
		}

		public static bool SquareHitTriangle(GameObject object1, GameObject object2)
		{
			double slope = (object2.getRight()-object2.getMiddleX())/(object2.getBottom()-object2.getMiddleY());
			int distanceX;
			int distanceY = object1.getBottom() - object2.getTop();
			bool returnVal = false;
			if (object1.isActive() && object2.isActive())
			{
				if (object1.getLeft() <= object2.getRight() && object1.getRight() >= object2.getLeft())
				{
					if (object1.getBottom() >= object2.getTop() && object1.getTop() <= object2.getBottom())
					{
						if (object1.getLeft() > object2.getMiddleX())
							distanceX = object1.getLeft() - object2.getMiddleX();
					
						else if (object1.getRight() < object2.getMiddleX())
							distanceX = object2.getMiddleX() - object1.getRight();

						else
							distanceX = 0;

						if (distanceY > distanceX * 2)
							returnVal = true;
					}
				}
			}

			return returnVal;
		}

		public static bool SquareHitCircle (GameObject object1, GameObject object2)
		{
			int distanceX = - 1;
			int distanceY = - 1;
			int slope;
			bool returnVal = false;
				
			if (object1.getBottom() <= object2.getMiddleY() && object1.getRight() <= object2.getMiddleX())
			{
				distanceY = object2.getMiddleY() - object1.getBottom();
				distanceX = object2.getMiddleX() - object1.getRight();
			}
			else if (object1.getBottom() <= object2.getMiddleY() && object1.getLeft() >= object2.getMiddleX())
			{
				distanceX = object1.getLeft() - object2.getMiddleX();
				distanceY = object2.getMiddleY() - object1.getBottom();
			}
			else if (object1.getTop() >= object2.getMiddleY() && object1.getRight() <= object2.getMiddleX())
			{
				distanceX = object2.getMiddleX() - object1.getRight();
				distanceY = object1.getTop() - object2.getMiddleY();
			}

			else if (object1.getTop() >= object2.getMiddleY() && object1.getLeft() >= object2.getMiddleX())
			{
				distanceX = object1.getLeft() - object2.getMiddleX();
				distanceY = object1.getTop() - object2.getMiddleY();
			}

			else 
			{
				if (object2.getLeft() < object1.getRight() && object2.getRight() > object1.getLeft() && object2.getBottom() > object1.getTop() && object2.getTop() < object1.getBottom())
				{
					returnVal = true;
				}
			}

			if (distanceX != -1 || distanceY != -1)
			{
				slope = Convert.ToInt16(Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY)));

				if (slope <= object2.getWidth()/2)
					returnVal = true;
			}
		
			return returnVal;
		}	

	}

}
