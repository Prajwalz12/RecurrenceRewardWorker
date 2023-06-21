using Domain.Models;
using Domain.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecurrenceModel = Domain.Models.RecurrenceModel;

namespace Domain.Services
{
    public class RecurrenceRewardService : IRecurrenceRewardService
    {
        private readonly IMongoCollection<RecurrenceModel.RecurrenceReward> _mongoCollection;

        public RecurrenceRewardService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.RecurrenceRewardSetting.DatabaseName);
            _mongoCollection = database.GetCollection<RecurrenceModel.RecurrenceReward>(settings.RecurrenceRewardSetting.CollectionName);
        }

        public List<RecurrenceModel.RecurrenceReward> Get(FilterDefinition<RecurrenceModel.RecurrenceReward> filterDefinition) => _mongoCollection.Find(filterDefinition).ToList();

        public UpdateResult Update(FilterDefinition<RecurrenceModel.RecurrenceReward> filterDefinition, UpdateDefinition<RecurrenceModel.RecurrenceReward> updateDefinition) => _mongoCollection.UpdateOne(filterDefinition, updateDefinition);
    }
}
