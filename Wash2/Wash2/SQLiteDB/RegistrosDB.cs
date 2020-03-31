using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Wash2.Models;
using Xamarin.Forms;

namespace Wash2.SQLiteDB
{
    public class RegistrosDB
    {
        private SQLiteConnection conn;

        public RegistrosDB()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Registros>();
        }

        public IEnumerable<Registros> GetRegistro()
        {
            var registrados = (from regs in conn.Table<Registros>() select regs);
            return registrados.ToList();
        }

        public string AddRegs(Registros regs)
        {
            try
            {
                conn.Insert(regs);
                return "Success";
            }
            catch (Exception ex){
                return ex.ToString();
            }
        }

        public string UpdateAll(int id, string nombre, string app, string apm, string fca_nac, string telefono, string correo, string password, int pagina)
        {
            try
            {
                var data = conn.Table<Registros>();
                var d1 = (from values in data
                          where values.id == id
                          select values).Single();
                if (true)
                {
                    d1.nombre = nombre;
                    d1.app = app;
                    d1.apm = apm;
                    d1.fecha = fca_nac;
                    d1.telefono = telefono;
                    d1.correo = correo;
                    d1.password = password;
                    d1.pagina = pagina;
                    conn.Update(d1);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string UpdateRegPaquete(int id, int paquete)
        {
            try
            {
                var res = "Fallo";
                var data = conn.Table<Registros>();
                var d1 = (from values in data
                          where values.id == id
                          select values).Single();
                if (true)
                {
                    d1.paquete = paquete;
                    conn.Update(d1);
                    res = "Success";
                }
                return "Res->"+res;
            }
            catch (Exception ex){
                return ex.ToString();
            }
        }

        public void DeleteRegistro(int ID)
        {
            conn.Delete<Registros>(ID);
        }

        public void DeleteAllReg()
        {
            conn.DeleteAll<Registros>();
        }

    }
}
