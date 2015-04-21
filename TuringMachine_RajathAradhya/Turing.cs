using System;
using System.Collections.Generic;
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
        public Turing(List<string> k, List<char> sigma,List<char> tapeChars, List<Transitions> delta, string s, List<string> a)
        {
            K = k.ToList();
            Sigma = sigma.ToList();
            TapeChars = tapeChars;
            AddDelta(delta);
            AddInitialState(s);
            Acceptingstates(a);
        }
        private bool PreviousStateDelta(Transitions delt)  /*!< trasition function */
        {
            return deltA.Any(vari => vari.StartState == delt.StartState && vari.InputSymbol == delt.InputSymbol);
            /*!< return true if start state is same for the symbol */
        }
        private bool ValidDelta(Transitions delt)
        {
            return K.Contains(delt.StartState) && K.Contains(delt.EndState) &&
                   Sigma.Contains(delt.InputSymbol) && /*!< check if start and end state is in delta and also check if thsymbol is valid */
                   !PreviousStateDelta(delt);
        }

        private void AddDelta(List<Transitions> delta) /*!< Setting all transition states */
        {
            foreach (Transitions del in delta.Where(ValidDelta))
            {
                deltA.Add(del);
            }
        }

        private void AddInitialState(string s)
        {
            if (s != null && K.Contains(s))
            {
                S = s;/*!< setting intial state */
            }
        }

        private void Acceptingstates(List<string> acceptingstates)
        {
            foreach (string acceptState in acceptingstates.Where(finalState => K.Contains(finalState)))
            {
                A.Add(acceptState); /*!< setting accepting state */
            }
        }

        public void inputCheck(string inputs, int threshold, int numOfTapes)
        {
            tape = inputs.Split(',').ToList<string>();
            multiTapes.Add(tape);
            for(int i = 0; i<numOfTapes;i++)
            {
                multiTapes.Add((" , ").Split(',').ToList<string>());
            }
        }
    }
}
