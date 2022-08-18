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
    ///     Defines a custom entity, which represents the SQL object "Brand" from the SQL
    /// database.
    /// </summary>
    public class BrandEntity : BaseEntity
    {
        /// <summary>
        ///     Contains the relations between the given <see cref="BrandEntity"/>
        /// entity and other entities.
        /// </summary>
        public BrandRelations Relations { get; set; }

        /// <summary>
        ///     Contains the filters of the given <see cref="BrandEntity"/>.
        /// </summary>
        public BrandFilters Filters { get; set; }

        /// <summary>
        ///     Contains the values for the given <see cref="BrandEntity"/>.
        /// </summary>
        public BrandValues Values { get; set; }

        /// <summary>
        ///     Creates a new <see cref="BrandEntity"/> object (an extension of the
        /// <see cref="BaseEntity"/> object).
        /// </summary>
        public BrandEntity() : base("TB_BRAND", "BRN_ID")
        {
            Relations = new(this);
            Filters = new(this);
            Values = new(this);
        }

        /// <summary>
        ///     Selects a list of "Brand" objects from the database using an <see cref="EntityDB"/>
        /// object, returning them as brands. Any filter applied before the call of this method
        /// will affect the returned results.
        /// </summary>
        /// 
        /// <param name="_entityDB">A target <see cref="EntityDB"/> object to perform the query.</param>
        /// 
        /// <returns>
        ///     A list with a set of brands from the database.
        /// </returns>
        public List<Brand> Select(EntityDB _entityDB)
        {
            List<Brand> brands = new();

            using (var reader = _entityDB.Query(SQLSelect()))
            {
                while (reader.Read())
                {
                    Brand brand = new()
                    {
                        Id = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        Name = reader.GetString(2)
                    };

                    brands.Add(brand);
                }
            }

            ClearParameters();

            return brands;
        }
    }
}
