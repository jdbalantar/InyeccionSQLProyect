using InyeccionSQLProyect.Models;

namespace InyeccionSQLProyect.Services.Oracle
{
    public interface IOUsersServices
    {
        public List<UsuariosOracle> GetUsers();
        public List<UsuariosOracle> GetUserByNameWithSecurity(string name);
        public List<UsuariosOracle> GetUserByNameWithoutSecurity(string name);
        public string InsertUserWithSecurity(string name);
        public string InsertUserWithoutSecurity(string name);
    }
}
