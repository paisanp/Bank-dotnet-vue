using BankAPI.Models;
using BankAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly DBContext _context;

        public BankController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<User>> GetAllUser()
        {
            try
            {
                return _context.User.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<UserRespone> GetUserById(Guid id)
        {
            try
            {
                var user = _context.User.FirstOrDefault(u => u.UserId == id);

                if (user == null)
                {
                    return NotFound();
                }
                var userResponse = new UserRespone
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    CurrentBalance = user.CurrentBalance,
                };

                return userResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Deposit(DepositAndWithDrawModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                if (model.amount <= 0)
                {
                    return Ok(new { message = "invalid deposit amount", status = 200 });

                }

                var user = _context.User.FirstOrDefault(u => u.UserId == model.UserId);

                if (user != null)
                {
                    user.CurrentBalance = user.CurrentBalance + model.amount;
                    _context.SaveChanges();
                    return Ok(new { message = "deposit successfully", status = 200 });

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult WithDraw(DepositAndWithDrawModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                if (model.amount <= 0)
                {
                    return Ok(new { message = "invalid withdraw amount", status = 200 });

                }
                var user = _context.User.FirstOrDefault(u => u.UserId == model.UserId);

                if (user != null)
                {
                    if (user.CurrentBalance < model.amount)
                    {
                        return Ok(new { message = "Insufficient balance", status = 200 });

                    }

                    user.CurrentBalance = user.CurrentBalance - model.amount;
                    _context.SaveChanges();
                    return Ok(new { message = "withdraw successfully", status = 200 });

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Tranfer(TranferwModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                if (model.amount <= 0)
                {
                    return Ok(new { message = "invalid tranfer amount", status = 200 });

                }
                var user = _context.User.FirstOrDefault(u => u.UserId == model.UserId);

                var receiver = _context.User.FirstOrDefault(u => u.UserId == model.ReceiverId);

                if (user != null && receiver != null)
                {
                    if (user.CurrentBalance < model.amount)
                    {
                        return Ok(new { message = "Insufficient balance", status = 200 });
                    }
                    //สร้าง log สำหรับ transaction
                    Transaction tranSender = new Transaction
                    {
                        TransactionId = Guid.NewGuid(),
                        Type = "S",
                        SenderId = user.UserId,
                        receiverId = receiver.UserId,
                        Amount = model.amount.ToString(),
                        Remain = (user.CurrentBalance - model.amount).ToString(),
                        Date = DateTime.Now
                    };
                    Transaction tranReceive = new Transaction
                    {
                        TransactionId = Guid.NewGuid(),
                        Type = "R",
                        SenderId = user.UserId,
                        receiverId = receiver.UserId,
                        Amount = model.amount.ToString(),
                        Remain = (receiver.CurrentBalance + model.amount).ToString(),
                        Date = DateTime.Now
                    };

                    _context.Transaction.Add(tranSender);
                    _context.Transaction.Add(tranReceive);

                    //หักเงินจริงๆ
                    user.CurrentBalance = user.CurrentBalance - model.amount;
                    receiver.CurrentBalance = receiver.CurrentBalance + model.amount;

                    _context.SaveChanges();
                    return Ok(new { message = "tranfer successfully", status = 200 });

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<Transaction>> GetHistoryTransaction(Guid UserId, string type)
        {
            try
            {
                if (UserId == null || string.IsNullOrEmpty(type))
                {
                    return BadRequest();
                }

                var user = _context.User.FirstOrDefault(u => u.UserId == UserId);

                if (user == null)
                {
                    return NotFound();
                }

                List<Transaction> history;

                if (type == "S")
                {
                    history = _context.Transaction.Where(u => u.SenderId == UserId && u.Type == "S").ToList();
                }
                else
                {
                    history = _context.Transaction.Where(u => u.receiverId == UserId && u.Type == "R").ToList();
                }
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
