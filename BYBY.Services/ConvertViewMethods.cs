using BYBY.Repository.Entities;
using BYBY.Services.Request;
using BYBY.Services.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure;

namespace BYBY.Services
{
    public static class ConvertViewMethods
    {
        public static IList<MedicalHistoryListView> C_To_MedicalHistoryListViews(this IEnumerable<TBMedicalHistory> source)
        {
            IList<MedicalHistoryListView> dest = new List<MedicalHistoryListView>();
            MedicalHistoryListView view;
            foreach (var item in source)
            {
                view = new MedicalHistoryListView
                {
                    FeMaleAge = item.FeMalePatient.Age.ToInt(),
                    FeMaleName = item.FeMalePatient.Name,
                    MaleAge = item.MalePatient.Age.ToInt(),
                    MaleName = item.MalePatient.Name,
                    Addtime = item.AddTime.ToDateTimeString(),
                    ConsultationStatus = item.ConsultationStatus.GetEnumDescription(),
                    ReferralStatus = item.ReferralStatus.GetEnumDescription(),
                    FeMalePhone = item.FeMalePatient.ContactPhone,
                    MalePhone = item.MalePatient.ContactPhone,

                    FeMaleBirthday = item.FeMalePatient.Birthday.ToDateString(),
                    //    FeMaleAddUserName= item.FeMalePatient.AddUserName,
                    FeMaleFixPhone = item.LandlinePhone,
                    FeMaleMarrad = item.FeMalePatient.MaritalStatus.GetEnumDescription(),

                    MaleBirthday = item.MalePatient.Birthday.ToDateString(),
                    AddUserName = item.AddUserName,
                    MaleFixPhone = item.LandlinePhone,
                    MaleMarrad = item.MalePatient.MaritalStatus.GetEnumDescription()
                };
                dest.Add(view);
            }
            return dest;
        }

        public static TBPatient C_To_FemaleTBPatient(this MedicalHistoryAddRequest source)
        {
            var female = new TBPatient();
            female.Birthday = source.FemaleBirthday.ToDate();
            female.Age = female.Birthday.GetAge();
            female.CardNo = source.FemaleCardNo;
            female.CardType = source.FemaleCardType.HasValue ? source.FemaleCardType.Value : CardType.SFZ;
            female.ContactPhone = source.FemalePhone;
            female.EthnicId = source.FemaleEthnic;
            female.HouseholdAddress = source.FemaleHouseholdAddress;
            female.JobId = source.FemaleJob;
            female.MaritalStatus = source.FemaleMarriage.HasValue ? source.FemaleMarriage.Value : MaritalStatus.YiHun;
            female.Name = source.FemaleName;
            female.NationaId = source.FemaleNation;
            female.NativePlace = source.FemaleNativePlace;
            female.Sex = Sex.Female;
            return female;

        }

        public static TBPatient C_To_MaleTBPatient(this MedicalHistoryAddRequest source)
        {
            var male = new TBPatient();
            male.Birthday = source.MaleBirthday.ToDate();
            male.Age = male.Birthday.GetAge();
            male.CardNo = source.MaleCardNo;
            male.CardType = source.MaleCardType.HasValue ? source.MaleCardType.Value : CardType.SFZ;
            male.ContactPhone = source.MalePhone;
            male.EthnicId = source.MaleEthnic;
            male.HouseholdAddress = source.MaleHouseholdAddress;
            male.JobId = source.MaleJob;
            male.MaritalStatus = source.MaleMarriage.HasValue ? source.MaleMarriage.Value : MaritalStatus.YiHun;
            male.Name = source.MaleName;
            male.NationaId = source.MaleNation;
            male.NativePlace = source.MaleNativePlace;
            male.Sex = Sex.Male;
            return male;

        }


        private static int ToInt(this int? val)
        {
            return val.HasValue ? val.Value : 0;
        }
        private static string ToDateTimeString(this DateTime? val)
        {
            return val.HasValue ? val.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
        }

        private static string ToDateString(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 根据枚举得到描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            if (value == null)
            {
                return "未知";
            }
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return "未知";
            }
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }


    }
}
