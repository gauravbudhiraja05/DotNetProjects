using System;
using System.Linq;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PickfordsIntranet.Services
{
    /// <summary>
    /// AuthService implemented IAuthService
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        //private IUnitOfWork _unitOfWork;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// IHostingEnvironment Data Member
        /// </summary>
        private IHostingEnvironment _env;


        /// <summary>
        /// AuthService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork object reference</param>
        public AuthService(IUnitOfWork unitOfWork,IHostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public LoggedInUser UserAuthenticate(AuthUserVM user)
        {
            try
            {
                var result = _unitOfWork.Repo.ExecuteQuery<LoggedInUser>("usp_AuthenticateAdminUser", SqlCommandType.StoredProcedure, new { Email = user.Email, Password=user.Password }).SingleOrDefault();
               
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RoleByUserName(string userName)
        {
            try
            {
                return _unitOfWork.Repo.ExecuteQuery<string>("usp_GetRoleNameByUserName", SqlCommandType.StoredProcedure, new { UserName = userName }).SingleOrDefault();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ChangePassword(ChangePasswordVM userPwd)
        {
            throw new NotImplementedException();
        }

        public FieldCheckVM IsEmailExistForPasswordReset(string email)
        {
            try
            {
                return _unitOfWork.Repo.ExecuteQuery<FieldCheckVM>("usp_IsEmailExistForPasswordReset", SqlCommandType.StoredProcedure, new { Email = email }).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResetPasswordForEmailTemplateVM GenerateTokenForResetPassword(string email)
        {
            try
            {
                return _unitOfWork.Repo.ExecuteQuery<ResetPasswordForEmailTemplateVM>("usp_GenerateTokenForPasswordReset", SqlCommandType.StoredProcedure, new { GeneratedFor = email }).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsTokenValidForPasswordReset(Guid token)
        {
            try
            {
                return _unitOfWork.Repo.ExecuteQuery<bool>("usp_IsTokenValidForPasswordReset", SqlCommandType.StoredProcedure, new { TokenId = token }).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult ResetPassword(ResetPasswordVM resetPassword)
        {
            try
            {
                return _unitOfWork.Repo.ExecuteQuery<BaseResult>("usp_ResetPassword", SqlCommandType.StoredProcedure, new { TokenId = resetPassword.Token, Password=resetPassword.NewPassword }).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public string GetEmailTemplateHtmlStringByName(string templateName)
        //{
        //    try
        //    {
        //        string result = _unitOfWork.Repo.ExecuteQuery<string>("usp_GetEmailTemplateHtmlStringByName", SqlCommandType.StoredProcedure, new { TemplateName = templateName }).FirstOrDefault();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public string GetEmailTemplateHtmlStringByName(string templateName)
        {
            try
            {
                string result = File.ReadAllText(_env.WebRootPath
                                                 + Path.DirectorySeparatorChar.ToString()
                                                 + "EmailTemplates"
                                                 + Path.DirectorySeparatorChar.ToString()
                                                 + templateName
                                                 + ".html");

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
