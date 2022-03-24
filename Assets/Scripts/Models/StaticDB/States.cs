using System.Collections.Generic;
using SimpleSQL;

namespace Models.StaticDB {
    public class states {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public bool is_init { get; set; }
        public bool is_accept { get; set; }
        public int state_machine_id { get; set; }
        public List<transitions> nextTransitions { get; set; }
        public List<transitions> prevTransitions { get; set; }
    }
}
