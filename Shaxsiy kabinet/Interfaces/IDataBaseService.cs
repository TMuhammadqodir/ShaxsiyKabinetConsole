using Shaxsiy_kabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Interfaces
{
    internal interface IDataBaseService
    {
        string[] GetDataOfUsers();

        string GetNoteDataOfUser(string login);

        string[] GetDataOfNotes();

        public void AddOrDelNote(string login, NoteModel noteModel, int noteNumber, bool checker);

        bool ChangeLoginPassword(string newLogin, string newPassword, string login);

        public bool CheckLogin(string login);
    }
}
