namespace NoteAndTask.Extensions
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifeTime { get; set; }
        public string SecurityKey { get; set; }
    }
}