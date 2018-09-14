using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaterCommon
{
    public static  class MD5Helper
    {
        public static string GetMD5String(string str)
        {
            //创建MD5对象
            MD5 md5=MD5.Create();
            //将待转字符串变成字节数组
            byte[] oldbyte = Encoding.UTF8.GetBytes(str);
            //将原始字节数组计算为新的加密字节数组
            byte[] newbyte = md5.ComputeHash(oldbyte);
            //创建字符串连接器
            StringBuilder sb = new StringBuilder();
            //将每个机密后的字节还原成字符，并串联在连接器上
            foreach (byte item in newbyte)
            {
                sb.Append(item.ToString("x2"));
            }
            //将整个连接器转换成加密字符串
            return sb.ToString();
        }
    }
}
