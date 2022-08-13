using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Setup
{
    public class WizardDriver
    {
        public WizardDriver()
        {
            Pages = new List<Control>();
            _validatorMap = new Dictionary<int, Func<bool>>();
        }

        public Button NextButton { get; set; }

        public Button PreviousButton { get; set; }

        public Panel PageContainer { get; set; }

        public List<Control> Pages { get; }

        public void SetPageValidator(int pageNo, Func<bool> validator)
        {
            Verify.ArgumentNotOutOfRange(pageNo, 1, Pages.Count, nameof(pageNo));
            Verify.ArgumentNotNull(validator, nameof(validator));
            _validatorMap[pageNo] = validator;
        }

        public void InitWizard(int pageNo = 1)
        {
            if (NextButton != null)
            {
                NextButton.Click += Next_Click;
            }

            if (PreviousButton != null)
            {
                PreviousButton.Click += Previous_Click;
            }

            _currentPageNo = pageNo;
            LoadPage(_currentPageNo);
        }

        private void LoadPage(int pageNo)
        {
            PageContainer?.Controls.Clear();
            PageContainer?.Controls.Add(Pages[pageNo - 1]);
            SetWizardNavigation(pageNo);
        }

        private void SetWizardNavigation(int pageNo)
        {
            if (PreviousButton != null && NextButton != null)
            {
                PreviousButton.Enabled = pageNo > 1;
                NextButton.Enabled = pageNo < Pages.Count;
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (_currentPageNo < Pages.Count)
            {
                var validator = _validatorMap.ContainsKey(_currentPageNo)
                    ? _validatorMap[_currentPageNo]
                    : null;
                if (validator != null && !validator())
                {
                    return;
                }

                _currentPageNo++;
                LoadPage(_currentPageNo);
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (_currentPageNo > 1)
            {
                _currentPageNo--;
                LoadPage(_currentPageNo);
            }
        }

        private int _currentPageNo;
        private IDictionary<int, Func<bool>> _validatorMap;
    }
}
