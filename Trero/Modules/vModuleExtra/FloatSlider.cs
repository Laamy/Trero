using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trero.Modules.vModuleExtra
{
    class FloatSlider
    {
        public string text = "FloatSlider";
        public float minValue = 0.5f;
        public float maxValue = 5f;
        public FloatSlider(string text, float minValue, float maxValue)
        {
            this.text = text;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}
