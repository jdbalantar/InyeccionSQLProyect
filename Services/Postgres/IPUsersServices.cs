using InyeccionSQLProyect.Models;

namespace InyeccionSQLProyect.Services.Postgres
{
    public interface IPUsersServices
    {
        public List<UsuariosPostgres> GetUsers();
        public List<UsuariosPostgres> GetUserByNameWithSecurity(string name);
        public List<UsuariosPostgres> GetUserByNameWithoutSecurity(string name);
        public string InsertUserWithSecurity(string name);
        public string InsertUserWithoutSecurity(string name);
    }
}
