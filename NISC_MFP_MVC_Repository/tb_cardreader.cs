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
    
    public partial class tb_cardreader
    {
        public int serial { get; set; }
        public string cr_id { get; set; }
        public string cr_ip { get; set; }
        public string cr_port { get; set; }
        public string cr_type { get; set; }
        public string cr_mode { get; set; }
        public string cr_card_switch { get; set; }
        public Nullable<System.DateTime> history_date { get; set; }
        public Nullable<System.DateTime> card_update_date { get; set; }
        public string cr_status { get; set; }
        public string cr_version { get; set; }
        public string cr_relay_status { get; set; }
    }
}
