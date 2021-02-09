using System;
using System.Collections.Generic;
using System.Text;
using PrimerParcial.Entidades;
using PrimerParcial.DAL;
using System.Linq;
using System.Linq.Expressions;

namespace PrimerParcial.BLL
{
    public class CiudadesBLL
    {
        public static bool Guardar(Ciudades ciudad)
        {
            if (!Existe(ciudad.CiudadId))
                return Insertar(ciudad);
            else
                return Modificar(ciudad);
        }

        private static bool Insertar(Ciudades ciudad)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Ciudades.Add(ciudad);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static bool Modificar(Ciudades ciudad)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Entry(ciudad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ciudades.Any(e => e.CiudadId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool ExisteNombre(string pronombre)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ciudades.Any(e => e.Nombre == pronombre);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                var ciudad = contexto.Ciudades.Find(id);

                if(ciudad != null)
                {
                    contexto.Ciudades.Remove(ciudad);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Ciudades Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Ciudades ciudad;

            try
            {
                ciudad = contexto.Ciudades.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ciudad;
        }

        private static List<Ciudades> GetList(Expression<Func<Ciudades, bool>> criterio)
        {
            List<Ciudades> lista = new List<Ciudades>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Ciudades.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

    }
}
