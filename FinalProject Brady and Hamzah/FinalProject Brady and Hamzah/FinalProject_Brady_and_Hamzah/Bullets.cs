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
	class Bullets
	{
		public Texture2D texture;
		public Vector2 position;
		public Vector2 velocity;
		public Vector2 origin;
		public Rectangle bulletRec;

		public bool isVisible;

		public Bullets(Texture2D newTexture)
		{
			texture = newTexture;
			isVisible = false;
		}

		//getters
		public Rectangle getBulletRec()
		{
			return bulletRec;
		}
		//setters
		public void getRec(Rectangle aBulletRec)
		{
			bulletRec = aBulletRec;
		}

		public void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
		}

	}
}
