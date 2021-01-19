using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ASP.NETCoreWebApplication
{
    public class ClientRole
    {
        public int ClientId { get; set; }
        public string RoleName { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public int Rank { get; set; }
    }
    
    public interface ISqlUserHelper
    {
        IEnumerable<Client> GetAll();
        string AddRole(ClientRole clientRole);
        string DeleteRole(ClientRole clientRole);
    }
    
    public class SqlUserHelper : SqlHelper, ISqlUserHelper
    {
        public SqlUserHelper(string connectionString) : base(connectionString)
        {
        }
        
        public IEnumerable<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public string AddRole(ClientRole clientRole)
        {
            return AppendOrDeleteRole(clientRole);
        }

        public string DeleteRole(ClientRole clientRole)
        {
            return AppendOrDeleteRole(clientRole, true);
        }
        
        private string AppendOrDeleteRole(ClientRole role, bool isDelete = false)
        {
            using var conn = new SqlConnection(cnnString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                conn.Close();
            }

            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = isDelete ? "RemoveRole" : "AppendRole"
            };
            cmd.Parameters.Add("@clientId", SqlDbType.Int);
            cmd.Parameters["@clientId"].Value = role.ClientId;
            cmd.Parameters.Add("@role", SqlDbType.NVarChar);
            cmd.Parameters["@role"].Value = role.RoleName;

            var currentClientRoles = cmd.ExecuteScalar();

            conn.Close();

            return (string) currentClientRoles;
        }
    }
}