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
        public Dictionary<string, Animation> SpriteFrames { get; set; }

        private SpriteRenderer spriteRenderer;
        private int currentIndex;
        private float fps;
        private float timeElapsed;
        private Rectangle[] rectangles;
        private string frameName;

        public Animator(GameObject obj) : base(obj)
        {
            fps = 5;
            //initialize the spriterenderer class
            this.spriteRenderer = obj.GetComponent<SpriteRenderer>();

            //initialize the dictionary
            SpriteFrames = new Dictionary<string, Animation>();

        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                OnAnimationDone(frameName);
                timeElapsed = 0;
                currentIndex = 0;
            }
            spriteRenderer.spriteRectangle = rectangles[currentIndex];
        }

        public void CreateAnimation(Animation animation, string name)
        {
            SpriteFrames.Add(name, animation);
        }

        public void CheckAnimation(string frameName)
        {
            if(this.frameName != frameName)
            {
                //checks if it's a new animation
                this.rectangles = SpriteFrames[frameName].Rectangles;
                //sets the rectangle
                this.spriteRenderer.spriteRectangle = rectangles[0];
                //sets the offset
                this.spriteRenderer.Offset = SpriteFrames[frameName].Offset;
                //sets the animation name
                this.frameName = frameName;
                //sets the fps
                this.fps = SpriteFrames[frameName].Fps;

                //resets the animation 

                timeElapsed = 0;
                currentIndex = 0;
            }
        }
        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in obj.components)
            {
                OnAnimationDone(animationName);
            }
        }
    }
}
