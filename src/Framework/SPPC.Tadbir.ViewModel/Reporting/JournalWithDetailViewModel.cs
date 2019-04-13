using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public class JournalWithDetailViewModel
    {
        public JournalWithDetailViewModel()
        {
            Items = new List<JournalWithDetailItemViewModel>();
        }

        public List<JournalWithDetailItemViewModel> Items { get; }

        public decimal DebitSum { get; set; }

        public decimal CreditSum { get; set; }

        public void SetItems(IEnumerable<JournalWithDetailItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
