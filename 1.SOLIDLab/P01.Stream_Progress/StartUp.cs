using P01.Stream_Progress.Contracts;
using System;

namespace P01.Stream_Progress
{
    public class StartUp
    {
        static void Main()
        {
            IStreamable music = new Music("Artist", "Album", 56, 23);
            IStreamable file = new File("Name", 56, 20);

            StreamProgressInfo spiMusic = new StreamProgressInfo(music);
            StreamProgressInfo spiFile = new StreamProgressInfo(file);

            Console.WriteLine(spiMusic.CalculateCurrentPercent());
            Console.WriteLine(spiFile.CalculateCurrentPercent());
        }
    }
}
