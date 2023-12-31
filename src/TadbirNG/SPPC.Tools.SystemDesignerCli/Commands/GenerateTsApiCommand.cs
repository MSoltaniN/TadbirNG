﻿using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesignerCli
{
    public class GenerateTsApiCommand : ICliCommand
    {
        public GenerateTsApiCommand(params string[] args)
        {
            if (args.Length < 1)
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Insufficient arguments were provided (needed 1).", "paramValues");
            }

            _typeNames = args[0].Split(',');
        }

        public void Execute()
        {
            string tsApiPath = ConfigurationManager.AppSettings["TsAppPath"];
            string csAssembly = ConfigurationManager.AppSettings["CsInterfacesAssembly"];
            string csNamespace = ConfigurationManager.AppSettings["CsApiNamespace"];
            var assembly = Assembly.Load(csAssembly);
            if (assembly == null)
            {
                Console.WriteLine(
                    "ERROR: Could not load view model assembly (Error code : {0}).", CliResult.TypeLoadError);
                return;
            }

            foreach (string typeName in _typeNames)
            {
                string tsTypeName = typeName.CamelCase();
                string fullName = String.Format("{0}.{1}", csNamespace, typeName);
                string generatedPath = String.Format(@"{0}\{1}.ts", tsApiPath, tsTypeName);
                var csType = assembly.GetType(fullName);

                Console.WriteLine("Generating TypeScript Api class '{0}'...", typeName);
                var template = new TsApiFromCsApi(csType);
                File.WriteAllText(generatedPath, template.TransformText());
            }
        }

        private readonly string[] _typeNames;
    }
}
