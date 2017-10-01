using System.IO;
using System.Linq;
using ModPacker_CSharp;
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

            using (var stream = new MemoryStream())
            {
                writer.SaveModule(stream, module);

                stream.Position = 0;
                File.WriteAllBytes(fileOutPath, stream.ToArray());
            }


            // TODO: DEBUGGING

            //var result =
            //    TempStaticStorage.ReaderGffList.Where(
            //        x => TempStaticStorage.WriterGffList.All(y => y.Resref != x.Resref))
            //        .ToList();


            // TODO: END DEBUGGING

        }
    }
}
