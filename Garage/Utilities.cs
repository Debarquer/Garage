using Garage.Contracts;

namespace Garage
{
    public class Utilities
    {
        /// <summary>
        /// Prompts the user to input a string.
        /// </summary>
        /// <param name="prompt">Prompt displayed to user.</param>
        /// <returns>User input</returns>
        public static string PromptUserForString(string prompt, IUI ui)
        {
            ui.PrintMessage(prompt);
            return ui.GetString();
        }

        /// <summary>
        /// Validates length of string and validates it using validator.
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="validator">Function used to validate user input.</param>
        /// <returns></returns>
        public static string PromptUserForValidInput(string prompt, Func<string, bool> validator, IUI ui, string error = "")
        {
            string input = "";
            bool isValidInput = false;
            do
            {
                input = PromptUserForString(prompt, ui);

                isValidInput = validator(input);

                if (!isValidInput)
                {
                    ui.PrintMessage(error == "" ? "Invalid input format." : error);
                }
            } while (!isValidInput);

            return input;
        }

        /// <summary>
        /// Prompts user for a string and validates length of string and if it contains only letters.
        /// </summary>
        /// <param name="prompt">Prompt displayed to user.</param>
        /// <returns></returns>
        public static string PromptUserForValidString(string prompt, IUI ui)
        {
            return PromptUserForValidInput(prompt, ValidateInputString, ui);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateInputString(string input)
        {
            return input.Length > 0 && ContainsOnlyLetters(input);
        }

        /// <summary>
        /// Prompts user for a number and validates length of string and if it contains only digits.
        /// </summary>
        /// <param name="prompt">Prompt displayed to user.</param>
        /// <returns></returns>
        public static int PromptUserForValidNumber(string prompt, IUI ui)
        {
            return int.Parse(PromptUserForValidInput(prompt, ValidateInputNumber, ui));
        }

        /// <summary>
        /// Validates length of string and if it contains only digits.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateInputNumber(string input)
        {
            return input.Length > 0 && ContainsOnlyDigits(input);
        }

        /// <summary>
        /// Checks if a string contains only letters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsOnlyLetters(string s)
        {
            foreach (char c in s.ToCharArray())
            {
                if (!char.IsLetter(c)) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if a string contains only digits.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsOnlyDigits(string s)
        {
            foreach (char c in s.ToCharArray())
            {
                if (!char.IsDigit(c)) return false;
            }

            return true;
        }
    }
}
