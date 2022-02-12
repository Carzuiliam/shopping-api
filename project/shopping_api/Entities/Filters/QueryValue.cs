namespace Shopping_API.Entities.Filters
{
    public class QueryValue
    {
        public string Attribute { get; set; }

        public string Value { get; set; }

        public QueryValue(string _attribute, object _value)
        {
            Attribute = _attribute;

            if (_value is bool)
            {
                Value = Convert.ToBoolean(_value) ? "1" : "0";
            }
            else if (_value is DateTime)
            {
                Value = string.Format("\'{0}\'", Convert.ToString(_value));
            }
            else
            {
                string? value = string.Format("{0}", Convert.ToString(_value));

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(_value), "Cannot convert the value parameter to string.");
                }

                Value = value;
            }
        }
    }
}
