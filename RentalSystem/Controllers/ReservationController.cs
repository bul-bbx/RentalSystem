using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Data.Models.Entities;
using RentalSystem.Data.Models;
using RentalSystem.Data.Services;

namespace RentalSystem.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _requestsService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService requestsService, IMapper mapper)
        {
            _requestsService = requestsService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("All", "Cars");
            }

            var serviceModel = _mapper.Map<ReservationServiceModel>(model);

            var result = await _requestsService.Create(serviceModel, User.Identity.Name);
            if (!result)
            {
                return RedirectToAction("All", "Cars");
            }

            return RedirectToAction("My", "Cars");
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var requests = await _requestsService.GetAll();

            if (User.IsInRole("Admin"))
            {
                var adminViewModels = requests.Select(_mapper.Map<ReservationListingViewModel>);
                return View("AdminRequestView", adminViewModels);
            }
            else
            {
                var userViewModels = requests.Select(_mapper.Map<CarListingViewModel>);
                return View("UserRequestView", userViewModels);
            }
        }
    }
}
