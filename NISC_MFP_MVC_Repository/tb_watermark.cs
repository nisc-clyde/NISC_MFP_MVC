//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NISC_MFP_MVC_Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_watermark
    {
        public int id { get; set; }
        public int type { get; set; }
        public int left_offset { get; set; }
        public int right_offset { get; set; }
        public int top_offset { get; set; }
        public int bottom_offset { get; set; }
        public int position_mode { get; set; }
        public string image_path { get; set; }
        public Nullable<int> fill_mode { get; set; }
        public string text { get; set; }
        public Nullable<int> horizontal_alignment { get; set; }
        public Nullable<int> vertical_alignment { get; set; }
        public string color { get; set; }
        public string font_name { get; set; }
        public Nullable<int> font_height { get; set; }
        public Nullable<float> rotation { get; set; }
    }
}
