using Excel.BLL;

namespace ReflectionExample
{
    class Program
    {
        public static int Ali { get; set; }
        static void Main()
        {
            EmployeeService employee = new();
           
            string Path = @"C:\Users\aliah\OneDrive\Desktop\New Microsoft Excel Worksheet.xlsx";
           employee.UploadEmployees(Path);
            
            Console.ReadLine();
        }
    }
}
