﻿namespace NISC_MFP_MVC_Common.Logger
{
    public class DepartmentLogHandler : AbstractLogHandler
    {
        public override object LogHandle(string type, string operate, object data)
        {
            LogRequest logRequest = data as LogRequest;
            LogResponse logResponse = new LogResponse();
            if (type == "Department")
            {
                switch (operate)
                {
                    case "Add":
                        logResponse.Operation = "新增部門";
                        logResponse.Message = $"(Id={logRequest.NewId}, Name={logRequest.NewContent})";
                        return logResponse;
                    case "Edit":
                        logResponse.Operation = "修改部門";
                        logResponse.Message = $"(原)：(Id={logRequest.OldId}, Name={logRequest.OldContent}) \n" +
                            $"(新)：(Id={logRequest.NewId}, Name={logRequest.NewContent})";
                        return logResponse;
                    case "Delete":
                        logResponse.Operation = "刪除部門";
                        logResponse.Message = $"(Id={logRequest.OldId}, Name={logRequest.OldContent})";
                        return logResponse;
                    default:
                        logResponse.Operation = "未知操作";
                        logResponse.Message = $"(Id={logRequest.OldId}, Name={logRequest.OldContent})";
                        return logResponse;
                }
            }
            else
            {
                return base.LogHandle(type, operate, data);
            }
        }
    }
}