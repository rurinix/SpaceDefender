using System;
using System.Drawing;

namespace GameTools
{
	/// <summary>
	/// Creates sprites from image files.  Is designed to progress down 
	/// a single column, using each row as the next frame (or cell) of 
	/// animation.  
	/// </summary>
	public class Sprite
	{
		int x;
		int y;
		int cellWidth;
		int cellHeight;
		int displayWidth;
		int displayHeight;
		int row;
		int column;
		Image image;

		public Sprite()
		{
			x = 0;
			y = 0;
			cellWidth = 0;
			cellHeight = 0;
			displayWidth = cellWidth;
			displayHeight = cellHeight;
			row = 0;
			column = 0;
			image = null;
			//
			// TODO: Add constructor logic here
			//
		}

		
		public Sprite (int locX, int locY, int width, int height) : this ()
		{
			x = locX;
			y = locY;
			cellWidth = width;
			cellHeight = height;
			displayWidth = cellWidth;
			displayHeight = cellHeight;
		}
		

		public Sprite (int locX, int locY, int width, int height, Image i) : this (locX, locY, width, height)
		{
			image = i;
			}
		/// <summary>
		/// Draw function which displays the current frame of animation at the current (x,y)
		/// coordinates.  Frame is derrived from the current column and incrimenting rows.
		/// </summary>
		/// <param name="g">Graphics object</param>
		public void Draw (Graphics g)
		{
			if (row + 1  < image.Height / cellHeight)
				row++;
			else
				row = 0;
		
			g.DrawImage(image, new Rectangle(x, y, displayWidth, displayHeight), cellWidth * column , row * cellHeight, cellWidth, cellHeight, GraphicsUnit.Pixel);
		}

		/// <summary>
		/// Draw function which displays current frame of animation at the (x,y) coordinates
		/// passed into function.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="locX">X</param>
		/// <param name="locY">Y</param>
		public void Draw (Graphics g, int locX, int locY)
		{
			x = locX;
			y = locY;
			
			Draw (g);
		}

		/// <summary>
		/// Draw function which displays current frame of animation a the (x,y) point
		/// passed into function.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="p"></param>
		public void Draw (Graphics g, Point p)
		{
			Draw (g, p.X, p.Y);
		}

		/// <summary>
		/// Draw function which displays specified frame of animation passed into the function
		/// Frame will be drawn at current (x,y) coordinates.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="rowNumber"></param>
		public void Draw (Graphics g, int rowNumber)
		{
			setRow(rowNumber);

			Draw (g);
		}
		
		/// <summary>
		/// Draw function which displays specified frame of animation at specified
		/// (x,y) coordinates.  All values are passed into the function.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="locX">X</param>
		/// <param name="locY">Y</param>
		/// <param name="rowNumber"></param>
		public void Draw (Graphics g, int locX, int locY, int rowNumber)
		{
			x = locX;
			y = locY;
			setRow(rowNumber);

			Draw (g);
		}

		/// <summary>
		/// Draw function which displays specified frame of animation at specified
		/// (x,y) coordinate points.  All values are passed into the function.		
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="p">Point for (x,y) coordinate system</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		public void Draw (Graphics g, Point p, int rowNumber)
		{
			Draw (g, p.X, p.Y, rowNumber);
		}

		/// <summary>
		/// Draw function which displays specified frame of animation, both row and 
		/// column, at specified (x,y) coordinates.  All values are passed into the function.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="locX">X coordinate location</param>
		/// <param name="locY">Y coordinate location</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		public void Draw (Graphics g, int locX, int locY, int rowNumber, int columnNumber)
		{
			setColumn(columnNumber);
			Draw (g, locX, locY, rowNumber);
		}

		/// <summary>
		/// Draw function which displays specified frame of animation, both row and 
		/// column, at specified (x,y) coordinate point.  All values are passed into the function.		
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="p">Point for (x,y) coordinate system</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		public void Draw (Graphics g, Point p, int rowNumber, int columnNumber)
		{
			Draw (g, p.X, p.Y, rowNumber, columnNumber);
		}

		/// <summary>
		/// Draw function which displays scaled frame of animation, both row and 
		/// column, at specified (x,y) coordinates.  All values are passed into the function.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="locX">X coordinate location</param>
		/// <param name="locY">Y coordinate location</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		/// <param name="scaledWidth">Width in pixels for display (output) size</param>
		/// <param name="scaledHeight">Height in pixels for display (output) size</param>
		public void Draw (Graphics g, int locX, int locY, int rowNumber, int columnNumber, int scaledWidth, int scaledHeight)
		{
			setWidth(scaledWidth);
			setHeight(scaledHeight);
			Draw (g, locX, locY, rowNumber, columnNumber);
		}

		/// <summary>
		/// Draw function which displays scaled frame of animation, both row and 
		/// column, at specified (x,y) coordinate point.  All values are passed into the function.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="p">Point for (x,y) coordinate system</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		/// <param name="scaledWidth">Width in pixels for display (output) size</param>
		/// <param name="scaledHeight">Height in pixels for display (output) size</param>
		public void Draw (Graphics g, Point p, int rowNumber, int columnNumber, int scaledWidth, int scaledHeight)
		{
			Draw (g, p.X, p.Y, rowNumber, columnNumber, scaledWidth, scaledHeight);
		}

		/// <summary>
		/// Draws specified frame of sprite map at (x,y) coordinates.  No animation occures.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="locX">X coordinate location</param>
		/// <param name="locY">Y coordinate location</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		public void DrawFrame (Graphics g, int locX, int locY, int rowNumber, int columnNumber)
		{
			x = locX;
			y = locY;

			setColumn(columnNumber);
			setRow(rowNumber);
	
			g.DrawImage(image, new Rectangle(x, y, displayWidth, displayHeight), cellWidth * column , row * cellHeight, cellWidth, cellHeight, GraphicsUnit.Pixel);
		}

		/// <summary>
		/// Draws specified frame of sprite at (x,y) point.  No animation occures.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="p">Point for (x,y) coordinate system</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		public void DrawFrame (Graphics g, Point p, int rowNumber, int columnNumber)
		{
			DrawFrame (g, p.X, p.Y, rowNumber, columnNumber);
		}
        
		/// <summary>
		/// Draws scaled frame of sprite at (x,y) coordiantes.  Scaling allows for image to be 
		/// streatched or shrunk.
		/// No animation occures.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="locX">X coordinate location</param>
		/// <param name="locY">Y coordinate location</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		/// <param name="scaledWidth">Width in pixels for display (output) size</param>
		/// <param name="scaledHeight">Height in pixels for display (output) size</param>
		public void DrawFrame (Graphics g, int locX, int locY, int rowNumber, int columnNumber, int scaledWidth, int scaledHeight)
		{
			setWidth(scaledWidth);
			setHeight(scaledHeight);
			DrawFrame (g, locX, locY, rowNumber, columnNumber);
		}

		/// <summary>
		///  Draws scaled frame of sprite at (x,y) coordiantes.  Scaling allows for image to be 
		/// streatched or shrunk.
		/// No animation occures.
		/// </summary>
		/// <param name="g">Graphics object</param>
		/// <param name="p">Point for (x,y) coordinate system</param>
		/// <param name="rowNumber">Row number on sprite map image (starting with 1)</param>
		/// <param name="columnNumber">Column number on sprite map (starting with 1)</param>
		/// <param name="scaledWidth">Width in pixels for display (output) size</param>
		/// <param name="scaledHeight">Height in pixels for display (output) size</param>
		public void DrawFrame (Graphics g, Point p, int rowNumber, int columnNumber, int scaledWidth, int scaledHeight)
		{
			DrawFrame(g, p.X, p.Y, rowNumber, columnNumber, scaledWidth, scaledHeight);
		}

		/// <summary>
		/// Sets which column on sprite map to use for animation.  (starting with 1)
		/// </summary>
		/// <param name="columnNumber"></param>
		public void setColumn (int columnNumber)
		{
			if (columnNumber  <= (image.Width / cellWidth))
				column = columnNumber - 1;
			else
				column = 0;
		}

		/// <summary>
		/// Sets which row on sprite map to use for animation.  (starting with 1)
		/// </summary>
		/// <param name="rowNumber"></param>
		public void setRow (int rowNumber)
		{
			if (rowNumber <= (image.Height / cellHeight))
				row = rowNumber -1;
			else
				row = 0;
		}

		public void setX (int locX)
		{
			x = locX;
		}

		public void setY (int locY)
		{
			y = locY;
		}

		/// <summary>
		/// Sets the width of the displayed graphics image. (in pixels)
		/// </summary>
		/// <param name="Width"></param>
		public void setWidth (int Width)
		{
			displayWidth = Width;
		}

		/// <summary>
		/// Sets the height of the displayed graphics image. (in pixels)
		/// </summary>
		/// <param name="Height"></param>
		public void setHeight (int Height)
		{
			displayHeight = Height;
		}

		public void setImage (Image i)
		{
			image = i;
		}

		/// <summary>
		/// Set image used for sprite animation.  Reads in new width and height used in
		/// sprite map grid.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="width">Width of cells on sprite map</param>
		/// <param name="height">Height of cells on sprite map</param>
		public void setImage (Image i, int width, int height)
		{
			image = i;
			cellWidth = width;
			cellHeight = height;
		}

		public int getX ()
		{
			return x;
		}

		public int getY ()
		{
			return y;
		}

		/// <summary>
		/// Returns the width image will be displayed as.  (For actual image size use getCellWidth)
		/// </summary>
		/// <returns></returns>
		public int getWidth ()
		{
			return displayWidth;
		}

		/// <summary>
		/// Returns the height image will be displayed as.  (For actual image size use getCellHeight)
		/// </summary>
		/// <returns></returns>
		public int getHeight ()
		{
			return displayHeight;
		}

		/// <summary>
		/// Returns the width of each cell of animation. (not displayed size)
		/// </summary>
		/// <returns></returns>
		public int getCellWidth()
		{
			return cellWidth;
		}

		/// <summary>
		/// Returns the height of each cell of animation. (not displayed size)
		/// </summary>
		/// <returns></returns>
		public int getCellHeight()
		{
			return cellHeight;
		}

		public Image getImage ()
		{
			return image;
		}

		public int getRow ()
		{
			return row + 1;
		}

		public int getColumn ()
		{
			return column + 1;
		}

		public int getMaxRow ()
		{
			return (image.Height / cellHeight);
		}
	}
}
