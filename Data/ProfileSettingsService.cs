using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

namespace it13Project.Data
{
    internal class ProfileSettingsService
    {
        /// <summary>
        /// Loads user details by userId.
        /// </summary>
        public static DataRow? GetUserById(int userId)
        {
            string query = "SELECT user_id, name, email, role FROM users WHERE user_id = @UserId";

            var dt = DatabaseHelper.ExecuteQuery(query,
                new SqlParameter("@UserId", userId));

            if (dt.Rows.Count > 0)
                return dt.Rows[0];

            return null;
        }

        /// <summary>
        /// Updates user profile (name, email, optional password).
        /// </summary>
        public static bool UpdateUserProfile(int userId, string name, string email, string? password = null)
        {
            string query;

            if (string.IsNullOrWhiteSpace(password))
            {
                query = @"UPDATE users 
                      SET name = @Name, email = @Email 
                      WHERE user_id = @UserId";
                return DatabaseHelper.ExecuteNonQuery(query,
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@UserId", userId)) > 0;
            }
            else
            {
                query = @"UPDATE users 
                      SET name = @Name, email = @Email, password_hash = @PasswordHash 
                      WHERE user_id = @UserId";

                string hashed = HashPassword(password);

                return DatabaseHelper.ExecuteNonQuery(query,
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@PasswordHash", hashed),
                    new SqlParameter("@UserId", userId)) > 0;
            }
        }

        /// <summary>
        /// Hash password (replace with BCrypt/Argon2 in production).
        /// </summary>
        private static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }


}