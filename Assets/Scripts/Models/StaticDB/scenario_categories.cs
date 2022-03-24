using System.Collections.Generic;
using SimpleSQL;

namespace Models.StaticDB {
    public class scenario_categories {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string info { get; set; }
    }
}
