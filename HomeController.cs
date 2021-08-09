using CoreAssement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace CoreAssement.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Regd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult saveInfo([Bind] CustomerModel emp, IFormFile files)
        {
            if (ModelState.IsValid)
            {
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        //Getting FileName
                        var fileName = Path.GetFileName(files.FileName);
                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);
                        // concatenating  FileName + FileExtension
                        //var newFileName = String.Concat(myUniqueFileName, fileExtension);
                        var newFileName = fileName;
                        // Combines two strings into a path.
                        var filepath =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";
                        emp.ImageUpload = newFileName;
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }
                        
                    }
                }
               
                int a = emp.InsertEmp(emp);
                if (a == 1)
                {
                    TempData["name"] = "Saved Successfully";
                    // ViewBag.Message = "Information Saved Successfully";
                    return View("Regd");
                }
            }
            return View("Regd");

        }       
      
        //Login
        public IActionResult LoginV()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginCheck([Bind] CustomerModel login)
        {
            try
            {


                int a = login.LoginCheck(login);
                if (a == 1)
                {
                    ViewBag.Message = "Login Success";
                    // ViewBag.Username = login.UsrName;
                    return RedirectToAction("Dashboard");

                }
                else
                {
                    ViewBag.Message = "Invalid Credential";
                    return View("LoginV");
                }

                //  return View("LoginV");

            }
            catch
            {
                return View();
            }
        }

        //Dashboard
        public IActionResult Dashboard()
        {
            CustomerModel objstaticcla = new CustomerModel();
            List<CustomerModel> listobj = new List<CustomerModel>();
            listobj= objstaticcla.Getdata().ToList();
           // listobj = objstaticcla.Getdata().Result.ToList();
            ViewBag.EmpDetails = listobj;

            return View();
        }
               
        public IActionResult DeleteCustInfo(int id)
        {
            CustomerModel objstaticcla = new CustomerModel();
            if (objstaticcla.DeleteCustInfo(id) > 0)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
            
        }



        public IActionResult Logout()
        {

            return RedirectToAction("LoginV");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
