using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PickfordsIntranet.Core.ThirdPartyServices
{
    /// <summary>
    /// The IMessage Inteface describes the implementation required for email services. SMTP services,
    /// mock mail services for testing, or 3rd party API services such as Courier can be implemented
    /// using this Interface.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// This is the list of one or more recipients that will receive the message. This collection
        /// contains one ore more Recipient objects.
        /// </summary>
        IEnumerable<Recipient> Recipients
        {
            get;
        }

        /// <summary>
        /// This is the list of one or more CC recipients that will receive the message. This collection
        /// contains one or more Recipient objects.
        /// </summary>
        IEnumerable<Recipient> CCRecipients
        {
            get;
        }

        /// <summary>
        /// This is the list of one or more BCC recipients that will receive the message. This collection
        /// contains one or more Recipient objects.
        /// </summary>
        IEnumerable<Recipient> BCCRecipients
        {
            get;
        }

        /// <summary>
        /// A method to easily add one or more Recipient name/email address objects to the collection of addresses
        /// that should receive this email. Note that not all email service implementations may use the 
        /// fullName value specified.
        /// </summary>
        /// <param name="fullName">The full name (friendly name) of the recipient to receive the email message (e.g. 'John Smith').</param>
        /// <param name="emailAddress">The email address of the recipient.</param>
        void AddRecipient(string fullName, string emailAddress);

        /// <summary>
        /// A method to easily add one or more Recipient name/email address objects to the collection of addresses
        /// that should receive this email. Note that not all email service implementations may use the 
        /// fullName value specified.
        /// </summary>
        /// <param name="fullName">The full name (friendly name) of the recipient to receive the email message (e.g. 'John Smith').</param>
        /// <param name="emailAddress">The email address of the recipient.</param>
        void AddCCRecipient(string fullName, string emailAddress);

        /// <summary>
        /// A method to easily add one or more Recipient name/email address objects to the collection of addresses
        /// that should receive this email. Note that not all email service implementations may use the 
        /// fullName value specified.
        /// </summary>
        /// <param name="fullName">The full name (friendly name) of the recipient to receive the email message (e.g. 'John Smith').</param>
        /// <param name="emailAddress">The email address of the recipient.</param>
        void AddBCCRecipient(string fullName, string emailAddress);

        /// <summary>
        /// Executes the process to send the email message. This may be a long running process due to tight coupling with
        /// external SMTP or API systems. Therefore this method is provided as asyncrhonous to free the hosting thread to process
        /// other requests.
        /// </summary>
        /// <returns>This method returns TRUE if the message sending process was successful.</returns>
        Task<bool> Send();

        /// <summary>
        /// This is the friendly name from which the email message will be sent.
        /// </summary>
        string FromName { get; set; }

        /// <summary>
        /// This is the email address from which the email message will be sent.
        /// </summary>
        string FromEmail { get; set; }

        /// <summary>
        /// This is the friendly reply-to name that will be used if the recipient replies to the message.
        /// </summary>
        string ReplyToName { get; set; }

        /// <summary>
        /// This is the reply-to email address that will be used if the recipient replies to the message.
        /// </summary>
        string ReplyToEmail { get; set; }

        /// <summary>
        /// This is the subject line of the email message.
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// This is a Boolean that, if true, will cause the body of the message to be treated as HTML content. Some
        /// service implementation may rely on this for special internal handling of the 'Body' content.
        /// </summary>
        bool BodyIsHtml { get; set; }

        /// <summary>
        /// This is the text or HTML body content of the email message.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Sets the message priority (default is normal). Not all implementation may use this value when
        /// setting the message's priority.
        /// </summary>
        MessagePriority Priority { get; set; }
    }
    public enum MessagePriority
    {
        Normal,
        Low,
        High
    }

    public class Recipient
    {
        public Recipient(string fullName, string email)
        {
            this.FullName = fullName;
            this.Email = email;
        }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
