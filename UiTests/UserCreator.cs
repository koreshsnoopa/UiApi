namespace BookingTests
{
    public class UserCreator
    {
        public const string USERNAME = "firstaccount33@yandex.by";
        public const string PASSWORD = "3ve9WNZ),nPFr2F";

        public static User WithCredentialsFromProperty()
        {
            return new User(USERNAME, PASSWORD);
        }
    }
}
