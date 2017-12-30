using AutoMapper;
using BYBY.Repository.Entities;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Views;
using System;
using BYBY.Infrastructure.Helpers;

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
                cfg.CreateMap<bool, string>().ConvertUsing<BooleanToStringConverter>();
                cfg.CreateMap<TBMedicalDetail, MedicalDetailRequest>()
                .ForMember(d => d.ManLastMarriageDate,
                expression => expression.ResolveUsing(s => s.ManLastMarriageDate.ToDateString()))
                .ForMember(d => d.MarriageLastPregnancyDate,
                expression => expression.ResolveUsing(s => s.MarriageLastPregnancyDate.ToDateString()))
                  .ForMember(d => d.MenstruationLast,
                expression => expression.ResolveUsing(s => s.MenstruationLast.ToDateString()));
                cfg.CreateMap<MedicalDetailRequest, TBMedicalDetail>();
                cfg.CreateMap<MedicalDetailAddRequest, TBMedicalDetail>();
                cfg.CreateMap<ConsultationAddRequest, TBConsultation>();
                cfg.CreateMap<ReferralAddRequest, TBReferral>();
                cfg.CreateMap<TBMedicalHistoryImage, MedicalHistoryImageRequest>();
                cfg.CreateMap<TBConsultation, ConsultationDetailModel>();
                cfg.CreateMap<ConsultationRecordEditRequest, TBConsultation>();
                cfg.CreateMap<TBMedicine, MedicineListView>().ForMember(d => d.InjectionMarkText,
                expression => expression.ResolveUsing(s => s.InjectionMark == true ? "是" : "否"))
                .ForMember(d => d.IsUsedText,
                expression => expression.ResolveUsing(s => s.IsUsed == true ? "是" : "否"));
                cfg.CreateMap<MedicineAddRequest, TBMedicine>();
                cfg.CreateMap<ConsultationMedicineListRequest, TBConsultationMedicine>();


                cfg.CreateMap<TBCheckAssay, CheckListView>().ForMember(d => d.AssayTypeText,
           expression => expression.ResolveUsing(s => s.AssayType.HasValue ? s.AssayType.GetEnumDescription() : ""))
           .ForMember(d => d.CheckModeText,
           expression => expression.ResolveUsing(s => s.CheckMode.HasValue? s.CheckMode.GetEnumDescription() : ""));


                cfg.CreateMap<CheckListView, TBCheckAssay>();

                cfg.CreateMap<ConsultationCheckListRequest, TBConsultationCheck>();

                cfg.CreateMap<ConsultationRoomListView, TBConsultationRoom>();
                cfg.CreateMap<TBConsultationRoom, ConsultationRoomListView>().ForMember(d => d.Pic,
           expression => expression.ResolveUsing(s => string.IsNullOrWhiteSpace(s.Pic) ? "/images/doctor.png" : s.Pic));

                cfg.CreateMap<TBDateSetup, DateSetupListView>().ForMember(d => d.STime,
     expression => expression.ResolveUsing(s => s.STime.ToString("HH:mm"))).ForMember(d => d.ETime,
     expression => expression.ResolveUsing(s => s.ETime.ToString("HH:mm")));

                cfg.CreateMap<TBPlan, PlanView>()
                .ForMember(d => d.DoctorName, expression => expression.ResolveUsing(s => s.Doctor.Name))
                .ForMember(d => d.RoomName, expression => expression.ResolveUsing(s => s.Room.Name));
                cfg.CreateMap<PlanView, TBPlan>();
            });

        }

        public class DateTimeHasNullToStringConverter : ITypeConverter<DateTime?, string>
        {
            public string Convert(DateTime? source, string value, ResolutionContext option)
            {
                if (source.HasValue)
                {
                    return source.Value.ToString("yyyy-MM-dd HH:mm:ss");
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
                if (source != DateTime.MaxValue && source != DateTime.MinValue)
                {
                    return source.ToString("yyyy-MM-dd HH:mm:ss");
                }

                return string.Empty;
            }
        }

        public class BooleanToStringConverter : ITypeConverter<bool, string>
        {
            public string Convert(bool source, string value, ResolutionContext option)
            {
                return source ? "是" : "否";
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
