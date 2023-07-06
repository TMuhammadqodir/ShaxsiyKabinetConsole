using Newtonsoft.Json;
using Shaxsiy_kabinet.Interfaces;
using Shaxsiy_kabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Services
{
    internal class DataBaseServise : IDataBaseService
    {
        string location1 = "D:/Users.txt";
        string location2 = "D:/Notes.txt";

        public string[] GetDataOfUsers()
        {
            try
            {
                 return File.ReadAllText(location1).Split("\n");
            }
            catch
            { 
                return new string[] { };
            }
        }

        public string GetNoteDataOfUser(string login)
        {
            string[] temp = GetDataOfNotes();

            for (int i=0; i<temp.Length-1; i++)
            {
                if (JsonConvert.DeserializeObject<Dictionary<string, List<NoteModel>>>(temp[i]).ContainsKey(login)){
                    return temp[i];
                }
            }
            return null;
        }

        public string[] GetDataOfNotes()
        {
            try
            {
                return File.ReadAllText(location2).Split("\n");
            }
            catch
            {
                return new string[] { };
            }
        }

        public void AddOrDelNote(string login, NoteModel noteModel, int noteNumber, bool checker)
        {
            string[] temp = GetDataOfNotes();
            bool check = true;

            File.WriteAllText(location2,"");

            for(int i=0; i<temp.Length-1; i++)
            {
                if (JsonConvert.DeserializeObject<Dictionary<string, List<NoteModel>>>(temp[i]).ContainsKey(login))
                {
                    check = false;

                    Dictionary<string, List<NoteModel>> dct = JsonConvert.DeserializeObject<Dictionary<string, List<NoteModel>>>(temp[i]);
                    if (checker)
                    {
                        dct[login].Add(noteModel);
                    }
                    else
                    {
                        dct[login].RemoveAt(noteNumber - 1);
                    }
                    File.AppendAllText(location2, $"{JsonConvert.SerializeObject(dct)}\n");
                }
                else
                {
                    File.AppendAllText(location2,$"{temp[i]}\n");
                }
            }

            if(check && checker)
            {
                Dictionary<string, List<NoteModel>> dct = new();

                dct[login] = new List<NoteModel> { noteModel };

                File.AppendAllText(location2, $"{JsonConvert.SerializeObject(dct)}\n");
            }

        }

        public bool ChangeLoginPassword(string newLogin, string newPassword, string login)
        {
            DataBaseServise dataBaseServise = new DataBaseServise();
            string[] temp = dataBaseServise.GetDataOfUsers();
            UserModel model = new UserModel();
            int index = 0;

            if(!(newLogin.Length>0 && newPassword.Length > 0))
            {
                return false;
            }

            for (int i = 0; i < temp.Length - 1; i++)
            {
                model = JsonConvert.DeserializeObject<UserModel>(temp[i]);

                if (model.Login == login)
                {
                    index = i;
                    break;
                }
            }

            for(int i=0; i<temp.Length-1; i++)
            {
                model = JsonConvert.DeserializeObject<UserModel>(temp[i]);
                if (i!=index && model.Login == newLogin) 
                {
                    return false;
                }
            }

            model.Login = newLogin;
            model.Password = newPassword;

            File.WriteAllText(location1, "");

            for(int i=0; i<temp.Length-1; i++)
            {
                if (i != index)
                {
                    File.AppendAllText(location1, $"{temp[i]}\n");
                }
                else
                {
                    File.AppendAllText(location1, $"{JsonConvert.SerializeObject(model)}\n");
                }
            }


            string[] temp2 = GetDataOfNotes();

            File.WriteAllText(location2, "");

            for (int i = 0; i < temp2.Length - 1; i++)
            {
                Dictionary<string, List<NoteModel>> dct = JsonConvert.DeserializeObject<Dictionary<string, List<NoteModel>>>(temp2[i]);

                if (dct.ContainsKey(login))
                {
                    Dictionary<string, List<NoteModel>> dct2 = new();
                    dct2[newLogin] = dct[login];
                    File.AppendAllText(location2, $"{JsonConvert.SerializeObject(dct2)}\n");
                }
                else
                {
                    File.AppendAllText(location2,$"{temp2[i]}\n");
                }
            }
            return true;
        }

        public bool CheckLogin(string login)
        {
            string[] temp = GetDataOfUsers();

            for(int i=0; i<temp.Length-1; i++)
            {
                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(temp[i]);

                if(userModel.Login == login)
                {
                    return false;
                }

            }
            return true;
        }

        public void AddUser(UserModel userModel)
        {
            File.AppendAllText(location1, $"{JsonConvert.SerializeObject(userModel)}\n");
        }
    }
}
