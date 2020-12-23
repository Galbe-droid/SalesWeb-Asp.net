using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); 

            return View(list);
        }

        //GET
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFromViewModel { Departments = departments};
            return View(viewModel);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sellers seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj);
        }
    }
}
