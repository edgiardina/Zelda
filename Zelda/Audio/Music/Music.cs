using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Test_Game
{
    public static class Music
    {
        private static AudioEngine engine;
        private static WaveBank wavebank;
        private static SoundBank soundbank;

        public static Cue Play(string name)
        {            
            Cue returnValue = soundbank.GetCue(name);
            returnValue.Play();             
            return returnValue;
        }

        public static void Stop(Cue cue)
        {
            cue.Stop(AudioStopOptions.Immediate);
        }

        public static void Initialize()
        {            
            engine = new AudioEngine("..\\..\\..\\Audio\\Music\\Project\\Win\\zelda.xgs");
            wavebank = new WaveBank(engine, "..\\..\\..\\Audio\\Music\\Project\\Win\\Wave Bank.xwb");
            soundbank = new SoundBank(engine, "..\\..\\..\\Audio\\Music\\Project\\Win\\Sound Bank.xsb");
        }

        public static void Update()
        {
            engine.Update();
        }

        public static void Shutdown()
        {
            soundbank.Dispose();
            wavebank.Dispose();
            engine.Dispose();
        }
    }
}
