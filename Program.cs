using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UITesting;
using System.Runtime.InteropServices;
using System.Threading;

namespace clicker
{
    class Program
    {

        static void Main(string[] args)
        {
            int clickRate = userClickRate();
            // int clickTime = userClickTime();
            Thread mouseClick = new Thread(() => clickMouse_Thread(clickRate));
            Console.WriteLine("Press \"N\" to Start....");
            bool loopFlag = true;
            while (loopFlag)
            {
                ConsoleKey e = Console.ReadKey(true).Key;
                bool boolvlaue = shortCutKeyPress(e, mouseClick);
                loopFlag = boolvlaue;

            }       

        }
        private static bool shortCutKeyPress(ConsoleKey e, Thread mouseClick)
        {
            bool boolValue = false;
            if (e == ConsoleKey.N)
            {
                boolValue = false;
                mouseClick.Start();
                               
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Press \"N\" to Start...." );
                Console.ForegroundColor = ConsoleColor.White;
            }
            return boolValue;
        }
        public static void clickMouse_Thread(int clickRate)
        {

            Form1 clickObj = new Form1();
            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine("Clicking begining in {0}...", i);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Clicking has begun...");

            int clicksPerSec = 1000 / clickRate;

            while (true)
            {
                Thread.Sleep(clicksPerSec);
                clickObj.clickFunction();
                Console.WriteLine("Click Event ");
            }
        }
        public static int userClickTime()
        {
            Console.Write("Enter how long you would like the clicking to last(sec) : ");
            int clickTime = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("_________________________________________________");

            return clickTime;
        }
        public static int userClickRate()
        {
            Console.WriteLine("Enter how many clicks per-second: ");
            int clicksPerSec = Convert.ToInt32(Console.ReadLine());

            return clicksPerSec;
        }
    }


    // Mouse clicking Obj
    public class Form1 : Form
    {

        //Constructor Function
        public Form1()
        {
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int mouseEvent1 = 0x02;
        private const int mouseEvent2 = 0x04;  

        public void clickFunction()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(mouseEvent1 | mouseEvent2, X, Y, 0, 0);
        }
    }
}
