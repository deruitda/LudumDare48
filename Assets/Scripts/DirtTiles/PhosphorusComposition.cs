using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    class PhosphorusComposition : ISoilComposition
    {
        [SerializeField]
        private int _nutritionalValue;

        public PhosphorusComposition(int nutritionalValue)
        {
            _nutritionalValue = nutritionalValue;
        }

        public int GetNutritionalValue()
        {
            return _nutritionalValue;
        }

}
