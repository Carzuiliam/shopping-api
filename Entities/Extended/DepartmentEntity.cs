using Microsoft.Data.Sqlite;
using Shopping_API.Entities.Base;
using Shopping_API.Entities.Connection;
using Shopping_API.Entities.Filters;
using Shopping_API.Entities.Relations;
using Shopping_API.Entities.Values;
using Shopping_API.Models;

namespace Shopping_API.Entities.Extended
{
    /// <summary>
    ///     Defines a custom entity, which represents the SQL object "Department" from the SQL
    /// database.
    /// </summary>
    public class DepartmentEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="DepartmentEntity"/>
        /// entity and other entities.
        /// </summary>
        public DepartmentRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="DepartmentEntity"/>.
        /// </summary>
        public DepartmentFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="DepartmentEntity"/>.
        /// </summary>
        public DepartmentValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="DepartmentEntity"/> object (an extension of
        /// the <see cref="BaseEntity"/> object).
        /// </summary>
        public DepartmentEntity() : base("TB_DEPARTMENT", "DPR_ID") 
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Selects a list of "Department" objects from the database using an
        /// <see cref="EntityDB"/> object, returning them as departments. Any filter
        /// applied before the call of this method will affect the returned results.
        /// </summary>
        /// 
        /// <param name="_entityDB">A target <see cref="EntityDB"/> object to perform the query.</param>
        /// 
        /// <returns>
        ///     A list with a set of departments from the database.
        /// </returns>
        public List<Department> Select(EntityDB _entityDB)
        {
            List<Department> departments = new();

            using (var reader = _entityDB.Query(SQLSelect()))
            {
                while (reader.Read())
                {
                    Department department = new()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };

                    departments.Add(department);
                }
            }

            ClearParameters();

            return departments;
        }
    }
}
