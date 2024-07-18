namespace ToDo.API.Validations
{
    public static class DateValidator
    {
        public static bool ValidateYear(int? year)
        {
            if (year == null)
            {
                return false;
            }

            DateTime currentDate = DateTime.Now;
            int minYear = currentDate.Year - 100;
            int maxYear = currentDate.Year + 100;

            return year >= minYear && year <= maxYear;
        }

        public static bool ValidateMonth(int? month)
        {
            return month != null && month >= 1 && month <= 12;
        }

        public static bool ValidateDay(int? day, int? month = null, int? year = null)
        {
            if (month == null || year == null)
            {
                return day != null && day >= 1 && day <= 31;
            }

            return day != null && day >= 1 && day <= DateTime.DaysInMonth(year.Value, month.Value);
        }
    }
}
