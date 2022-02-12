using shopping_api.Entities.Default;

namespace Shopping_API.Entities.Filters
{
    public class EntityFilter
    {
        public enum RelationType
        {
            FULL,
            OPTIONAL
        }

        public BaseEntity EntityClass { get; set; }

        public RelationType EntityRelation { get; set; }

        public EntityFilter(BaseEntity _entityClass, RelationType _entityRelation)
        {
            EntityClass = _entityClass;
            EntityRelation = _entityRelation;
        }
    }
}
