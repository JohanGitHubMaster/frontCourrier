using FrontCourrier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontCourrier.Controllers
{
    public class CourrierController : Controller
    {
        // GET: Courrier
        public ActionResult Index()
        {
            CourrierDAO d = new CourrierDAO();
            var s = d.FindObject("Courriers");
            List<Courriers> c = new List<Courriers>();
            foreach(var sq in s)
            {
                c.Add((Courriers)sq);
            }
            return View(c);
        }
        public ActionResult IndexCoursier()
        {
            CourrierDAO d = new CourrierDAO();
            var s = d.FindObject("Coursier");
            List<Coursier> c = new List<Coursier>();
            foreach (var sq in s)
            {
                c.Add((Coursier)sq);
            }
            return View(c);
        }

        public ActionResult IndexMouvementCourrier()
        {
            CourrierDAO d = new CourrierDAO();
            var s = d.FindObject("MouvementCourrier");
            List<MouvementCourrier> c = new List<MouvementCourrier>();
            foreach (var sq in s)
            {
                c.Add((MouvementCourrier)sq);
            }
            return View(c);
        }

        // GET: Courrier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Courrier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courrier/Create
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

        // GET: Courrier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Courrier/Edit/5
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

        // GET: Courrier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Courrier/Delete/5
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
    }
}
