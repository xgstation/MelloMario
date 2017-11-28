using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Theming
{
    using MelloMario.Sounds;

    internal class SoundManager
    {
        public readonly IListener<ISoundable> SoundEffectListener;
        private readonly BGMManager sounds;
        private IModel model;

        public SoundManager()
        {
            SoundEffectListener = new SoundEffectListener();
            //sounds = new BGMManager();
        }

        public void BindModel(IModel newGameModel)
        {
            model = newGameModel;
        }
        public void ToggleMute()
        {
            sounds?.ToggleMute();
            (SoundEffectListener as SoundEffectListener)?.ToggleMute();
        }

        public void Update(int time)
        {

        }

        public void Initialize()
        {
            
        }
    }
}
