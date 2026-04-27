namespace OfficeReservations.Services;

public static class CodeGenerator
{
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static readonly Random _random = new();

    public static string GenerateUniqueCode(IEnumerable<string?> existingCodes)
    {
        var existing = existingCodes
            .Where(c => c is not null)
            .Select(c => c!)
            .ToHashSet();
        string code;
        do
        {
            code = new string(Enumerable
                .Range(0, 5)
                .Select(_ => Characters[_random.Next(Characters.Length)]) // Random index of string
                .ToArray());
        } while (existing.Contains(code));

        return code;
    }
}