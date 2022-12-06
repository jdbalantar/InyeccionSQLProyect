using InyeccionSQLProyect.DataContext;
using InyeccionSQLProyect.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace InyeccionSQLProyect.Services.Oracle
{
    /// <summary>
    /// Clase que contiene los métodos para el registro de información a la base de datos Oracle
    /// </summary>
    public class OUsersServices : IOUsersServices
    {
        private readonly OracleContext _context;

        public OUsersServices(OracleContext context)
        {
            _context = context;
        }

        //Los métodos de esta región están el SQL concatenando variables, dejando vulnerable la base de datos
        #region  MetodosSinSeguridad

        public List<UsuariosOracle> GetUserByNameWithoutSecurity(string name)
        {
            string sql = $"SELECT ID, NOMBRE FROM ORACLEAPIDB.USUARIOS WHERE NOMBRE = '{name}'";
            return _context.Usuarios.FromSqlRaw(sql).ToList();
        }

        public string InsertUserWithoutSecurity(string name)
        {
            var Id = _context.Usuarios.Count();

            string sql = $"INSERT INTO ORACLEAPIDB.USUARIOS (ID, NOMBRE) VALUES('{Id}', '{name}')";
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

        public List<UsuariosOracle> GetUserByNameWithSecurity(string name)
        {
            string sql = $"SELECT ID, NOMBRE FROM ORACLEAPIDB.USUARIOS WHERE NOMBRE = :Name";
            OracleParameter nameParameter = new OracleParameter("Name", name);
            return _context.Usuarios.FromSqlRaw(sql, nameParameter).ToList();
        }
        public string InsertUserWithSecurity(string name)
        {
            int Id = _context.Usuarios.Count();
            string sql = $"INSERT INTO ORACLEAPIDB.USUARIOS (ID, NOMBRE) VALUES('{Id}', :Name)";
            string result;
            try
            {
                OracleParameter nameParameter = new OracleParameter("Name", name);
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
        public List<UsuariosOracle> GetUsers()
        {
            string sql = "SELECT ID, NOMBRE FROM ORACLEAPIDB.USUARIOS";
            return _context.Usuarios.FromSqlRaw(sql).ToList();
        }
        #endregion
    }
}