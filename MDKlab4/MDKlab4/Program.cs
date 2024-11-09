using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication
{
    
    enum Dealer
    {
        КировскийЗавод = 0,
        ЮжныйДеталестроительныйЗавод,
        АгроМашинСтрой,
        НовоеФабрикаторскоеОбъединение,
        ФабрикаСеверная,
        ВолжскийАвтомобильныйЗавод
    };
    struct DetailOrder
    {
        public string detailName;
        public Dealer dealer;
        public decimal cost;
        public DateTime dateOfOrder;
        public uint nounOfDetails;
        public decimal markupOnCost;
        public decimal fullCostOfOrder { get; }

        public DetailOrder(string detailName, Dealer dealer, decimal cost,
            DateTime dateOfOrder, uint nounOfDetails, decimal markupOnCost)
        {
            this.detailName = detailName;
            this.dealer = dealer;
            this.cost = cost;
            this.dateOfOrder = dateOfOrder;
            this.nounOfDetails = nounOfDetails;
            this.markupOnCost = markupOnCost;
            this.fullCostOfOrder = cost * nounOfDetails + markupOnCost;
        }
        public string GetAlldata()
        {
            return $"Имя: {detailName}\nПроизводитель: {dealer}\nЦена: {cost}\nДата заказа: {dateOfOrder}\n" +
                $"Кол-во деталей: {nounOfDetails}\nНаценка на стоимость: {markupOnCost}\nПолная стоимость: {fullCostOfOrder}";
        }
    }
    internal class Program
    {
        //функции к заданию 1

        //Возвращает все опции меню
        public static string GetMenuConsoleInterface()
        {
            return "1.Добавить новый заказ в список\n" +
                        "2.Удалить n-ый в списке заказ\n" +
                        "3.Вывести все заказы\n" +
                        "4.Изменить первую запись в списке по названию детали\n" +
                        "5.Найти и вывести все заказы по выбираемому производителю\n" +
                        "6.Найти среднюю стоимость заказа\n" +
                        "7.Выйти из матрицы";
        }

        public static bool AskUserIsHeSureCons(string comment) //Убеждается, что юзер правильно ввёд данные
        {
            Console.Write(comment);
            string inputOfYesOrNo = Console.ReadLine().ToLower();
            return inputOfYesOrNo == "y" || inputOfYesOrNo == "д";
        }

        public static string GetAllDealers() //Возвращает всех производителей
        {
            string toReturnStr = "";
            string[] DEALERNAMES = {"Кировский завод", "Южный Деталестроительный завод", "АгроМашинСтрой",
                "Новое Фабрикаторское Объединение", "Фабрика \"Северная\"", "Волжский автомобильный завод"};
            for (int i = 0; DEALERNAMES.Length > i; i++)
            {
                toReturnStr += $"{i} {DEALERNAMES[i]}\n";
            }
            return toReturnStr;
            
        }
        static string InputDetailNameFromCons() //Ввод имени детали с клавиатуры
        {
            string name;
            do
            {
                Console.Write("Введите название детали: ");
                name = Console.ReadLine();
            }
            while (!AskUserIsHeSureCons("Верно ввели?(y/n д/н)") || string.IsNullOrEmpty(name)); //НЕ повторять ввод, если пользователь ввёл корректно
            return name;
        }
        static Dealer InputDealerFromCons() //Ввод производителя с консоли
        {
            Console.WriteLine(GetAllDealers());
            Console.Write("Выберите производителя: ");
            byte inp = 255;

            while (!byte.TryParse(Console.ReadLine(), out inp) | inp > 5) //dealer.VolgskiyAutomobilniyZavod = 5 последний в enum(e)
            {
                Console.WriteLine(GetAllDealers());
                Console.Write("Введите один из предложенных номеров: ");
            }
            return (Dealer)inp;
        }
        static decimal InputCostFromCons() //Ввод цены с консоли
        {
            decimal cost = -1;
            Console.Write("Введите стоимость детали (в рублях):");
            while (!decimal.TryParse(Console.ReadLine(), out cost) || cost <= 0)
            Console.Write("Введите стоимость детали (положительное, ненулевое, в рублях):");
            return cost;
        }

        static DateTime InputDateOfOrderFromCons() //Ввод даты заказа с консоли
        {
            DateTime date = new DateTime();
            bool isCorrect = false;
            while(!isCorrect)
            {
                Console.Write("Введите дату заказа (в формате дд.мм.гггг [чч:мм:сс], время необязательно):");
                try
                {
                    
                    date = Convert.ToDateTime(Console.ReadLine());

                   

                    isCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вы ввели дату некорректно!");
                }
            }
            return date;
        }
        static uint InputNounOfDetailsFromCons() //Ввод кол-ва деталей с консоли
        {
            uint noun = 0;
            Console.Write("Введите количество заказанных деталей:");
            while (!uint.TryParse(Console.ReadLine(), out noun) || noun == 0)
                Console.Write("Введите количество заказанных деталей (положительное, не нуль, целое):");
            return noun;
        }
        static decimal InputMarkupOnCostFromCons() //Ввод производителя с консоли
        {
            decimal markup = -1;
            Console.Write("Введите наценку детали (в рублях):");
            while (!decimal.TryParse(Console.ReadLine(), out markup) || markup < 0)
                Console.Write("Введите наценку детали (положительное или ноль, в рублях):");
            return markup;
        } 
        static void AddNewDetailOrderToListFromCons(ref List<DetailOrder> detailOrders) //Добавить новый заказ в список заказов
        {
            Console.WriteLine("Сейчас будет вводиться новый заказ детали:");

            detailOrders.Add(
                new DetailOrder( //Конструктор заказа
                    InputDetailNameFromCons(), InputDealerFromCons(),InputCostFromCons(),
                    InputDateOfOrderFromCons(), InputNounOfDetailsFromCons(), InputMarkupOnCostFromCons()
                    )
                );
        }
        static string OutputAllDetailOrders(ref List<DetailOrder> detailOrders) //Возвращает строку с информацией обо всех элементах
        {
            string toReturn = "Вывод элементов:\n";
            for (int i = 0; i < detailOrders.Count(); i++ )
            {
                toReturn += $"{i}: {detailOrders[i].GetAlldata()}\n\n";
            }
            
            return toReturn;
        }
        
        public static void RemoveDetailOrderFromListFromCons(ref List<DetailOrder> detailOrders) //Убирает введённый n-ый элемент
        {
            int n;
            
            if (AskUserIsHeSureCons("Вы точно хотите удалить(y/n д/н): ") && detailOrders.Count() != 0)
            {
                do
                {
                    Console.WriteLine($"Введите n-ый элемент от 0 до {detailOrders.Count() - 1}");
                } while (!int.TryParse(Console.ReadLine(), out n) || (n < 0) || (n > detailOrders.Count() - 1));
                detailOrders.RemoveAt(n);
            }
            
                
            
        }
        public static void EditDetailOrderByNameInCons(ref List<DetailOrder> detailOrders) //Изменить запись по найденному имени
        {
            string Search = InputDetailNameFromCons();
            int IndexOfFoundOrder = -1;
            for (int i = 0; i < detailOrders.Count; i++)
            {
                if (detailOrders[i].detailName == Search)
                {
                    IndexOfFoundOrder = i;
                    break;
                }
            }
            if (IndexOfFoundOrder > -1)
            {
                Console.WriteLine("Найден объект:");
                Console.WriteLine(detailOrders[IndexOfFoundOrder].GetAlldata());
                bool toContinue = AskUserIsHeSureCons("Вы хотите изменить? (y/n д/н)");
                if(toContinue)
                {
                    detailOrders[IndexOfFoundOrder] = new DetailOrder(InputDetailNameFromCons(), InputDealerFromCons(), InputCostFromCons(),
                    InputDateOfOrderFromCons(), InputNounOfDetailsFromCons(), InputMarkupOnCostFromCons()); //Ввод изменённой структуры
                }
            }
            else
                Console.WriteLine("Мы такой записи не нашли");
        }
        public static string OutputAllDetailOrdersByDealer(ref List<DetailOrder> detailOrders, Dealer dealer)
        {
            string toReturn = "Вывод всех деталей по выбранному производителю\n";
            for (int i = 0; i < detailOrders.Count; i++)
            {
                if (detailOrders[i].dealer == dealer)
                {
                    toReturn += $"{detailOrders[i].GetAlldata()}\n\n";
                }
            }
            return toReturn;
        }
        public static decimal AverageOrdersFullCost(ref List<DetailOrder> detailOrders)
        {
            int count = detailOrders.Count();
            decimal average = 0;
            for (int i = 0; i < count; i++)
            {
                average += detailOrders[i].fullCostOfOrder /count;
            }
            return average;
        }
        public static bool MainMenu( ref List<DetailOrder> detailOrders) //Функция с Менюшками
        {
            bool toContinue = true;
            while (toContinue)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine(GetMenuConsoleInterface());
                Console.Write("Ваш выбор: ");
                byte choice = 0;
                byte.TryParse(Console.ReadLine(), out choice);
                Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<");

                switch (choice)
                {

                    case 1:
                        AddNewDetailOrderToListFromCons(ref detailOrders);
                        break;
                    case 2:
                        RemoveDetailOrderFromListFromCons(ref detailOrders);
                        break;
                    case 3:
                        Console.WriteLine();
                        Console.WriteLine(OutputAllDetailOrders(ref detailOrders));
                        break;
                    case 4:
                        EditDetailOrderByNameInCons(ref detailOrders);
                        break;
                    case 5:
                        Console.WriteLine(OutputAllDetailOrdersByDealer(ref detailOrders, InputDealerFromCons()));
                        break;
                    case 6:
                        Console.WriteLine($"Средняя стоимость заказ: {AverageOrdersFullCost(ref detailOrders):F2}");
                        break;
                    case 7:
                        toContinue = false;
                        break;
                    default:
                        Console.WriteLine("Вы ввели неожиданное значение.");
                    break;
                }
            }
            return toContinue;
        }
        //конец функций к заданию 1


        static void Main(string[] args)
        {
            //Задание 1
            List<DetailOrder> detailOrders = new List<DetailOrder>();
            detailOrders.Add(new DetailOrder("Шуруп", Dealer.АгроМашинСтрой, 0.5m, new DateTime(2024, 10, 9), 80, 10m));
            detailOrders.Add(new DetailOrder("Трактор", Dealer.АгроМашинСтрой, 34560m, new DateTime(2024, 6, 6), 4, 2000m));
            detailOrders.Add(new DetailOrder("Автомобиль \"Запорожец\"", Dealer.ВолжскийАвтомобильныйЗавод, 204980m, new DateTime(2016, 9, 9), 2, 6000m));
            MainMenu(ref detailOrders);
        }
    }
}
