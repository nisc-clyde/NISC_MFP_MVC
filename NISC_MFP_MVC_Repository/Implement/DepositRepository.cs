﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using NISC_MFP_MVC_Common;
using NISC_MFP_MVC_Repository.DTOs.Deposit;
using NISC_MFP_MVC_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace NISC_MFP_MVC_Repository.Implement
{
    public class DepositRepository : IDepositRepository
    {
        protected MFP_DBEntities _db { get; private set; }
        private Mapper _mapper;

        public DepositRepository()
        {
            _db = new MFP_DBEntities();
            _mapper = InitializeAutomapper();
        }

        public void Insert(InitialDepositRepoDTO instance)
        {
            //NOP
        }

        public IQueryable<InitialDepositRepoDTO> GetAll()
        {
            return _db.tb_logs_deposit.ProjectTo<InitialDepositRepoDTO>(_mapper.ConfigurationProvider);
        }

        public IQueryable<InitialDepositRepoDTO> GetAll(DataTableRequest dataTableRequest)
        {
            string[] columns = {
                "user_name",
                "user_id",
                "card_id",
                "card_user_id",
                "card_user_name",
                "pbalance",
                "deposit_value",
                "final_value",
                "deposit_date"
            };

            string[] searches = {
                dataTableRequest.ColumnSearch_0,
                dataTableRequest.ColumnSearch_1,
                dataTableRequest.ColumnSearch_2,
                dataTableRequest.ColumnSearch_3,
                dataTableRequest.ColumnSearch_4,
                dataTableRequest.ColumnSearch_5,
                dataTableRequest.ColumnSearch_6,
                dataTableRequest.ColumnSearch_7,
                dataTableRequest.ColumnSearch_8,
            };

            IQueryable<InitialDepositRepoDTO> tb_Logs_Deposit = _db.tb_logs_deposit.AsNoTracking().ProjectTo<InitialDepositRepoDTO>(_mapper.ConfigurationProvider);

            //GlobalSearch
            tb_Logs_Deposit = GetWithGlobalSearch(tb_Logs_Deposit, dataTableRequest.GlobalSearchValue);

            //Column Search
            tb_Logs_Deposit = GetWithColumnSearch(tb_Logs_Deposit, columns, searches);

            tb_Logs_Deposit = tb_Logs_Deposit.OrderBy(dataTableRequest.SortColumnProperty + " " + dataTableRequest.SortDirection);
            //-----------------Performance BottleNeck-----------------
            dataTableRequest.RecordsFilteredGet = tb_Logs_Deposit.Count();
            //-----------------Performance BottleNeck-----------------
            tb_Logs_Deposit = tb_Logs_Deposit.Skip(dataTableRequest.Start).Take(dataTableRequest.Length);

            List<InitialDepositRepoDTO> takeTenRecords = tb_Logs_Deposit.ToList();

            return takeTenRecords.AsQueryable().AsNoTracking();
        }

        public IQueryable<InitialDepositRepoDTO> GetWithGlobalSearch(IQueryable<InitialDepositRepoDTO> source, string search)
        {
            source = source
                .Where(p =>
                ((!string.IsNullOrEmpty(p.user_name)) && p.user_name.ToUpper().Contains(search.ToUpper())) ||
                ((!string.IsNullOrEmpty(p.user_id)) && p.user_id.ToUpper().Contains(search.ToUpper())) ||
                ((!string.IsNullOrEmpty(p.card_id)) && p.card_id.ToUpper().Contains(search.ToUpper())) ||
                ((!string.IsNullOrEmpty(p.card_user_id)) && p.card_user_id.ToUpper().Contains(search.ToUpper())) ||
                ((!string.IsNullOrEmpty(p.card_user_name)) && p.card_user_name.ToUpper().Contains(search.ToUpper())) ||
                ((p.pbalance != null) && p.pbalance.ToString().ToUpper().Contains(search.ToUpper())) ||
                ((p.deposit_value != null) && p.deposit_value.ToString().Contains(search.ToUpper())) ||
                ((p.final_value != null) && p.final_value.ToString().ToUpper().Contains(search.ToUpper())) ||
                ((!string.IsNullOrEmpty(p.deposit_date.ToString())) && p.deposit_date.ToString().ToUpper().Contains(search.ToUpper())));

            return source;
        }

        public IQueryable<InitialDepositRepoDTO> GetWithColumnSearch(IQueryable<InitialDepositRepoDTO> source, string[] columns, string[] searches)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                if (!string.IsNullOrEmpty(searches[i]))
                {
                    source = source.Where(columns[i] + "!=null &&" + columns[i] + ".ToString().ToUpper().Contains(@0)", searches[i].ToString().ToUpper());
                }
            }
            return source;
        }

        public InitialDepositRepoDTO Get(string column, string value, string operation)
        {
            tb_logs_deposit result = _db.tb_logs_deposit.Where(column + operation, value).FirstOrDefault();
            return _mapper.Map<tb_logs_deposit, InitialDepositRepoDTO>(result);
        }


        public void Update(InitialDepositRepoDTO instance)
        {
            var dataModel = _mapper.Map<InitialDepositRepoDTO, tb_logs_deposit>(instance);
            _db.Entry(dataModel).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(InitialDepositRepoDTO instance)
        {
            var dataModel = _mapper.Map<InitialDepositRepoDTO, tb_logs_deposit>(instance);
            _db.Entry(dataModel).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }

        /// <summary>
        /// 建立AutoMapper配置
        /// </summary>
        /// <returns></returns>
        private Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
