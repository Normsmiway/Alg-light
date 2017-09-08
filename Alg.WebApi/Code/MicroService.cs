using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alg.WebApi.Code
{
    public static class MicroService
    {

        static List<double> _cummulativeAdministeredVaccines;
        static IEnumerable<int> _administered;

        static List<ReportModel> _reports;
        static MicroService()
        {
            _cummulativeAdministeredVaccines = new List<double>();
            _administered = new List<int>();
            _reports = new List<ReportModel>();

        }
        public static IEnumerable<ReportModel> GetReports( DateTime startDate, DateTime endDate,int target=83567)
        {
            
            #region variables
            //Local Variables
            List<ReportModel> models = new List<ReportModel>(); //Initialiaze Report Model
            List<int> administered;
            List<DateTime> workingDays;
            double[] Progress;
            List<double> cummulative;
            double rate;
            int i = 0;
            #endregion

            BuildReportData(startDate, endDate, out administered, out workingDays, out Progress, out cummulative, out rate, target);
           
            workingDays.ForEach(day =>
            {
                double wages = administered[i] * rate;

                double cWages = cummulative[i] * rate;

                models.Add(CreateReportModel(day, administered, Progress, cummulative, i, wages, cWages));
                i++;
            });

            _reports = models;

            return models;

        }

        /// <summary>
        /// Returns full comprehensive report
        /// </summary>
        /// 
        public static List<DayModel> GetOperationDays(DateTime startDate,DateTime endDate)
        {
            List<DayModel> days = new List<Code.DayModel>();
            GetWorkinDays(startDate, endDate).ToList().ForEach(day =>
            {
                days.Add(new DayModel()
                {
                    Date = day.ToShortDateString(),
                    Name = day.ToString("dddd")
                });
            });

            return days;
        }

        public static IEnumerable<WorkSchedule> GetWorkSchedule()
        {
            List<WorkSchedule> workSchedules = new List<WorkSchedule>();

            WorkSchedule workSchedule = new WorkSchedule();
            var rate = Analytics.GetRate();
           
           
            _reports.ForEach(schedule =>
            {
                var units = Split((int)schedule.Administered, 50);

                var wages = GetWages(units.ToList(), schedule.Wages, schedule.Administered);
                workSchedules.Add(
                    new WorkSchedule()
                    {
                        Id = schedule.Id,
                        AssignedUnit = units,
                        Administered = (int)schedule.Administered,
                        Wages = wages,
                        DailyWages=schedule.Wages,
                        CalculatedDailyWages=Math.Round(GetSum(wages),0)
            });
            });

            workSchedules.OrderBy(i => i.Id);

            return workSchedules;

        }

        public static IEnumerable<WorkSchedule> GetWorkSchedule(List<ReportModel> models)
        {
            List<WorkSchedule> workSchedules = new List<WorkSchedule>();

            WorkSchedule workSchedule = new WorkSchedule();
            var rate = Analytics.GetRate();


            models?.ForEach(schedule =>
            {
                var units = Split((int)schedule.Administered, 50);

                var wages = GetWages(units.ToList(), schedule.Wages, schedule.Administered);
                workSchedules.Add(
                    new WorkSchedule()
                    {
                        Id = schedule.Id,
                        AssignedUnit = units,
                        Administered = (int)schedule.Administered,
                        Wages = wages,
                        DailyWages = schedule.Wages,
                        CalculatedDailyWages = Math.Round(GetSum(wages), 0)
                    });
            });

            workSchedules.OrderBy(i => i.Id);

            return workSchedules;

        }

        public static IEnumerable<FlatData> GetFlatData(List<WorkSchedule> structuredSchedule)
        {
            var flatData = new List<FlatData>();
            structuredSchedule.ForEach(sd =>
            {
                sd.AssignedUnit.ToList().ForEach(u =>
                {
                    flatData.Add(new FlatData()
                    {
                        // = "Day " +;
                    });
                });
            });
            return flatData;
            
        }
        private static double GetSum(List<double> list)
        {
            double sum = 0;
            list.ForEach(l =>
            {
                sum += l;
            });
            return sum;
        }

        private static List<double> GetWages(List<int> units, double wage,double administerd)
        {
            var list = new List<double>();

            var rate = Analytics.GetRate((int)administerd, wage);
            units.ForEach(a =>
            {
                list.Add(Math.Round(a*rate,2));
            });
            return list;
        }

        #region Private Helper Methods

        private static void BuildReportData(DateTime startDate, DateTime endDate, out List<int> administered, out List<DateTime> workingDays, out double[] Progress, out List<double> cummulative, out double rate, int target = 83567)
        {
            administered =Split(target, (int)GetNumberOfOperationDays(startDate, endDate)).ToList();
            workingDays = GetWorkinDays(startDate, endDate);
            Progress = GetProgress(target, startDate, endDate).ToArray();
            cummulative = _cummulativeAdministeredVaccines;
            rate = Analytics.GetRate();

            //this method has default parameters that can be changed
        }
        private static ReportModel CreateReportModel(DateTime day, List<int> administered, double[] Progress, List<double> cummulative, int i, double wages, double cWages)
        {
            return new ReportModel()
            {
                Id = i + 1,
                Date = day.Date.ToShortDateString(),
                Day = day.ToString("dddd"),
                Progress = Math.Round(Progress[i], 2),
                Administered = administered[i],
                CummulativeAdinistered = cummulative[i],
                Wages = Math.Round(wages, 0),
                CummulativeWages = Math.Round(cWages, 0)
            };
        }
        private static IEnumerable<DateTime> Range(this DateTime startdate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startdate).Days + 1).Select(d => startdate.AddDays(d));
        }
        private static double GetNumberOfOperationDays(DateTime startDate, DateTime endDate)
        {
            double calcBusinessDays = 1 + ((endDate - startDate).TotalDays * 5 - (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

            if (endDate.DayOfWeek == DayOfWeek.Saturday)
            {
                calcBusinessDays--;
            }
            if (endDate.DayOfWeek == DayOfWeek.Sunday)
            {
                calcBusinessDays--;
            }

            return calcBusinessDays;
        }
        private static List<DateTime> GetWorkinDays(DateTime startDate, DateTime endDate)
        {
            return Range(startDate, endDate).Where(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday).ToList();

        }

        private static IEnumerable<int> Split(this int number, int parts)
        {
            var slots = Enumerable.Repeat(0, parts).ToList();
            var random = new Random();
            while (number > 0)
            {
                var slot = random.Next(0, parts);
                slots[slot]++;
                number--;
            }
            return slots;
        }

        private  static IEnumerable<Double> GetProgress(int target,DateTime startDate,DateTime endDate)
        {
            
            var list = new List<double>();

            double sum = 0;
            
            Split(target, (int)GetNumberOfOperationDays(startDate, endDate)).ToList().ForEach(p =>
              {
                  sum += p;
                  _cummulativeAdministeredVaccines.Add(sum);
                  list.Add((sum / target) * 100);
              });

            return list;

        }
        #endregion
    }

    public class FlatData
    {
        public string Id { get; set; }
        public string Agent { get; set; }
        public double Wage { get; set; }
    }

    public class Child
    {
        public string Name { get; set;}
        public int Age { get; set; }
        public string Address { get; set; }
    }
    public class AgentReportModel
    {
        public string AgentFullName { get; set; }
        public List<Child> Children { get; set; }
    }
    public class ReportModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public double Administered{get;set;}

        public double CummulativeAdinistered { get; set; }
        public double Progress { get; set; }

        public double Wages { get; set; }
        public double CummulativeWages { get; set; }
    }
    public class Analytics
    {
        //public static int Target { get; set; }
        //public static int TotalWorkers { get; set; }
        //public static double Budget { get; set; }
        public static double Rate { get; set; }

        public static double GetRate(int Target=83567, double Budget=50000)
        {
            return Budget / Target;
        }

        public static int GetAdministrationAverage(int Target=83567, double TotalWorkers=50)
        {
            double average = Target / TotalWorkers;
            return (int)Math.Round(average, 0);
        }

        public static double GetTaskPay(int Target=83567, double Budget=50000,int number=1)
        {
            double rate = GetRate(Target,Budget);

            return number;
        }

    }

    public class WorkSchedule
    {
        public int Id { get; set; }
        public IEnumerable<int> AssignedUnit { get; set; }
        public IEnumerable<double> Wages { get; set; }
        public int Administered { get; set; }
        public int AssignedUnitCount { get { return this.AssignedUnit.Count(); } }
        public int WagesCount { get { return this.Wages.Count(); } }
        public double DailyWages { get; set; }
        public double CalculatedDailyWages { get; set; }
    }
}