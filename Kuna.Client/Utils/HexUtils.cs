using System;
using System.Text;

namespace Kuna.Client.Utils {
    public static class HexUtils {

        private const string HEX = "0123456789abcdef";

        public static string ToHexString(this byte[] data) {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var sb = new StringBuilder(data.Length * 2);
            foreach (var bt in data) {
                sb.Append(HEX[bt >> 4]);
                sb.Append(HEX[bt & 0x0f]);
            }
            return sb.ToString();
        }

        public static byte[] FromHexString(string value) {
            var len = value.Length;
            var res = new byte[(len + 1) >> 1];
            for (var i = 0; i < len - 1; i += 2) {
                var high = GetHexDigit(value[i]);
                var low = GetHexDigit(value[i + 1]);
                res[i / 2] = (byte)(high << 4 + low);
            }
            if ((len & 1) == 1) {
                res[res.Length - 1] = (byte)(GetHexDigit(value[value.Length - 1]) << 4);
            }
            return res;
        }

        private static byte GetHexDigit(char ch) {
            if (ch >= '0' && ch <= '9') {
                return (byte)(ch - '0');
            }
            if (ch >= 'a' && ch <= 'f') {
                return (byte)(ch - 'a' + 10);
            }
            if (ch >= 'A' && ch <= 'F') {
                return (byte)(ch - 'A' + 10);
            }
            throw new ArgumentException(nameof(ch));
        }

    }
}