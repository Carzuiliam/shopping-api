using Microsoft.Data.Sqlite;
using shopping_api.Entities.Extended;
using shopping_api.Models;
using shopping_api.Utils;

namespace shopping_api.Handler.Default
{
    public class UserHandler : BaseHandler
    {
        public Result<User> Result { get; set; }

        public UserHandler()
        {
            Result = new();
        }

        public Result<User> List()
        {
            List<User> users = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    UserEntity userEntity = new();

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = userEntity.Select();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Name = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = users;

            return Result;
        }

        public Result<User> Get(int _userId)
        {
            List<User> users = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    UserEntity userEntity = new();
                    userEntity.Filters.Id = _userId;

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = userEntity.Select();

                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            users.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Name = reader.GetString(2)
                            });
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = users;

            return Result;
        }
    }
}
