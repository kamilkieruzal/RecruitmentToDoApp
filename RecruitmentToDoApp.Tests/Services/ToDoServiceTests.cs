using Microsoft.Extensions.Logging;
using NSubstitute;
using RecruitmentToDoApp.Models;
using RecruitmentToDoApp.Services;
using MockQueryable.NSubstitute;

namespace RecruitmentToDoApp.Tests.Services
{
    public class ToDoServiceTests
    {
        private ToDoService toDoService;
        private IToDoAppContext dbContextMock;
        private ILogger<ToDoService> loggerMock;
        private IList<ToDo> testData = TestData.GetFakeToDos();

        public ToDoServiceTests()
        {
            loggerMock = Substitute.For<ILogger<ToDoService>>();
            dbContextMock = Substitute.For<IToDoAppContext>();

            var mockSet = testData.BuildMockDbSet();
            dbContextMock.ToDos.Returns(mockSet);

            toDoService = new ToDoService(dbContextMock, loggerMock);
        }

        [Fact]
        public async void ToDoService_GetToDoByIdAsync_Success()
        {
            var result = await toDoService.GetToDoByIdAsync(1);

            Assert.Equal(testData[0], result);
        }

        [Fact]
        public async void ToDoService_GetToDoByIdAsync_Null()
        {
            var result = await toDoService.GetToDoByIdAsync(100);

            Assert.Null(result);
        }
    }
}
