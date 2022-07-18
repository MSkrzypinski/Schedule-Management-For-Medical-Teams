using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Domain.DDDBlocks
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetPropertiesToCompare();

        public bool Equals(ValueObject valueObject)
        {
            if (valueObject == null)
                return false;

            return GetPropertiesToCompare().SequenceEqual(valueObject.GetPropertiesToCompare());
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as ValueObject);
        }
        public static bool operator ==(ValueObject first,ValueObject second)
        {
            if (object.Equals(first, null))
            {
                if (object.Equals(second, null))
                {
                    return true;
                }
                return false;
            }

            return first.Equals(second);
        }
        public static bool operator !=(ValueObject first, ValueObject second)
        {
            return !(first==second);
        }
        public override int GetHashCode()
        {
            int hash = 13;
            foreach (var item in GetPropertiesToCompare())
            {
                unchecked
                {
                    hash = (hash * 7) + item.GetHashCode();
                }
            }
            return hash;
        }
    }
}
