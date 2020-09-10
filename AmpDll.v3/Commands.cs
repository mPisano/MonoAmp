using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    public static class StringEnum
    {
        public static string GetCodeValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            System.Reflection.FieldInfo fi = type.GetField(value.ToString());
            CodeValue[] attrs =
            fi.GetCustomAttributes(typeof(CodeValue), false) as CodeValue[];
            if (attrs.Length > 0)
                output = attrs[0].Value;
            return output;
        }
    }

    public class CodeValue : System.Attribute
    {
        private string _value;
        public CodeValue(string value)
        {
            _value = value;
        }
        public string Value
        {
            get { return _value; }
        }
    }
    public enum Command   
    {
        [CodeValue("PA")]
        Public_Address,
        [CodeValue("PR")]
        Power,
        [CodeValue("MU")]
        Mute,
        [CodeValue("DT")]
        Do_Not_Disturb,
        [CodeValue("VO")]
        Volume,
        [CodeValue("TR")]
        Treble,
        [CodeValue("BS")]
        Bass,
        [CodeValue("BL")]
        Balance,
        [CodeValue("CH")]
        Source,
        [CodeValue("LS")]
        Connected
    };

    public static class CommandValues
    {
        public static Int32 Max(string Property)
        {
            switch (Property)
            {

                case "PA":
                    return 1;
                case "PR":
                    return 1;
                case "MU":
                    return 1;
                case "DT":
                    return 1;
                case "VO":
                    return 38;
                case "TR":
                    return 14;
                case "BS":
                    return 14;
                case "BL":
                    return 14;
                case "CH":
                    return 6;
                case "LS":
                    return 1;
            }
            return 0;
        }

        public static Int32 Min(string Property)
        {
            if (Property == "CH")
                return 1;
            else
                return 0;
        }
    }
}

        //'Select Case True
        //'    Case code = "PA"
        //'        name = "Public Address"
        //'    Case code = "PR"
        //'        name = "Power"
        //'    Case code = "MU"
        //'        name = "Mute"
        //'    Case code = "DT"
        //'        name = "Do Not Disturb"
        //'    Case code = "VO"
        //'        name = "Volume"
        //'    Case code = "TR"
        //'        name = "Treble"
        //'    Case code = "BS"
        //'        name = "Bass"
        //'    Case code = "BL"
        //'        name = "Balance"
        //'    Case code = "CH"
        //'        name = "Source"
        //'    Case code = "LS"
        //'        name = "Connected"
        //'End Select