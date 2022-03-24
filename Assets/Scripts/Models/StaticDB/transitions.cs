using System.Collections.Generic;
using SimpleSQL;

namespace Models.StaticDB {
    public class transitions {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int state_machine_id { get; set; }
        public int prev_state_id { get; set; }
        public int next_state_id { get; set; }
        public customer_actions customer_action { get; set; }
        public triggers trigger { get; set; }
    }
}
