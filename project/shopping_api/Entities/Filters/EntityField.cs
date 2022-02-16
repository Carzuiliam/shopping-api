namespace Shopping_API.Entities.Attributes
{
    /// <summary>
    ///     Defines a field in an entity. This field consists basically as a mapping between
    /// an entity attribute and its value. Although this object is similar to structures like
    /// a key-value pair or a dictionary, this object is able to determine when an attribute
    /// holds a SQL's "NULL" value.
    /// </summary>
    public class EntityField
    {
        /// <summary>
        ///     Defines the NULL SQL value for integer types. The default value is
        /// <see cref="int.MinValue"/>.
        /// </summary>
        public static readonly int NULL_INT = int.MinValue;

        /// <summary>
        ///     Defines the NULL SQL value for decimal types. The default value is
        /// <see cref="decimal.MinValue"/>.
        /// </summary>
        public static readonly decimal NULL_DECIMAL = decimal.MinValue;

        /// <summary>
        ///     Defines the NULL SQL value for text types. The default value is the symbol
        /// for NULL types in SQL language, i.e., a low Greek letter omega (ω).
        /// </summary>
        public static readonly string NULL_STRING = "ω";

        /// <summary>
        ///     Contains the name of the attribute.
        /// </summary>
        public string Attribute { get; }

        /// <summary>
        ///     Contains the value of the attribute.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Creates a new <see cref="EntityField"/> object. You may use the fields
        /// <see cref="NULL_INT"/>, <see cref="NULL_DECIMAL"/> and <see cref="NULL_STRING"/>
        /// on the value parameter in order to set NULL values for the given attribute.
        /// </summary>
        /// 
        /// <param name="_attribute">The name of the attribute.</param>
        /// <param name="_value">The value of the attribute.</param>
        /// 
        /// <exception cref="ArgumentException">When the value parameter is an object from an invalid type.</exception>
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
                        throw new ArgumentException(nameof(_value), "Cannot convert the value parameter to string.");
                    }
            }
          

            Attribute = _attribute;
            Value = value;
        }
    }
}
