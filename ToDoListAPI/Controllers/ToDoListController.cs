using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoListAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoListController : ControllerBase
	{
		private readonly ToDoListService _toDoListService;

		public ToDoListController(ToDoListService toDoListService)
		{
			_toDoListService = toDoListService;
		}

		[HttpGet]
		public ActionResult<List<ToDoListModel>> Get() =>
			_toDoListService.Get();

		[HttpGet("{id}", Name = "GetList")]
		public ActionResult<ToDoListModel> Get(string id)
		{
			var list = _toDoListService.Get(id);

			if (list == null)
			{
				return NotFound();
			}

			return list;
		}

		[HttpPost]
		public ActionResult<ToDoListModel> Create(ToDoListModel list)
		{
			_toDoListService.Create(list);

			return CreatedAtRoute("GetList", new { id = list.Id.ToString() }, list);
		}

		[HttpPut("{id}")]
		public IActionResult Update(string id, ToDoListModel listIn)
		{
			var list = _toDoListService.Get(id);

			if (list == null)
			{
				return NotFound();
			}

			_toDoListService.Update(id, listIn);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			var list = _toDoListService.Get(id);

			if (list == null)
			{
				return NotFound();
			}

			_toDoListService.Remove(list.Id);

			return NoContent();
		}
    }
}
