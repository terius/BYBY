using BYBY.Repository.Entities;
using BYBY.Services.Views;
using System.Collections.Generic;

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
                    MaleName = item.MalePatient.Name
                };

                dest.Add(view);
            }
            return dest;
        }


        private static int ToInt(this int? val)
        {
            return val.HasValue ? val.Value : 0;
        }
    }
}
