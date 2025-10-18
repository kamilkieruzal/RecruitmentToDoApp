using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RecruitmentToDoApp.Controllers;
using RecruitmentToDoApp.Models;
using RecruitmentToDoApp.Services;

namespace RecruitmentToDoApp.Tests.Controllers
{
    public class ToDoControllerTests
    {
        private ToDoController toDoController;
        private IToDoService toDoServiceMock;
        private ILogger<ToDoController> loggerMock;

        public ToDoControllerTests()
        {
            loggerMock = Substitute.For<ILogger<ToDoController>>();
            toDoServiceMock = Substitute.For<IToDoService>();

            toDoController = new ToDoController(toDoServiceMock, loggerMock);
        }

        [Fact]
        public async void ToDoController_GetToDoAsync_Success()
        {
            var toDos = TestData.GetFakeToDos();
            toDoServiceMock.GetToDoByIdAsync(1).Returns(toDos[0]);

            var result = await toDoController.GetToDoAsync(1);

            Assert.Equal(toDos[0].Id, GetObjectResultContent(result).Id);
            Assert.Equal(StatusCodes.Status200OK, GetObjectResult(result).StatusCode);
        }

        [Fact]
        public async void ToDoController_GetToDoAsync_Fail()
        {
            var toDos = TestData.GetFakeToDos();
            toDoServiceMock.GetToDoByIdAsync(1).Returns(toDos[0]);

            var result = await toDoController.GetToDoAsync(50);

            Assert.Equal(StatusCodes.Status404NotFound, (result.Result as NotFoundResult)?.StatusCode);
        }

        [Fact]
        public async void ToDoController_GetToDosAsync_All_Success()
        {
            var toDos = TestData.GetFakeToDos();
            var filter = new ToDoParams();

            toDoServiceMock.GetToDosAsync(filter).Returns(toDos.Take(10).ToList());

            var result = await toDoController.GetToDosAsync(filter);

            //returning max page size from the begining
            Assert.Equal(10, GetObjectResultContent(result).Count);
            Assert.Equal(toDos[0].Id, GetObjectResultContent(result)[0].Id);
            await toDoServiceMock.Received().GetToDosAsync(filter);
            Assert.Equal(StatusCodes.Status200OK, GetObjectResult(result).StatusCode);
        }

        [Fact]
        public async void ToDoController_GetToDosAsync_All_LastPage()
        {
            var toDos = TestData.GetFakeToDos();
            var filter = new ToDoParams
            {
                PageNumber = 10,
                PageSize = 3
            };

            toDoServiceMock.GetToDosAsync(filter).Returns(toDos.Skip(25).ToList());

            var result = await toDoController.GetToDosAsync(filter);

            Assert.Equal(3, GetObjectResultContent(result).Count);
            Assert.Equal(26, GetObjectResultContent(result)[0].Id);
            await toDoServiceMock.Received().GetToDosAsync(filter);
            Assert.Equal(StatusCodes.Status200OK, GetObjectResult(result).StatusCode);
        }

        [Fact]
        public async void ToDoController_GetToDosAsync_Filtered_Success()
        {
            var toDos = TestData.GetFakeToDos();
            var filter = new ToDoParams
            {
                MinExpiryDate = new DateTime(2026, 2, 3),
                MaxExpiryDate = new DateTime(2026, 2, 3, 20, 0, 0)
            };

            toDoServiceMock.GetToDosAsync(filter).Returns(toDos.Where(x => x.ExpiryDate.Date == new DateTime(2026, 2, 3)).ToList());

            var result = await toDoController.GetToDosAsync(filter);

            Assert.Single(GetObjectResultContent(result));
            Assert.Equal(StatusCodes.Status200OK, GetObjectResult(result).StatusCode);
        }

        //REFACTOR TESTS filter (service)
        [Fact]
        public async void ToDoController_GetToDosAsync_Filtered_None()
        {
            var toDos = TestData.GetFakeToDos();
            var filter = new ToDoParams
            {
                MinExpiryDate = new DateTime(2030, 1, 1),
                MaxExpiryDate = new DateTime(2035, 1, 1)
            };

            toDoServiceMock.GetToDosAsync(filter).Returns(new List<ToDo>());

            var result = await toDoController.GetToDosAsync(filter);

            Assert.Empty(GetObjectResultContent(result));
            Assert.Equal(StatusCodes.Status200OK, GetObjectResult(result).StatusCode);
        }

        [Fact]
        public async void ToDoController_GetToDosAsync_Fail()
        {
            var toDos = TestData.GetFakeToDos();
            toDoServiceMock.GetToDoByIdAsync(1).Returns(toDos[0]);

            var result = await toDoController.GetToDoAsync(1);

            Assert.Equal(toDos[0].Id, GetObjectResultContent(result).Id);
            Assert.Equal(StatusCodes.Status200OK, GetObjectResult(result).StatusCode);
        }

        [Fact]
        public async void ToDoController_UpdateToDoAsync_Success()
        {
            var toDo = new ToDo { CompletePercentage = 10, Description = "Desc", ExpiryDate = DateTime.Now, Title = "Title" };
            toDoServiceMock.UpdateToDoAsync(1, toDo).Returns(true);

            var result = await toDoController.UpdateToDoAsync(1, toDo);

            Assert.Equal(StatusCodes.Status200OK, ((OkResult)result).StatusCode);
            await toDoServiceMock.Received().UpdateToDoAsync(1, toDo);
        }

        [Fact]
        public async void ToDoController_UpdateToDoAsync_Fail()
        {
            var toDo = new ToDo { CompletePercentage = 110, Description = "Desc", ExpiryDate = DateTime.Now, Title = "Title" };
            toDoServiceMock.UpdateToDoAsync(1, toDo).Returns(false);

            var result = await toDoController.UpdateToDoAsync(1, toDo);

            Assert.Equal(StatusCodes.Status400BadRequest, ((BadRequestResult)result).StatusCode);
        }


        [Fact]
        public async void ToDoController_CreateToDoAsync_Success()
        {
            var toDo = new ToDo { CompletePercentage = 10, Description = "Desc", ExpiryDate = DateTime.Now, Title = "Title" };
            toDoServiceMock.CreateToDoAsync(toDo).Returns(true);

            var result = await toDoController.CreateToDoAsync(toDo);

            Assert.Equal(StatusCodes.Status200OK, ((OkResult)result).StatusCode);
            await toDoServiceMock.Received().CreateToDoAsync(toDo);
        }

        [Fact]
        public async void ToDoController_CreateToDoAsync_Fail()
        {
            var toDo = new ToDo { CompletePercentage = 110, Description = "Desc", ExpiryDate = DateTime.Now, Title = "Title" };
            toDoServiceMock.CreateToDoAsync(toDo).Returns(false);

            var result = await toDoController.CreateToDoAsync(toDo);

            Assert.Equal(StatusCodes.Status400BadRequest, ((BadRequestResult)result).StatusCode);
        }

        [Fact]
        public async void ToDoController_DeleteToDoAsync_Success()
        {
            var toDos = TestData.GetFakeToDos();
            toDoServiceMock.DeleteToDoAsync(1).Returns(true);

            var result = await toDoController.DeleteToDoAsync(1);

            Assert.Equal(StatusCodes.Status200OK, ((OkResult)result).StatusCode);
            await toDoServiceMock.Received().DeleteToDoAsync(1);
        }

        [Fact]
        public async void ToDoController_DeleteToDoAsync_Fail()
        {
            var toDos = TestData.GetFakeToDos();

            var result = await toDoController.DeleteToDoAsync(-1);

            Assert.Equal(StatusCodes.Status400BadRequest, GetObjectResult<bool>(result).StatusCode);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result) => (T)GetObjectResult(result).Value;
        private static ObjectResult GetObjectResult<T>(ActionResult<T> result) => result.Result as ObjectResult;
    }
}
