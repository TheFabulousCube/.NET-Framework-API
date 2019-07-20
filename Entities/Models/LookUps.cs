using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LookUps
    {
        public enum Catagory
        {
            [EnumMember(Value = "Adults T-shirts")]
            adultts,
            [EnumMember(Value = "Infant Ts")]
            babyts,
            [EnumMember(Value = "Camisoles")]
            cams,
            [EnumMember(Value = "Childs T-shirts")]
            childsts,
            [EnumMember(Value = "State Magnets")]
            magnets,
            [EnumMember(Value = "Onesies")]
            onesies
        }
    }
}
