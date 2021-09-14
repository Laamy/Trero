namespace Trero.Modules.vModuleExtra
{
    internal class FloatSlider
    {
        public float maxValue = 5f;
        public float minValue = 0.5f;
        public string text = "FloatSlider";

        public FloatSlider(string text, float minValue, float maxValue)
        {
            this.text = text;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}