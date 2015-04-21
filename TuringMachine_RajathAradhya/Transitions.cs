using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine_RajathAradhya
{
    public class Transitions
    {
        public string StartState { get; private set; } /*!< Start state. */
        public char InputSymbol { get; private set; } /*!< input symbols */
        public string EndState { get; private set; } /*!< End states. */
        public char WriteSymbol { get; private set; } /*!< symbol to be written on to tape */
        public char Direction { get; private set; } /*!< direction tape moves */
        public List<string> InputsNtapes { get; private set; }
        public Transitions(string startState, char inputSymbol, string endState, char writeSymbol, char direction, List<string> inputsNtapes)
       {
           StartState = startState;  /*!< Start state. */
           InputSymbol = inputSymbol; /*!< each input symbols */
           EndState = endState;  /*!< End states. */
           WriteSymbol = writeSymbol;
           Direction = direction;
           InputsNtapes = inputsNtapes;
      }
       /*! \brief Formatting  */
       //public override string ToString() /*!< Formatting  */  
       //{
       //    return string.Format("{0},{1},{2},{3},{4}", StartState, InputSymbol, EndState,WriteSymbol,Direction);
       //}
    }
}
