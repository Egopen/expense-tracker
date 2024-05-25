using Expense_Tracker.DataBase;
using Expense_Tracker.Features;
using Expense_Tracker.ResponseJson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("Expense-Tracker/[controller]/[action]")]
    public class GraficalInfoPage : ControllerBase
    {
        private UserManager<User> userManager;
        private ApplicationContext applicationContext;
        public GraficalInfoPage(UserManager<User> userManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.applicationContext = context;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Access")]
        public async Task<GraficalInfoRes> GetInfo()
        {
            var userid = HttpContext.User.FindFirstValue("UserId");
            if (userid != null)
            {
                var info = new GraficalInfoRes();
                var user = await userManager.FindByIdAsync(userid);
                var list = await (from exp in applicationContext.Expenses
                            where exp.UserId == userid && exp.Date.Month == DateTime.UtcNow.Month && exp.Date.Year== DateTime.UtcNow.Year
                            join cat in applicationContext.Categories on exp.CategoryId equals cat.Id
                            select new { Id = cat.Id, Name = cat.Name, Price = exp.Sum }).ToListAsync();
                info.CategoryInfo=(from ls in list
                                  group ls by ls.Id into exp
                                  select new CategoryExpenseInfoRes { 
                                      Id=exp.FirstOrDefault().Id, 
                                      CategorName=exp.FirstOrDefault().Name,
                                      ExpenseSum=exp.Sum(x => x.Price)
                                  }).ToList();

                await ChartCreator.GeneratePieChartAsBytes(info);

                if (info.Image != null)
                {
                    return info;
                }
                else
                {
                    return null;
                }



            }
            else
            {
                return null;
            }
        }
    }
}
