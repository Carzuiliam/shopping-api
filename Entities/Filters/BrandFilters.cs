using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Filters
{
    /// <summary>
    ///     Contains a mapping between SQL attributes for the "Brand" database object
    /// and the <see cref="BrandEntity"/> class, which is utilized to filter values
    /// from the same database object.
    /// </summary>
    public class BrandFilters
    {
        /// <summary>
        ///     The parent <see cref="BrandEntity"/>.
        /// </summary>
        public BrandEntity Entity { set; get; }

        ///     Internal fields of the class.
        private int _id = 0;
        private string _code = "";
        private string _name = "";

        /// <summary>
        ///     Creates a new <see cref="BrandFilters"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public BrandFilters(BrandEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="BrandEntity"/>
        /// attribute.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                Entity.QueryFilters.Add(new EntityField("BRN_ID", _id));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="BrandEntity"/>
        /// attribute.
        /// </summary>
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                Entity.QueryFilters.Add(new EntityField("BRN_CODE", _code));
            }
        }

        /// <summary>
        ///     A field that contains a filter for the corresponding <see cref="BrandEntity"/>
        /// attribute.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Entity.QueryFilters.Add(new EntityField("BRN_NAME", _name));
            }
        }
    }
}
