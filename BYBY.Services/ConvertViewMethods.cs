using BYBY.Repository.Entities;
using BYBY.Services.Request;
using BYBY.Services.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure;
using AutoMapper;

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
                    Id = item.Id,
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

        public static TBPatient C_To_FemaleTBPatient(this MedicalHistoryAddRequest source, TBPatient female = null)
        {
            if (female == null)
            {
                female = new TBPatient();
            }
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
            female.Education = source.FemaleEducation;

            return female;

        }

        public static TBPatient C_To_MaleTBPatient(this MedicalHistoryAddRequest source, TBPatient male = null)
        {
            if (male == null)
            {
                male = new TBPatient();
            }
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
            male.Education = source.MaleEducation;
            return male;

        }

        public static MedicalHistoryEditRequest C_To_EditView(this TBMedicalHistory source)
        {
            var view = new MedicalHistoryEditRequest();
            var female = source.FeMalePatient;
            view.MHId = source.Id;
            view.Address = source.Address;
            view.FixPhone = source.LandlinePhone;
            view.Remark = source.Remark;
            view.MedicalHistoryNo = source.MedicalHistoryNo;

            view.FemaleId = female.Id;
            view.FemaleBirthday = female.Birthday.ToDateString();
            view.FemaleCardNo = female.CardNo;
            view.FemaleCardType = female.CardType;
            view.FemaleEducation = female.Education;
            view.FemaleEthnic = female.EthnicId;
            view.FemaleHouseholdAddress = female.HouseholdAddress;
            view.FemaleJob = female.JobId;
            view.FemaleMarriage = female.MaritalStatus;
            view.FemaleName = female.Name;
            view.FemaleNation = female.NationaId;
            view.FemaleNativePlace = female.NativePlace;
            view.FemalePhone = female.ContactPhone;
            view.FemaleAge = female.Birthday.GetAge();

            view.FemaleCardTypeText = female.CardType.GetEnumDescription();
            view.FemaleEducationText = female.Education.GetEnumDescription();
            view.FemaleEthnicText = female.Ethnic == null ? "" : female.Ethnic.Name;
            view.FemaleJobText = female.Job == null ? "" : female.Job.Name;
            view.FemaleMarriageText = female.MaritalStatus.GetEnumDescription();
            view.FemaleNationText = female.Nationality == null ? "" : female.Nationality.Chinese;

            var male = source.MalePatient;
            view.MaleId = male.Id;
            view.MaleBirthday = male.Birthday.ToDateString();
            view.MaleCardNo = male.CardNo;
            view.MaleCardType = male.CardType;
            view.MaleEducation = male.Education;
            view.MaleEthnic = male.EthnicId;
            view.MaleHouseholdAddress = male.HouseholdAddress;
            view.MaleJob = male.JobId;
            view.MaleMarriage = male.MaritalStatus;
            view.MaleName = male.Name;
            view.MaleNation = male.NationaId;
            view.MaleNativePlace = male.NativePlace;
            view.MalePhone = male.ContactPhone;
            view.MaleAge = male.Birthday.GetAge();

            view.MaleCardTypeText = male.CardType.GetEnumDescription();
            view.MaleEducationText = male.Education.GetEnumDescription();
            view.MaleEthnicText = male.Ethnic == null ? "" : male.Ethnic.Name;
            view.MaleJobText = male.Job == null ? "" : male.Job.Name;
            view.MaleMarriageText = male.MaritalStatus.GetEnumDescription();
            view.MaleNationText = male.Nationality == null ? "" : male.Nationality.Chinese;

            return view;
        }


        public static IList<MedicalDetailRequest> C_To_MedicalDetailRequests(this IEnumerable<TBMedicalDetail> source)
        {
            IList<MedicalDetailRequest> dest = new List<MedicalDetailRequest>();
            MedicalDetailRequest view;
            foreach (var item in source)
            {
                view = item.C_To_MedicalDetailRequest();
                dest.Add(view);
            }
            return dest;
        }

        public static MedicalDetailRequest C_To_MedicalDetailRequest(this TBMedicalDetail source)
        {
            var view = Mapper.Map<MedicalDetailRequest>(source);
            view.MDId = source.Id;
            return view;
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
