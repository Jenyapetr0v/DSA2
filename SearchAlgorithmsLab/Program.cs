using System;
using System.Diagnostics;
using System.Text;

namespace SearchAlgorithmsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; 
            Console.WriteLine("--- Завдання А: Дослідження методів пошуку ---\n");

            int n = 10000; 
            int m = 1000;  

            int[] array = new int[n];
            int[] keys = new int[m];
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                array[i] = rand.Next(0, n);
            }

            for (int i = 0; i < m; i++)
            {
                keys[i] = rand.Next(0, n);
            }

            Stopwatch swSequential = Stopwatch.StartNew();
            int seqFoundCount = 0;
            foreach (int key in keys)
            {
                if (SequentialSearch(array, key) != -1)
                {
                    seqFoundCount++;
                }
            }
            swSequential.Stop();
            Console.WriteLine($"Послідовний пошук:");
            Console.WriteLine($"Знайдено елементів: {seqFoundCount} з {m}");
            Console.WriteLine($"Витрачений час: {swSequential.ElapsedTicks} тіків\n");

            Array.Sort(array);

            Stopwatch swInterpolation = Stopwatch.StartNew();
            int intFoundCount = 0;
            foreach (int key in keys)
            {
                if (InterpolationSearch(array, key) != -1)
                {
                    intFoundCount++;
                }
            }
            swInterpolation.Stop();
            Console.WriteLine($"Інтерполяційний пошук:");
            Console.WriteLine($"Знайдено елементів: {intFoundCount} з {m}");
            Console.WriteLine($"Витрачений час: {swInterpolation.ElapsedTicks} тіків\n");
            Console.WriteLine("----------------------------------------------\n");

            Console.WriteLine("--- Завдання Б: Обробка рядка ---");
            Console.WriteLine("Введіть рядок символів латинського алфавіту:");

            string input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                StringBuilder result = new StringBuilder();

                result.Append(input[0]);

                for (int i = 1; i < input.Length; i++)
                {
                    if (char.IsUpper(input[i]))
                    {
                        result.Append(" "); 
                    }
                    result.Append(input[i]);
                }

                Console.WriteLine($"\nОтриманий рядок: {result.ToString()}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        static int SequentialSearch(int[] arr, int key)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == key)
                    return i;
            }
            return -1; 
        }

        static int InterpolationSearch(int[] arr, int key)
        {
            int low = 0;
            int high = arr.Length - 1;

            while (low <= high && key >= arr[low] && key <= arr[high])
            {
                if (low == high)
                {
                    if (arr[low] == key) return low;
                    return -1;
                }

                if (arr[high] == arr[low])
                {
                    if (arr[low] == key) return low;
                    return -1;
                }

                int pos = low + (((key - arr[low]) * (high - low)) / (arr[high] - arr[low]));

                if (arr[pos] == key)
                    return pos;

                if (arr[pos] < key)
                    low = pos + 1;
                else
                    high = pos - 1;
            }
            return -1;
        }
    }
}