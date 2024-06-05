namespace cred_system_back_end_app.Application.Common.Helpers
{
    public class NpiHelper
    {
        public bool IsValidNPI(string request)
        {
            int[] digits = request.Select(c => int.Parse(c.ToString())).ToArray();
            int checkDigitExpected = digits[digits.Length - 1];
            digits = digits.Take(9).ToArray();


            for (int i = 0; i <= digits.Length - 1; i += 2)
            {
                int doubledDigit = digits[i] * 2;
                digits[i] = doubledDigit > 9 ? doubledDigit - 9 : doubledDigit;
            }

            // Sum all the digits (including the doubled digits) and constant number
            int sum = digits.Sum() + 24;

            //Calculate the check digit
            int checkDigit = sum * 9 % 10;

            //Check if the last digit of the input matches the calculated check digit
            return checkDigitExpected == checkDigit;
        }
    }
}
