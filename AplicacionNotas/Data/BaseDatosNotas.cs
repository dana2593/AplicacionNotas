using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AplicacionNotas.Models;

namespace AplicacionNotas.Data
{
    public class BaseDatosNotas
    {
        private SQLiteConnection conn;
        private readonly string _dbPath;

        public string StatusMessage { get; set; }

        public BaseDatosNotas(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Nota>();
        }

        public void AgregarNuevaNota(string texto)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(texto))
                    throw new Exception("Texto válido requerido");

                result = conn.Insert(new Nota { Texto = texto, Fecha = DateTime.Now });

                StatusMessage = string.Format("{0} registro(s) agregado(s) [Texto: {1})", result, texto);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Falló al agregar {0}. Error: {1}", texto, ex.Message);
            }
        }

        public List<Nota> ObtenerTodasLasNotas()
        {
            try
            {
                Init();
                return conn.Table<Nota>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Falló al recuperar datos. {0}", ex.Message);
            }

            return new List<Nota>();
        }

        public void EliminarNota(int id)
        {
            try
            {
                Init();

                var nota = conn.Table<Nota>().FirstOrDefault(n => n.Id == id);
                if (nota != null)
                {
                    conn.Delete(nota);
                    StatusMessage = "Nota eliminada correctamente";
                }
                else
                {
                    StatusMessage = "Nota no encontrada";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar: {0}", ex.Message);
            }
        }

        public void ModificarNota(int id, string nuevoTexto)
        {
            try
            {
                Init();

                var nota = conn.Table<Nota>().FirstOrDefault(n => n.Id == id);
                if (nota != null)
                {
                    nota.Texto = nuevoTexto;
                    nota.Fecha = DateTime.Now;
                    conn.Update(nota);
                    StatusMessage = "Nota modificada correctamente";
                }
                else
                {
                    StatusMessage = "Nota no encontrada";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al modificar: {0}", ex.Message);
            }
        }
    }

}