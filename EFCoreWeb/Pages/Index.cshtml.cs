using EFCoreDataAccessLibrary.DataAccess;
using EFCoreDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

// Benefits of Entity Framework Core
// 1. Faster development speed
// 2. You don't have to know SQL

// Benefits of Dapper
// 1. Faster in production
// 2. Easier to work with for SQL developer
// 3. Designed for loose coupling

namespace EFCoreWeb.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly PeopleContext _db;

		public IndexModel(ILogger<IndexModel> logger, PeopleContext db)
		{
			_logger = logger;
			_db = db;
		}

		public void OnGet()
		{
			LoadSampleData();

			var people = _db.People
				.Include(a => a.Addresses)
				.Include(e => e.EmailAddresses)
				//.Where(x => ApprovedAge(x.Age))
				.Where(x => x.Age >= 18 && x.Age <= 65)
				.ToList();
		}

		private bool ApprovedAge(int age)
		{
			return (age >= 18 && age <= 65);
		}

		private void LoadSampleData()
		{
			if (_db.People.Count() == 0)
			{
				string file = System.IO.File.ReadAllText("generated.json");
				var people = JsonSerializer.Deserialize<List<Person>>(file);
				_db.AddRange(people);
				_db.SaveChanges();
			}
		}
	}
}
