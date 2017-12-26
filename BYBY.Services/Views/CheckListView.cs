using BYBY.Infrastructure;

namespace BYBY.Services.Views
{
    public class CheckListView
    {
        public int Id { get; set; }


        /// <summary>
        /// 项目分类编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 项目分类名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 化验说明
        /// </summary>
        public AssayType? AssayType { get; set; }



        public string AssayTypeText { get; set; }

        /// <summary>
        ///  所属上级项目
        /// </summary>
        public int? ParentItem { get; set; }


        /// <summary>
        /// 有效时长
        /// </summary>
        public int? EffectiveTime { get; set; }


        /// <summary>
        /// 模式
        /// </summary>
        public CheckMode? CheckMode { get; set; }

        public string CheckModeText { get; set; }



    }
}
