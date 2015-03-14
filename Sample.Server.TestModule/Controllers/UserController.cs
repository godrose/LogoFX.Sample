using System;
using System.Linq;
using System.Web.Http;
using LogoFX.DAL.Repository;
using Sample.Data.Contracts;
using Sample.Server.DAL.Entities;

namespace Sample.Server.TestModule.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("~/api/User/AddUser")]
        public void AddUser(UserDto userDto)
        {
            var userRepo = _unitOfWork.Repository<UserEntity>();
            userRepo.Add(new UserEntity
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Id = Guid.NewGuid()
            });
            _unitOfWork.Save();
        }

        [HttpGet]
        [Route("~/api/User/GetUsers")]
        public UserDto[] GetUsers()
        {
            var userRepo = _unitOfWork.Repository<UserEntity>();
            return userRepo.GetAll().Select(t => new UserDto
            {
                FirstName = t.FirstName,
                LastName = t.LastName
            }).ToArray();
        }
    }
}
