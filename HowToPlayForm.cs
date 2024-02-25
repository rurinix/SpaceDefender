using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GameTools;

namespace SpaceDefender
{
	/// <summary>
	/// Summary description for HowToPlayForm.
	/// </summary>
	public class HowToPlayForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer HowToPlayTimer;

		PowerUp lW;
		PowerUp lL;
		PowerUp lF;
		PowerUp lP;
		PowerUp lS;

		public HowToPlayForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public HowToPlayForm(Point p) 
		{
			InitializeComponent ();
			p.X -= this.ClientSize.Width/2;
			p.Y -= this.ClientSize.Height/2;
			this.Location = p;

			//Enable double buffering
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);			

		
			lW = new PowerUp(20, 10, 30, 15, Image.FromFile("images\\powerup.png"), 0, 5, true, true);
			lF = new PowerUp(20, 35, 30, 15, Image.FromFile("images\\powerup.png"), 1, 5, true, true);
			lL = new PowerUp(20, 60, 30, 15, Image.FromFile("images\\powerup.png"), 2, 5, true, true);		
			lP = new PowerUp(20, 85, 30, 15, Image.FromFile("images\\powerup.png"), 3, 5, true, true);	
			lS = new PowerUp(20, 110, 30, 15, Image.FromFile("images\\powerup.png"), 4, 5, true, true);	

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HowToPlayForm));
			this.HowToPlayTimer = new System.Windows.Forms.Timer(this.components);
			// 
			// HowToPlayTimer
			// 
			this.HowToPlayTimer.Enabled = true;
			this.HowToPlayTimer.Interval = 30;
			this.HowToPlayTimer.Tick += new System.EventHandler(this.HowToPlayTimer_Tick);
			// 
			// HowToPlayForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(290, 264);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HowToPlayForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "How To Play";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.HowToPlayForm_Load);

		}
		#endregion

		private void HowToPlayForm_Load(object sender, System.EventArgs e)
		{
			
		}
		protected override void OnPaint (PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			lW.Draw(g);
			lF.Draw(g);
			lL.Draw(g);
			lP.Draw(g);
			lS.Draw(g);

			g.DrawString("= Wingman", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White), 54, lW.getTop()-1);
			g.DrawString("= Faster Lasers", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White), 54, lF.getTop()-1);
			g.DrawString("= Longer Lasers", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White), 54, lL.getTop()-1);
			g.DrawString("= More Powerful Lasers", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White), 54, lP.getTop()-1);
			g.DrawString("= Sheild Recharge", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White), 54, lS.getTop()-1);
		
		}

		private void HowToPlayTimer_Tick(object sender, System.EventArgs e)
		{
			Invalidate();
		}
	}
}
