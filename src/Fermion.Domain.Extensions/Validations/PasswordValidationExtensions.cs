namespace Fermion.Domain.Extensions.Validations;

public static class PasswordValidationExtensions
{
    public static bool ContainUppercase(string password)
    {
        return password.Any(char.IsUpper);
    }

    public static bool ContainLowercase(string password)
    {
        return password.Any(char.IsLower);
    }

    public static bool ContainDigit(string password)
    {
        return password.Any(char.IsDigit);
    }

    public static bool ContainSpecialCharacter(string password)
    {
        var specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        return password.Any(c => specialChars.Contains(c));
    }

    public static bool NotBeCommonPassword(string password)
    {
        return !CommonPasswords.Contains(password);
    }

    public static bool NotContainSequentialCharacters(string password)
    {
        var sequences = new[]
        {
            "0123456789", "abcdefghijklmnopqrstuvwxyz", "qwertyuiop", "asdfghjkl", "zxcvbnm"
        };

        foreach (var sequence in sequences)
        {
            for (int i = 0; i <= sequence.Length - 3; i++)
            {
                var subSequence = sequence.Substring(i, 3);
                if (password.ToLower().Contains(subSequence))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool NotContainRepeatingCharacters(string password)
    {
        for (int i = 0; i < password.Length - 2; i++)
        {
            if (password[i] == password[i + 1] && password[i + 1] == password[i + 2])
            {
                return false;
            }
        }
        return true;
    }

    public static bool ContainUsername(string password, string username)
    {
        if (string.IsNullOrEmpty(username)) return false;
        return password.ToLower().Contains(username.ToLower());
    }

    public static bool ContainEmailParts(string password, string email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        var emailParts = email.Split('@');
        if (emailParts.Length < 2) return false;

        var localPart = emailParts[0];
        if (localPart.Length >= 3 && password.ToLower().Contains(localPart.ToLower()))
        {
            return true;
        }

        return false;
    }

    private static readonly HashSet<string> CommonPasswords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "password", "123456", "123456789", "qwerty", "abc123", "password123",
        "admin", "letmein", "welcome", "monkey", "dragon", "master",
        "12345678", "1234567890", "qwertyuiop", "asdfghjkl"
    };
}