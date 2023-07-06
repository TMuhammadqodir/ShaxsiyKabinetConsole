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
    internal static class AccsessService
    {
        public static UserModel CheckLoginAndPassword(string login, string password)
        {
            DataBaseServise dataBaseServise = new DataBaseServise();

            string[] temp = dataBaseServise.GetDataOfUsers();

            for (int i=0; i<temp.Length-1; i++)
            {
                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(temp[i]);

                if (userModel.Login == login && userModel.Password == password)
                {
                    return userModel;  
                }
            }

            return null;
        }
    }
}
