using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.BL
{
    public class TextUtils
    {

        /// <summary>
        /// Removes diacritics and replaces non-alphanumeric characters with dashes.
        /// </summary>
        public static string ToUrlName(string name)
        {
            var sb = new StringBuilder();

            name = name.Normalize(NormalizationForm.FormD);
            for (int i = 0; i < name.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(name[i]) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(name[i]);
                }
                else if (Char.IsLetterOrDigit(name[i]))
                {
                    sb.Append(name[i]);
                }
                else if (sb.Length > 0 && sb[sb.Length - 1] != '-')
                {
                    sb.Append('-');
                }
            }

            if (sb.Length > 0 && sb[sb.Length - 1] == '-')
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }
        
    }
}
