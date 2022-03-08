using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Filters
{
    /// <summary>
    ///     Contains a mapping between SQL attributes for the "Department" database object
    /// and the <see cref="DepartmentEntity"/> class, which is utilized to filter values
    /// from the same database object.
    /// </summary>
    public class DepartmentFilters
    {
        /// <summary>
        ///     The parent <see cref="DepartmentEntity"/>.
        /// </summary>
        public DepartmentEntity Entity { set; get; }

        ///     Internal fields of the class.
        private int _id = 0;
        private string _name = "";

        /// <summary>
        ///     Creates a new <see cref="DepartmentFilters"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public DepartmentFilters(DepartmentEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="DepartmentEntity"/>
        /// attribute.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                Entity.QueryFilters.Add(new EntityField("DPR_ID", _id));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="DepartmentEntity"/>
        /// attribute.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Entity.QueryFilters.Add(new EntityField("DPR_NAME", _name));
            }
        }
    }
}
