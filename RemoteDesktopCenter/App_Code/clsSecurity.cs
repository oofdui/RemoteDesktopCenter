using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;
using System.IO;
using System.Data;

public class clsSecurity
{
    #region Encrypt Decrypt Code
    //private static readonly byte[] _key = { 0xA1, 0xF1, 0xA6, 0xBB, 0xA2, 0x5A, 0x37, 0x6F, 0x81, 0x2E, 0x17, 0x41, 0x72, 0x2C, 0x43, 0x27 };
    //private static readonly byte[] _initVector = { 0xE1, 0xF1, 0xA6, 0xBB, 0xA9, 0x5B, 0x31, 0x2F, 0x81, 0x2E, 0x17, 0x4C, 0xA2, 0x81, 0x53, 0x61 };

    private static readonly byte[] _key = { 0xA2, 0xF2, 0xA7, 0xBB, 0xA3, 0x6A, 0x38, 0x7F, 0x82, 0x3E, 0x18, 0x42, 0x73, 0x3C, 0x44, 0x28 };
    private static readonly byte[] _initVector = { 0xE2, 0xF2, 0xA7, 0xBB, 0xA0, 0x6B, 0x32, 0x3F, 0x82, 0x3E, 0x18, 0x5C, 0xA3, 0x82, 0x54, 0x62 };
    #endregion
    /// <summary>
    /// ถอดรหัสข้อมูล
    /// </summary>
    /// <param name="Value">ข้อมูลที่ต้องการให้ถอดรหัส</param>
    /// <returns>ข้อมูลหลังจากถอดรหัส</returns>
    /// <example>
    /// clsSecurity.Decrypt("e0NDKIlUhHF3qcIdkmGpZw==");
    /// </example>
    public string Decrypt(string Value)
    {
        #region Variable
        SymmetricAlgorithm mCSP;
        ICryptoTransform ct = null;
        MemoryStream ms = null;
        CryptoStream cs = null;
        byte[] byt;
        byte[] result;
        #endregion
        #region Procedure
        mCSP = new RijndaelManaged();

        try
        {
            mCSP.Key = _key;
            mCSP.IV = _initVector;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);


            byt = Convert.FromBase64String(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();
            result = ms.ToArray();
        }
        catch
        {
            result = null;
        }
        finally
        {
            if (ct != null)
                ct.Dispose();
            if (ms != null)
                if (ms.CanRead)
                {
                    ms.Dispose();
                }
            if (cs != null)
                if (cs.CanRead)
                {
                    cs.Dispose();
                }
        }
        try
        {
            return ASCIIEncoding.UTF8.GetString(result);
        }
        catch (Exception)
        {
            return "";
        }
        #endregion
    }
    /// <summary>
    /// เข้ารหัสข้อมูล
    /// </summary>
    /// <param name="Password">ข้อมูลที่ต้องการให้เข้ารหัส</param>
    /// <returns>ข้อมูลหลังจากเข้ารหัส</returns>
    /// <example>
    /// clsSecurity.Encrypt("offjunior");
    /// </example>
    public string Encrypt(string Password)
    {
        if (string.IsNullOrEmpty(Password))
            return string.Empty;

        #region Variable
        byte[] Value = Encoding.UTF8.GetBytes(Password);
        SymmetricAlgorithm mCSP = new RijndaelManaged();
        #endregion
        #region Procedure
        mCSP.Key = _key;
        mCSP.IV = _initVector;
        using (ICryptoTransform ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    cs.Write(Value, 0, Value.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                    try
                    {
                        return Convert.ToBase64String(ms.ToArray());
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
        }
        #endregion
    }
}