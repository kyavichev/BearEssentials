using System.Collections.Generic;

namespace Bears.Core
{
    public static class DictionaryExtensions
    {
        public static void AddOrModify<TA,TB>(this Dictionary<TA,TB> dictionary, TA key, TB value)
        {
            if ( dictionary.ContainsKey( key ) )
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add( key, value );
            }
        }
    }
}