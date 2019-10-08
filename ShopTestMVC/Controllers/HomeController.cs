using System;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using ShopTestMVC.Repository;
using ShopTestMVC.Tools;

namespace ShopTestMVC.Controllers
{
    public class HomeController : Controller
    {

        private UserDbContext db = new UserDbContext();


        public ActionResult Index()
        {

                var res = db.UsersData.ToList();
                return View(res);
            
        }


        /// <summary>
        /// Загрузка файла 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFileData()
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var upload = Request.Files[file];
                    if (upload != null)
                    {
                        // получаем имя файла
                        string fileName = Path.GetFileName(upload.FileName);

                        var FileExtension = Path.GetExtension(fileName);

                        if (FileExtension == ".csv")
                        {

                            // сохраняем файл в папку Files в проекте

                            string ServFilePath = Server.MapPath("~/UploadFiles/" + fileName);

                            upload.SaveAs(ServFilePath);


                            // приводим к utf-8
                            byte[] ansiBytes = System.IO.File.ReadAllBytes(ServFilePath);
                            var utf8String = System.Text.Encoding.Default.GetString(ansiBytes);
                            System.IO.File.WriteAllText(ServFilePath, utf8String);

                            FileCSVReader FileWork = new FileCSVReader(ServFilePath);

                            var fields = FileWork.get_DataLines();

                            // разбираем на строки для бд
                            foreach (var UserDataUpl in fields)
                            {
                                if (UserDataUpl[0] != "Id")
                                {

                                    int k = 0;
                                    Int32.TryParse(UserDataUpl[4], out k);

                                    Model.Users UserModel = new Model.Users
                                    {
                                        Name = UserDataUpl[1],
                                        DateBirth = DateTime.Parse(UserDataUpl[2]),
                                        Gender = UserDataUpl[3],
                                        ParentId = k,

                                    };


                                    Model.Relatives RelativeModel = new Model.Relatives
                                    {
                                        ParentId = k
                                    };

                                    db.UsersData.Add(UserModel);
                                    db.RelativesData.Add(RelativeModel);
                                    db.SaveChanges();
                                }
                            }


                        }
                        else
                        {
                            return Json("неверный формат");
                        }
                    }
                }

            }
            catch (IOException)
            {
                return Json("ошибка загрузки");
            }

            return Json("файk успешно загружен");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "My contacts";

            return View();
        }


    }
}