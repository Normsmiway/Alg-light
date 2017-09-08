using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alg.WebApi.Controllers
{
    [RoutePrefix("api/v1")]
    public class AgentController : ApiController
    {
       
        // GET api/<controller>
        [Route("agents")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Route("agent/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [Route("agent/create")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [Route("agent/update")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [Route("agent/delete")]
        public void Delete(int id)
        {
        }
    }
}