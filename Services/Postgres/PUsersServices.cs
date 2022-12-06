using InyeccionSQLProyect.DataContext;
using InyeccionSQLProyect.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace InyeccionSQLProyect.Services.Postgres
{
    /// <summary>
    /// Clase que contiene los métodos para el registro de información a la base de datos Oracle
    /// </summary>
    public class PUsersServices : IPUsersServices
    {
        private readonly PgContext _context;

        public PUsersServices(PgContext context)
        {
            _context = context;
        }
        //Los métodos de esta región están el SQL concatenando variables, dejando vulnerable la base de datos
        #region  MetodosSinSeguridad

        public List<UsuariosPostgres> GetUserByNameWithoutSecurity(string name)
        {
            string sql = $@"SELECT ""id"", ""nombre"" FROM public.""usuarios"" WHERE ""nombre"" = '{name}'";
            return _context.Usuarios.FromSqlRaw(sql).ToList();
        }

        public string InsertUserWithoutSecurity(string name)
        {
            string sql = @$"INSERT INTO public.""usuarios"" (""id"", ""nombre"") VALUES(DEFAULT, '{name}')";
            string result;
            try
            {
                _context.Database.ExecuteSqlRaw(sql);
                result = "ok";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        #endregion

        //Los métodos de esta región están usando parámetros para evitar fugas de seguridad
        #region  MetodosConSeguridad
        public List<UsuariosPostgres> GetUserByNameWithSecurity(string name)
        {
            string sql = $@"SELECT ""id"", ""nombre"" FROM public.""usuarios"" WHERE ""nombre"" = (@Name)";
            NpgsqlParameter nameParameter = new NpgsqlParameter("Name", name);
            return _context.Usuarios.FromSqlRaw(sql, nameParameter).ToList();
        }

        public string InsertUserWithSecurity(string name)
        {
            string sql = $@"INSERT INTO public.""usuarios"" (""id"", ""nombre"") VALUES(DEFAULT, (@Name))";
            string result;
            try
            {
                NpgsqlParameter nameParameter = new NpgsqlParameter("Name", name);
                _context.Database.ExecuteSqlRaw(sql, nameParameter);
                result = "ok";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        #endregion

        #region MetodosQueNoRequierenSeguridadExtra
        public List<UsuariosPostgres> GetUsers()
        {
            string sql = @"SELECT ""id"", ""nombre"" FROM public.""usuarios""";
            return _context.Usuarios.FromSqlRaw(sql).ToList();
        }
        #endregion
    }
}