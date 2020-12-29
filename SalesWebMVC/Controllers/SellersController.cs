using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exception;

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
            var list = _sellerService.FindAllAsync(); 

            return View(list);
        }

        //GET
        public IActionResult Create()
        {
            var departments = _departmentService.FindAllAsync();
            var viewModel = new SellerFromViewModel { Departments = departments};
            return View(viewModel);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sellers seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAllAsync();
                var viewModel = new SellerFromViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { Message = "Id not provided"});

            var obj = _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });

            return View(obj);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });

            var obj = _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });

            var obj = _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });

            List<Department> departments = _departmentService.FindAllAsync();
            SellerFromViewModel viewModel = new SellerFromViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Sellers seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAllAsync();
                var viewModel = new SellerFromViewModel { Seller = seller ,Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id missmatch" });
            }
            try
            {
                _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(viewModel);
        }
    }
}
