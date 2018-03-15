using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using PersonelCrud.Models.EntityFramework;
using PersonelCrud.ViewModels;

namespace PersonelCrud.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();

        // GET: Personel
        public ActionResult Index()
        {
            /*PersonelDB.exmx -> Lazy loading enable = False
            Her bir satır için sorgu göndermek yerine lazy loadingi devre dışı bırakıp
            İger loading yapmak: Bu yöntem inner join ile tek bir sorgu gönderir.
            var model = db.Personel ;
            */

            var model = db.Personel.Include(x => x.Departman).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            var model = new PersonelDepartmanViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                personel = new Personel()
            };
            return View("PersonelForm",model);
        }

        [HttpPost]
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelDepartmanViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    personel = new Personel()
                };
                return View("PersonelForm", model);
            }

            if (personel.Id == 0)
            {
                db.Personel.Add(personel);
            }
            else
            {
                    /*
                    guncellepersonel.Ad = personel.Ad;
                    guncellepersonel.Soyad = personel.Soyad;
                    guncellepersonel.DepartmanId = personel.DepartmanId;
                    guncellepersonel.DogumGunu = personel.DogumGunu;
                    guncellepersonel.Maas = personel.Maas;
                    guncellepersonel.Cinsiyet = personel.Cinsiyet;
                    */

                    db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                        
                
            }


            db.SaveChanges();

            return RedirectToAction("Index", "Personel");
        }


        //Güncelleme işlemi için bilgilerin view içerisine gelmesi için gerekli
        public ActionResult Güncelle(int id)
        {
            var model = new PersonelDepartmanViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                personel = db.Personel.Find(id)
            };

            return View("PersonelForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silPersonel = db.Personel.Find(id);

            if (silPersonel == null)
                HttpNotFound();

            db.Personel.Remove(silPersonel);
            db.SaveChanges();

            return RedirectToAction("Index", "Personel");
  
        }
    }
}