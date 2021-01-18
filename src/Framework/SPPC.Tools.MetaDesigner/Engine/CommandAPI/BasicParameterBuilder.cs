using System;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class BasicParameterBuilder : ICommandParameterBuilder
    {
        public ICommandContext Context { get; set; }

        public object Build(string parameterName)
        {
            if (Context == null)
            {
                throw ExceptionBuilder.NewInvalidOperationException("Command context is not set.");
            }

            object parameter = null;
            if (parameterName == "item")
            {
                parameter = Context.Item;
            }
            else if (parameterName == "context")
            {
                parameter = Context;
            }
            else if (parameterName == "collection")
            {
                parameter = Context.Collection;
            }
            else if (parameterName == "object")
            {
                parameter = Context.Object;
            }
            else if (parameterName == "wrapper")
            {
                parameter = Context.Model;
            }
            else if (parameterName == "editor")
            {
                var editorConfig = _configuration.GetEditor(Context);
                if (editorConfig != null)
                {
                    parameter = Reflector.Instantiate(editorConfig.TypeName);
                }
            }
            else
            {
                parameter = BuildGenericParameter(Context.Command, parameterName);
            }

            return parameter;
        }

        private object BuildGenericParameter(string commandName, string parameterName)
        {
            object parameter = null;
            var paramConfig = _configuration.GetParameter(commandName, parameterName);
            if (paramConfig != null)
            {
                var paramType = Type.GetType(paramConfig.Type);
                if (paramType.IsEnum)
                {
                    parameter = Enum.Parse(paramType, paramConfig.Value);
                }
                else
                {
                    parameter = (String.IsNullOrEmpty(paramConfig.Value))
                        ? Reflector.Instantiate(paramType)
                        : Convert.ChangeType(paramConfig.Value, paramType);
                }
            }

            return parameter;
        }

        private CommandConfiguration _configuration = new CommandConfiguration();
    }
}
