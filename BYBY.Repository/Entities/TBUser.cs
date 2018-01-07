using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BYBY.Repository.Entities
{
    public class TBUser : BaseEntity<string>, IUser
    {

        public TBUser()
        {
            //  this.Id = Guid.NewGuid().ToString();
            this.UserRoles = new HashSet<TBUserRole>();
            this.Doctors = new HashSet<TBDoctor>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }


        [Required]
        [StringLength(20)]
        public string UserName { get; set; }


        [Required]
        [StringLength(30)]
        public string Name { get; set; }


        [Required]
        [StringLength(200)]
        public string Password { get; set; }



        public DateTime? LastLoginTime { get; set; }

        public int? HospitalId { get; set; }

        public virtual ICollection<TBUserRole> UserRoles { get; set; }

        public virtual ICollection<TBDoctor> Doctors { get; set; }

        [ForeignKey("HospitalId")]
        public virtual TBHospital Hospital { get; set; }

        protected override void Validate()
        {
            base.Validate();

        }

        public IList<TBModule> GetModules(string roleName)
        {

            return UserRoles.FirstOrDefault(d => d.Role.Name == roleName).Role.RoleModules.Select(d => d.Module).OrderBy(d => d.OrderBy).ToList();


        }
        [NotMapped]
        public string RoleName
        {
            get
            {
                return UserRoles.First().Role.Name;

            }
        }


        [NotMapped]
        public bool IsDoctor
        {
            get
            {
                return RoleName == "doctor";

            }
        }

        [NotMapped]
        public bool IsMasterDoctor
        {
            get
            {
                if (!IsDoctor)
                {
                    return false;
                }
                var d = Doctors.FirstOrDefault();
                return d == null ? false : d.IsMasterDoctor;

            }
        }

        [NotMapped]
        public bool IsChildDoctor
        {
            get
            {
                if (!IsDoctor)
                {
                    return false;
                }
                var d = Doctors.FirstOrDefault();
                var rs = d == null ? false : d.IsMasterDoctor;
                return !rs;
            }
        }

        [NotMapped]
        public int MasterHospitalId
        {
            get
            {
                if (!HospitalId.HasValue)
                {
                    return 0;
                }
                //if (!IsDoctor)
                //{
                //    return 0;
                //}
                //var hospital = Doctors.First().Hospital;
                return Hospital.ParentHospitalId.HasValue ? Hospital.ParentHospital.Id : HospitalId.Value;
            }
        }

        [NotMapped]
        public int DoctorId
        {
            get
            {
                var doctor = Doctors.FirstOrDefault();
                return doctor == null ? 0 : doctor.Id;

            }
        }

        [NotMapped]
        public string UserImg
        {
            get
            {
                var doctor = Doctors.FirstOrDefault();
                return doctor == null ? "" : doctor.ImageUrl;

            }
        }

        //[NotMapped]
        //public int HospitalId
        //{
        //    get
        //    {
        //        var doctor = Doctors.FirstOrDefault();
        //        return doctor == null ? 0 : doctor.Hospital.Id;

        //    }
        //}


    }
}
