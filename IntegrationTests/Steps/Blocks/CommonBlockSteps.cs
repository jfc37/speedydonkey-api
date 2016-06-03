using System.Collections.Generic;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using Contracts.Enrolment;
using Contracts.Teachers;
using Contracts.Users;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Blocks
{
    [Binding]
    public class CommonBlockSteps
    {
        [Given(@"a block exists")]
        public void GivenABlockExists()
        {
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.GivenAValidBlockIsReadyToBeSubmitted();
            createBlockSteps.WhenTheBlockIsAttemptedToBeCreated();
            createBlockSteps.ThenBlockCanBeRetrieved();

            var blockId = ScenarioCache.GetId(ModelIdKeys.BlockId);
            var blockIds = ScenarioCache.Get<List<int>>(ModelIdKeys.BlockIds);
            blockIds.Add(blockId);
            ScenarioCache.Store(ModelIdKeys.BlockIds, blockIds);
        }

        [Given(@"a block with '(.*)' classes exists")]
        public void GivenABlockWithClassesExists(int numberOfClasses)
        {
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.GivenAValidBlockIsReadyToBeSubmitted();
            createBlockSteps.GivenTheNumberOfClassesInTheBlockIs(numberOfClasses);
            createBlockSteps.WhenTheBlockIsAttemptedToBeCreated();
            createBlockSteps.ThenBlockCanBeRetrieved();
        }

        [Given(@"a teacher is teaching a solo block and a partnered block")]
        public void GivenATeacherIsTeachingASoloBlockAndAPartneredBlock()
        {
            GivenABlockWithClassesExists(2);
            var mainTeacher = new TeacherModel(ScenarioCache.GetTeacherId());

            GivenABlockWithClassesExists(2);
            var secondTeacher = new TeacherModel(ScenarioCache.GetTeacherId());

            var teachers = new List<TeacherModel>
            {
                mainTeacher,
                secondTeacher
            };
            new UpdateBlockSteps().SetTeachersForBlock(teachers);
        }

        [Given(@"a block exists with a student limit of '(.*)'")]
        public void GivenABlockExistsWithAStudentLimitOf(int classCapacity)
        {
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.GivenAValidBlockIsReadyToBeSubmitted();
            createBlockSteps.GivenTheBlockClassCapacityIs(classCapacity);
            createBlockSteps.WhenTheBlockIsAttemptedToBeCreated();
            createBlockSteps.ThenBlockCanBeRetrieved();
        }

        [Given(@"a block exists that is full")]
        public void GivenABlockExistsThatIsFull()
        {
            GivenABlockExistsWithAStudentLimitOf(1);

            var userId = ScenarioCache.GetUserId();

            GivenTheUserEnrolsInTheBlock();

            ScenarioCache.Store(ModelIdKeys.UserId, userId);
        }


        [Given(@"an invite only block exists")]
        public void GivenAnInviteOnlyBlockExists()
        {
            var createBlockSteps = new CreateBlockSteps();
            createBlockSteps.GivenAValidBlockIsReadyToBeSubmitted();
            createBlockSteps.GivenTheBlockIsInviteOnly();
            createBlockSteps.WhenTheBlockIsAttemptedToBeCreated();
            createBlockSteps.ThenBlockCanBeRetrieved();
        }


        [Given(@"'(.*)' blocks exists")]
        public void GivenBlocksExists(int numberOfBlocks)
        {
            numberOfBlocks.ToNumberRange()
                .Each(x =>
                {
                    GivenABlockExists();
                });
        }

        [Given(@"the user enrols in the block")]
        public void GivenTheUserEnrolsInTheBlock()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(new EnrolmentModel(ScenarioCache.GetId(ModelIdKeys.BlockId)),
                Routes.GetEnrolUserInBlock(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Given(@"'(.*)' users are enrols in the block")]
        public void GivenUsersAreEnrolsInTheBlock(int numberOfUsers)
        {
            for (int i = 1; i <= numberOfUsers; i++)
            {
                new CommonUserSteps().GivenAUserExists();
                GivenTheUserEnrolsInTheBlock();
            }
        }

        [Given(@"the user enrols in '(.*)' blocks")]
        public void GivenTheUserEnrolsInBlocks(int numberOfBlocks)
        {
            for (int blockId = 1; blockId <= numberOfBlocks; blockId++)
            {
                var response = ApiCaller.Post<ActionReponse<UserModel>>(new EnrolmentModel(blockId),
                    Routes.GetEnrolUserInBlock(ScenarioCache.GetUserId()));

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);   
            }

        }

    }
}
