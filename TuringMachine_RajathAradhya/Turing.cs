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
            int inputNumber = 0;
            List<string> inputSplit = new List<string>();
            List<int> headers = new List<int>(); 
            string currentState = S;
            tape = inputs.Split(',').ToList<string>();
            inputSplit = inputs.Split(' ').ToList<string>();
            inputSplit.RemoveAt(0);
            inputSplit.RemoveAt(inputSplit.Count - 1);
            List<int> inputStartHeaders = new List<int>();
            for (int u = 1; u < (tape.Count-1); u++)
            {
                if (tape[u] == " ")
                {
                    inputStartHeaders.Add(u+1);
                }
            }
            for (int i = 1; i < numOfTapes; i++)
            {
                multiTapes.Add((" ,").Split(',').ToList<string>());
            }

            for (int i = 1; i < numOfTapes; i++)
            {
                headers.Add(1);
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
                    if ((iterationCount - 1) == threshold || A.Contains(currentState))
                    {
                        Console.WriteLine("Input --> " + inputSplit[inputNumber]);
                        if (A.Contains(currentState))
                            Console.WriteLine(" Input Accepted");
                        else
                            Console.WriteLine(" Input Rejected");
                        printing(writer);
                        if (inputNumber < (inputSplit.Count-1))
                        {
                            writer.Write("");
                            writer.Write("-----------------Next input-------------------");
                            writer.Write("");
                            j = inputStartHeaders[inputNumber];
                            inputNumber++;
                            currentState = S;
                            continue;
                        }
                        else
                        halt = false;
                        continue;
                    }
                    Transitions transit = deltA.Find(t => t.StartState == currentState && t.InputSymbol == Convert.ToChar(tape[j]));
                    if (transit == null)
                    {
                        Console.WriteLine("Input --> " + inputSplit[inputNumber]);
                        if (A.Contains(currentState))
                            Console.WriteLine(" Input Accepted");
                        else
                            Console.WriteLine(" Input Rejected");
                        printing(writer);
                        if (inputNumber < (inputSplit.Count - 1))
                        {
                            writer.Write("");
                            writer.Write("-----------------Next input-------------------");
                            writer.Write("");
                            j = inputStartHeaders[inputNumber];
                            inputNumber++;
                            currentState = S;
                            continue;
                        }
                        else
                        halt = false;
                        continue;
                    }
                    currentState = transit.EndState;
                    tape[j] = transit.WriteSymbol.ToString();
                    Dictionary<string, string> alphas = transit.InputsNtapes;
                    int count = 0;
                    foreach (List<string> taps in multiTapes)
                    {
                        var item = alphas.ElementAt(count);
                        
                        try
                        {
                            taps[headers[count]] = item.Key;
                        }
                        catch
                        {
                            taps.Add(item.Key);
                        }
                        if (item.Value == "r")
                            headers[count]++;
                        else if (item.Value == "l")
                            headers[count]--;
                        count++;
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
