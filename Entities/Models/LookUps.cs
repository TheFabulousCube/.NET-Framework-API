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
        public enum Catagory_Lookup
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

        public enum Role
        {
            [EnumMember(Value = "Administrator")]
            a,
            [EnumMember(Value = "Guest")]
            g,
            [EnumMember(Value = "User")]
            u
        }

        public enum sleeve
        {
            s34,
            fl,
            no,
            sh
        }

        //public enum Size
        //{
        //    [EnumMember(Value = "1218 Months")]
        //    monthm,
        //    [EnumMember(Value = "36 Months")]
        //    36m,
        //    [EnumMember(Value = "69 Months")]
        //    69m,
        //    [EnumMember(Value = "912 Months")]
        //    912m,
        //    [EnumMember(Value = "Adult Large")]
        //    adlg,
        //    [EnumMember(Value = "Adult Medium")]
        //    ad-med,
        //    [EnumMember(Value = "Adult Small")]
        //    ad-sm, 
        //    [EnumMember(Value = "Adult X-Large")]
        //    ad-xlg, 
        //    [EnumMember(Value = "Adult X-Small")]
        //    ad-xs, 
        //    [EnumMember(Value = "Adult XX-Large")]
        //    ad-xxl, 
        //    [EnumMember(Value = "Youth Large")]
        //    y-lg,
        //    [EnumMember(Value = "Youth Medium")]
        //    y-med, 
        //    [EnumMember(Value = "Youth Small")]
        //    y-sm, 
        //    [EnumMember(Value = "Youth X-Large")]
        //    y-xlg, 
        //    [EnumMember(Value = "Youth X-Small")]
        //    y-xsm, 
        //    [EnumMember(Value = "Youth 8-10")]
        //    y8-10

        //}
    }
}
