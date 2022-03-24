using SimpleSQL;

namespace Models.StaticDB {
    public class trigger_sentences {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string body_en { get; set; }
        public string body_ja { get; set; }
        public int trigger_id { get; set; }
        public int position { get; set; }
    }
}
