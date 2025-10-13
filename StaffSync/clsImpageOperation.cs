using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsImpageOperation
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsImpageOperation() { 

        }

        // Convert an image into a byte array.
        public byte[] ImageToBytes(System.Drawing.Image image, ImageFormat format, bool Overwrite)
        {
            using (MemoryStream image_stream = new MemoryStream())
            {
                if (Overwrite == false && image_stream.Length == 0)
                    return image_stream.ToArray();

                image.Save(image_stream, format);
                return image_stream.ToArray();
            }
        }

        // Convert a byte array into an image.
        public Bitmap BytesToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (MemoryStream image_stream = new MemoryStream(bytes))
            {
                Bitmap bm = new Bitmap(image_stream);
                image_stream.Close();
                return bm;
            }
        }


    }
}
