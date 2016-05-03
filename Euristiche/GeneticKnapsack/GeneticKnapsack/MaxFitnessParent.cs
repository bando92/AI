using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticKnapsack
{
    class MaxFitnessParent
    {
        private double maxFitnessValue;
        private bool[] maxFitnessParent;
        private int index = 0;
        private int iteration = 0;

        public MaxFitnessParent(double maxFitnessValue, bool[] maxFitnessParent, int index, int iteration)
        {
            this.maxFitnessParent = maxFitnessParent;
            this.maxFitnessValue = maxFitnessValue;
            this.index = index;
            this.iteration = iteration;
        }

        public void SetMaxFitnessValue(double maxFitnessValue)
        {
            this.maxFitnessValue = maxFitnessValue;
        }

        public void SetMaxFitnessParent(bool[] maxFitnessParent)
        {
            this.maxFitnessParent = maxFitnessParent;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public void SetIteration(int iter)
        {
            this.iteration = iter;
        }

        public double GetMaxFitnessValue()
        {
            return maxFitnessValue;
        }

        public bool[] GetMaxFitnessParent()
        {
            return maxFitnessParent;
        }

        public int GetIndex()
        {
            return index;
        }

        public int GetIteration()
        {
            return iteration;
        }
    }
}
