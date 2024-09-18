using System;
using System.Collections.Generic;
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
            int[] nums = new int[3]; //объявление массива
            Console.WriteLine("Если число больше среднего арифметического всех трёх,то число будет выведено жёлтым(если чётное) или зелёным(если нечётное) цветом.");
            Console.WriteLine("Введите три Целых числа:");

            for (int i = 0; i < nums.Length; i++) //Ввод чисел с клавиатуры
            {
                bool isValid = false;
                while (!isValid)
                {
                    try //обработка исключений
                    {
                        Console.Write((i + 1) + ": ");
                        nums[i] = Convert.ToInt32(Console.ReadLine());
                        isValid = true;
                    }
                    catch
                    {
                        Console.WriteLine("вы, вероятно, ошиблись при вводе. введите заново");
                    }
                }
            }

            double u = 0; //Переменная для среднего арифметическое
            for (int i = 0; i < nums.Length; i++)
            {
                u += nums[i];  //Счёт суммы чисел
            }
            u /= nums.Length; //Вычисление среднего арифметического

            for (int i = 0; i < nums.Length; i++)
            {
                if ((nums[i] > u) && (nums[i] % 2 == 0)) //Проверка больше ли среднего арифметического И чётности
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(nums[i] + " ");
                }
                else if ((nums[i] > u) && (Math.Abs(nums[i] % 2) == 1)) //Проверка больше ли среднего арифметического И НЕчётности
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(nums[i] + " ");
                }
            }
            Console.ReadKey();



            //ЗАДАНИЕ 2

            //    const string LETTERS = "абвгдеёжзийклмнопрстуфхцчшщыьэюя";
            //    int howManyLetters = LETTERS.Length;
            //    Console.WriteLine("Добро пожаловать в программу. Вам будет предложена буква. Напишите слово, оканчивающееся этой буквой. \nДля завершения введите пустую строку.");
            //    Random random = new Random();
            //    bool toStay = true;
            //    uint score = 0;
            //    uint tries = 0;
            //    while (toStay)
            //    {
            //        char lastLetter = LETTERS[random.Next(howManyLetters)];
            //        Console.WriteLine($"Напишите слово с буквой \"{lastLetter}\" на конце.");
            //        string input = Console.ReadLine().ToLower();

            //        if (string.Equals(input, ""))
            //        {
            //            toStay = false;
            //        }
            //        else
            //        {
            //            tries++;
            //            if (lastLetter == input[input.Length - 1])
            //            {
            //                score++;
            //                Console.WriteLine("Верно!");

            //            }
            //            else
            //                Console.WriteLine("Неверно");
            //        }
            //    }
            //    if (tries > 0)
            //    {
            //        Console.WriteLine($"Попыток: {tries}\t Правильных: {score}\tВаш балл: {100 * score / tries}");
            //    }
            //    else Console.WriteLine("Вы не пытались");
            //    Console.ReadKey();
            }
        }
}
