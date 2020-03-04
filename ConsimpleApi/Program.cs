using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsimpleApi
{
    class Program
    {
        const string _uri = "https://tester.consimple.pro/";

        static void Main(string[] args)
        {
            var run = true;
            while(run)
            {
                Console.WriteLine("Введите номер команды, чтобы продолжить...");
                Console.WriteLine("1 - Отправить запрос API\n0 - Выйти");

                try
                {
                    Console.Write("Номер команды: ");
                    var command = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch(command)
                    {
                        case 0:
                            run = false;
                            break;
                        case 1:
                            SentRequest();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Данный функционал еще не реализован.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                    }
                }
                catch(FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Скорее всего Вы ввели не целое число. Попробуйте еще раз...");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Хм.. Что-то Вы не то нажали... :)");
                }
            }
        }

        private static void SentRequest()
        {
            var data = ApiHelper.GetData(_uri);

            var result = data.Products.Select(x => new
            {
                ProductName = x.Name,
                CategoryName = data.Categories.Where(y => y.Id == x.CategoryId)?.First()?.Name
            }).OrderBy(x => x.CategoryName).ToArray();

            var productNameColumn = "Product name";
            var categoryNameColumn = "Category name";
            var padding = 20;
            var str = "".PadRight(productNameColumn.Length + (padding - productNameColumn.Length) + categoryNameColumn.Length + 2, '-');

            Console.WriteLine(str);
            Console.WriteLine(productNameColumn.PadRight(padding) + "| " + categoryNameColumn);
            Console.WriteLine(str);
            foreach (var item in result)
            {
                Console.WriteLine(item.ProductName.PadRight(padding) + "| " + item.CategoryName);
            }
            Console.WriteLine(str);
        }
    }
}
