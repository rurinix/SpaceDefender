using System;
using System.Drawing;


namespace GameTools
{
	/// <summary>
	/// Summary description for GameObjects.
	/// </summary>
	public abstract class GameObject
	{
		protected bool active;
		protected bool visible;

		protected int top;
		protected int bottom;
		protected int middleY;
		protected int middleX;
		protected int left;
		protected int right;
		protected int width;
		protected int height;
		protected int lastX;
		protected int lastY;
		protected int speedX;
		protected int speedY;

		/// <summary>
		/// Game object, by default is invisible, inactive, with no width, or height, located at coords 0,0
		/// </summary>
		public GameObject()
		{
			active = false;
			visible = false;

			top = 0;
			bottom = 0;
			middleY = 0;
			middleX = 0;
			left = 0;
			right = 0;

			width = 0;
			height = 0;

			speedX = 0;
			speedY = 0;
		}

		/// <summary>
		/// Game object which is invisible and inactive.  Uses Width and Height for the objects size.  Placed at coords X,Y.
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="wide">Width</param>
		/// <param name="high">Height</param>
		public GameObject (int x, int y, int wide, int high) : this ()
		{
			top = y;
			left = x;
			setBottom();
			setRight();
			setMiddleY();
			setMiddleX();

			width = wide;
			height = high;
		}
		
		/// <summary>
		/// Game object.  Visiibility and activity passed as arguments.  Size determined by Width and Height.  Located at coords X,Y.
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="wide">Width</param>
		/// <param name="high">Height</param>
		/// <param name="seen">Visible</param>
		/// <param name="working">Active</param>
		public GameObject (int x, int y, int wide, int high, bool seen, bool working) : this (x, y, wide, high)
		{
			visible = seen;
			active = working;
		}
		public int getWidth()
		{
			return width;
		}

		public int getHeight()
		{
			return height;
		}

		public int getTop()
		{
			return top;
		}

		public int getBottom()
		{
			setBottom();
			return bottom;
		}
		
		public int getLeft ()
		{
			return left;
		}

		public int getRight ()
		{
			right = left + width;
			return right;
		}

		public int getMiddleY ()
		{
			setMiddleY();
			return middleY;
		}

		public int getMiddleX ()
		{
			setMiddleX();
			return middleX;
		}

		public virtual void setTop (int i)
		{
			lastY = top;
			top = i;
			speedY = top - lastY;
			setBottom();
			setMiddleY();
		}

		public virtual void setLeft (int i)
		{
			lastX = left;
			left = i;
			speedX = left - lastX;
			setRight();
			setMiddleX();
		}

		public virtual void setBottom ()
		{
			bottom = top + height;
		}

		public virtual void setRight ()
		{
			right = left + width;
		}

		public virtual void setRight (int i)
		{
			setLeft (i - width);
		}

		public virtual void setMiddleY ()
		{
			middleY = top + (height / 2);
		}

		public virtual void setMiddleX ()
		{
			middleX = left + (width / 2);
		}

		public virtual void setWidth(int i)
		{
			width = i;
		}

		public virtual void setHeight (int i)
		{
			height = i;
		}

		public virtual void setActive (bool trueFalse)
		{
			active = trueFalse;
		}

		public bool isActive ()
		{
			return active;
		}

		public virtual void setVisible (bool trueFalse)
		{
			visible = trueFalse;
		}

		public bool isVisible ()
		{
			return visible;
		}

		public int getLastTop ()
		{
			return lastX;
		}

		public int getLastLeft ()
		{
			return lastX;
		}

		public int getSpeedX ()
		{
			return speedX;
		}

		public int getSpeedY ()
		{
			return speedY;
		}

		public virtual void Animate()
		{
			// change object as needed;
		}
	}
}
