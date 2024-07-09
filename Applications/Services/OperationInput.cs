using iocXG.Applications.Interfaces;

namespace iocXG.Applications.Services
{
    public class OperationInput : IOpreration
    {
        public double Perform(double input){
            //thực hiện phép toán lũy thừa 2 của số đầu vào
            return Math.Pow(input, 2);
        }
    }
}