using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;

namespace VueContactsAPI.Infrastructures
{
    public class ExportToExcel
    {
        public static bool Download<T>(string outputLocation, List<T> objectList)
        {
            using (var fs = new FileStream(outputLocation, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Laporan");
                IRow row = excelSheet.CreateRow(0);

                // Populate the header
                var col = 0;
                foreach (var propInfo in objectList[0].GetType().GetProperties())
                {
                    row.CreateCell(col).SetCellValue(propInfo.Name);
                    col++;
                }


                // Populate the data         
                for (int i = 0; i < objectList.Count; i++)
                {
                    row = excelSheet.CreateRow(i + 1);

                    int j = 0;
                    foreach (var propInfo in objectList[i].GetType().GetProperties())
                    {
                        var value = propInfo.GetValue(objectList[i]);
                        var strValue = value?.ToString();
                        row.CreateCell(j).SetCellValue(strValue);
                        j++;
                    }
                }

                workbook.Write(fs);
            }

            return true;
        }
    }
}
