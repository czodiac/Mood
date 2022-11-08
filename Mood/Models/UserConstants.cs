namespace Mood.Models {
    public class UserConstants {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "kim", EmailAddress = "kim@email.com", Password = "MyPass0", GivenName = "Jason", Surname = "Kim", Role = "Administrator" },
            new UserModel() { Username = "jason", EmailAddress = "jason@email.com", Password = "MyPass0", GivenName = "Jason", Surname = "Lee", Role = "User" },
        };
    }
}
