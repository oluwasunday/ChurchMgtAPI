using church_mgt_dtos.AuthenticationDtos;
using church_mgt_utilities.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using church_mgt_core.services.interfaces;
using church_mgt_dtos.Dtos;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace church_mgt_core.services.implementations
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger _logger;
        public EmailService(IOptions<MailSettings> options, ILogger logger)
        {
            _mailSettings = options.Value;
            _logger = logger;
        }
        public async Task<Response<string>> SendEmailAsync(MailRequestDto requestDto)
        {
            _logger.Information("Registration sending mail");
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(requestDto.ToEmail));
            email.Subject = requestDto.Subject;

            var builder = new BodyBuilder();
            if (requestDto.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in requestDto.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            _logger.Information("Sending mail");
            builder.HtmlBody = requestDto.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return Response<string>.Success("Mail sent successfully", "Success", StatusCodes.Status204NoContent);
        }
    }
}
