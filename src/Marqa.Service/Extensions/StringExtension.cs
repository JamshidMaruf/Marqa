using System.Text;

namespace Marqa.Service.Extensions;

public static class StringExtension
{
    /// <summary>
    /// This method drops characters except digits such as "+ - ()" and 
    /// checks whether these digits make a valid phone number together
    /// </summary>
    /// <param name="phone"></param>
    /// <returns>
    /// It returns trimmed phone number and true if the phone is valid. Otherwise null and false.
    /// 
    /// Note: It will not throw any exception if the phone is null or invalid!
    /// </returns>
    public static (string Phone, bool IsSuccessful) TrimPhoneNumber(this string phone)
    {
        if (phone == null)
            return (null, false);

        var trimPhone = phone.Trim().ToCharArray();

        StringBuilder trimmedPhone = new StringBuilder();

        foreach (var item in trimPhone)
        {
            if (char.IsDigit(item))
            {
                trimmedPhone.Append(item);
            }
        }

        if (trimmedPhone.Length == 9)
        {
            return ("998" + trimmedPhone, true);

        }
        else if (trimmedPhone.Length == 12)
        {
            if (trimmedPhone.ToString().StartsWith("998"))
            {
                return (trimmedPhone.ToString(), true);
            }
        }

        return (null, false);
    }
    
}
