using System;

namespace JosephusProblem
{
    public class UserInterface
    {
        public void RunInterface()
        {
            JosephusProblemSolver josephusProblemSolver = new JosephusProblemSolver();
            bool endProgram = false;

            while ( !endProgram )
            {
                Console.WriteLine( "To find the position of the safe seat in a Josephus problem, enter the number of participants:" );
                string userInputNumberOfParticipants = Console.ReadLine();
                Console.WriteLine( josephusProblemSolver.FindSafeSeatPosition( userInputNumberOfParticipants ) );
                Console.WriteLine( "Press [E] to end. Press any other key to play again." );
                string userInputEndOrContinueProgram = Console.ReadLine();

                if ( userInputEndOrContinueProgram.ToUpper() == "E" )
                {
                    endProgram = true;
                }
            }
        }
    }
}
