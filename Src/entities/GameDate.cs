using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class GameDate
    {
        private int delay;
        private int day;
        private int month;
        private int year;

        public GameDate()
        {
            this.delay = 0;
            this.day = 1;
            this.month = 1;
            this.year = 1;
        }

        public void Update()
        {
            if(delay++ == 210)
            {
                delay = 0;
                if(day++ == 30)
                {
                    day = 1;
                    if(month++ == 12)
                    {
                        month = 1;
                        year++;
                    }
                }
            }
        }

        public string DateToString()
        {
            string date = "";
            if(day < 10)
                date += "0" + day + "-";
            else
                date += day + "-";
            if (month < 10)
                date += "0" + month + "-";
            else
                date += month + "-";
            if (year < 1000)
            {
                if (year < 100)
                {
                    if (year < 10)
                        date += "000" + year;
                    else
                        date += "00" + year;
                } else
                    date += "0" + year;
            } else
                date += year;
            return date;
        }
    }
}
