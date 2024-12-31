using System.Security.Cryptography;
using System.Text;

namespace MyToDo.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// this 的作用：它告诉编译器这个方法是一个扩展方法，并且可以被任何该类型的实例调用，这意味着任何 string 类型的实例都可以调用这个方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            // 创建一个 MD5 对象
            using (MD5 md5 = MD5.Create())
            {
                // 将输入字符串转换为字节数组
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);

                // 计算字节数组的哈希值
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 将字节数组转换为十六进制字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2")); // X2 格式化为两位十六进制数
                }

                // 返回十六进制字符串
                return sb.ToString();
            }
        }
    }
}
