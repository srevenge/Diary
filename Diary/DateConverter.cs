using System;

namespace Model
{
    class DateConverter
    {
        private DateConverter()
        { }

        public static String Convert(PersianDateTime pc)
        {
            int diffYear = PersianDateTime.Now.Year - pc.Year;
            if (diffYear == 0)
            {
                int diffMonth = PersianDateTime.Now.Month - pc.Month;
                if (diffMonth == 0)
                {
                    int diffDay = PersianDateTime.Now.Year - pc.Year;
                    if (diffDay == 0)
                    {
                        int diffHour = PersianDateTime.Now.Hour - pc.Hour;
                        if (diffHour == 0)
                        {
                            int diffMinute = PersianDateTime.Now.Minute - pc.Minute;
                            if (diffMinute == 0)
                                return "چندی پیش";

                            else
                                return String.Format("{0} دقیقه پیش", diffMinute);
                        }
                        else
                            return String.Format("{0} ساعت پیش", diffHour);
                    }
                    else
                        return String.Format("{0} روز پیش", diffDay);
                }
            else
                    return String.Format("{0} ماه پیش", diffMonth);
            }
            else
                return String.Format("{0} سال پیش", diffYear);
        }
    }
}
