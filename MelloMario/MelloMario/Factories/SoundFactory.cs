﻿namespace MelloMario.Factories
{
    #region

    using System.Collections.Generic;
    using MelloMario.Sounds.Tracks;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Media;

    #endregion

    internal class SoundFactory : ISoundFactory<ContentManager>
    {
        private readonly IDictionary<string, Song> songs;
        private readonly IDictionary<string, SoundEffectInstance> soundEffects;
        private ContentManager content;

        private SoundFactory()
        {
            songs = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffectInstance>();
        }

        public static ISoundFactory<ContentManager> Instance { get; } = new SoundFactory();

        public void BindLoader(ContentManager loader)
        {
            content = loader;
        }

        public ISoundTrack CreateSoundTrack(string name)
        {
            switch (name)
            {
                case "Normal":
                    return new SoundTrack(GetSong("Music/01-main-theme-overworld"));
                case "BelowGround":
                    return new SoundTrack(GetSong("Music/02-underworld"));
                case "Star":
                    return new SoundTrack(GetSong("Music/05-starman"));
                case "Hurry":
                    return new SoundTrack(GetSong("Music/18-hurry-overworld-"));
                default:
                    // never reach
                    return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public ISoundEffect CreateSoundEffect(string name)
        {
            switch (name)
            {
                case "Bounce":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_jumpsmall"));
                case "PowerBounce":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_jump"));
                case "BumpBlock":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_bump"));
                case "BreakBlock":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_breakblock"));
                case "Coin":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_coin"));
                case "Death":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_mariodie"));
                case "PowerUp":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_powerup"));
                case "EnemyKill":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_stomp"));
                case "Pipe": //Same sound for downgrade to standard mario
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_pipe"));
                case "GameOver":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_gameover"));
                case "GameWon":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_stage_clear"));
                case "Unveil":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_powerup_appears"));
                case "OneUpCollect":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/smb_1"));
                case "Thwomp":
                    return new Sounds.Effects.SoundEffect(GetSoundEffect("SFX/thwomp"));
                default:
                    // never reach
                    return null;
            }
        }

        private Song GetSong(string name)
        {
            if (!songs.ContainsKey(name))
            {
                songs.Add(name, content.Load<Song>(name));
            }

            return songs[name];
        }

        private SoundEffectInstance GetSoundEffect(string name)
        {
            if (!soundEffects.ContainsKey(name))
            {
                soundEffects.Add(name, content.Load<SoundEffect>(name).CreateInstance());
            }

            return soundEffects[name];
        }
    }
}
