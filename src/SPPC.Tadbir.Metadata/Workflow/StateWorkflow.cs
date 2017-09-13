using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Metadata.Workflow
{
    /// <summary>
    /// اطلاعات فراداده ای مورد استفاده در گردش کار تغییر وضعیت مستند اداری را نگهداری می کند
    /// </summary>
    public class StateWorkflow
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public StateWorkflow()
        {
            NextActions = new List<StateAction>();
        }

        /// <summary>
        /// شناسه دیتابیسی نوع مستند اداری
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        /// نوع مستند اداری
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// اطلاعات فراداده ای تمام اقدامات تعریف شده برای مستند اداری
        /// </summary>
        public IList<StateAction> AllActions
        {
            get
            {
                var allActions = new List<StateAction>(NextActions);
                allActions.Insert(0, FirstAction);
                return allActions;
            }
        }

        /// <summary>
        /// اطلاعات فراداده ای اولین اقدام تعریف شده برای مستند اداری
        /// </summary>
        public StateAction FirstAction { get; set; }

        /// <summary>
        /// مجموعه ای از اطلاعات فراداده ای اقدامات بعدی برای مستند اداری
        /// </summary>
        public IList<StateAction> NextActions { get; private set; }
    }
}
