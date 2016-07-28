using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services
{
    public class Async
    {
        public Task<double> GetValueAsync(double num1, double num2)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }
        public async void DisplayValue()
        {
            double result = await GetValueAsync(1234.5, 1.01);
            System.Diagnostics.Debug.WriteLine("Value is : " + result);
        }
    }
}
