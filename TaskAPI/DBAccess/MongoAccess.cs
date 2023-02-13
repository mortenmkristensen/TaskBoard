using MongoDB.Bson;
using MongoDB.Driver;
using TaskAPI.Models;

namespace TaskAPI.DBAccess {
    public class MongoAccess : IDBAccess {

        MongoClient Client;
        IMongoDatabase Database { get; set; }
        IMongoCollection<Assignment> Collection { get; set; }
        IDBConfig Config { get; set; }
        public MongoAccess(IDBConfig config) {
            Client = new MongoClient();
            Config = config;
            Database = Client.GetDatabase(Config.Database);
            Collection = Database.GetCollection<Assignment>(Config.Collection);
        }
        public bool Delete(string id) {
            var result = false;
            try {
                if (id != null) {
                    var objectId = new ObjectId(id);
                    var filter = Builders<Assignment>.Filter.Eq("_id", objectId);
                    result = Collection.DeleteOne(filter).IsAcknowledged;
                }
            }
            catch (MongoException me) {
                throw new Exception("Something went wrong when trying to delte a script", me);
            }
            return result;
        }

        public IEnumerable<Assignment> Get() {
            Task<List<Assignment>> assignments = null;
            try {
                assignments = Collection.Find<Assignment>(f => true).ToListAsync();
            }
            catch (MongoException me) {
                throw new Exception("Something went wrong when trying to get the assignmets", me);
            }
            return assignments.Result;
        }
        public Assignment Get(string id) {
            Task<Assignment> assignment = null;
            try {
                if (id != null) {
                    var objectId = new ObjectId(id);
                    var filter = Builders<Assignment>.Filter.Eq("_id", objectId);
                    assignment = Collection.Find(filter).FirstOrDefaultAsync();
                }
            }
            catch (MongoException me) {
                throw new Exception("Something went wrong when trying to get a script", me);
            }
            return assignment.Result;
        }

        public Assignment Upsert(Assignment assignment) {
            try {
                //If a new assignment is created that has no Id, it is inserted (and MongoDB generates a new Id). 
                if (string.IsNullOrEmpty(assignment.Id)) {
                    Collection.InsertOne(assignment);
                    //If the assignment already has an Id, the assignment is replaced. 
                } else {
                    var objectId = new ObjectId(assignment.Id);
                    var filter = Builders<Assignment>.Filter.Eq("_id", objectId);
                    Collection.ReplaceOne(filter, assignment, new ReplaceOptions { IsUpsert = true });
                }
            }
            catch (MongoException me) {
                throw new Exception("Something went wrong when trying to insert an assignment", me);
            }
            return assignment;
        }
    }
}
