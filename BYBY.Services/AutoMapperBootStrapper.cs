using AutoMapper;
using BYBY.Repository.Entities;
using BYBY.Services.Request;
using System;

namespace BYBY.Services
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DateTime?, string>().ConvertUsing<DateTimeHasNullToStringConverter>();
                cfg.CreateMap<DateTime, string>().ConvertUsing<DateTimeToStringConverter>();
                cfg.CreateMap<string, DateTime?>().ConvertUsing<StringToDateTimeHasNullConverter>();
                cfg.CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();
                cfg.CreateMap<TBMedicalDetail, MedicalDetailRequest>();
                cfg.CreateMap<MedicalDetailRequest, TBMedicalDetail>();
                //        cfg.CreateMap<TBMedicalDetail, MedicalDetailRequest>().ForMember(d => d.MenstruationLast,
                //expression => expression.ResolveUsing(s => s.MenstruationLast.HasValue ? s.MenstruationLast.Value.ToString("yyyy-MM-dd") : ""));
            });

        }

        public class DateTimeHasNullToStringConverter : ITypeConverter<DateTime?, string>
        {
            public string Convert(DateTime? source, string value, ResolutionContext option)
            {
                if (source.HasValue)
                {
                    return source.Value.ToString("yyyy-MM-dd");
                }

                return string.Empty;
            }
        }

        public class StringToDateTimeHasNullConverter : ITypeConverter<string, DateTime?>
        {
            public DateTime? Convert(string source, DateTime? value, ResolutionContext option)
            {
                if (string.IsNullOrWhiteSpace(source))
                {
                    return null;
                }
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(source, out dt))
                {
                    return dt;
                }

                return null;
            }
        }

        public class DateTimeToStringConverter : ITypeConverter<DateTime, string>
        {
            public string Convert(DateTime source, string value, ResolutionContext option)
            {
                if (source != DateTime.MaxValue && source != DateTime.MinValue )
                {
                    return source.ToString("yyyy-MM-dd");
                }

                return string.Empty;
            }
        }

        public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime value, ResolutionContext option)
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(source, out dt))
                {
                    return dt;
                }

                return dt;
            }
        }
    }

    //public class ItemPriceResolver : ValueResolver<CustomerService, string>
    //{
    //    protected override string ResolveCore(CustomerService source)
    //    {
    //        return source.Created.Value.ToString("yyyy-MM-dd HH:mm:ss");
    //    }
    //}

    ////public class OrderStatusResolver : ValueResolver<Order, bool>
    ////{
    ////    protected override bool ResolveCore(Order source)
    ////    {
    ////        if (source.Status == OrderStatus.Submitted)
    ////        {
    ////            return true;
    ////        }
    ////        else
    ////        {
    ////            return false;
    ////        }
    ////    }
    ////}


    //public class MoneyFormatter
    //{
    //    public string FormatValue(ResolutionContext context)
    //    {
    //        if (context.SourceValue is decimal)
    //        {
    //            decimal money = (decimal)context.SourceValue;

    //            return money.FormatMoney();
    //        }

    //        return context.SourceValue.ToString();
    //    }
    //}

    //public class DTFFormatter
    //{
    //    public DateTime FormatValue(ResolutionContext context)
    //    {
    //        if (context.SourceValue is DateTimeOffset)
    //        {
    //            var dt = (DateTimeOffset)context.SourceValue;

    //            return dt == null ? dt.DateTime : DateTime.MinValue;
    //        }

    //        return DateTime.Now;
    //    }
    //}

}
