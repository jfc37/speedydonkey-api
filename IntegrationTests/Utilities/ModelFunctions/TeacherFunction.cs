using System.Collections.Generic;
using System.Net;
using Contracts.Teachers;

namespace IntegrationTests.Utilities.ModelFunctions
{
    public static class TeacherFunction
    {
        public static List<TeacherModel> GetAllTeachers()
        {
            var response = ApiCaller.Get<List<TeacherModel>>(Routes.Teachers);

            return response.StatusCode == HttpStatusCode.NotFound 
                ? new List<TeacherModel>() 
                : response.Data;
        }
    }
}