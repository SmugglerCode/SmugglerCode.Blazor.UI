namespace Intilium.Sandbox.Blazor.Components.UI.Calendar
{
    public partial class Calendar
    {
        private readonly string[] _days = ["Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"];
        private readonly string[] _months = ["Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December"];

        public List<CalendarRow> Rows { get; set; } = [];

        /// <summary>
        /// Gets or sets the current month.
        /// </summary>
        public int CurrentMonth { get; set; } = 0;

        public int CurrentYear { get; set; } = 0;
        public Calendar()
        {
            var today = DateTime.Now;
            CurrentMonth = today.Month;
            CurrentYear = today.Year;

            CreateMonth(CurrentYear, CurrentMonth);
        }

        public void NextMonth()
        {
            if (CurrentMonth == 12)
            {
                CurrentMonth = 1;
                CurrentYear = CurrentYear + 1;
            }
            else
            {
                CurrentMonth = CurrentMonth + 1;
            }

            CreateMonth(CurrentYear, CurrentMonth);
        }

        public void PreviousMonth()
        {
            if (CurrentMonth == 1)
            {
                CurrentMonth = 12;
                CurrentYear = CurrentYear - 1;
            }
            else
            {
                CurrentMonth = CurrentMonth - 1;
            }

            CreateMonth(CurrentYear, CurrentMonth);
        }

        public void CreateMonth(int year, int month)
        {
            Rows.Clear();

            var firstDay = new DateOnly(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1).Day;
            var firstDayOfWeek = (int)firstDay.DayOfWeek;
            var offset = firstDayOfWeek == 0 ? 6 : firstDayOfWeek - 1;

            var row = new CalendarRow();
            var lastCreatedDay = row.CreateCalendarRow(offset, 1, lastDay, year, month);
            Rows.Add(row);

            while (lastCreatedDay < lastDay)
            {
                var nextRow = new CalendarRow();
                lastCreatedDay = nextRow.CreateCalendarRow(0, lastCreatedDay + 1, lastDay, year, month);
                Rows.Add(nextRow);
            }
        }
    }

    public sealed class CalendarRow
    {
        public List<CalendarDay> Days { get; set; } = [];

        /// <summary>
        /// The offset is needed for the first empty cells, only needed for the first week since a month doesn't start at the first item.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="start"></param>
        public int CreateCalendarRow(int offset, int start, int maxDay, int year, int month)
        {
            var day = 0;
            var today = DateTime.Now;
            var dayCounter = 0;
            for (var i = 0; i < 7; i++)
            {
                var cell = new CalendarDay();
                Days.Add(cell);

                if (i >= offset)
                {
                    day = start + dayCounter;
                    if (day <= maxDay)
                    {
                        cell.Label = $"{day}";
                        cell.IsToday = today.Year == year && today.Month == month && today.Day == day;
                        dayCounter++;
                    }
                }
            }

            // returns the last added day
            return day;
        }
    }

    public sealed class CalendarDay
    {
        public DateOnly Day { get; set; }
        public string Label { get; set; } = string.Empty;
        public bool IsToday { get; set; } = false;
    }

}
