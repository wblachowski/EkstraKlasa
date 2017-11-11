using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class GoalEntity
    {
        public GoalEntity(int Minute, string Firstname, string Lastname, bool HostGoal)
        {
            this.Minute = Minute;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.HostGoal = HostGoal;
        }

        public int Minute;
        public string Firstname;
        public string Lastname;
        public bool HostGoal;
    }
}
