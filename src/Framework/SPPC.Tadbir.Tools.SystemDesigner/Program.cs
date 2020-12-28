using System;
using System.Globalization;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CalendarLeapDemo();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        static void CalendarLeapDemo()
        {
            var greg = new GregorianCalendar();
            var pers = new PersianCalendar();
            string leapQA = String.Format(@"
Q: Is 2020 leap year? A: {0}
Q: Is Feb 2020 leap month? A: {1}
Q: Is 29 Feb 2020 leap day? A: {2}
Q: Is 2021 leap year? A: {3}
Q: Is Feb 2021 leap month ? A : {4}
Q: Is 28 Feb 2021 leap day? A: {5}

Q: Is 1399 leap year? A: {6}
Q: Is Esf 1399 leap month? A: {7}
Q: Is 30 Esf 1399 leap day? A: {8}
Q: Is 1398 leap year? A: {9}
Q: Is Esf 1398 leap month ? A : {10}
Q: Is 29 Esf 1398 leap day? A: {11}",
                greg.IsLeapYear(2020), greg.IsLeapMonth(2020, 2), greg.IsLeapDay(2020, 2, 29),
                greg.IsLeapYear(2021), greg.IsLeapMonth(2021, 2), greg.IsLeapDay(2021, 2, 28),
                pers.IsLeapYear(1399), pers.IsLeapMonth(1399, 12), pers.IsLeapDay(1399, 12, 30),
                pers.IsLeapYear(1398), pers.IsLeapMonth(1398, 12), pers.IsLeapDay(1398, 12, 29));
        }
    }
}
