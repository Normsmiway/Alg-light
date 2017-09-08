using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alg.WebApi.Models
{
    public class ReportApi
    {
        public int Target { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}