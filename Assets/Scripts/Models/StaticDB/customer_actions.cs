using SimpleSQL;

namespace Models.StaticDB {
    public class customer_actions {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public int transition_id { get; set; }
        public string text_en { get; set; }
        public string text_ja { get; set; }
    }
}
