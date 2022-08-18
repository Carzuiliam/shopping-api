namespace Shopping_API.Entities.Base
{
    /// <summary>
    ///     Defines a relation (binding) between entities. When two entities are binded,
    /// it is possible to perform queries that uses both entities on a SQL database, e.g.,
    /// INNER JOIN or LEFT JOIN.
    /// </summary>
    public class EntityRelation
    {
        /// <summary>
        ///     Defines how two entities can be binded. The <see cref="FULL"/> value
        /// allows to perform <see cref="BaseEntity.SQLJoin()"/> as a INNER JOIN command,
        /// while the <see cref="OPTIONAL"/> value allows to perform the same method
        /// as a LEFT JOIN.
        /// </summary>
        public enum RelationMode
        {
            FULL,
            OPTIONAL
        }

        /// <summary>
        ///     Contains the binded entity.
        /// </summary>
        public BaseEntity BindedEntity { get; set; }

        /// <summary>
        ///     Contains the relation type between entities, as defined in the
        /// <see cref="RelationMode"/> field.
        /// </summary>
        public RelationMode RelationType { get; set; }

        /// <summary>
        ///     Creates a new <see cref="EntityRelation"/> object.
        /// </summary>
        /// 
        /// <param name="_bindedEntity">The entity to bind.</param>
        /// <param name="_relationType">The relation type between the entities.</param>
        public EntityRelation(BaseEntity _bindedEntity, RelationMode _relationType)
        {
            BindedEntity = _bindedEntity;
            RelationType = _relationType;
        }
    }
}
