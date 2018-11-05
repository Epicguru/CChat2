using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapChat
{
    public static class Audio
    {
        public static Dictionary<string, BufferedWaveProvider> Providers = new Dictionary<string, BufferedWaveProvider>();
        public static WaveOut Output;
        public static WaveIn Input;
        public static MixingSampleProvider Mixer;
        
        public static WaveFormat GetFormat()
        {
            return new WaveFormat();
        }

        public static void InitAll()
        {
            Output = new WaveOut();
            Input = new WaveIn();
            Input.DataAvailable += NewInputData;
            Mixer = new MixingSampleProvider(GetFormat());

            Input.StartRecording();
            Output.Play();
        }

        public static void QueuePlay(string key, byte[] bytes, int count)
        {
            if (!Providers.ContainsKey(key))
            {
                Main.Log("Creating new Buffered Wave Provider for key '" + key + "'");

            }
            Providers[key].AddSamples(bytes, 0, count);
        }

        private static void NewInputData(object sender, WaveInEventArgs e)
        {
            if (Net.IsConnected)
            {

            }
        }
    }
}
