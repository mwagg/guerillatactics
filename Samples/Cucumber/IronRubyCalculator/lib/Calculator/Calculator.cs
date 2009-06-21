using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class Calculator
    {
        private List<int> _numbers = new List<int>();
        private int _result;

        public void EnterNumber(int number)
        {
            _numbers.Add(number);
        }

        public void Add()
        {
            _result = 0;
            foreach (var number in _numbers)
            {
                _result += number;
            }
        }

        public int Result()
        {
            return _result;
        }
    }
}
