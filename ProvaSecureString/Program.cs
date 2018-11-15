using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ProvaSecureString
{
    static class Program
    {
        static void Main(string[] args)
        {

            var pass1 = EnterPassword();
            var pass2 = EnterPassword();

            if (CompareSecureStrings(pass1, pass2))
            {
                Console.Write($"Password has {pass1.Length} chars and is ");
                WritePassword(pass1);
            }
            else
                Console.WriteLine("Password don't match");
            Console.ReadKey();




        }


        static SecureString EnterPassword()
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
            return securePwd;
        }

        //
        // Funció writePassword
        // secureString paràmetre tipus SecureString
        // escriu per pantall utilitzant un punter (IntPTR)
        // un cop utilitzat s'allibera el punter per tal d'eliminar
        // la informació en text pla
        //
        static void WritePassword (SecureString secureString)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(secureString);

            Console.WriteLine(Marshal.PtrToStringBSTR(bstr));

            Marshal.ZeroFreeBSTR(bstr);
        }

        public static bool CompareSecureStrings(this SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }


    }
}
