using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace EtherShip
{
    public class GameObject
    {
        private bool isLoaded = false;

        private List<Component> components = new List<Component>();
        public Vector2 position;

        public GameObject(Vector2 position)
        {
            this.position = position;
        }

        public void AddComponnent(Component component)
        {
            components.Add(component);
        }

        public Component GetComponent(string componentName)
        {
            return components.Find(n => n.GetType().Name == componentName);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in components)
            {
                if (component is IUpdateable)
                {
                    (component as IUpdateable).Update(gameTime);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is IDrawable)
                {
                    (component as IDrawable).Draw(spriteBatch);
                }
            }
        }
    }
}
