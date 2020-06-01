using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;
using TrackerLib.Models;
using TrackerLib.Models.InternalModels;
using TrackerLib.Models.OutputModels;
using System.Net;
using System.ComponentModel;

namespace TrackerLib.Managers {
    public static class CommonManager {
        /// <summary>
        /// получаем пользовательские данные
        /// </summary>
        public static InternalResultModel GetUserDataByApikey(string apikey) {
            try {
                if (string.IsNullOrEmpty(apikey))
                    return new InternalResultModel(StatusCodeEnum.ApikeyIsEmpty);
                using (var db = new TaskTrackerEntities()) {
                    var userData = db.UserAuthData
                        .FirstOrDefault(a => a.Apikey == apikey
                                             && !a.UserProfiles.IsDeleted)
                                             ?.UserProfiles;
                    if (userData == null)
                        return new InternalResultModel(StatusCodeEnum.ApikeyIsWrong);
                    var userModel = new InternalUserModel(db, userData);
                    return new InternalResultModel(StatusCodeEnum.Success, userModel);
                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// генерируем апикей bли пароль
        /// </summary>
        public static string GetRandomString(int count) {
            try {
                var rand = new Random();
                var result = "";
                var buff = 0;
                while (buff < count) {
                    var num = rand.Next(48, 122);
                    if (num > 48 && num < 57 ||
                        num > 92 && num < 97) continue;
                    result += (char)num;
                    buff++;
                }
                return result;
            } catch (Exception ex) {
                return "";
            }
        }

        public static void WritePdfFile(string fileName, string data) {
            string path = Path.Combine(@"C:\\Users\\Inna\", fileName);

            if (File.Exists(path))
                File.Delete(path);

            byte[] bytes = Encoding.ASCII.GetBytes(data);

            System.IO.BinaryWriter writer =
                new BinaryWriter(new FileStream(path, FileMode.CreateNew));
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
        }

        public static bool WriteFile(string fileName, List<InternalTaskModel> list) {
            try {
                string path = Path.Combine(@"C:\\Users\\Inna\", fileName);

                if (File.Exists(path))
                    File.Delete(path);
                // Build the file content
                var csv = new StringBuilder();

                var polesNames = new List<string>() {
                "UserId",
                "TaskId",
                "Имя",
                "Фамилия",
                "Название задачи",
                "Статус",
                "Количество часов",
                "Дата начала",
                "Дата завершения",
                "Дата дедлайна",
                "Имя создателя задачи",
                "Фамилия создателя задачи"
            };

                csv.AppendLine(string.Join(";", polesNames));

                list.ForEach(test => {
                    var dataForSaving = new List<string>();
                    dataForSaving.Add(test.UserId.ToString());
                    dataForSaving.Add(test.TaskId.ToString());
                    dataForSaving.Add(test.UserFirstName.ToString());
                    dataForSaving.Add(test.UserLastName.ToString());
                    dataForSaving.Add(test.TaskName.ToString());
                    if (test.TaskStatus == TaskStatusEnum.Dev) {
                        dataForSaving.Add("DEVELOP");
                    } else if (test.TaskStatus == TaskStatusEnum.New) {
                        dataForSaving.Add("NEW");
                    } else if (test.TaskStatus == TaskStatusEnum.QA) {
                        dataForSaving.Add("QA");
                    } else {
                        dataForSaving.Add("READY");
                    }
                    dataForSaving.Add(test.HoursCount.ToString());
                    dataForSaving.Add(test.DateCreate.ToString());
                    dataForSaving.Add(test.DateFinished.ToString());
                    dataForSaving.Add(test.DateDeadline.ToString());
                    dataForSaving.Add(test.CreaterFirstName.ToString());
                    dataForSaving.Add(test.CreaterLastName.ToString());

                    csv.AppendLine(string.Join(";", dataForSaving));
                });

                File.WriteAllText(path, csv.ToString());
                return true;
            } catch (Exception) {
                return false;
            }
        }


    }
}
