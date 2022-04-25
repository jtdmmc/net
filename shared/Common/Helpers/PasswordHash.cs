using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Common.Helpers
{
    public sealed class PasswordHash
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 1000;
        readonly byte[] _salt, _hash;

        /// <summary>
        /// 这是一个轻量版的密码哈希辅助类。
        /// 另外一个完整的密码辅助库是：https://github.com/adamcaudill/libsodium-net
        /// <see cref="http://csharptest.net/470/another-example-of-how-to-store-a-salted-password-hash/"/>
        /// </summary>
        /// <param name="password"></param>
        /// <example>
        /// // Store a password hash:
        /// PasswordHash hash = new PasswordHash("password");
        /// byte[] hashBytes = hash.ToArray();
        /// // Check password against a stored hash
        /// byte[] hashBytes = //read from store.
        /// PasswordHash hash = new PasswordHash(hashBytes);
        /// if(!hash.Verify("newly entered password"))
        /// throw new System.UnauthorizedAccessException();
        /// </example>
        public PasswordHash(string password)
        {
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            _hash = new System.Security.Cryptography.Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        }

        public PasswordHash(byte[] hashBytes)
        {
            Array.Copy(hashBytes, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, _hash = new byte[HashSize], 0, HashSize);
        }

        public PasswordHash(byte[] salt, byte[] hash)
        {
            Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
        }

        public byte[] ToArray()
        {
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }

        public byte[] Salt { get { return (byte[])_salt.Clone(); } }

        public byte[] Hash { get { return (byte[])_hash.Clone(); } }

        public bool Verify(string password)
        {
            byte[] test = new System.Security.Cryptography.Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
                if (test[i] != _hash[i])
                    return false;
            return true;
        }
    }
}
