using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Note
    {
        protected string _fullName; //Протектед поля
        protected string _phoneNumber;
        public string FullName { //Прокладки для геттеров и сеттеров протектед полей
            get {return _fullName; } 
            set { _fullName = value; } 
        }
        public string PhoneNumber {
            get { return _phoneNumber; }
            set {_phoneNumber = value; } 
        }
        public Note(string fullName, string phoneNumber) //Конструктор
        {
            this.FullName = fullName;
            this.PhoneNumber = phoneNumber;
        }
        
        public virtual string GetFullInfo() //Виртуальный метод
        {
            return $"ФИО: {_fullName}\nНомер телефона: {_phoneNumber}";
        }
    }
}
