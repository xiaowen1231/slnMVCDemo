using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMauiDemo.Models
{
    public class CLotto
    {
        public string getNumber()
        {

            HashSet<int> numbers = new HashSet<int>();

            Random rnd = new Random();

            while (numbers.Count<6)
            {
                numbers.Add(rnd.Next(1,50));
            }
            
            var newarr = numbers.ToArray();


            for (int i = newarr.Length-1; i >0; i--)
            {
                for (int j = 0; j < i ; j++)
                {
                    if (newarr[j] > newarr[j + 1])
                    {
                        int big = newarr[j];
                        newarr[j] = newarr[j + 1];
                        newarr[j+1] = big;
                    }
                }
            }

            string result = string.Empty;

            foreach(int i in newarr)
            {
                result += i+" ";
            }

            return result;
        }

        private bool is在陣列中已有此隨機數(int temp, int[] numbers)
        {
            foreach(int i in numbers)
            {
                if(temp == i)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
