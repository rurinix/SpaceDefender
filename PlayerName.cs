using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SpaceDefender
{
	/// <summary>
	/// Summary description for PlayerName.
	/// </summary>
	public class PlayerName : System.Windows.Forms.Form
	{

		int newScore;
		string newName;
		string[] names;
		int[] highScore;

		private System.Windows.Forms.TextBox NameBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PlayerName()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			newScore = 0;
			newName = "";
			highScore = new int [10];
			names = new string [10];
			loadHighScores();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public PlayerName (int newHighScore, Point p)
		{
			InitializeComponent();
			newScore = newHighScore;
			newName = "";
			highScore = new int [10];
			names = new string [10];
			p.X -= this.ClientSize.Width/2;
			p.Y -= this.ClientSize.Height/2;
			this.Location = p;
			loadHighScores();
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
			this.NameBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// NameBox
			// 
			this.NameBox.Location = new System.Drawing.Point(8, 8);
			this.NameBox.MaxLength = 25;
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(160, 20);
			this.NameBox.TabIndex = 0;
			this.NameBox.Text = "Enter Your Name";
			this.NameBox.WordWrap = false;
			this.NameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameBox_KeyDown);
			this.NameBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NameBox_MouseDown);
			// 
			// PlayerName
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.ClientSize = new System.Drawing.Size(176, 40);
			this.Controls.Add(this.NameBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "PlayerName";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.Load += new System.EventHandler(this.PlayerName_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void PlayerName_Load(object sender, System.EventArgs e)
		{
		
		}

		private void NameBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			NameBox.Text = "";
		}

		private void NameBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyValue == 13 && NameBox.Text != "")
			{				
				for (int count = 0; count < NameBox.Text.Length ; count++)
				{
					if (NameBox.Text.Substring(count, 1) != ",")
						newName += NameBox.Text.Substring(count, 1);
					else
						count = NameBox.Text.Length;
				}

				saveScores();

				this.Close();
			}

		}

		private void loadHighScores()
		{
			System.IO.StreamReader listLoad = null;
			//work with file

			listLoad = new System.IO.StreamReader("highScores.dat");


			for (int count = 0; listLoad.Peek() >= 0 && count < 10; count++)
			{
				String tempString = listLoad.ReadLine();
				names[count] = tempString.Substring(0, tempString.LastIndexOf(","));
				highScore[count] = Convert.ToInt32(tempString.Substring(tempString.LastIndexOf(",")+1));
			}

			listLoad.Close();

		}

		private void saveScores()
		{
			System.IO.StreamWriter saveFile = new System.IO.StreamWriter("highScores.dat", false);


			if (newScore > highScore[highScore.Length-1])
			{
				for (int loopCount = highScore.Length -1; loopCount >= 0; loopCount--)
				{
					if (highScore[loopCount] < newScore)
					{
						if (loopCount < highScore.Length -1)
						{
							names[loopCount + 1] = names[loopCount];
							highScore[loopCount + 1] = highScore[loopCount];
						}
						highScore[loopCount] = newScore;
						names[loopCount] = newName;
					}
					else
					{
						loopCount = -1;
					}
				}
			}

			for (int loopCount = 0; loopCount < highScore.Length; loopCount++)
			{	
				saveFile.WriteLine(names[loopCount] + "," + highScore[loopCount]);
			}

			saveFile.Close();
		}

	}
}
