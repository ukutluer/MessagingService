using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.Entities.Events;
using MessagingService.Entities.User;
using MessagingService.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserDal _userDal;
        public event EventHandler<UserLoginTransactionEventArgs> OnUserLoginTransactionProcessed;
        private readonly IAuditService _auditService;

        public UserService(IOptions<AppSettings> appSettings, IUserDal userDal, IAuditService auditService)
        {
            _appSettings = appSettings.Value;
            _auditService = auditService;
            _auditService.Subscribe(this);
            _userDal = userDal;
        }

        public IEnumerable<User> GetAll()
        {
            // all user passwords setted null
            return _userDal.Get().ToList().Select(x =>
            {
                x.Password = null;
                return x;
            });
        }

        public bool IsUserExist(User user)
        {
            return _userDal.Get(q => q.UserName.ToLower() == user.UserName.ToLower()).FirstOrDefault() != null;
        }

        public async Task InsertAsync(User user)
        {
            await _userDal.AddAsync(user);
        }

        User IUserService.Authenticate(string name, string password)
        {
            var user = _userDal.Get(q => q.UserName == name & q.Password == password).FirstOrDefault();

            // if user not found return null
            if (user == null)
            {
                if (OnUserLoginTransactionProcessed != null)
                    OnUserLoginTransactionProcessed(this, new UserLoginTransactionEventArgs(false,name));
                return null;
            }
            else
            {
                if (OnUserLoginTransactionProcessed != null)
                    OnUserLoginTransactionProcessed(this, new UserLoginTransactionEventArgs(true, name));
            }
                

            // if authentication is success then generate jwttoken
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // password sending null
            user.Password = null;

            return user;
        }
    }
}