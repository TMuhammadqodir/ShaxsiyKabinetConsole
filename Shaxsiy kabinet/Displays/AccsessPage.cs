using Shaxsiy_kabinet.Models;
using Shaxsiy_kabinet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Displays
{
    internal class AccsessPage : DisplayAbstarct
    {

        public void StartProect()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("1. Entered\n2. Create Account");

            try
            {
                int n = int.Parse(Console.ReadLine());
                if (n == 1)
                {
                    Show();
                }else if (n == 2)
                {
                    Console.Clear();
                    CreateAccunt();
                }
            }
            catch
            {
                Console.Clear();
                StartProect();
            }

            Console.ResetColor();
        }

        public override void Show(params dynamic[] Data)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Animation("Login", 50);
            Console.Write("Login: ");
            string login = Console.ReadLine();

            Animation("Password", 20);
            Console.Write($"Password: ");
            string passowrd = Console.ReadLine();

            Console.ResetColor();

            var userModel = AccsessService.CheckLoginAndPassword(login, passowrd);

            if (userModel != null)
            {   
                Console.Clear();
                MenuPage menuPage = new MenuPage();
                menuPage.Show(userModel, login);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid login or password!");
                Thread.Sleep(3000);
                Console.ResetColor();
                Console.Clear();
                StartProect();
            }
        }

        public void CreateAccunt()
        {
            DataBaseServise dataBaseServise = new DataBaseServise();

            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Surname: ");
            string surName = Console.ReadLine();
            Console.Write("Birthday: ");
            string birthday = Console.ReadLine();
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (name.Length > 0 && surName.Length > 0 && birthday.Length > 0 && login.Length>0 && password.Length > 0)
            {
                UserModel userModel = new UserModel() { Name = name, Surname = surName, Birthday = birthday, Login = login, Password = password};

                if (dataBaseServise.CheckLogin(login)==false)
                {
                    Console.WriteLine("Bu Foydalanuvchi tizimda mavjud boshqa login kiriting!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    CreateAccunt();
                }
                else
                {
                    Console.WriteLine("Create account successfull");
                    dataBaseServise.AddUser(userModel);
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Xato malumot kiritildi qayta kiriting");
                Thread.Sleep(2000);
                Console.Clear();
                CreateAccunt();
            }
            StartProect();
        }

        public void Animation(string str, int speed) 
        { 
            int n = str.Length - 1;
            string strEmpty = "";

            for (int i = 0; i < n; i++)
            {
                strEmpty += " ";
            }

            for (int i = 0; i < n; i++)
            {

                for (int j = i; j < n; j++)
                {
                    Console.Write(str[..i]);
                    Console.Write(strEmpty[..(n - j - 1)]);
                    Console.WriteLine(str[i]);
                    Thread.Sleep(speed);
                    Console.Clear();
                }
            }
        }
    }
}
