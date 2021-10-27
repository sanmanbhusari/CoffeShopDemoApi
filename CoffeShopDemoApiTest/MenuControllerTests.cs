using AutoFixture;
using CoffeShopDemoApi.Controllers;
using CoffeShopDemoApi.Model;
using CoffeShopDemoRepository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CoffeShopDemoApiTest
{
    public class MenuControllerTests
    {

        private MenuController _sut;
        private Mock<IRepository<MenuItem>> _mockRepo;
        private Mock<ILogger<MenuController>> _mockLogger;

        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockRepo = new Mock<IRepository<MenuItem>>();
            _mockLogger = new Mock<ILogger<MenuController>>();
            _sut = new MenuController(_mockLogger.Object, _mockRepo.Object);
        }

        [Test]
        public async Task GetMenuItems_Returns_MenuItems_When_MenuItemsAreNotEmpty()
        {
            var menuItems = _fixture.CreateMany<MenuItem>();
            _mockRepo.Setup(repository => repository.Get()).ReturnsAsync(menuItems);
            var testMenuItems = await _sut.GetMenuItems();
            testMenuItems.Should().BeEquivalentTo(menuItems);
        }

        [Test]
        public async Task GetMenuItems_Returns_Empty_When_MenuItemsAreEmpty()
        {
            _mockRepo.Setup(repository => repository.Get()).ReturnsAsync(new MenuItem[] { });
            var testMenuItems = await _sut.GetMenuItems();
            testMenuItems.Should().BeEmpty();
        }

    }
}