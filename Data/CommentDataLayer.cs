using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Data
{
    public class CommentDataLayer : IComment
    {
        public string convertedDate(string dateCreated)
        {
            
            string currentYear = DateTime.Now.Year.ToString();
            string currentMonth = DateTime.Now.Month.ToString();
            string currentDate = DateTime.Now.Day.ToString();

            string fullDateInString = currentYear + "" + currentMonth + "" + currentDate;

            int fullSystemDate = Convert.ToInt32(fullDateInString);

            int dateCreatedInInt = Convert.ToInt32(dateCreated);

            int totalDays = fullSystemDate - dateCreatedInInt;
            string stringDays = Convert.ToString(totalDays);

            if (fullSystemDate - dateCreatedInInt == 0)
            {
                return "today";
            }
            else
            {
                return stringDays + " days ago";
            } 
        }
    }
}
