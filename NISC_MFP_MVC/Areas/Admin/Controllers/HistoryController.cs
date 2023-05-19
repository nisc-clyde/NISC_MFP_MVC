﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using NISC_MFP_MVC.ViewModels;
using NISC_MFP_MVC_Common;
using NISC_MFP_MVC_Service.Implement;
using NISC_MFP_MVC_Service.Interface;
using System.Linq;
using System.Web.Mvc;
using MappingProfile = NISC_MFP_MVC.Models.MappingProfile;

namespace NISC_MFP_MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "history")]
    public class HistoryController : Controller, IDataTableController<HistoryViewModel>
    {
        private IHistoryService historyService;
        private Mapper mapper;

        /// <summary>
        /// Service和AutoMapper初始化
        /// </summary>
        public HistoryController()
        {
            historyService = new HistoryService();
            mapper = InitializeAutomapper();
        }

        /// <summary>
        /// History Index View
        /// </summary>
        /// <returns>reutrn Index View</returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("InitialDataTable")]
        public ActionResult SearchDataTable()
        {
            DataTableRequest dataTableRequest = new DataTableRequest(Request.Form);
            IQueryable<HistoryViewModel> searchPrintResultDetail = InitialData(dataTableRequest);

            return Json(new
            {
                data = searchPrintResultDetail,
                draw = dataTableRequest.Draw,
                recordsFiltered = dataTableRequest.RecordsFilteredGet
            }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public IQueryable<HistoryViewModel> InitialData(DataTableRequest dataTableRequest)
        {
            return historyService.GetAll(dataTableRequest).ProjectTo<HistoryViewModel>(mapper.ConfigurationProvider);
        }

        //[HttpPost]
        //[ActionName("InitialDataTable")]
        //public ActionResult SearchHistoryDataTable()
        //{
        //    DataTableRequest dataTableRequest = new DataTableRequest(Request.Form);
        //    IQueryable<HistoryViewModel> searchHistoryResultDetail = InitialData();
        //    dataTableRequest.RecordsTotalGet = searchHistoryResultDetail.AsQueryable().Count();
        //    //searchHistoryResultDetail = GlobalSearch(searchHistoryResultDetail, dataTableRequest.GlobalSearchValue);
        //    searchHistoryResultDetail = searchHistoryResultDetail.AsQueryable().OrderBy(dataTableRequest.SortColumnProperty + " " + dataTableRequest.SortDirection);
        //    dataTableRequest.RecordsFilteredGet = searchHistoryResultDetail.AsQueryable().Count();
        //    searchHistoryResultDetail = searchHistoryResultDetail.Skip(dataTableRequest.Start).Take(dataTableRequest.Length);

        //    return Json(new
        //    {
        //        data = searchHistoryResultDetail,
        //        draw = dataTableRequest.Draw,
        //        recordsTotal = dataTableRequest.RecordsTotalGet,
        //        recordsFiltered = dataTableRequest.RecordsFilteredGet
        //    }, JsonRequestBehavior.AllowGet);
        //}


        //[NonAction]
        //public IQueryable<HistoryViewModel> InitialData()
        //{
        //    IQueryable<HistoryInfo> resultModel = _historyService.GetAll();
        //    IQueryable<HistoryViewModel> viewmodel = resultModel.ProjectTo<HistoryViewModel>(mapper.ConfigurationProvider);

        //    return viewmodel;
        //}

        //[NonAction]
        //public IQueryable<HistoryViewModel> GlobalSearch(IQueryable<HistoryViewModel> searchData, string searchValue)
        //{
        //    IQueryable<HistoryInfo> viewmodelBefore = searchData.ProjectTo<HistoryInfo>(mapper.ConfigurationProvider);
        //    IQueryable<HistoryViewModel> viewmodelAfter = _historyService.GetWithGlobalSearch(viewmodelBefore, searchValue).ProjectTo<HistoryViewModel>(mapper.ConfigurationProvider);

        //    return viewmodelAfter;
        //}

        //[NonAction]
        //public List<SearchHistoryDTO> GlobalSearch(List<SearchHistoryDTO> searchData, string searchValue)
        //{
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        searchData = searchData.Where(
        //            p => p.date_time.ToUpper().Contains(searchValue.ToUpper()) ||
        //            p.login_user_id.ToUpper().Contains(searchValue.ToUpper()) ||
        //            p.login_user_name.ToUpper().Contains(searchValue.ToUpper()) ||
        //            p.operation.ToUpper().Contains(searchValue.ToUpper()) ||
        //            p.affected_data.ToUpper().Contains(searchValue.ToUpper())
        //            ).ToList();
        //    }
        //    return searchData;
        //}

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