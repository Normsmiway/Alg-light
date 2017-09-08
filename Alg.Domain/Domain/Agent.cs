using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg.Domain
{
   public partial class Agent:Entity<Agent>
    {
        public string Identification  { get; set; }
        protected override void OnInserting(ref string sql)
        {
            Identification = DomainObject.GetAgentId(this.FirstName, this.LastName);
        }
        
    }
}
