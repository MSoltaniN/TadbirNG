using System;
using System.IO;
using System.Threading;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Utility;
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
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(SysParameterUtility.ApiServer.Name));
            path = Path.Combine(PathConfig.LocalServerRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(SysParameterUtility.LicenseServer.Name));
            path = Path.Combine(PathConfig.ResourceRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(SysParameterUtility.DbServer.Name));
            path = Path.Combine(PathConfig.WebAppRoot, "checksum");
            File.WriteAllText(path, ChecksumUtility.CalculateChecksum(SysParameterUtility.WebApp.Name));
        }

        public static void TestChecksum()
        {
            Console.WriteLine("=========== Checksum Test ===========");
            Console.WriteLine();
            Console.WriteLine("Calculating checksum for Api Server...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(SysParameterUtility.ApiServer.Name));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Calculating checksum for License Server...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(SysParameterUtility.LicenseServer.Name));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Calculating checksum for Db Server...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(SysParameterUtility.DbServer.Name));
                Thread.Sleep(1000);
            }

            Console.WriteLine("Calculating checksum for Web App...");
            for (int count = 1; count <= 5; count++)
            {
                Console.Write($"(Pass {count}) Checksum : ");
                Console.WriteLine(ChecksumUtility.CalculateChecksum(SysParameterUtility.WebApp.Name));
                Thread.Sleep(1000);
            }
        }

        public static void TestRunnerWithExpectedPath()
        {
            Environment.CurrentDirectory = PathConfig.DockerCacheRoot;
            var output = new CliRunner().Run("docker image ls");
        }

        public static void TestCabMaker()
        {
            var archive = new ArchiveUtility(null, false);
            archive.Cab(@"D:\Temp\__Test__\runner");
        }

        public static void ShowSysParameters()
        {
            Console.WriteLine($"Current system parameters :{Environment.NewLine}");
            var sysParams = JsonHelper.From(SysParameterUtility.AllParameters);
            Array.ForEach(sysParams.Split(Environment.NewLine),
                line => Console.WriteLine(line));
        }
    }
}
