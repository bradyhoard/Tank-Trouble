using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProject_Brady_and_Hamzah
{
	class Menu
	{

		public Rectangle background;
		public Texture2D backgroundPic;
		Color picColor = Color.White;

		public Menu(Rectangle background, Texture2D backgroundPic)
		{
			setbackgound(background);
			setbackgoundPic(backgroundPic);
		}


		public void setbackgound(Rectangle Abackground)
		{
			background = Abackground;
		}

		public Rectangle getbackgound()
		{
			return background;
		}

		public void setbackgoundPic(Texture2D AbackgroundPic)
		{
			backgroundPic = AbackgroundPic;
		}

		public Texture2D getbackgoundPic()
		{
			return backgroundPic;
		}

		public void DrawMethod(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(backgroundPic, background, Color.White);
		}
	}
}
