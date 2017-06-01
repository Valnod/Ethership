using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Wall : Component, IUpdateable
    {
        public bool visible = true;
        public Wall(GameObject obj) : base(obj)
        {

        }
        public void Update(GameTime gameTime)
        {
            foreach (int wall in GameWorld.Instance.uiWall)
            {
                this.visible = false;
            }
            if (this.visible == false)
            {
                obj.GetComponent<SpriteRenderer>().Color = Color.White;
            }
        }
    }
}
