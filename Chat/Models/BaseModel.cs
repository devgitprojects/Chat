using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class BaseModel : IEquatable<BaseModel>
    {
        public int Id { get; set; }

        #region IEquatable<BaseModel>

        public override int GetHashCode()
        {
            var hashCode = 1736594352;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseModel);
        }
        public bool Equals(BaseModel other)
        {
            return other != null
                && Id == other.Id;
        }

        #endregion
    }
}
