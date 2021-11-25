namespace ToDoListAPI.Interfaces
{
	public interface IToDoListsDatabaseSettings
	{
		public string ToDoListsCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}