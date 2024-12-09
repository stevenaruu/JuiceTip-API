using JuiceTip_API.Data;
using JuiceTip_API.Model;

namespace JuiceTip_API.Factories
{
    public class UserFactory
    {
        public MsUser CreateUser(RegisterRequest user)
        {
            return new MsUser
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Telephone = user.Telephone,
                Gender = user.Gender,
                ProfileImage = null,
                JuiceCoin = 0,
                Created = DateTime.Now,
                DOB = user.DOB
            };
        }
    }
}
