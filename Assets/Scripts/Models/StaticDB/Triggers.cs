using System.Collections.Generic;
using SimpleSQL;

namespace Models.StaticDB {
    public class triggers {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int transition_id { get; set; }
        public List<trigger_keywords> trigger_keywords { get; set; }
        public List<trigger_sentences> trigger_sentences { get; set; }

    }
}
