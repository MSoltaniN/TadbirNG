using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public class JournalViewModel
    {
        public JournalViewModel()
        {
            Items = new List<JournalItemViewModel>();
        }

        public List<JournalItemViewModel> Items { get; }

        public decimal DebitSum { get; set; }

        public decimal CreditSum { get; set; }

        public void SetItems(IEnumerable<JournalItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
