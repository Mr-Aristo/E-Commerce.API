using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.API.Controllers;
using E_Commerce.Application.Repositories;

namespace xUnitTest
{
    /// <summary>
    /// Her kontroller icin tekrarlayarak setup belirlemek yerine tek bir classla controllerlar setup edildi.
    /// Controllerlarin constructorlari farkli parametreler alabilecegi icin her fonksiyon icinde parametreleri mock etmek maliyetlidir.
    /// Dolayisiyla bu islemi tek bir classa tasimak isi kolaylastirir.
    /// </summary>
    public class ControllerTestFixture
    {
        public Mock<IProductReadRepository> MockProductRead { get; private set; }
        public Mock<IProductWriteRepository> MockProductWrite { get; private set; }
        public Mock<IOrderWriteRepository> MockOrderWrite { get; private set; }
        public Mock<ICustomerWriteRepository> MockCustomerWrite { get; private set; }
        public MyTestController ProductController { get; private set; }

        public ControllerTestFixture()
        {
            MockProductRead = new Mock<IProductReadRepository>();
            MockProductWrite = new Mock<IProductWriteRepository>();
            MockOrderWrite = new Mock<IOrderWriteRepository>();
            MockCustomerWrite = new Mock<ICustomerWriteRepository>();

            ProductController = new MyTestController(MockProductRead.Object, MockProductWrite.Object, MockOrderWrite.Object, MockCustomerWrite.Object);
        }
    }
}

/*
 * Her test metodunda tekrar tekrar aynı setup kodunu yazmak yerine, 
 * bu setup işlemlerini ortak bir metoda veya kurulum (setup) metoduna taşıyabilirsiniz. 
 * Bu tür durumlar için xUnit'in IClassFixture<T>
 * veya TestInitialize gibi özelliklerini kullanabilirsiniz. 
 * Bu şekilde, test sınıfınızda tekrar kullanmak üzere global setup işlemleri yapabilirsiniz.
 */
