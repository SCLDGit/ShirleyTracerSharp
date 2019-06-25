using System;

namespace TheNextWeek.Constants
{
    internal static class RenderConstants
    {
        internal static int ImageSizeX => 1920;
        internal static int ImageSizeY => 1080;
        internal static int NumberOfSamples => 128;
        internal static bool PrintRenderInfo => true;
        internal static bool UseParallelProcessing => true;
        internal static string SaveLocation => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ShirleyTracer", "Renders", "TheNextWeek", $@"TheNextWeek_{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.png");
    }
}
