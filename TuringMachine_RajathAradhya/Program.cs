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
            threshold = Convert.ToInt32(machineReader.ReadLine());
            string readingline = machineReader.ReadLine();
            string[] line = readingline.Split('/');
            List<string> K = new List<string>();    /*!< finite set of states */
            List<char> sigma = new List<char>();    /*!< input alphabets  */
            List<char> tapeChars = new List<char>(); /*!< tape charectors */
            List<Transitions> delta = new List<Transitions>();  /*!< trasition function */
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
                line = readingline.Split(',');
                delta.Add(new Transitions(line[0], Convert.ToChar(line[1]), line[2], Convert.ToChar(line[3]), Convert.ToChar(line[4])));
            }
            machineReader.Close();
            Turing dfsm = new Turing(K, sigma, delta, s, A);
        }
    }
}
