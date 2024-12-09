using JuiceTip_API.Data;
using JuiceTip_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;
using static JuiceTip_API.Output.CustomerOutput;
using JuiceTip_API.Output;
using JuiceTip_API.Factories;

namespace JuiceTip_API.Helper
{
    public class UserHelper
    {
        private JuiceTipDBContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserHelper(JuiceTipDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public MsUser GetUser([FromBody] LoginRequest user)
        {
            try
            {
                var data = _dbContext.MsUser.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (data != null) return data;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GenerateOTP()
        {
            // Generate a random 6-digit OTP
            Random rnd = new Random();
            int otpNumber = rnd.Next(100000, 999999);
            return otpNumber.ToString();
        }

        public string SendOTPEmail([FromBody] OTP user)
        {
            try
            {
                string otp = GenerateOTP();
                string sender = _configuration["Email:Sender"];
                string password = _configuration["Email:Password"];

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(sender, password),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(sender),
                    Subject = "OTP Verification",
                    Body = $"Hello, {user.Name}!<br/><br/>Your JuiceTip Apps OTP code is <b>{otp}</b><br/>Beware of Fraud! This code is only for you to enter in JuiceTip Apps.<br/>Don't give your OTP code to anyone, including JuiceTip.<br/>Ignore this email if you feel like you didn't make the OTP request.<br/>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(user.Email);
                smtpClient.Send(mailMessage);

                return otp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MsUser CheckDuplicateEmail([FromBody] RegisterRequest user)
        {
            var duplicateUser = _dbContext.MsUser.Where(x => x.Email == user.Email).FirstOrDefault();

            if (duplicateUser != null)
            {
                return duplicateUser;
            }

            return null;
        }

        public MsUser InsertUser([FromBody] RegisterRequest user)
        {
            try
            {
                if (user != null)
                {
                    var userFactory = new UserFactory();
                    var newUser = userFactory.CreateUser(user);

                    _dbContext.MsUser.Add(newUser);
                    _dbContext.SaveChanges();

                    return newUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MsUser TopUp([FromBody] TopUpRequest user)
        {
            try
            {
                var currUser = _dbContext.MsUser.Where(x => x.UserId == user.UserId).FirstOrDefault();
                
                if (currUser != null)
                {
                    currUser.JuiceCoin = currUser.JuiceCoin + user.JuiceCoin;

                    _dbContext.Update(currUser);
                    _dbContext.SaveChanges();

                    return currUser;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MsUser DecreaseBalance([FromBody] TopUpRequest user)
        {
            try
            {
                var currUser = _dbContext.MsUser.Where(x => x.UserId == user.UserId).FirstOrDefault();

                if (currUser != null)
                {
                    currUser.JuiceCoin = currUser.JuiceCoin - user.JuiceCoin;

                    _dbContext.Update(currUser);
                    _dbContext.SaveChanges();

                    return currUser;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CustomerModel GetUserById([FromBody] UserRequest user)
        {
            try
            {
                var data = (from customer in _dbContext.MsUser
                            where customer.UserId == user.UserId
                            select new CustomerModel
                            {
                                UserId = customer.UserId,
                                Email = customer.Email,
                                FirstName = customer.FirstName,
                                LastName = customer.LastName,
                                ProfileImage = customer.ProfileImage
                            }).ToList().FirstOrDefault();

                if (data != null) return data;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
