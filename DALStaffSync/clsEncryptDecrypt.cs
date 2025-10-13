using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEncryptDecrypt
    {
        dbStaffSync.clsEncryptDecrypt objEncryptDecrypt = new dbStaffSync.clsEncryptDecrypt();

        public clsEncryptDecrypt() { 

        }

        public string encryptText(string normalText)
        {
            string sbBytes = "";

            sbBytes = objEncryptDecrypt.encryptText(normalText);

            return sbBytes.ToString();
        }

        public string decryptText(string encryptedText)
        {
            int CharsLength = encryptedText.Length;
            string bytesarray = "";

            bytesarray = objEncryptDecrypt.decryptText(encryptedText);

            return bytesarray;
        }
    }
}
