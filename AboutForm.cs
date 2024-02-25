using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceDefender
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer aboutTimer;
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.Timer delayTimer;

		Random random;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			random = new Random();


			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public AboutForm(Point p)
		{
			InitializeComponent ();
			p.X -= this.ClientSize.Width/2;
			p.Y -= this.ClientSize.Height/2;
			this.Location = p;
			
			random = new Random();

			//Enable double buffering
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);			
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.aboutTimer = new System.Windows.Forms.Timer(this.components);
			this.delayTimer = new System.Windows.Forms.Timer(this.components);
			// 
			// aboutTimer
			// 
			this.aboutTimer.Interval = 30;
			this.aboutTimer.Tick += new System.EventHandler(this.aboutTimer_Tick);
			// 
			// delayTimer
			// 
			this.delayTimer.Enabled = true;
			this.delayTimer.Interval = 2000;
			this.delayTimer.Tick += new System.EventHandler(this.delayTimer_Tick);
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(194, 218);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "About Space Defender";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.AboutForm_Load);

		}
		#endregion

		protected override void OnPaint (PaintEventArgs e)
		{
			Graphics g = e.Graphics;

				drawInfo(g);
		}

		private void aboutTimer_Tick(object sender, System.EventArgs e)
		{

			Invalidate();
		}

		private void delayTimer_Tick(object sender, System.EventArgs e)
		{
			if (!aboutTimer.Enabled)
			{
				aboutTimer.Enabled = true;
				delayTimer.Enabled = false;
			}
		}

		private void drawInfo (Graphics g)
		{
			Font titleFont = new Font ("Arial Rounded MT Bold", 14, FontStyle.Bold);

			Font aboutFont = new Font ("Arial", 10, FontStyle.Regular);
			Font aboutBold = new Font ("Arial", 10, FontStyle.Bold);

			g.DrawImage(Image.FromFile("images\\HeroShip.png"), new Rectangle(10, 10, 50, 50), 50, 0, 50, 50, GraphicsUnit.Pixel);
			g.DrawString("Space\n     Defender", titleFont, Brushes.White , 62, 0);
			g.DrawString("By Michael Reeves", aboutFont, Brushes.WhiteSmoke, 68, 40);
			g.DrawString("Version 0.8.2", aboutFont, Brushes.Gray, this.ClientSize.Width - 93, 56);
			g.DrawLine(Pens.Silver, 5,74, this.ClientSize.Width - 5, 74);
			g.DrawString("A tribute to the video games I \ngrew up playing.\n                                   Enjoy", aboutFont, Brushes.WhiteSmoke, 5, 78);
			//g.DrawLine(Pens.Silver, 5, 117, this.ClientSize.Width - 5, 117);
			//g.DrawLine(Pens.Silver, 25, 122, this.ClientSize.Width - 25, 122);
			g.DrawLine(Pens.Silver, 5, 129, this.ClientSize.Width - 5, 129);
			g.DrawString("This program is open-source \nand freely distributed at: ", aboutFont, Brushes.WhiteSmoke, 5,134);
			g.DrawString("http://reeves.whitacres.net/", aboutBold, Brushes.DarkGray, 5, 165);
			g.DrawString("http://reeves.whitacres.net/", aboutBold, Brushes.Blue, 6, 164);
			g.DrawString("Copyright 2009", aboutFont, Brushes.Gray, this.ClientSize.Width-93, this.ClientSize.Height-20);
		}

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
