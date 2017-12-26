using System;

namespace BYBY.Services.Request
{
    public class ConsultationMedicineListRequest
    {
        public int Id { get; set; }

        /// <summary>
        /// 药品Id
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// 会诊Id
        /// </summary>
        public int ConsultationId { get; set; }


        /// <summary>
        /// 药品名称
        /// </summary>
        public string ShortName { get; set; }


        /// <summary>
        /// 剂量
        /// </summary>
        public string Dose { get; set; }


        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }


        /// <summary>
        /// 频次
        /// </summary>
        public string Frequency { get; set; }



        /// <summary>
        /// 使用方法
        /// </summary>
        public string Instructions { get; set; }


        /// <summary>
        /// 用药天数
        /// </summary>
        public int UsedDays { get; set; }


        /// <summary>
        /// 总剂量
        /// </summary>
        public int AllDose { get; set; }


        /// <summary>
        /// 执行日期
        /// </summary>
        public string ActionDate { get; set; }
    }
}
