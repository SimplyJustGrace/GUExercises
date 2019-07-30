using GUExercises.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GUExercisesTests
{
    [TestClass]
    public class AnswerServiceTests
    {
        private AnswersService _answersService;

        [TestInitialize]
        public void Initialize()
        {
            _answersService = new AnswersService();
        }

        [TestMethod]
        public void GetUser()
        {
            var userModel = _answersService.GetUser();
            Assert.IsNotNull(userModel);
            Assert.AreEqual("Grace Uy", userModel.Name);
            Assert.AreEqual("1234-455662-22233333-3333", userModel.Token);
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
