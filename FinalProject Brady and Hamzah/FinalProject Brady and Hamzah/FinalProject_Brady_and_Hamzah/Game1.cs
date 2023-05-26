using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProject_Brady_and_Hamzah
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		//menu 
		Menu background1;
		Menu background2;
		Menu background3;
		Menu background4;
		Menu winnerscreen;
		Menu controlsBackground;
		bool menu1 = true;
		bool menu2 = false;
		bool menu3 = false;
		bool menu4 = false;
		bool twoPlayers = false;
		bool threePlayers = false;
		bool fourPlayers = false;
		bool endGame = false;
		bool gameStart = false;
		Texture2D backgroundPic, backgroundPic2, backgroundPic3, backgroundPic4, winnersPic, controlsPic, arrow;
		Rectangle arrowSize;
		SpriteFont menuFont, time;
		string[] menuScreen1 = new string[4];
		string[] menuScreen2 = new string[3];
		string[] menuScreen3 = new string[3];
		string[] menuScreen4 = new string[2];
		string[] winnerScreenTanks = new string[4];
		int[] score = new int[4];
		string menu4Text = "Next";
		string winnerTittle = "Winners!";
		string timeTittle = "Time Remaining ";

		//timer and eliminations per player
		int timer;
		int tick;
		int[] tankKills = new int[4];
		int redTankKills;
		int yellowTankKills;
		int blueTankKills;
		int greenTankKills;

		//arrays and allowing the player to control the options in the menu 
		Vector2 controlMenu;
		Vector2[] menuPostions = new Vector2[4];
		Vector2[] menuPostions2 = new Vector2[2];
		Vector2[] winnerTanks = new Vector2[4];
		Vector2 winnerTop;
		Vector2 timerPos, tittleTimer;
		GamePadState pad1;
		GamePadState oldpad1;

		//tank variables 
		Rectangle Bar;

		//create rectangles array for Maze 
		Rectangle[] MazeHorizontalRec = new Rectangle[10];
		Rectangle[] MazeVerticalRec = new Rectangle[11];

		//second array for collision
		Rectangle[] MazeHorizontalRecBottom = new Rectangle[10];
		Rectangle[] MazeVerticalRecRight = new Rectangle[11];

		//create textures array for Maze background
		Texture2D[] MazeHorizontalTexture = new Texture2D[10];
		Texture2D[] MazeVerticalTexture = new Texture2D[11];

		//creates Texture2D for bar at the bottom
		Texture2D BarPic;

		//create color variable, and set it to Dark Gray
		Color recColor = Color.DarkGray;
		Color recBarColor = Color.LightGray;

		//Movement of tank
		//SPRITE ROTATION
		Texture2D spriteTexture;
		Rectangle spriteRectangle;

		//center of image
		Vector2 spriteOrigin;
		Vector2 spritePosition;

		float rotation;

		Vector2 spriteVelocity;
		const float tangentialVelocity = 2.3f;
		float friction = 0.2f;
		//Movement variables set


		//tank 2
		Texture2D spriteTexture2;
		Rectangle spriteRectangle2;

		Vector2 spriteOrigin2;
		Vector2 spritePosition2;

		float rotation2;

		Vector2 spriteVelocity2;
		const float tangentialVelocity2 = 2.3f;
		float friction2 = 0.2f;


		//tank 3
		Texture2D spriteTexture3;
		Rectangle spriteRectangle3;

		Vector2 spriteOrigin3;
		Vector2 spritePosition3;

		float rotation3;

		Vector2 spriteVelocity3;
		const float tangentialVelocity3 = 2.3f;
		float friction3 = 0.2f;


		//tank 4
		Texture2D spriteTexture4;
		Rectangle spriteRectangle4;

		Vector2 spriteOrigin4;
		Vector2 spritePosition4;

		float rotation4;

		Vector2 spriteVelocity4;
		const float tangentialVelocity4 = 2.3f;
		float friction4 = 0.2f;


		//Portal code
		Rectangle PurplePortal;
		Texture2D PurpleTexture;
		Rectangle PurplePortal2;

		Rectangle BluePortal;
		Rectangle BluePortal2;

		//lives with pic
		Rectangle LiveRectangle;
		Rectangle LiveRectangle2;
		Rectangle LiveRectangle3;
		Rectangle LiveRectangle4;

		//display their kills at the bottom
		Vector2 redKillsPos;
		Vector2 yellowKillsPos;
		Vector2 blueKillsPos;
		Vector2 greenKillsPos;


		//End portal code
		GamePadState pad2;
		GamePadState pad3;
		GamePadState pad4;

		//songs/sound effects
		Song songMenu;
		Song songGame;
		Song songEnd;
		Vector2 soundOPT;
		String soundMenu = "Press B for sound";
		SoundEffect shot;
		SoundEffect plus; 

		//varibles used for playing the pitch,volume, pan for each sound
		float floatPitch = 0;
		float floatVolume = 0f;
		float floatPan = 0;

		double floatPitchNum = 0.0;
		double floatVolumeNum = 0.3;
		double floatPanNum = 0.0;

		//winners screen text
		Vector2 exitGame;
		String exitMessage = "Press Back to exit";

		//Bullets
		Bullets newBullet;

		//Bullets
		Bullets newBullet2;

		//Bullets
		Bullets newBullet3;

		//Bullets
		Bullets newBullet4;

		//Bullets
		List<Bullets> bullets = new List<Bullets>();
		List<Bullets> bullets2 = new List<Bullets>();
		List<Bullets> bullets3 = new List<Bullets>();
		List<Bullets> bullets4 = new List<Bullets>();


		Rectangle[] bulletRec = new Rectangle[4];



		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			//the picture will take up the entire screen 
			Rectangle backgroundRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			Rectangle controlsRec = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 - 70, 300, 300);

			arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 4, 30, 15);
			//load the the pictures for the menu 
			backgroundPic = Content.Load<Texture2D>("background");
			backgroundPic2 = Content.Load<Texture2D>("background2");
			backgroundPic3 = Content.Load<Texture2D>("background3");
			backgroundPic4 = Content.Load<Texture2D>("background4");
			controlsPic = Content.Load<Texture2D>("Xbox+controller");
			winnersPic = Content.Load<Texture2D>("winnerscreen");
			//from the menu it will displyaky these pics 
			background1 = new Menu(backgroundRec, backgroundPic);
			background2 = new Menu(backgroundRec, backgroundPic2);
			background3 = new Menu(backgroundRec, backgroundPic3);
			background4 = new Menu(backgroundRec, backgroundPic4);
			winnerscreen = new Menu(backgroundRec, winnersPic);
			controlsBackground = new Menu(controlsRec, controlsPic);

			//texts that will display and be classified to each array 
			menuScreen1[0] = "Play";
			menuScreen1[1] = "Instructions";
			menuScreen1[2] = "Exit";
			menuScreen1[3] = " Ultimate F4A";

			menuScreen2[0] = "2 players";
			menuScreen2[1] = "3 players";
			menuScreen2[2] = "4 players";

			menuScreen3[0] = "1 minute";
			menuScreen3[1] = "2 minute";
			menuScreen3[2] = "5 minute";

			menuScreen4[0] = " (LS) Move Tanks X and Y coordinates";
			menuScreen4[1] = "(A) Shoot";


			//tank kills reprsent each variable 
			tankKills[0] = redTankKills;
			tankKills[1] = yellowTankKills;
			tankKills[2] = blueTankKills;
			tankKills[3] = greenTankKills;

			winnerScreenTanks[0] = "Red Tank";
			winnerScreenTanks[1] = "Yellow Tank";
			winnerScreenTanks[2] = "Blue Tank";
			winnerScreenTanks[3] = "Green Tank";

			//top wall
			MazeHorizontalRec[0] = new Rectangle(0, 60, 70, 5);
			MazeHorizontalRec[1] = new Rectangle(0, 130, 70, 5);
			MazeHorizontalRec[2] = new Rectangle(0, 300, 160, 5);
			MazeHorizontalRec[3] = new Rectangle(160, 300, 90, 5);
			MazeHorizontalRec[4] = new Rectangle(0, 380, GraphicsDevice.Viewport.Width, 5);
			MazeHorizontalRec[5] = new Rectangle(450, 300, 90, 5);
			MazeHorizontalRec[6] = new Rectangle(450, 100, 90, 5);
			MazeHorizontalRec[7] = new Rectangle(250, 100, 90, 5);
			MazeHorizontalRec[8] = new Rectangle(710, 310, 90, 5);
			MazeHorizontalRec[9] = new Rectangle(630, 75, 75, 5);

			//bottom wall
			MazeHorizontalRecBottom[0] = new Rectangle(0, 65, 70, 3);
			MazeHorizontalRecBottom[1] = new Rectangle(0, 135, 70, 3);
			MazeHorizontalRecBottom[2] = new Rectangle(0, 305, 160, 3);
			MazeHorizontalRecBottom[3] = new Rectangle(160, 305, 90, 3);
			MazeHorizontalRecBottom[5] = new Rectangle(450, 305, 90, 3);
			MazeHorizontalRecBottom[6] = new Rectangle(450, 105, 98, 3);
			MazeHorizontalRecBottom[7] = new Rectangle(250, 105, 90, 3);
			MazeHorizontalRecBottom[8] = new Rectangle(710, 315, 90, 3);
			MazeHorizontalRecBottom[9] = new Rectangle(630, 80, 75, 3);


			//left wall
			MazeVerticalRec[0] = new Rectangle(140, 0, 5, 105);
			MazeVerticalRec[1] = new Rectangle(70, 130, 5, 105);
			MazeVerticalRec[2] = new Rectangle(160, 200, 5, 105);
			MazeVerticalRec[3] = new Rectangle(250, 100, 5, 105);
			MazeVerticalRec[4] = new Rectangle(350, 200, 5, 105);
			MazeVerticalRec[5] = new Rectangle(450, 300, 5, 105);
			MazeVerticalRec[6] = new Rectangle(620, 240, 5, 145);
			MazeVerticalRec[7] = new Rectangle(450, 100, 5, 105);
			MazeVerticalRec[8] = new Rectangle(540, 0, 5, 105);
			MazeVerticalRec[9] = new Rectangle(630, 75, 5, 85);
			MazeVerticalRec[10] = new Rectangle(705, 75, 5, 85);

			//right wall
			MazeVerticalRecRight[0] = new Rectangle(145, 0, 3, 105);
			MazeVerticalRecRight[1] = new Rectangle(75, 130, 3, 105);
			MazeVerticalRecRight[2] = new Rectangle(165, 200, 3, 105);
			MazeVerticalRecRight[3] = new Rectangle(255, 100, 3, 105);
			MazeVerticalRecRight[4] = new Rectangle(355, 200, 3, 105);
			MazeVerticalRecRight[5] = new Rectangle(455, 300, 3, 105);
			MazeVerticalRecRight[6] = new Rectangle(625, 240, 3, 145);
			MazeVerticalRecRight[7] = new Rectangle(455, 100, 3, 105);
			MazeVerticalRecRight[8] = new Rectangle(545, 0, 3, 105);
			MazeVerticalRecRight[9] = new Rectangle(635, 75, 3, 85);
			MazeVerticalRecRight[10] = new Rectangle(710, 75, 3, 85);

			//players menu
			Bar = new Rectangle(0, 385, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

			//portals 
			PurplePortal = new Rectangle(5, 3, 60, 60);
			PurplePortal2 = new Rectangle(740, 320, 60, 60);
			BluePortal = new Rectangle(640, 85, 60, 60);
			BluePortal2 = new Rectangle(5, 315, 60, 60);
			//postion on the tanks 
			LiveRectangle = new Rectangle(50, 410, 40, 40);
			LiveRectangle2 = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 130, 410, 40, 40);
			LiveRectangle3 = new Rectangle(GraphicsDevice.Viewport.Width / 2 + 100, 410, 40, 40);
			LiveRectangle4 = new Rectangle(730, 410, 40, 40);

			//postions of the tanks kills
			redKillsPos = new Vector2(90, 415);
			yellowKillsPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - 170, 415);
			blueKillsPos = new Vector2(GraphicsDevice.Viewport.Width / 2 + 145, 415);
			greenKillsPos = new Vector2(695, 415);

			//making rectangles 
			bulletRec[0] = new Rectangle(90, 415, 10, 10);
			bulletRec[1] = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 170, 415, 10, 10);
			bulletRec[2] = new Rectangle(GraphicsDevice.Viewport.Width / 2 + 145, 415, 10, 10);
			bulletRec[3] = new Rectangle(695, 415, 10, 10);





			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			//font 
			menuFont = Content.Load<SpriteFont>("Menu");
			time = Content.Load<SpriteFont>("timerFont");
			//cursor for the menu 
			arrow = Content.Load<Texture2D>("arrow");
			//songs and sound effects
			songMenu = Content.Load<Song>("Drake");
			songEnd = Content.Load<Song>("sicko");
			songGame = Content.Load<Song>("Intense+Beat");
			shot = Content.Load<SoundEffect>("tankShot");
			plus = Content.Load<SoundEffect>("killcountplus");


			//for loops for loading in vertical walls 
			for (int i = 0; i < 11; i++)
			{
				MazeVerticalTexture[i] = Content.Load<Texture2D>("blackmaze");
			}
			//for loops for loading in horazonal walls walls 
			for (int i = 0; i < 10; i++)
			{
				MazeHorizontalTexture[i] = Content.Load<Texture2D>("blackmaze");
			}
			//bar at the boom to display thescore and the time remaining 
			BarPic = Content.Load<Texture2D>("graymaze");
			PurpleTexture = Content.Load<Texture2D>("PurplePortal");

			// Tank content
			spriteTexture = Content.Load<Texture2D>("tankredSmall");
			spritePosition = new Vector2(20,100);

			// Tank content 2
			spriteTexture2 = Content.Load<Texture2D>("tankgreenSmall");
			spritePosition2 = new Vector2(500, 25);

			// Tank content 3
			spriteTexture3 = Content.Load<Texture2D>("tankblueSmall");
			spritePosition3 = new Vector2(750, 25);

			// Tank content 4
			spriteTexture4 = Content.Load<Texture2D>("tankyellowSmall");
			spritePosition4 = new Vector2(490, 350);

			//Bullet Tex
			Texture2D bulletTex = Content.Load<Texture2D>("bullet");

		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			///code for player one controlling the menu and starting screens
			pad1 = GamePad.GetState(PlayerIndex.One);

			//music for menu 
			if (oldpad1.Buttons.B == ButtonState.Released && pad1.Buttons.B == ButtonState.Pressed)
			{
				MediaPlayer.Play(songMenu);
			}

			if (gameStart)
			{
				MediaPlayer.Pause(); 
			}

			if (endGame == true && timer == 0)
			{
				MediaPlayer.Play(songEnd); 
			}

			//making the volume of sounds more perscise and converting from float to double 
			floatPitch = float.Parse(floatPitchNum.ToString());
			floatVolume = float.Parse(floatVolumeNum.ToString());
			floatPan = float.Parse(floatPanNum.ToString());


			if (menu1 == true)
			{
				OptionSelction1();
			}

			else if (menu2 == true)
			{
				OptionSelction2();
			}

			else if (menu3 == true)
			{
				OptionSelction3();
			}

			if (menu4 == true)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 244, 30, 15);
				if (menu4 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
				{
					arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 4, 30, 15);
					menu4 = false;
					menu1 = true;
				}
			}

			//code for entering the next screen 
			//menu selction for mneu 1
			if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 4 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
			{
				menu1 = false;
				menu2 = true;
			}

			else if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
			{
				menu1 = false;
				menu4 = true;

			}

			else if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
			{
				this.Exit();

			}

			//menu selction for menu 2
			else if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 4 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
			{
				menu2 = false;
				menu3 = true;
				twoPlayers = true;
			}

			else if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
			{
				menu2 = false;
				menu3 = true;
				threePlayers = true;
			}


			else if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
			{
				menu2 = false;
				menu3 = true;
				fourPlayers = true;

			}

			
			//menu selction for menu 3
			else if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 4 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
				{
					menu3 = false;
					timer = 3600;
					gameStart = true;
					
				}

				else if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
				{
					menu3 = false;
					timer = 7200;
					gameStart = true;
					
				}

				else if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.Buttons.A == ButtonState.Released && pad1.Buttons.A == ButtonState.Pressed)
				{
					menu3 = false;
					timer = 18000;
					gameStart = true;
	
				}
			

			//tank game and letting each player play 
			if (gameStart && twoPlayers && timer > 0)
				pad2 = GamePad.GetState(PlayerIndex.Two);
			if (gameStart && threePlayers && timer > 0)
				pad3 = GamePad.GetState(PlayerIndex.Three);
			if (gameStart && fourPlayers && timer > 0)
				pad4 = GamePad.GetState(PlayerIndex.Four);
			if (gameStart)
			{
				//Shoots bullets (shots are commented out because of how long they last but it does work)
				if (Keyboard.GetState().IsKeyDown(Keys.Space) || pad1.Buttons.A == ButtonState.Pressed)
				{
					Shoot();
					//shot.Play(floatVolume, floatPitch, floatPan);
				}

				if (Keyboard.GetState().IsKeyDown(Keys.J) || pad2.Buttons.A == ButtonState.Pressed)
				{
					Shoot2();
					//shot.Play(floatVolume, floatPitch, floatPan);
				}

				if (Keyboard.GetState().IsKeyDown(Keys.M) || pad3.Buttons.A == ButtonState.Pressed)
				{
					Shoot3();
					//shot.Play(floatVolume, floatPitch, floatPan);
				}

				if (Keyboard.GetState().IsKeyDown(Keys.NumPad4) || pad3.Buttons.A == ButtonState.Pressed)
				{
					Shoot4();
					//shot.Play(floatVolume, floatPitch, floatPan);
				}

			}
			UpdateBullets();
			UpdateBullets2();
			UpdateBullets3();
			UpdateBullets4();

			//code for testing if a bullet hits another player and adding to their kill count 
			//two players 
			if (twoPlayers == true)
			{
				for (int i = 0; i < bullets.Count; i++)
				{
					bulletRec[0].X = (int)bullets[i].position.X;
					bulletRec[0].Y = (int)bullets[i].position.Y;

					if (bulletRec[0].Intersects(spriteRectangle2))
					{
						bullets.RemoveAt(i);
						redTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}


				}


				for (int i = 0; i < bullets2.Count; i++)
				{
					bulletRec[1].X = (int)bullets2[i].position.X;
					bulletRec[1].Y = (int)bullets2[i].position.Y;

					if (bulletRec[1].Intersects(spriteRectangle))
					{
						bullets2.RemoveAt(i);
						yellowTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);

					}
				}
			}
			//three players 
			if (threePlayers == true)
			{
				for (int i = 0; i < bullets.Count; i++)
				{
					bulletRec[0].X = (int)bullets[i].position.X;
					bulletRec[0].Y = (int)bullets[i].position.Y;

					if (bulletRec[0].Intersects(spriteRectangle2) || bulletRec[0].Intersects(spriteRectangle3))
					{
						bullets.RemoveAt(i);
						plus.Play(floatVolume, floatPitch, floatPan);
						redTankKills++;
					}


				}


				for (int i = 0; i < bullets2.Count; i++)
				{
					bulletRec[1].X = (int)bullets2[i].position.X;
					bulletRec[1].Y = (int)bullets2[i].position.Y;

					if (bulletRec[1].Intersects(spriteRectangle) || bulletRec[1].Intersects(spriteRectangle3))
					{
						bullets2.RemoveAt(i);
						yellowTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}
				}

				for (int i = 0; i < bullets3.Count; i++)
				{
					bulletRec[2].X = (int)bullets3[i].position.X;
					bulletRec[2].Y = (int)bullets3[i].position.Y;

					if (bulletRec[2].Intersects(spriteRectangle) || bulletRec[2].Intersects(spriteRectangle2))
					{
						bullets3.RemoveAt(i);
						greenTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}
				}

			}
			//four players 
			if (fourPlayers == true)
			{
				for (int i = 0; i < bullets.Count; i++)
				{
					bulletRec[0].X = (int)bullets[i].position.X;
					bulletRec[0].Y = (int)bullets[i].position.Y;

					if (bulletRec[0].Intersects(spriteRectangle2) || bulletRec[0].Intersects(spriteRectangle3) || bulletRec[0].Intersects(spriteRectangle4))
					{
						bullets.RemoveAt(i);
						redTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}


				}

				for (int i = 0; i < bullets2.Count; i++)
				{
					bulletRec[1].X = (int)bullets2[i].position.X;
					bulletRec[1].Y = (int)bullets2[i].position.Y;

					if (bulletRec[1].Intersects(spriteRectangle) || bulletRec[1].Intersects(spriteRectangle3) || bulletRec[1].Intersects(spriteRectangle4))
					{
						bullets2.RemoveAt(i);
						yellowTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}
				}

				for (int i = 0; i < bullets3.Count; i++)
				{
					bulletRec[2].X = (int)bullets2[i].position.X;
					bulletRec[2].Y = (int)bullets2[i].position.Y;

					if (bulletRec[2].Intersects(spriteRectangle) || bulletRec[2].Intersects(spriteRectangle2) || bulletRec[0].Intersects(spriteRectangle4))
					{
						bullets3.RemoveAt(i);
						greenTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}
				}

				for (int i = 0; i < bullets4.Count; i++)
				{
					bulletRec[3].X = (int)bullets4[i].position.X;
					bulletRec[3].Y = (int)bullets4[i].position.Y;

					if (bulletRec[2].Intersects(spriteRectangle) || bulletRec[2].Intersects(spriteRectangle2) || bulletRec[0].Intersects(spriteRectangle3))
					{
						bullets3.RemoveAt(i);
						blueTankKills++;
						plus.Play(floatVolume, floatPitch, floatPan);
					}

					

				}

			}
			//allowing the bullet to stop and be removed at the location it hit any of these walls 
			//tank 1
			for (int i = 0; i < bullets.Count; i++)
			{
				if (bulletRec[0].Y > 385)
				{
					bullets.RemoveAt(i); 
				}


				for (int j = 0; j < MazeVerticalRec.Length; j++)
				{
					if (bulletRec[0].Intersects(MazeVerticalRec[j]))
					{
						bullets.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeVerticalRecRight.Length; j++)
				{
					if (bulletRec[0].Intersects(MazeVerticalRecRight[j]))
					{
						bullets.RemoveAt(i);
					}
				}
			}

			for (int i = 0; i < bullets.Count; i++)
			{

				for (int j = 0; j < MazeHorizontalRec.Length ; j++)
				{
					if (bulletRec[0].Intersects(MazeHorizontalRec[j]))
					{
						bullets.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeHorizontalRecBottom.Length; j++)
				{
					if (bulletRec[0].Intersects(MazeHorizontalRecBottom[j]))
					{
						bullets.RemoveAt(i);
					}
				}




			}

			//tank 2
			for (int i = 0; i < bullets2.Count; i++)
			{
				if (bulletRec[1].Y > 385)
				{
					bullets2.RemoveAt(i);
				}


				for (int j = 0; j < MazeVerticalRec.Length; j++)
				{
					if (bulletRec[1].Intersects(MazeVerticalRec[j]))
					{
						bullets2.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeVerticalRecRight.Length; j++)
				{
					if (bulletRec[1].Intersects(MazeVerticalRecRight[j]))
					{
						bullets2.RemoveAt(i);
					}
				}
			}

			for (int i = 0; i < bullets2.Count; i++)
			{

				for (int j = 0; j < MazeHorizontalRec.Length; j++)
				{
					if (bulletRec[1].Intersects(MazeHorizontalRec[j]))
					{
						bullets2.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeHorizontalRecBottom.Length; j++)
				{
					if (bulletRec[1].Intersects(MazeHorizontalRecBottom[j]))
					{
						bullets2.RemoveAt(i);
					}
				}
			}

			//tank 3
			for (int i = 0; i < bullets3.Count; i++)
			{
				if (bulletRec[2].Y > 385)
				{
					bullets3.RemoveAt(i);
				}


				for (int j = 0; j < MazeVerticalRec.Length; j++)
				{
					if (bulletRec[2].Intersects(MazeVerticalRec[j]))
					{
						bullets3.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeVerticalRecRight.Length; j++)
				{
					if (bulletRec[2].Intersects(MazeVerticalRecRight[j]))
					{
						bullets3.RemoveAt(i);
					}
				}
			}

			for (int i = 0; i < bullets3.Count; i++)
			{

				for (int j = 0; j < MazeHorizontalRec.Length; j++)
				{
					if (bulletRec[2].Intersects(MazeHorizontalRec[j]))
					{
						bullets3.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeHorizontalRecBottom.Length; j++)
				{
					if (bulletRec[2].Intersects(MazeHorizontalRecBottom[j]))
					{
						bullets3.RemoveAt(i);
					}
				}
			}

			//tank 4
			for (int i = 0; i < bullets4.Count; i++)
			{
				if (bulletRec[3].Y > 385)
				{
					bullets4.RemoveAt(i);
				}


				for (int j = 0; j < MazeVerticalRec.Length; j++)
				{
					if (bulletRec[3].Intersects(MazeVerticalRec[j]))
					{
						bullets4.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeVerticalRecRight.Length; j++)
				{
					if (bulletRec[3].Intersects(MazeVerticalRecRight[j]))
					{
						bullets4.RemoveAt(i);
					}
				}
			}

			for (int i = 0; i < bullets4.Count; i++)
			{

				for (int j = 0; j < MazeHorizontalRec.Length; j++)
				{
					if (bulletRec[3].Intersects(MazeHorizontalRec[j]))
					{
						bullets4.RemoveAt(i);
					}
				}

				for (int j = 0; j < MazeHorizontalRecBottom.Length; j++)
				{
					if (bulletRec[3].Intersects(MazeHorizontalRecBottom[j]))
					{
						bullets4.RemoveAt(i);
					}
				}
			}

			//Tank code to allow it to move 
			spriteRectangle = new Rectangle((int)spritePosition.X - 18, (int)spritePosition.Y - 18, spriteTexture.Width, spriteTexture.Height);

			spritePosition = spriteVelocity + spritePosition;

			spriteOrigin = new Vector2(spriteRectangle.Width / 2, spriteRectangle.Height / 2);

			if (Keyboard.GetState().IsKeyDown(Keys.Right) || pad1.ThumbSticks.Left.X > 0)
			{
				rotation += 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Left) || pad1.ThumbSticks.Left.X < 0)
			{
				rotation -= 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Up) || pad1.Triggers.Right > 0)
			{
				spriteVelocity.X = (float)Math.Cos(rotation + 1.6) * tangentialVelocity;
				spriteVelocity.Y = (float)Math.Sin(rotation + 1.6) * tangentialVelocity;
			}
			else if (spriteVelocity != Vector2.Zero)
			{
				Vector2 i = spriteVelocity;
				spriteVelocity = i -= friction * i;
			}
			//Tank code finished

			//Tank code2
			spriteRectangle2 = new Rectangle((int)spritePosition2.X - 18, (int)spritePosition2.Y - 18, spriteTexture2.Width, spriteTexture2.Height);

			spritePosition2 = spriteVelocity2 + spritePosition2;

			spriteOrigin2 = new Vector2(spriteRectangle2.Width / 2, spriteRectangle2.Height / 2);

			if (Keyboard.GetState().IsKeyDown(Keys.D) || pad2.ThumbSticks.Left.X > 0)
			{
				rotation2 += 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.A) || pad2.ThumbSticks.Left.X < 0)
			{
				rotation2 -= 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.W) || pad2.Triggers.Right > 0)
			{
				spriteVelocity2.X = (float)Math.Cos(rotation2 + 1.6) * tangentialVelocity2;
				spriteVelocity2.Y = (float)Math.Sin(rotation2 + 1.6) * tangentialVelocity2;
			}
			else if (spriteVelocity2 != Vector2.Zero)
			{
				Vector2 i = spriteVelocity2;
				spriteVelocity2 = i -= friction2 * i;
			}
			//Tank2 code finished

			//Tank code3
			spriteRectangle3 = new Rectangle((int)spritePosition3.X - 18, (int)spritePosition3.Y - 18, spriteTexture3.Width, spriteTexture3.Height);

			spritePosition3 = spriteVelocity3 + spritePosition3;

			spriteOrigin3 = new Vector2(spriteRectangle3.Width / 2, spriteRectangle3.Height / 2);

			if (Keyboard.GetState().IsKeyDown(Keys.K) || pad3.ThumbSticks.Left.X > 0)
			{
				rotation3 += 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.H) || pad3.ThumbSticks.Left.X < 0)
			{
				rotation3 -= 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.U) || pad3.Triggers.Right > 0)
			{
				spriteVelocity3.X = (float)Math.Cos(rotation3 + 1.6) * tangentialVelocity3;
				spriteVelocity3.Y = (float)Math.Sin(rotation3 + 1.6) * tangentialVelocity3;
			}
			else if (spriteVelocity3 != Vector2.Zero)
			{
				Vector2 i = spriteVelocity3;
				spriteVelocity3 = i -= friction3 * i;
			}

			//Tank3 code finished

			//Tank code4
			spriteRectangle4 = new Rectangle((int)spritePosition4.X - 18, (int)spritePosition4.Y - 18, spriteTexture4.Width, spriteTexture4.Height);

			spritePosition4 = spriteVelocity4 + spritePosition4;

			spriteOrigin4 = new Vector2(spriteRectangle4.Width / 2, spriteRectangle4.Height / 2);

			if (Keyboard.GetState().IsKeyDown(Keys.NumPad3) || pad4.ThumbSticks.Left.X > 0)
			{
				rotation4 += 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) || pad4.ThumbSticks.Left.X > 0)
			{
				rotation4 -= 0.1f;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.NumPad5) || pad4.Triggers.Right > 0)
			{
				spriteVelocity4.X = (float)Math.Cos(rotation4 + 1.6) * tangentialVelocity4;
				spriteVelocity4.Y = (float)Math.Sin(rotation4 + 1.6) * tangentialVelocity4;
			}
			else if (spriteVelocity4 != Vector2.Zero)
			{
				Vector2 i = spriteVelocity4;
				spriteVelocity4 = i -= friction4 * i;
			}

			//Tank code finished

			//bind red tank1 to inside of maze
			if (spritePosition.X > GraphicsDevice.Viewport.Width - 12)
			{
				spritePosition.X -= 3;
			}

			if (spritePosition.X < 12)
			{
				spritePosition.X += 3;
			}

			if (spritePosition.Y < 12)
			{
				spritePosition.Y += 3;
			}
			//bind tank2 to inside of maze
			if (spritePosition2.X > GraphicsDevice.Viewport.Width - 12)
			{
				spritePosition2.X -= 3;
			}

			if (spritePosition2.X < 12)
			{
				spritePosition2.X += 3;
			}

			if (spritePosition2.Y < 12)
			{
				spritePosition2.Y += 3;
			}
			//bind tank3 to inside of maze
			if (spritePosition3.X > GraphicsDevice.Viewport.Width - 12)
			{
				spritePosition3.X -= 3;
			}

			if (spritePosition3.X < 12)
			{
				spritePosition3.X += 3;
			}

			if (spritePosition3.Y < 12)
			{
				spritePosition3.Y += 3;
			}

			//bind tank4 to inside of maze
			if (spritePosition4.X > GraphicsDevice.Viewport.Width - 12)
			{
				spritePosition4.X -= 3;
			}

			if (spritePosition4.X < 12)
			{
				spritePosition4.X += 3;
			}

			if (spritePosition4.Y < 12)
			{
				spritePosition4.Y += 3;
			}
			//Portal code Black TANK 1
			if (spriteRectangle.Intersects(PurplePortal))
			{
				spritePosition.Y = PurplePortal2.Y + 30;
				spritePosition.X = PurplePortal2.X - 20;
			}

			if (spriteRectangle.Intersects(PurplePortal2))
			{
				spritePosition.Y = PurplePortal.Y + 25;
				spritePosition.X = PurplePortal.X + 80;
			}

			//Blue portal code
			if (spriteRectangle.Intersects(BluePortal))
			{
				spritePosition.Y = BluePortal2.Y + 30;
				spritePosition.X = BluePortal2.X + 80;
			}

			if (spriteRectangle.Intersects(BluePortal2))
			{
				spritePosition.Y = BluePortal.Y + 80;
				spritePosition.X = BluePortal.X + 35;
			}


			//Portal code Black TANK 2
			if (spriteRectangle2.Intersects(PurplePortal))
			{
				spritePosition2.Y = PurplePortal2.Y + 30;
				spritePosition2.X = PurplePortal2.X - 20;
			}

			if (spriteRectangle2.Intersects(PurplePortal2))
			{
				spritePosition2.Y = PurplePortal.Y + 25;
				spritePosition2.X = PurplePortal.X + 80;
			}

			//Blue portal code
			if (spriteRectangle2.Intersects(BluePortal))
			{
				spritePosition2.Y = BluePortal2.Y + 30;
				spritePosition2.X = BluePortal2.X + 80;
			}

			if (spriteRectangle2.Intersects(BluePortal2))
			{
				spritePosition2.Y = BluePortal.Y + 80;
				spritePosition2.X = BluePortal.X + 35;
			}

			//Portal code Black TANK 3
			if (spriteRectangle3.Intersects(PurplePortal))
			{
				spritePosition3.Y = PurplePortal2.Y + 30;
				spritePosition3.X = PurplePortal2.X - 20;
			}

			if (spriteRectangle3.Intersects(PurplePortal2))
			{
				spritePosition3.Y = PurplePortal.Y + 25;
				spritePosition3.X = PurplePortal.X + 80;
			}

			//Blue portal code
			if (spriteRectangle3.Intersects(BluePortal))
			{
				spritePosition3.Y = BluePortal2.Y + 30;
				spritePosition3.X = BluePortal2.X + 80;
			}

			if (spriteRectangle3.Intersects(BluePortal2))
			{
				spritePosition3.Y = BluePortal.Y + 80;
				spritePosition3.X = BluePortal.X + 35;
			}

			//Portal code Black TANK 4
			if (spriteRectangle4.Intersects(PurplePortal))
			{
				spritePosition4.Y = PurplePortal2.Y + 30;
				spritePosition4.X = PurplePortal2.X - 20;
			}

			if (spriteRectangle4.Intersects(PurplePortal2))
			{
				spritePosition4.Y = PurplePortal.Y + 25;
				spritePosition4.X = PurplePortal.X + 80;
			}

			//Blue portal code
			if (spriteRectangle4.Intersects(BluePortal))
			{
				spritePosition4.Y = BluePortal2.Y + 30;
				spritePosition4.X = BluePortal2.X + 80;
			}

			if (spriteRectangle4.Intersects(BluePortal2))
			{
				spritePosition4.Y = BluePortal.Y + 80;
				spritePosition4.X = BluePortal.X + 35;
			}

			//collsion horizontal
			for (int i = 0; i < 10; i++)
			{
				if (spriteRectangle.Intersects(MazeHorizontalRec[i]))
				{

					spritePosition.Y -= 3;

				}

				if (spriteRectangle.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition.Y += 3;
				}

				if (spriteRectangle.Intersects(MazeHorizontalRec[i]) && spriteRectangle.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition.Y += 5;

				}
			}

			//collision vertical
			for (int i = 0; i < 11; i++)
			{
				if (spriteRectangle.Intersects(MazeVerticalRec[i]))
				{
					spritePosition.X -= 3;
				}

				if (spriteRectangle.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition.X += 3;
				}

				if (spriteRectangle.Intersects(MazeVerticalRec[i]) && spriteRectangle.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition.X += 5;
				}

			}
			//tank2 collision
			for (int i = 0; i < 10; i++)
			{
				if (spriteRectangle2.Intersects(MazeHorizontalRec[i]))
				{

					spritePosition2.Y -= 3;

				}

				if (spriteRectangle2.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition2.Y += 3;

				}

				if (spriteRectangle2.Intersects(MazeHorizontalRec[i]) && spriteRectangle2.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition2.Y += 5;

				}
			}

			//collision vertical TANK 2
			for (int i = 0; i < 11; i++)
			{
				if (spriteRectangle2.Intersects(MazeVerticalRec[i]))
				{
					spritePosition2.X -= 3;
				}

				if (spriteRectangle2.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition2.X += 3;
				}

				if (spriteRectangle2.Intersects(MazeVerticalRec[i]) && spriteRectangle2.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition2.X += 5;
				}
			}
			//tank3 collision
			for (int i = 0; i < 10; i++)
			{
				if (spriteRectangle3.Intersects(MazeHorizontalRec[i]))
				{

					spritePosition3.Y -= 3;

				}

				if (spriteRectangle3.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition3.Y += 3;

				}

				if (spriteRectangle3.Intersects(MazeHorizontalRec[i]) && spriteRectangle3.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition3.Y += 5;

				}
			}
			//collision vertical TANK 3
			for (int i = 0; i < 11; i++)
			{
				if (spriteRectangle3.Intersects(MazeVerticalRec[i]))
				{
					spritePosition3.X -= 3;
				}

				if (spriteRectangle3.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition3.X += 3;
				}

				if (spriteRectangle3.Intersects(MazeVerticalRec[i]) && spriteRectangle3.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition3.X += 5;
				}
			}

			//collsion horizontal Tank4
			for (int i = 0; i < 10; i++)
			{
				if (spriteRectangle4.Intersects(MazeHorizontalRec[i]))
				{

					spritePosition4.Y -= 3;

				}

				if (spriteRectangle4.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition4.Y += 3;

				}

				if (spriteRectangle4.Intersects(MazeHorizontalRec[i]) && spriteRectangle4.Intersects(MazeHorizontalRecBottom[i]))
				{

					spritePosition4.Y += 5;

				}
			}

			//collision vertical tank4
			for (int i = 0; i < 11; i++)
			{
				if (spriteRectangle4.Intersects(MazeVerticalRec[i]))
				{
					spritePosition4.X -= 3;
				}

				if (spriteRectangle4.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition4.X += 3;
				}

				if (spriteRectangle4.Intersects(MazeVerticalRec[i]) && spriteRectangle4.Intersects(MazeVerticalRecRight[i]))
				{
					spritePosition4.X += 5;
				}
			}
			timer--;
			oldpad1 = pad1;
			//allowing the timer remaining to be more realalistic 
			tick = timer / 60;

			if (timer == 0)
			{
				endGame = true;
			}

			//allowing the player to exit in the winners screen 
			if (endGame == true && oldpad1.Buttons.Back == ButtonState.Released && pad1.Buttons.Back == ButtonState.Pressed)
			{
				MediaPlayer.Pause(); 
				this.Exit();
			}


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			//poistions for the texts in the menu 
			menuPostions[0] = new Vector2(GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height / 4);
			menuPostions[1] = new Vector2(GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height / 2);
			menuPostions[2] = new Vector2(GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height - 120);
			menuPostions[3] = new Vector2(250, 50);
			//postions for the timer 
			timerPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - 20, 410);
			tittleTimer = new Vector2(GraphicsDevice.Viewport.Width / 2 - 80, 385);
			//menu postions for the instrution menu 
			menuPostions2[0] = new Vector2(GraphicsDevice.Viewport.Width / 3 - 270, GraphicsDevice.Viewport.Height / 4 - 50);
			menuPostions2[1] = new Vector2(GraphicsDevice.Viewport.Width / 3 + 280, GraphicsDevice.Viewport.Height / 2 - 85);
			winnerTop = new Vector2(GraphicsDevice.Viewport.Width / 2 - 70, 30);
			controlMenu = new Vector2(GraphicsDevice.Viewport.Width / 3 + 95, GraphicsDevice.Viewport.Height - 120);

			//displaying the tanks and how many kills they had 
			winnerTanks[1] = new Vector2(550, 210);
			winnerTanks[3] = new Vector2(550, 250);
			winnerTanks[2] = new Vector2(550, 295);
			winnerTanks[0] = new Vector2(550, 340);

			//guide messages 
			soundOPT = new Vector2(600, 5);
			exitGame = new Vector2(580, 450);
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			Display();

			if (gameStart == true)
			{
				gameMap();
				//timer displayed at the bottom 
				spriteBatch.DrawString(time, tick.ToString(), timerPos, Color.White);
				spriteBatch.DrawString(menuFont, timeTittle, tittleTimer, Color.White);
				//bullets
				foreach (Bullets bullet in bullets)
					bullet.Draw(spriteBatch);

				foreach (Bullets bullet2 in bullets2)
					bullet2.Draw(spriteBatch);

				foreach (Bullets bullet3 in bullets3)
					bullet3.Draw(spriteBatch);

				foreach (Bullets bullet4 in bullets4)
					bullet4.Draw(spriteBatch);





				if (twoPlayers == true)
				{
					//tank1
					spriteBatch.Draw(spriteTexture, spritePosition, null, Color.White, rotation, spriteOrigin, 1f, SpriteEffects.None, 0);
					//tank2
					spriteBatch.Draw(spriteTexture2, spritePosition2, null, Color.White, rotation2, spriteOrigin2, 1f, SpriteEffects.None, 0);
					//scores at the bottom
					spriteBatch.Draw(spriteTexture, LiveRectangle, recBarColor);
					spriteBatch.Draw(spriteTexture2, LiveRectangle2, recBarColor);
					spriteBatch.DrawString(menuFont, "x " + redTankKills.ToString(), redKillsPos, Color.Black);
					spriteBatch.DrawString(menuFont, "x " + yellowTankKills.ToString(), yellowKillsPos, Color.Black);
				}

				if (threePlayers == true)
				{
					//tank1
					spriteBatch.Draw(spriteTexture, spritePosition, null, Color.White, rotation, spriteOrigin, 1f, SpriteEffects.None, 0);
					//tank2
					spriteBatch.Draw(spriteTexture2, spritePosition2, null, Color.White, rotation2, spriteOrigin2, 1f, SpriteEffects.None, 0);
					//tank3
					spriteBatch.Draw(spriteTexture3, spritePosition3, null, Color.White, rotation3, spriteOrigin3, 1f, SpriteEffects.None, 0);
					//scores at the bottom
					spriteBatch.Draw(spriteTexture, LiveRectangle, recBarColor);
					spriteBatch.Draw(spriteTexture2, LiveRectangle2, recBarColor);
					spriteBatch.Draw(spriteTexture3, LiveRectangle3, recBarColor);
					spriteBatch.DrawString(menuFont, "x " + redTankKills.ToString(), redKillsPos, Color.Black);
					spriteBatch.DrawString(menuFont, "x " + yellowTankKills.ToString(), yellowKillsPos, Color.Black);
					spriteBatch.DrawString(menuFont, "x " + blueTankKills.ToString(), blueKillsPos, Color.Black);
				}

				if (fourPlayers == true)
				{
					//tank1
					spriteBatch.Draw(spriteTexture, spritePosition, null, Color.White, rotation, spriteOrigin, 1f, SpriteEffects.None, 0);
					//tank2
					spriteBatch.Draw(spriteTexture2, spritePosition2, null, Color.White, rotation2, spriteOrigin2, 1f, SpriteEffects.None, 0);
					//tank3
					spriteBatch.Draw(spriteTexture3, spritePosition3, null, Color.White, rotation3, spriteOrigin3, 1f, SpriteEffects.None, 0);
					//tank4
					spriteBatch.Draw(spriteTexture4, spritePosition4, null, Color.White, rotation4, spriteOrigin4, 1f, SpriteEffects.None, 0);
					//scores at the bottom 
					spriteBatch.Draw(spriteTexture, LiveRectangle, recBarColor);
					spriteBatch.Draw(spriteTexture2, LiveRectangle2, recBarColor);
					spriteBatch.Draw(spriteTexture3, LiveRectangle3, recBarColor);
					spriteBatch.Draw(spriteTexture4, LiveRectangle4, recBarColor);
					spriteBatch.DrawString(menuFont, "x " + redTankKills.ToString(), redKillsPos, Color.Black);
					spriteBatch.DrawString(menuFont, "x " + yellowTankKills.ToString(), yellowKillsPos, Color.Black);
					spriteBatch.DrawString(menuFont, "x " + greenTankKills.ToString(), greenKillsPos, Color.Black);
					spriteBatch.DrawString(menuFont, "x " + blueTankKills.ToString(), blueKillsPos, Color.Black);
				}
			}
			//winners screen display
			if (endGame == true)
			{
				winnerscreen.DrawMethod(spriteBatch);
				spriteBatch.DrawString(menuFont, exitMessage, exitGame, Color.White);
				spriteBatch.DrawString(menuFont, winnerTittle, winnerTop, Color.Black);
				spriteBatch.DrawString(menuFont, winnerScreenTanks[0] + " : " + redTankKills, winnerTanks[0], Color.Red);
				spriteBatch.DrawString(menuFont, winnerScreenTanks[3] + " : " + yellowTankKills, winnerTanks[1], Color.Green);
				spriteBatch.DrawString(menuFont, winnerScreenTanks[2] + " : " + blueTankKills, winnerTanks[2], Color.Blue);
				spriteBatch.DrawString(menuFont, winnerScreenTanks[1] + " : " + greenTankKills, winnerTanks[3], Color.Yellow);



			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
		//for the selection option in the first menu 
		public void OptionSelction1()
		{
			if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 4 && oldpad1.DPad.Down == ButtonState.Released && pad1.DPad.Down == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 124, 30, 15);
			}

			else if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.DPad.Down == ButtonState.Released && pad1.DPad.Down == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 244, 30, 15);
			}

			else if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.DPad.Up == ButtonState.Released && pad1.DPad.Up == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 4, 30, 15);
			}

			else if (menu1 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.DPad.Up == ButtonState.Released && pad1.DPad.Up == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 124, 30, 15);
			}
		}
		//for the selection option in the second menu 
		public void OptionSelction2()
		{
			if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 4 && oldpad1.DPad.Down == ButtonState.Released && pad1.DPad.Down == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 124, 30, 15);
			}

			else if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.DPad.Down == ButtonState.Released && pad1.DPad.Down == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 244, 30, 15);
			}

			else if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.DPad.Up == ButtonState.Released && pad1.DPad.Up == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 4, 30, 15);
			}

			else if (menu2 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.DPad.Up == ButtonState.Released && pad1.DPad.Up == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 124, 30, 15);
			}
		}
		//for the selection option in the third menu 
		public void OptionSelction3()
		{
			if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 4 && oldpad1.DPad.Down == ButtonState.Released && pad1.DPad.Down == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 124, 30, 15);
			}

			else if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.DPad.Down == ButtonState.Released && pad1.DPad.Down == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 244, 30, 15);
			}

			else if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 124 && oldpad1.DPad.Up == ButtonState.Released && pad1.DPad.Up == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 4, 30, 15);
			}

			else if (menu3 == true && arrowSize.Y == GraphicsDevice.Viewport.Height / 4 + 244 && oldpad1.DPad.Up == ButtonState.Released && pad1.DPad.Up == ButtonState.Pressed)
			{
				arrowSize = new Rectangle(GraphicsDevice.Viewport.Width / 3 - 30, GraphicsDevice.Viewport.Height / 4 + 124, 30, 15);
			}
		}
		//method that will display the entire menu called in the spritebatch 
		public void Display()
		{
			if (menu1 == true)
			{

				background1.DrawMethod(spriteBatch);
				spriteBatch.DrawString(menuFont, soundMenu, soundOPT, Color.Black);
				spriteBatch.Draw(arrow, arrowSize, Color.White);
				for (int i = 0; i < menuPostions.Length; i++)
				{
					spriteBatch.DrawString(menuFont, menuScreen1[i], menuPostions[i], Color.White);

				}

			}

			if (menu2 == true)
			{
				background2.DrawMethod(spriteBatch);
				spriteBatch.Draw(arrow, arrowSize, Color.White);
				for (int i = 0; i < menuPostions.Length - 1; i++)
				{
					spriteBatch.DrawString(menuFont, menuScreen2[i], menuPostions[i], Color.White);

				}
			}

			if (menu3 == true)
			{
				background3.DrawMethod(spriteBatch);
				spriteBatch.Draw(arrow, arrowSize, Color.White);
				for (int i = 0; i < menuPostions.Length - 1; i++)
				{
					spriteBatch.DrawString(menuFont, menuScreen3[i], menuPostions[i], Color.White);
				}
			}

			if (menu4 == true)
			{
				background4.DrawMethod(spriteBatch);
				controlsBackground.DrawMethod(spriteBatch);
				spriteBatch.Draw(arrow, arrowSize, Color.White);
				spriteBatch.DrawString(menuFont, menu4Text, menuPostions[2], Color.White);
				for (int i = 0; i < menuPostions2.Length; i++)
				{
					spriteBatch.DrawString(menuFont, menuScreen4[i], menuPostions2[i], Color.Black);
				}

			}

		}

		public void gameMap()
		{
			for (int i = 0; i < 10; i++)
			{
				spriteBatch.Draw(MazeHorizontalTexture[i], MazeHorizontalRec[i], recColor);
				spriteBatch.Draw(MazeHorizontalTexture[i], MazeHorizontalRecBottom[i], recColor);
			}

			for (int i = 0; i < 11; i++)
			{
				spriteBatch.Draw(MazeVerticalTexture[i], MazeVerticalRec[i], recColor);
				spriteBatch.Draw(MazeVerticalTexture[i], MazeVerticalRecRight[i], recColor);
			}

			//Draw bar at bottom
			spriteBatch.Draw(BarPic, Bar, recBarColor);

			//Draw portal
			spriteBatch.Draw(PurpleTexture, PurplePortal, Color.Black);
			spriteBatch.Draw(PurpleTexture, PurplePortal2, Color.Black);
			spriteBatch.Draw(PurpleTexture, BluePortal, Color.Blue);
			spriteBatch.Draw(PurpleTexture, BluePortal2, Color.Blue);
		}

		public void UpdateBullets()
		{
			//Go through each bullet in the list 
			foreach (Bullets bullet in bullets)
			{
				//creates hit box for bullet
				bullet.bulletRec = new Rectangle((int)bullet.position.X, (int)bullet.position.Y, bullet.texture.Width, bullet.texture.Height);
				bullet.position += bullet.velocity; //sets bullet position

				//bullet placement 
				if (Vector2.Distance(bullet.position, spritePosition) > 500)
					bullet.isVisible = false;
			}

			//removes bullets 
			for (int i = 0; i < bullets.Count; i++)
			{
				if (!bullets[i].isVisible)
				{
					bullets.RemoveAt(i);
					i--;
				}
			}
		}

		public void Shoot()
		{
			//loads bullet img
			Texture2D bulletTex = Content.Load<Texture2D>("bullet");
			Vector2 adjustment = new Vector2(7); //used to adjust bullet placement to center of the ship
			newBullet = new Bullets(bulletTex); //creates newBullets
												//bullet speed
			newBullet.velocity = new Vector2((float)Math.Cos(rotation + 1.6), (float)Math.Sin(rotation + 1.6)) * 3f; //+ tangentialVelocity;

			//bullet placement
			newBullet.position = spritePosition - adjustment + newBullet.velocity * 5;

			newBullet.isVisible = true;

			//Adds bullets
			if (bullets.Count() < 1)
			{
				bullets.Add(newBullet);
			}
		}








		public void UpdateBullets2()
		{
			//Go through each bullet in the list 
			foreach (Bullets bullet2 in bullets2)
			{
				//creates hit box for bullet
				bullet2.bulletRec = new Rectangle((int)bullet2.position.X, (int)bullet2.position.Y, bullet2.texture.Width, bullet2.texture.Height);
				bullet2.position += bullet2.velocity; //sets bullet position

				//bullet placement 
				if (Vector2.Distance(bullet2.position, spritePosition2) > 500)
					bullet2.isVisible = false;
			}

			//removes bullets 
			for (int i = 0; i < bullets2.Count; i++)
			{
				if (!bullets2[i].isVisible)
				{
					bullets2.RemoveAt(i);
					i--;
				}
			}
		}

		public void Shoot2()
		{
			//loads bullet img
			Texture2D bulletTex = Content.Load<Texture2D>("bullet");
			Vector2 adjustment = new Vector2(7); //used to adjust bullet placement to center of the ship
			newBullet = new Bullets(bulletTex); //creates newBullets
												//bullet speed
			newBullet.velocity = new Vector2((float)Math.Cos(rotation2 + 1.6), (float)Math.Sin(rotation2 + 1.6)) * 3f; //+ tangentialVelocity;

			//bullet placement
			newBullet.position = spritePosition2 - adjustment + newBullet.velocity * 5;

			newBullet.isVisible = true;

			//Adds bullets
			if (bullets2.Count() < 1)
			{
				bullets2.Add(newBullet);
			}
		}









		public void UpdateBullets3()
		{
			//Go through each bullet in the list 
			foreach (Bullets bullet3 in bullets3)
			{
				//creates hit box for bullet
				bullet3.bulletRec = new Rectangle((int)bullet3.position.X, (int)bullet3.position.Y, bullet3.texture.Width, bullet3.texture.Height);
				bullet3.position += bullet3.velocity; //sets bullet position

				//bullet placement 
				if (Vector2.Distance(bullet3.position, spritePosition3) > 500)
					bullet3.isVisible = false;
			}

			//removes bullets 
			for (int i = 0; i < bullets3.Count; i++)
			{
				if (!bullets3[i].isVisible)
				{
					bullets3.RemoveAt(i);
					i--;
				}
			}
		}

		public void Shoot3()
		{
			//loads bullet img
			Texture2D bulletTex = Content.Load<Texture2D>("bullet");
			Vector2 adjustment = new Vector2(7); //used to adjust bullet placement to center of the ship
			newBullet = new Bullets(bulletTex); //creates newBullets
												//bullet speed
			newBullet.velocity = new Vector2((float)Math.Cos(rotation3 + 1.6), (float)Math.Sin(rotation3 + 1.6)) * 3f; //+ tangentialVelocity;

			//bullet placement
			newBullet.position = spritePosition3 - adjustment + newBullet.velocity * 5;

			newBullet.isVisible = true;

			//Adds bullets
			if (bullets3.Count() < 1)
			{
				bullets3.Add(newBullet);
			}
		}


		public void UpdateBullets4()
		{
			//Go through each bullet in the list 
			foreach (Bullets bullet4 in bullets4)
			{
				//creates hit box for bullet
				bullet4.bulletRec = new Rectangle((int)bullet4.position.X, (int)bullet4.position.Y, bullet4.texture.Width, bullet4.texture.Height);
				bullet4.position += bullet4.velocity; //sets bullet position

				//bullet placement 
				if (Vector2.Distance(bullet4.position, spritePosition4) > 500)
					bullet4.isVisible = false;
			}

			//removes bullets 
			for (int i = 0; i < bullets4.Count; i++)
			{
				if (!bullets4[i].isVisible)
				{
					bullets4.RemoveAt(i);
					i--;
				}
			}
		}

		public void Shoot4()
		{
			//loads bullet img
			Texture2D bulletTex = Content.Load<Texture2D>("bullet");
			Vector2 adjustment = new Vector2(7); //used to adjust bullet placement to center of the ship
			newBullet = new Bullets(bulletTex); //creates newBullets
												//bullet speed
			newBullet.velocity = new Vector2((float)Math.Cos(rotation4 + 1.6), (float)Math.Sin(rotation4 + 1.6)) * 3f; //+ tangentialVelocity;

			//bullet placement
			newBullet.position = spritePosition4 - adjustment + newBullet.velocity * 5;

			newBullet.isVisible = true;

			//Adds bullets
			if (bullets4.Count() < 1)
			{
				bullets4.Add(newBullet);
			}
		}
	}
}
