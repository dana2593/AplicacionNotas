using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AplicacionNotas
{
    public static class Constantes
    {
        public const string NombreArchivoBaseDatos = "NotasSQLite.db3";

        public const SQLiteOpenFlags Banderas =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public static string RutaBaseDatos =>
            Path.Combine(FileSystem.AppDataDirectory, NombreArchivoBaseDatos);
    }
}