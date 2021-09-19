﻿using ExpensesAPI.Data;
using ExpensesAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ExpensesAPI.Controllers
{
	[EnableCors("http://localhost:4200", "*", "*")]
    public class EntriesController : ApiController
    {
        public IHttpActionResult GetEntries()
		{
			try
			{
				using (var context = new AppDbContext())
				{
					var entries = context.Entries.ToList();
					return Ok(entries);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IHttpActionResult> PostEntry([FromBody]Entry entry)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			try
			{
				using (var context = new AppDbContext())
				{
					context.Entries.Add(entry);
					await context.SaveChangesAsync();

					return Ok("Entry was created!");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
    }
}
