using Shopping_API.Entities.Filters;
using System.Text;

namespace shopping_api.Entities.Default
{
    public class BaseEntity
    {
        private List<EntityFilter> QueryEntities { get; set; }

        private List<EntityField> QueryFilters { get; set; }

        private List<EntityField> QueryValues { get; set; }
        
        public string EntityName { get; set; }

        public string EntityKey { get; set; }

        public BaseEntity(string _entityName, string _entityKey)
        {
            EntityName = _entityName;
            EntityKey = _entityKey;

            QueryEntities = new();
            QueryFilters = new();
            QueryValues = new();
        }

        private void ClearFilters()
        {
            QueryEntities.Clear();
            QueryFilters.Clear();
            QueryValues.Clear();
        }

        protected void AddEntityFilter(BaseEntity _entity, EntityFilter.RelationType _relationType)
        {
            QueryEntities.Add(new EntityFilter(_entity, _relationType));
        }

        protected void AddQueryFilter(string _attribute, object _value)
        {
            QueryFilters.Add(new EntityField(_attribute, _value));
        }

        protected void AddQueryValue(string _attribute, object _value)
        {
            QueryValues.Add(new EntityField(_attribute, _value));
        }

        public string Insert()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("INSERT INTO {0} (", EntityName);

            for (int i = 0; i < QueryValues.Count; i++)
            {
                string strColumn = (i == QueryValues.Count - 1) ? "{0}" : "{0}, ";
                strBuilder.AppendFormat(strColumn, QueryValues[i].Attribute);
            }

            strBuilder.Append(") VALUES (");

            for (int i = 0; i < QueryValues.Count; i++)
            {
                string strColumn = (i == QueryValues.Count - 1) ? "{0}" : "{0}, ";
                strBuilder.AppendFormat(strColumn, QueryValues[i].Value);
            }

            strBuilder.Append(')');

            ClearFilters();

            return strBuilder.ToString();
        }

        public string Select()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("SELECT * FROM {0}", EntityName);
            
            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE {0} = {1}" : " AND {0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearFilters();

            return strBuilder.ToString();
        }

        public string Join()
        {
            StringBuilder strBuilder = new();

            strBuilder.Append("SELECT TBL.*");

            for (int i = 0; i < QueryEntities.Count; i++)
            {
                strBuilder.AppendFormat(", AX{0}.*", i);
            }

            strBuilder.AppendFormat(" FROM {0} TBL", EntityName);

            for (int i = 0; i < QueryEntities.Count; i++)
            {
                switch (QueryEntities[i].EntityRelation) {
                    
                    case EntityFilter.RelationType.FULL:
                        {
                            strBuilder.AppendFormat(
                                " INNER JOIN {0} AX{1} ON TBL.{2} = AX{1}.{2} ",
                                QueryEntities[i].EntityClass.EntityName, i, QueryEntities[i].EntityClass.EntityKey
                            );
                        }
                        break;

                    case EntityFilter.RelationType.OPTIONAL:
                        {
                            strBuilder.AppendFormat(
                                " LEFT JOIN {0} AX{1} ON TBL.{2} = AX{1}.{2} ",
                                QueryEntities[i].EntityClass.EntityName, i, QueryEntities[i].EntityClass.EntityKey
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

            ClearFilters();

            return strBuilder.ToString();
        }

        public string Update()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("UPDATE {0} SET", EntityName);

            for (int i = 0; i < QueryValues.Count; i++)
            {
                string strColumn = (i == QueryValues.Count - 1) ? " {0} = {1}" : " {0} = {1},";
                strBuilder.AppendFormat(strColumn, QueryValues[i].Attribute, QueryValues[i].Value);
            }

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE {0} = {1}" : " AND {0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearFilters();

            return strBuilder.ToString();
        }

        public string Delete()
        {
            StringBuilder strBuilder = new();

            strBuilder.AppendFormat("DELETE FROM {0}", EntityName);

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                string strColumn = (i == 0) ? " WHERE {0} = {1}" : " AND {0} = {1}";
                strBuilder.AppendFormat(strColumn, QueryFilters[i].Attribute, QueryFilters[i].Value);
            }

            ClearFilters();

            return strBuilder.ToString();
        }
    }
}
