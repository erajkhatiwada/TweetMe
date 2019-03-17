using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Data
{
    public class CommentDataLayer : IComment
    {
        public string convertedDate(string dateCreated)
        {

            try
            {
                var date = DateTime.Parse(dateCreated, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                DateTime dateTime = DateTime.UtcNow.Date;
                var x = (dateTime.Date - date.Date).Days;
                if (x == 0)
                {
                    return "today";
                }
                else
                {
                    return x + " days ago";
                }
            }
            catch
            {
                string currentYear = DateTime.Now.Year.ToString();
                string currentMonth = DateTime.Now.Month.ToString();
                if (currentMonth.Length < 2)
                {
                    currentMonth = "0" + currentMonth;
                }
                string currentDate = DateTime.Now.Day.ToString();
                if (currentDate.Length < 2)
                {
                    currentDate = "0" + currentDate;
                }

                string fullDateInString = currentYear + "" + currentMonth + "" + currentDate;

                int fullSystemDate = Convert.ToInt32(fullDateInString);

                int dateCreatedInInt = Convert.ToInt32(dateCreated);

                int totalDays = fullSystemDate - dateCreatedInInt;
                string stringDays = Convert.ToString(totalDays);

                if (fullSystemDate - dateCreatedInInt == 0)
                {
                    return "today";
                }
                else if (fullSystemDate - dateCreatedInInt != 1)
                {
                    return stringDays + " days ago";
                }
                else
                {
                    return ("1 day ago");
                }
            }

           

            //return fullSystemDate + "";
        }
    }
}
