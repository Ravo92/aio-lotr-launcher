using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System.Media;
using System.Reflection;

namespace Helper
{
    public class SoundPlayerHelper
    {
        public SoundPlayer _soundPlayer = new();
        public static void PlaySoundFile(XAudio2 device, Stream fileName)
        {
            var stream = new SoundStream(fileName);
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            var sourceVoice = new SourceVoice(device, waveFormat, true);
            // Adds a sample callback to check that they are working on source voices
            sourceVoice.SubmitSourceBuffer(buffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();

            int count = 0;
            while (sourceVoice.State.BuffersQueued > 0)
            {
                if (count == 50)
                {
                    count = 0;
                }
                Thread.Sleep(10);
                count++;
            }

            sourceVoice.DestroyVoice();
            sourceVoice.Dispose();
            buffer.Stream.Dispose();
        }

        public static void PlaySoundClick()
        {
            Stream _SoundClickFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_BUTTONSOUND_CLICK)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            PlaySoundFile(_sound, _SoundClickFile);
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static void PlaySoundHover()
        {
            Stream _SoundHoverFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_BUTTONSOUND_HOVER)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            PlaySoundFile(_sound, _SoundHoverFile);
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public void PlayTheme(string theme)
        {
            switch (theme)
            {
                case ConstStrings.C_THEMESOUND_DEFAULT:
                    {
                        Stream _ThemeDefault = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_DEFAULT)!;

                        _soundPlayer.Stream = _ThemeDefault;
                        _soundPlayer.PlayLooping();
                        break;
                    }
                case ConstStrings.C_THEMESOUND_GONDOR:
                    {
                        Stream _ThemeGondor = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_GONDOR)!;

                        _soundPlayer.Stream = _ThemeGondor;
                        _soundPlayer.PlayLooping();
                        break;
                    }
                case ConstStrings.C_THEMESOUND_ROHAN:
                    {
                        Stream _ThemeRohan = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_ROHAN)!;

                        _soundPlayer.Stream = _ThemeRohan;
                        _soundPlayer.PlayLooping();
                        break;
                    }
                case ConstStrings.C_THEMESOUND_ISENGARD:
                    {
                        Stream _ThemeIsengard = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_ISENGARD)!;

                        _soundPlayer.Stream = _ThemeIsengard;
                        _soundPlayer.PlayLooping();
                        break;
                    }
                case ConstStrings.C_THEMESOUND_MORDOR:
                    {
                        Stream _ThemeMordor = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_MORDOR)!;

                        _soundPlayer.Stream = _ThemeMordor;
                        _soundPlayer.PlayLooping();
                        break;
                    }
            }
        }

        public void StopTheme()
        {
            _soundPlayer.Stop();
        }
    }
}
