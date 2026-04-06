using Infrastructure.Entities;

namespace ApiTests.Helpers;

public static class TestDataHelper
{
    public static User BaseUser =>
        new()
        {
            Username = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            Coins = 100,
        };
}
