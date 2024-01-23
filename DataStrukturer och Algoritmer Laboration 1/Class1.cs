namespace DataStrukturer_och_Algoritmer_Laboration_1
{
    public class Class1
    {

        static void Main(string[] args)
        {
            string path = "test";
            List<string> words = ReadWordsFromFile(path, 1000);
        }

        public static List<string> ReadWordsFromFile(string path, int maxWords)
        {
            string text = File.ReadAllText(path);
            string[] words = text.Split(' ', '\n', '\r', '\t', '.', ',');

            List<string> selectedwords = words.Take(maxWords).ToList();

            return selectedwords;
        }

        static IEnumerable<KeyValuePair<string, int>> CountUsingList(string[] words)
        {
            List<KeyValuePair<string, int>> counters = new List<KeyValuePair<string, int>>();

            return counters;
        }
    }
}
