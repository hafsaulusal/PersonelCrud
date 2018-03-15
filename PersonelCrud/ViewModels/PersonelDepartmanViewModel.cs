using PersonelCrud.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelCrud.ViewModels
{
    public class PersonelDepartmanViewModel
    {
        public IEnumerable<Departman> Departmanlar {get; set;}

        public Personel personel { get; set; }
    
    }
}