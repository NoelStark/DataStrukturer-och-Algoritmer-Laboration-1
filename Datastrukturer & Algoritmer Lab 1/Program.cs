using System.Diagnostics;
using System.IO;
using System.Linq;
using Utilities;
namespace DataStrukturer_och_Algoritmer_Lab_1
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
            bool reachedMaxWords = false;

            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    words.ToList().ForEach(word =>
                    {
                        if (!reachedMaxWords)
                        {
                            if (myDictionary.TryGetValue(word, out int count))
                            {
                                myDictionary[word] = count + 1;
                            }
                            else
                                myDictionary[word] = 1;

                            if (maxWords > 0)
                            {
                                maxWords--;
                                if (maxWords == 0)
                                    reachedMaxWords = true;
                            }
                            else
                                reachedMaxWords = true;
                        }
                       
                    });
                  
                }
            }

            return myDictionary;
        }


        static IEnumerable<KeyValuePair<string, int>> CountUsingSortedList(string path, int maxWords)
        {
            SortedList<string, int> mySortedList = new();
            bool reachedMaxWords = false;
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    words.ToList().ForEach(word =>
                    {
                        if (!reachedMaxWords)
                        {
                            int index = mySortedList.IndexOfKey(word);
                            if (index != -1)
                                mySortedList[word]++;

                            else
                                mySortedList.Add(word, 1);

                            if (maxWords > 0)
                            {
                                maxWords--;

                                if(maxWords == 0)
                                    reachedMaxWords = true;
                            }
                            else
                                reachedMaxWords = true;
                            
                        }
                        
                    });
                }
            }

            return mySortedList;
        }
        static IEnumerable<KeyValuePair<string, int>> CountUsingBinaryTree(string path, int maxWords)
        {
            BinarySearchTree<string, int> binaryTree = new();
            bool reachedMaxWords = false;
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    words.ToList().ForEach(word =>
                    {
                        if (!reachedMaxWords)
                        {
                            if (binaryTree.Contains(word))
                                binaryTree[word]++;
                            else
                            binaryTree.Add(word, 1);

                            if (maxWords > 0)
                            {
                                maxWords--;

                                if (maxWords == 0)
                                    reachedMaxWords = true;
                            }
                            else
                                reachedMaxWords = true;

                        }

                    });
                }
            }

            return binaryTree;
        }



        static string FormatTime(double ms)
        {
            TimeSpan timespan = TimeSpan.FromMilliseconds(ms);
            return timespan.ToString(@"ss\:fffff");
        }

        static double[] Measure(Action action)
        {
            double[] timesDoubles = new double[2];

            Restart();
            Stopwatch sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            timesDoubles[1] = Stop();
            timesDoubles[0] = sw.ElapsedMilliseconds;
            return timesDoubles;
        }

        /// <summary>
        /// Gets the amount of words in a file
        /// </summary>
        /// <param name="path">Path to the text file</param>
        /// <returns></returns>
        private static int CheckMaxWords(string path)
        {
            int count = 0;
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    foreach (var word in words)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Reads all the information from the CSV file
        /// </summary>
        /// <param name="path">Path to the CSV File</param>
        /// <param name="averageTimes">A list that contains all the average times in the CSV File</param>
        /// <param name="averageCpuTimes">A list that contains all the average CPU times in the CSV File</param>
        /// <param name="mostFrequentWords">A list containing the most frequent words</param>
        /// <param name="uniqueWords">A list containing the amount of unique words</param>
        static void ReadResultsFromCSV(string path, out List<string> averageTimes, out List<string> averageCpuTimes, out List<string> mostFrequentWords, out List<string> uniqueWords)
        {
            averageTimes = new List<string>();
            averageCpuTimes = new List<string>();
            mostFrequentWords = new List<string>();
            uniqueWords = new List<string>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] values = line.Split(',');
                        averageTimes.Add(values[1]);
                        averageCpuTimes.Add(values[2]);
                        mostFrequentWords.Add(values[3]);
                        uniqueWords.Add(values[4]);
                    }
                }
            }
        }
        /// <summary>
        /// Method responsible for printing information to the console
        /// </summary>
        /// <param name="path">Path to the CSV thats passed to another Method responsible for reading from CSV</param>
        /// <param name="type">Variable containing what kind of ADT that is being used</param>
        static void PrintResults(string path,string type)
        {
            List<string> averageTimes, averageCpuTimes, mostFrequentWords, uniqueWords;
            ReadResultsFromCSV(path, out averageTimes, out averageCpuTimes, out mostFrequentWords, out uniqueWords);

            string[] texts = { "Verne_TwentyThousandLeaguesUnderTheSea.txt" };

            int wordCounts = CheckMaxWords(@"F:\Downloads\Texts\Verne_TwentyThousandLeaguesUnderTheSea.txt");

            Console.WriteLine($"--- Evaluating {type} with Text: {texts[0]} : {wordCounts} words ---");

            Console.WriteLine("\nNumber of words\t\tRealtime (sec)\t\tCPU Time (sec)\t\tMost Frequent Word\tUnique Words");
            for (int i = 0; i < averageTimes.Count; i++)
            {
                Console.WriteLine($"{i * 10000 + 10000}/{wordCounts}\t\t{averageTimes[i]:F6}\t\t{averageCpuTimes[i]:F6}\t\t{mostFrequentWords[i]}\t\t{uniqueWords[i]}");
            }
        }

        /// <summary>
        /// The method that runs the experiment and writes to file
        /// </summary>
        /// <param name="path">Path to the Text File</param>
        /// <param name="csvFilePath">Path to the CSV File</param>
        /// <param name="countMethod">A function that adds re-usability of code where you can pass in desired method handling different ADTs</param>
        static void RunExperimentAndSaveToCSV(string path, string csvFilePath, Func<string, int, IEnumerable<KeyValuePair<string, int>>> countMethod)
        {
            List<double[]> timeSpans = new List<double[]>();

            using (StreamWriter streamWriter = new StreamWriter(csvFilePath))
            {
                streamWriter.WriteLine("NumberOfWords,AverageTime,AverageCpuTime");
                int maxWords = CheckMaxWords(path);
                for (int i = 10000; i < maxWords; i += 10000)
                {
                    var myDictionary = countMethod(path, i);
                    int uniques = myDictionary.Distinct().Count();

                    var mostCommonWord = myDictionary.OrderByDescending(x => x.Value).FirstOrDefault();

                    for (int j = 0; j < 10; j++)
                    {
                        Restart();
                        timeSpans.Add(Measure(() => countMethod(path, i)));
                    }

                    string averageTime = FormatTime(timeSpans[0].Average());
                    string averageCpuTime = FormatTime(timeSpans[1].Average());

                    streamWriter.WriteLine($"{i},{averageTime},{averageCpuTime},{mostCommonWord.Key}({mostCommonWord.Value}),{uniques}");
                    timeSpans.Clear();
                }
            }
        }

        static void Main(string[] args)
        {
            string path = @"F:\Downloads\Texts\Verne_TwentyThousandLeaguesUnderTheSea.txt";
            string csvFilePath = "times.csv";

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Run experiments for CountUsingDictionary
            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingDictionary);
            PrintResults(csvFilePath, "Dictionary");
 
            // Run experiments for CountUsingSortedList
            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingSortedList);
            PrintResults(csvFilePath, "SortedList");

            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingBinaryTree);
            PrintResults(csvFilePath, "SortedList");

        }

    }
}
