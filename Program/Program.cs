using System.Diagnostics;
using System.IO;

namespace DataStrukturer_och_Algoritmer_Laboration_1
{
    public class Program
    {
        public static double ElapsedTotalSeconds { get; private set; }
        private static double processUserTimeStart;

        public static void Restart()
        {
            processUserTimeStart = Process.GetCurrentProcess().UserProcessorTime.TotalSeconds;
        }

        public static double Stop()
        {
            ElapsedTotalSeconds = Process.GetCurrentProcess().UserProcessorTime.TotalSeconds - processUserTimeStart;
            return ElapsedTotalSeconds;
        }

        static IEnumerable<KeyValuePair<string, int>> CountUsingDictionary(string path, int maxWords)
        {
            Dictionary<string, int> myDictionary = new();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && myDictionary.Values.Sum() < maxWords)
                {
                    string[] words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':');
                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            if (myDictionary.TryGetValue(word, out int count))
                            {
                                myDictionary[word] = count + 1;
                            }
                            else
                            {
                                myDictionary[word] = 1;
                            }
                        }

                    }
                }
            }

            return myDictionary;
        }

        static IEnumerable<KeyValuePair<string, int>> CountUsingSortedList(string path, int maxWords)
        {
            SortedList<string, int> mySortedList = new();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && mySortedList.Values.Sum() < maxWords)
                {
                    string[] words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':');
                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int index = mySortedList.IndexOfKey(word);
                            if (index != -1)
                            {
                                mySortedList[word]++;
                            }
                            else
                            {
                                mySortedList.Add(word, 1);
                            }
                        }
                    }
                }
            }

            return mySortedList;
        }


        static string FormatTime(double ms)
        {
            TimeSpan timespan = TimeSpan.FromMilliseconds(ms);
            return timespan.ToString(@"ss\:fffff");
        }

        static double[] Measure(Action action)
        {
            Restart();
            Stopwatch sw = Stopwatch.StartNew();
            action();
            Stop();
            sw.Stop();
            double[] timesDoubles = new double[2];
            timesDoubles[0] = sw.ElapsedMilliseconds;
            timesDoubles[1] = Stop();
            return timesDoubles;
        }

        static void Main(string[] args)
        {
            string path = @"C:\Users\noelk\Downloads\Texts\Texts\Kipling_TheJungleBook.txt";

            List<double[]> timeSpans = new();


            for (int i = 0; i < 10; i++)
            {
                Restart();

                timeSpans.Add(Measure(() => CountUsingDictionary(path, 10000)));

            }
            Console.WriteLine("Average over 10 readings: " + FormatTime(timeSpans[0].Average()) + " sec");
            Console.WriteLine("Average CPU over 10 readings: " + FormatTime(timeSpans[1].Average()) + " sec");

            timeSpans = new();

            for (int i = 0; i <= 10; i++)
            {
                Restart();

                timeSpans.Add(Measure(() => CountUsingSortedList(path, 10000)));

            }
            Console.WriteLine("Average over 10 readings: " + FormatTime(timeSpans[0].Average()) + " sec");
            Console.WriteLine("Average CPU over 10 readings: " + FormatTime(timeSpans[1].Average()) + " sec");
        }

        /*

        static IEnumerable<KeyValuePair<string, int>> CountUsingList(string[] words)
        {
            List<KeyValuePair<string, int>> counters = new List<KeyValuePair<string, int>>();

            return counters;
        }*/
    }
}
