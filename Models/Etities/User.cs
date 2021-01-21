namespace api_imdb.Models.Etities
{
    public class User
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public bool Active { get; set; }
        public User(string appUserId)
        {
            AppUserId = appUserId;
            Active = true;
        }
    }
}
