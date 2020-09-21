using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(
            IWalkerRepository walkerRepository,
            INeighborhoodRepository neighborhoodRepository,
            IDogRepository dogRepository,
            IWalkRepository walkRepository,
            IOwnerRepository ownerRepository)
        {
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
            _dogRepo = dogRepository;
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
        }
        // GET: Walkers
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();
            Owner owner = _ownerRepo.GetOwnerById(ownerId);
            List<Walker> allWalkers = _walkerRepo.GetAllWalkers();

            if (ownerId == 0)
            {
                return View(allWalkers);
            }
            else
            {
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);
                return View(walkers);
            }
        }

        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            Neighborhood neighborhood = _neighborhoodRepo.GetNeighborhoodByWalkerId(walker.Id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(walker.Id);


            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Neighborhood = neighborhood,
                Walks = walks,
                Dogs = dogs
            };

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
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
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                return int.Parse(id);
            }
            else
            {
                return 0;
            }
        }
    }
}
