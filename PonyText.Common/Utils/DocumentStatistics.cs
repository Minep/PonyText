namespace PonyText.Common.Utils {
    public class DocumentStatistics {
        public int WordCounts{get;private set;}
        public int NonWSWordCounts{get;private set;}
        public int ParagraphCounts{get;private set;}

        public void CountWords(string text) {
            if(!string.IsNullOrWhiteSpace(text)){
                NonWSWordCounts+=text.Length;
            }
            WordCounts+=text.Length;
        }

        public void CountParagraph() {
            ParagraphCounts++;
        }
    }
}