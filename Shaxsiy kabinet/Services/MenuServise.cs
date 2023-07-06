using Newtonsoft.Json;
using Shaxsiy_kabinet.Displays;
using Shaxsiy_kabinet.Extensions;
using Shaxsiy_kabinet.Interfaces;
using Shaxsiy_kabinet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Services
{
    internal struct MenuServise : IMenuService
    {
        public NoteModel GetNote(string login, int number)
        {
            DataBaseServise dataBaseServise = new DataBaseServise();

            string temp = dataBaseServise.GetNoteDataOfUser(login);

            if (temp != null)
            {
                Dictionary<string, List<NoteModel>> notes = JsonConvert.DeserializeObject<Dictionary<string, List<NoteModel>>>(temp);

                int count = 1;

                foreach (string key in notes.Keys)
                {
                    for (int i = 0; i < notes[key].Count; i++)
                    {
                        if (number == count++)
                        {
                            return notes[key][i];
                        }
                    }
                }
            }
            return null;
        }

        public string EnterNote(string title)
        {
            string result = "";

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.S && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    return result;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    result += "\n";
                }
                else
                {
                    result.AddText(keyInfo.KeyChar, out result);

                    Console.Clear();
                    Console.WriteLine($"Title:{title}\nCTRL+S for save");
                    Console.WriteLine(result);
                }
            } while (true);
        }

        public bool CheckLoginPassword(string currentLogin, string currentPassword, string login)
        {
            DataBaseServise dataBaseServise=new DataBaseServise();
            string[] temp = dataBaseServise.GetDataOfUsers();
            UserModel model = new UserModel();

            for(int i=0; i<temp.Length-1; i++)
            {
                model = JsonConvert.DeserializeObject<UserModel>(temp[i]);

                if (model.Login == login && model.Login == currentLogin && model.Password == currentPassword)
                {
                    return true;
                }
            }
            return false;
        }

        public void Case2(string login, DataBaseServise dataBaseServise)
        {
            NoteModel note = new NoteModel();
            Console.Clear();

            Console.Write("Title: ");
            string? title = Console.ReadLine();

            if (title.Length != 0)
            {
                note.Title = title;
            }
            else
            {
                throw new Exception();
            }

            Console.Clear();
            Console.WriteLine($"Title:{title}\nCTRL+S for save");
            note.Text = EnterNote(title);

            dataBaseServise.AddOrDelNote(login, note, 0, true);

            Console.Clear();
        }

        public void Case3(string login, DataBaseServise dataBaseServise, int countNote)
        {
            int note = int.Parse(Console.ReadLine());

            if (countNote > note && note > 0)
            {
                dataBaseServise.AddOrDelNote(login, new NoteModel(), note, false);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Case4(string login, DataBaseServise dataBaseServise, UserModel userModel)
        {
            MenuPage menu = new MenuPage(); 

            Console.Write("Current login >>> ");
            string currentLogin = Console.ReadLine();

            Console.Write("Current Password >>> ");
            string currentPassword = Console.ReadLine();

            if (CheckLoginPassword(currentLogin, currentPassword, login))
            {
                Console.Clear();

                Console.Write("New login >>> ");
                string newLogin = Console.ReadLine();

                Console.Write("New Password >>> ");
                string newPassword = Console.ReadLine();

                if (dataBaseServise.ChangeLoginPassword(newLogin, newPassword, login))
                {
                    Console.Clear();
                    Console.WriteLine("The password has been changed successfull");
                    Thread.Sleep(2000);

                    userModel.Login = newLogin;
                    userModel.Password = newPassword;

                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid login or password");
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid login or password");
                Thread.Sleep(2000);
            }
        }
    }
}
