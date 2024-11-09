using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MDKlab6;
using Notes;

namespace ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Note> notes = new List<Note>();
            while (NoteConsole.mainMenu(ref notes));
        }
    }
}
