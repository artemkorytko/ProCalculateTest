using System;
using System.Collections.Generic;


namespace ProCalculate.Calculator
{
    [Serializable]
    public class StorageState
    {
        public string CurrentExpression;
        public List<string> History = new List<string>();
    }
}