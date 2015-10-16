using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DESSample
{
    public class DesHelper
    {
        // 对称加密算法提供器
        private readonly ICryptoTransform _encryptor;     // 加密器对象
        private readonly ICryptoTransform _decryptor;     // 解密器对象
        private const int BufferSize = 1024;
        public DesHelper(string key)
        {
            //加密算法方式,对称加密还是非对称加密
            var provder = SymmetricAlgorithm.Create("TripleDES");
            //加密秘钥
            provder.Key = Encoding.UTF8.GetBytes(key);
            provder.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            _encryptor = provder.CreateEncryptor();
            _decryptor = provder.CreateDecryptor();
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public string Encrypt(string clearText)
        {
            var clearBuffer = Encoding.UTF8.GetBytes(clearText);
            var clearStream = new MemoryStream(clearBuffer);
            var encryStream = new MemoryStream();
            var cryStream = new CryptoStream(encryStream, _encryptor, CryptoStreamMode.Write);
            var reader = 0;
            var buffer = new byte[BufferSize];
            //从明文流中截取数据,将其写入算法中
            do
            {
                reader = clearStream.Read(buffer, 0, BufferSize);
                cryStream.Write(buffer, 0, reader);
            } while (reader > 0);
            //更新密文流
            cryStream.FlushFinalBlock();
            var msg = encryStream.ToArray();
            return Convert.ToBase64String(msg);
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        /// <returns></returns>
        public string Decrypt(string encryText)
        {
            var encryStream = new MemoryStream(Convert.FromBase64String(encryText));
            var cryStream = new CryptoStream(encryStream, _decryptor, CryptoStreamMode.Read);
            var decryStream = new MemoryStream();
            var buffer = new byte[BufferSize];
            var reader = 0;
            //不同流之间的转换
            do
            {
                reader = cryStream.Read(buffer, 0, BufferSize);
                decryStream.Write(buffer, 0, reader);
            } while (reader > 0);

            buffer = decryStream.GetBuffer();
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
    }
}
