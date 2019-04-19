namespace FestivalManager.Tests
{
    // When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)
    using FestivalManager.Core.Controllers;
    using FestivalManager.Core.Controllers.Contracts;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Contracts;
    using FestivalManager.Entities.Instruments;
    using FestivalManager.Entities.Sets;

    //DO NOT REMOVE the governmental
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class SetControllerTests
    {
        [Test]
        public void PerformSetsShouldReturnErrorMessage()
        {
            IStage stage = new Stage();

            ISetController setController = new SetController(stage);

            ISet set = new Short("PlayList");

            stage.AddSet(set);

            string expectedResult = $"1. PlayList:\r\n-- Did not perform";
            string actualResult = setController.PerformSets();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PerformSetsShouldReturnSuccessfulMessage()
        {
            IStage stage = new Stage();

            ISetController setController = new SetController(stage);

            ISet set = new Short("PlayList");

            ISong song = new Song("Song", new TimeSpan(0, 2, 30));

            IPerformer performer = new Performer("Pesho", 20);

            performer.AddInstrument(new Guitar());
            set.AddPerformer(performer);
            set.AddSong(song);
            stage.AddSet(set);

            string expectedResult = $"1. PlayList:\r\n-- 1. Song (02:30)\r\n-- Set Successful";
            string actualResult = setController.PerformSets();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PerformSetsShouldWearDownInstruments()
        {
            IStage stage = new Stage();

            ISetController setController = new SetController(stage);

            ISet set = new Short("PlayList");

            ISong song = new Song("Song", new TimeSpan(0, 2, 30));

            IPerformer performer = new Performer("Pesho", 20);

            IInstrument instrument = new Guitar();

            performer.AddInstrument(instrument);
            set.AddPerformer(performer);
            set.AddSong(song);
            stage.AddSet(set);

            double instrumentWearBeforePerformance = instrument.Wear;

            setController.PerformSets();

            double instrumentWearAfterPerformance = instrument.Wear;

            Assert.AreNotEqual(instrumentWearBeforePerformance, instrumentWearAfterPerformance);
        }
    }
}