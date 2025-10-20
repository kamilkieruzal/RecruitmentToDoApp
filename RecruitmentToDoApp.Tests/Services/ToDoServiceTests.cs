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

        [Fact]
        public async void ToDoService_GetToDosAsync_EmptyFilter()
        {
            var filter = new ToDoParams();
            var result = await toDoService.GetToDosAsync(filter);

            Assert.NotNull(result);
            Assert.Equal(28, result.Count);
        }

        [Fact]
        public async void ToDoService_GetToDosAsync_Filter()
        {
            var filter = new ToDoParams
            {
                MinExpiryDate = new DateTime(2026, 2, 1),
                MaxExpiryDate = new DateTime(2026, 3, 31),
            };

            var result = await toDoService.GetToDosAsync(filter);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async void ToDoService_GetToDosAsync_EmptyResultFilter()
        {
            var filter = new ToDoParams
            {
                MinExpiryDate = new DateTime(2027, 1, 1),
                MaxExpiryDate = new DateTime(2027, 2, 1),
            };

            var result = await toDoService.GetToDosAsync(filter);

            Assert.Empty(result);
        }

        [Fact]
        public async void ToDoService_GetToDosAsync_WrongDates()
        {
            var filter = new ToDoParams
            {
                MinExpiryDate = new DateTime(2027, 1, 1),
                MaxExpiryDate = new DateTime(2027, 2, 1),
            };

            var result = await toDoService.GetToDosAsync(filter);

            Assert.Empty(result);
        }

        [Fact]
        public async void ToDoService_GetToDosAsync_Pagination()
        {
            var filter = new ToDoParams
            {
                PageNumber = 6,
                PageSize = 5
            };

            var result = await toDoService.GetToDosAsync(filter);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async void ToDoService_GetToDosAsync_PaginationTooFar()
        {
            var filter = new ToDoParams
            {
                PageNumber = 9,
                PageSize = 5
            };

            var result = await toDoService.GetToDosAsync(filter);

            Assert.Empty(result);
        }

        [Fact]
        public async void ToDoService_CreateToDoAsync_Create()
        {
            var toDo = new ToDo
            {
                CompletePercentage = 0,
                Description = "Desc",
                ExpiryDate = DateTime.Now,
                Title = "Title"
            };
            dbContextMock.SaveChangesAsync().Returns(1);

            var result = await toDoService.CreateToDoAsync(toDo);

            Assert.True(result);
        }

        [Fact]
        public async void ToDoService_CreateToDoAsync_Fail()
        {
            var toDo = new ToDo
            {
                Id = 1,
                CompletePercentage = 0,
                Description = "Desc",
                ExpiryDate = DateTime.Now,
            };
            dbContextMock.SaveChangesAsync().Returns(0);

            var result = await toDoService.CreateToDoAsync(toDo);

            Assert.False(result);
        }

        [Fact]
        public async void ToDoService_UpdateToDoAsync_Update()
        {
            var toDo = new ToDo();
            dbContextMock.SaveChangesAsync().Returns(1);

            var result = await toDoService.UpdateToDoAsync(1, toDo);

            Assert.True(result);
        }

        [Fact]
        public async void ToDoService_UpdateToDoAsync_Fail()
        {
            var toDo = new ToDo();
            dbContextMock.SaveChangesAsync().Returns(0);
            var result = await toDoService.UpdateToDoAsync(100, toDo);

            Assert.False(result);
        }

        [Fact]
        public async void ToDoService_DeleteToDoAsync_Delete()
        {
            dbContextMock.SaveChangesAsync().Returns(1);
            var result = await toDoService.DeleteToDoAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async void ToDoService_DeleteToDoAsync_Fail()
        {
            var result = await toDoService.DeleteToDoAsync(100);

            Assert.False(result);
        }
    }
}
