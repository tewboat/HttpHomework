using System;
using System.Text;

namespace HttpHomework.View
{
    public class ProgressBar
    {
        public double MaxValue { get; }
        public double CurrentValue { get; private set; }
        public int Length { get; }
        public string Text { get; }
        private double divisionValue;

        public ProgressBar(string text, double maxValue, int length = 10)
        {
            Text = text;
            MaxValue = maxValue;
            Length = length;
            divisionValue = maxValue / length;
        }

        public void DrawEmptyProgressBar()
        {
            Console.WriteLine(Text);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('[');
            stringBuilder.Append('-', Length);
            stringBuilder.Append(']');
            Console.Write(stringBuilder.ToString());
        }

        public void NotifyValueChanged(double value)
        {
            CurrentValue = value;
            var filled = (int) (value / divisionValue);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('[');
            stringBuilder.Append('#', filled);
            stringBuilder.Append('-', Length - filled);
            stringBuilder.Append(']');
            Console.Write("\r");
            Console.Write(stringBuilder.ToString());
        }
    }
}