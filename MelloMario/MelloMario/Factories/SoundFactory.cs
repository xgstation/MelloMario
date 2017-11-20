namespace MelloMario.Factories
{
    #region

    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Media;
    using Sounds;

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
                soundEffects.Add(name, content.Load<Microsoft.Xna.Framework.Audio.SoundEffect>(name).CreateInstance());
            }

            return soundEffects[name];
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

        public ISoundEffect CreateSoundEffect(string name)
        {
            switch (name)
            {
                case "Bounce":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_jumpsmall"));
                case "PowerBounce":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_jump"));
                case "BumpBlock":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_bump"));
                case "BreakBlock":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_breakblock"));
                case "Coin":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_coin"));
                case "Death":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_mariodie"));
                case "SizeUp":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_powerup"));
                case "EnemyKill":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_stomp"));
                case "Pipe":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_pipe"));
                case "GameOver":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_gameover"));
                case "GameWon":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_stage_clear"));
                case "SizeUpAppear":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_powerup_appears"));
                case "OneUpCollect":
                    return new Sounds.SoundEffect(GetSoundEffect("SFX/smb_1"));
                default:
                    // never reach
                    return null;
            }
        }
    }
}
