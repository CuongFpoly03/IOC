using iocXG.Applications.Services;
using Microsoft.AspNetCore.Mvc;

namespace iocXG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        private static string numberFilePath = "number.txt";
        private static string logAllFilePath = "progess_logAll.txt";
        private static int CountProces = 0;

        private readonly IServiceProvider _serviceProvider;


        public MathController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("{getNumber}")]
        public ActionResult<double> getNumer()
        {
            //ktra xem file có....
            if (!System.IO.File.Exists(numberFilePath))
            {
                return NotFound(new { message = "not found file number.txt" });
            }
            //đọc số từ file 
            double number = Convert.ToDouble(System.IO.File.ReadAllText(numberFilePath));
            return Ok(new { message = "ok", number = number });
        }

        [HttpPost("{updateNumber}")]
        public ActionResult updateNumber()
        {
            //ktra xem file có....
            if (!System.IO.File.Exists(numberFilePath))
            {
                return NotFound(new { message = "not found file number.txt" });
            }

            CountProces++; //Tăng bộ đêm theo thứ tự
            double number = Convert.ToDouble(System.IO.File.ReadAllText(numberFilePath));
            double newNumber;
            if (CountProces % 2 != 0)
            {
                //use services singleton
                var operation = _serviceProvider.GetService<OperationInput>();
                if (operation == null)
                {
                    return NotFound(new { message = "not found service" });
                }
                newNumber = operation.Perform(number);
            }
            else
            {
                //use services scoped
                var operation = _serviceProvider.GetService<OperationMath>();
                if (operation == null)
                {
                    return NotFound(new { message = "not found service" });
                }
                newNumber = operation.Perform(number);
            }

            //giờ với ghi số mới vào file ...!
            System.IO.File.WriteAllText(numberFilePath, newNumber.ToString());
            LogProcess(number, newNumber); // Ghi lại log
            return Ok();
        }
        
        // Ghi log quá trình
        private void LogProcess(double before, double after)
        {
            string logEntry = $"Seq_{CountProces} [{before}] [{after}]\n";
            System.IO.File.AppendAllText(logAllFilePath, logEntry);
        }
        [HttpGet("getLog")]
        public ActionResult<string> GetLog()
        {
            // Kiểm tra file log tồn tại
            if (!System.IO.File.Exists(logAllFilePath))
            {
                return NotFound("Log file not found.");
            }

            // Đọc log từ file
            string logContent = System.IO.File.ReadAllText(logAllFilePath);
            return Ok(logContent); // Trả về nội dung log
        }

        [HttpPost("reset")]
        public ActionResult Reset()
        {
            // Đặt lại số về 1 và xóa log
            System.IO.File.WriteAllText(numberFilePath, "1");
            System.IO.File.WriteAllText(logAllFilePath, string.Empty);
            CountProces = 0; // Đặt lại bộ đếm

            return Ok(new { message = "reset ok" });
        }



    }
}