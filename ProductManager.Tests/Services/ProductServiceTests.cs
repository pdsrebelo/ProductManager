using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductManager.Data.Repositories;
using ProductManager.Model.Entities;
using ProductManager.Service;
using ProductManager.Service.Mappings;
using Product = ProductManager.Data.Entities.Product;

namespace ProductManager.Tests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestInitialize]
        public void Setup()
        {
            // Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        [TestCleanup]
        public void TearDown()
        {
            AutoMapperConfiguration.Reset();
        }

        [TestMethod]
        public async Task GetAllProductsExpectsListOfProducts_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                List<Product> products = new List<Product>
                {
                    TestsHelper.CreateDbProduct(1),
                    TestsHelper.CreateDbProduct(2)
                };

                var repo = mock.Mock<IProductRepository>();
                repo.Setup(x => x.GetProducts(CancellationToken.None))
                    .Returns(Task.FromResult(products));

                ProductService service = new ProductService(repo.Object);

                var list = await service.GetAllProductsAsync();

                // Confirm that the mock was called
                mock.Mock<IProductRepository>().
                    Verify(m => m.GetProducts(CancellationToken.None), Times.Once());

                Assert.IsNotNull(list);
                Assert.AreEqual(list.Count(), 2);
            }
        }

        [TestMethod]
        public async Task GetAllProductsExpectsEmptyListOfProducts_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var repo = mock.Mock<IProductRepository>();
                repo.Setup(x => x.GetProducts(CancellationToken.None))
                    .Returns(Task.FromResult(new List<Product>()));

                ProductService service = new ProductService(repo.Object);

                var list = await service.GetAllProductsAsync();

                // Confirm that the mock was called
                mock.Mock<IProductRepository>().
                    Verify(m => m.GetProducts(CancellationToken.None), Times.Once());

                Assert.IsNotNull(list);
                Assert.AreEqual(list.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetProductExpectsEmptyProduct_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var repo = mock.Mock<IProductRepository>();
                repo.Setup(x => x.GetProductById(1, CancellationToken.None))
                    .Returns(Task.FromResult((Product) null));

                ProductService service = new ProductService(repo.Object);

                var returnProduct = await service.GetProductByIdAsync(1);

                // Confirm that the mock was called
                mock.Mock<IProductRepository>().
                    Verify(m => m.GetProductById(1, CancellationToken.None), Times.Once());

                Assert.IsNull(returnProduct);
            }
        }

        [TestMethod]
        public async Task GetProductExpectsProduct_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Product dbProduct = TestsHelper.CreateDbProduct(1);

                var repo = mock.Mock<IProductRepository>();
                repo.Setup(x => x.GetProductById(1, CancellationToken.None))
                    .Returns(Task.FromResult(dbProduct));

                ProductService service = new ProductService(repo.Object);

                var returnProduct = await service.GetProductByIdAsync(dbProduct.Id);

                // Confirm that the mock was called
                mock.Mock<IProductRepository>().
                    Verify(m => m.GetProductById(1, CancellationToken.None), Times.Once());

                Assert.IsNotNull(returnProduct);
                Assert.IsTrue(TestsHelper.ProductComparer(dbProduct, returnProduct));
            }
        }

        [TestMethod]
        public async Task CreateProductExpectsResultOk_Test()
        {
            using (var mock = AutoMock.GetLoose())
            {
                ProductManager.Model.Entities.Product product = TestsHelper.CreateProduct(1);

                var repo = mock.Mock<IProductRepository>();
                repo.Setup(x => x.CreateProduct(It.IsAny<Product>(), CancellationToken.None))
                    .Returns(Task.FromResult(1));
                repo.Setup(x => x.IsValidProductKey(It.IsAny<string>(), CancellationToken.None))
                    .Returns(Task.FromResult(true));

                ProductService service = new ProductService(repo.Object);

                var reason = await service.CreateProductAsync(product);

                // Confirm that the mock was called
                mock.Mock<IProductRepository>().
                    Verify(m => m.CreateProduct(It.IsAny<Product>(), CancellationToken.None), Times.Once());

                Assert.IsNotNull(reason);
                Assert.AreEqual(reason, Reason.Ok);
            }
        }
    }
}
