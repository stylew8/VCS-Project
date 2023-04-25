using DataAccess;
using DataAccessNew.Repositories;
using Moq;
using ShopApi.Models.Users;
using ShopApi.Services;
using ShopApi.Services.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Test.Services
{
    [TestClass]
    public class CreateUserServiceTests
    {   
        private CreateUserModel modelToCreate;
        private Mock<IRepository<User>> userRepositoryMock;

        //paleidziamas pries kiekviena testa is naujo
        [TestInitialize]
        public void TestInitialize()
        {
            modelToCreate = new CreateUserModel(
                "username",
                "petras123",
                "Petras",
                "Petraitis",
                "petras.petraitis@gmail.com"
            );
            //netikra repozitorija
            userRepositoryMock = new Mock<IRepository<User>>();

            userRepositoryMock
                .Setup(
                    x => x.CreateAsync(It.IsAny<User>())
                )
                .Callback<User>(
                    s =>
                    {
                        s.Id = 1;
                        s.Name = "Jonas";
                    }
                );
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public async Task ThrowValidationException()
        {
            var createUserService = new CreateUserService(userRepositoryMock.Object);


            var result = await createUserService.CallAsync(
                new CreateUserModel(
                    "string",
                    "fdsfsf",
                    "fdfs",
                    "Gfdg",
                    "fdsff")
                );
        }

        [TestMethod]
        public async Task CallUserRepositoryOnce()
        {
            //arrange
            var createUserService = new CreateUserService(userRepositoryMock.Object);

            //act
            await createUserService.CallAsync(modelToCreate);
            //assert
            userRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<User>()),
                Times.Once()
            );

            userRepositoryMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task ResultsOCallCointainsValuesFromRepositoryResult()
        {
            //arrange

            var createUserService = new CreateUserService(userRepositoryMock.Object);

            //act
            var result = await createUserService.CallAsync(modelToCreate);

            //assert
            
            Assert.AreEqual( 1, result.Id);
            Assert.AreEqual("Jonas",result.Name);
            Assert.AreEqual("Petraitis",result.Surname);
            Assert.AreEqual("petras.petraitis@gmail.com", result.Email);
            Assert.AreEqual("username", result.Username);
            Assert.AreEqual("petras123", result.Password);

        } 
    }
}
