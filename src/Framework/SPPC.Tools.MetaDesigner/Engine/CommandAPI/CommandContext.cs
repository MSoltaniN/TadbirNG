using System;
using System.Collections;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.MetaDesigner.Configuration;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class CommandContext : ICommandContext
    {
        public string Command { get; set; }
        public IRepositoryModel Model { get; set; }
        public object Item { get; set; }
        public IList Collection { get; set; }
        public object Object { get; set; }
        public CommandElement Config { get; set; }
        public bool IsItemContext
        {
            get { return (Item != null && Collection != null); }
        }

        public bool IsCollectionContext
        {
            get { return (Collection != null && Object != null); }
        }

        public bool IsObjectContext
        {
            get { return (Object != null && Item == null && Collection == null); }
        }

        public string Title
        {
            get
            {
                var title = String.Empty;
                var config = new CommandConfiguration();
                var command = config.GetCommand(Command);
                if (command != null)
                {
                    title = command.Title;
                }

                if (IsCollectionContext)
                {
                    var itemType = (Collection is IEnumerable && Collection.GetType().IsGenericType)
                        ? Collection.GetType().GenericTypeArguments[0]
                        : typeof(object);
                    if (itemType != typeof(object))
                    {
                        title = String.Format(title, itemType.Name);
                    }
                }

                return title;
            }
        }
    }
}
