using FilmScanner.Contracts;
using FilmScanner.Controllers;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmScanner.Tests.Controllers
{
	[TestFixture]
	public class UsersControllerTests
	{
		private Mock<IRepositoryWrapper> _repositoryMock;
		private UsersController _controller;

		[SetUp]
		public void SetUp()
		{
			_repositoryMock = new Mock<IRepositoryWrapper>();
		}

		[Test]
		public async Task GetAllUsers_NoUsersInRepository_ReturnsEmpty()
		{
			var users = new List<User>();
			_repositoryMock.Setup(r => r.User.GetAllUsersAsync()).ReturnsAsync(users);
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.GetAllUsers();

			_repositoryMock.Verify(r => r.User.GetAllUsersAsync(), Times.Once);
			Assert.That(result, Is.Empty);
		}

		[Test]
		public async Task GetAllUsers_OneUserInRepository_ReturnsUser()
		{
			var user = GetTestUser();
			var users = new List<User>
			{
				user
			};
			_repositoryMock.Setup(r => r.User.GetAllUsersAsync()).ReturnsAsync(users);
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.GetAllUsers();

			_repositoryMock.Verify(r => r.User.GetAllUsersAsync(), Times.Once);
			Assert.That(result.Single(), Is.EqualTo(user));
		}

		[Test]
		public async Task GetAllUsers_MoreThanOneUserInRepository_ReturnsAllUsers()
		{
			var user = GetTestUser();
			var user2 = new User
			{
				ID = 2,
				CreatedAt = DateTime.Now,
				Password = "password2",
				UserName = "username2"
			};
			var users = new List<User>
			{
				user,
				user2
			};
			_repositoryMock.Setup(r => r.User.GetAllUsersAsync()).ReturnsAsync(users);
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.GetAllUsers();

			_repositoryMock.Verify(r => r.User.GetAllUsersAsync(), Times.Once);
			CollectionAssert.AreEqual(users, result);
		}

		[Test]
		public async Task GetUserById_NoUsersInRepository_NotFoundResult()
		{
			_repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User) null);
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.GetUserById(1);

			_repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
			Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
		}

		[Test]
		public async Task GetUserById_OneUserInRepository_ReturnsCorrectUser()
		{
			var user = GetTestUser();
			_repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.GetUserById(1);

			_repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
			Assert.That(result.Value, Is.EqualTo(user));
		}

		[Test]
		public async Task CreateUser_Null_BadRequestResult()
		{
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.CreateUser(null);

			_repositoryMock.Verify(r => r.User.Create(It.IsAny<User>()), Times.Never);
			Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
		}

		[Test]
		public async Task CreateUser_CorrectUser_CreatedAtActionResult()
		{
			var user = GetTestUser();
			_repositoryMock.Setup(r => r.User.Create(It.IsAny<User>()));
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.CreateUser(user);

			_repositoryMock.Verify(r => r.User.Create(It.IsAny<User>()), Times.Once);
			Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
		}

		[Test]
		public async Task UpdateUser_NullUser_BadRequestResult()
		{
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.UpdateUser(1, null);

			_repositoryMock.Verify(r => r.User.Update(It.IsAny<User>()), Times.Never);
			Assert.That(result, Is.TypeOf<BadRequestResult>());
		}

		[Test]
		public async Task UpdateUser_UserWithWrongId_BadRequestResult()
		{
			var user = GetTestUser();
			_repositoryMock.Setup(r => r.User.Update(It.IsAny<User>()));
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.UpdateUser(2, user);

			_repositoryMock.Verify(r => r.User.Update(user), Times.Never);
			Assert.That(result, Is.TypeOf<BadRequestResult>());
		}

		[Test]
		public async Task UpdateUser_CorrectUser_UserUpdated()
		{
			var user = GetTestUser();
			_repositoryMock.Setup(r => r.User.Update(It.IsAny<User>()));
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.UpdateUser(1, user);

			_repositoryMock.Verify(r => r.User.Update(user), Times.Once);
			Assert.That(result, Is.TypeOf<NoContentResult>());
		}

		[Test]
		public async Task DeleteUser_NoUserInRepository_NotFoundResult()
		{
			_repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User) null);
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.DeleteUser(1);

			_repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
			_repositoryMock.Verify(r => r.User.Delete(It.IsAny<User>()), Times.Never);
			Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
		}

		[Test]
		public async Task DeleteUser_CorrectId_DeletedUserReturned()
		{
			var user = GetTestUser();
			_repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
			_repositoryMock.Setup(r => r.User.Delete(It.IsAny<User>()));
			_controller = new UsersController(_repositoryMock.Object);

			var result = await _controller.DeleteUser(1);

			_repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
			_repositoryMock.Verify(r => r.User.Delete(user), Times.Once);
			Assert.That(result.Value, Is.EqualTo(user));
		}
		private static User GetTestUser()
		{
			return new User
			{
				ID = 1,
				CreatedAt = DateTime.Now,
				Password = "password",
				UserName = "username"
			};
		}
	}
}