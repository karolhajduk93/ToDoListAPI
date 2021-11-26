using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoListAPI.Models
{
	[BsonIgnoreExtraElements]
	public class ToDoListModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("Name")]
		public string ListName { get; set; }

		public string Date { get; set; }

		public List<ToDoItemModel> ToDoItems { get; set; }
	}
}
