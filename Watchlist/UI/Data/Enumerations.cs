using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Data
{
    public class Enumerations
    {
        public enum Role { User, Admin };

        [DefaultValue(PlanToWatch)]
        public enum Status { Watching, Watched, PlanToWatch };
    }
}
