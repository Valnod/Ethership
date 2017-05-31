using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace EtherShip
{
    class SFX
    {
        public Song song;

        public List<SoundEffect> soundEffects;

        public SFX()
        {
            soundEffects = new List<SoundEffect>();
        }

        public void MediaPlayer()
        {

        }

        public void LoadContent(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("hitSound"));

            soundEffects[0].Play(); //Plays the first soundeffect

            //Soundeffects can be manipulated after they are played
            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = false;
            instance.Play();
        }
    }
}
