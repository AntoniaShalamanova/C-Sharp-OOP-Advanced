namespace FestivalManager.Core.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Entities.Contracts;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Factories.Contracts;

    public class FestivalController : IFestivalController
    {
        private readonly IStage stage;
        private readonly ISetFactory setFactory;
        private readonly IInstrumentFactory instrumentFactory;

        public FestivalController(IStage stage, ISetFactory setFactory,
            IInstrumentFactory instrumentFactory)
        {
            this.stage = stage;
            this.setFactory = setFactory;
            this.instrumentFactory = instrumentFactory;
        }

        public string RegisterSet(string[] args)
        {
            string name = args[0];
            string type = args[1];

            ISet set = this.setFactory.CreateSet(name, type);

            this.stage.AddSet(set);

            return $"Registered {type} set";
        }

        public string SignUpPerformer(string[] args)
        {
            var name = args[0];
            var age = int.Parse(args[1]);

            var instruments = args.Skip(2).ToArray();

            IPerformer performer = new Performer(name, age);

            foreach (var instrumentType in instruments)
            {
                IInstrument instrument = this.instrumentFactory
                    .CreateInstrument(instrumentType);

                performer.AddInstrument(instrument);
            }

            this.stage.AddPerformer(performer);

            return $"Registered performer {performer.Name}";
        }

        public string RegisterSong(string[] args)
        {
            string songName = args[0];
            int[] songDurationTokens = args[1].Split(":")
                .Select(int.Parse)
                .ToArray();

            int minutes = songDurationTokens[0];
            int seconds = songDurationTokens[1];

            TimeSpan duration = new TimeSpan(0, minutes, seconds);

            ISong song = new Song(songName, duration);

            this.stage.AddSong(song);

            return $"Registered song {song}";
        }

        public string AddSongToSet(string[] args)
        {
            var songName = args[0];
            var setName = args[1];

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            if (!this.stage.HasSong(songName))
            {
                throw new InvalidOperationException("Invalid song provided");
            }

            var set = this.stage.GetSet(setName);
            var song = this.stage.GetSong(songName);

            set.AddSong(song);

            return $"Added {song} to {set.Name}";
        }

        public string AddPerformerToSet(string[] args)
        {
            var performerName = args[0];
            var setName = args[1];

            if (!this.stage.HasPerformer(performerName))
            {
                throw new InvalidOperationException("Invalid performer provided");
            }

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            var performer = this.stage.GetPerformer(performerName);
            var set = this.stage.GetSet(setName);

            set.AddPerformer(performer);

            return $"Added {performer.Name} to {set.Name}";
        }

        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }

        public string ProduceReport()
        {
            StringBuilder result = new StringBuilder();

            TimeSpan totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            result.AppendLine($"Festival length: {TimeSpanToString(totalFestivalLength)}");

            foreach (var set in this.stage.Sets)
            {
                result.AppendLine($"--{set.Name} ({TimeSpanToString(set.ActualDuration)}):");

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    result.AppendLine($"---{performer.Name} ({instruments})");
                }

                if (!set.Songs.Any())
                    result.AppendLine("--No songs played");
                else
                {
                    result.AppendLine("--Songs played:");
                    foreach (var song in set.Songs)
                    {
                        result.AppendLine($"----{song.Name} ({TimeSpanToString(song.Duration)})");
                    }
                }
            }

            return result.ToString().TrimEnd();
        }

        private string TimeSpanToString(TimeSpan timeSpan)
        {
            int minutes = timeSpan.Hours * 60 + timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            return $"{minutes:D2}:{seconds:D2}";
        }
    }
}