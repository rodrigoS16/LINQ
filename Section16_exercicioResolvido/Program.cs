using Section16_exercicioResolvido.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Section16_exercicioResolvido
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite o caminho do arquivo: ");
            string path = Console.ReadLine();

            ExercicioFixacao(path); 
        }

        static void ExercicioResolvido(string path)
        {            
            using StreamReader sr = File.OpenText(path);

            List<Product> products = new List<Product>();

            while (!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split(',');

                products.Add(new Product(data[0], double.Parse(data[1], CultureInfo.InvariantCulture)));
            }

            double average = products.Select(p => p.Price).DefaultIfEmpty().Average();
            Console.Write("Average price: " + average.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine();

            var names =
                from p in products
                where p.Price < average
                orderby p.Name descending
                select p.Name;

            foreach (String obj in names)
            {
                Console.WriteLine(obj);
            }
        }

        static void ExercicioFixacao(string path)
        {
            using StreamReader sr = File.OpenText(path);
            List<Employee> employees = new List<Employee>();

            while (!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split(',');
                employees.Add(new Employee(data[0], data[1], double.Parse(data[2], CultureInfo.InvariantCulture)));               
            }

            Console.Write("Digite o valor de filtro do salario: ");
            Console.WriteLine();
            double filter = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            var emails = employees.Where(e => e.Salary > filter).DefaultIfEmpty().OrderBy(e => e.Email).Select(e => e.Email);
            foreach (string obj in emails)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine();
            Console.WriteLine("A soma dos salarios começados por 'M' é: " + employees.Where(e => e.Name[0] == 'M').DefaultIfEmpty().Sum(e => e.Salary));
        }
    }
}
