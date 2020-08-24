using System.Linq;
using Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organization.Models;


namespace Organization.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public IActionResult Index()
        {

            return View();
        }


        public IActionResult GetTree(int? Id)
        {
         
            if (Id == null)
            {
                var Tree = db.Houses.Select(x => new
                {
                    id = x.Id,
                    text = x.Name,
                    levelTree = 1,
                    flag = db.Equipment.Where(y=>y.Office.HouseId == x.Id).Select(y=>y.Id).Count(),
                    hasChildren = (db.Offices.Where(y => y.HouseId == x.Id).Select(y => new
                    {
                        OfficeId = y.Id
                    }).Count())
                });
                return Json(Tree.ToList(), new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                var Tree = db.Offices.Where(x => x.HouseId == Id).Select(x => new
                {
                    id = x.Id,
                    text = x.Name,
                    levelTree = 2,
                    flag = db.Equipment.Where(y => y.OfficeId == x.Id).Select(y => y.Id).Count()
                });
                return Json(Tree.ToList(), new Newtonsoft.Json.JsonSerializerSettings());
            }

        }

        public IActionResult Grid([DataSourceRequest]DataSourceRequest request, int Id, int Level)
        {

            IQueryable<Data.Equipment> equipment = db.Equipment;
            if (Level == 1)
            {

                DataSourceResult result = equipment.Where(x => x.Office.HouseId == Id).ToDataSourceResult(request, entity => new EquipmentViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Count = entity.Count,
                    OfficeId = entity.OfficeId
                });
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                DataSourceResult result = equipment.Where(x => x.OfficeId == Id).ToDataSourceResult(request, entity => new EquipmentViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Count = entity.Count,
                    OfficeId = entity.OfficeId
                });
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }

        }

        public IActionResult Create([DataSourceRequest]DataSourceRequest request, EquipmentViewModel model, int OffId)
        {

            var entity = new Data.Equipment
                {
                    Name = model.Name,
                    Count = model.Count,
                    OfficeId = OffId
                };

                db.Equipment.Add(entity);
                db.SaveChanges();
            return Json(new[] { model }.ToDataSourceResult(request));
        }
        public IActionResult Update([DataSourceRequest]DataSourceRequest request, EquipmentViewModel model)
        {
  
            var entity = new Data.Equipment
                {
                    Id = model.Id,
                    Name = model.Name,
                    Count = model.Count,
                    OfficeId = model.OfficeId
                };

                db.Equipment.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult Destroy([DataSourceRequest]DataSourceRequest request, EquipmentViewModel model)
        {
                var entity = db.Equipment.FirstOrDefault(q => q.Id == model.Id);

                db.Equipment.Attach(entity);
                db.Equipment.Remove(entity);
                db.SaveChanges();

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }

}
