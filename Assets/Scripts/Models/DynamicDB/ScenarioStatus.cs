using SimpleSQL;

namespace Models.DynamicDB {
    public class ScenarioStatus {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [Indexed]
        public int state_machine_id { get; set; }
        public bool is_cleared { get; set; }
    }
}
