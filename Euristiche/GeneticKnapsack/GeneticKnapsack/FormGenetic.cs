using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticKnapsack
{
    public partial class FormGenetic : Form
    {
        public FormGenetic()
        {
            InitializeComponent();
        }

        Problem[] problems;
        List<Point> acoPoint = new List<Point>();
        List<Point> geneticPoint = new List<Point>();

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "TextFiles (.txt)|*.txt";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = "D:\\Università\\AI\\_PROGETTI\\#4";

            

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(filename);
                string src;

                bool flagReadParams = false;
                int nProblems = int.Parse(sr.ReadLine()); //salvo il numemo di problemi
                int problemCounter = 0;
                int rowCounter = 0;
                int valuesPerRowCounter = 0;
                
                int valuesPerRow = 0;
                int totalRows = 0;
                double optimalSolution = 0;

                double[,] mat = null;
                double[] totalRowValues = null;

                problems = new Problem[nProblems];



                do      //la lettura avviene riga per riga
                {

                    src = sr.ReadLine();
                   
                    string[] stringOfValuesInFile = src.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    double[] problemData = new double[stringOfValuesInFile.Length];
                   
                    if (stringOfValuesInFile[0] == "%")   //se abbiamo %, significa che sta per iniziare un nuovo problema
                        flagReadParams = true;
                    else if (flagReadParams)
                    {
                        valuesPerRow = int.Parse(stringOfValuesInFile[0]);
                        totalRows = int.Parse(stringOfValuesInFile[1]);
                        optimalSolution  = Convert.ToDouble(stringOfValuesInFile[2], new CultureInfo("en-US"));
                        
                        problems[problemCounter] = new Problem (problemCounter, valuesPerRow, totalRows, optimalSolution);
                        //Console.Write("\n__PAR: ["+problemCounter+"] "+ valuesPerRow+" "+ totalRows+" "+ optimalSolution+"\n");

                        mat = new double[totalRows, valuesPerRow];
                        totalRowValues = new double[valuesPerRow];

                        flagReadParams = false;
                        rowCounter++;
                    }

                    else if (rowCounter == totalRows + 2) // stiamo studiando l'ultima riga, ovvero la riga del bound
                    {
                        problems[problemCounter].setCoeffRValues(mat); //setto i coefficienti R, ovvero la matrice dei coefficienti dei vincoli

                        rowCounter = 0;
                        problems[problemCounter].setBoundValues(stringOfValuesInFile.Select(n => Convert.ToDouble(n)).ToArray());

                        //Console.Write("bounds\n");
                        for (int n = 0; n < stringOfValuesInFile.Length; n++)
                        {
                            problemData[n] = Convert.ToDouble(stringOfValuesInFile[n], new CultureInfo("en-US"));
                            //Console.Write(problemData[n] + " ");
                        }
                        problemCounter++;
                    }
                    else //se siamo in una riga di valori, leggiamo i dati
                    {
                        //riga dei coefficenti di Peso p[j]
                        if (rowCounter == 1)
                        {    
                            for (int n = 0; n < stringOfValuesInFile.Length; n++)
                            {
                                problemData[n] = Convert.ToDouble(stringOfValuesInFile[n], new CultureInfo("en-US"));
                                //Console.Write( problemData[n] + "| ");
                                totalRowValues[valuesPerRowCounter] = Convert.ToDouble(stringOfValuesInFile[n], new CultureInfo("en-US"));
                                valuesPerRowCounter++;
                            }

                            if (valuesPerRowCounter == valuesPerRow)
                            {
                                problems[problemCounter].setCoeffPValues(totalRowValues);

                            }
                            
                        }
                        else
                        {
                            for (int n = 0; n < stringOfValuesInFile.Length; n++)
                            {
                                problemData[n] = Convert.ToDouble(stringOfValuesInFile[n], new CultureInfo("en-US"));

                                mat[rowCounter - 2, valuesPerRowCounter] = problemData[n];
                                //Console.Write(mat[rowCounter - 2, valuesPerRowCounter] + " ");
                                valuesPerRowCounter++;
                            }
                        }

                        //il file va a capo ogni tot valori per leggibilità.
                        //Quindi rowcounter non deve aumentare se il file va a capo per mere questioni di leggibilità
                        //La riga è finita solo se il numero di valori letto corrisponde al numero di valori per riga
                        if (valuesPerRowCounter >= valuesPerRow)
                        {
                            rowCounter++;
                            valuesPerRowCounter = 0;
                            //Console.Write("\n");
                        }
                    }

                }  while (!sr.EndOfStream);

                sr.Close();
                Console.Write("\nFile readed correctly.");
                InitializeFormAfterLoading(nProblems, filename);
            }
        }

        private void InitializeFormAfterLoading(int nProblems, string filename)
        {
            lblFilename.Text = filename;
            cmbBoxProblems.Enabled = true;
            for (int i = 1; i <= nProblems; i++ )
                cmbBoxProblems.Items.Add("" + i);
            cmbBoxProblems.SelectedIndex = 0;
            btnStart.Enabled = true;
            buttonPlot.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int problemIndex = cmbBoxProblems.SelectedIndex;
            double time = 0;
            bool iterValueOk = true;
            int nIter = 0;
            try
            {
                nIter = int.Parse(txtIter.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Inserisci un numero intero.", "Errore",MessageBoxButtons.OK, MessageBoxIcon.Error);
                iterValueOk = false;
            }

            if (iterValueOk)
            {
#region initialization

                outputPrev.Text = "0";
                outputOtt.Text = "0";
                lblTimeElapsed.Text = "0 ms";
                lblIter.Text = "0";

                int N_PARENTS = 30;
                Problem problem = problems[problemIndex];
                int nVariable = problem.getNVariables();
                bool[,] population = new bool[N_PARENTS, nVariable];
                double[] fitnessValues = new double[N_PARENTS];
                double[] coeffP = new double[nVariable];    //pesi
                coeffP = problem.getCoeffPValues();
                int[] selectedParentIndex = new int[2];
                bool[][] selectedParentGenes = new bool[2][];
                bool[] son = new bool[nVariable];
                double sonFitness = 0;
                int minFitnessIndex = 0;

                int nBounds = problem.getNConstraint();
                int nAnts = 30;
                var coeffR = problem.getCoeffRValues();
                var boundValues = problem.getBoundValues();
                double tMin = 0.01;
                double tMax = 5;
                double alpha = 2;
                double beta = 5;
                double rho = 0.98; //fattore di evaporazione (da 0 a 1)
                bool[,] matAnts = new bool[nAnts, nVariable];
                double[] trails = new double[nVariable];
                bool[] feasibleAnts = new bool[nVariable];
                List<int> candidateIndexes = new List<int>();
                List<int> candidateCopy = new List<int>();
                List<double> candidateProbabilities = new List<double>();
                double[] fitnessAnts = new double[nAnts];
                int selectedIndex = 0;
                int t = 0;
                double maxFintessNow = 0;
                int maxIndex = -1;
                bool[] ant = new bool[nVariable];
                double [] heuristicFactor = new double[nVariable];
                MaxFitnessParent m = new MaxFitnessParent(maxFintessNow, ant, maxIndex, 0);

                outputPrev.Text = problem.getSolOttima().ToString();
               
                Console.Write("\nStarted...\n");
#endregion

#region genetic
                if (rdbGen.Checked)
                {
                    geneticPoint.Clear();
                    //inizializzo la popolazione
                    
                    InitializePopulation(N_PARENTS, nVariable, problem, ref population);

                    time = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

                    //calcola il fitness iniziale della popolazione
                    fitnessValues = EvaluateFitness(N_PARENTS, nVariable, population, coeffP);

                    //prendo il genitore con il valore di fitness più alto
                    m = FindMaxFitnessParent(population, fitnessValues, nVariable);
                    
                    do
                    {
                        Console.WriteLine("Iterazione " + t);
                        //estrazione montecarlo
                        selectedParentIndex = MontecarloExtraction(fitnessValues, N_PARENTS);
                        
                        //altro metodo: Tournament selection con t=2
                        //selectedParentIndex = TournamentSelection(fitnessValues, N_PARENTS, 2);
                        
                        //dopo aver selezionato 2 indici casuali prendo i loro rispettivi geni
                        selectedParentGenes[0] = new bool[nVariable];
                        selectedParentGenes[1] = new bool[nVariable];
                        Buffer.BlockCopy(population, sizeof(bool) * nVariable * selectedParentIndex[0], selectedParentGenes[0], 0, sizeof(bool) * nVariable);
                        Buffer.BlockCopy(population, sizeof(bool) * nVariable * selectedParentIndex[1], selectedParentGenes[1], 0, sizeof(bool) * nVariable);

                        //generato figlio con crossover
                        son = Crossover(selectedParentGenes, nVariable);
                        
                        //mutato il figlio generato
                        son = Mutate(son, nVariable);
                        
                        //riparo il figlio per renderlo feasible solo se non è già feasible
                        if (!IsFeasible(son, nVariable, problem.getBoundValues(), problem.getCoeffRValues()))
                        {
                            son = Repair(son, nVariable, problem.getBoundValues(), problem.getCoeffRValues());
                        }
                        
                        //valuto la fitness del figlio
                        sonFitness = EvaluateFitness(son, nVariable, problem.getCoeffPValues());
                        
                        //trovo l'indice del genitore con il minor fitness value
                        minFitnessIndex = fitnessValues.ToList().IndexOf(fitnessValues.Min());
                        
                        //rimpiazzo l'utimo con il figlio (se il figlio ha fitness maggiore)
                        if (sonFitness > fitnessValues[minFitnessIndex])
                        {
                            for (int j = 0; j < nVariable; j++)
                                population[minFitnessIndex, j] = son[j];

                            //se il figlio ha fitness maggiore del maxFitnessParent lo sostituisco
                            if (sonFitness > m.GetMaxFitnessValue())
                            {
                                m.SetIndex(minFitnessIndex);
                                m.SetMaxFitnessValue(sonFitness);
                                m.SetMaxFitnessParent(son);
                                m.SetIteration(t);
                            }
                        }

                        if (problem.getSolOttima() != 0)
                            if (m.GetMaxFitnessValue() >= problem.getSolOttima())
                                break;
                        t++; //incremento il numero di iterazioni
                        geneticPoint.Add(new Point(t, (int)sonFitness));
                    } while (t < nIter);

                }
#endregion

#region ACO
                else
                {
                    acoPoint.Clear();
                    
                    for (int i = 0; i < nAnts; i++)
                        for (int j = 0; j < nVariable; j++)
                            matAnts[i, j] = false;
                    
                    for (int i = 0; i < nVariable; i++)
                        trails[i] = tMax;

                    int rand = 0, prevRand = -1;
                    time = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

                    do
                    {
                        Console.WriteLine("\nIter: " + t);
                        for (int k = 0; k < nAnts; k++)
                        {
                            candidateIndexes.Clear();
                            candidateProbabilities.Clear();
                            do
                                rand = new Random().Next(nVariable);
                            while (prevRand == rand);
                            prevRand = rand;
                            matAnts[k, rand] = true;
                            Buffer.BlockCopy(matAnts, sizeof(bool) * nVariable * k, feasibleAnts, 0, sizeof(bool) * nVariable);
                            
                            for (int i = 0; i < nVariable; i++) //aggiungo i candidati
                            {
                                if (!feasibleAnts[i])
                                {
                                    feasibleAnts[i] = true;
                                    if (IsFeasible(feasibleAnts, nVariable,problem.getBoundValues(), problem.getCoeffRValues()) )
                                    {
                                        candidateIndexes.Add(i);
                                    }
                                    feasibleAnts[i] = false;
                                }
                            }
                            
                            while (candidateIndexes.Count > 0)
                            {
                                heuristicFactor = HeurusticFactor(coeffR, boundValues, coeffP, nVariable, nBounds, feasibleAnts);
                                double sum = 0;
                                foreach (int cand in candidateIndexes) 
                                {
                                    sum += ( Math.Pow(trails[cand], alpha) * Math.Pow(heuristicFactor[cand],beta) );
                                }

                                foreach (int cand in candidateIndexes)
                                {
                                    double numerator =  Math.Pow(trails[cand], alpha) * Math.Pow(heuristicFactor[cand],beta);
                                    candidateProbabilities.Add(numerator / sum);
                                }

                                selectedIndex = MontecarloSingleExtraction(candidateProbabilities, candidateProbabilities.Count);
                                
                                feasibleAnts[candidateIndexes.ElementAt(selectedIndex)] = true;

                                candidateIndexes.RemoveAt(selectedIndex);
                                candidateProbabilities.Clear();

                                if (candidateIndexes.Count > 0)
                                {
                                    foreach (int index in candidateIndexes)
                                    {
                                        
                                        if (!feasibleAnts[index])
                                        {
                                            candidateCopy.Add(index);
                                            
                                            feasibleAnts[index] = true;
                                            
                                            if (!IsFeasible(feasibleAnts, nVariable, problem.getBoundValues(), problem.getCoeffRValues()))
                                            {
                                                candidateCopy.Remove(index);
                                            }
                                            feasibleAnts[index] = false;
                                        }
                                    }
                                   
                                    candidateIndexes.Clear();
                                    candidateIndexes.AddRange(candidateCopy);
                                    candidateCopy.Clear();

                                }
                            }
                            Buffer.BlockCopy(feasibleAnts, 0, matAnts, sizeof(bool) * nVariable * k, sizeof(bool) * nVariable);
                        }

                        fitnessAnts = EvaluateFitness(nAnts, nVariable, matAnts, coeffP);
                        maxFintessNow = fitnessAnts.Max();
                        ant = new bool[nVariable];
                        maxIndex = fitnessAnts.ToList().IndexOf(maxFintessNow);
                        Buffer.BlockCopy(matAnts, sizeof(bool) * nVariable * maxIndex, ant, 0, sizeof(bool) * nVariable);
                        
                        if (maxFintessNow > m.GetMaxFitnessValue())
                        {
                            m.SetIndex(maxIndex);
                            m.SetMaxFitnessParent(ant);
                            m.SetMaxFitnessValue(maxFintessNow);
                            m.SetIteration(t);
                        }

                        trails = updatePheromoneTrails(trails, m.GetMaxFitnessValue(), maxFintessNow, rho, ant, tMin, tMax);

                        if (problem.getSolOttima() != 0)
                            if (m.GetMaxFitnessValue() >= problem.getSolOttima())
                                break;
                        t++; //incremento il numero di iterazioni
                        for (int i = 0; i < nAnts; i++)
                            for (int j = 0; j < nVariable; j++)
                                matAnts[i, j] = false;
                        acoPoint.Add(new Point(t, (int)maxFintessNow));
                    } while (t < nIter);

                }
#endregion
                lblTimeElapsed.Text = ((int)(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds - time)).ToString() + " ms";
                outputOtt.Text = m.GetMaxFitnessValue().ToString();
                lblIter.Text = t.ToString();
                Console.WriteLine("\nrisultato:");
                Console.Write("Miglior membro della popolazione: \n");
                for (int j = 0; j < nVariable; j++)
                {
                    Console.Write(" " + Convert.ToInt32(m.GetMaxFitnessParent()[j]));
                }
                Console.Write("\nAll'iterazione " + m.GetIteration() + " e fitness " + m.GetMaxFitnessValue());
            }
        }

        private double [] updatePheromoneTrails(double[] trails, double maxFitnessValue, double maxFintessNow,
            double rho, bool [] ant, double tMin, double tMax)
        {
            for (int i = 0; i < trails.Length; i++)
            {
                trails[i] *= rho;
                if (ant[i])
                {
                    trails[i] += (1 / (1 + maxFitnessValue - maxFintessNow));
                }
                if (trails[i] < tMin)
                    trails[i] = tMin;
                if (trails[i] > tMax)
                    trails[i] = tMax;
            }
            return trails;
        }

        private bool IsFeasible(bool[] element, int nVariable, double[] boundValues, double[,] coeffR)
        {
            double[] r = new double[boundValues.Length];
            for (int i = 0; i < boundValues.Length; i++)
            {
                r[i] = 0;
                for (int j = 0; j < nVariable; j++)
                {
                    if (element[j])
                        r[i] += coeffR[i, j];
                }
            }
            for (int i = 0; i < boundValues.Length; i++)
            {
                if (r[i] > boundValues[i])
                    return false;
            }
            return true;
        }

        private bool[] Repair(bool[] son, int nVariable, double[] boundValues, double[,] coeffR)
        {
            double[] r = new double[boundValues.Length];
            bool rOverBound = false;
            //initialize
            for (int i = 0; i < boundValues.Length; i++)
            {
                r[i] = 0;
                for (int j = 0; j < nVariable; j++)
                {
                    if(son[j])
                        r[i] += coeffR[i,j];
                }
            }
            //drop phase
            for (int j = nVariable - 1; j >= 0; j--)
            {
                rOverBound = false;
                for (int i = 0; i < boundValues.Length; i++)
                {
                    if (r[i] > boundValues[i])
                        rOverBound = true;
                }
                if(rOverBound && son[j])
                {
                    son[j] = false;
                    for (int i = 0; i < boundValues.Length; i++)
                    {
                        r[i] -= coeffR[i, j];
                    }
                }
            }
            rOverBound = false;
            //add phase
            for (int j = 0; j < nVariable; j++)
            {
                for (int i = 0; i < boundValues.Length; i++)
                {
                    if (r[i] + coeffR[i, j] > boundValues[i])
                        rOverBound = true;
                }
                if (!rOverBound && !son[j])
                {
                    son[j] = true;
                    for (int i = 0; i < boundValues.Length; i++)
                    {
                        r[i] += coeffR[i, j];
                    }
                }
            }
            return son;
        }

        private bool[] Mutate(bool[] son, int nVariable)
        {
            int rand1 = 0, rand2 = 0;
            rand1 = new Random().Next(0, nVariable);
            do
                rand2 = new Random().Next(0, nVariable);
            while (rand2 == rand1);
            son[rand1] = !son[rand1];
            son[rand2] = !son[rand2];
            return son;
        }

        private bool[] Crossover(bool[][] selectedParentGenes, int nVariable)
        {
            bool[] son = new bool[nVariable];
            double rand = 0;
            List<double> exitNumber = new List<double>();
            for (int i = 0; i < nVariable; i++)
            {
                do
                    rand = new Random().NextDouble();
                while(exitNumber.Contains(rand));
                exitNumber.Add(rand);
                if (rand > 0.5)
                    son[i] = selectedParentGenes[0][i];
                else
                    son[i] = selectedParentGenes[1][i];
            }
            return son;
        }

        private int[] TournamentSelection(double[] fitnessValues, int N_PARENTS, int t)
        {
            int[] selectedParentIndex = new int[2];
            int[] pool1 = new int[t];
            int[] pool2 = new int[t];
            int rand = 0;
            for (int i = 0; i < t; i++)
            {
                pool1[i] = -1;
                pool2[i] = -1;
            }
            for (int i = 0; i < t; i++)
            {
                do
                    rand = new Random().Next(0, N_PARENTS);
                while (pool1.Contains(rand) || pool2.Contains(rand));
                pool1[i] = rand;
                
                do
                    rand = new Random().Next(0, N_PARENTS);
                while (pool1.Contains(rand) || pool2.Contains(rand));
                pool2[i] = rand;
                
            }
            //max del pool1
            selectedParentIndex[0] = pool1.First( x => fitnessValues[x] == pool1.Max(x1 => fitnessValues[x1]));
            //max del pool2
            selectedParentIndex[1] = pool2.First( x => fitnessValues[x] == pool2.Max(x1 => fitnessValues[x1]));
            return selectedParentIndex;
        }

        private int[] MontecarloExtraction(double[] fitnessValues, int N_PARENTS)
        {
            int[] selectedParentIndex = new int[2];
            double sum = 0;
            double partialSum = 0;
            double rand = 0;
            for (int i = 0; i < N_PARENTS; i++)
            {
                sum += fitnessValues[i];
            }
            //primo indice
            rand = new Random().Next(0, 100)*sum/100;
            partialSum = 0;
            for (int i = 0; i < N_PARENTS; i++)
            {
                partialSum += fitnessValues[i];
                if (partialSum >= rand)
                {
                    selectedParentIndex[0] = i;
                    break;
                }
            }
            //secondo indice(diverso dal primo)
            rand = new Random().Next(0, 100) * sum / 100;
            partialSum = 0;
            for (int i = 0; i < N_PARENTS; i++)
            {
                partialSum += fitnessValues[i];
                if (partialSum >= rand && selectedParentIndex[0] != i)
                {
                    selectedParentIndex[1] = i;
                    break;
                }
            }
            return selectedParentIndex;
        }

        private int MontecarloSingleExtraction(List<double> probabilities, int nCandidates)
        {
            double sum = 0;
            double partialSum = 0;
            double rand = 0;
            int pos = 0;
            foreach (double prob in probabilities)
            {
                sum += prob;
            }
            //indice
            rand = new Random().Next(0, 100) * sum / 100;
            partialSum = 0;

            foreach (double prob in probabilities)
            {
                partialSum += prob;
                if (partialSum >= rand)
                {
                    pos = probabilities.IndexOf(prob);
                    break;
                }
            }
            return pos;
        }

        private MaxFitnessParent FindMaxFitnessParent(bool[,] population, double[] fitnessValues, int nVariable)
        {
            bool[] maxFitnessVector = new bool[nVariable];
            double maxFitnessValue = 0;
            int maxIndex = 0;
            for (int i = 0; i < fitnessValues.Length; i++ )
            {
                if (i == 0)
                {
                    Buffer.BlockCopy(population, 0, maxFitnessVector, 0, sizeof(bool) * nVariable);
                    maxFitnessValue = fitnessValues[0];
                    maxIndex = 0;
                }
                else
                    if (fitnessValues[i] >= maxFitnessValue)
                    {
                        maxFitnessValue = fitnessValues[i];
                        Buffer.BlockCopy(population, sizeof(bool) * nVariable * i, maxFitnessVector, 0, sizeof(bool) * nVariable);
                        maxIndex = i;
                    }
            }
            MaxFitnessParent maxFitnessParent = new MaxFitnessParent(maxFitnessValue, maxFitnessVector, maxIndex, 0);
            return maxFitnessParent;
        }

        private double[] EvaluateFitness(int N_PARENTS, int nVariable, bool[,] population, double[] coeffP)
        {
            double[] fitnessValues = new double[N_PARENTS];
            for (int i = 0; i < N_PARENTS; i++)
            {
                fitnessValues[i] = 0;
                for (int j = 0; j < nVariable; j++)
                {
                    if(population[i,j])
                        fitnessValues[i] = fitnessValues[i] + coeffP[j];
                }
            }
            return fitnessValues;
        }

        private double EvaluateFitness(bool[] genes, int nVariable, double[] coeffP)
        {
            double fitnessValue = 0;
            for (int j = 0; j < nVariable; j++)
            {
                if (genes[j])
                    fitnessValue += coeffP[j];
            }
            return fitnessValue;
        }

        private void InitializePopulation(int N_PARENTS, int nVariable, Problem problem, ref bool[,] population)
        {
            int t = nVariable;
            int nConstraint = problem.getNConstraint();
            double[] r = new double[nConstraint];
            double[] rCopy = new double[nConstraint];
            List<int> exitIndexes = new List<int>();
            int rand = 0;
            bool flag, exitBefore = true;
            //inizializzo la popolazione
            for (int k = 0; k < N_PARENTS; k++)
            {
                flag = true;
                exitIndexes.Clear();
                //inizializzo il singolo genitore con tutti 0
                for (int j = 0; j < nVariable; j++)
                {
                    population[k,j] = false;
                    exitIndexes.Add(j);
                }
                //inizializzo il vettore r(per rendere la stringa feasible)
                for (int i = 0; i < nConstraint; i++)
                {
                    r[i] = 0;
                    rCopy[i] = 0;
                }
                //al primo giro seleziono un indice di variabile a caso da trasformare in 1
                rand = new Random().Next(0, nVariable);
                //controllo se posso trasformare quella variabile in 1
                for (int i = 0; i < nConstraint; i++)
                {
                    r[i] = r[i] + problem.getCoeffRValues()[i, rand];
                    if (r[i] > problem.getBoundValues()[i])
                    {
                        flag = false;
                        r = rCopy;
                        break;
                    }
                }
                rCopy = r;
                exitIndexes.Remove(rand);
                //finchè riesco a soddisfare i vincoli continuo a cambiare un valore selezionando un indice casualmente
                while (flag)
                {
                    population[k, rand] = true;
                    //seleziono un indice che ancora non ho estratto; se li ho già estratti tutti(exitnumbers) esco da tutto
                    do
                    {
                        rand = new Random().Next(0, nVariable);
                        if(exitIndexes.Contains(rand))
                        {//se lo contiene vuol dire che il valore è ok
                            exitIndexes.Remove(rand);
                            exitBefore = false;
                            //imposto flag controllando se l'indice scelto va bene oppure no
                            for (int i = 0; i < nConstraint; i++)
                            {
                                r[i] = r[i] + problem.getCoeffRValues()[i, rand];
                                if (r[i] > problem.getBoundValues()[i])
                                {
                                    flag = false;
                                    r = rCopy;
                                    break;
                                }
                            }
                            rCopy = r;
                        }
                        else
                        {
                            exitBefore = true;
                            //se ho finito gli elementi da cambiare
                            if(exitIndexes.Count == 0)
                            {
                                flag = false;
                            }
                        }
                    } while(exitBefore);
                }
            }
        }

        private double[] HeurusticFactor(double [,] coeffR, double[] boundValues, double [] coeffP, 
            int nVariable, int nBounds, bool [] ants)
        {
            double [] heuristicFactor = new double[nVariable];
            double [] consumedQuantity = new double[nBounds];
            double [] remainedCapacity = new double[nBounds];
            double [] tightness = new double[nVariable];
            for (int i = 0; i < nBounds; i++)
            {
                consumedQuantity[i] = 0;
                for (int g = 0; g < nVariable; g++)
                {
                    if (ants[g])
                    {
                        consumedQuantity[i] += coeffR[i, g];
                    }
                }
                remainedCapacity[i] = boundValues[i] - consumedQuantity[i];
            }

            for (int j = 0; j < nVariable; j++)
            {
                tightness[j] = 0;
                for (int i = 0; i < nBounds; i++)
                {
                    tightness[j] += (coeffR[i, j] / remainedCapacity[i]);
                }
                heuristicFactor[j] = coeffP[j] / tightness[j];
            }
            return heuristicFactor;
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            if(acoPoint.Count > 0)
            {
                foreach(Point point in acoPoint)
                {
                    chart1.Series["ACO"].Points.AddXY((double)point.X, (double)point.Y);
                }
                chart1.Series["ACO"].Color = Color.Red;
            }
            if (geneticPoint.Count > 0)
            {
                foreach (Point point in geneticPoint)
                {
                    chart1.Series["Genetic"].Points.AddXY((double)point.X, (double)point.Y);
                }
                chart1.Series["Genetic"].Color = Color.Blue;
            }
        }

    }
}
