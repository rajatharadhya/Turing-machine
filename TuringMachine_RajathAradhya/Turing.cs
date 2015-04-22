using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine_RajathAradhya
{
    public class Turing
    {
        private readonly List<string> K = new List<string>();    /*!< finite set of states */
        private readonly List<char> Sigma = new List<char>();    /*!< input alphabets  */
        private readonly List<Transitions> deltA = new List<Transitions>();  /*!< trasition function */
        List<char> TapeChars = new List<char>(); /*!< tape charectors */
        private string S;                                        /*!< start state */
        private readonly List<string> A = new List<string>();    /*!< accepting states */
        private List<string> tape = new List<string>();
        public List<List<string>> multiTapes = new List<List<string>>();
        /*! \brief Constructor */
        public Turing(List<string> k, List<char> sigma, List<char> tapeChars, List<Transitions> delta, string s, List<string> a)
        {
            K = k.ToList();
            Sigma = sigma.ToList();
            TapeChars = tapeChars;
            deltA = delta;
            S = s;
            A = a;
        }


        public void inputCheck(string inputs, int threshold, int numOfTapes)
        {
            string currentState = S;
            tape = inputs.Split(',').ToList<string>();
            for (int i = 1; i < numOfTapes; i++)
            {
                multiTapes.Add((" ,").Split(',').ToList<string>());
            }
            bool halt = true;
            int j = 1;
            int iterationCount = 1;
            using (StreamWriter writer = new StreamWriter("logfile.txt"))
            {
                writer.WriteLine("Intial input" + inputs);
                while (halt)
                {
                    writer.WriteLine("");
                    writer.WriteLine("current state = " + currentState + " current alphabet = " + tape[j] + " iteration Count =" + iterationCount);
                    iterationCount++;
                    if (tape[j] == null || tape[j] == " " || (iterationCount - 1) == threshold || A.Contains(currentState))
                    {
                        if (A.Contains(currentState))
                            Console.WriteLine(" Input Accepted");
                        else
                            Console.WriteLine(" Input Rejected");
                        printing(writer);
                        halt = false;
                        continue;
                    }
                    Transitions transit = deltA.Find(t => t.StartState == currentState && t.InputSymbol == Convert.ToChar(tape[j]));
                    if (transit == null)
                    {
                        Console.WriteLine(" Input Rejected");
                        printing(writer);
                        halt = false;
                        continue;
                    }
                    currentState = transit.EndState;
                    tape[j] = transit.WriteSymbol.ToString();
                    int k = 0;
                    List<string> alphas = transit.InputsNtapes;
                    foreach (List<string> taps in multiTapes)
                    {
                        try
                        {
                            taps[j] = alphas[k];
                        }
                        catch
                        {
                            taps.Add(alphas[k]);
                        }
                        k++;
                    }
                    printing(writer);
                    if (transit.Direction == 'r')
                        j++;
                    else
                        j--;
                }
            }
        }
        public void printing(StreamWriter writer)
        {
            writer.Write("Tape 1-");
            foreach (string alpa in tape)
            {
                writer.Write(alpa);
            }
            writer.WriteLine("");
            int k = 2;
            foreach (List<string> tapsi in multiTapes)
            {
                writer.Write("Tape " + k + " -");
                foreach (string alpa in tapsi)
                {
                    writer.Write(alpa);
                }
                k++;
                writer.WriteLine("");
            }
        }
    }
}
