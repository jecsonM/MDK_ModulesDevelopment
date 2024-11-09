using Worker;

namespace ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Note> notes = new List<Note>();
            notes.Add(new Note("Мосеев Евгений Васильевич", "+7 931 156 25 24"));
            notes.Add(new Note("Чистяков-Лаврентьев Константин Дмитриевич", "+7 941 786 91 86"));
            notes.Add(new Worker.Worker("Байбиков Роман Анатольевич", "+7 937 48 12 98", JobTitle.director, "director@college.spbstu.ru"));

            Console.WriteLine("Все записи: ");
            foreach (Note note in notes)
            {
                Console.WriteLine(note.GetFullInfo());
                Console.WriteLine();
            }

            notes[2].PhoneNumber = "8 954 245 351 2554";

            Console.WriteLine("Все записи после изменения: ");
            foreach (Note note in notes)
            {
                Console.WriteLine(note.GetFullInfo());
                Console.WriteLine();
            }

            char choosenLetter;
            do Console.Write("Введите Первую букву фамилии: ");
            while (!char.TryParse(Console.ReadLine().ToUpper(), out choosenLetter));

            Console.WriteLine("Отобранные записи: ");
            foreach (Note note in notes)
            {
                if (note.FullName[0] == choosenLetter)
                    Console.WriteLine(note.GetFullInfo());
                Console.WriteLine();
            }
        }
    }
}