using Moq;
using PetClinicAPI.Controllers;
using PetClinicAPI.Services;
using PetClinicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetClinicAPI.Models.Requests;

namespace PetClinicAPITests
{
    public class ClientControllerTests
    {

        private ClientController _clientController;
        private Mock<IClientRepository> _mockClientRepository;

        public ClientControllerTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _clientController = new ClientController(_mockClientRepository.Object);
        }

        [Fact]
        public void CreateClientTest()
        {
            // Arrange
            int expected = 1;
            _mockClientRepository.Setup(repository => repository.Create(It.IsAny<Client>())).Returns(expected);

            // Act
            ActionResult<int> result = _clientController.Create(new CreateClientRequest());

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<int>(((OkObjectResult)result.Result).Value);
            Assert.Equal(expected, ((OkObjectResult)result.Result).Value);
            _mockClientRepository.Verify(repository => repository.Create(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public void UpdateClientTest()
        {
            // Arrange
            int expected = 1;
            _mockClientRepository.Setup(repository => repository.Update(It.IsAny<Client>())).Returns(expected);

            // Act
            ActionResult<int> result = _clientController.Update(new UpdateClientRequest());

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<int>(((OkObjectResult)result.Result).Value);
            Assert.Equal(expected, ((OkObjectResult)result.Result).Value);
            _mockClientRepository.Verify(repository => repository.Update(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public void DeleteClientTest()
        {
            // Arrange
            int expected = 1;
            _mockClientRepository.Setup(repository => repository.Delete(0)).Returns(expected);

            // Act
            ActionResult<int> result = _clientController.Delete(0);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<int>(((OkObjectResult)result.Result).Value);
            Assert.Equal(expected, ((OkObjectResult)result.Result).Value);
            _mockClientRepository.Verify(repository => repository.Delete(0), Times.Once);
        }

        [Fact]
        public void GetClientByIdTest()
        {
            // Arrange
            _mockClientRepository.Setup(repository => repository.GetById(0)).Returns(new Client());

            // Act
            ActionResult<Client> result = _clientController.GetById(0);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<Client>(((OkObjectResult)result.Result).Value);
            _mockClientRepository.Verify(repository => repository.GetById(0), Times.Once);
        }

        [Fact]
        public void GetAllClientsTest()
        {
            // Arrange
            _mockClientRepository.Setup(repository =>
            repository.GetAll(0)).Returns(new List<Client>()
            {
                new Client(),
                new Client(),
                new Client()
            });

            // Act
            ActionResult<List<Client>> result = _clientController.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<List<Client>>(((OkObjectResult)result.Result).Value);
            _mockClientRepository.Verify(repository => repository.GetAll(0), Times.Once);
        }

    }
}
