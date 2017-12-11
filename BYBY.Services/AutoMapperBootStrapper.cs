using AutoMapper;
using BYBY.Repository.Entities;
using BYBY.Services.Request;

namespace BYBY.Services
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {

            Mapper.Initialize(cfg => {
                cfg.CreateMap<TBMedicalDetail, MedicalDetailRequest>();
            });

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
