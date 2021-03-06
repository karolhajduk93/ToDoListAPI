using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoListAPI.Models
{
	[BsonIgnoreExtraElements]
	public class ToDoItemModel
	{
		public string Text { get; set; }
		public string UserId { get; set; }
		public string ToDoItemId { get; set; }
		public string Date { get; set; }
		public bool Checked { get; set; }
	}
}