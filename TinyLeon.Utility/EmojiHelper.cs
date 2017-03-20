using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class EmojiHelper
    {
        private static char ec = (char)int.Parse("d83c", NumberStyles.HexNumber);
        private static char ed = (char)int.Parse("d83d", NumberStyles.HexNumber);
        private static char ee = (char)int.Parse("d83e", NumberStyles.HexNumber);

        private static string ERegex = "\\[e:([A-F0-9]{4})]";
        private static string ECRegex = "\\[ec:([A-F0-9]{4})]";
        private static string EDRegex = "\\[ed:([A-F0-9]{4})]";
        private static string EERegex = "\\[ee:([A-F0-9]{4})]";
        /// <summary>
        /// 将含有emoji表情的字符串处理成[e:2600]
        /// [\ud83c\udc00-\ud83c\udfff]|[\ud83d\udc00-\ud83d\udfff]|[\ud83e\udc00-\ud83e\udfff]|[\u2600-\u27ff]
        /// u2600 => e
        /// ud83c => ec
        /// ud83d => ed
        /// ud83e => ee
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncodeEmoji(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }
            string encodeEmojiStr = string.Empty;
            for (int index = 0; index < s.Length; index++)
            {
                if (Regex.IsMatch(s[index].ToString(), "\ud83c", RegexOptions.IgnoreCase))
                {
                    index++;
                    encodeEmojiStr += "[ec:" + ((int)s[index]).ToString("X") + "]";
                    continue;
                }
                if (Regex.IsMatch(s[index].ToString(), "\ud83d", RegexOptions.IgnoreCase))
                {
                    index++;
                    encodeEmojiStr += "[ed:" + ((int)s[index]).ToString("X") + "]";
                    continue;
                }
                if (Regex.IsMatch(s[index].ToString(), "\ud83e", RegexOptions.IgnoreCase))
                {
                    index++;
                    encodeEmojiStr += "[ee:" + ((int)s[index]).ToString("X") + "]";
                    continue;
                }
                bool isEmoji = Regex.IsMatch(s[index].ToString(), "[\u2600-\u27ff]", RegexOptions.IgnoreCase);
                if (isEmoji)
                {
                    encodeEmojiStr += "[e:" + ((int)s[index]).ToString("X") + "]";
                }
                else
                {
                    encodeEmojiStr += s[index];
                }
            }
            return encodeEmojiStr;
        }


        /// <summary>
        /// 将含有emoji表情的[e:2600]进行解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecodeEmoji(string s)
        {
            string result = s;
            if (string.IsNullOrWhiteSpace(result))
            {
                return result;
            }

            MatchCollection coll = Regex.Matches(s, "(\\[e:[A-F0-9]{4}]|\\[ed:[A-F0-9]{4}]|\\[ec:[A-F0-9]{4}]|\\[ee:[A-F0-9]{4}])");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (Match match in coll)
            {
                if (!dic.ContainsKey(match.ToString()))
                {
                    if (Regex.IsMatch(match.ToString(), ERegex))
                    {
                        dic.Add(match.ToString(), ((char)int.Parse(Regex.Replace(match.ToString(), ERegex, "$1"), NumberStyles.HexNumber)).ToString());
                    }
                    else if (Regex.IsMatch(match.ToString(), ECRegex))
                    {
                        dic.Add(match.ToString(), ec.ToString() + (char)int.Parse(Regex.Replace(match.ToString(), ECRegex, "$1"), NumberStyles.HexNumber));
                    }
                    else if (Regex.IsMatch(match.ToString(), EDRegex))
                    {
                        dic.Add(match.ToString(), ed.ToString() + (char)int.Parse(Regex.Replace(match.ToString(), EDRegex, "$1"), NumberStyles.HexNumber));
                    }
                    else if (Regex.IsMatch(match.ToString(), EERegex))
                    {
                        dic.Add(match.ToString(), ee.ToString() + (char)int.Parse(Regex.Replace(match.ToString(), EERegex, "$1"), NumberStyles.HexNumber));
                    }
                }
            }

            return dic.Aggregate(result, (current, keyValuePair) => current.Replace(keyValuePair.Key, keyValuePair.Value));
        }
    }
}
