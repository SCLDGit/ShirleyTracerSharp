using System;

namespace InOneWeekend.Constants
{
    internal static class RenderConstants
    {
        internal static int ImageSizeX => 1280;
        internal static int ImageSizeY => 720;
        internal static int NumberOfSamples => 8;
        internal static bool PrintRenderInfo => true;
        internal static bool UseParallelProcessing => true;
        internal static string SaveLocation => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ShirleyTracer", "Renders", $@"render_{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.png");
    }
}
