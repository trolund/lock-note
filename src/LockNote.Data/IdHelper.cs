using NanoidDotNet;

namespace LockNote.Data;

public static class IdHelper
{
    public static async Task<string> NewId() {
        return await Nanoid.GenerateAsync(Nanoid.Alphabets.LettersAndDigits, 10);
    }
    
}