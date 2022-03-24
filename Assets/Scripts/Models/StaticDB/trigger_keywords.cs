using SimpleSQL;

namespace Models.StaticDB {
    public class trigger_keywords {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string word { get; set; }
        public int trigger_id { get; set; }
    }
}
