// A lambda function that getting a name and returns if the name is valid by checking if the structure of the name is:
// Hebrew first name + space + Hebrew middle name (optional) + space and Hebrew last name using Regex.
private readonly Func < string,
bool > IsValidName = name =>Regex.Replace(name, " {2,}", " ").TrimStart().TrimEnd().Split(' ').Length > 1 && Regex.Match(name, "^[א-ת ']+$").Success;
//validating an email
private readonly Func < string,
bool > IsValidEmail = email =>new EmailAddressAttribute().IsValid(email);
//Validating an israeli phone number
private readonly Func < string,
bool > IsValidPhoneNumber = phoneNum =>Regex.IsMatch(phoneNum, @"^(?:(?:(\+?972|\(\+?972\)|\+?\(972\))(?:\s|\.|-)?([1-9]\d?))|(0[23489]{1})|(0[57]{1}[0-9]))(?:\s|\.|-)?([^0\D]{1}\d{2}(?:\s|\.|-)?\d{4})$");

//VALIDATING AN ISRAELI ID
public bool IsValidId(string Id) {
  try {
    char[] digits = Id.PadLeft(9, '0').ToCharArray();
    int[] oneTwo = {
      1,
      2,
      1,
      2,
      1,
      2,
      1,
      2,
      1
    };
    int[] multiply = new int[9];
    int[] oneDigit = new int[9];
    for (int i = 0; i < 9; i++)
    multiply[i] = Char.GetNumericValue(digits[i]) * oneTwo[i];
    for (int i = 0; i < 9; i++)
    oneDigit[i] = (int)(multiply[i] / 10) + multiply[i] % 10;
    int sum = 0;
    for (int i = 0; i < 9; i++)
    sum += oneDigit[i];
    return sum % 10 == 0;
  }
  catch {
    return false;
  }

}

//VALIDATING A CCN
public static bool IsCardNumberValid(string cardNumber) {
  int i,
  checkSum = 0;

  // checksum of every other digit starting from right one
  for (i = cardNumber.Length - 1; i >= 0; i -= 2)
  checkSum += Char.GetNumericValue(cardNumber[i]);

  //taking the digits that not included in first checksum, multiple by two,
  // and getting the checksum of resulting digits
  for (i = cardNumber.Length - 2; i >= 0; i -= 2) {
    int val = (Char.GetNumericValue(cardNumber[i]) * 2);
    while (val > 0) {
      checkSum += (val % 10);
      val /= 10;
    }
  }

  // Number is valid if sum of both checksums MOD 10 equals 0
  return ((checkSum % 10) == 0);
}
