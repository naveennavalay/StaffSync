using Krypton.Toolkit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public static class Download
    {
        //myDBClass objDBClass = new myDBClass();
        //OleDbConnection conn = null;
        //DataSet dtDataset;
        //clsStates objState = new clsStates();
        //clsCountries objCountry = new clsCountries();

        //public Download() { 

        //}

        public static void DownloadExcel(ListView lstListBox)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
            int i = 1;
            int i2 = 1;
            foreach (ListViewItem lvi in lstListBox.Items)
            {
                i = 1;
                foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                {
                    ws.Cells[i2, i] = lvs.Text;
                    i++;
                }
                i2++;
            }
        }

        public static bool DownloadExcel(string file, KryptonDataGridView dtSpecificDataGrid)
        {
            bool fileGenerated = false;
            using (StreamWriter csv = new StreamWriter(file, false))
            {
                int totalcolms = dtSpecificDataGrid.ColumnCount;

                foreach (DataGridViewColumn colm in dtSpecificDataGrid.Columns)
                {
                    if(!colm.Visible == false)
                        csv.Write(colm.HeaderText + ',');
                }

                csv.Write('\n');
                string data = "";
                foreach (DataGridViewRow row in dtSpecificDataGrid.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    data = "";
                    for (int i = 0; i < totalcolms; i++)
                    {
                        if (!dtSpecificDataGrid.Columns[i].Visible == false)
                            data += (row.Cells[i].Value ?? "").ToString() + ',';
                    }
                    if (data != string.Empty)
                    {
                        csv.WriteLine(data);
                        fileGenerated = true;
                    }
                }
            }
            return fileGenerated;
        }

        public static void OpenCSV(string strFilePath)
        {
            if (!File.Exists(strFilePath))
                return;

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = strFilePath,
                    UseShellExecute = true
                }
            };

            proc.Start();

            Thread.Sleep(1000);

            proc.WaitForExit();

            try
            {
                File.Delete(strFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting file: {ex.Message}");
            }
        }

        public static void DownloadPDF(string strFilePath)
        {
            if (!File.Exists(strFilePath))
                return;

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = strFilePath,
                    UseShellExecute = true
                }
            };

            proc.Start();

            Thread.Sleep(1000);

            proc.WaitForExit();

            try
            {
                File.Delete(strFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting file: {ex.Message}");
            }
        }
    }
}
