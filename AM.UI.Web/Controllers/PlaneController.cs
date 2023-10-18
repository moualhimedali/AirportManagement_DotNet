﻿using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using Plane = AM.ApplicationCore.Domain.Plane;

namespace AM.UI.Web.Controllers
{
    public class PlaneController : Controller
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        public IPlaneService planeService { get; private set; }
        public IWebHostEnvironment webHostEnvironment { get; private set; }
        public PlaneController(IUnitOfWork unitOfWork , IPlaneService planeService, IWebHostEnvironment webHostEnvironment)
        {
            this.UnitOfWork = unitOfWork;
            this.planeService = planeService;
            this.webHostEnvironment = webHostEnvironment;
        }
        // GET: PlaneControllerr
        public ActionResult Index()
        {
            return View(planeService.GetAll());
        }

        // GET: PlaneControllerr/Details/5
        public ActionResult Details(int id)
        {
            return View(planeService.GetById(id));
        }

        // GET: PlaneControllerr/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: PlaneControllerr/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plane plane)
        {
            try
            {
                planeService.Add(plane);
                UnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaneControllerr/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlaneControllerr/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaneControllerr/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlaneControllerr/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
