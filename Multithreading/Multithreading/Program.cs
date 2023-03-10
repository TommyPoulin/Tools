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
        static BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);

        static void Main(string[] args) {
            TryPasswords();
            Console.ReadLine();
        }

        static void TryPasswords() {
            int[] charPos = new int[passwordLength];
            for (int i = 0; i < passwordLength; i++) {
                if (i == 0)
                    charPos[i] = startingLetter;
                else
                    charPos[i] = 0;
            }
            //startingLetter += 4;

            char[] pass = new char[passwordLength];

            int robbedAmount = 0;
            while (robbedAmount != -1) {
                for (int i = 0; i < passwordLength; i++)
                    pass[i] = BankOfBitsNBytes.acceptablePasswordChars[charPos[i]];
                robbedAmount = bbb.WithdrawMoney(pass);
                OutputCharArray(pass);
                charPos = NextPassword(charPos);
            }
        }

        static Random r = new Random(); //To prevent it being re-created every frame based on sys clock (Which would produce non-random number)
        static char[] GenerateRandomPassword(int passwordLength) {
            char[] toRet = new char[passwordLength];
            for (int i = 0; i < passwordLength; ++i) {
                int randomInt = (r.Next() % BankOfBitsNBytes.acceptablePasswordChars.Length);
                toRet[i] = BankOfBitsNBytes.acceptablePasswordChars[randomInt];
            }
            return toRet;
        }

        //This is very expensive and just for debugging. You do not need to output in the final test
        static void OutputCharArray(char[] toOut) {
            Console.Out.WriteLine(new string(toOut));
        }

        static int[] NextPassword(int[] password) {
            int[] tempPassword = password;
            int index = tempPassword.Length - 1;
            int maxIndex = BankOfBitsNBytes.acceptablePasswordChars.Length - 1;
            while (index >= 0) {
                if (tempPassword[index] < maxIndex) {
                    tempPassword[index] += 1;
                    index = -1;
                }
                else {
                    tempPassword[index] = 0;
                    index--;
                }
            }
            return tempPassword;
        }
    }
}