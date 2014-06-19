using System;
using System.Collections.Generic;
using System.Text;

using OpenSSL.Core;
using OpenSSL.Crypto;

namespace BDJX.BSCP.Common
{
    /// <summary>
    /// Openssl加密解密类--封装了Openssl.Net
    /// </summary>
    public class OpensslHelper
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>
        /// 初始化向量
        /// </summary>
        public byte[] IV { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public byte[] Password;

        /// <summary>
        /// 编码名称
        /// </summary>
        string EncodingName { get; set; }

        /// <summary>
        /// 加密解密上下文
        /// </summary>
        CipherContext cipherContext;

        /// <summary>
        /// 构造函数，初始化加密解密算法，生成密钥和初始化向量
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encodingname"></param>
        public OpensslHelper(string password, string encodingname)
        {
            this.EncodingName = encodingname;
            this.Password = Encoding.GetEncoding(EncodingName).GetBytes(password);
            //加密，解密算法：3DES
            cipherContext = new CipherContext(Cipher.DES_EDE3_CBC);
            GenerateKeyIV();
        }

        /// <summary>
        /// 生成密钥和初始化向量
        /// </summary>
        public void GenerateKeyIV()
        {
            byte[] Iv;
            //密钥（8位）生成算法：密码+MD5摘要算法
            Key = cipherContext.BytesToKey(MessageDigest.MD5, null, this.Password, 8, out Iv);
            this.IV = Iv;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="msg">明文</param>
        /// <returns>密文</returns>
        public byte[] Encrypt(byte[] msg)
        {
            return cipherContext.Encrypt(msg, this.Key, this.IV);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="msg">密文</param>
        /// <returns>明文</returns>
        public byte[] Decrypt(byte[] msg)
        {
            return cipherContext.Decrypt(msg, this.Key, this.IV);
        }
    }
}
