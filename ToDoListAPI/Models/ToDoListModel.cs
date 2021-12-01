using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoListAPI.Models
{
	public class ToDoListModel
	{
		[BsonId]
		public string Id { get; set; }

		public string ListName { get; set; }

		public string Date { get; set; }

		public List<ToDoItemModel> ToDoItems { get; set; }
	}
}
