using System.Collections.Generic;
using SimpleSQL;

namespace Models.StaticDB {
    public class state_machines {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int scenario_category_id { get; set; }
        public int position { get; set; }
        public string name { get; set; }
        public List<states> states { get; set; }
        public List<transitions> transitions { get; set; }
    }
}
