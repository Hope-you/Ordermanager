﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DbHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ordermanager.Dal.Dal.BaseDal;
using Ordermanager.Dal.RedisContext;
using Ordermanager.Model;
using StackExchange.Redis;

namespace Ordermanager.Dal
{
    /// <summary>
    /// 这个接口继承了Idal<User> 用来扩展Userdal的接口，
    /// 因为不是所有实体的接口都是一样的，只是把一样的给提取出啦
    /// 这里已经指定了泛型T是user
    /// </summary>
    public interface IUserDal : IBaseOverAllDal<User>
    {
        public User GetUserByLogin(string userName, string userPwd);
        IEnumerable<User> SelectAll();

        public bool IsAuthenticated(LoginRequestBody request, out string token);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="regUser"></param>
        /// <returns></returns>
        public bool RegUser(User regUser);

    }

    /// <summary>
    /// 继承了类BaseDal中的基本方法，接口IUserDal进行扩展的方法
    /// </summary>
    public class UserDal : BaseOverAllDal<User>, IUserDal
    {
        private readonly IBaseOverAllDal<User> _baseOverAllDal;
        private readonly IOptions<TokenManagement> tokenManagement;

        public UserDal(IBaseOverAllDal<User> baseOverAllDal, IOptions<TokenManagement> tokenManagement) : base(baseOverAllDal)
        {
            _baseOverAllDal = baseOverAllDal;
            this.tokenManagement = tokenManagement;
        }
        public User GetUserByLogin(string userName, string userPwd)
        {
            string sql = "select * from User where userName=@userName and userPwd=@userPwd";
            //var aaa = _db.QueryFirst<User>(sql, new {userName, userPwd});
            return _baseOverAllDal.QueryFirst<User>(sql, new { userName, userPwd });
        }

        public IEnumerable<User> SelectAll()
        {
            return _baseOverAllDal.GetAll();
        }


        public bool RegUser(User regUser)
        {
            //注册之前需要查询当前是否存在这个用户
            string sql = "SELECT id from User WHERE userName=@userName and userPwd=@userPwd";
            if (_baseOverAllDal.IsExist(sql, new { regUser.userName, regUser.userPwd })) return false;
            //并没有返回影响的行数，没有参考价值
            _baseOverAllDal.Insert(regUser);
            if (_baseOverAllDal.IsExist(sql, new { regUser.userName, regUser.userPwd })) return true;
            return false;

        }



        /// <summary>
        /// 判断并生成token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsAuthenticated(LoginRequestBody request, out string token)
        {
            token = string.Empty;
            //查询是否存在
            var loginUser = GetUserByLogin(request.userName, request.passWord);
            //在数据库中验证数据
            if (loginUser == null)
                return false;
            token = GeneratorToken(loginUser.id);
            return true;
        }


        /// <summary>
        /// 生成token的具体方法
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GeneratorToken(string username)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Value.Secret));
            // 加密
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            // 自定义claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,username)
            };
            var token = new JwtSecurityToken(
                 tokenManagement.Value.Issuer, // 发行者
                 tokenManagement.Value.Audience, // 使用者
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
