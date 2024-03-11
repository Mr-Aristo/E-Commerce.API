using E_Commerce.Application.Repositories;
using E_Commerce.Domain.Entities;

namespace xUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mock = new Mock<IProductReadRepository>();
            //mock.Setup(repo=>repo.GetAll()).Returns(Gettes)
        }

        //private List<Product> GetTestUsers()
        //{
        //    var users = new List<Product>
        //    {
        //        new User { Id=1, Name="Tom", Age=35},
        //        new User { Id=2, Name="Alice", Age=29},
        //        new User { Id=3, Name="Sam", Age=32},
        //        new User { Id=4, Name="Kate", Age=30}
        //    };
        //    return users;
        //}
    }
}