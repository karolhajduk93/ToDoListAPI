using System.Collections.Generic;
using MongoDB.Driver;
using ToDoListAPI.Interfaces;
using ToDoListAPI.Models;

namespace ToDoListAPI.Services
{
	public class ToDoListService
	{
		private readonly IMongoCollection<ToDoListModel> _todosLists;

		public ToDoListService(IToDoListsDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_todosLists = database.GetCollection<ToDoListModel>(settings.ToDoListsCollectionName);
		}

		public List<ToDoListModel> Get() =>
			_todosLists.Find(list => true).ToList();

		public ToDoListModel Get(string id) =>
			_todosLists.Find<ToDoListModel>(list => list.Id == id).FirstOrDefault();

		public ToDoListModel Create(ToDoListModel list)
		{
			_todosLists.InsertOne(list);
			return list;
		}

		public void Update(string id, ToDoListModel ToDoListIn) =>
			_todosLists.ReplaceOne(list => list.Id == id, ToDoListIn);

		public void Remove(ToDoListModel listIn) =>
			_todosLists.DeleteOne(list => list.Id == listIn.Id);

		public void Remove(string id) =>
			_todosLists.DeleteOne(list => list.Id == id);
	}
}