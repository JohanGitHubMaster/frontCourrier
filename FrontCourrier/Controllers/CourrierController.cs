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
        public ActionResult Index(String statusid,String searchstring,String take="600", String skip="0")
        {
         
        
            ViewBag.searchstring = searchstring;
            ViewBag.statusid = statusid;
           CourrierDAO d = new CourrierDAO();
            List<Status> stat = new List<Status>();


            //var counts = d.FindObject("Courriers", null, Int32.Parse(skip), Int32.Parse(take));
           

            var statu = d.FindObject("Status", null,null,null);
            stat.Add(new Status() { Id = 0,Type="Tous" });
            foreach (var co in statu)
            {
                stat.Add((Status)co);
            }
            ViewBag.Status = new SelectList(stat, "Id", "Type");

            //var s = d.FindObject("Courriers", condition);
            Dictionary<string, string> condition = null;
            if (statusid != null && statusid != "0")
            {
                if(condition==null)
                condition = new Dictionary<string, string>();
                condition.Add("StatusId", statusid);
               
            }
            if (!String.IsNullOrEmpty(searchstring))
            {
                searchstring = searchstring.Replace("'", "''");
                if (condition == null)
                    condition = new Dictionary<string, string>();
                condition.Add("Expediteur","%"+ searchstring + "%' OR [Commentaire] LIKE '%"+ searchstring + "%'  OR [CoursierId] LIKE '%" + searchstring + "%' OR [Réferences] LIKE '%" + searchstring+ "%' OR [Objet] LIKE '%"+ searchstring+"%");
            }
            var s = d.FindObject("Courriers", condition, Int32.Parse(skip), Int32.Parse(take));
            var counts = d.FindObject("Courriers", condition, null, null);
            double TotalPage = 0;
            if (take != null)
                TotalPage = (double)counts.Count / double.Parse(take);

            ViewBag.TotalPage = Math.Ceiling(TotalPage);
           

            List<Courriers> c = new List<Courriers>();
            var cours = new List<String>();
            var recept = new List<String>();
            var flag = new List<String>();
            var status = new List<String>();
            foreach (var sq in s)
            {
                Dictionary<string, string> conditioncoursier = new Dictionary<string, string>();
                Dictionary<string, string> conditionreceptioniste = new Dictionary<string, string>();
                Courriers scour = (Courriers)sq;
                c.Add(scour);
                cours.Add(((Coursier)d.ElementCourrier("Coursier", scour.CoursierId)).Nom);               
                recept.Add(((Receptioniste)d.ElementCourrier("Receptioniste", scour.ReceptionisteId)).Nom);
                flag.Add(((Flag)d.ElementCourrier("Flag", scour.FlagId)).Type);                             
                status.Add(((Status)d.ElementCourrier("Status", scour.StatusId)).Type);
                          
                ViewBag.ListCoursier = cours;
                ViewBag.ListReceptioniste = recept;
                ViewBag.ListFlag = flag;
                ViewBag.ListStatus = status;
            }
            return View(c);
        }

        public ActionResult IndexCoursier()
        {
            CourrierDAO d = new CourrierDAO();
            var s = d.FindObject("Coursier",null,null,null);
            List<Coursier> c = new List<Coursier>();
            foreach (var sq in s)
            {
                c.Add((Coursier)sq);
            }
            return View(c);
        }

        public ActionResult IndexMouvementCourrier(string courrierId)
        {
            CourrierDAO d = new CourrierDAO();
            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("CourriersId", courrierId.ToString());
            var s = d.FindObject("MouvementCourrier", condition, null,null);
            String courrier = ((Courriers)d.ElementCourrier("Courriers", Int32.Parse(courrierId))).Réferences;
            var coursier = new List<String>();
            var reception = new List<String>();
            var status = new List<String>();

            List<MouvementCourrier> c = new List<MouvementCourrier>();
            foreach (var sq in s)
            {
                var elementMouvementCourrier = (MouvementCourrier)sq;
               if (elementMouvementCourrier.CoursierId != null)
                    coursier.Add(((Coursier)d.ElementCourrier("Coursier", (int)elementMouvementCourrier.CoursierId)).Nom);
                else
                    coursier.Add(null);

                if (elementMouvementCourrier.StatusId != null)
                    status.Add(((Status)d.ElementCourrier("Status", (int)elementMouvementCourrier.StatusId)).Type);
                else
                    status.Add(null);

                if (elementMouvementCourrier.ReceptionisteId != null)
                    reception.Add(((Receptioniste)d.ElementCourrier("Receptioniste", (int)elementMouvementCourrier.ReceptionisteId)).Nom);
                else
                    reception.Add(null);

                c.Add(elementMouvementCourrier);
            }
            ViewBag.courrier = courrier;
            ViewBag.coursier = coursier;
            ViewBag.reception = reception;
            ViewBag.status = status;
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
