using GUExercises.Models;

namespace GUExercises.Services
{
    public class AnswersService
    {
        public UserModel GetUser()
        {
            return new UserModel() { Name = "Grace Uy", Token = "1234-455662-22233333-3333" };
            //return new UserModel() { Name = "test", Token = "1234-455662-22233333-3333" };
        }
    }
}
