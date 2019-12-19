using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace Test_Task_OZ
{
    public class CsvHelper
    {
        public static FileInfo FileSource = new FileInfo(@"..\\..\\NameList.csv");

        public static DataTable GetDataTableFromCsv(FileInfo fullFileName)
        {
            string path = fullFileName.Directory.FullName;
            string fileName = fullFileName.Name;

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Text;HDR=YES\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable { Locale = CultureInfo.CurrentCulture };
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public static string GetCellValue(DataRow row, string columnName)
        {
            return row[columnName].ToString();
        }
    }
}
