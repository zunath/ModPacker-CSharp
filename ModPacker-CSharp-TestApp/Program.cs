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

            ModuleReader reader = new ModuleReader();

            using (var stream = File.OpenRead(fileInPath))
            {
                NWModule module = reader.LoadModule(stream);


            }
                
        }
    }
}
