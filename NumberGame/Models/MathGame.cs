namespace NumberGame.Models
{
    public class MathGame
    {
        public string Operation { get; set; }  // e.g., "+"
        public int TargetResult { get; set; }  // e.g., 27
        public List<int> Numbers { get; set; }  // e.g., { 16, 11, 5, 9 }
    }
}