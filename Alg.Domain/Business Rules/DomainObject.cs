using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg.Domain
{
    public class DomainObject
    {
        static Agent _agent;
        public DomainObject()
        {

        }

        public DomainObject(Agent agent)
        {
            _agent = agent;
        }

        public static string GetAgentId(string f, string l,int number)
        {

            string value = "";

            var f1 = f.Substring(0, 1).ToUpper(); 
            var l1 = l.Substring(0, 1).ToUpper();


            var rand0 = DateTime.Now.Millisecond;
          //  var rand1 = DateTime.Now.Second != 0 ? DateTime.Now.Second : 1;

            var rand = Math.Abs(DateTime.Now.Minute * Math.Exp(rand0 / number));


            var fl = f.Substring(f.Count() - 1, 1).ToUpper();
            var ll = l.Substring(l.Count() - 1, 1).ToUpper();

            string Id = f1 + ll + rand;// + fl + l1;

            var data = Id.Split(new char[] { '.', '+' });
            data.ToList().ForEach(d =>
            {
                value += d;
            });

            return value;

        }
        public static string GetAgentId(string f, string l)
        {

            string value = "";

            var f1 = f.Substring(0, 1).ToUpper();
            var l1 = l.Substring(0, 1).ToUpper();


            var rand0 = DateTime.Now.Millisecond;
            var rand1 = DateTime.Now.Second != 0 ? DateTime.Now.Second : 1;

            var rand = Math.Abs(DateTime.Now.Minute * Math.Exp(rand0));


            var fl = f.Substring(f.Count() - 1, 1).ToUpper();
            var ll = l.Substring(l.Count() - 1, 1).ToUpper();

            string Id = f1 + ll + rand + fl + l1;

            var data = Id.Split(new char[] { '.', '+' });
            data.ToList().ForEach(d =>
            {
                value += d;
            });

            return value;

        }
    }

    public class Analytics
    {
        public int Population { get; } = 50;
        public TimeSpan TimeSpan { get; } = new TimeSpan(60, 0, 0, 0, 0);
        public double Budget { get;} = 50000;

    }

    public class Demographic
    {
        public double MaleAboveTen { get; } = 30000;
        public double FemaleBetweenTenToFifteen { get; }=40345;
        public double ChildrenBelowAgeTen { get; } = 83567;
        public double FemaleAboveAgeFifteen { get; } = 32256;

    }

    public class ProgressAnalytics
    {
        public double Rate { get; set; } = 50;
        public int AgentAverageWorkHour { get; set; } = 4;
        public int AverageVaccinatedChildrenPerHour { get; set; } = 168;
        public int AverageVaccinatedChildrenPerDayPerGroupOfTwo { get; set; } = 672;
    }
    //Business Rules
    /*
     * No Worker should work more than 20hrs 
     * Rate is $50/hr
     * Average workhour/day/Agent is 4hrs
     * Average Number of children that must be vaccinated/hr =168 (or 167)
     * Average Number of children that must be vaccinated/day/GroupOfTwo =672 ( or 668)
     * Average Number of children that must be vaccinated/day/DailyGroup =672*5=3360 (or 3343)  children/day/fiveGroup in a day
     * 
     */

    /// <summary>
    //Once an Agent is registered, create a schedule table for him/her
    //The schedule table is such the each Agent has 10 entries
    /// </summary>
    public class Task
    {
       public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Task(DateTime start, DateTime end)
        {
            StartDate = start;
            end = StartDate;
            var d = DateTime.DaysInMonth(2017, 9);
            TimeSpan s = new TimeSpan();
            //+ DateTime.Now.AddDays(41);
            EndDate = end;
        }
        public void Schedule()
        {

        }
    }
    
}