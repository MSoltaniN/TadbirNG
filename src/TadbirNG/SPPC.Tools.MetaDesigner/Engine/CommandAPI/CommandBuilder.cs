using System;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class CommandBuilder
    {
        public CommandBuilder()
        {
            _paramBuilder = new BasicParameterBuilder();
        }

        public CommandBuilder(ICommandParameterBuilder paramBuilder)
        {
            _paramBuilder = paramBuilder;
        }

        public IRepositoryCommand Build(ICommandContext context)
        {
            Verify.ArgumentNotNull(context, "context");

            var config = new CommandConfiguration();
            var commandConfig = config.GetCommand(context.Command);
            var command = Reflector.Instantiate(commandConfig.HandlerTypeName) as IRepositoryCommand;
            _paramBuilder.Context = context;
            Array.ForEach(
                config.GetParameterNames(context.Command),
                name => command.Parameters.Add(name, _paramBuilder.Build(name)));
            return command;
        }

        private ICommandParameterBuilder _paramBuilder;
    }
}
