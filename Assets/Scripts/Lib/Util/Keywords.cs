using System;

namespace Lib.Util {
    public class Keywords {
        public string[] values { get; private set; }
        public Keywords (string[] keywords) {
            this.values = keywords;
        }

        public bool include (Keywords words) {
            foreach (string word in words.values) {
                if (!Array.Exists (this.values, p => p.ToLower () == word.ToLower ())) {
                    return false;
                }
            }
            return true;
        }
    }
}
