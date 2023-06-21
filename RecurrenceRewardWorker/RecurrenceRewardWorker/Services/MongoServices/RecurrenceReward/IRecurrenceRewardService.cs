using MongoDB.Driver;
using RecurrenceModel = Domain.Models.RecurrenceModel;

namespace Domain.Services;

public interface IRecurrenceRewardService
{
    List<RecurrenceModel.RecurrenceReward> Get(FilterDefinition<RecurrenceModel.RecurrenceReward> filterDefinition);
    UpdateResult Update(FilterDefinition<RecurrenceModel.RecurrenceReward> filterDefinition, UpdateDefinition<RecurrenceModel.RecurrenceReward> updateDefinition);
}
