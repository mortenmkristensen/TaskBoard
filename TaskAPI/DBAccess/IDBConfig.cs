namespace TaskAPI.DBAccess {
    public interface IDBConfig {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }
}