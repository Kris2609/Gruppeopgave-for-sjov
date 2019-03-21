using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDRL
{
    public interface GameState
    {
        List<GameObject> StateList { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Enter();
        void Execute(GameTime gameTime);
        void Exit();
    }
}
