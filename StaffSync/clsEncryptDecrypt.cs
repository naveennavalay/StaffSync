using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsEncryptDecrypt
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsEncryptDecrypt() { 

        }

        public string encryptText(string normalText)
        {
            Byte[] stringBytes = System.Text.Encoding.Unicode.GetBytes(normalText);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
        public string decryptText(string encryptedText)
        {
            int CharsLength = encryptedText.Length;
            byte[] bytesarray = new byte[CharsLength / 2];
            for (int i = 0; i < CharsLength; i += 2)
            {
                bytesarray[i / 2] = Convert.ToByte(encryptedText.Substring(i, 2), 16);
            }
            return System.Text.Encoding.Unicode.GetString(bytesarray);
        }
    }
}
