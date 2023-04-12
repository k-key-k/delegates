using Microsoft.VisualBasic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace delegates
{
    internal class Program
    {
        delegate double Func(double x);

        static void PrintFunc(double a, double b, double step, Func del) // Вывод значений функции с пользовательским делегатом
        {
            Console.WriteLine("\nЗначения функции {3} на отрезке [{0:f3}, {1:f3}] с шагом {2:f3}", a, b, step, del.Method.Name);

            for (double i = a; i < b; i += step)
            {
                Console.WriteLine("x = {0:f3}\t f(x) = {1:f3}", i, del(i));
            }
        }
        static void PrintFuncTest(double a, double b, double step, Func<double, double> del) // Вывод значений функции с встроенным делегатом
        {
            Console.WriteLine("\nЗначения функции {3} на отрезке [{0:f3}, {1:f3}] с шагом {2:f3}", a, b, step, del.Method.Name);

            for (double i = a; i < b; i += step)
            {
                Console.WriteLine("x = {0:f3}\t f(x) = {1:f3}", i, del(i));
            }
        }

        static int Counter(double a, double b, double step, Func del, Predicate<double> predicte) // счётчик для задания г
        {
            int count = 0;
            for (double i = a; i <= b; i += step)
            {
                if (predicte(del(i)))
                {
                    count++;
                }
            }
            return count;
        }

        static double[,] MinMax(Func<double, double>[] func, double[] points)
        {
            double[,] results = new double[func.Length, 2]; // массив максимума и минимума

            for (int i = 0; i < func.Length; i++)
            {
                double MinV = double.MaxValue;
                double MaxV = double.MinValue;

                for (int j = 1; j < points.Length; j++)
                {
                    double value = func[i](points[j]);
                    if (value < MinV)
                    {
                        MinV = value;
                    }
                    if (value > MaxV)
                    {
                        MaxV = value;
                    }
                }
                results[i, 0] = MinV;
                results[i, 1] = MaxV;
            }
            return results;
        }

        static double Cosinus(double x) // Task a.a
        {
            return Math.Cos(x);
        }
        static double SqrtArifm(double x) // Task a.b
        {
            return 2 * Math.Sqrt(Math.Abs(x - 1)) + 1;
        }
        static double Pishka(double x) // Task a.c
        {
            return -1 * Math.Pow(x / Math.PI, 2) - 2 * x + 5 * Math.PI;
        }
        static double Infinity(double x) // Task a.d
        {
            double sum = 0.0;
            for (int i = 1; i < 101; i++)
            {
                sum += Math.Pow((x / (Math.PI * i)) - 1, 2);
            }
            return sum;
        }
        static double Ways(double x) // Task a.e
        {
            if (x < 0)
            {
                return 0.25 * Math.Pow(Math.Sin(x), 2) + 1;
            }
            else
            {
                return 0.5 * Math.Cos(x) - 1;
            }
        }
        static void Main(string[] args)
        {
            // Task a

            /*Func func = Cosinus;
            PrintFunc(Math.PI * -2, Math.PI * 2, Math.PI / 6, func);
            func = SqrtArifm;
            PrintFunc(Math.PI * -2, Math.PI * 2, Math.PI / 6, func);
            func = Pishka;
            PrintFunc(Math.PI * -2, Math.PI * 2, Math.PI / 6, func);
            func = Infinity;
            PrintFunc(Math.PI * -2, Math.PI * 2, Math.PI / 6, func);
            func = Ways;
            PrintFunc(Math.PI * -2, Math.PI * 2, Math.PI / 6, func);*/

            // Task б

            /*Func<double, double> Function = Cosinus;
            PrintFuncTest(Math.PI * -2, Math.PI * 2, Math.PI / 6, Function);*/

            // Task с

            /*Func[] AllFunc = {Cosinus, SqrtArifm, Pishka, Infinity, Ways};
            foreach (var del in AllFunc)
            {
                PrintFunc(Math.PI * -2, Math.PI * 2, Math.PI / 6, del);
            }*/

            // Task г

            /*int Negative_Count = 0;
            int OneToOne_Count = 0;
            
            foreach (var del in AllFunc)
            {
                Negative_Count += Counter(Math.PI * -2, Math.PI * 2, Math.PI / 6, del, x => x < 0);
                OneToOne_Count += Counter(Math.PI * -2, Math.PI * 2, Math.PI / 6, del, x => x >= -1 && x <= 1);
            }

            Console.WriteLine("Количество отрицательных значений: {0}\nКоличество значений на отрезке [-1,1]: {1}", Negative_Count, OneToOne_Count);*/

            // Task д

            /*Random rnd = new Random();
            int num = 20;
            double[] points = new double[num];
            for (int i = 0; i < num; i++)
            {
                points[i] = rnd.NextDouble() * 20 - 10;
            }

            Func<double, double>[] AllFunction = new Func<double, double>[]
            {
                Cosinus,
                SqrtArifm,
                Pishka,
                Infinity,
                Ways
            };

            double[,] MinMaxValue = MinMax(AllFunction, points);
            for (int i = 0; i < AllFunction.Length; i++)
            {
                Console.WriteLine("{0}: min = {1:f3}, max = {2:f3}", AllFunction[i].Method.Name, MinMaxValue[i, 0], MinMaxValue[i, 1]);
            }*/

            // Task 2

            string[] words = { "olofmeister", "SIMPLE", "tenet", "1234567890", "White", "all-inclusive" };

            Func<string, bool> Palindrom = s =>
            {
                s = s.Replace(" ", "").ToLower();

                for (int i = 0; i < s.Length / 2; i++)
                {
                    if (s[i] != s[s.Length - i - 1])
                    {
                        return false;
                    }
                }
                return true;
            };

            int Lower = words.Count(s => Regex.IsMatch(s, "[a-z]"));
            Console.WriteLine("Количество строк без заглавных букв - {0}", Lower);

            int Ten = words.Count(s => Regex.IsMatch(s, "^.{10}$"));
            Console.WriteLine("Количество десятисимвольных строк - {0}", Ten);

            int Letters = words.Count(s => Regex.IsMatch(s, "\\b\\w{5}\\b"));
            Console.WriteLine("Количество пятисимвольных слов в строке - {0}", Letters);

            List<string> W = words.Where(s => Regex.IsMatch(s, @"\b[Ww]\w*\b")).ToList();
            Console.WriteLine($"Слова, начинающиеся на 'W': {string.Join(", ", W)}");

            List<string> defis = words.Where(s => Regex.IsMatch(s, @"\b\w+-\w+\b")).ToList();
            Console.WriteLine($"Слова с дефисом: {string.Join(", ", defis)}");
        }       
    }
}