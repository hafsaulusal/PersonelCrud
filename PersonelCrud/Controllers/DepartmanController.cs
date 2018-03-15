using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelCrud.Models.EntityFramework;

namespace PersonelCrud.Controllers
{

    public class DepartmanController : Controller
    {
       
        PersonelDbEntities db = new PersonelDbEntities();

        // GET: Departman
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());
        }

        [HttpPost] //form methodunda belirlenen post işlemi için 
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }


            if (departman.Id == 0)
                db.Departman.Add(departman);
            else
            {
                var guncelleDepartman = db.Departman.Find(departman.Id);

                if (guncelleDepartman == null)
                    return HttpNotFound();
                else
                {
                    guncelleDepartman.Ad = departman.Ad;
                }

            }

            db.SaveChanges();
            
            //kayıt tamamlandıktan sonra Index sayfasına yönlendirdi
            return RedirectToAction("Index","Departman");
        }

        public ActionResult Güncelle(int id)

        {
            //helper methodu ile ilgili textbox dolduruldu.
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();

            return View("DepartmanForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silDepartman = db.Departman.Find(id);

            if (silDepartman == null)
                return HttpNotFound();

            db.Departman.Remove(silDepartman);
            db.SaveChanges();

            return RedirectToAction("Index","Departman");

        }

    }
}