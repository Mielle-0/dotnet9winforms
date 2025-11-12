using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace it13Project.Data
{
    internal class DatabaseHelper
    {

        // Centralized connection string (you can move this to app.config for production)
        private static readonly string connectionString;
            //  @"Data Source=DESKTOP-626PCB6\SQLEXPRESS;Initial Catalog=VideoGameCRM;Integrated Security=True;Trust Server Certificate=True";
            // @"Server=; Database=; User Id=; Password=; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";
             
        static DatabaseHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            string server = config["Database:Server"];
            string database = config["Database:Database"];
            string username = config["Database:Username"];
            string password = config["Database:Password"];

            connectionString = $@"Server={server};Database={database};User Id={username};Password={password};Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";
        }

        /// <summary>
        /// Executes a SELECT query and returns a DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Executes INSERT/UPDATE/DELETE and returns rows affected
        /// </summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a scalar query (returns a single value)
        /// </summary>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
