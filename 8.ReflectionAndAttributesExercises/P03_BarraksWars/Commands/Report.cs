using _03BarracksFactory.Contracts;

namespace P03_BarraksWars.Commands
{
    public class Report : Command
    {
        public Report(string[] data, IRepository repository, IUnitFactory unitFactory) 
            : base(data, repository, unitFactory)
        {
        }

        public override string Execute()
        {
            string output = this.Repository.Statistics;

            return output;
        }
    }
}
