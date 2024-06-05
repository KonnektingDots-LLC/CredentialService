using cred_system_back_end_app.Application.Common.Constants;

namespace cred_system_back_end_app.Infrastructure.Smtp.Template
{
    public static class NotificationTemplate
    {
        public const string InsurerCompanyNotificationTemplate = $@"{EmailConstant.OCSEmailAddress}.";

        public const string InsurerEmployeeNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>Insurer,
                <br>
                <br>
                [Insurer's Name] has elected you as an insurer's employee within the OCS Credentialing System.
                <br>
                <br>
                Please <a href='[link]'>click here</a> to accept the invitation and access the features to assist you in these tasks.
                <br>
                Should you have any questions or require assistance, please email {EmailConstant.OCSEmailAddress}.
                <br>
                <br>
                Thank you for using OCS Credentialing System
                </p>
                
            </body>
            </html>";

        public const string ProviderReviewNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>Dear Provider,
                <br>
                <br>
                We are pleased to inform you that all the necessary tasks related to your credentials have been completed by your delegate within OCS Credentialing System. Your immediate attention is required to review the entire record.
                <br>
                <br>
                Please <a href='[link]'>click here</a> to review the completed credentials.
                <br>
                <br>
                Remember, as the provider, you are solely responsible for the accuracy and completeness of these documents.
                <br>
                Ensure that everything is in order.
                <br>
                <br>
                Should you encounter any issues or have questions, please do not hesitate to contact our support team at {EmailConstant.OCSEmailAddress}.
                <br>
                <br>
                Thank you for using OCS Credentialing System
                </p>
            </body>
            </html>";

        public const string ProfileCompletionNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>Dear User,
                <br>
                <br>
                Congratulations! Your account on OCS Credentialing System has been successfully created, and your profile is now complete.
                <br>
                <br>
                You can now access all the features and services available within our system.
                <br>
                <br>
                Should you have any questions or require assistance, please do not hesitate to contact our support team at {EmailConstant.OCSEmailAddress}.
                <br>
                <br>
                Thank you for using OCS Credentialing System
                </p>
            </body>
            </html>";

        public const string DelegateInvitationNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>Delegate,
                <br>
                <br>
                [Provider's Name] has elected you as a delegate within the OCS Credentialing System. You have been granted access to manage and monitor registration, facilitating the credentialing process.
                <br>
                <br>
                Please <a href='[link]'>click here</a> to accept the invitation and access the features to assist you in these tasks.
                <br>
                Should you have any questions or require assistance, please email {EmailConstant.OCSEmailAddress}.
                <br>
                <br>
                Thank you for using OCS Credentialing System
                </p>               
            </body>
            </html>";

        public const string ProviderSubmitToInsurerNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>
                <br>
                We are pleased to inform you that the provider's profile for [Provider's Name] has been completed with all the required information and documentation. This profile is ready for your review as part of the credentialing process.
                <br>
                <br>
                Please <a href='[link]'>click here</a> to access the provider's profile and proceed with the credentialing process.
                <br>
                Should you have any questions or require assistance, please email {EmailConstant.OCSEmailAddress}.
                <br>
                <br>
                <br>
                <br>
                Thank you,
                <br>
                <br>
                <b>OCS Credentialing System</b>
                </p>              
            </body>
            </html>";

        public const string ProviderSubmitNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>
                <br>
                We are glad to confirm that your profile, complete with all required information and documentation, has been successfully submitted to the insurance company for credentialing.
                <br>
                <br>
                You can contact the insurance company directly for any inquiries.
                Should you need assistance or have any questions, please email {EmailConstant.OCSEmailAddress}.
                <br>
                <br>
                Thank you for using <b>OCS Credentialing System.</b>
                </p>    
            </body>
            </html>";

        public const string ProviderSubmitToDelegateNotificationTemplate = $@"
            <html>
            <head></head>
            <body>
                <p>
                <br>
                We are pleased to confirm that the case you worked with as a delegate for [Provider's Name] has been successfully submitted and is now available for the selected insurance companies to finish the credentialing process.
                <br>
                <br>
                Thank you for using <b>OCS Credentialing System.</b>
                </p>    
            </body>
            </html>";

        public struct DelegateStatusUpdate
        {
            public const string ProviderNameToken = "[PROVIDER_NAME]";

            public const string Subject = $@"Subject: OCS Credentialing System - Notice of Delegation Change;";

            public const string Body = $@"
                <html>
                    <body>
                        <p>
                            <br>

                            Dear Delegate,

                            <br>
                            <br>

                            This email informs you of a recent change regarding your delegated 
                            permissions and access within. 
                            
                            <br>
                            <br>

                            The OCSCredentialing System provider, 
                            <b>{ProviderNameToken}</b>, has modified the delegation associated with your 
                            profile. As a result, your permissions and access within the system may 
                            have been revoked or altered. 
                            
                            <br>
                            <br>

                            If you have questions about these changes 
                            or need further assistance, please contact your provider directly.

                            <br>
                            <br>

                            Thank you,                        
                        
                            <br>
                        
                            OCS Credentialing System.
                        </p>    
                    </body>
                </html>";
        }

        public struct ProviderDelegateStatusUpdate
        {
            public const string DelegateNameToken = "[DELEGATE_NAME]";

            public const string Subject = $@"Subject: OCS Credentialing System - Notice of Delegation Change;";

            public const string Body = $@"
                <html>
                    <body>
                        <p>
                            <br>

                            Dear Provider,

                            <br>
                            <br>

                            This email confirms a recent change you made to the delegation 
                            associated with your profile in the OCSCredentialing System. 

                            <br>
                            <br>

                            The permissions and access for your delegate, <b>{DelegateNameToken}</b>, 
                            have been modified per your instructions. As the provider, you 
                            have the right to revoke or modify this delegation at any time. 
                            
                            <br>
                            <br>

                            If this action was not intended or you need assistance, be sure 
                            to contact our support team immediately at credenciales@ocspr.com.
                                
                            <br>
                            <br>

                            Thank you for using the OCS Credentialing System.
                        </p>    
                    </body>
                </html>";
        }

        public struct InsurerProviderStatusUpdate
        {
            public const string ProviderNameToken = "[PROVIDER_NAME]";

            public const string Subject = $@"OCS Credentialing System - Status Changed;";

            public const string Link = "[LINK]";

            public const string Body = $@"
            <html>
            <head></head>
            <body>
                <p>
                <br>
                We are pleased to inform you that the credentialing process for <b>{ProviderNameToken}</b> has been closed by the insurance company in the OCS Credentialing System.
                The final resolution, along with any related details, is now available on our website. You can visit <a href='{Link}'>{Link}</a> to access the updates related to your case.
                Should you have any questions or require further information, please email credenciales@ocs.pr.gov
                <br>
                <br>
                Thank you for your collaboration and trust in the <b>OCS Credentialing System.</b>
                </p>    
            </body>
            </html>";
        }

        public struct InsurerProviderStatusRTPUpdate
        {
            public const string Subject = $@"OCS Credentialing System - Status Changed;";

            public const string Body = $@"
            <html>
            <head></head>
            <body>
                <p>
                <br>
                Please refer to the notice from the insurance company regarding your <b>Return to Provider Status</b>.
                Ensure direct communication with the insurance company managing your recredentialing process as a provider.
                This is essential for a comprehensive and timely review of your case
                <br>
                <br>
                Thank you for your collaboration and trust in the <b>OCS Credentialing System.</b>
                </p>    
            </body>
            </html>";
        }
    }
}
