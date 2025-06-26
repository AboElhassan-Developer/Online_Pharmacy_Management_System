//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol.Core.Types;
//using Online_Pharmacy_Management_System.Models;
//using Online_Pharmacy_Management_System.Repository;
//using Online_Pharmacy_Management_System.ViewModel;

//namespace Online_Pharmacy_Management_System.Controllers
//{
//    //[Authorize(Roles = "User")]
//    //[Authorize(Roles = "Admin")]
//    public class UserController : Controller
//    {
//        private readonly IUserRepository _userRepository;
//        public UserController(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public IActionResult Index()
//        {
//            var users = _userRepository.GetAll();
//            return View(users);
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpGet]
//        public IActionResult Add()
//        {
//            return View("Add");
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        public IActionResult Add(AddUserModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new User
//                {
//                    Name = model.Name,
//                    Email = model.Email,
//                    Password = model.Password,
//                    Role = model.Role
//                };
//                _userRepository.Add(user);
//                _userRepository.Save();
//                return RedirectToAction("Index");
//            }

//            return View(model);
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpGet]
//        public IActionResult Edit(int id)
//        {
//            var user = _userRepository.GetById(id);
//            if (user == null) { return NotFound(); }
//            var userViewModel = new EditUserModel
//            {
//                Name = user.Name,
//                Email = user.Email,
//                Password = user.Password,
//                Role = user.Role

//            };
//            return View("Edit", userViewModel);
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        public IActionResult Edit(EditUserModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new User
//                {
//                    Name = model.Name,
//                    Email = model.Email,
//                    Password = model.Password,
//                    Role = model.Role
//                };
//                _userRepository.Update(user);
//                _userRepository.Save();
//                return RedirectToAction("Index");
//            }
//            return View(model);
//        }
//        [Authorize(Roles = "Admin")]
//        [HttpGet]
//        public IActionResult Delete(int id)
//        {
//            var user = _userRepository.GetById(id);
//            if (user == null) { return NotFound(); };
//            var viewModwl = new DeleteUserModel()
//            {
//                UserId = user.UserId,
//                Name = user.Name,
//                Email = user.Email,
//                Password = user.Password,
//                Role = user.Role,
//                Massege = "Are you sure you want to delete this User?"
//            };

//            return View(viewModwl);
//        }

//        [HttpPost]
//        public IActionResult ConfirmDelete(DeleteUserModel model)
//        {
//            //if (ModelState.IsValid)
//            //{
//            var user = _userRepository.GetById(model.UserId);
//            if (user == null) { return NotFound(); }
//            _userRepository.Delete(model.UserId);
//            _userRepository.Save();
//            return RedirectToAction("Index");
//            //}

//            return View("Delete", model);

//        }
//    }
//}
