using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Relations
{
    /// <summary>
    ///     Defines an object that contains the relations between the <see cref="CartEntity"/>
    /// and other entities.
    /// </summary>
    public class CartRelations
    {
        /// <summary>
        ///     The parent <see cref="CartEntity"/>.
        /// </summary>
        public CartEntity Entity { set; get; }

        /// <summary>
        ///     Creates a new <see cref="CartRelations"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public CartRelations(CartEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     Adds (binds) an entity to the given <see cref="CartEntity"/>.
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
