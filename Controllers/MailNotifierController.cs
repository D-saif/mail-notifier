using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace MailNotifierApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class MailNotifierController : ControllerBase
{

    private readonly ILogger<MailNotifierController> _logger;

    public MailNotifierController(ILogger<MailNotifierController> logger)
    {
        _logger = logger;
    }
     public String SendMailNotification(string body, string destination_adress,string subject,int ticket_number)
    {
        
        string FromMail = "xyz.service.client@gmail.com";
        string Email_password = "bmeohytjppdevqku";
        // string body = "Cher client\n"+"nous avons bien recu votre reclamation ( ticket inscrit sous le numero: "+ticket_number+")";
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress(FromMail);
        mail.To.Add(destination_adress);
        mail.Subject = subject;
        mail.Body = body;
        SmtpServer.Port = 587; 
        SmtpServer.Credentials = new System.Net.NetworkCredential(FromMail, Email_password);
        SmtpServer.EnableSsl = true;
        SmtpServer.Send(mail);
        return body;
    }
   
    [HttpGet("/confirm_ticket_submission/{destination_adress}/{ticket_number}")]
    public String confirmTicketSubmission( string destination_adress,int ticket_number){
        string body = "Cher client\n"+"nous avons bien recu votre réclamation ( ticket inscrit sous le numero: "+ticket_number+")";
        string subject = "Confirmation du reclamation" ; 
        SendMailNotification(body, destination_adress, subject,ticket_number);
        return "mail sent successfully";
    }
    
    
    [HttpGet("/confirm_ticket_closure/{destination_adress}/{ticket_number}")]
    public String confirmTicketClosure(string destination_adress,int ticket_number){
        string body = "Cher client\n"+"votre réclamation ( ticket inscrit sous le numero: "+ticket_number+") a ete bien traitée ";
        string subject = "Confirmation de cloture du ticket" ; 
        SendMailNotification(body, destination_adress, subject,ticket_number);
        return "";
    }
    
   
}
