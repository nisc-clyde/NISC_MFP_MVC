﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NISC_MFP_MVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MFP_DBEntities : DbContext
    {
        public MFP_DBEntities()
            : base("name=MFP_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<doc_detail> doc_detail { get; set; }
        public virtual DbSet<doc_mng> doc_mng { get; set; }
        public virtual DbSet<tb_card> tb_card { get; set; }
        public virtual DbSet<tb_cardreader> tb_cardreader { get; set; }
        public virtual DbSet<tb_department> tb_department { get; set; }
        public virtual DbSet<tb_group> tb_group { get; set; }
        public virtual DbSet<tb_logs_deposit> tb_logs_deposit { get; set; }
        public virtual DbSet<tb_logs_history> tb_logs_history { get; set; }
        public virtual DbSet<tb_logs_remote_history> tb_logs_remote_history { get; set; }
        public virtual DbSet<tb_logs_sentmail> tb_logs_sentmail { get; set; }
        public virtual DbSet<tb_pdfback> tb_pdfback { get; set; }
        public virtual DbSet<tb_scan> tb_scan { get; set; }
        public virtual DbSet<tb_use_page> tb_use_page { get; set; }
        public virtual DbSet<tb_user> tb_user { get; set; }
        public virtual DbSet<tb_watermark> tb_watermark { get; set; }
        public virtual DbSet<tb_logs_cardreader> tb_logs_cardreader { get; set; }
        public virtual DbSet<tb_logs_import> tb_logs_import { get; set; }
        public virtual DbSet<tb_logs_print> tb_logs_print { get; set; }
        public virtual DbSet<tb_mfp> tb_mfp { get; set; }
        public virtual DbSet<tb_print_price> tb_print_price { get; set; }
        public virtual DbSet<tb_token> tb_token { get; set; }
        public virtual DbSet<tblusers> tblusers { get; set; }
        public virtual DbSet<watermark> watermark { get; set; }
    }
}