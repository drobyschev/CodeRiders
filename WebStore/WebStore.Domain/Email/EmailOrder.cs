
using System.Text;
using System.Net;
using System.Net.Mail;

namespace WebStore.Domain.Email
{
    class EmailOrder
    {
        private EmailSettings m_email_settings;

        public EmailOrder(EmailSettings email_settings)
        {
            m_email_settings = email_settings;
        }

        public void Order(Cart cart, ShippingDetails shipping_info)
        {
            using (SmtpClient smtp_client = new SmtpClient())
            {
                smtp_client.Host = m_email_settings.Host;
                smtp_client.Port = m_email_settings.Port;
                smtp_client.EnableSsl = m_email_settings.UseSSL;
                smtp_client.UseDefaultCredentials = false;
                smtp_client.Credentials = new NetworkCredential(m_email_settings.Username, m_email_settings.Password);

                if (m_email_settings.WriteAsFile)
                {
                    smtp_client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp_client.PickupDirectoryLocation = m_email_settings.FileLocation;
                    smtp_client.EnableSsl = false;
                }

                StringBuilder body_msg = new StringBuilder();
                body_msg.AppendLine("Новый заказ обработан");
                body_msg.AppendLine("---");
                body_msg.AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    decimal sum = line.Product.Price * line.Quantity;
                    body_msg.AppendFormat("{0} x {1} (итого: {2:c}", line.Quantity, line.Product.Name, sum);
                }

                body_msg.AppendFormat("Общая стоимость: {0:c}", cart.TotalSum())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shipping_info.Name)
                    .AppendLine(shipping_info.Line1)
                    .AppendLine(shipping_info.Line2 ?? "")
                    .AppendLine(shipping_info.Line3 ?? "")
                    .AppendLine(shipping_info.City)
                    .AppendLine(shipping_info.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}", shipping_info.GiftWrap ? "Да" : "Нет");

                MailMessage mail_msg = new MailMessage(m_email_settings.AddressFrom,
                                                       m_email_settings.AddressTo,
                                                       "Новый заказ отправлен!",
                                                       body_msg.ToString());

                if (m_email_settings.WriteAsFile)
                {
                    mail_msg.BodyEncoding = Encoding.UTF8;
                    mail_msg.HeadersEncoding = Encoding.UTF8;
                }

                smtp_client.Send(mail_msg);
            }
        }
    }
}
