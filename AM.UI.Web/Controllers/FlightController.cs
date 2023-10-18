using AM.ApplicationCore;
using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.UI.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AM.UI.Web.Controllers
{
    public class FlightController : Controller
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        public IFlightService flightService { get; private set; }
        public IWebHostEnvironment webHostEnvironment { get; private set; }
       // public IPlaneService planeService { get; private set; }  
        
        // GET: FlightController1
        public FlightController(IUnitOfWork unitOfWork , IFlightService flightService , IWebHostEnvironment webHostEnvironment)
        {
            this.UnitOfWork = unitOfWork;
            this.flightService = flightService;
            this.webHostEnvironment = webHostEnvironment;
        }
       
        public ActionResult Index()
        {     
            return View(flightService.GetAll());
        }
        [HttpPost]
        public ActionResult Index(DateTime ?date)
        {     
            if (date == null)
            {
                return View(flightService.GetAll());
            }
            return View(flightService.GetFlightsByDate(date.Value));

        }

        // GET: FlightController1/Details/5
        public ActionResult Details(int id)
        {
            return View(flightService.GetById(id));
        }

        // GET: FlightController1/Create
        public ActionResult Create()
        {
            IPlaneService planeService = new PlaneService(UnitOfWork);
            var allPlanes = planeService.GetAll();
            ViewBag.Planes = new SelectList(allPlanes.Select(e => new { e.PlaneId, description = "id :" + e.PlaneId + "{ capacity :" + e.Capacity +"}"}), nameof(Plane.PlaneId), "description");
            return View();
        }

        // POST: FlightController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight flight)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                flight.Pilot = Request.AddRequestFile(webHostEnvironment, "wwwroot", "upload");
                flightService.Add(flight);
                UnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View(flightService.GetById(id));
        }

        // POST: FlightController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                flightService.Update(flight);
                UnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View(flightService.GetById(id));
        }

        // POST: FlightController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Flight flight)
        {
            try
            {
                Flight flight1=flightService.GetById(id);
                flightService.Delete(flight1);
                UnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
