﻿using NISC_MFP_MVC.Models.DTO;
using NISC_MFP_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using System.Diagnostics;

namespace NISC_MFP_MVC.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        private static readonly string DISABLE = "1";
        private static readonly string ENABLE = "2";
        public ActionResult Department()
        {
            return View();
        }

        [HttpPost]
        [ActionName("department")]
        public ActionResult SearchDepartmentDataTable()
        {
            DataTableRequest dataTableRequest = new DataTableRequest(Request.Form);
            using (MFP_DBEntities db = new MFP_DBEntities())
            {
                List<SearchDepartmentDTO> searchDepartmentResult = InitialData(db);

                dataTableRequest.RecordsTotalGet = searchDepartmentResult.Count;

                searchDepartmentResult = GlobalSearch(searchDepartmentResult, dataTableRequest.GlobalSearchValue);

                searchDepartmentResult = ColumnSearch(searchDepartmentResult, dataTableRequest);

                searchDepartmentResult = searchDepartmentResult.AsQueryable().OrderBy(dataTableRequest.SortColumnProperty + " " + dataTableRequest.SortDirection).ToList();

                dataTableRequest.RecordsFilteredGet = searchDepartmentResult.Count;

                searchDepartmentResult = searchDepartmentResult.Skip(dataTableRequest.Start).Take(dataTableRequest.Length).ToList();

                foreach (SearchDepartmentDTO dto in searchDepartmentResult)
                {
                    dataTableRequest.SearchDTO.Add(dto);
                }

                return Json(new
                {
                    data = dataTableRequest.SearchDTO,
                    draw = dataTableRequest.Draw,
                    recordsTotal = dataTableRequest.RecordsTotalGet,
                    recordsFiltered = dataTableRequest.RecordsFilteredGet
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [NonAction]
        public List<SearchDepartmentDTO> InitialData(MFP_DBEntities db)
        {
            List<SearchDepartmentDTO> searchDepartmentResult = db.tb_department
                .Select(department => new SearchDepartmentDTO
                {
                    serial=department.serial,
                    dept_id = department.dept_id,
                    dept_name = department.dept_name,
                    dept_value = department.dept_value,
                    dept_month_sum = department.dept_month_sum,
                    dept_usable = department.dept_usable == "0" ? "停用" : "啟用",
                    dept_email = department.dept_email,
                }).ToList();
            return searchDepartmentResult;
        }

        [NonAction]
        public List<SearchDepartmentDTO> GlobalSearch(List<SearchDepartmentDTO> searchData, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchData = searchData.Where(
                    p => p.dept_id.ToUpper().Contains(searchValue.ToUpper()) ||
                    p.dept_name.ToUpper().Contains(searchValue.ToUpper()) ||
                    p.dept_value.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                    p.dept_month_sum.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                    p.dept_usable.ToUpper().Contains(searchValue.ToUpper()) ||
                    p.dept_email.ToUpper().Contains(searchValue.ToUpper()) ||
                    p.serial.ToString().ToUpper().Contains(searchValue.ToUpper())
                    ).ToList();
            }
            return searchData;
        }

        [NonAction]
        public List<SearchDepartmentDTO> ColumnSearch(List<SearchDepartmentDTO> searchData, DataTableRequest searchReauest)
        {
            if (!string.IsNullOrEmpty(searchReauest.ColumnSearch_0))
            {
                searchData = searchData.Where(department => department.dept_id.ToUpper().Contains(searchReauest.ColumnSearch_0.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(searchReauest.ColumnSearch_1))
            {
                searchData = searchData.Where(department => department.dept_name.ToUpper().Contains(searchReauest.ColumnSearch_1.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(searchReauest.ColumnSearch_2))
            {
                searchData = searchData.Where(department => department.dept_value.ToString().ToUpper().Contains(searchReauest.ColumnSearch_2.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(searchReauest.ColumnSearch_3))
            {
                searchData = searchData.Where(department => department.dept_month_sum.ToString().ToUpper().Contains(searchReauest.ColumnSearch_3.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(searchReauest.ColumnSearch_4))
            {
                searchData = searchData.Where(department => department.dept_usable.ToUpper().Contains(searchReauest.ColumnSearch_4.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(searchReauest.ColumnSearch_5))
            {
                searchData = searchData.Where(department => department.dept_email.ToUpper().Contains(searchReauest.ColumnSearch_5.ToUpper())).ToList();
            }
            return searchData;
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            SearchDepartmentDTO initialDepartmentDTO = new SearchDepartmentDTO();
            initialDepartmentDTO.dept_usable = DISABLE;
            ViewBag.formTitle = Request["formTitle"];
            return PartialView(initialDepartmentDTO);
        }

        [HttpPost]
        public ActionResult AddDepartment(SearchDepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                tb_department result = new tb_department();
                result.dept_id = string.IsNullOrEmpty(department.dept_id) ? "" : department.dept_id;
                result.dept_name = string.IsNullOrEmpty(department.dept_name) ? "" : department.dept_name;
                result.dept_value = department.dept_value == null ? 0 : department.dept_value;
                result.dept_month_sum = department.dept_month_sum == null ? 0 : department.dept_month_sum;
                result.dept_usable = string.IsNullOrEmpty(department.dept_usable) ? "" : department.dept_usable;
                result.dept_email = string.IsNullOrEmpty(department.dept_email) ? "" : department.dept_email;

                using (MFP_DBEntities db = new MFP_DBEntities())
                {
                    db.tb_department.Add(result);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("Department");
        }

    }
}