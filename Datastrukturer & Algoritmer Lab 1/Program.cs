using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Utilities;
namespace DataStrukturer_och_Algoritmer_Lab_1
{
    public class Program
    {
        // Properties to store elapsed time information
        public static double ElapsedTotalSeconds { get; private set; }
        private static double processUserTimeStart;

        // Method to restart the process user time measurement
        public static void Restart()
        {
            processUserTimeStart = Process.GetCurrentProcess().UserProcessorTime.TotalSeconds;
        }
        // Method to stop the process user time measurement and return the elapsed time
        public static double Stop()
        {
            ElapsedTotalSeconds = Process.GetCurrentProcess().UserProcessorTime.TotalSeconds - processUserTimeStart;
            return ElapsedTotalSeconds;
        }

        // Method to count word occurrences using a List
        static IEnumerable<KeyValuePair<string, int>> CountUsingList(string path, int maxWords)
        {
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));


                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            var existingItem = myList.FindIndex(x => x.Key == word);
                            if (existingItem != -1)
                            {
                                var item = myList[existingItem];
                                myList[existingItem] = new KeyValuePair<string, int>(item.Key, item.Value + 1);
                                
                            }
                            else
                            {
                                myList.Add(new KeyValuePair<string, int>(word, 1));
                            }

                            if (maxWords > 0)
                            {
                                maxWords--;
                                if (maxWords == 0)
                                    break;
                            }
                        }
                    }

                  
                }
            }

            return myList;
        }

        // Method to count word occurrences using a SortedDictionary
        static IEnumerable<KeyValuePair<string, int>> CountUsingSortedDictionary(string path, int maxWords)
        {
            SortedDictionary<string, int> mySortedDictionary = new SortedDictionary<string, int>();

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            if (mySortedDictionary.TryGetValue(word, out int count))
                            {
                                mySortedDictionary[word] = count + 1;
                            }
                            else
                            {
                                mySortedDictionary.Add(word, 1);
                            }

                            if (maxWords > 0)
                            {
                                maxWords--;
                                if (maxWords == 0)
                                    break;
                            }
                        }
                    }

                }
            }

            return mySortedDictionary;
        }


        // Method to count word occurrences using a Dictionary
        static IEnumerable<KeyValuePair<string, int>> CountUsingDictionary(string path, int maxWords)
        {
            Dictionary<string, int> myDictionary = new();

            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

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
                                myDictionary.Add(word, 1);
                            }

                            if (maxWords > 0)
                            {
                                maxWords--;
                                if (maxWords == 0)
                                    break;
                            }
                        }
                    }

                }
            }

            return myDictionary;
        }

        // Method to count word occurrences using a SortedList
        static IEnumerable<KeyValuePair<string, int>> CountUsingSortedList(string path, int maxWords)
        {
            SortedList<string, int> mySortedList = new();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    foreach(var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            if (mySortedList.TryGetValue(word, out int count))
                            {
                                mySortedList[word] = count + 1;
                            }
                            else
                            {
                                mySortedList.Add(word, 1);
                            }

                            if (maxWords > 0)
                            {
                                maxWords--;
                                if (maxWords == 0)
                                    break;
                            }
                        }
                    }
                   
                }
            }

            return mySortedList;
        }

        // Method to count word occurrences using a Binary Search Tree
        static IEnumerable<KeyValuePair<string, int>> CountUsingBinaryTree(string path, int maxWords)
        {
            BinarySearchTree<string, int> binaryTree = new();
            using (StreamReader streamReader = new StreamReader(path))
            {
                string? line;
                while ((line = streamReader.ReadLine().ToLower()) != null && maxWords > 0)
                {
                    var words = line.Split(' ', '\n', '\t', '\r', ',', '.', ';', ':').Where(x => !string.IsNullOrEmpty(x));

                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            //If the word already exists, the count gets increased by 1
                            if (binaryTree.Contains(word))
                            {
                                binaryTree[word]++;
                            }
                            else
                            {
                                binaryTree.Add(word, 1);
                            }

                            if (maxWords > 0)
                            {
                                maxWords--;
                                if (maxWords == 0)
                                    break;
                            }
                        }
                    }
                }
            }

            return binaryTree;
        }

        // Method to measure the elapsed time and CPU time of a given action
        static double[] Measure(Action action)
        {
            double[] timesDoubles = new double[2];

            Restart();
            Stopwatch sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            //First item has the RealTime while the second item has the CPU time
            timesDoubles[1] = Stop();
            timesDoubles[0] = sw.ElapsedMilliseconds;
            return timesDoubles;
        }

        // Method to check the maximum number of words in a text file
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

                    //Puts the different values into Lists
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

            int wordCounts = CheckMaxWords(@"C:\Users\noelk\Downloads\Texts\Texts\Verne_TwentyThousandLeaguesUnderTheSea.txt");

            Console.WriteLine($"--- Evaluating {type} with Text: {texts[0]} : {wordCounts} words ---");

            Console.WriteLine("\nNumber of words\t\tRealtime (sec)\t\tCPU Time (sec)\t\tMost Frequent Word\tUnique Words");
            for (int i = 0; i < averageTimes.Count; i++)
            {
                Console.WriteLine($"{i * 10000 + 10000}/{wordCounts}\t\t{averageTimes[i].Replace(',',':')}\t\t{averageCpuTimes[i].Replace(',', ':')}\t\t{mostFrequentWords[i]}\t\t{uniqueWords[i]}");
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
                    var myStructure = countMethod(path, i);
                    int uniques = myStructure.Distinct().Count();

                    //Gets the most common word
                    var mostCommonWord = myStructure.OrderByDescending(x => x.Value).FirstOrDefault();
                    double averageRunTime = 0;
                    double averageCpuRunTime = 0;

                    //Runs each experiment 10 times to get an average
                    for (int j = 0; j < 10; j++)
                    {
                        timeSpans.Add(Measure(() => countMethod(path, i)));
                        averageRunTime = timeSpans[j][0];
                        averageCpuRunTime = timeSpans[j][1];

                    }

                    string averageTime = ((averageRunTime/10) / 1000).ToString("0.000000");
                    string averageCpuTime = (averageCpuRunTime/10).ToString("0.000000");

                    streamWriter.WriteLine($"{i},{averageTime.Replace(',', ':')},{averageCpuTime.Replace(',', ':')},{mostCommonWord.Key}({mostCommonWord.Value}),{uniques}");
                    timeSpans.Clear();
                }
            }
        }

        // Main method to run experiments and print results for different ADTs
        static void Main(string[] args)
        {
            string path = @"C:\Users\noelk\Downloads\Texts\Texts\Verne_TwentyThousandLeaguesUnderTheSea.txt";
            string csvFilePath = "times.csv";
           
            // Run experiments for CountUsingDictionary
            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingDictionary);
            PrintResults(csvFilePath, "Dictionary");

            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingList);
            PrintResults(csvFilePath, "List");

            // Run experiments for CountUsingSortedList
            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingSortedList);
            PrintResults(csvFilePath, "SortedList");


            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingBinaryTree);
            PrintResults(csvFilePath, "Binary Tree");

            RunExperimentAndSaveToCSV(path, csvFilePath, CountUsingSortedDictionary);
            PrintResults(csvFilePath, "Sorted Dictionary");

           
        }

    }
}
