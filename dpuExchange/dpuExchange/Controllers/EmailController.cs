﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dpuExchange.Models;
using System.Net.Mail;
using System.Web.Security;

namespace dpuExchange.Controllers
{
    public class EmailController : Controller
    {
        // Returns RequestPage.cshtml View
        // GET: /Email/
        public ActionResult RequestPage(String id, String bookTitle)
        {
            EmailModel email = new EmailModel();
            email.Seller = id;
            email.Sender = User.Identity.Name;
            email.BookTitle = bookTitle;
            return View(email);
        }

        // Takes message parameters Seller, Sender, BookTitle, and Message and sends email message 
        public void Send(String Seller, String Sender, String BookTitle, String Message)
        {
            MailMessage message = new MailMessage();
            MembershipUser sellerAddress = Membership.GetUser(Seller);
            MembershipUser senderAddress = Membership.GetUser(Sender);
            message.To.Add(new MailAddress(sellerAddress.Email));
            message.From = new MailAddress(senderAddress.Email);
            message.Subject = "Someone is interested in one of your books!";
            string emailBody = "";
            if (Message == "")
            {
                emailBody = "This is an automatically generated email from DePauwBookExchange! "
                + "The user " + Sender + " is interested in your book titled \"" + BookTitle + "\".\n\n"
                + "To follow up with this user, please contact them at " + senderAddress.Email + ". \n\n"
                + "Thank you for using DePauwBookExchange!";
            }
            else
            {
                emailBody = "This is an automatically generated email from DePauwBookExchange! "
                + "The user " + Sender + " is interested in your book titled \"" + BookTitle + "\".\n\n"
                + "Below is a personal message from the user: \n \""
                + Message + "\"\n"
                + "To follow up with this user, please contact them at " + senderAddress.Email + ". \n\n"
                + "Thank you for using DePauwBookExchange!";
            }
            message.Body = emailBody;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.Send(message);
        }

    }
}
