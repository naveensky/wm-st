﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Norm;
using StudentTracker.Models;
using StudentTracker.Repository;

//using StudentTracker.Repository.MongoDb;

namespace StudentTracker.Services.Core {
    public class CoreService {
        private const string _serverKey = "Server";
        private const string _portKey = "Port";
        private const string _dbKey = "Database";

        private const string _tempPath = "/Tmp/";

        private static ISqlUnitOfWork _uow;

        public CoreService(ISqlUnitOfWork uow) {
            _uow = uow;
        }

        public static string GetTempPath(string extension) {
            return string.Format("{0}{1}.{2}", _tempPath, Guid.NewGuid(), extension);
        }

        public static bool DeleteFile(string path) {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            return true;
        }

        public static string ConvertToAbsolute(string relativePath) {
            return HttpContext.Current.Server.MapPath(relativePath);
        }
    }
}
