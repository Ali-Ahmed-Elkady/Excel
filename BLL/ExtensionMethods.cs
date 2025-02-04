using DAL.Repo;
using OfficeOpenXml;

namespace Excel.BLL
{
    public static class ExtensionMethods
    {
        public static void Upload<T>(this string filePath, Irepo<T> repo) where T : class, new()
        {
            List<T> entities = new List<T>();
            var properties = typeof(T).GetProperties();
            List<string> propertyNames = new List<string>();

            foreach (var item in properties)
                propertyNames.Add(item.Name);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    if (worksheet.Dimension == null)
                    {
                        Console.WriteLine("The worksheet is empty.");
                        return;
                    }

                    List<string> columnNames = new List<string>();

                    for (int i = worksheet.Dimension.Start.Column; i <= worksheet.Dimension.End.Column; i++)
                    {
                        columnNames.Add(worksheet.Cells[1, i].Text);
                    }

                    int mismatchCount = 0;
                    foreach (string columnName in columnNames)
                    {
                        if (!propertyNames.Contains(columnName))
                        {
                            Console.WriteLine($"Column '{columnName}' does not match any property in the class.");
                            mismatchCount++;
                        }
                    }

                    if (mismatchCount > 0)
                    {
                        Console.WriteLine("Data validation failed. Please check the Excel file columns.");
                        return;
                    }

                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        T entity = new();
                        for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                        {
                            string columnName = worksheet.Cells[1, col].Text;
                            var property = properties[propertyNames.IndexOf(columnName)];
                            string cellValue = worksheet.Cells[row, col].Text;

                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                var value = Convert.ChangeType(cellValue, property.PropertyType);
                                property.SetValue(entity, value);
                            }
                        }
                        entities.Add(entity);
                    }
                }

                
                repo.AddRange(entities);
                Console.WriteLine("Data has been successfully inserted into the database.");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
