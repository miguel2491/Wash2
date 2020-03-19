using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wash2.Models;
using Xamarin.Forms;

namespace Wash2.SQLiteDB
{
    public class UserDB
    {
        private SQLiteConnection conn;

        public UserDB()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Usuario>();

        }

        public IEnumerable<Usuario> GetMembers()
        {
            var members = (from mem in conn.Table<Usuario>() select mem);
            return members.ToList();
        }


        public string AddMember(Usuario member)
        {
            try
            {
                conn.Insert(member);
                return "success baby bluye ;*";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public void DeleteMember(int ID)
        {
            conn.Delete<Usuario>(ID);
        }
    }
}
