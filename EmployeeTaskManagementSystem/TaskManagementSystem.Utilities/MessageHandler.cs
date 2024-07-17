namespace TaskManagementSystem.Utilities
{
    public static class MessageHandler
    {
        public static class ResponseMsg
        {
            public const string Get_Success = "Data fetched successfully";
            public const string Add_Success = "Data added successfully";
            public const string Update_Success = "Data updated successfully";
            public const string Delete_Success = "Data deleted successfully";
            public const string PasswordChange_Success = "Password changed successfully";
            public const string PasswordChange_Error = "Error, Current Password is Invalid";
            public const string Login_Error = "Error. Username or password is incorrect";
            public const string Token_Success = "Token generated successfully";
            public const string RefreshToken_Success = "Refresh Token generated successfully";
            public const string RefreshToken_Error = "Error. RefreshToken is Expired";
            public const string RevokeToken_Success = "Token Revoked Successfully";
            public const string ForgotPassword_Success = "Password Recovery Successful";
            public const string ForgotPassword_Error = "Password Recovery Failed";
            public const string Subject_ForgotPassword = "CSPGCL - Password Recovery";
            public const string Subject_CreateAccount = "CSPGCL - Account Creation";
            public const string Error = "Error. Please try again with appropriate data";
            public const string validFileExtension = "Error. Please Upload The Valid Files Only";
            public const string APIEndPointPermission = "You do not have a permission to access this API url";
        }

        public static class StatusMsg
        {
            public const string Success = "success";
            public const string Error = "error";
        }
    }
}
