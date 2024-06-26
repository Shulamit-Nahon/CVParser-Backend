using CVParserAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CVParserAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<ApplicantData> _applicantsCollection;

        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _applicantsCollection = mongoDatabase.GetCollection<ApplicantData>("Applicants");
        }

        public async Task<List<ApplicantData>> GetAsync() =>
            await _applicantsCollection.Find(_ => true).ToListAsync();

        public async Task<ApplicantData?> GetAsync(string id) =>
            await _applicantsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ApplicantData newApplicant) =>
            await _applicantsCollection.InsertOneAsync(newApplicant);

        public async Task UpdateAsync(string id, ApplicantData updatedApplicant) =>
            await _applicantsCollection.ReplaceOneAsync(x => x.Id == id, updatedApplicant);

        public async Task RemoveAsync(string name) =>
            await _applicantsCollection.DeleteOneAsync(x => x.Name == name);
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}