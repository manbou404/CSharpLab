using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaizaBench
{
    public class Question
    {
        public string Func(string input)
        {
            //var num = Convert.ToInt32(input);
            var nums = input.Split(',').Select(x => Convert.ToInt32(x));

            ;

            var ans = nums.Sum();
            return ans.ToString();
        }

        public string Func(string[] inputs)
        {
            var nums = inputs.Select(x => Convert.ToInt32(x));

            ;

            var ans = string.Join(",", nums);
            return ans.ToString();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var obj = new Question();

            var input = Console.ReadLine();
            var output = obj.Func(input);

            //var inputNum = 5;
            //var inputs = Enumerable.Repeat(Console.ReadLine(), inputNum).ToArray();
            //var output = obj.Func(inputs);


            Console.WriteLine(output);
        }
    }
}
