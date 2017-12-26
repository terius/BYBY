using System;

namespace BYBY.Services.Request
{
    public class ConsultationCheckListRequest
    {
        public int Id { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        public int CheckId { get; set; }

        /// <summary>
        /// 会诊Id
        /// </summary>
        public int ConsultationId { get; set; }


        /// <summary>
        /// 检查项名称
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public string ActionDate { get; set; }
    }
}
