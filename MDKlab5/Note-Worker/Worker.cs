using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notes
{
    public enum JobTitle
    {
        director,
        CEO,
        accountant,
        programmer
    }

    

    public class Worker : Note
    {
        protected JobTitle _title;
        protected string _email;

        public  JobTitle Title {
            get { return _title; }
            set { _title = value; } 
        }
        public string Email 
        {
            get { return this._email;}
            set //Проверка формата почты
            {
                if (value.Length < 255 && Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                //Регулярное выражение почты ^ - в начале;  [^@\s] НЕ@ и не \s (пробельные символы);
                //+ - подходит под маску предыдущего символа один или несколько раз;
                // \. - точка; $ - конец слова
                {
                    _email = value;
                }
                else
                    throw new ArgumentException("Некорретный формат почты");

            }
        }
        public Worker(string fullName, string phoneNumber, JobTitle title, string email) 
            : base(fullName, phoneNumber) //Передача в базовый конструктор параметров
        {
            this.Title = title;
            this.Email = email;
        }
        public override string GetFullInfo() //Перезаписанный метод
        {
            return $"{base.GetFullInfo()}\nДолжность: {_title}\nЭл. почта: {_email}";
        }
    }
}
