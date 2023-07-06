using Newtonsoft.Json;
using Shaxsiy_kabinet.Interfaces;
using Shaxsiy_kabinet.Models;
using Shaxsiy_kabinet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Displays
{
    internal class MenuPage : DisplayAbstarct
    {
        public override void Show(params dynamic[] Data)
        {
        key:
            //Services
            MenuServise menuServise = new MenuServise();
            DataBaseServise dataBaseServise = new DataBaseServise();

            UserModel userModel = Data[0];
            string login = Data[1];

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"    User: {userModel.Name} {userModel.Surname}");

            int countNote = ShowNotes(login);

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "|                1.View Note                 |\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "|                2.Add Note                  |\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "|                3.Del Note                  |\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "|                4.Change Login              |\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                             "|                5.Exit                      |\n" +
                             "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("Select>>> ");
            try
            {
                int n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        {
                            Console.Write("Select note >>> ");
                            try
                            {
                                int notenumber = int.Parse(Console.ReadLine());

                                if (countNote > notenumber && notenumber > 0)
                                {
                                shownotee:

                                    Console.Clear();

                                    ShowNote(login, notenumber);

                                    Console.WriteLine("1.Edit  2.back");
                                    try
                                    {
                                        Console.Write("Select >>> ");
                                        int select = int.Parse(Console.ReadLine());
                                        if (select == 1)
                                        {
                                            dataBaseServise.AddOrDelNote(login, new NoteModel(), notenumber, false);

                                           menuServise.Case2(login, dataBaseServise);
                                        }
                                        else if (select == 2)
                                        {
                                            Console.Clear();
                                            goto key;
                                        }
                                        else
                                        {
                                            throw new Exception();
                                        }
                                    }
                                    catch
                                    {
                                        goto shownotee;
                                    }

                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                goto key;
                            }

                            goto key;
                        }

                    case 2:
                        {
                            try
                            {
                               menuServise.Case2(login, dataBaseServise);

                            }
                            catch
                            {
                                Console.Clear();
                                goto key;
                            }

                            goto key;
                        }

                    case 3:
                        {
                            Console.Write("Select note >>> ");
                            try
                            {
                               menuServise.Case3(login, dataBaseServise, countNote);
                            }
                            catch
                            {
                                Console.Clear();
                                goto key;
                            }

                            Console.Clear();
                            goto key;
                        }

                    case 4:
                        {
                            menuServise.Case4(login, dataBaseServise, userModel);

                            Console.Clear();
                            goto key;
                        }

                    case 5:
                        {
                            Console.Clear();
                            break;
                        }

                    default:
                        {
                            goto key;
                        }
                }
            }
            catch
            {
                Console.Clear();
                goto key;
            }

            Console.ResetColor();
        }

        public int ShowNotes(string login)
        {
            DataBaseServise dataBaseServise = new DataBaseServise();

            string temp = dataBaseServise.GetNoteDataOfUser(login);

            if (temp != null)
            {
                Dictionary<string, List<NoteModel>> notes = JsonConvert.DeserializeObject<Dictionary<string, List<NoteModel>>>(temp);

                int number = 1;

                foreach (string key in notes.Keys)
                {
                    notes[key].ForEach(note =>
                    {
                        Console.WriteLine($"note{number++} {note.Title}");
                    });
                }
                return number;
            }
            return 0;
        }

        public void ShowNote(string login, int number)
        {
            MenuServise menuServise = new MenuServise();
            NoteModel noteModel = menuServise.GetNote(login, number);

            Console.WriteLine($"Title: {noteModel.Title}\n{noteModel.Text}\n");
        }
    }
}
