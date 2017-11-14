﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Factories;

namespace MelloMario.Sounds
{
    class SoundController
    {
        // TODO: use a factory
        public static Song Normal, BelowGround, Hurry, gameOver;
        public static SoundEffect bounce, powerBounce, bumpBlock, breakBlock, coin, death, sizeUp, sizeUpAppear, oneUpCollect, enemyKill, pipe;

        public enum Songs { normal, belowGround, hurry, pause, title , gameover}
        public static Songs currentSong;

        public SoundController(Game1 game)
        {
            bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
            powerBounce = SoundFactory.Instance.CreateSoundEffect("smb_jump-super");
            bumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
            breakBlock = SoundFactory.Instance.CreateSoundEffect("smb_breakblock");
            coin = SoundFactory.Instance.CreateSoundEffect("smb_coin");
            death = SoundFactory.Instance.CreateSoundEffect("smb_mariodie");
            sizeUp = SoundFactory.Instance.CreateSoundEffect("smb_powerup");
            enemyKill = SoundFactory.Instance.CreateSoundEffect("smb_stomp");
            pipe = SoundFactory.Instance.CreateSoundEffect("smb_pipe");
            

            sizeUpAppear = SoundFactory.Instance.CreateSoundEffect("smb_powerup_appears");
            oneUpCollect = SoundFactory.Instance.CreateSoundEffect("smb_1-up");

            Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
            BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
            Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");
            gameOver = SoundFactory.Instance.CreateSong("09-game-over");
            currentSong = Songs.normal;
        }

        public static void PlayMusic(Songs song)
        {
            switch (song)
            {
                case Songs.normal:
                    MediaPlayer.Play(Normal);
                    break;
                case Songs.hurry:
                    MediaPlayer.Play(Hurry);
                    break;
                case Songs.gameover:
                    MediaPlayer.Play(gameOver);
                    break;
                case Songs.belowGround:
                    MediaPlayer.Play(BelowGround);
                    break;
                default:
                    break;
            }
            MediaPlayer.IsRepeating = true;
        }

        public void Mute()
        {
            MediaPlayer.Stop();
        }
    }
}
