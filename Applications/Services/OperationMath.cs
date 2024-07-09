using iocXG.Applications.Interfaces;

namespace iocXG.Applications.Services
{
    public class OperationMath : IOpreration
    {
        public double Perform(double input){
            //  thực hiện phép toán căn bậc 2 và lấy phần nguyên của số đầu vào
            return Math.Floor(Math.Sqrt(input));
        }
        
    }
}