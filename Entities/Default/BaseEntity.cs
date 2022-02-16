using Shopping_API.Entities.Attributes;
using System.Text;

namespace shopping_api.Entities.Default
{
    /// <summary>
    ///     Defines a base entity. In this project/context, an entity is an object that
    /// acts as an "additional layer" between the application itself and an element (e.g.,
    /// a table) on a SQL database. Its objective is to optimize how the application
    /// performs operations on the given database by building queries, filtering attributes
    /// and handling query parameters.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        ///     Contains a list of entities to bind with the current entity. Binded entities
        /// are used to perform cross-entity operations like INNER or LEFT joins using the
        /// <see cref="Join()"/> method.
        /// </summary>
        private List<EntityRelation> BindedEntities { get; set; }

        /// <summary>
        ///     Contains a list of fields to be used as query filters. These filters affects
        /// how the queries are performed -- for example, you can add filters on this field
        /// before calling the <see cref="Select()"/> method in order to perform a "SELECT
        /// (...) WHERE (...)" operation.
        /// </summary>
        private List<EntityField> QueryFilters { get; set; }

        /// <summary>
        ///     Contains a list of fields to be set values for an element on the database.
        /// For example, you can add values to set into database objects before calling the
        /// <see cref="Update()"/> method in order to perform a "UPDATE (...) SET (...)
        /// WHERE (...)" operation.
        /// </summary>
        private List<EntityField> FieldValues { get; set; }
        
        /// <summary>
        ///     Contains the name of the current entity.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        ///     Contains the (primary) key of the current entity.
        /// </summary>
        public string EntityKey { get; set; }

        /// <summary>
        ///     Creates a new <see cref="BaseEntity"/> object.
        /// </summary>
        /// 
        /// <param name="_entityName">The name of the entity.</param>
        /// <param name="_entityKey">The (primary) key of the entity.</param>
        public BaseEntity(string _entityName, string _entityKey)
        {
            EntityName = _entityName;
            EntityKey = _entityKey;

            BindedEntities = new();
            QueryFilters = new();
            FieldValues = new();
        }

        /// <summary>
        ///     Clears all the parameters in the entity.
        /// </summary>
        private void ClearParameters()
        {
            BindedEntities.Clear();
            QueryFilters.Clear();
            FieldValues.Clear();
        }

        /// <summary>
        ///     Adds (binds) a new entity filter to the entity.
        /// </summary>
        /// 
        /// <param name="_entity">The entity to bind with the current entity.</param>
        /// <param name="_relationType">How the relation will be performed (full or optional).</param>
        protected void AddEntityFilter(BaseEntity _entity, EntityRelation.RelationMode _relationType)
        {
            BindedEntities.Add(new EntityRelation(_entity, _relationType));
        }

        /// <summary>
        ///     Adds a new attribute to use as a query filter.
        /// </summary>
        /// 
        /// <param name="_attribute">The name of the attribute.</param>
        /// <param name="_value">The value of the attribute added.</param>
        protected void AddQueryFilter(string _attribute, object _value)
        {
            QueryFilters.Add(new EntityField(_attribute, _value));
        }

        /// <summary>
        ///     Adds a new attribute to use as a field value.
        /// </summary>
        /// 
        /// <param name="_attribute">The name of the attribute.</param>
        /// <param name="_value">The value of the attribute added.</param>
        protected void AddFieldValue(string _attribute, object _value)
        {
            FieldValues.Add(new EntityField(_attribute, _value));
        }

        /// <summary>
        ///     Returns a SQL's INSERT command as as text, based on the field values added on
        /// the <see cref="FieldValues"/> element.
        /// </summary>
        /// 
        /// <returns>
        ///     A SQL command string with the full INSERT command, e.g., "INSERT (...) VALUES (...)".
        /// </returns>
        public string Insert()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("INSERT INTO {0} (", EntityName);

            for (int i = 0; i < FieldValues.Count; i++)
            {
                string strColumn = (i == FieldValues.Count - 1) ? "{0}" : "{0}, ";
                strBuilder.AppendFormat(strColumn, FieldValues[i].Attribute);
            }

            strBuilder.Append(") VALUES (");

            for (int i = 0; i < FieldValues.Count; i++)
            {
                string strColumn = (i == FieldValues.Count - 1) ? "{0}" : "{0}, ";
                strBuilder.AppendFormat(strColumn, FieldValues[i].Value);
            }

            strBuilder.Append(')');

            ClearParameters();

            return strBuilder.ToString();
        }

        /// <summary>
        ///     Returns a SQL's SELECT command as as text, based on the query filters added on
        /// the <see cref="QueryFilters"/> element. This is the only method that will return
        /// a valid SQL command even if there is no query filters added previously, as a 
        /// "SELECT * FROM (...)" command.
        /// </summary>
        /// 
        /// <returns>
        ///     A SQL command string with the full SELECT command, e.g., "SELECT (...) WHERE (...)".
        /// </returns>
        public string Select()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("SELECT * FROM {0}", EntityName);
            
            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE {0} = {1}" : " AND {0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearParameters();

            return strBuilder.ToString();
        }

        /// <summary>
        ///     Returns a SQL's JOIN command as as text, based on the binded entities on
        /// <see cref="BindedEntities"/> and the query filters added on the <see cref="QueryFilters"/>
        /// element.
        /// </summary>
        /// 
        /// <returns>
        ///     A SQL command string with the full SELECT command, e.g., "SELECT (...) JOIN (...) WHERE (...)".
        /// </returns>
        public string Join()
        {
            StringBuilder strBuilder = new();

            strBuilder.Append("SELECT TBL.*");

            for (int i = 0; i < BindedEntities.Count; i++)
            {
                strBuilder.AppendFormat(", AX{0}.*", i);
            }

            strBuilder.AppendFormat(" FROM {0} TBL", EntityName);

            for (int i = 0; i < BindedEntities.Count; i++)
            {
                switch (BindedEntities[i].RelationType) {
                    
                    case EntityRelation.RelationMode.FULL:
                        {
                            strBuilder.AppendFormat(
                                " INNER JOIN {0} AX{1} ON TBL.{2} = AX{1}.{2} ",
                                BindedEntities[i].BindedEntity.EntityName, i, BindedEntities[i].BindedEntity.EntityKey
                            );
                        }
                        break;

                    case EntityRelation.RelationMode.OPTIONAL:
                        {
                            strBuilder.AppendFormat(
                                " LEFT JOIN {0} AX{1} ON TBL.{2} = AX{1}.{2} ",
                                BindedEntities[i].BindedEntity.EntityName, i, BindedEntities[i].BindedEntity.EntityKey
                            );
                        }
                        break;

                    default:
                        {
                            throw new ArgumentNullException("EntityFilter", "Invalid RelationType for the entity.");
                        }
                }                                
            }

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE TBL.{0} = {1}" : " AND TBL.{0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearParameters();

            return strBuilder.ToString();
        }

        /// <summary>
        ///     Returns a SQL's UPDATE command as as text, based on the field values added on
        /// the <see cref="FieldValues"/> element.
        /// </summary>
        /// 
        /// <returns>
        ///     A SQL command string with the full UPDATE command, e.g., "UPDATE (...) SET (...) WHERE (...)".
        /// </returns>
        public string Update()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("UPDATE {0} SET", EntityName);

            for (int i = 0; i < FieldValues.Count; i++)
            {
                string strColumn = (i == FieldValues.Count - 1) ? " {0} = {1}" : " {0} = {1},";
                strBuilder.AppendFormat(strColumn, FieldValues[i].Attribute, FieldValues[i].Value);
            }

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE {0} = {1}" : " AND {0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearParameters();

            return strBuilder.ToString();
        }

        /// <summary>
        ///     Returns a SQL's DELETE command as as text, based on the query filters added on
        /// the <see cref="QueryFilters"/> element.
        /// </summary>
        /// 
        /// <returns>
        ///     A SQL command string with the full DELETE command, e.g., "DELETE (...) WHERE (...)".
        /// </returns>
        public string Delete()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("DELETE FROM {0}", EntityName);

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE {0} = {1}" : " AND {0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearParameters();

            return strBuilder.ToString();
        }
    }
}
