using Notes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDKlab6
{
    internal class NoteConsole
    {
        /// <summary>
        /// Главная функция, которая выводит на консоль 
        /// опции и вызывает функции для работы со списком записей
        /// </summary>
        /// <param name="notes">Список записей, изминяемых в меню</param>
        /// <returns>Возвращает false, если пользователь желает выйти из меню. 
        /// В остальных случаях true </returns>
        public static bool mainMenu(ref List<Note> notes)
        {
            Console.Write("Укажите ваш выбор:\n" +
                "1. Добавить запись (или работника)\n" +
                "2. Вывести все сохраннёные записи\n" +
                "3. Изменить запись по ФИО\n" +
                "4. Изменить запись по номеру телефона\n" +
                "5. Выйти\n" +
                "Ваш выбор:");
            byte choice;
            byte.TryParse(Console.ReadLine(), out choice);
            switch(choice)
            {
                case 1:
                    Console.WriteLine("Вы хотите запись или работника? (з/р)");
                    ConsoleKeyInfo ch = Console.ReadKey();
                    Console.WriteLine();
                    if(ch.Key == ConsoleKey.P) //P - клавиша з - запись
                    { notes.Add(GetNoteFromCons()); }
                    if (ch.Key == ConsoleKey.H) //H - клавиша р - работник
                    { notes.Add(GetWorkerFromCons()); }
                            break;
                case 2:
                    Console.WriteLine(showAllNotes(ref notes));

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    return false;
                default:
                    Console.WriteLine("Такой опции нет");
                break;
            };
            Console.WriteLine();
            return true;
        }
        
        /// <summary>
        /// Возвращает информацию по всем записям в списке
        /// </summary>
        /// <param name="notes">список с записями</param>
        /// <returns>Строка со всей информацией записей</returns>
        static string showAllNotes(ref List<Note> notes)
        {
            string toReturn ="";
            foreach (Note note in notes)
            {
                toReturn += $"{note.GetFullInfo()}\n\n";
            }
            return toReturn;
        }

        /// <summary>
        /// Запрашивает с консоли ввод нового Note
        /// </summary>
        /// <returns>Note с параметрами, введённых с консоли</returns>
        public static Note GetNoteFromCons()
        {
            ConsoleKeyInfo ch;
            Note note;
            do
            {
                Console.WriteLine("Введите имя в записи: ");
                string name = Console.ReadLine();
                Console.WriteLine("Введите номер телефона: ");
                string phone = Console.ReadLine();
                note = new Note(name, phone);
                Console.WriteLine("Вы верно ввели запись? (д/н)");

                ch = Console.ReadKey();
                Console.WriteLine();
            }
            while (ch.Key != ConsoleKey.L); // пока не ответ "Да". L - клавиша д - да
            return note;
        }

        /// <summary>
        /// Запрашивает с консоли ввод нового Worker
        /// </summary>
        /// <returns>Worker с параметрами, введённых с консоли</returns>
        public static Worker GetWorkerFromCons()
        {
            ConsoleKeyInfo ch;
            Worker worker;
            do
            {
                Console.WriteLine("Введите имя в записи: ");
                string name = Console.ReadLine();
                Console.WriteLine("Введите номер телефона: ");
                string phone = Console.ReadLine();
                JobTitle title = getJobTitle();

                string email = getEmail();

                worker = new Worker(name, phone, title, email);
                Console.WriteLine("Вы верно ввели запись? (д/н)");

                ch = Console.ReadKey();
                Console.WriteLine();
            }
            while (ch.Key != ConsoleKey.L); // пока не ответ "Да". L - клавиша д - да
            return worker;
        }

        /// <summary>
        /// Выводит на консоль список возможных должностей и возвращает выбираемую
        /// пользователем должность
        /// </summary>
        /// <returns>выбранная должность</returns>
        static JobTitle getJobTitle()
        {
            var jobTitles = Enum.GetValues(typeof(JobTitle));
            foreach (JobTitle title in jobTitles)
            {
                Console.WriteLine($"{(int)title} {title}");

            }
            JobTitle choice;
            do
                Console.WriteLine("Ваш выбор: ");
            while (!(Enum.TryParse(Console.ReadLine(), out choice) && Enum.IsDefined(typeof(JobTitle), choice)));
            return choice;
        }
        
        /// <summary>
        /// Просит пользователя ввести почту нужного формата
        /// </summary>
        /// <returns>почта корректного формата</returns>
        static string getEmail()
        {
            string email; 
            do
            {
                Console.Write("Введите КОРРЕКТНЫЙ адрес электронной почты: ");
                email = Console.ReadLine();
            }
            while (!(email.Length < 255 && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)));
            return email;
        }
    }
}
