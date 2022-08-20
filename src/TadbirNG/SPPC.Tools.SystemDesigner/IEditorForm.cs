using System;

namespace SPPC.Tools.SystemDesigner
{
    public interface IEditorForm
    {
        object Entity { get; set; }

        event EventHandler<EventArgs> Saved;

        event EventHandler<EventArgs> Cancelled;
    }
}
