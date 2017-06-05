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
        public SoundEffect effect;
        public string effectName;

        public string EffectName
        {
            get { return effectName; }
            set { effectName = value; }
        }


        public List<SoundEffect> soundEffects = new List<SoundEffect>();

        public SFX(string effectName)
        {
            this.EffectName = effectName;
        }

        public void MediaPlayer()
        {
            
        }

        public void LoadContent(ContentManager Content)
        {

            song = Content.Load<Song>("ebAndFlow");

            effect = Content.Load<SoundEffect>("hitSound");
            effect = Content.Load<SoundEffect>("knirk 01");
            effect = Content.Load<SoundEffect>("knirk 02");
            effect = Content.Load<SoundEffect>("knirk 03");
            effect = Content.Load<SoundEffect>("knirk 04");
            soundEffects.Add(Content.Load<SoundEffect>("knirk 05"));
            soundEffects.Add(Content.Load<SoundEffect>("Tower_Attack"));
            soundEffects.Add(Content.Load<SoundEffect>("Tower_Destroyd"));

            //soundEffects[0].Play(); //Plays the first soundeffect

            ////Soundeffects can be manipulated after they are played
            //var instance = soundEffects[0].CreateInstance();
            //instance.IsLooped = false;
            //instance.Play();
        }
        public void PlayBackground()
        {

        }
        public void Shoot()
        {

        }
    }
}
