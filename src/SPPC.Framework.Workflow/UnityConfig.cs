using System;
using SPPC.Framework.Unity;
using SwForAll.Platform.Persistence;

namespace SPPC.Framework.Workflow
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public sealed class UnityConfig
    {
        private UnityConfig()
        {
        }

        #region Unity Container

        private static Lazy<TypeContainer> container = new Lazy<TypeContainer>(() =>
        {
            TypeContainer temp = null;
            TypeContainer container = null;
            try
            {
                temp = new TypeContainer();
                temp.RegisterAll();
                var nhibernate = temp.Get<IORMapper>();
                nhibernate.Initialize();
                container = temp;
                temp = null;
            }
            finally
            {
                if (temp != null)
                {
                    temp.Dispose();
                }
            }

            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static TypeContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion
    }
}
