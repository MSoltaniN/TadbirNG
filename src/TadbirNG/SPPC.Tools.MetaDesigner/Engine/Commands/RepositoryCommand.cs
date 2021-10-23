using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public abstract class RepositoryCommand : IRepositoryCommand
    {
        protected RepositoryCommand()
        {
            this.Parameters = new Dictionary<string, object>();
            this.ContextManager = MetaDesignerContext.Current;
        }

        public IDictionary<string, object> Parameters { get; private set; }
        public bool IsComplete { get; protected set; }
        public IUserInputCollector InputCollector { get; protected set; }

        public virtual void Execute()
        {
            if (!ValidateParameters())
            {
                throw ExceptionBuilder.NewInvalidOperationException(BuildValidationError());
            }
        }

        protected IMetaDesignerContextManager ContextManager { get; private set; }

        protected abstract IDictionary<string, string> GetRequiredParameters();
        protected virtual bool ValidateParameters()
        {
            bool validated = true;
            foreach (var key in RequiredParameters.Keys)
            {
                if (IsMissingParameter(key) || HasInvalidType(key))
                {
                    validated = false;
                    break;
                }
            }

            return validated;
        }

        private IDictionary<string, string> RequiredParameters
        {
            get
            {
                if (_requiredParams == null)
                {
                    _requiredParams = GetRequiredParameters();
                }

                return _requiredParams;
            }
        }

        private string BuildValidationError()
        {
            var paramInfoItems = new List<string>(RequiredParameters.Count);
            Array.ForEach(
                RequiredParameters.Keys.ToArray(),
                key => paramInfoItems.Add(String.Format("* {0} ({1})", key, RequiredParameters[key])));
            return (String.Format(
                "Parameters are invalid. Required parameters are :\r\n{0}",
                String.Join("\r\n", paramInfoItems.ToArray())));
        }

        private bool IsMissingParameter(string parameterName)
        {
            return !Parameters.ContainsKey(parameterName);
        }

        private bool HasInvalidType(string parameterName)
        {
            bool hasInvalidType = false;
            var requiredType = Type.GetType(_requiredParams[parameterName]);
            if (Parameters[parameterName] != null)
            {
                var actualType = Parameters[parameterName].GetType();
                hasInvalidType = (!String.IsNullOrEmpty(_requiredParams[parameterName])
                    && (requiredType == null || !requiredType.IsAssignableFrom(actualType)));
            }

            return hasInvalidType;
        }

        private IDictionary<string, string> _requiredParams;
    }
}
