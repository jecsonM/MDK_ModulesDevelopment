using Notes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                "3. Изменить/удалить запись по ФИО\n" +
                "4. Изменить/удалить запись по номеру телефона\n" +
                "5. Выйти\n" +
                "Ваш выбор:");
            byte choice;
            byte.TryParse(Console.ReadLine(), out choice);
            Console.WriteLine();
            
            
            switch (choice)
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
                    Console.WriteLine("Введите имя искомой записи в записи: ");
                    string name = Console.ReadLine();
                    Note note = findNoteWithName(name, ref notes);
                    Console.WriteLine();
                    if (!(note is null))
                    {
                        Console.WriteLine(note.GetFullInfo());
                        Console.WriteLine("Вы хотите удалить или изменить? (у/и)");
                        ch = Console.ReadKey();
                        Console.WriteLine();
                        if (ch.Key == ConsoleKey.B) //B - клавиша и - изменить
                            editNote(note);
                        else if (ch.Key == ConsoleKey.E) //E - клавиша у - удалить
                            notes.Remove(note);
                        else
                            Console.WriteLine("Мы не поняли ваш выбор");

                    }
                    else Console.WriteLine("Извините, ничего не найдено по такому имени");

                    break;
                case 4:
                    
                    Console.WriteLine("Введите номер телефона искомой записи в записи: ");
                    string phone = Console.ReadLine();
                    note = findNoteWithPhoneNumber(phone, ref notes);
                    Console.WriteLine();
                    if (!(note is null))
                    {
                        Console.WriteLine(note.GetFullInfo());
                        Console.WriteLine("Вы хотите удалить или изменить? (у/и)");
                        ch = Console.ReadKey();
                        Console.WriteLine();
                        if (ch.Key == ConsoleKey.B) //B - клавиша и - изменить
                            editNote(note);
                        else if (ch.Key == ConsoleKey.E) //E - клавиша у - удалить
                            notes.Remove(note);
                        else
                            Console.WriteLine("Мы не поняли ваш выбор");
                    }
                    else Console.WriteLine("Извините, ничего не найдено по такому имени");
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
        /// Позволяет менять информацию о записях при помощи консоли
        /// </summary>
        /// <param name="note">запись, у которой данные будут меняться</param>
        public static void editNote(Note note)
        {
            if (!((note as Worker) is null)) //Если можно привести к рабочему
            {
                editWorker(note as Worker);
                return;
            }

            ConsoleKeyInfo ch;
            do
            {
                Console.WriteLine("\n0-имя\n1-номер телефона\n");
                byte choice;
                do
                {
                    Console.Write("Вы хотите исправить: ");
                }
                while (!(byte.TryParse(Console.ReadLine(), out choice) && choice < 2));

                switch (choice)
                {
                    case 0:

                        Console.WriteLine("Введите имя в записи: ");
                        note.FullName = Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Введите номер телефона: ");
                        note.PhoneNumber = Console.ReadLine();
                        break;
                }
                Console.WriteLine();
                Console.WriteLine(note.GetFullInfo());
                Console.WriteLine("Вы окончательно ввели данные записи? (д/н)");
                ch = Console.ReadKey();
                Console.WriteLine();
            }
            while (ch.Key != ConsoleKey.L); // пока ответ не "Да". L - клавиша д - да

        }
        /// <summary>
        /// Позволяет менять информацию о рабочем при помощи консоли
        /// </summary>
        /// <param name="worker">рабочий, чьи данные будет меняться</param>
        public static void editWorker(Worker worker)
        {
            ConsoleKeyInfo ch;
            do
            {
                Console.WriteLine("\n0-имя\n1-номер телефона\n2-должность\n3-почта\n");
                byte choice;
                do
                {
                    Console.Write("Вы хотите исправить: ");
                }
                while (!(byte.TryParse(Console.ReadLine(), out choice) && choice < 4));

                switch (choice)
                {
                    case 0:

                        Console.WriteLine("Введите имя работника: ");
                        worker.FullName = Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Введите номер телефона: ");
                        worker.PhoneNumber = Console.ReadLine();
                        break;
                    case 2:
                        worker.Title = getJobTitle();
                        break;
                    case 3:
                        worker.Email = getEmail();
                        break;
                }
                Console.WriteLine();
                Console.WriteLine(worker.GetFullInfo());
                Console.WriteLine("Вы верно ввели данные работника? (д/н)");
                ch = Console.ReadKey();
                Console.WriteLine();
            } 
            while (ch.Key != ConsoleKey.L); // пока ответ не "Да". L - клавиша д - да

        }
        /// <summary>
        /// Ищет в списке запись с указанным номером телефона и возвращает первую найденную
        /// </summary>
        /// <param name="PhoneNumber">Номер телефона записис</param>
        /// <param name="notes">Список, в котором происходит поиск</param>
        /// <returns>Найденная запись, если null, значит не найдена запись </returns>
        public static Note findNoteWithPhoneNumber(string PhoneNumber, ref List<Note> notes)
        {
            Note found = null;
            foreach (var item in notes)
            {
                if (item.PhoneNumber == PhoneNumber)
                {
                    found = item;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// Ищет в списке запись с указанным именем и возвращает первую найденную
        /// </summary>
        /// <param name="fullName">ФИО записис</param>
        /// <param name="notes">Список, в котором происходит поиск</param>
        /// <returns>Найденная запись</returns>
        public static Note findNoteWithName(string fullName, ref List<Note> notes)
        {
            Note found = null;
            foreach (var item in notes)
            {
                if (item.FullName == fullName)
                { 
                    found = item; 
                    break; 
                }
            }
            return found;
        }
        
        /// <summary>
        /// Возвращает информацию по всем записям в списке
        /// </summary>
        /// <param name="notes">список с записями</param>
        /// <returns>Строка со всей информацией записей</returns>
        public static string showAllNotes(ref List<Note> notes)
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
            
            Console.WriteLine("Введите имя в записи: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите номер телефона: ");
            string phone = Console.ReadLine();
            note = new Note(name, phone);
            Console.WriteLine("Вы верно ввели запись? (д/н)");

            ch = Console.ReadKey();
            Console.WriteLine();
            if(ch.Key != ConsoleKey.L) editNote(note); // пока ответ не "Да". L - клавиша д - да
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
            Console.WriteLine("Введите имя работника: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите номер телефона: ");
            string phone = Console.ReadLine();
            JobTitle title = getJobTitle();

            string email = getEmail();

            worker = new Worker(name, phone, title, email);
            Console.WriteLine("Вы верно ввели данные работника? (д/н)");

            ch = Console.ReadKey();
            Console.WriteLine();
            if (ch.Key != ConsoleKey.L)
                editWorker(worker);

            
            return worker;
        }

        /// <summary>
        /// Выводит на консоль список возможных должностей и возвращает выбираемую
        /// пользователем должность
        /// </summary>
        /// <returns>выбранная должность</returns>
        public static JobTitle getJobTitle()
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
        public static string getEmail()
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
