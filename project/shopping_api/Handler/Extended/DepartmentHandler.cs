using Microsoft.Data.Sqlite;
using shopping_api.Entities;
using shopping_api.Entities.Extended;
using shopping_api.Models;
using shopping_api.Utils;

namespace shopping_api.Handler.Default
{
    public class DepartmentHandler : BaseHandler
    {
        public Result<Department> Result { get; set; }

        public DepartmentHandler()
        {
            Result = new();
        }

        public Result<Department> List()
        {
            List<Department> departments = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    DepartmentEntity departmentEntity = new();

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = departmentEntity.Select();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = departments;

            return Result;
        }

        public Result<Department> Get(int _departmentId)
        {
            List<Department> departments = new();

            try
            {
                using (var db = new SqliteConnection(CONNECTION_STRING))
                {
                    db.Open();

                    DepartmentEntity departmentEntity = new();
                    departmentEntity.Filters.Id = _departmentId;

                    SqliteCommand command = db.CreateCommand();
                    command.CommandText = departmentEntity.Select();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            departments.Add(new()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }                     
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status.Capture(ex);
            }

            Result.Data = departments;

            return Result;
        }
    }
}
