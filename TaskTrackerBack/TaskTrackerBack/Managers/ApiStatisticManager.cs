using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using IdentityModel.Client;
using Newtonsoft.Json;
using TaskTrackerBack.Models.OutputModels;
using TrackerLib;
using TrackerLib.Enums;
using TrackerLib.Managers;
using TrackerLib.Models;
using TrackerLib.Models.OutputModels;

namespace TaskTrackerBack.Managers {
    public class FormPathModel {
        public FormPathModel(string path) {
            Path = path;
        }
        [JsonProperty("path")]
        public string Path { get; set; }
    }
    public static class ApiStatisticManager {

        public static HttpResponseMessage GetForm(string apikey, int userId, DateTime? start, DateTime? finish) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey))
                        return null;

                    var user = CommonManager.GetUserDataByApikey(apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return null;
                    }
                    var statistic = GetStatisticList(db);

                    var userForStat = db.UserProfiles.FirstOrDefault(a => a.UserId
                    == (userId == 0 ? ((InternalUserModel)user.Data).UserId : userId)
                    && !a.IsDeleted);
                    if (userForStat == null)
                        return null;

                    if (start != null && finish != null
                         && start < DateTime.Now
                          && finish <= DateTime.Now
                           && start < finish) {
                        statistic = statistic.Where(a => a.DateCreate >= start
                        && a.DateCreate < finish).ToList();
                    } else {
                        if (start != null && start < DateTime.Now) {
                            statistic = statistic.Where(a => a.DateCreate >= start).ToList();
                        }

                        if (finish != null && finish <= DateTime.Now) {
                            statistic = statistic.Where(a => a.DateCreate < finish).ToList();
                        }
                    }



                    var useStat = statistic.Where(a => a.UserId 
                    == (userId == 0 ? ((InternalUserModel)user.Data).UserId : userId))
                    .ToList();
                    if (useStat == null ||
                        useStat.Count == 0) 
                        return null;
                    var formModel = new FormCreateModel(useStat);
                    var form = String.Format(formModel.UserStatisticForm,
                        formModel.FullName,
                        formModel.Start,
                        formModel.Finish,
                        formModel.TotalCount,
                        formModel.TotalNew,
                        formModel.TotalDev,
                        formModel.TotalQA,
                        formModel.TotalReady,
                        formModel.CountClosedInTime,
                        formModel.CountOvertime,
                        formModel.CountOpened,
                        formModel.CountHours);

                    CommonManager.WritePdfFile("tasks_form.pdf", form);
                    var res = DownloadText("tasks_form.pdf");

                    return res;
                }
            } catch (Exception ex) {
                return null;
            }
        }


        public static void save_pdf(string fileName) {
            try {

                string pdfPath = @"C:\\Users\\Inna\";
                var formFieldMap = PDFHelper.GetFormFieldNames(pdfPath);

                string username = "Test";
                string password = "12345";

                var pdfContents = PDFHelper.GeneratePDF(pdfPath, formFieldMap);

                File.WriteAllBytes(Path.Combine(pdfPath, fileName), pdfContents);

                WebRequest request = WebRequest.Create(Path.Combine(pdfPath, fileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(username, password);
                Stream reqStream = request.GetRequestStream();
                reqStream.Close();
            } catch (Exception) {

            }

        }

        public static List<InternalTaskModel> GetStatisticList(TaskTrackerEntities db) {
            try {
                var users = db.UserProfiles.Where(a => !a.IsAdmin && !a.IsDeleted).ToList();
                var list = new List<InternalTaskModel>();
                var tasks = db.Tasks
                    .Where(a => a.IsActive && !a.UserProfiles.IsDeleted)
                    .OrderBy(a => a.UserId)
                    .ToList();
                tasks.ForEach(task => {
                    list.Add(new InternalTaskModel(db, task));
                });
                return list;
            } catch (Exception) {
                return null;
            }
        }
        public static HttpResponseMessage GetStatistic(string apikey) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return null;
                    }
                    var user = CommonManager.GetUserDataByApikey(apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return null;
                    }
                    if (!((InternalUserModel)user.Data).IsAdmin)
                        return null;

                    var list = GetStatisticList(db);

                    var result = CommonManager.WriteFile("tasks_statistic.xls", list);
                    return Download("tasks_statistic.xls");
                }
            } catch (Exception ex) {
                return null;
            }
        }

        public static HttpResponseMessage DownloadText(string fileName) {
            try {
                string path = Path.Combine(@"C:\\Users\\Inna\", fileName);
                if (!File.Exists(path))
                    return null;

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(path, FileMode.Open, FileAccess.Read));
                response.Content.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("Application/pdf");

                return response;
            } catch (Exception ex) {
                return null;
            }
        }

        public static HttpResponseMessage Download(string fileName) {
            try {
                string path = Path.Combine(@"C:\\Users\\Inna\", fileName);

                if (!File.Exists(path))
                    return null;

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(path, FileMode.Open, FileAccess.Read));
                response.Content.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");

                return response;
            } catch (Exception ex) {
                return null;
            }
        }
    }
}