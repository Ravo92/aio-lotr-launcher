using PatchLauncher.Helper;
using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace PatchLauncher.Helper
{
    public class SoundPlayerHelper
    {
        public static void PLaySoundFile(XAudio2 device, string text, string fileName)
        {
            var stream = new SoundStream(File.OpenRead(fileName));
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
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", ConstStrings.C_BUTTONSOUND_CLICK);
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }

        public static void PlaySoundHover()
        {
            XAudio2 _xaudio2 = new();
            MasteringVoice _masteringVoice = new(_xaudio2);
            PLaySoundFile(_xaudio2, "", ConstStrings.C_BUTTONSOUND_HOVER);
            _masteringVoice.Dispose();
            _xaudio2.Dispose();
        }
    }
}
