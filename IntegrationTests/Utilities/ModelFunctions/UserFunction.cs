using System.Collections.Generic;
using Contracts.Users;

namespace IntegrationTests.Utilities.ModelFunctions
{
    public static class UserFunction
    {
        public static List<UserModel> GetAllUsers()
        {
            var response = ApiCaller.Get<List<UserModel>>(Routes.Users);

            return response.Data;
        }
    }
}
