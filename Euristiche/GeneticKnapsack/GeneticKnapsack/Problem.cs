using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticKnapsack
{
    class Problem
    {
        private int indexProblem;
        private int nVariables;
        private int nConstraint;
        private double solOttima;
        private double [] coeffP;
        private double[,] coeffR;
        private double[] bound;

        /// <summary>
        /// istanza di un problema
        /// </summary>
        /// <param name="indexProblem">indice del problema, da 0 al primo numero nel file</param>
        /// <param name="nVariables">primo valore della prima riga del problema</param>
        /// <param name="nConstraint">secondo valore della prima riga del problema</param>
        /// <param name="solOttima">terzo valore della prima riga del problema</param>
        public Problem(int indexProblem, int nVariables, int nConstraint, double solOttima)
        {
            this.indexProblem = indexProblem;
            this.nVariables = nVariables;
            this.nConstraint = nConstraint;
            this.solOttima = solOttima;
            coeffP = new double [nVariables];
            coeffR = new double [nConstraint, nVariables];
            bound = new double [nConstraint];
        }

        public void setCoeffPValues(double[] coeffP)
        {
            this.coeffP = coeffP;
        }

        public void setCoeffRValues(double[,] coeffR)
        {
            this.coeffR = coeffR;
        }

        public void setBoundValues (double [] bound) {
            this.bound = bound;
        }


        public double [] getCoeffPValues()
        {
            return coeffP;
        }

        public double[,] getCoeffRValues()
        {
            return coeffR;
        }

        public double[] getBoundValues()
        {
            return bound;
        }

        public int getIndexProblem()
        {
            return indexProblem;
        }
        public int getNVariables()
        {
            return nVariables;
        }
        public int getNConstraint()
        {
            return nConstraint;
        }
        public double getSolOttima()
        {
            return solOttima;
        }
    }
}
