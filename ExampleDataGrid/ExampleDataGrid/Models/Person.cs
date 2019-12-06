using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleDataGrid.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
    }
}
