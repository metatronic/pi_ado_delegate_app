using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class UserDataStore
    {
        SqlConnection connection;
//        SqlCommand command;
//        SqlDataReader reader;

        public UserDataStore(string connectionstring)
        {
            connection = new SqlConnection(connectionstring);
        }

        public Roles ValidateUser(string username, string password)
        {
            try
            {
                string sql = "Select userrole from userdata where username = @usrname and password = @pswd";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("usrname", username);
                command.Parameters.AddWithValue("pswd", password);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                string role = null;
                object output = command.ExecuteScalar();
                //Pridicate example
                Predicate<object> isObjNotNull = obj => obj != null;
                if (isObjNotNull(output))
                {
                    role = output.ToString();
                    if (role.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Roles.Admin;
                    }
                    else
                    {
                        return Roles.User;
                    }
                }
                else
                {
                    throw new IncorrectCredentialException("Username or password invalid");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
