using System;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using GameTools;

namespace SpaceDefender
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		// Global variables can be usable by all Methods
		int targetX;
		int targetY;
		int mouseX;
		int mouseY;
		int lastMouseX;
		int lastMouseY;
		int[] targetingCounter;
		int[] wall;
		int countWall;
		int level;
		int lives;
		int score;
		int flashCount;
		int shieldLoss;
		int[] highScore;
		string [] names;
		int laserPower;
		int []highScoreTop;
		int highScoreDelay;

		String testString;

		bool gameOver;
		bool roundComplete;
		bool roundStarted;
		bool paused;

		// game's random number generator
		Random rand = new Random();

		// instantiate all objects needed for the game
		HeroShip ship = new HeroShip(0, 455, 50, 50, Image.FromFile("images\\HeroShip.png"), 200);
		Star[] star = new Star[35];
		EnemySaucer[] enemySaucer = new EnemySaucer[20];
		EnemyMine[] enemyMine = new EnemyMine[3];
		PowerUp[] powerUp = new PowerUp[3]; 
		
		private System.Windows.Forms.Timer FrameChange_Timer;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem mFile;
		private System.Windows.Forms.MenuItem mNewGame;
		private System.Windows.Forms.MenuItem mExit;
		private System.Windows.Forms.MenuItem mSeperator1;
		private System.Windows.Forms.Timer Target_Timer;
		private System.Windows.Forms.MenuItem mOptions;
		private System.Windows.Forms.MenuItem mPauseOffScreen;
		private System.Windows.Forms.Timer PlayerHit_Timer;
		private System.Windows.Forms.MenuItem mPlaySounds;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mHTP;
		private System.Windows.Forms.MenuItem mAbout;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			InitializeComponent();
			
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
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.FrameChange_Timer = new System.Windows.Forms.Timer(this.components);
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.mFile = new System.Windows.Forms.MenuItem();
			this.mNewGame = new System.Windows.Forms.MenuItem();
			this.mSeperator1 = new System.Windows.Forms.MenuItem();
			this.mExit = new System.Windows.Forms.MenuItem();
			this.mOptions = new System.Windows.Forms.MenuItem();
			this.mPauseOffScreen = new System.Windows.Forms.MenuItem();
			this.mPlaySounds = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mHTP = new System.Windows.Forms.MenuItem();
			this.mAbout = new System.Windows.Forms.MenuItem();
			this.Target_Timer = new System.Windows.Forms.Timer(this.components);
			this.PlayerHit_Timer = new System.Windows.Forms.Timer(this.components);
			// 
			// FrameChange_Timer
			// 
			this.FrameChange_Timer.Enabled = true;
			this.FrameChange_Timer.Interval = 30;
			this.FrameChange_Timer.Tick += new System.EventHandler(this.FrameChange_Timer_Tick);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mFile,
																					 this.mOptions,
																					 this.menuItem1});
			// 
			// mFile
			// 
			this.mFile.Index = 0;
			this.mFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				  this.mNewGame,
																				  this.mSeperator1,
																				  this.mExit});
			this.mFile.Text = "File";
			// 
			// mNewGame
			// 
			this.mNewGame.Index = 0;
			this.mNewGame.Text = "New Game";
			this.mNewGame.Click += new System.EventHandler(this.mNewGame_Click);
			// 
			// mSeperator1
			// 
			this.mSeperator1.Index = 1;
			this.mSeperator1.Text = "-";
			// 
			// mExit
			// 
			this.mExit.Index = 2;
			this.mExit.Text = "Exit";
			this.mExit.Click += new System.EventHandler(this.mExit_Click);
			// 
			// mOptions
			// 
			this.mOptions.Index = 1;
			this.mOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mPauseOffScreen,
																					 this.mPlaySounds});
			this.mOptions.Text = "Options";
			// 
			// mPauseOffScreen
			// 
			this.mPauseOffScreen.Checked = true;
			this.mPauseOffScreen.Index = 0;
			this.mPauseOffScreen.Text = "Pause when off-screen";
			this.mPauseOffScreen.Click += new System.EventHandler(this.mPauseOffScreen_Click);
			// 
			// mPlaySounds
			// 
			this.mPlaySounds.Index = 1;
			this.mPlaySounds.Text = "Play Sounds";
			this.mPlaySounds.Click += new System.EventHandler(this.mPlaySounds_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mHTP,
																					  this.mAbout});
			this.menuItem1.Text = "Help";
			// 
			// mHTP
			// 
			this.mHTP.Index = 0;
			this.mHTP.Text = "How to play";
			this.mHTP.Click += new System.EventHandler(this.mHTP_Click);
			// 
			// mAbout
			// 
			this.mAbout.Index = 1;
			this.mAbout.Text = "About";
			this.mAbout.Click += new System.EventHandler(this.mAbout_Click);
			// 
			// Target_Timer
			// 
			this.Target_Timer.Interval = 30;
			this.Target_Timer.Tick += new System.EventHandler(this.Target_Timer_Tick);
			// 
			// PlayerHit_Timer
			// 
			this.PlayerHit_Timer.Interval = 50;
			this.PlayerHit_Timer.Tick += new System.EventHandler(this.PlayerHit_Timer_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(590, 564);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(600, 600);
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(600, 600);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Space Defender ";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
			this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.MainForm_MouseLeave);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		} // end Main()

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			// initialize variables for targeting bullseye
			targetingCounter = new int [2];
			targetingCounter[0] = 10;
			targetingCounter[1] = 5;

			highScoreTop = new int [2];
			highScoreTop[0] = 600;
			highScoreTop[1] = 600;
			highScoreDelay = 20;
			highScore = new int [10];
			names = new string [10];
			for (int count = 0 ; count < highScore.Length; count ++)
			{
				highScore[count] = 5000 + 500 *(highScore.Length - count);
				names[count] = "MJR";
			}
			loadHighScores();
			StartAgain();
			testString = " ";
		} // end MainForm_Load (object, EventArgs)

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			this.DrawBottomLayer(g);

			if (level % 3 != 1 || roundStarted)
			{

				//Draw PowerUps
				for (int PowerUpIndex = 0; PowerUpIndex < powerUp.Length; PowerUpIndex++)
				{
					//if (powerUp[PowerUpIndex].isVisible())
						powerUp[PowerUpIndex].Draw(g);
				} // end for

				// Draw Enemies
				for (int count = 0; count < enemySaucer.Length; count++)
				{
					if (!paused)
						enemySaucer[count].drawLasers(g);

					enemySaucer[count].DrawSprite(g);
				}
				for (int count = 0; count < enemyMine.Length; count++)
				{
					enemyMine[count].DrawSprite(g);
				}

				// Draw Lasers
				ship.drawLasers(g);
				ship.wingman[0].drawLasers(g);
				ship.wingman[1].drawLasers(g);

				for (int WingmanIndex = 0; WingmanIndex < ship.wingman.Length; WingmanIndex ++)
				{
					if (ship.wingman[WingmanIndex].isVisible())
					{
						ship.wingman[WingmanIndex].Draw(g);
						//g.DrawImage(ship.wingman[WingmanIndex].getImage(), ship.wingman[WingmanIndex].getLeft(), ship.wingman[WingmanIndex].getTop(), ship.wingman[WingmanIndex].getWidth(), ship.wingman[WingmanIndex].getHeight());	 
					} // end if
				} // end for

				// Draw Ship
				ship.Draw(g);
				this.DrawOverlay(g);
			}

			this.DrawTopLayer(g);
			g.DrawString(testString, new Font("Arial", 14), Brushes.White, 0, 0);
		} // end OnPaint (PaintEventArgs)

		private void DrawBottomLayer(Graphics g)
		{
			// add code here
			// Draw Stars
			for (int count = 0; count < star.Length; count++)
				star[count].Draw(g);

			// Draw Wall
			if (level%3 == 1)
			{
				for (int count= 0; count < wall.Length; count++)
					g.DrawLine(new Pen(Color.Brown, 1), count, wall[count], count, this.ClientSize.Height);
			}
		}
		private void DrawOverlay(Graphics g)
		{
			// add code here
			//Draw Exploding Lasers
			for (int count = 0; count < enemySaucer.Length; count++)
			{
				for (int countLaser = 0; countLaser < enemySaucer[count].laser.Length; countLaser++)
				{
					if (enemySaucer[count].laser[countLaser].isExploding())
						enemySaucer[count].laser[countLaser].Draw(g);
				}
				
			}

			//Draw PowerUps Overlay
			for (int PowerUpIndex = 0; PowerUpIndex < powerUp.Length; PowerUpIndex++)
			{
				//if (powerUp[PowerUpIndex].isVisible())
				powerUp[PowerUpIndex].DrawOverlay(g);
			} // end for
		}
		private void DrawTopLayer(Graphics g)
		{
			if (roundStarted)
			{
				// Top layers
				// Draw targeting bullseye
				if (!gameOver)// && roundStarted)
				{
					for (int count = 0; count < targetingCounter.Length; count++)
						g.DrawEllipse(new Pen(Color.FromArgb(255 - (targetingCounter[count] * 20), 255, 0, 0), 2), targetX - targetingCounter[count], targetY - targetingCounter[count], targetingCounter[count] * 2, targetingCounter[count] * 2);
					//	Draw target "deadeye"
					g.FillEllipse(Brushes.Black, targetX-1, targetY -1, 2, 2);
				}

				
				// Draw Lives
				if (lives <= 3)
				{
					for (int count = 0; count < lives; count ++)
					{
						g.DrawImage(ship.getImage(), new Rectangle(10 + 20 * count, 10, 20, 20), 50, 0, 50, 50, GraphicsUnit.Pixel);
					}
				}
				else
				{
					g.DrawImage(ship.getImage(), new Rectangle( 10, 10, 20, 20), 50, 0, 50, 50, GraphicsUnit.Pixel);
					g.DrawString("x " + lives, new Font ("Arial", 12, FontStyle.Bold), new SolidBrush(Color.White), 35, 10);
				}

				// Draw Shield Bar
				for (int count = 0; count < ship.shield.getCharge(); count++)
				{
					g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(155, 0, 255, 0)), 2), this.Width/2 - (ship.shield.getMaxCharge()/2 - count), 15, this.Width/2 - (ship.shield.getMaxCharge()/2 - count), 25);
				}
				for (int count = 0; count < shieldLoss; count++)
				{		
					if (shieldLoss < 155)
						g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(155/shieldLoss * count, 0, 255, 0)), 2), this.Width/2 - ship.shield.getMaxCharge()/2 + ship.shield.getCharge() - 1 + (shieldLoss - count), 15, this.Width/2 - ship.shield.getMaxCharge()/2 + ship.shield.getCharge() -1  + (shieldLoss - count), 25);
					else
						g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(155, 0, 255, 0)), 2), this.Width/2 - ship.shield.getMaxCharge()/2 + ship.shield.getCharge() - 1 + (shieldLoss - count), 15, this.Width/2 - ship.shield.getMaxCharge()/2 + ship.shield.getCharge() -1  + (shieldLoss - count), 25);
				}
				g.DrawImage(Image.FromFile("images\\bar.png"), this.Width/2 - (ship.shield.getMaxCharge()/2) - 6, 10, ship.shield.getMaxCharge() + 11, 20);

				// Print score
				String scoreString = Convert.ToString(score);
				Font scoreFont = new Font("Arial", 14);
				g.DrawString(scoreString, scoreFont, Brushes.Red, this.ClientSize.Width - (scoreString.Length * (scoreFont.SizeInPoints - 3)) - 12, 5);
				if (score > highScore[highScore.Length-1])
					g.DrawString(scoreString, scoreFont, Brushes.Yellow, this.ClientSize.Width - (scoreString.Length * (scoreFont.SizeInPoints - 3)) - 12, 5);
					
				// Print "Game Over"
				if (gameOver)
				{
					DrawHighScores(g);
					g.DrawString("Game Over", new Font("Arial", 24), new SolidBrush (Color.FromArgb(190, 55, 55, 55)), (this.ClientSize.Width/2 - (12*7)) + 2, (this.ClientSize.Height/3 - 12) + 2);
					g.DrawString("Game Over", new Font("Arial", 24), Brushes.Red, this.ClientSize.Width/2 - (12*7), this.ClientSize.Height/3 - 12);
					
				}
			} // end if (roundStarted)

			if (!roundStarted)
			{
				g.DrawString("Level " + level, new Font("Arial", 24), new SolidBrush(Color.FromArgb(190, 55, 55, 55)), (this.ClientSize.Width/2 - (12*5) + 2), (this.ClientSize.Height/3 - 12) + 2);
				g.DrawString("Level " + level, new Font("Arial", 24), Brushes.White, this.ClientSize.Width/2 - (12*5), this.ClientSize.Height/3 - 12);
			} // end else

			if (paused) // if paused, say so
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb(65, 255, 0, 0)), 0, 0, this.ClientSize.Width, this.ClientSize.Height);
				g.DrawString("Pause", new Font("Arial", 24), Brushes.Black, this.ClientSize.Width/2 - (12*5 - 3), this.ClientSize.Height/2 - 9);
				g.DrawString("Pause", new Font("Arial", 24), Brushes.Red, this.ClientSize.Width/2 - (12*5), this.ClientSize.Height/2 - 12);
			} // end if (paused)

		}

		private void DrawHighScores (Graphics g)
		{
			for (int loopTop = 0; loopTop < highScoreTop.Length; loopTop++)
			{
				string tempString = "High Scores";
				g.DrawString(tempString, new Font ("Courier New", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(190,55,55,55)), (this.ClientSize.Width/2 - (tempString.Length * 6)) + 1, (highScoreTop[loopTop] - 32) + 1);
				g.DrawString(tempString, new Font ("Courier New", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(190,255,255,255)), this.ClientSize.Width/2 - (tempString.Length * 6), highScoreTop[loopTop] - 32);

				for (int count = highScore.Length -1; count >= 0 ; count --)
				{
					g.DrawString(names[count], new Font ("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(190,55,55,55)), (this.ClientSize.Width/2 - ((names[count].Length +1) * 10)) +1, (highScoreTop[loopTop] + (count * 16))+1);
					g.DrawString(" " + highScore[count], new Font ("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(190,55,55,55)), (this.ClientSize.Width/2) +1, (highScoreTop[loopTop] + (count * 16))+1);
					g.DrawString(names[count], new Font ("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(190,255,255,255)), this.ClientSize.Width/2 - ((names[count].Length +1) * 10), highScoreTop[loopTop] + (count * 16));
					g.DrawString(" " + highScore[count], new Font ("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(190,255,255,255)), this.ClientSize.Width/2, highScoreTop[loopTop] + (count * 16));
				}
			}
		}

		private void FrameChange_Timer_Tick(object sender, System.EventArgs e)
		{
			//testString = ""; // Change testString for testing of values

			// Animate shield bar
			if (ship.shield.isActive())// && roundStarted && !roundComplete)
			{
				ship.shield.adjustCharge(-1);
				shieldLoss++;
			}
			if (shieldLoss > 0)
				shieldLoss --;
			// Animate Shield
			if (ship.shield.isVisible())
				ship.shield.Animate();


			// relocate targeting "DeadEye"
			targetX = mouseX;
			if (level % 3 != 2)
				targetY = mouseY;

			// update background color
			if (this.BackColor != Color.Black)
			{
				int color = this.BackColor.R;
				if (color - 25 > 0)
					color-=25;
				else
					color = 0;
				this.BackColor = Color.FromArgb(color, color, color);
			}

			// Before round starts, draw "loading animation"
			if (!roundStarted)
			{
				IntroAnimation();
			} // end if
            
			// animate if during level gameplay
			else if (!roundComplete)
			{
				// Twinkle Stars
				AnimateStars();

				// Animate Bullseye 
				for (int count = 0; count < targetingCounter.Length; count++)
				{
					if (targetingCounter[count] > 0)
						targetingCounter[count]--;
					else 
						targetingCounter[count] = 10;
				} // end for

				//Animate Ship Movement
				if (ship.getLastLeft() < ship.getLeft())
				{
					ship.setSpriteColumn(3);
					ship.setLeft(ship.getLeft());
				}
				else if (ship.getLastLeft() > ship.getLeft())
				{
					ship.setSpriteColumn(1);
					ship.setLeft(ship.getLeft());
				}
				else
					ship.setSpriteColumn(2);

				// Animate Lasers
				ship.AnimateLasersUp(0);
				for (int count = 0; count < ship.wingman.Length; count++)
					ship.wingman[count].AnimateLasersUp(0);
				for (int count = 0; count < enemySaucer.Length; count++)
				{
					enemySaucer[count].AnimateLasersDown(this.ClientSize.Height);
					if (enemySaucer[count].getHitDelay() > 1)
						enemySaucer[count].setHitDelay(enemySaucer[count].getHitDelay() - 1);
					else if (enemySaucer[count].getHitDelay() == 1 && !enemySaucer[count].isExploding())
					{
						enemySaucer[count].setHitDelay(enemySaucer[count].getHitDelay() - 1);
						enemySaucer[count].setHitStatus(false);
						enemySaucer[count].setSpriteColumn(1);
					}
				} // end for			


				// Animate Power-Ups
				for (int count = 0; count < powerUp.Length; count++)
				{
					if (powerUp[count].isActive())
						powerUp[count].Animate();
					if (powerUp[count].getTop() > this.Height)
					{
						powerUp[count].setActive(false);
						powerUp[count].setVisible(false);
					}
					if (powerUp[count].displayString.isActive())
						powerUp[count].displayString.Animate();
				} // end for

				// Animate Wingmen
				AnimateWingmen();

				// Check for Collisions
				DetectCollisions();
				
				AnimateEnemies();

				// wingmen try to fire weapons
				for (int count = 0; count < ship.wingman.Length; count ++)
				{
					if (ship.wingman[count].isActive() && !roundComplete)
					{
						ship.wingman[count].setLaserDelay(ship.wingman[count].getLaserDelay() - 1);
						if (ship.wingman[count].getLaserDelay() <= 0)
						{
							ship.wingman[count].tryToFire(mPlaySounds.Checked);
							ship.wingman[count].setLaserDelay(15);
						} // end if
					} // end if
				} // end for

				TestRoundComplete();

			} // end else if (!roundComplete)

			else if (roundComplete & !gameOver)
			{
				LevelEndAnimation();
			}// end else if

			if (gameOver)
			{
				if (highScoreDelay > 0)
					highScoreDelay --;
				else
				{
					for (int count = 0; count < highScoreTop.Length; count++)
					{
						highScoreTop[count]--;
						if (highScoreTop[count] < -600)
							highScoreTop[count] = 600;
					}
				}

			}
				Invalidate();			
		} // end FrameChange_Timer_Tick(object, EventArgs)

		private void StartAgain()
		{
			this.Cursor = new Cursor("images\\Blank.cur");
			this.BackColor = Color.Black;
			loadHighScores();
			level = 0;
			lives = 3;
			paused = false;
			FrameChange_Timer.Enabled = true;
			score = 0;
			gameOver = false;
			ship.setActive(true);
			ship.setVisible(true);
			ship.wingman[0].setActive(false);
			ship.wingman[0].setVisible(false);
			ship.wingman[1].setActive(false);
			ship.wingman[1].setVisible(false);
			flashCount = 0;
			ship.shield.adjustCharge(ship.shield.getMaxCharge());
			shieldLoss = 0;
			laserPower = 1;
			for (int countLasers = 0 ; countLasers < ship.laser.Length; countLasers++)
			{
				ship.laser[countLasers].setHeight(9);
				ship.laser[countLasers].setSpeed(6);
			}// end for

			StartRound();

			
		} // end StartAgain ()

		private void StartRound ()
		{
			roundStarted = false;
			roundComplete = false;
			level++;


			// make wall
			if (level%3 == 1)
			{
				wall = new int [this.ClientSize.Width];
				for (int count = 0; count < wall.Length; count++)
					wall[count] = this.ClientSize.Height;
				countWall = 0;
			}
			// make stars
			for (int count = 0; count < star.Length; count++)
			{
				makeStar(count);
			} // end for

			// make enemies
			MakeEnemies();
			
			// make power-ups.
			for (int countPowerUps = 0; countPowerUps < powerUp.Length; countPowerUps++)
			{
				powerUp[countPowerUps] = new PowerUp(0, 0, 30, 15, Image.FromFile("images\\powerup.png"));
			} // end for

			// reset ship
			if (level % 3 == 1)
				ship.setTop(455);
			else
				ship.setTop(600);
			ship.wingman[0].setTop(ship.getMiddleY() - 10);
			ship.wingman[1].setTop(ship.getMiddleY() - 10);
			// reset lasers
			ship.moveLasers(ship.getMiddleX(), ship.getMiddleY());
			for (int count = 0; count < ship.wingman.Length; count++)
			{
				ship.wingman[count].moveLasers(ship.wingman[count].getMiddleX(), ship.wingman[count].getMiddleY());
			} // end for

		} // end StartRound()
		private void mExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void mNewGame_Click(object sender, System.EventArgs e)
		{
			StartAgain();
		}

		private void makeWall (int max)
		{
			for (int count = 0; count < max && !roundStarted; count++)
			{
				int upOrDown;
				if (countWall < wall.Length)
				{
					if (countWall == 0)
					{
						wall[countWall] = this.ClientSize.Height - rand.Next(25, 50);
					}
					else 
					{
						if (wall[countWall -1] > this.ClientSize.Height - 15)
							upOrDown = 0;
						else if (wall[countWall -1] < this.ClientSize.Height - 50)
							upOrDown = 1;
						else
							upOrDown = rand.Next(1, 100);

					
						switch (upOrDown%3)
						{
							case 0:
								wall[countWall] = wall[countWall - 1] - rand.Next(1, 3);
								break;
							case 1:
								wall[countWall] = wall[countWall - 1] + rand.Next(1, 3);
								break;
							default:
								wall[countWall] = wall[countWall -1];
								break;
						}// end switch
					} // end else
					countWall++;
				}// end if
				else
					roundStarted = true;
			} // end for
		} // end makeWall (int)

		private void MainForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!roundComplete)
			{
				// center target cursor
				mouseX = e.X;
				mouseY = e.Y;

				// move ship
				if (e.X + ship.getWidth()/2 >= this.ClientSize.Width)
				{
					ship.setLeft(this.ClientSize.Width - ship.getWidth());
				}
				else if (e.X - ship.getWidth()/2 <= 0)
				{
					ship.setLeft(0);
				}
				else
				{
					ship.setLeft(e.X - ship.getWidth()/2);
				}

				if (level % 3 == 2)
				{

					targetY = 90;

					if (roundStarted)
					{
						if (e.Y < 100)
							ship.setTop(100);
						else if (e.Y >475)
							ship.setTop(475);
						else
							ship.setTop(e.Y);

					}
				}

				ship.shield.setLeft(ship.getLeft() - 10);
				ship.shield.setTop(ship.getTop() - 5);
				
			//	if (paused)
					//Invalidate();

			} // end if
		} // end MainForm_MouseMove (object, MouseEventArgs)

		private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button.Equals(MouseButtons.Left))
			{
				if (paused) // if paused, unpause game
				{
					unPause();
				}
				else // if game not paused
				{
			
					if (roundStarted && !roundComplete) // game is already started (done loading)
					{
						ship.tryToFire(mPlaySounds.Checked);
					}
					else if (!roundStarted)
					{
						if (level % 3 == 1)  // if game is still loading
							makeWall(this.ClientSize.Width); // generate rest of the wall
						else if (level % 3 == 2 && !roundComplete)
							ship.setTop(e.Y);
						else if (level % 3 == 0)
							ship.setTop(450);
					}
				}
			} // end if

			if (e.Button.Equals(MouseButtons.Right))
			{
				if (!gameOver && !paused && roundStarted && !roundComplete && ship.shield.getCharge() > 0)
				{
					ship.shield.StartUp();
					//ship.shield.setActive(true);
					//ship.shield.adjustCharge(-1);
				}
			}

		} // end MainForm_MouseDown (object, MouseEventArgs)

		private void MainForm_MouseLeave(object sender, System.EventArgs e)
		{
			if (!gameOver && mPauseOffScreen.Checked && !paused)
			{
				pause();
			} // end if
		} // end MainForm_MouseLeave (object, System.Events)

		private void AnimateStars ()
		{
			for (int count = 0; count < star.Length; count++)
			{
				star[count].Animate(rand.Next(20,100));
				// relocate to top of screen
				star[count].setY(star[count].getY() + star[count].getSpeed());
				if (star[count].getY() > this.ClientSize.Height + star[count].getSize())
					makeStar(count, 0);
			} // end for
		} // end AnimateStars ()

		private void DetectCollisions()
		{
			// check collisions between hero ship and power-ups
			for (int countPowerUp = 0; countPowerUp < powerUp.Length; countPowerUp++)
			{
				if (CollisionDetection.SquareHitTriangle(powerUp[countPowerUp], ship))
				{
					string printString = "Error";
					switch (powerUp[countPowerUp].getType())
					{
						case 0:
							if (!ship.wingman[0].isActive())
							{
								printString = "Wingman";
								ship.wingman[0].setRight(ship.getLeft());
								ship.wingman[0].setActive(true);
								ship.wingman[0].setVisible(true);
								ship.wingman[0].setTop(this.Height);
							}
							else if (!ship.wingman[1].isActive())
							{
								printString = "Wingman";
								ship.wingman[1].setLeft(ship.getRight());
								ship.wingman[1].setActive(true);
								ship.wingman[1].setVisible(true);
								ship.wingman[1].setTop(this.Height);
							}
							else
							{
								printString = "1-Up";
								lives++;
							}
							break;

						case 1:
							for (int countLasers = 0 ; countLasers < ship.laser.Length; countLasers++)
							{
								if (ship.laser[countLasers].getSpeed() + 1 < 9)
									ship.laser[countLasers].setSpeed(ship.laser[countLasers].getSpeed() +1);
								else
								{
									ship.laser[countLasers].setSpeed(9);
								}
							} // end for
							
							if (ship.laser[1].getSpeed() == 9)
							{
								udpateScore(200);
								//score += 200;
								printString = "+200";
							}
							else
								printString = "Faster Lasers";


							break;

						case 2:
							for (int countLasers = 0 ; countLasers < ship.laser.Length; countLasers++)
							{
								if (ship.laser[countLasers].getHeight() + 1 <= 15)
									ship.laser[countLasers].setHeight(ship.laser[countLasers].getHeight() +1);
								else
								{
									ship.laser[countLasers].setHeight(15);
								}

							}// end for

							if (ship.laser[1].getHeight() == 15)
							{
								printString = "+200";
								//score += 200;
								udpateScore(200);
							}
							else
								printString = "Longer Lasers";

							break;
						
						case 3:
						
							if (laserPower < 5)
							{
								laserPower++;
								printString = "Laser Power Up";
							}
							else
							{
								udpateScore(200);
								//score += 200;
								printString = "+200";

							}
							break;
						case 4:
							printString = "Shield Recharge\n          ";
							if (ship.shield.getCharge() + ship.shield.getMaxCharge()/4 >  ship.shield.getMaxCharge())
							{
							//	int scoreIncrease = 
								udpateScore((ship.shield.getCharge() + ship.shield.getMaxCharge()/4 - ship.shield.getMaxCharge()) * 5);
								//score += (ship.shield.getCharge() + ship.shield.getMaxCharge()/4 - ship.shield.getMaxCharge()) * 5;
								printString += " +" + ((ship.shield.getCharge() + ship.shield.getMaxCharge()/4 - ship.shield.getMaxCharge()) * 5);
							}
							ship.shield.adjustCharge(ship.shield.getMaxCharge()/4);
							//displayString[countPowerUp] = new DisplayString(powerUp[countPowerUp].getMiddleX(), powerUp[countPowerUp].getMiddleY(), new Font("Arial", 12, FontStyle.Bold), "Shield Recharge");

							break;

					} // end switch

					powerUp[countPowerUp].setActive(false);
					if (powerUp[countPowerUp].getType() != 4)
					{
						if ((powerUp[countPowerUp].getMiddleX() + 12*printString.Length) > this.ClientSize.Width)
							powerUp[countPowerUp].displayString.setLeft(this.ClientSize.Width - printString.Length*12);
						else
							powerUp[countPowerUp].displayString.setLeft(powerUp[countPowerUp].getMiddleX());
					}
					else
					{
						if ((powerUp[countPowerUp].getMiddleX() + 12*15) > this.ClientSize.Width)
							powerUp[countPowerUp].displayString.setLeft(this.ClientSize.Width - 12*15);
						else
							powerUp[countPowerUp].displayString.setLeft(powerUp[countPowerUp].getMiddleX());
					}

					powerUp[countPowerUp].displayString.setTop(powerUp[countPowerUp].getMiddleY());

					if (powerUp[countPowerUp].getMiddleX() > this.ClientSize.Width/2)
					{
						powerUp[countPowerUp].displayString.setLeft(powerUp[countPowerUp].displayString.getLeft()-1);
					}
					else
					{
						powerUp[countPowerUp].displayString.setLeft(powerUp[countPowerUp].displayString.getLeft()+1);
					}
					if (powerUp[countPowerUp].getMiddleY() > this.ClientSize.Height/2)
					{
						powerUp[countPowerUp].displayString.setTop(powerUp[countPowerUp].displayString.getTop()-1);
					}
					else
					{
						powerUp[countPowerUp].displayString.setTop(powerUp[countPowerUp].displayString.getTop()+2);
					}
					
					powerUp[countPowerUp].displayString.resetValues(printString);

					//powerUp[countPowerUp].setVisible(false);
				} // end if
			} // end for
			// check collisions with hero ship's lasers
			for (int countLaser = 0; countLaser < ship.laser.Length; countLaser ++)
			{
				// lasers hitting enemyMines

				for (int countMine = 0; countMine < enemyMine.Length; countMine++)
				{
					if (CollisionDetection.SquareHitCircle(ship.laser[countLaser], enemyMine[countMine]) && enemyMine[countMine].isActive())
					{
						enemyMine[countMine].setActive(false);
						enemyMine[countMine].setExploding(true);
						enemyMine[countMine].setVisible(false);

						ship.laser[countLaser].HitAnimation();
					}
				}
				//lasers hitting enemySaucer ships
				for (int countShip = 0; countShip < enemySaucer.Length; countShip++)
				{
					if (CollisionDetection.LaserHitSquare(ship.laser[countLaser], enemySaucer[countShip]) && enemySaucer[countShip].isActive())
					{
						enemySaucer[countShip].setHitPoints(enemySaucer[countShip].getHitPoints() - laserPower);
						enemySaucer[countShip].setSpriteColumn(2);
						enemySaucer[countShip].setHitStatus(true);
						enemySaucer[countShip].setHitDelay(3);

						ship.laser[countLaser].HitAnimation();
					

						if (enemySaucer[countShip].getHitPoints() <= 0)
						{
							enemySaucer[countShip].setActive(false);
							enemySaucer[countShip].setExploding(true);
							//score += enemySaucer[countShip].getPointValue();
							udpateScore(enemySaucer[countShip].getPointValue());

							// create power ups
							if (rand.Next(1, 100)%15 == 5)  
							{
								ActivatePowerup(enemySaucer[countShip].getMiddleY(), enemySaucer[countShip].getLeft());
							} // end if
						} // end if

					}// end if
				}// end for
				for (int countWingman = 0; countWingman < ship.wingman.Length; countWingman++)
				{
					for (int countShip = 0; countShip < enemySaucer.Length; countShip++)
					{
						if (CollisionDetection.LaserHitSquare(ship.wingman[countWingman].laser[countLaser], enemySaucer[countShip]) && enemySaucer[countShip].isActive())
						{
							enemySaucer[countShip].setHitPoints(enemySaucer[countShip].getHitPoints() -1);
							enemySaucer[countShip].setSpriteColumn(2);
							enemySaucer[countShip].setHitStatus(true);
							enemySaucer[countShip].setHitDelay(3);

							ship.wingman[countWingman].laser[countLaser].HitAnimation();

							if (enemySaucer[countShip].getHitPoints() == 0)
							{

								enemySaucer[countShip].setActive(false);
								enemySaucer[countShip].setExploding(true);
								//score += enemySaucer[countShip].getPointValue();
								udpateScore(enemySaucer[countShip].getPointValue());

								// create power ups
								if (rand.Next(1, 100)% 15 == 7)
								{
									ActivatePowerup(enemySaucer[countShip].getMiddleY(), enemySaucer[countShip].getLeft());

								} // end if
							} // end if 
						} //end if
					} // end for
				} // end for
			} // end for

				// check collisions with enemySaucer ship's lasers
			for (int countEnemy = 0; countEnemy < enemySaucer.Length; countEnemy++) // for all enemySaucer ships
			{
				if (CollisionDetection.SquareHitCircle(enemySaucer[countEnemy], ship.shield) && ship.shield.isActive() && enemySaucer[countEnemy].isActive())
				{
					while (enemySaucer[countEnemy].getHitPoints() > 0)
					{
						enemySaucer[countEnemy].setHitPoints(enemySaucer[countEnemy].getHitPoints() -1);
						ship.shield.adjustCharge(- 2);
						shieldLoss += 2;
					}
					ship.shield.hitSomething();
					enemySaucer[countEnemy].setExploding(true);
					enemySaucer[countEnemy].setActive(false);
				}

				else if (CollisionDetection.SquareHitTriangle(enemySaucer[countEnemy], ship) && !PlayerHit_Timer.Enabled )
				{
					enemySaucer[countEnemy].setExploding(true);
					enemySaucer[countEnemy].setActive(false);
					
					if (!PlayerHit_Timer.Enabled)		
					{
						this.BackColor = Color.White;
						if (ship.wingman[1].isActive())
						{
							ship.wingman[1].setActive(false);
							ship.wingman[1].setVisible(false);
						}
						else if (ship.wingman[0].isActive())
						{
							ship.wingman[0].setActive(false);
							ship.wingman[0].setVisible(false);
						}
						else
							lives--;
							
					
						if (lives < 0)
						{
							ship.setActive(false);
							ship.setVisible(false);
							gameOver = true;
							this.Cursor = Cursors.Default;
							highScoreTop[0] = this.ClientSize.Height/2;
							highScoreTop[1] = highScoreTop[0] + 600;
							highScoreDelay = 150;
							if (score > highScore[highScore.Length -1])
							{

								for (int loopCount = highScore.Length -1; loopCount >= 0; loopCount--)
								{
									if (highScore[loopCount] < score)
									{
										if (loopCount < highScore.Length -1)
										{
											names[loopCount + 1] = names[loopCount];
											highScore[loopCount + 1] = highScore[loopCount];
										}
										highScore[loopCount] = score;
										names[loopCount] = "You!";
									}
									else
									{
										loopCount = -1;
									}
								}
								
								recordHighScore();
							}

						}
						else 
							PlayerHit_Timer.Enabled = true;
					}
				}

				for (int countLaser = 0; countLaser < enemySaucer[countEnemy].laser.Length; countLaser++) // check all lasers
				{
					if (CollisionDetection.SquareHitCircle(enemySaucer[countEnemy].laser[countLaser], ship.shield) && ship.shield.isActive() && enemySaucer[countEnemy].laser[countLaser].isActive())
					{
						enemySaucer[countEnemy].laser[countLaser].HitAnimation();
						ship.shield.adjustCharge(-2);
						shieldLoss += 2;
						ship.shield.hitSomething();
					}
					if (CollisionDetection.LaserHitPyramid(enemySaucer[countEnemy].laser[countLaser], ship) && !PlayerHit_Timer.Enabled)
					{
						enemySaucer[countEnemy].laser[countLaser].HitAnimation();

						this.BackColor = Color.White;

						if (ship.wingman[1].isActive())
						{
							ship.wingman[1].setActive(false);
							ship.wingman[1].setVisible(false);
						}
						else if (ship.wingman[0].isActive())
						{
							ship.wingman[0].setActive(false);
							ship.wingman[0].setVisible(false);
						}
						else
							lives--;
							
					
						if (lives < 0)
						{
							ship.setActive(false);
							ship.setVisible(false);
							gameOver = true;
							this.Cursor = Cursors.Default;
							highScoreTop[0] = this.ClientSize.Height/2;
							highScoreTop[1] = highScoreTop[0] + 600;
							highScoreDelay = 150;
							if (score > highScore[highScore.Length -1])
							{						
								for (int loopCount = highScore.Length -1; loopCount >= 0; loopCount--)
								{
									if (highScore[loopCount] < score)
									{
										if (loopCount < highScore.Length -1)
										{
											names[loopCount + 1] = names[loopCount];
											highScore[loopCount + 1] = highScore[loopCount];
										}
										highScore[loopCount] = score;
										names[loopCount] = "You!";
									}
									else
									{
										loopCount = -1;
									}
								}
								
								recordHighScore();
							}
						}
						else 
							PlayerHit_Timer.Enabled = true;
					}
					// check to see if lazer hit the wall
					if (level%3 == 1)
					{
						for (int countWall = 0; countWall < wall.Length; countWall++)
						{
							if (enemySaucer[countEnemy].laser[countLaser].getMiddleX() == countWall &&  enemySaucer[countEnemy].laser[countLaser].getBottom() >= wall[countWall])
							{							
								enemySaucer[countEnemy].laser[countLaser].setActive(false);
								enemySaucer[countEnemy].laser[countLaser].HitAnimation(enemySaucer[countEnemy].laser[countLaser].getMiddleX(), enemySaucer[countEnemy].laser[countLaser].getMiddleY());
							
								for (int countBoom = countWall - enemySaucer[countEnemy].laser[countLaser].getWidth() ; countBoom < countWall + enemySaucer[countEnemy].laser[countLaser].getWidth(); countBoom++)
								{
									wall[countBoom] += enemySaucer[countEnemy].laser[countLaser].getHeight()/2;

								}
							} // end if

						} //end for
					} // end if (level%3 == 1)
				} // end for
			} // end for 
			
																							   
		} // end DetectCollisions()

		private void makeStar(int count)
		{
			star[count] = new Star(rand.Next(this.ClientSize.Width), rand.Next(this.ClientSize.Height), Color.Transparent, rand.Next(1,4), 0, rand.Next(20, 100));
			int colorSwitch = rand.Next(0, 5);

			switch (colorSwitch)
			{
				case 0:
					star[count].setColor(Color.White); // white
					break;
				case 1:
					star[count].setColor(Color.FromArgb(180, 180, 255)); // blue
					break;
				case 2:
					star[count].setColor(Color.FromArgb(245, 195, 145)); //yellow
					break;
				case 3:
					star[count].setColor(Color.FromArgb(185, 185, 185)); // light grey
					break;
				default:
					star[count].setColor(Color.Transparent);
					break;
			}

			if (level %3 != 1)
				star[count].setSpeed();
		} // end makeStar (int)

		/// <summary>
		/// Method used to move stars from the bottom of the screen to the top of the screen.
		/// This is stricktly to emulate space travel.  Will not be implimented in the 
		/// "Space Defender" program, but is in place for the sequel "Space Avenger"
		/// </summary>
		/// <param name="count">Number of star in array of stars</param>
		/// <param name="Y">Y coordinate to be reloacted to</param>
		private void makeStar (int count, int Y)
		{
			makeStar(count);
			star[count].setY(Y - star[count].getSize());
		} //end makeStar (int, int)

		private void moveStar (int count)
		{
			if (star[count].getY() > this.Height)
			{
				makeStar (count, -2);
				star[count].setSpeed();
			}
			else
				star[count].setY(star[count].getY() + star[count].getSpeed());

												
		} // end moveStar (int)

		private void mPauseOffScreen_Click(object sender, System.EventArgs e)
		{
			if (mPauseOffScreen.Checked)
				mPauseOffScreen.Checked = false;
			else
				mPauseOffScreen.Checked = true;
		} // end mPauseOffScreen_Click (object, System.EventArgs)

		private void Target_Timer_Tick(object sender, System.EventArgs e)
		{

			if (paused) // if paused and mouse is in window, redraw screen to show ship movement
			{
				Invalidate();
			}
		} // end Target_Timer_Tick (object, System.EventArgs)

		private void TestRoundComplete ()
		{
			// assume round is over
			roundComplete  = true;
			// if an enemySaucer exists make roundComplete false;
			for (int count = 0 ; count < enemySaucer.Length; count++)
			{
				if (enemySaucer[count].isActive())
				{					
					roundComplete = false;
				}
				
				for (int countLasers = 0; countLasers < enemySaucer[count].laser.Length; countLasers ++)
				{
					if (enemySaucer[count].laser[countLasers].isActive())
						roundComplete = false;
				}
			}
			// if a PowerUp exists make roundComplete false
			for (int count = 0; count < powerUp.Length; count ++)
			{
				if (powerUp[count].isActive())
					roundComplete = false;
			}

			if (roundComplete)
			{
				ship.setSpriteColumn(2);
				if (ship.shield.isActive())
					ship.shield.ShutDown();
				if (mPlaySounds.Checked)
				{
					Sound.FlushAudio();
					Sound.PlayAudioAsync("sounds\\engine.wav");
					Sound.PlayAudioAsync("sounds\\engine2.wav");
				}
			}
		
		} // end TestRoundComplete()

		private void LevelEndAnimation ()
		{
			if (ship.getBottom() > - 50)
			{
				targetY = -20;
				ship.setTop(ship.getTop() - ship.getHeight());
				ship.wingman[0].setTop(ship.wingman[0].getTop() - ship.getHeight());
				ship.wingman[1].setTop(ship.wingman[1].getTop() - ship.getHeight());
				ship.shield.setTop(ship.shield.getTop() - ship.getHeight());
			
				// Animate Lasers
				ship.AnimateLasersUp(0);
				for (int count = 0; count < ship.wingman.Length; count++)
					ship.wingman[count].AnimateLasersUp(0);
				for (int count = 0; count < enemySaucer.Length; count++)
					enemySaucer[count].AnimateLasersDown(this.ClientSize.Height);
				
				// Animate PowerUp overlays
				for (int count = 0; count < powerUp.Length; count++)
					powerUp[count].displayString.Animate();
				// Animate Enemy explosions
				for (int count = 0; count < enemySaucer.Length; count++)
					enemySaucer[count].Animate();
			}
			else
				StartRound();
		} // end LevelEndAnimation()

		private void PlayerHit_Timer_Tick(object sender, System.EventArgs e)
		{
			flashCount++;
			if (ship.isVisible())
				ship.setVisible(false);
			else
				ship.setVisible(true);

			if (flashCount > 20)
			{
				ship.setVisible(true);
				PlayerHit_Timer.Enabled = false;
				flashCount = 0;
			}
		} // end PlayerHit_Timer_Tick(object, System.EventArgs)

		private void IntroAnimation()
		{
			if (level % 3 == 1)
			{
				makeWall(15);
			}
			else if (level % 3 == 2)
			{
				ship.setTop(ship.getTop()-10);
				

				/// ***** ////
				if (ship.getTop() <= mouseY && !roundStarted)
				{
					roundStarted = true;
				}

			}
			else if (level % 3 == 0)
			{
				ship.setTop(ship.getTop()-10);

				if (ship.getTop() <= 450)
					roundStarted = true;
			}
		}

		private void ActivatePowerup(int locx, int locy)
		{
			for (int countPowerup = 0; countPowerup < powerUp.Length; countPowerup++)
			{
				if (!powerUp[countPowerup].isActive())
				{
					powerUp[countPowerup].setType(rand.Next(100)%5);
					powerUp[countPowerup].setTop(locx);
					powerUp[countPowerup].setLeft(locy);
					powerUp[countPowerup].setActive(true);
					powerUp[countPowerup].setVisible(true);

					countPowerup = powerUp.Length;
				}
			}
		}

		private void mPlaySounds_Click(object sender, System.EventArgs e)
		{
			if (mPlaySounds.Checked)
				mPlaySounds.Checked = false;
			else
				mPlaySounds.Checked = true;
		}

		private void mHTP_Click(object sender, System.EventArgs e)
		{
			if (!paused)
			{
				FrameChange_Timer.Enabled = false;
				paused = true;
				Invalidate();
			}

			Point startPoint = this.Location;
			startPoint.X += this.ClientSize.Width/2;
			startPoint.Y += this.ClientSize.Height/2;

			HowToPlayForm newHTPForm = new HowToPlayForm(startPoint);
			newHTPForm.ShowDialog();
		}

		private void MainForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				if (!paused)			
					pause();
				else
					unPause();
			}

			else if (e.KeyChar == '+')
				FrameChange_Timer.Interval -= 10;
			else if (e.KeyChar == '-')
				FrameChange_Timer.Interval += 10;
			else if (e.KeyChar == 'w')
			{
				ship.wingman[0].setActive(true);
				ship.wingman[0].setVisible(true);
				ship.wingman[1].setActive(true);
				ship.wingman[1].setVisible(true);
			}
		}

		private void pause()
		{
			//ship.shield.ShutDown();
			FrameChange_Timer.Enabled = false;
			paused = true;
			this.Cursor = Cursors.Default;
			Invalidate();
		}

		private void unPause()
		{
			this.Cursor = new Cursor("images\\Blank.cur");
			FrameChange_Timer.Enabled=true;
			paused = false;
		}

		private void MainForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button.Equals(MouseButtons.Right))
			{
				if (ship.shield.isActive())
					ship.shield.ShutDown();
					//ship.shield.setActive(false);
			}
		}

		private void mAbout_Click(object sender, System.EventArgs e)
		{

			if (!paused)
			{
				FrameChange_Timer.Enabled = false;
				paused = true;
				Invalidate();
			}

			Point startPoint = this.Location;
			startPoint.X += this.ClientSize.Width/2;
			startPoint.Y += this.ClientSize.Height/2;

			AboutForm newAboutForm = new AboutForm(startPoint);
			newAboutForm.ShowDialog();
			
		
		}

		private void udpateScore (int increase)
		{
			int before = score / 5000;
			lives += ((score + increase) / 5000) - before;
			score += increase;
		}

		private void loadHighScores()
		{
			//int loopCount = 1;
			//string loadVal = "True";
			//int loadInt;
			System.IO.StreamReader listLoad = null;
			//work with file
			try 
			{
				listLoad = new System.IO.StreamReader("highScores.dat");
			}
			catch (FileNotFoundException e)
			{
				StandardTools.recordError(e.Message);
				saveScores();
				listLoad = new System.IO.StreamReader("highScores.dat");
			}

			catch (Exception e)
			{
				StandardTools.recordError(e.Message);
				saveScores();
				listLoad = new System.IO.StreamReader("highScores.dat");
			}

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

			for (int loopCount = 0; loopCount < highScore.Length; loopCount++)
			{	
				saveFile.WriteLine("MJR," + highScore[loopCount]);
			}

			saveFile.Close();
		}

		private void recordHighScore()
		{
			Point startPoint = this.Location;
			startPoint.X += this.ClientSize.Width/2;
			startPoint.Y += this.ClientSize.Height/2;
			PlayerName playerNameForm = new PlayerName(score, startPoint);
			playerNameForm.ShowDialog();
		}

		private void MainForm_MouseEnter(object sender, System.EventArgs e)
		{
			if (gameOver)
				loadHighScores();
		}
		// save the scores through a for loop

		private void AnimateWingmen()
		{
			for (int count = 0; count < ship.wingman.Length; count ++)
			{

				if (ship.wingman[count].isActive())
				{

					if (ship.wingman[count].getTop() != ship.getMiddleY())
					{
						if (ship.wingman[count].getTop() - 20 >= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() - 20 );
						else if (ship.wingman[count].getTop() - 10 >= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() - 10 );
						else if (ship.wingman[count].getTop() - 5 >= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() - 5 );
						else if (ship.wingman[count].getTop() - 2 >= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() - 2 );
						else if (ship.wingman[count].getTop() + 20 <= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() + 20 );
						else if (ship.wingman[count].getTop() + 10 <= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() + 10 );
						else if (ship.wingman[count].getTop() + 5 <= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() + 5 );
						else if (ship.wingman[count].getTop() + 2 <= ship.getMiddleY() )
							ship.wingman[count].setTop(ship.wingman[count].getTop() + 2 );
						else
							ship.wingman[count].setTop(ship.getMiddleY());
					}
					if (count == 0 && ship.wingman[count].getRight() != ship.getLeft())
					{
						if (ship.wingman[count].getRight() - 75 > ship.getLeft())
							ship.wingman[count].setRight(ship.getLeft() + 75);
						else if (ship.wingman[count].getRight() - 10 >= ship.getLeft() )
							ship.wingman[count].setRight(ship.wingman[count].getRight() - 10);
						else if (ship.wingman[count].getRight() - 5 >= ship.getLeft() )
							ship.wingman[count].setRight(ship.wingman[count].getRight() - 5);
						else if (ship.wingman[count].getRight() - 2 >= ship.getLeft() )
							ship.wingman[count].setRight(ship.wingman[count].getRight() - 2);
						else if (ship.wingman[count].getRight() + 100 < ship.getLeft() )
							ship.wingman[count].setRight(ship.getLeft() - 100);
						else if (ship.wingman[count].getRight() + 10 <= ship.getLeft() )
							ship.wingman[count].setRight(ship.wingman[count].getRight() + 10);
						else if (ship.wingman[count].getRight() + 5 <= ship.getLeft() )
							ship.wingman[count].setRight(ship.wingman[count].getRight() + 5);
						else if (ship.wingman[count].getRight() + 2 <= ship.getLeft() )
							ship.wingman[count].setRight(ship.wingman[count].getRight() + 2);
						else
							ship.wingman[count].setRight(ship.getLeft());
					}
					else if (count == 1 && ship.wingman[count].getRight() != ship.getLeft())
					{
						if (ship.wingman[count].getLeft() - 75 > ship.getRight())
							ship.wingman[count].setLeft(ship.getRight() + 75);
						else if (ship.wingman[count].getLeft() - 10 >= ship.getRight() )
							ship.wingman[count].setLeft(ship.wingman[count].getLeft() - 10);
						else if (ship.wingman[count].getLeft() - 5 >= ship.getRight() )
							ship.wingman[count].setLeft(ship.wingman[count].getLeft() - 5);
						else if (ship.wingman[count].getLeft() - 2 >= ship.getRight() )
							ship.wingman[count].setLeft(ship.wingman[count].getLeft() - 2);
						else if (ship.wingman[count].getLeft() + 100 < ship.getRight() )
							ship.wingman[count].setLeft(ship.getRight() - 100);
						else if (ship.wingman[count].getLeft() + 10 <= ship.getRight() )
							ship.wingman[count].setLeft(ship.wingman[count].getLeft() + 10);
						else if (ship.wingman[count].getLeft() + 5 <= ship.getRight() )
							ship.wingman[count].setLeft(ship.wingman[count].getLeft() + 5);
						else if (ship.wingman[count].getLeft() + 2 <= ship.getRight() )
							ship.wingman[count].setLeft(ship.wingman[count].getLeft() + 2);
						else
							ship.wingman[count].setLeft(ship.getRight());
					}

					if (ship.wingman[count].getLastLeft() < ship.wingman[count].getLeft())
					{
						ship.wingman[count].setSpriteColumn(3);
						ship.wingman[count].setLeft(ship.wingman[count].getLeft());
					}
					else if (ship.wingman[count].getLastLeft() > ship.wingman[count].getLeft())
					{
						ship.wingman[count].setSpriteColumn(1);
						ship.wingman[count].setLeft(ship.wingman[count].getLeft());
					}
					else
						ship.wingman[count].setSpriteColumn(2);

				}
				lastMouseX = mouseX;
				lastMouseY = mouseY;
					
			} // end for
		}

		private void AnimateEnemies()
		{
			// Animate Mines
			for (int count = 0; count < enemyMine.Length; count++)
			{
				enemyMine[count].Animate();
				if (enemyMine[count].getTop() >= this.ClientSize.Height)
				{

					enemyMine[count].setTop(rand.Next(100, 1000) * -1 );
					enemyMine[count].setLeft(rand.Next(0, this.ClientSize.Width - enemyMine[count].getWidth()));
					enemyMine[count].setTop(enemyMine[count].getTop() +6);
				}
			}
						

			// enemySaucer tries to fire weapons 
			for (int count = 0; count < enemySaucer.Length; count++)
			{
				enemySaucer[count].setLaserDelay(enemySaucer[count].getLaserDelay() - 1);
				if (enemySaucer[count].getLaserDelay() <= 0 && enemySaucer[count].isActive())
				{
					enemySaucer[count].tryToFire(mPlaySounds.Checked);
					enemySaucer[count].setLaserDelay(rand.Next(30, 60));
				} // end if 
					
				if (enemySaucer[count].isVisible())
				{
					enemySaucer[count].Animate();
				}
		
				// relocate ships to top of screen
				if (level % 3 == 2)
				{
					if (enemySaucer[count].getTop() > this.Height)
						enemySaucer[count].setTop(rand.Next(200) * -1);
				}

			}// end for

			// enemySaucer move
			for (int count = 0; count < enemySaucer.Length; count++)
			{
				if (enemySaucer[count].getLeft()<=0 && enemySaucer[count].isActive())
				{ 
					enemySaucer[count].directionChange("right");
					if (level % 3 != 2)
					{
						for (int loop = 0; loop < enemySaucer.Length; loop++)
						{
							enemySaucer[loop].directionChange("right");
							//if (enemySaucer[loop].getTop() < this.Height - (enemySaucer[loop].getHeight() + 100))	
							if (enemySaucer[loop].getTop() < this.ClientSize.Height - enemySaucer[loop].getHeight() - 100 + (loop/10 * 50))
								enemySaucer[loop].setTop(enemySaucer[loop].getTop()+10);
							count = enemySaucer.Length;
						} // end for
					}
				} // end if
				else if (enemySaucer[count].getRight()>=this.ClientSize.Width && enemySaucer[count].isActive())
				{
					enemySaucer[count].directionChange("left");
					if (level % 3 != 2)
					{
						for (int loop = 0; loop < enemySaucer.Length; loop++)
						{
							enemySaucer[loop].directionChange("left");
							if (enemySaucer[loop].getTop() < this.ClientSize.Height - enemySaucer[loop].getHeight() - 100 + (loop/10 * 50))
								enemySaucer[loop].setTop(enemySaucer[loop].getTop()+10);
							count = enemySaucer.Length;
						} // end for
					}
				} // end else if
					
			} // end for
		}

		private void MakeEnemies()
		{
			if (level % 3 != 2)
			{
				for (int countMines = 0; countMines < enemyMine.Length; countMines++)
				{
					enemyMine[countMines] = new EnemyMine(new Point(100 * countMines, 100 * countMines), false);
				}

				for (int countShips = 0; countShips < enemySaucer.Length ; countShips++)
				{
					int column = countShips %10;
					int row = countShips / 10;
					enemySaucer[countShips] = new EnemySaucer(new Point(22 + 50*column, 32 + 50*row), -1, this.ClientSize.Width +1, true, rand.Next(30,60));
					//enemySaucer[countShips] = new EnemyShip(22 + 50*column, 32 + 50*row, 50, 25, Image.FromFile("images\\saucer.png"), true, 50, rand.Next(30,60));

					enemySaucer[countShips].directionChange("right"); // set initial direction of ship


					
					for (int countLasers = 0; countLasers < enemySaucer[countShips].laser.Length; countLasers++)
					{
						enemySaucer[countShips].laser[countLasers].setSpeed(6 + 2*((level-1)/3));
						enemySaucer[countShips].laser[countLasers].setActive(false);
						enemySaucer[countShips].laser[countLasers].setTop(enemySaucer[countShips].getTop());
					} // end for

					enemySaucer[countShips].setHitPoints(level);
				} // end for
			}
			else
			{
				targetY = 90;

				for (int countMines = 0; countMines < enemyMine.Length; countMines++)
				{
					enemyMine[countMines] = new EnemyMine(new Point(rand.Next(10, this.Width - enemyMine[countMines].getWidth()), rand.Next(100, 1000) * -1), true);
					enemyMine[countMines].directionChange("down");
				}
				for (int countShips = 0; countShips < enemySaucer.Length ; countShips++)
				{
					enemySaucer[countShips].setHitPoints(level);
					
					for (int countLasers = 0; countLasers < enemySaucer[countShips].laser.Length; countLasers++)
					{
						enemySaucer[countShips].laser[countLasers].setSpeed(6 + 2*((level-1)/3));
						enemySaucer[countShips].laser[countLasers].setActive(false);
						enemySaucer[countShips].laser[countLasers].setTop(enemySaucer[countShips].getTop());
					} // end for

					enemySaucer[countShips] = new EnemySaucer(new Point(rand.Next(50, this.Width - (enemySaucer[countShips].getWidth() + 50)), rand.Next(100, 1000) * -1), 0, this.ClientSize.Width, true, rand.Next(30,60)); 
					//enemySaucer[countShips] = new EnemyShip(rand.Next(50, this.Width - (enemySaucer[countShips].getWidth() + 50)), rand.Next(100, 1000) * -1, 50, 25, Image.FromFile("images\\saucer.png"), true, 50, rand.Next(30,60));
					enemySaucer[countShips].setSpeed(rand.Next(2, 4));
					enemySaucer[countShips].directionChange("down"); // set initial direction of ship
					if (rand.Next(100)%2 == 1)
						enemySaucer[countShips].directionChange("right");
					else
						enemySaucer[countShips].directionChange("left");

					for (int countLasers = 0; countLasers < enemySaucer[countShips].laser.Length; countLasers++)
					{
						enemySaucer[countShips].laser[countLasers].setSpeed(6 + 2*((level-1)/3));
						enemySaucer[countShips].laser[countLasers].setActive(false);
						enemySaucer[countShips].laser[countLasers].setTop(enemySaucer[countShips].getTop());
					} // end for
					enemySaucer[countShips].setHitPoints(level);
				}
			}
		}
	}
}
