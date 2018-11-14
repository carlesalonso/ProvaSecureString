using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ProvaSecureString
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the secure string.
            SecureString securePwd = new SecureString();
            ConsoleKeyInfo key;

            Console.Write("Enter password: ");
            // 
            // Es capturen les tecles i s'impriemeix per pantalla *
            // i es guarda el resultat en un SecureString
            //
            do
            {
                key = Console.ReadKey(true);

                // Ignore any key out of range.
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    // Append the character to the password.
                    securePwd.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");

                }
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            writePassword(securePwd);
            Console.ReadKey();




        }

        //
        // Funció writePassword
        // secureString paràmetre tipus SecureString
        // escriu per pantall utilitzant un punter (IntPTR)
        // un cop utilitzat s'allibera el punter per tal d'eliminar
        // la informació en text pla
        //
        static void writePassword (SecureString secureString)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(secureString);

            Console.WriteLine(Marshal.PtrToStringBSTR(bstr));

            Marshal.ZeroFreeBSTR(bstr);
        }
    }
}
