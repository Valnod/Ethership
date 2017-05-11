using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EtherShip
{
    class Animator : Component, IUpdateable
    {
        public Dictionary<string, Frame> SpriteFrames { get; set; }
        private SpriteRendere spriteRendere;
        private int currentIndex;
        private float fps;
        public string frameName;

        public Animator(GameObject obj, SpriteRendere spriteRendere, int currentIndex, float fps, string frameName) : base(obj)
        {
            this.spriteRendere = spriteRendere;
            this.currentIndex = currentIndex;
            this.fps = fps;
            this.frameName = frameName;
        }

        public void Update(GameTime gameTime)
        {

        }

    }
}
