using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleDataGrid.Data;
using ExampleDataGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleDataGrid.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //List<Person> persons = new List<Person>();

            //for (int i = 0; i < 100; i++)
            //{
            //    persons.Add(new Person { Id = Guid.NewGuid(), Name = $"Person {i}", LastName = $"LastName {i}", Age = new Random().Next(18, 80), Salary = new Random().Next(300, 15000) });
            //}

            //_context.Persons.AddRange(persons);
            //_context.SaveChanges();


            return View(_context.Persons.ToList());
        }

        public IActionResult GetData(string name, string lastName, int? age, decimal? salary)
        {

            IQueryable<Person> dataToFilter = null;
            List<Person> result = new List<Person>();


            if (!string.IsNullOrEmpty(name))
            {
                dataToFilter = _context.Persons.Where(x => EF.Functions.Like(x.Name, $"%{name}%"));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                if (dataToFilter != null)
                {
                    dataToFilter = dataToFilter.Where(x => EF.Functions.Like(x.LastName, $"%{lastName}%"));
                }
                else
                {
                    dataToFilter = _context.Persons.Where(x => EF.Functions.Like(x.LastName, $"%{lastName}%"));
                }
            }

            if (age != null)
            {
                if (dataToFilter != null)
                {
                    dataToFilter = dataToFilter.Where(x => x.Age == age);
                }
                else
                {
                    dataToFilter = _context.Persons.Where(x => x.Age == age);
                }
            }

            if (salary != null)
            {
                if (dataToFilter != null)
                {
                    dataToFilter = dataToFilter.Where(x => x.Salary == salary);
                }
                else
                {
                    dataToFilter = _context.Persons.Where(x => x.Salary == salary);
                }
            }

            if (dataToFilter == null)
            {
                result = _context.Persons.ToList();
            }
            else
            {
                result = dataToFilter.AsNoTracking().ToList();
            }

            return Ok(result);
        }
    }
}