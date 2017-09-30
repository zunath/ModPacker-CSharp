using System.IO;
using ModPacker_CSharp.GFFParser;
using ModPacker_CSharp.NWObjects;

namespace ModPacker_CSharp_TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileInPath = @"./Module/Cyberpunk Zombie Survival.mod";
            const string fileOutPath = @"./Module/OutputMod.mod";

            ModuleReader reader = new ModuleReader();
            ModuleWriter writer = new ModuleWriter();

            NWModule module;
            using (var stream = File.OpenRead(fileInPath))
            {
                module = reader.LoadModule(stream);
            }



                
        }
    }
}
