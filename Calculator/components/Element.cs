namespace Calculator
{
    public class Element
    {
        public decimal? Digit { get; set; } = null;
        public Type Object { get; set; }
        public int Priority { get; set; }
    }
}
