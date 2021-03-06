using System;
using System.IO;
using System.Reflection;

namespace Samples.Core.Recording
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory =
                new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            var destination = Path.Combine(currentDirectory, "record.ts");

            using (var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory))
            {

                var mediaOptions = new[]
                {
                    ":sout=#file{dst=" + destination + "}",
                    ":sout-keep"
                };

                mediaPlayer.SetMedia(new Uri("http://hls1.addictradio.net/addictrock_aac_hls/playlist.m3u8"),
                    mediaOptions);

                mediaPlayer.Play();

                Console.WriteLine($"Recording in {destination}");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
