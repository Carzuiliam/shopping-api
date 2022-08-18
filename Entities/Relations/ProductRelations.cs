using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Relations
{
    /// <summary>
    ///     Defines an object that contains the relations between the <see cref="ProductEntity"/>
    /// and other entities.
    /// </summary>
    public class ProductRelations
    {
        /// <summary>
        ///     The parent <see cref="ProductEntity"/>.
        /// </summary>
        public ProductEntity Entity { set; get; }

        /// <summary>
        ///     Creates a new <see cref="ProductRelations"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public ProductRelations(ProductEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     Adds (binds) an entity to the given <see cref="ProductEntity"/>.
        /// </summary>
        /// 
        /// <param name="_entity">The entity to bind with the current entity.</param>
        /// <param name="_relationType">How the relation will be performed (full or optional).</param>
        public void Bind(BaseEntity _entity, EntityRelation.RelationMode _relationType)
        {
            Entity.BindedEntities.Add(new EntityRelation(_entity, _relationType));
        }
    }
}
