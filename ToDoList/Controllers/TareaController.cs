using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TareaController : Controller
    {
        private static int IdT=1;
        private List<Tarea> ListaTareas()
        {
            List<Tarea> lista = Session["Tareas"] as List<Tarea>;
            if (lista == null)
            {
                lista = new List<Tarea>();
                Session["Tareas"] = lista;
            }
            return lista;
        }

        // GET: Tarea
        public ActionResult Index()
        {
   
            return View();
        }

        // GET: Tarea
        [HttpGet]
        public JsonResult GetTareas()
        {
            List<Tarea> lista = ListaTareas();
            return Json(lista.Select(t => new {
                t.Id,
                t.Title,
                t.Description,
                Created_at = t.Created_at.ToString("dd/MM/yyyy"),
                DeadLine = t.DeadLine.ToString("dd/MM/yyyy"),
                t.Status,
                t.Priority
            }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(string title, string description,DateTime deadline, string priority)
        {
            Tarea tarea = new Tarea();
            tarea.Id= IdT++;
            tarea.Title = title;
            tarea.Description = description;
            tarea.Created_at = DateTime.Now;
            tarea.DeadLine = deadline;
            tarea.Status = "Pendiente";
            tarea.Priority = priority;
            List<Tarea> lista = ListaTareas();
            lista.Add(tarea);
            Session["Tareas"] = lista;
            return Json(tarea, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SearchTarea(int id)
        {
            List<Tarea> lista = ListaTareas();

            Tarea tarea = lista.Find(t => t.Id == id);

            return Json(new
            {
                tarea.Id,
                tarea.Title,
                tarea.Description,
                DeadLine = tarea.DeadLine.ToString("yyyy-MM-dd"),
                tarea.Status,
                tarea.Priority
            }, JsonRequestBehavior.AllowGet);
        }


       [HttpPost]
       public JsonResult Edit( int id, string title, string description, DateTime deadline, string priority, string status)
       {
            List<Tarea> lista = ListaTareas();

           Tarea tarea = lista.Find(t => t.Id == id);

           tarea.Title = title;
           tarea.Description = description;

           tarea.DeadLine = deadline;
           tarea.Status = status;
           tarea.Priority = priority;

       
           Session["Tareas"] = lista;

           return Json(tarea, JsonRequestBehavior.AllowGet);
       }

        
        [HttpPost]
        public JsonResult Delete(int id)
        {
            List<Tarea> lista = ListaTareas();
            
            Tarea tarea = lista.Find(t => t.Id == id);

            lista.Remove(tarea);

            Session["Tareas"] = lista;

            return Json(tarea, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult FilterTareas(string estado)
        {
            List<Tarea> lista = ListaTareas();

            if (estado != "Todas")
            {
                lista = lista.Where(t => t.Status == estado).ToList();
            }

            return Json(lista.Select(t => new {
                t.Id,
                t.Title,
                t.Description,
                Created_at = t.Created_at.ToString("dd/MM/yyyy"),
                DeadLine = t.DeadLine.ToString("dd/MM/yyyy"),
                t.Status,
                t.Priority
            }), JsonRequestBehavior.AllowGet);

        }


        /*

       // GET: Tarea/Details/5
       public ActionResult Details(int id)
       {
           return View();
       }

       // GET: Tarea/Create
       public ActionResult Create()
       {
           return View();
       }

       // POST: Tarea/Create
       [HttpPost]
       public ActionResult Create(FormCollection collection)
       {
           try
           {
               // TODO: Add insert logic here

               return RedirectToAction("Index");
           }
           catch
           {
               return View();
           }
       }

       // GET: Tarea/Edit/5
       public ActionResult Edit(int id)
       {
           return View();
       }

       // POST: Tarea/Edit/5
       [HttpPost]
       public ActionResult Edit(int id, FormCollection collection)
       {
           try
           {
               // TODO: Add update logic here

               return RedirectToAction("Index");
           }
           catch
           {
               return View();
           }
       }

       // GET: Tarea/Delete/5
       public ActionResult Delete(int id)
       {
           return View();
       }

       // POST: Tarea/Delete/5
       [HttpPost]
       public ActionResult Delete(int id, FormCollection collection)
       {
           try
           {
               // TODO: Add delete logic here

               return RedirectToAction("Index");
           }
           catch
           {
               return View();
           }
       }
       */
    }

}
