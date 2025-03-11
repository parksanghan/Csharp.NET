using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace _231106_MultiGame_Client
{

    public class SoundManager
    {
        private readonly Dictionary<string, List<IWavePlayer>> loadedSounds = new Dictionary<string, List<IWavePlayer>>();
        private readonly object lockObject = new object();

        public void SoundPlay(string path, float volume)
        {
            lock (lockObject)
            {
                if (loadedSounds.ContainsKey(path))
                {
                    // If the sound is already loaded, create a new wave player and play
                    IWavePlayer wavePlayer = CreateWavePlayer(path, volume);
                    loadedSounds[path].Add(wavePlayer);
                    wavePlayer.Play();
                }
                else
                {
                    // If the sound is not loaded, create a new list of wave players and play
                    List<IWavePlayer> wavePlayers = new List<IWavePlayer>();
                    IWavePlayer wavePlayer = CreateWavePlayer(path, volume);
                    wavePlayers.Add(wavePlayer);
                    wavePlayer.Play();

                    // Store the list of wave players in the dictionary for future reuse
                    loadedSounds.Add(path, wavePlayers);
                }
            }
        }

        private IWavePlayer CreateWavePlayer(string path, float volume)
        {
            var reader = new AudioFileReader(path);
            var volumeProvider = new VolumeSampleProvider(reader.ToSampleProvider());
            volumeProvider.Volume = volume;

            var waveOut = new WaveOutEvent();
            waveOut.Init(volumeProvider);

            waveOut.PlaybackStopped += (sender, args) =>
            {
                lock (lockObject)
                {
                    // When playback stops, remove the wave player from the list
                    if (loadedSounds.ContainsKey(path))
                    {
                        List<IWavePlayer> wavePlayers = loadedSounds[path];
                        wavePlayers.Remove(waveOut);

                        // If all wave players for this sound are stopped, remove the entry from the dictionary
                        if (wavePlayers.Count == 0)
                        {
                            loadedSounds.Remove(path);
                        }
                    }
                }
            };

            return waveOut;
        }

        public void StopAll()
        {
            lock (lockObject)
            {
                foreach (var wavePlayers in loadedSounds.Values)
                {
                    foreach (var wavePlayer in wavePlayers)
                    {
                        wavePlayer.Stop();
                    }
                }
            }
        }
    }
}
