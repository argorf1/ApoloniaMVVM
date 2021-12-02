using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ApoloniaApp.Services
{
    public class MailingService
    {
        public static SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("chachesoflo@gmail.com", "Chachesoflo21*"),
            EnableSsl = true,
        };

        public static void NewUser(string recipient, string name,string user, string password)
        {
            smtpClient.Send("chachesoflo@gmail.com", recipient,"Nuevo Usuario Registrado"
                ,name+"\n\n" +
                "Se ha creado su cuenta con exito\n " +
                "\n" +
                "\n" +
                "Usuario:\n" +
                user+"\n" +
                "\n" +
                "Password:\n" +
                password);

        }

    }
}
