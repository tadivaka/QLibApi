using System;
using System.Collections.Generic;
using System.Text;

namespace QLibrary.Common.Helper
{
    public static class PasswordGenerator
    {
        public static string CreatePassword()
        {

            const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = @"!#$%&*@\";

            string characterSet = "";
            characterSet += LOWERCASE_CHARACTERS;
            characterSet += UPPERCASE_CHARACTERS;
            characterSet += NUMERIC_CHARACTERS;
            characterSet += SPECIAL_CHARACTERS;

            char[] password = new char[8];
            int characterSetLength = characterSet.Length;

            System.Random random = new System.Random();
            for (int characterPosition = 0; characterPosition < 6; characterPosition++)
            {
                password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

                bool moreThanTwoIdenticalInARow =
                    characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && password[characterPosition] == password[characterPosition - 1]
                    && password[characterPosition - 1] == password[characterPosition - 2];

                if (moreThanTwoIdenticalInARow)
                {
                    characterPosition--;
                }

            }
            password[6] = SPECIAL_CHARACTERS[random.Next(SPECIAL_CHARACTERS.Length - 1)];
            password[7] = NUMERIC_CHARACTERS[random.Next(NUMERIC_CHARACTERS.Length - 1)];

            return string.Join(null, password);

        }
    }
}
