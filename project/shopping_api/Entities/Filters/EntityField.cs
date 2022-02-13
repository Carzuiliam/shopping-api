namespace Shopping_API.Entities.Filters
{
    public class EntityField
    {
        public static readonly int NULL_INT = int.MinValue;
        public static readonly decimal NULL_DECIMAL = decimal.MinValue;
        public static readonly string NULL_STRING = "ω";

        public string Attribute { get; }

        public string Value { get; }

        public EntityField(string _attribute, object _value)
        {
            string value;

            switch (_value)
            {
                case int:
                    {
                        int aux = Convert.ToInt32(_value);

                        value = (aux != NULL_INT) ? Convert.ToString(aux) : "NULL";
                    }
                    break;

                case decimal:
                    {
                        decimal aux = Convert.ToDecimal(_value);

                        value = (aux != NULL_DECIMAL) ? Convert.ToString(aux) : "NULL";
                    }
                    break;

                case bool:
                    {
                        bool aux = Convert.ToBoolean(_value);

                        value = aux ? "1" : "0";
                    }
                    break;

                case string:
                    {
                        string? aux = Convert.ToString(_value);

                        value = (aux != null || aux != NULL_STRING) ? string.Format("\'{0}\'", Convert.ToString(aux)) : "NULL";
                    }
                    break;

                case DateTime:
                    {
                        DateTime aux = Convert.ToDateTime(_value);

                        value = string.Format("\'{0}\'", aux.ToString());
                    }
                    break;

                default:
                    {
                        throw new ArgumentNullException(nameof(_value), "Cannot convert the value parameter to string.");
                    }
            }
          

            Attribute = _attribute;
            Value = value;
        }
    }
}
