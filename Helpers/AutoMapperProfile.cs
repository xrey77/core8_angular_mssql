using AutoMapper;
using core8_angular_mssql.Entities;
using core8_angular_mssql.Models.dto;

namespace core8_angular_mssql.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserRegister, User>();
            CreateMap<UserLogin, User>();
            CreateMap<UserUpdate, User>();
            CreateMap<UserPasswordUpdate, User>();

        }
    }
    

}