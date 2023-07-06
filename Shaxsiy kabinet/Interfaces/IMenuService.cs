using Shaxsiy_kabinet.Models;
using Shaxsiy_kabinet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Interfaces
{
    internal interface IMenuService
    {
        public NoteModel GetNote(string login, int number);

        public string EnterNote(string title);

        public bool CheckLoginPassword(string currentLogin, string currentPassword, string login);

        public void Case2(string login, DataBaseServise dataBaseServise);

        public void Case3(string login, DataBaseServise dataBaseServise, int countNote);

        public void Case4(string login, DataBaseServise dataBaseServise, UserModel userModel);
    }
}
