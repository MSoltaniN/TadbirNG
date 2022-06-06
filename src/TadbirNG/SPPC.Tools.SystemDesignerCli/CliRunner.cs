using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.SystemDesignerCli
{
    public static class CliRunner
    {
        public static int Run(string[] args)
        {
            DisplayBanner();
            if (!EnsureHasConfiguration())
            {
                return CliResult.MissingConfiguration;
            }

            if (!EnsureHasCommand(args))
            {
                return CliResult.NoCommand;
            }

            if (!EnsureCommandIsSupported(args))
            {
                return CliResult.UnsupportedCommand;
            }

            if (!EnsureNoMissingParameter(args))
            {
                return CliResult.MissingParameter;
            }

            var command = GetCommand(args);
            int result = HandleCommand(command, args);
            if (result != CliResult.Done)
            {
                return result;
            }

            Console.WriteLine("Done.");
            Console.WriteLine();
            return CliResult.Done;
        }

        private static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("SPPC Framework : Project Command-Line Interface (v1.2)");
            Console.WriteLine("(c) Copyright {0}, SPPC, All Rights Reserved", DateTime.Now.Year);
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        private static bool EnsureHasConfiguration()
        {
            using (var reader = new StreamReader(
                typeof(Program).Assembly.GetManifestResourceStream(_jsonConfigUri)))
            {
                string jsonConfig = reader.ReadToEnd();
                _config = JsonHelper.To<CliConfiguration>(jsonConfig);
            }

            return true;
        }

        private static bool EnsureHasCommand(string[] args)
        {
            string commandName = GetParameter(args, _commandParam);
            return !String.IsNullOrEmpty(commandName);
        }

        private static bool EnsureCommandIsSupported(string[] args)
        {
            bool isSupported = true;
            var command = GetCommand(args);
            if (command == null)
            {
                string paramSwitch = String.Format("-{0}:", _commandParam);
                Console.WriteLine("Command '{0}' is not supported.", args[0].Replace(paramSwitch, String.Empty));
                isSupported = false;
            }

            return isSupported;
        }

        private static bool EnsureNoMissingParameter(string[] args)
        {
            bool noMissingParam = true;
            var cliCommand = GetCommand(args);
            var requiredParams = cliCommand.Parameters
                .Where(param => param.Required);
            foreach (var param in requiredParams)
            {
                var value = GetParameter(args, param.Name);
                if (String.IsNullOrEmpty(value))
                {
                    noMissingParam = false;
                    break;
                }
            }

            return noMissingParam;
        }

        private static CliCommand GetCommand(string[] args)
        {
            string commandName = GetParameter(args, _commandParam);
            return _config.Commands
                .Where(cmd => cmd.Name == commandName)
                .SingleOrDefault();
        }

        private static int HandleCommand(CliCommand command, string[] args)
        {
            var paramValues = new List<string>();
            foreach (var param in command.Parameters)
            {
                paramValues.Add(GetParameter(args, param.Name, param.Required));
            }

            if (Reflector.Instantiate(
                Type.GetType(command.HandlerType), paramValues.ToArray()) is not ICliCommand handler)
            {
                Console.WriteLine("ERROR: Could not load handler for command '{0}'.", command.Name);
                return CliResult.TypeLoadError;
            }

            handler.Execute();
            return CliResult.Done;
        }

        private static string GetParameter(string[] args, string paramName, bool required = true)
        {
            string paramSwitch = String.Format("-{0}:", paramName);
            string param = args
                .Where(arg => arg.StartsWith(paramSwitch))
                .FirstOrDefault();
            param = param?.Replace(paramSwitch, String.Empty).Trim('"', '\'');
            if (required && String.IsNullOrEmpty(param))
            {
                Console.WriteLine("ERROR: A required parameter ({0}) is missing.", paramSwitch);
            }

            return param;
        }

        private static readonly string _jsonConfigUri = "SPPC.Tools.SystemDesignerCli.cli-config.json";
        private static readonly string _commandParam = "cmd";
        private static CliConfiguration _config;
    }
}
