using AutoMapper;
using BlazorWalletTrackerBL.Services;
using BlazorWalletTrackerDAL.Models;
using Microsoft.Extensions.Logging;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorWalletTrackerBL.Models
{
    public class AutoMapperconfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<User, UserBL>();
                //cfg.CreateMap<UserBL, User>();
                //cfg.CreateMap<UserBL, LoginUserModel>();
                //cfg.CreateMap<Transaction, TransactionBL>();
                //cfg.CreateMap<TransactionBL, Transaction>();

                cfg.CreateMap<User, UserBL>().ReverseMap();
                cfg.CreateMap<Transaction, TransactionBL>().ReverseMap();
                cfg.CreateMap<UserBL, LoginUserModel>().ReverseMap();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
