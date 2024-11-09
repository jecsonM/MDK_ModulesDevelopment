using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Program
    {
        //функции к заданию 1
        static string LongestDigitSequanceInRow(string s, out int indRes, out int nRes)
        {
            indRes = 0;
            nRes = 0;
            int n = 0;
            if (s == "") return "";
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= 48) && (s[i] <= 57)) //Проверяет, что символ s[i] это цифра (по коду символа)
                {
                    n++;
                    if (nRes < n)
                    {
                        nRes = n;
                        indRes = i; //сейчас это индекс последней цифры в последовательности
                    }

                }
                else
                    n = 0;
            }
            indRes -= nRes - 1; //получение индекса первой цифры последовательности
            return s.Substring(indRes, nRes); //Возвращает самую длинную последовательность цифр
        }

        static string LongestDigitSequanceInRow(string s) //Функция без возвращаемых переменных indRes nRes
        {
            int indRes, nRes = 0;
            return LongestDigitSequanceInRow(s, out indRes, out nRes); //Вызов функции с такой же реализацией
        }

        static string removeLongestDigitsSequence(string s)
        {
            int indRes, nRes, nResFirst = 0;
            LongestDigitSequanceInRow(s, out indRes, out nResFirst);
            nRes = nResFirst;
            do
            {
                s = s.Substring(0, indRes) + s.Substring(indRes + nRes); //соединяю две подстроки без подстроки, (indRes, nRes)
                LongestDigitSequanceInRow(s, out indRes, out nRes);
            } while ((nRes == nResFirst) && (nRes != 0)); //удаление последовательно всех последовательностей цифр
            return s;
        }
        //конец функций к заданию 1

        //функции к заданию 2

        static void coloredWriteLine(string str, ConsoleColor color) //функция для красочного вывода в консоль
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        static void inputMassivesFromConsole(ref int[] mas) //ввод массива с консоли
        {
            int len = -1;
            do
            {
                Console.Write("Введите длину массива(от 1 до 1000): ");
            } while (!int.TryParse(Console.ReadLine(), out len) ||
            (len <= 0) || (len > 1000)); //ввод длины массива с проверкой на ошибки
            mas = new int[len];
        ChoiceInput:    //метка для обработки ошибок
            Console.WriteLine("Как вы хотите ввести ввести массив?");
            Console.WriteLine("1)Вручную каждое значение\n2)Случайным образом заполнить");
            Console.Write("Ваш выбор: ");
            byte choice = 0;
            byte.TryParse(Console.ReadLine(), out choice); //если вводили ошибочное значение, то choice так и останется 0
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Вручную:\n Вводите целые числа:"); //ручное заполнение массива
                    for (int i = 0; i < mas.Length; i++)
                    {
                        do
                        {
                            Console.Write($"[{i}]: ");
                        } while (!int.TryParse(Console.ReadLine(), out mas[i]));   
                    }
                    break;
                case 2:
                    Console.WriteLine("Случайным образом:\n"); //автоматическое заполнение массива
                    Random random = new Random();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        mas[i] = random.Next(-50000, 50000);
                    }
                    break;
                default:
                    coloredWriteLine("\nВы ввели неожиданное значение.Введите одно из предложенных.", ConsoleColor.Red);    //Обработка ошибок
                    goto ChoiceInput; //goto в начало выбора
            }
        }
        static void outputMassivesToConsole(int[] mas) //вывод массива на консоль
        {
            for (int i = 0; i < mas.Length; i++)
            {
                Console.WriteLine($"[{i}]: {mas[i]}");
            }

        }
        static int findMax(int[] mas) //поиск максимального элемента
        {
            int max = mas[0];
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] > max) max = mas[i];
            }
            return max;
        }

        static void resetEveryMultipleOfN(int[] mas, int n) //Зануление всех чисел в массиве, кратных N
        {
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] % n == 0) mas[i] = 0; 
            }
        }
            //конец функций к заданию 2

            static void Main(string[] args)
        {
            //Задание 1

            //Console.WriteLine("Введите строку s");
            //Console.WriteLine("В этой строке мы найдём самую длинную последовательность подряд идущих цифр и удалим её.");

            //Console.Write("Введите s:");

            //string s = Console.ReadLine();

            //Console.WriteLine($"Первая самая длинная последовательность цифр в строке: {LongestDigitSequanceInRow(s)}");
            //Console.WriteLine($"Строка после удаления всех последовательностей самой большой длины: {removeLongestDigitsSequence(s)}");
            //Console.ReadKey();

            //Задание2

            int[][] massives = new int[2][];
            Console.WriteLine("Вам будет предложено выбрать длину массива чисел,");
            Console.WriteLine("а потом выбрать тип заполнения этого массива (вручную или случайным образом.");
            Console.WriteLine("Если максимум массива будет чётным, то в этом массиве будут занулены чётные элементы.");
            for (int i = 0; i < massives.Length; i++)
            {
                coloredWriteLine($"\n{i + 1}-ый массив:", ConsoleColor.Yellow);
                inputMassivesFromConsole(ref massives[i]);

                coloredWriteLine($"Вывод {i + 1}-го массив:", ConsoleColor.Green);
                outputMassivesToConsole(massives[i]);
                int max = findMax(massives[i]);
                if (max % 2 == 0)
                {
                    resetEveryMultipleOfN(massives[i], 2);
                    Console.WriteLine($"Т.к. максимальный чётный ({max}), мы обнулили все чётные элементы:");
                    outputMassivesToConsole(massives[i]);
                }
            }
            Console.ReadKey();
        }
    }
}
