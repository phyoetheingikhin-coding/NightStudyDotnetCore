using Microsoft.AspNetCore.Mvc;
using NightStudyDotNetCore.MVCApp.Models;

namespace NightStudyDotNetCore.MVCApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        public CustomerController()
        {
            _context = new AppDbContext();
        }

        [ActionName("Customer")]
        public IActionResult Customer()
        {
            List<CustomerModel> lst = _context.Customers.ToList();
            return View("Customer", lst);
        }


        [HttpGet]
        [ActionName("Edit")]
        public IActionResult CustomerEdit(int id)
        {
            CustomerModel item = _context.Customers.FirstOrDefault(item => item.CustomerId == id)!;
            if (item is null)
            {
                return Redirect("/Customer");
            }
            else
            {
                return View("CustomerEdit",item);
            }

        }


        [ActionName("Create")]
        public IActionResult CustomerCreate()
        {
            return View("CustomerCreate");

        }


        [ActionName("Save")]
        public IActionResult CustomerSave(CustomerModel item)
        {
            _context.Customers.Add(item);
            var result = _context.SaveChanges();
            return Redirect("Customer/Customer");
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult CustomerUpdate(int id, CustomerModel model)
        {
            CustomerModel? item = _context.Customers.FirstOrDefault(item => item.CustomerId == id)!;
            if (item is null)
            {
                return Redirect("Customer/Customer");
            }
            else
            {
                item.CustomerName = model.CustomerName;
                item.PhoneNo = model.PhoneNo;
                item.DateOfBirth = model.DateOfBirth;
                item.Gender = model.Gender;
                _context.SaveChanges();
                return Redirect("/Customer");
            }
        }

        [ActionName("Delete")]
        public IActionResult CustomerDelete(int id)
        {
            CustomerModel? item = _context.Customers.FirstOrDefault(item => item.CustomerId == id)!;
            if (item is null)
            {
                return Redirect("Customer/Customer");
            }
            else
            {
                _context.Remove(item);
                _context.SaveChanges();
                return Redirect("Customer/Customer");
            }
        }
    }
}
