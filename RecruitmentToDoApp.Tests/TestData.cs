using Bogus;
using RecruitmentToDoApp.Models;

namespace RecruitmentToDoApp.Tests
{
    public static class TestData
    {
        //Fake data for tests
        public static IList<ToDo> GetFakeToDos()
        {
            var faker = new Faker<ToDo>()
                .RuleFor(t => t.Id, f => f.IndexFaker + 1)
                .RuleFor(t => t.Description, f => "Description " + f.IndexFaker + 1)
                .RuleFor(t => t.Title, f => "Title " + f.IndexFaker + 1)
                .RuleFor(t => t.ExpiryDate, f => f.Date.Between(new DateTime(2025, 12, 10, 10, 0, 0), new DateTime(2026, 1, 10, 10, 0, 0)))
                .RuleFor(t => t.CompletePercentage, f => 0);

            var result = faker.Generate(25);

            result.AddRange(new List<ToDo>()
            {
                new ToDo {Id = 26, CompletePercentage = 0, Description = "Description 26", ExpiryDate = new DateTime(2026, 2, 1, 11, 0, 0)},
                new ToDo {Id = 27, CompletePercentage = 10, Description = "Description 27", ExpiryDate = new DateTime(2026, 2, 2, 12, 0, 0)},
                new ToDo {Id = 28, CompletePercentage = 15, Description = "Description 28", ExpiryDate = new DateTime(2026, 2, 3, 13, 0, 0)},
            });

            return result;
        }
    }
}
