﻿using LoyaltyPointsExchangeSystem.Models;
using LoyaltyPointsExchangeSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoyaltyPointsExchangeSystem.Controllers
{
    public enum Products
    {
        belt = 200,
        socks = 150,
        Jean = 500,
        jacket = 1000,
        shirt = 300
    }

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext appDbContext;

        public HomeController(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var user = await userManager.GetUserAsync(User);

            var profileViewModel = new ProfileViewModel { Email = user.Email, FirstName = user.firstName, Gender = user.gender, LastName = user.lastName, Points = user.Points ?? 0, pointsTransferHistory = appDbContext.pointsTransferHistories.Where(x => x.ApplicationUserId.ToString() == user.Id).ToList() };
            return View(profileViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Simulatepay()
        {
            return View("SimulatePay");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SimulatepayAsync(PayViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                PointsTransferHistory pointsTransferHistory = new PointsTransferHistory()
                {
                    Id = new Guid(),
                    pointIn = (long?)(model.amount / 100),
                    transactionDate = DateTime.Now,
                    totalPoints = (long?)(model.amount / 100) + user.Points ?? 0
                };
                user.Points += (long?)(model.amount / 100);
                user.pointsTransferHistory = new List<PointsTransferHistory> { pointsTransferHistory };
                var results = await userManager.UpdateAsync(user);
                appDbContext.SaveChanges();
            }
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> TransferPoints()
        {
            var LoginUser = await userManager.GetUserAsync(User);
            var users = userManager.Users.Where(x => x.Id != LoginUser.Id).ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var user in users.ToList())
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.Id,
                    Selected = user.Id == users.First().Id
                };
                selectListItems.Add(selectListItem);
            }
            ViewBag.Users = selectListItems;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TransferPoints(TransferPointsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var LoginUser = await userManager.GetUserAsync(User);
                var userTo = userManager.Users.FirstOrDefault(x => x.Id == model.user);

                LoginUser.Points -= model.pointsToTransfer;
                PointsTransferHistory pointsTransferHistoryFrom = new PointsTransferHistory()
                {
                    Id = new Guid(),
                    pointIn = null,
                    pointOut = model.pointsToTransfer,
                    transactionDate = DateTime.Now,
                    totalPoints = LoginUser.Points
                };
                LoginUser.pointsTransferHistory  = new List<PointsTransferHistory> { pointsTransferHistoryFrom };

                userTo.Points += model.pointsToTransfer;
                PointsTransferHistory pointsTransferHistoryTo = new PointsTransferHistory()
                {
                    Id = new Guid(),
                    pointIn = model.pointsToTransfer,
                    pointOut = null,
                    transactionDate = DateTime.Now,
                    totalPoints = userTo.Points
                };
                userTo.pointsTransferHistory = new List<PointsTransferHistory> { pointsTransferHistoryTo };

                appDbContext.SaveChanges();
            }
            return RedirectToAction("index", "home");
        }
    }
}