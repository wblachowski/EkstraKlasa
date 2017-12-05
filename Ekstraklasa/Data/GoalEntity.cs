using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class GoalEntity
    {
        public GoalEntity(PlayerEntity Scorer, int Minute, bool HostGoal)
        {
            this.Scorer = Scorer;
            this.Minute = Minute;
            this.HostGoal = HostGoal;
        }

        public PlayerEntity Scorer { get; set; }
        public int Minute;
        public bool HostGoal;
    }
}
