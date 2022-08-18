using Shopping_API.Entities.Base;
using Shopping_API.Entities.Extended;

namespace Shopping_API.Entities.Relations
{
    /// <summary>
    ///     Defines an object that contains the relations between the <see cref="UserEntity"/>
    /// and other entities.
    /// </summary>
    public class UserRelations
    {
        /// <summary>
        ///     The parent <see cref="UserEntity"/>.
        /// </summary>
        public UserEntity Entity { set; get; }

        /// <summary>
        ///     Creates a new <see cref="UserRelations"/> object.
        /// </summary>
        /// 
        /// <param name="_entity">The parent entity.</param>
        public UserRelations(UserEntity _entity)
        {
            Entity = _entity;
        }

        /// <summary>
        ///     Adds (binds) an entity to the given <see cref="UserEntity"/>.
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
