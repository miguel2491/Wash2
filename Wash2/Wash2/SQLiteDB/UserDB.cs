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

        public string UpdateMember(int id, int idB, string nombre, string username, string password, int idWasher,int status)
        {
            try {
                var res = "Fallo";
                var data = conn.Table<Usuario>();
                var d1 = (from values in data
                          where values.id == id
                          select values).Single();
                if (true)
                {
                    d1.idB = idB;
                    d1.nombre = nombre;
                    d1.username = username;
                    d1.password = password;
                    d1.idWasher = idWasher;
                    d1.status = status;
                    conn.Update(d1);
                    res = "Correcto";
                }
                return "Res->"+res;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public string UpdateMemberToken(int id, string token, int status)
        {
            try
            {
                var res = "Fallo";
                var data = conn.Table<Usuario>();
                var d1 = (from values in data
                          where values.id == id
                          select values).Single();
                if (true)
                {
                    d1.token = token;
                    d1.status = status;
                    conn.Update(d1);
                    res = "Correcto";
                }
                return "Res->" + res;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public string CerrarSesion(int id, int status)
        {
            try
            {
                var res = "Fallo";
                var data = conn.Table<Usuario>();
                var d1 = (from values in data
                          where values.id == id
                          select values).Single();
                if (true)
                {
                    d1.status = status;
                    conn.Update(d1);
                    res = "Correcto";
                }
                return "Res->" + res;
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
        public void DeleteMembers()
        {
            conn.DeleteAll<Usuario>();
        }
    }
}
