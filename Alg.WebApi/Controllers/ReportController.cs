using Alg.WebApi.Code;
using Alg.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alg.WebApi.Controllers
{

    [RoutePrefix("api/v1")]
    public class ReportController : ApiController
    {

        List<int> _administered;
        List<ReportModel> _reports;

        public ReportController()
        {
            _administered = new List<int>();
            _reports = new List<ReportModel>();
        }
        [Route("days")]
        public List<DayModel> GetOperationDays()
        {
            return MicroService.GetOperationDays(DateTime.Now, DateTime.Now.AddMonths(2));
        }

        [Route("reports")]
        public IEnumerable<ReportModel> GetAllReports()
        {
            var report = MicroService.GetReports(startDate: DateTime.Now, endDate: DateTime.Now.AddMonths(2), target: 83567);

            _reports = report.ToList();

            return report;
        }
        [Route("data")]
        public AnalyticsData GetAnalytics()
        {
            
            return new AnalyticsData()
            {
                Budget = 50000,
                Target = 83567,
                Span = "2 months",
                Workers = 50,
            };

        }
       [Route("worktable/{id}")]
       public WorkSchedule GetWorkTable([FromBody]List<ReportModel> model,int id)
        {
           // id = 1;
            return MicroService.GetWorkSchedule(model).ToList().
                Where(wk => wk.Id == id).FirstOrDefault();
        }

        [Route("worktable")]

        public IEnumerable<WorkSchedule> GetWorkTable()
        {
            return MicroService.GetWorkSchedule();
        }

        [Route("getworktable")]
        [HttpPost]
        public IEnumerable<WorkSchedule> GenerateWorkTable([FromBody]List<ReportModel> models)
        {
            return MicroService.GetWorkSchedule(models);
        }
        // GET api/<controller>/5
        [Route("report/{id}")]
        public IEnumerable<int> Get(int id)
        {
            var agentReport = new List<AgentReportModel>();
            _reports.ForEach(report =>
            {

            });

            return null;
            //.Where(d => d.Id == id).FirstOrDefault()?.Value ?? $"No value for Id {id}"; //Coersion in play

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}