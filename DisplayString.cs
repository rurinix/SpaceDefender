using System;
using System.Drawing;

namespace GameTools
{
	/// <summary>
	/// Summary description for DisplayString.
	/// </summary>
	public class DisplayString : GameObject
	{

		Font displayFont;
		string displayValue;
		int transparency;


		public DisplayString() : base ()
		{

			displayFont = new Font("Arial", 12);
			displayValue = " ";
			transparency = 255;
			speedX = 1;
			speedY = -1;
			//
			// TODO: Add constructor logic here
			//
		}

		public DisplayString (int x, int y, Font font) : this ()
		{
			this.setLeft(x);
			this.setTop(y);
			displayFont = font;
			displayValue = " ";
			transparency = 255;
			speedX = 1;
			speedY = -1;
		}

		public DisplayString (int x, int y, Font font, string printString) : this (x, y, font)
		{
			displayValue = printString;
			transparency = 255;
			speedX = 1;
			speedY = -1;
		}

		public DisplayString (int x, int y, Font font, string printString, int opacity) : this (x, y, font, printString)
		{
			transparency = opacity;
			speedX = 1;
			speedY = -1;
		}

		public override void Animate ()
		{
			if (transparency - 10 >= 0)
			{
				transparency -= 10;
			}
			else
			{
				transparency = 0;
				this.setVisible(false);
			}
			this.setLeft(this.getLeft() + speedX);
			this.setTop(this.getTop() + speedY);
		}

		public void Draw (Graphics g)
		{
			if (this.isVisible())
				g.DrawString(displayValue, displayFont, new SolidBrush(Color.FromArgb(transparency, 80, 10, 10)), this.getLeft()+1, this.getTop()+1);
				g.DrawString(displayValue, displayFont, new SolidBrush(Color.FromArgb(transparency, 255, 255, 0)), this.getLeft(), this.getTop());

		}

		public void resetValues (string printString)
		{
			transparency = 255;
			displayValue = printString;
			this.setVisible(true);
			this.setActive(true);
		}

		public void resetValues (int x, int y, string printString) 
		{
			this.resetValues (printString);
			this.setLeft(x);
			this.setTop(y);
		}

		public void setDisplayString (string displayString)
		{
				displayValue = displayString;
		}
																		  
	}
}
