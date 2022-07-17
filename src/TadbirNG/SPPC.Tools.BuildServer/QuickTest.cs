using System;
using System.IO;
using System.Threading;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.BuildServer
{
    public static class QuickTest
    {
        public static void TestProcessEnvironment()
        {
            // Archive utility sets Path variable in current process environment, using given relative path
            var archive = new ArchiveUtility(@"..\..\..\misc\tools");
            Console.WriteLine("Compressing runtimes folder to 'runtimes.tar' ...");
            archive.Tar("runtimes.tar", "runtimes");
            Console.WriteLine("Compressing 'runtimes.tar' to 'runtimes.tar.gz' ...");
            archive.GZip("runtimes.tar");
        }

        public static void GeneratePasswords()
        {
            int length = 1;
            while (length > 0)
            {
                Console.Write("Length (0 to quit): ");
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out length))
                {
                    if (length > 0)
                    {
                        Console.WriteLine($"Password : {PasswordGenerator.Generate(length)}");
                    }
                }
                else
                {
                    length = 10;
                    Console.WriteLine($"Password : {PasswordGenerator.Generate()}");
                }
            }
        }

        public static void CreateChecksumFiles()
        {
            var path = Path.Combine(PathConfig.WebApiRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.ApiServer));
            path = Path.Combine(PathConfig.LocalServerRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.LicenseServer));
            path = Path.Combine(PathConfig.ResourceRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.DbServer));
            path = Path.Combine(PathConfig.WebAppRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(DockerService.WebApp));
        }

        public static void TestChecksum()
        {
            Console.WriteLine("=========== Checksum Test ===========");
            Console.WriteLine();
            Console.WriteLine("Calculating checksum for Api Server...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(DockerService.ApiServer));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Calculating checksum for License Server...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(DockerService.LicenseServer));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Calculating checksum for Db Server...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(DockerService.DbServer));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Calculating checksum for Web App...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(DockerService.WebApp));
                Thread.Sleep(1000);
            }
        }
    }
}
