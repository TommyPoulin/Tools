using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBitsAndBytes
{
    class Program
    {
        static readonly int passwordLength = 2; //Can you solve up to 6?
        static int startingLetter = 0;

        static void Main(string[] args) {
            BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
            int robbedAmount = 0;
            while (robbedAmount != -1) {
                char[] guess = GenerateRandomPassword(passwordLength);
                OutputCharArray(guess);
                robbedAmount = bbb.WithdrawMoney(GenerateRandomPassword(passwordLength));

            }
            Console.ReadLine();
        }

        static void TryPasswords() {
            int[] charPos = {
                startingLetter,
                0
            };
            startingLetter += 4;

            int robbedAmount = 0;
            while (robbedAmount != -1) {

            }
        }

        static Random r = new Random(); //To prevent it being re-created every frame based on sys clock (Which would produce non-random number)
        static char[] GenerateRandomPassword(int passwordLength)
        {
            char[] toRet = new char[passwordLength];
            for (int i = 0; i < passwordLength; ++i)
            {
                int randomInt = (r.Next() % BankOfBitsNBytes.acceptablePasswordChars.Length);
                toRet[i] = BankOfBitsNBytes.acceptablePasswordChars[randomInt];
            }
            return toRet;
        }

        //This is very expensive and just for debugging. You do not need to output in the final test
        static void OutputCharArray(char[] toOut)
        {
            Console.Out.WriteLine(new string(toOut));
        }
    }
}
