using System.Collections.Generic;
using System.Text;

namespace _4GenericSwapMethodIntegers
{
    public class Box<T>
    {
        private List<T> inputs;

        public Box(List<T> inputs)
        {
            this.inputs = inputs;
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            T swap = this.inputs[firstIndex];

            this.inputs[firstIndex] = this.inputs[secondIndex];
            this.inputs[secondIndex] = swap;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.inputs)
            {
                sb.AppendLine($"{item.GetType().FullName}: {item}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
