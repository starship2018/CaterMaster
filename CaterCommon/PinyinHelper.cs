using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;

namespace CaterCommon
{
    public static class PinyinHelper
    {
        public static string GetPinyin(string str)
        {
            string Pinyin="";
            foreach (char c in str)
            {
                try
                {
                    ChineseChar cc = new ChineseChar(c);
                    Pinyin += cc.Pinyins[0][0];
                }
                catch (Exception e)
                {
                    continue;
                }
                
            }
            return Pinyin;
        }
    }
}
