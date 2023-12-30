using System;
using System.Text.RegularExpressions;

namespace JosephusProblem
{
    public class JosephusProblemSolver
    {
        public string FindSafeSeatPosition( string userInput )
        {
            string result = "Input a valid number, please.";
            int.TryParse( Regex.Replace( userInput, @"[^\w\s]", "" ), out int numberOfParticipants );

            if ( numberOfParticipants >= 2 )
            {
                //Here, I convert the number to a binary string, then move the string's first character to the end of the string
                //Finally, I convert the binary string back to base 10 and then concatenate
                string numberOfParticipantsInBinary = Convert.ToString( numberOfParticipants, 2 );
                result = "The safe position is seat number: " + Convert.ToInt32( numberOfParticipantsInBinary.Substring( 1 ) + numberOfParticipantsInBinary.Substring( 0, 1 ), 2 );
            }
            else if ( numberOfParticipants == 1 )
            {
                result = "The safe position is seat number: 1";
            }

            return result;
        }
    }
}
