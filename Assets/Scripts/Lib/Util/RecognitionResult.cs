namespace Lib.Util {
    public class RecognitionResult {

        public string result { get; private set; }

        public RecognitionResult (string result) {
            this.result = result;
        }

        public Keywords getKeywords () {
            return new Keywords (result.Split (new string[] { " ", ".", "　" }, System.StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
