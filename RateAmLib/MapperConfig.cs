using AutoMapper;
using RateAmData.Entities;

namespace RateAmLib
{
    public class MapperConfig
    {
        public static readonly IMapper Mapper = Configure();
        private static IMapper Configure()
        {
            MapperConfiguration config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<BankEntity, Bank>();
                    cfg.CreateMap<Bank, BankEntity>();

                    cfg.CreateMap<RateEntity, Rate>();
                    cfg.CreateMap<Rate, RateEntity>();
                    //cfg.CreateMap<RateEntity, Rate>()
                    //    .ForMember(dest => dest.BuyRate, opt => opt.MapFrom(src => src.BuyRate.ToString("F")))
                    //    .ForMember(dest => dest.SellRate, opt => opt.MapFrom(src => src.SellRate.ToString("F")));

                    //cfg.CreateMap<Rate, RateEntity>()
                    //    .ForMember(dest => dest.BuyRate, opt => opt.MapFrom(src => decimal.Parse(src.BuyRate)))
                    //    .ForMember(dest => dest.SellRate, opt => opt.MapFrom(src => decimal.Parse(src.SellRate)));

                    cfg.CreateMap<CurrencyEntity, Currency>();
                    cfg.CreateMap<Currency, CurrencyEntity>();
                }
            );
            return config.CreateMapper();
        }
    }
}
