using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKKInfo
{
    public class Helpers
    {
        public static bool CheckCadastralNumber(string cn)
        {
            foreach (char c in cn)
            {
                if (!Char.IsDigit(c) && c != ':')
                    return false;
            }

            string[] parts = cn.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 4)
                return false;

            return true;
        }

        public static string CompressCadastralNumber(string cn)
        {
            if (Helpers.CheckCadastralNumber(cn))
            {
                string[] parts = cn.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                int district;
                int region;
                int quarter;
                int number;

                try
                {
                    district = Int32.Parse(parts[0]);
                    region = Int32.Parse(parts[1]);
                    quarter = Int32.Parse(parts[2]);
                    number = Int32.Parse(parts[3]);
                }
                catch (FormatException)
                {
                    return String.Empty;
                }

                return String.Format($"{district}:{region}:{quarter}:{number}");

            }

            return String.Empty;
        }

        public static string HardClearCadastralNumber(string cn)
        {
            string result = String.Empty;

            foreach (char c in cn)
            {
                if (Char.IsDigit(c) || c == ':')
                    result += c;
            }

            if (result.Length == 0)
                return result;

            int pos = -1;
            while ((pos = result.IndexOf("::")) != -1)
                result = result.Remove(pos, 1);

            return result;
        }
    }
}
