namespace TaskAPI.DBAccess {
    public class DBConfig : IDBConfig {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }

        public DBConfig() {
            ConnectionString = "mongodb://localhost:27017";
            Database = "Taskboard";
            Collection = "Assignments";
        }

    }
}
