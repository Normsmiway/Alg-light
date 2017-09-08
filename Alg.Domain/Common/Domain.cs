using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg.Domain
{
    public partial class Report : Entity<Report>
    {
        public Report()
        {

        }
        public Report(bool defaults) : base(defaults)
        {

        }

    }

    public partial class Agent : Entity<Agent>
    {
        public Agent()
        {

        }
        public Agent(bool defaults) : base(defaults)
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgentIdentification { get; set; }

    }
    public partial class Schedule : Entity<Schedule>
    {
        public Schedule()
        {

        }
        public Schedule(bool defaults) : base(defaults)
        {

        }
        public int Id { get; set; }
        public int WorkHour { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public int GroupId { get; set; }
    }

    public partial class Group : Entity<Group>
    {
        public Group()
        {
                
        }
        public Group(bool defaults):base(defaults)
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfMembers { get; set; }
    }
}
