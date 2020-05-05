using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        NWDataContext nDC = new NWDataContext();
        // GET: Product
        public ActionResult Index()
        {
            var products = from p in nDC.Products select p;
            return View(products);
        }
        public ActionResult Details(int id)
        {
            var product = from p in nDC.Products where p.ProductID == id select p; // trả về 1 mảng model
            var p1 = nDC.Products.Where(m => m.ProductID == id).FirstOrDefault(); // trả về 1 model
            return View(product);
        }
        public ActionResult Create() // hiển thị form thêm SP
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fC) // hành động thêm
        {
            var maSP = fC["MaSP"];
            var tenSP = fC["TenSP"];
            if (string.IsNullOrEmpty(maSP))
            {
                ViewData["Loi"] = "Không được để trống";

            }
            else if (string.IsNullOrEmpty(tenSP))
            {
                ViewData["nullProductName"] = "Không được để trống product name";
            }
            else
            {
                Product p = new Product();
                p.ProductName = tenSP;
                nDC.Products.InsertOnSubmit(p);
                nDC.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id) // lấy ra product để hiển thị lên place holder 
        {
            var product = nDC.Products.Where(m => m.ProductID == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(int id , FormCollection fC) // ấn nút save 
        {
            var tenSP = fC["TenSP"];
            var product = nDC.Products.Where(m => m.ProductID == id).FirstOrDefault();
            product.ProductName = tenSP;
            nDC.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var productDelete = nDC.Products.Where(m => m.ProductID == id).FirstOrDefault();
            nDC.Products.DeleteOnSubmit(productDelete);
            nDC.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}