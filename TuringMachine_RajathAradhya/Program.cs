using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine_RajathAradhya
{
    class Program
    {
        static void Main(string[] args)
        {
            int threshold;
            StreamReader machineReader = new StreamReader("Machine.txt");
            string[] sd = machineReader.ReadLine().Split(',');
            threshold = Convert.ToInt32(sd[0]);
            int numOfTapes = Convert.ToInt32(sd[1]);
            string readingline = machineReader.ReadLine();
            string[] line = readingline.Split('/');
            List<string> K = new List<string>();    /*!< finite set of states */
            List<char> sigma = new List<char>();    /*!< input alphabets  */
            List<char> tapeChars = new List<char>(); /*!< tape charectors */
            List<Transitions> transits = new List<Transitions>();  /*!< trasition function */
            string s;                               /*!< start state */
            List<string> A = new List<string>();    /*!< accepting states */
            K = line[0].Split(',').ToList<string>();
            foreach (string str in line[1].Split(','))
            {
                sigma.Add(Convert.ToChar(str));
            }
            foreach (string str in line[2].Split(','))
            {
                tapeChars.Add(Convert.ToChar(str));
            }
            s = line[3].ToString();
            A = line[4].Split(',').ToList<string>();
            while ((readingline = machineReader.ReadLine()) != null)
            {
                List<string> InputsNtapes = new List<string>();
                line = readingline.Split(',');
                if(numOfTapes < 1 )
                {
                    Console.WriteLine("Number of tapes has to be 1 or more. Rerun the program");
                    Console.ReadLine();
                    System.Environment.Exit(1);
                }
                if (numOfTapes > 1)
                {
                    for (int i = 5; i < (4 + numOfTapes); i++)
                    {
                        InputsNtapes.Add(line[i]);
                    }
                }

                transits.Add(new Transitions(line[0], Convert.ToChar(line[1]), line[2], Convert.ToChar(line[3]), Convert.ToChar(line[4]), InputsNtapes));
            }
            machineReader.Close();
            Turing tur = new Turing(K, sigma, tapeChars, transits, s, A);
            string inputs;
            StreamReader inputReader = new StreamReader("input.txt");
            if ((inputs = inputReader.ReadLine()) != null)
            {
                Console.WriteLine("Input --> " + inputs);
                tur.inputCheck(inputs, threshold, numOfTapes);
            }
            Console.Read();
        }
    }
}
