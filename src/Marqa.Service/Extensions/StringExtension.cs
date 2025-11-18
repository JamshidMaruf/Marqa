using System.Text;

namespace Marqa.Service.Extensions;

public static class StringExtension
{
    public static (string Phone, bool IsSuccessful) TrimPhoneNumber(this string phone)
    {
        var trimPhone = phone.Trim().ToCharArray();

        StringBuilder trimmedPhone = new StringBuilder();

        foreach(var item in trimPhone)
        {
            if(char.IsDigit(item))
            {
                trimmedPhone.Append(item);
            }
        }

        if(trimmedPhone.Length == 9)
        {
            return ("998" + trimmedPhone, true);

        }
        else if(trimmedPhone.Length == 12)
        {
            if (trimmedPhone.ToString().StartsWith("998"))
            {
                return (trimmedPhone.ToString(), true);
            }
        }

        return (null, false);
    }
}
