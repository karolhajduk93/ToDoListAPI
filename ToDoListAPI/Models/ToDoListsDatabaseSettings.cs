using ToDoListAPI.Interfaces;

namespace ToDoListAPI.Models
{
	class ToDoListsDatabaseSettings : IToDoListsDatabaseSettings
	{
		public string ToDoListsCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}