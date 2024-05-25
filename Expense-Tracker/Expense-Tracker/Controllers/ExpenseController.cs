using Expense_Tracker.DataBase;
using Expense_Tracker.RequestJson;
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
    public class ExpenseController : ControllerBase
    {
        private UserManager<User> userManager;
        private ApplicationContext applicationContext;
        public ExpenseController(UserManager<User> userManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.applicationContext = context;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Access")]
        public async Task<List<ExpenseRes>> GetExpenses()
        {
            var userid = HttpContext.User.FindFirstValue("UserId");
            if (userid != null)
            {
                var Expenses = await (from exp in applicationContext.Expenses
                                      where exp.UserId == userid
                                      select new ExpenseRes
                                      {
                                          Id = exp.Id,
                                          Sum = exp.Sum,
                                          Description = exp.Description,
                                          Date = exp.Date,
                                          CategoryName = applicationContext.Categories.FirstOrDefault(cat => cat.Id == exp.CategoryId).Name,
                                          CategoryId=exp.CategoryId
                                      }).ToListAsync();
                return Expenses;
            }
            return null;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Access")]
        public async Task<IActionResult> AddExpense(ExpenseReq expenseRes)
        {
            var userid = HttpContext.User.FindFirstValue("UserId");
            Console.WriteLine(userid);
            if (userid != null)
            {
                var result = await applicationContext.Expenses.AddAsync(new Expense
                {
                    Sum = expenseRes.Sum,
                    Description = expenseRes.Description,
                    Date = new DateOnly(expenseRes.Year,expenseRes.Month,expenseRes.Day),
                    UserId = userid,
                    CategoryId = applicationContext.Categories.FirstOrDefault(cat => cat.Id == expenseRes.CategoryId).Id
                });
                Console.WriteLine(result);
                if (result != null)
                {
                    applicationContext.SaveChanges();
                    return Ok();
                }
                return BadRequest("Ошибка на стороне сревера");
            }
            else
            {
                return BadRequest("Непредвиденная ошибка");
            }

        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Access")]
        public async Task<IActionResult> DeleteExpense(int expenseid)
        {
            var userid = HttpContext.User.FindFirstValue("UserId");
            if (userid != null)
            {
                var exp = await applicationContext.Expenses.FirstOrDefaultAsync(exp => exp.Id == expenseid);
                if (exp != null)
                {
                    var result = applicationContext.Expenses.Remove(exp);
                    if (result != null)
                    {
                        applicationContext.SaveChanges();
                        return Ok();
                    }

                }
                return BadRequest("Ошибка на стороне сревера");
            }
            else
            {
                return BadRequest("Непредвиденная ошибка");
            }

        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Access")]
        public async Task<List<ExpenseRes>> GetExpensesByCategory(int categoryid)
        {
            var userid = HttpContext.User.FindFirstValue("UserId");
            if (userid != null)
            {
                var Expenses = await (from exp in applicationContext.Expenses
                                      where exp.UserId == userid
                                      select new ExpenseRes
                                      {
                                          Id = exp.Id,
                                          Sum = exp.Sum,
                                          Description = exp.Description,
                                          Date = exp.Date,
                                          CategoryName = applicationContext.Categories.FirstOrDefault(cat => cat.Id == exp.CategoryId).Name,
                                          CategoryId=exp.CategoryId
                                      }).ToListAsync();
                Expenses=Expenses.Where(exp=>exp.CategoryId == categoryid).ToList();
                return Expenses;
            }
            return null;
        }
    }
}
