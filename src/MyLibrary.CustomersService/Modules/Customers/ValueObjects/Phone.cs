namespace MyLibrary.CustomersService.Modules.Customers.ValueObjects
{
    public class Phone
    {
        public string AreaCode { get; private set; }
        public string Prefix { get; private set; }
        public string LineNumber { get; private set; }

        public Phone(string areaCode, string prefix, string lineNumber)
        {
            if (!IsValidAreaCode(areaCode) || !IsValidPrefix(prefix) || !IsValidLineNumber(lineNumber))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            AreaCode = areaCode;
            Prefix = prefix;
            LineNumber = lineNumber;
        }

        public Phone(string phoneNumber)
        {
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (!IsValidPhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            AreaCode = phoneNumber.Substring(0, 3);
            Prefix = phoneNumber.Substring(3, 3);
            LineNumber = phoneNumber.Substring(6, 4);
        }

        public override string ToString() => $"({AreaCode}) {Prefix}-{LineNumber}";

        // Implicit operator to create a Phone from a string in the format "(123) 456-7890"
        public static implicit operator Phone(string phoneNumber) => new Phone(phoneNumber);

        // Validation methods
        private static bool IsValidAreaCode(string areaCode) => areaCode.Length == 3 && int.TryParse(areaCode, out _);
        private static bool IsValidPrefix(string prefix) => prefix.Length == 3 && int.TryParse(prefix, out _);
        private static bool IsValidLineNumber(string lineNumber) => lineNumber.Length == 4 && int.TryParse(lineNumber, out _);
        private static bool IsValidPhoneNumber(string phoneNumber) => phoneNumber.Length == 10 && int.TryParse(phoneNumber, out _);
    }

}
