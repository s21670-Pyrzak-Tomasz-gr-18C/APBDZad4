using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class CSVService
    {
        public static string pathToFile = "./Data/students.csv";

        public static void ReadCSV(List<Student> list)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
            foreach (string line in lines)
            {
                var values = line.Split(',');
                list.Add(new Student()
                {
                    FirstName = values[0],
                    LastName = values[1],
                    indexNumber = values[2],
                    Birthdate = values[3],
                    Studies = values[4],
                    Mode = values[5],
                    Email = values[6],
                    FathersName = values[7],
                    MothersName = values[8]
                });
            }
        }

        public static void findStudent(List<Student> list, string indexNumber) 
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
            foreach (string line in lines)
            {
                var values = line.Split(',');
                if(values[2].Equals(indexNumber))
                {
                    list.Add(new Student()
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        indexNumber = values[2],
                        Birthdate = values[3],
                        Studies = values[4],
                        Mode = values[5],
                        Email = values[6],
                        FathersName = values[7],
                        MothersName = values[8]
                    });
                }
            }
        }

        public static List<Student> updateStudent(Student student, string indexNumber)
        {
            List<Student> students = new List<Student>();
            var lines = System.IO.File.ReadAllLines(pathToFile);
            foreach (string line in lines)
            {
                var values = line.Split(',');
                if (!values[2].Equals(indexNumber))
                {
                    students.Add(new Student()
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        indexNumber = values[2],
                        Birthdate = values[3],
                        Studies = values[4],
                        Mode = values[5],
                        Email = values[6],
                        FathersName = values[7],
                        MothersName = values[8]
                    });
                }
                else
                {
                    students.Add(student);
                }
            }
            return students;
        }

        public static void SaveToCSV(List<Student> saveData, bool overwrite)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(Student)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            //lines.Add(header);
            var valueLines = saveData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            if (overwrite == true)
            {
                File.WriteAllLines(pathToFile, lines.ToArray());
            }
            else
            {
                File.AppendAllLines(pathToFile, lines.ToArray());
            }
        }

        public static List<Student> deleteStudent(string indexNumber)
        {
            List<Student> students = new List<Student>();
            var lines = System.IO.File.ReadAllLines(pathToFile);
            foreach (string line in lines)
            {
                var values = line.Split(',');
                if (!values[2].Equals(indexNumber))
                {
                    students.Add(new Student()
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        indexNumber = values[2],
                        Birthdate = values[3],
                        Studies = values[4],
                        Mode = values[5],
                        Email = values[6],
                        FathersName = values[7],
                        MothersName = values[8]
                    });
                }
            }
            return students;
        }

    }
}
