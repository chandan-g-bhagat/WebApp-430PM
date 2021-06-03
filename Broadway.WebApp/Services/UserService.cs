﻿using Broadway.WebApp.Common;
using Broadway.WebApp.Data;
using Broadway.WebApp.Models;
using Broadway.WebApp.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Broadway.WebApp.Services
{
    public static class UserService
    {
        private static DefaultContext db = new DefaultContext();

        public static StudentUserResponseViewModel CreateStudentUser(StudentUserViewModel model)
        {
            var response = new StudentUserResponseViewModel();
            try
            {
                model.StudentId = Guid.NewGuid();
                model.UserId = Guid.NewGuid();

                var user = new User
                {
                    Email = model.Email,
                    Username = model.Username,
                    Id = model.UserId,
                    HashedPassword = Md5Hash.Create(model.Password),
                    Status = true,
                    UserType = UserType.Student
                };

                db.Users.Add(user);

                var student = new Student()
                {
                    Id = model.StudentId,
                    UserId = model.UserId,
                    FName = model.FirstName,
                    LName = model.LastName,
                    Address = model.Address,
                    Gender = model.Gender,
                    IsActive = true,
                    MName = model.MiddleName

                };

                db.Students.Add(student);
                db.SaveChanges();

                response.UserId = user.Id;
                response.StudentId = student.Id;
                response.Status = true;
                response.Message = "Student and User Created";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}