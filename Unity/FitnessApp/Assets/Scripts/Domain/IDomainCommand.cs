using FitnessAppAPI;

namespace FitnessApp.Domain
{
    public interface IDomainCommand
    {
        void Execute(FitnessApiFacade api);
    }
}