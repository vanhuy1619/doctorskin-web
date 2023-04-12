using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using Quartz.Impl;
using System.Data.Entity;
using DoctorSkin.Models;
using Quartz;
using System.Threading.Tasks;

namespace DoctorSkin.config
{
    public class MyJob : IJob
    {
        public string createAt()
        {
            DateTime now = DateTime.UtcNow;
            long secondsSinceEpoch = (long)(now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            string finalString = secondsSinceEpoch.ToString();
            return finalString;
        }

        public void Execute(IJobExecutionContext context)
        {
            int now = int.Parse(createAt());
            // Lấy danh sách các bản ghi cũ hơn 3 phút
            using (var db = new DoctorSkinEntities())
            {
                var oldRecords = db.Forgots
                    .Where(r => now - int.Parse(r.createAt) > 3)
                    .ToList();

                // Xóa các bản ghi cũ hơn 3 phút
                db.Forgots.RemoveRange(oldRecords);
                db.SaveChanges();
            }
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }

}