using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class UserDataStoreDisconnected
    {
        SqlConnection connection;
        DataSet ds;
        public UserDataStoreDisconnected(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            string sql = "Select * from userdata";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            ds = new DataSet();
            adapter.Fill(ds, "userdata");
        }

        public Roles ValidateUser(string username, string password)
        {
            DataTable dt = ds.Tables["userdata"];
            DataRow[] row = dt.Select($"username = '{username}' and password = '{password}'");
            Predicate<int> RowLengthCheck = l => l == 1;
            if(RowLengthCheck(row.Length))
            {
                string role = row[0]["userrole"].ToString();
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
    }
}
