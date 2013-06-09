using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Experts.Core.Utils;

namespace Experts.Core.Entities
{
    public class User : IAuditableEntity
    {
        public User()
        {
            Transfers = new Collection<Transfer>();
            Posts = new Collection<Post>();
            Questions = new Collection<Thread>();
            Chats = new Collection<Chat>();
        }

        public int Id { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string NewEmail { get; set; }

        public bool IsActivated { get; set; }

        public DateTime? LastAskEncouragementDate { get; set; }

        public string ActivationKey { get; set; }

        public string ResetKey { get; set; }

        public virtual Expert Expert { get; private set; }

        public virtual Partner Partner { get; private set; }

        public virtual Moderator Moderator { get; private set; }

        public virtual Consultant Consultant { get; private set; }

        public virtual ICollection<Chat> Chats { get; private set; } 

        public bool IsExpert { get { return Expert != null; } }

        public bool IsPartner { get { return Partner != null; } }

        public bool IsModerator { get { return Moderator != null; } }

        public bool IsConsultant { get { return Consultant != null; } }

        public bool IsNoSignUpUser { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public int AskedQuestions { get; set; }

        public int AcceptedQuestions { get; set; }

        public virtual ICollection<Thread> Questions { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }

        public EmailType EmailConfiguration
        {
            get { return (EmailType)LongEmailConfiguration; }
            set { LongEmailConfiguration = (long)value; }
        }

        public long LongEmailConfiguration { get; set; }

        public string BankAccountNumber { get; set; }

        public void EncryptPassword()
        {
            PasswordSalt = CryptoHelper.CreateSalt();
            Password = CryptoHelper.CreateHash(Password, PasswordSalt);
        }

        public void GenerateActivationKey()
        {
            ActivationKey = GenerateKey();
        }

        public void GenerateResetKey()
        {
            ResetKey = GenerateKey();
        }

        private string GenerateKey()
        {
            var key = CryptoHelper.CreateSalt();
            return Regex.Replace(key, @"\W", string.Empty);
        }

        public decimal GetAvailableCash()
        {
            return Transfers.Sum(transfer => transfer.Value);
        }

        public decimal GetTotalCash()
        {
            return Transfers.Where(transfer => !transfer.IsPending).Sum(transfer => transfer.Value);
        }

        public bool HasActiveChats()
        {
            return Chats.Any(c => !c.IsClosed);
        }
    }
}
