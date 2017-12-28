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
using BYBY.Services.Account;
using System.Threading.Tasks;
using BYBY.Services.Models;

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

        static readonly UserManager _userManager = UserFactory.GetUserManager();
        public static string GetNameByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return "";
            }
            var rs = Task.Run(() =>
            {
                var user = _userManager.FindByNameAsync(userName);

                return user;
            }).Result;

            return rs.Name;
        }



        private static int ToInt(this int? val)
        {
            return val.HasValue ? val.Value : 0;
        }
        private static string ToStringData(this object val)
        {
            return val == null ? "" : val.ToString();
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


        public static IList<ConsultationListView> C_To_ConsultationListViews(this IEnumerable<TBConsultation> source)
        {
            IList<ConsultationListView> dest = new List<ConsultationListView>();
            ConsultationListView view;
            foreach (var item in source)
            {
                var MHInfo = item.MedicalHistory;
                view = new ConsultationListView
                {
                    Id = item.Id,
                    FemaleAge = MHInfo.FeMalePatient.Age.ToInt(),
                    FemaleName = MHInfo.FeMalePatient.Name,
                    MaleAge = MHInfo.MalePatient.Age.ToInt(),
                    MaleName = MHInfo.MalePatient.Name,
                    AddTime = item.AddTime.ToDateTimeString(),
                    ConsultationStatus = item.ConsultationStatus.GetEnumDescription(),
                    AddUser = GetNameByUserName(item.AddUserName),
                    Doctor = item.Doctor.Name,
                    Hospital = item.Hospital.Name,
                    RequestDate = item.RequestDate.ToDateString(),
                    MHId = item.MedicalHistory.Id,
                    ConsultationStatusColorClass = GetFontColorClass(item.ConsultationStatus),
                    MHConsultationStatus = item.MedicalHistory.ConsultationStatus.GetEnumDescription(),
                    MHConsultationStatusColorClass = GetFontColorClass(item.MedicalHistory.ConsultationStatus),
                    IsNewest = item.Id == item.MedicalHistory.NewestConsultationId
                };

                dest.Add(view);
            }
            return dest;
        }



        private static string GetFontColorClass(ConsultationStatus status)
        {
            switch (status)
            {
                case ConsultationStatus.No:
                    return "font-grey-salt";
                case ConsultationStatus.Requesting:
                    return "font-purple";
                case ConsultationStatus.Cancel:
                    return "font-red-sunglo";
                case ConsultationStatus.Confirm:
                    return "font-blue-steel";
                case ConsultationStatus.Complete:
                    return "font-green-jungle";
                default:
                    break;
            }
            return "";
        }

        private static string GetReferralFontColorClass(ReferralStatus status)
        {
            switch (status)
            {
                case ReferralStatus.No:
                    return "font-grey-salt";
                case ReferralStatus.Requesting:
                    return "font-purple";
                case ReferralStatus.Cancel:
                    return "font-red-sunglo";
                case ReferralStatus.Confirm:
                    return "font-green-jungle";
                default:
                    break;
            }
            return "";
        }


        public static IList<ReferralListView> C_To_ReferralListViews(this IEnumerable<TBReferral> source)
        {
            IList<ReferralListView> dest = new List<ReferralListView>();
            ReferralListView view;
            foreach (var item in source)
            {
                var MHInfo = item.MedicalHistory;
                view = new ReferralListView
                {
                    Id = item.Id,
                    FemaleAge = MHInfo.FeMalePatient.Age.ToInt(),
                    FemaleName = MHInfo.FeMalePatient.Name,
                    MaleAge = MHInfo.MalePatient.Age.ToInt(),
                    MaleName = MHInfo.MalePatient.Name,
                    AddTime = item.AddTime.ToDateTimeString(),
                    ReferralStatus = item.ReferralStatus.GetEnumDescription(),
                    AddUser = GetNameByUserName(item.AddUserName),
                    Hospital = item.Hospital.Name,
                    RequestDate = item.RequestDate.ToDateString(),
                    MHId = item.MedicalHistory.Id,
                    IsNewest = item.Id == item.MedicalHistory.NewestReferralId,
                    MHReferralStatus = item.MedicalHistory.ReferralStatus.GetEnumDescription(),
                    MHReferralStatusColorClass = GetReferralFontColorClass(item.MedicalHistory.ReferralStatus),
                    ReferralStatusColorClass = GetReferralFontColorClass(item.ReferralStatus),
                    Remark = item.Remark


                };

                dest.Add(view);
            }
            return dest;
        }

        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBHospital> source)
        {
            var dest = new List<SelectItem>();
            SelectItem sitem;
            foreach (var item in source)
            {
                sitem = new SelectItem();
                sitem.id = item.Id.ToString();
                sitem.text = item.Name;
                sitem.title = item.IsMaster ? "1" : "0";
                dest.Add(sitem);
            }
            return dest;
        }

        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBDoctor> source)
        {
            var dest = new List<SelectItem>();
            SelectItem sitem;
            foreach (var item in source)
            {
                sitem = new SelectItem();
                sitem.id = item.Id.ToString();
                sitem.text = item.Name;
                sitem.title = item.JobTitle;
                dest.Add(sitem);
            }
            return dest;
        }

        public static ConsultationDetailModel C_To_ConsultationDetailModel(this TBConsultation source)
        {
            var view = Mapper.Map<ConsultationDetailModel>(source);
            view.FemaleName = source.MedicalHistory.FeMalePatient.Name;
            view.FemaleAge = source.MedicalHistory.FeMalePatient.Age.ToStringData();
            view.MaleName = source.MedicalHistory.MalePatient.Name;
            view.MaleAge = source.MedicalHistory.MalePatient.Age.ToStringData();
            view.RequestUser = GetNameByUserName(source.AddUserName);
            if (source.STime.Date == source.ETime.Date)
            {
                view.RequestDate = source.STime.ToString("yyyy-MM-dd HH:mm") + " - " + source.ETime.ToString("HH:mm");
            }
            else
            {
                view.RequestDate = source.STime.ToString("yyyy-MM-dd HH:mm") + " - " + source.ETime.ToString("yyyy-MM-dd HH:mm");
            }
            //  view.Remark = source.Remark;
            view.Hospital = source.Hospital.Name;
            view.ConsultationStatus = GetEnumDescription(source.ConsultationStatus);
            view.ApprovedUser = GetNameByUserName(source.ApprovedUser);
            view.ApprovedTime = source.ApprovedTime.ToDateTimeString();
            view.Doctor = source.Doctor.Name;
            view.RecordUser = GetNameByUserName(source.RecordUser);
            view.RecordTime = source.RecordTime.ToDateTimeString();
            view.ConsultationMedicineList = source.ConsultationMedicines.C_To_ConsultationMedicineList();
            view.ConsultationCheckList = source.ConsultationChecks.C_To_ConsultationCheckListRequest();
            return view;
        }

        public static IList<ConsultationMedicineListRequest> C_To_ConsultationMedicineList(this IEnumerable<TBConsultationMedicine> source)
        {
            IList<ConsultationMedicineListRequest> views = new List<ConsultationMedicineListRequest>();
            ConsultationMedicineListRequest view;
            foreach (var item in source)
            {
                view = new ConsultationMedicineListRequest();
                view.ActionDate = item.ActionDate.ToDateString();
                view.AllDose = item.AllDose;
                view.UsedDays = item.UsedDays;
                view.Dose = item.Medicine.Dose.ToString();
                view.Frequency = item.Medicine.Frequency;
                view.Id = item.Id;
                view.Instructions = item.Medicine.Instructions;
                view.MedicineId = item.MedicineId;
                view.ShortName = item.Medicine.ShortName;
                view.Unit = item.Medicine.Unit;
                view.ConsultationId = item.ConsultationId;
                views.Add(view);
            }
            return views;
        }

        public static IList<ConsultationCheckListRequest> C_To_ConsultationCheckListRequest(this IEnumerable<TBConsultationCheck> source)
        {
            IList<ConsultationCheckListRequest> views = new List<ConsultationCheckListRequest>();
            ConsultationCheckListRequest view;
            foreach (var item in source)
            {
                view = new ConsultationCheckListRequest();
                view.ActionDate = item.ActionDate.ToDateString();
                view.CheckId = item.CheckId;
                view.CheckName = item.CheckAssay.Name;
                view.ConsultationId = item.ConsultationId;
                view.Id = item.Id;
                views.Add(view);
            }
            return views;
        }

        public static IList<DoctorListView> C_To_DoctorListView(this IEnumerable<TBDoctor> source)
        {
            IList<DoctorListView> views = new List<DoctorListView>();
            DoctorListView view;
            foreach (var item in source)
            {
                view = new DoctorListView();
                view.Age = item.Birthday.GetAge();
                view.Hospital = item.Hospital.Name;
                view.IsMasterDoctor = item.IsMasterDoctor ? "是" : "否";
                view.JobTitle = item.JobTitle;
                view.Name = item.Name;
                view.Sex = item.Sex.GetEnumDescription();
                view.Id = item.Id;
                views.Add(view);
            }
            return views;
        }
    }
}
