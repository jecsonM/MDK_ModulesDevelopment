using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Program
    {
        //Задание 1

        static void Main(string[] args)
        {
            //Console.WriteLine("Будет создан массив вместительностью n и выведен на экран.");
            //Console.WriteLine("Если кол-во нечётных эл-ов по значению будет более 3, то выведется прои-ие положительных эл-ов");
            //Console.WriteLine("Иначе отрицательные элементы будут увеличены на 3.");
            //Console.Write("Введите n:");
            //byte n;
            //Console.Write("n: ");
            //while (!(byte.TryParse(Console.ReadLine(), out n))) Console.Write("Введите n (от 0 до 255): "); ; //Ввод длины массива
            //int[] mas = new int[n];

            //Random random = new Random(50); //Зерно 50 для проверки. Ввод 15, про-ие= 21372385238131200, что верно (проверено калькуляторм)
            //for (int i = 0; i < mas.Length; i++) //Заполнение случайными числами
            //{
            //    mas[i] = random.Next(-1000, 1000);
            //    Console.WriteLine($"{i+1}: {mas[i]}");
            //}

            //int odds = 0;
            //for (int i = 0; i < mas.Length; i++) { //Подсчёт кол-ва нечётных по значению чисел
            //    if (Math.Abs(mas[i]) % 2 == 1) odds++;
            //}

            //if (odds > 3)
            //{
            //    ulong product = 1;
            //    for (int i = 0; i < mas.Length; i++)
            //    {
            //        if (mas[i] > 0) product*= (uint)mas[i];
            //    }
            //    Console.WriteLine($"Произведение положительных: {product}");
            //}
            //else
            //{
            //    Console.WriteLine("Отрицательные увеличены на 3");
            //    for (int i = 0; i < mas.Length; i++)
            //    {
            //        if (mas[i] < 0) mas[i] += 3;
            //        Console.WriteLine($"{i + 1}: {mas[i]}");
            //    }
            //}


            //Задание2

            Console.WriteLine("Будет создан двумерный массив вместительностью n на m и выведен на экран.");
            Console.WriteLine("Первый положительный сверху элемент в столбце будет отмечен жёлтым.");
            Console.WriteLine("Кол-во положительных элементов будет подсчитано.");
            Console.Write("Введите n:");

            byte n;
            while (!(byte.TryParse(Console.ReadLine(), out n))) Console.Write("Введите n (от 0 до 255): "); //Ввод кол-ва столбцов массива
            Console.Write("Введите m:");
            byte m;
            while (!(byte.TryParse(Console.ReadLine(), out m))) Console.Write("Введите m (от 0 до 255): "); //Ввод кол-ва строк массива

            int[,] mas = new int[n, m];
            int[] nOfPositivesInColumn = new int[m];

            Random rand = new Random();
            Console.WriteLine("Массив: ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mas[i, j] = rand.Next(-60000, 60000);
                    Console.Write($"{mas[i, j]}\t"); //Присваивание случайных значений элементам массива
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nМассив проанализирован: ");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mas[i, j] > 0)
                    {
                        if (nOfPositivesInColumn[j] == 0) Console.ForegroundColor = ConsoleColor.Yellow;
                        nOfPositivesInColumn[j]++;
                    }
                    Console.Write($"{mas[i, j]}\t");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nКол-ва положительных значений в столбцах:");
            for (int i = 0; i < m; i++)
            {
                Console.Write($"{nOfPositivesInColumn[i]}\t");
            }
            Console.ReadKey();


        }
    }
}
