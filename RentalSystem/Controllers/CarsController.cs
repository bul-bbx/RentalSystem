using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Data.Models;
using RentalSystem.Data.Models.Entities;
using RentalSystem.Data.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RentalSystem.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carsService;
        private readonly IReservationService _requestsService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carsService, IReservationService requestsService, IMapper mapper)
        {
            _carsService = carsService;
            _requestsService = requestsService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var allCars = await _carsService.GetAll();
            var carsViewModel = allCars.Select(_mapper.Map<CarListingViewModel>);

            var viewModel = new AllCarsViewModel
            {
                Cars = carsViewModel
            };

            return View(viewModel);
        }


        [Authorize]
        public async Task<IActionResult> My()
        {
            var requests = await _requestsService.GetAllForUser(User.Identity.Name);
            var requestViewModels = requests.Select(_mapper.Map<ReservationListingViewModel>);
            return View(requestViewModels);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CarCreateBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bindingModel);
            }

            var serviceModel = _mapper.Map<CarServiceModel>(bindingModel);
            await _carsService.CreateAsync(serviceModel);

            return RedirectToAction("All");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var car = await _carsService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CarEditViewModel>(car);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, CarEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var existingCar = await _carsService.GetByIdAsync(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            _mapper.Map(viewModel, existingCar);
            await _carsService.UpdateAsync(existingCar);

            return RedirectToAction("All");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            var car = await _carsService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CarDetailsViewModel>(car);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var car = await _carsService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CarDeleteViewModel>(car);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var deletedCar = await _carsService.DeleteAsync(id);
            if (deletedCar == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult AvailableCars()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                ModelState.AddModelError("", "End Date must be greater than Start Date.");
                return View(); // Return the view without passing any data
            }

            var availableCars = await _carsService.GetAvailableCars(startDate, endDate);
            var carsViewModel = availableCars.Select(_mapper.Map<CarListingViewModel>);
            return View(carsViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RequestRental(ReservationCreateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, return a bad request response
                return BadRequest(ModelState);
            }

            try
            {
                // Map the binding model to the service model
                var requestServiceModel = _mapper.Map<ReservationServiceModel>(model);

                // Add the request asynchronously using the service
                var createdRequest = await _requestsService.AddRequestAsync(requestServiceModel);

                // Check if the request was successfully created
                if (createdRequest != null)
                {
                    // If the request was successfully created, redirect to a success page or display a success message
                    return RedirectToAction("RequestSuccess", "Home");
                }
                else
                {
                    // If the request creation failed, return a bad request response or redirect to an error page
                    return BadRequest("Failed to create request.");
                }
            }
            catch (Exception ex)
            {
                // Redirect to an error page or return a bad request response with an error message
                return BadRequest("An unexpected error occurred while processing the request.");
            }
        }
    }
}