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
        public SoundEffect hitEffect;
        public SoundEffect leftKnirtEffect;
        public SoundEffect rightKnirkEffect;
        public SoundEffect stopKnirkEffect;
        public SoundEffect towerShootEffect;





        public List<SoundEffect> soundEffects = new List<SoundEffect>();

        public SFX()
        {
         
        }

        public void MediaPlayer()
        {
            
        }

        public void LoadContent(ContentManager Content)
        {

            song = Content.Load<Song>("ebAndFlow");

            //hitEffect = Content.Load<SoundEffect>("hitSound");
            //leftKnirtEffect = Content.Load<SoundEffect>("knirk 01");
            //rightKnirkEffect = Content.Load<SoundEffect>("knirk 02");
            //stopKnirkEffect = Content.Load<SoundEffect>("knirk 03");
            //towerShootEffect = Content.Load<SoundEffect>("Tower_Attack");

            //soundEffects.Add(Content.Load<SoundEffect>("knirk 05"));
            //soundEffects.Add(Content.Load<SoundEffect>("Tower_Attack"));
            //soundEffects.Add(Content.Load<SoundEffect>("Tower_Destroyd"));

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
