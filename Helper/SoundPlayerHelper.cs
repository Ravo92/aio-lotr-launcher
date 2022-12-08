using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System.Media;
using System.Reflection;

namespace PatchLauncher.Helper
{
    public class SoundPlayerHelper
    {
        public static readonly CancellationTokenSource _source = new();
        public static CancellationToken _token = _source.Token;

        public static void PlaySoundFile(XAudio2 device, Stream fileName, CancellationToken _token)
        {
            //SoundPlayer _soundPlayer = new(fileName);
            //_soundPlayer.PlayLooping();

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
                if (_token.IsCancellationRequested) break;

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
            PlaySoundFile(_sound, _SoundClickFile, _token);
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static void PlaySoundHover()
        {
            Stream _SoundHoverFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_BUTTONSOUND_HOVER)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            PlaySoundFile(_sound, _SoundHoverFile, _token);
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static async void PlayThemeDefault()
        {
            Stream _ThemeDefault = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_DEFAULT)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            await Task.Run(() => PlaySoundFile(_sound, _ThemeDefault, _token));
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static async void PlayThemeGondor()
        {
            Stream _ThemeGondor = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_GONDOR)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            await Task.Run(() => PlaySoundFile(_sound, _ThemeGondor, _token));
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static async void PlayThemeRohan()
        {
            Stream _ThemeRohan = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_ROHAN)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            await Task.Run(() => PlaySoundFile(_sound, _ThemeRohan, _token));
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static async void PlayThemeIsengard()
        {
            Stream _ThemeIsengard = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_ISENGARD)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            await Task.Run(() => PlaySoundFile(_sound, _ThemeIsengard, _token));
            _masteringVoice.Dispose();
            _sound.Dispose();
        }

        public static async void PlayThemeMordor()
        {
            Stream _ThemeMordor = Assembly.GetExecutingAssembly().GetManifestResourceStream(ConstStrings.C_THEMESOUND_MORDOR)!;

            XAudio2 _sound = new();
            MasteringVoice _masteringVoice = new(_sound);
            _masteringVoice.SetVolume(0.5f);
            await Task.Run(() => PlaySoundFile(_sound, _ThemeMordor, _token));
            _masteringVoice.Dispose();
            _sound.Dispose();
        }
    }
}
