using System;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using ShopTestMVC.Repository;
using ShopTestMVC.Tools;
using ShopTestMVC.Model;

namespace ShopTestMVC.Controllers {
    public class HomeController : Controller {

        private UserDbContext db = new UserDbContext();
        private FileCSVReader FileWork;

        public ActionResult Index() {
            var res = db.UsersData.ToList();
            return View(res);
        }


        /// <summary>
        /// Загрузка файла 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFileData() {
            try {
                foreach (string file in Request.Files) {
                    var upload = Request.Files[file];
                    if (upload != null) {
                        // получаем имя файла
                        string fileName = Path.GetFileName(upload.FileName);
                        string FileExtension = Path.GetExtension(fileName);

                        if (FileExtension == ".csv") {
                            // сохраняем файл в папку Files в проекте
                            string ServFilePath = Server.MapPath("~/UploadFiles/" + fileName);                        
                            FileWork = new FileCSVReader(ServFilePath, upload.InputStream);
                            var fields = FileWork.get_DataLines();

                            if (fields.Any()) {
                                CleanDbData();

                                // разбираем на строки для бд
                                foreach (string[] RowsUpload in fields) {
                                    if (RowsUpload[0] != "Id") {

                                        int _k = 0;
                                        Int32.TryParse(RowsUpload[4], out _k);

                                        int _id = 0;
                                        Int32.TryParse(RowsUpload[0], out _id);

                                        bool isMan = false;

                                        if (!string.IsNullOrEmpty(RowsUpload[3])) {
                                            string firstCh = RowsUpload[3].ToLower().Substring(0, 1);
                                            switch (firstCh) {
                                                case "m":
                                                    isMan = true;
                                                    break;
                                                case "м":
                                                    isMan = true;
                                                    break;
                                                default:
                                                    isMan = false;
                                                    break;
                                            }
                                        }

                                        var UserModel = new Users {
                                            Id = _id,
                                            Name = RowsUpload[1],
                                            DateBirth = DateTime.Parse(RowsUpload[2]),
                                            Gender = isMan,
                                            ParentId = _k,
                                        };

                                        var RelativeModel = new Relatives {
                                            ParentId = _k
                                        };

                                        db.UsersData.Add(UserModel);
                                        db.RelativesData.Add(RelativeModel);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        else {
                            return Json("неверный формат");
                        }
                    }
                }
            }
            catch (IOException) {
                return Json("ошибка загрузки");
            }

            return Json("файл успешно загружен");
        }


        public ActionResult Contact() {
            ViewBag.Message = "My contacts";
            return View();
        }
        /// <summary>
        /// Удалить все записи из Users и Relatives
        /// </summary>
        private void CleanDbData() {
            var relativesTableData = db.RelativesData.ToList();
            var usersTableData = db.UsersData.ToList();
            usersTableData.ForEach(x => {
                db.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            });
            relativesTableData.ForEach(x => {
                db.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            });

            db.SaveChanges();
        }


    }
}