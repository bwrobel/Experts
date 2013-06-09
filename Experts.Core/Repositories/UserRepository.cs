using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Authentication;
using Experts.Core.Data;
using Experts.Core.Entities;
using Experts.Core.Exceptions;
using Experts.Core.Utils;
using Experts.Core.ViewModels;

namespace Experts.Core.Repositories
{
    public class UserRepository : AuditableRepository<User>
    {
        public UserRepository(DataContext db)
            : base(db)
        { }

        public override void Add(User user)
        {
            //GeneratePublicName(user);
            user.EncryptPassword();
            user.GenerateActivationKey();
            base.Add(user);
            UpdateUserStats(user);
        }

        public void TransformNoSignUpAccount(User user)
        {
            //GeneratePublicName(user);
            user.IsNoSignUpUser = false;
            user.EncryptPassword();
            user.GenerateActivationKey();
            base.Update(user);
        }

        public User Activate(string activationKey)
        {
            var user = FindByActivationKey(activationKey);

            if (user.IsActivated)
                throw new UserAlreadyActivatedException();

            user.IsActivated = true;
            Update(user);

            return user;
        }

        public User ConfirmNewEmail(string activationKey)
        {
            var user = FindByActivationKey(activationKey);
            user.Email = user.NewEmail;
            Update(user);

            return user;
        }
        
        public User FindByEmail(string email)
        {
            return Find(u => u.Email == email);
        }

        public bool IsEmailUnique(string email)
        {
            return Find(u => u.Email == email) == null;
        }

        public bool IsNoSignUpUser(string email)
        {
            var user = Find(u => u.Email == email);
            return user != null && user.IsNoSignUpUser;
        }

        public User VerifyCredentials(string email, string password)
        {
            var user = FindByEmail(email);
            if (user == null)
                throw new AuthenticationException();

            var passwordHash = CryptoHelper.CreateHash(password, user.PasswordSalt);
            if (passwordHash != user.Password)
                throw new AuthenticationException();

            if (!user.IsActivated)
                throw new UserAccountNotActivatedException();

            return user;
        }

        public User VerifyCredentials(string key)
        {
            var user = FindByActivationKey(key);

            if (!user.IsNoSignUpUser)
                throw new UserNotFoundException();

            return user;
        }

        public void ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;
            user.EncryptPassword();
            Update(user);
        }

        public User FindByResetKey(string resetKey)
        {
            var user = Find(u => u.ResetKey == resetKey);

            if (user == null)
                throw new UserNotFoundException();

            return user;
        }

        public void AddEmailConfigurationDefaultValue(User user)
        {
            user.EmailConfiguration = 0;

            var nonObligatoryEmailTypes = EmailMetadataHelper.GetEmailTemplates().Where(m => !m.IsObligatory).Select(m => m.EmailType);
            foreach (var nonObligatoryEmailType in nonObligatoryEmailTypes)
                user.EmailConfiguration |= nonObligatoryEmailType;

            Update(user);
        }

        private User FindByActivationKey(string activationKey)
        {
            var user = Find(u => u.ActivationKey == activationKey);

            if (user == null)
                throw new UserNotFoundException();

            return user;
        }
        
        public void UpdateUserStats(User user)
        {
            var askedQuestions = 0;
            var acceptedQuestions = 0;
            if(user.Questions == null) user.Questions = new Collection<Thread>();
            foreach (var question in user.Questions)
            {
                askedQuestions++;
                if(question.State == ThreadState.Closed) acceptedQuestions++;
            }
            user.AskedQuestions = askedQuestions;
            user.AcceptedQuestions = acceptedQuestions;
            Update(user);
        }

        public IEnumerable<PaymentStatistic> GetPaymentStatistics(int userId)
        {
            var list = Db.Transfers.Where(t => t.Owner.Id == userId && t.IsPending == false && t.Value > 0 && (t.OrderDate.Year == DateTime.Now.Year || t.OrderDate.Year == DateTime.Now.Year - 1))
                            .GroupBy(t => new { t.OrderDate.Month })
                            .Select(g => new PaymentStatistic { Value = g.Sum(v => v.Value), Month = g.Key.Month }).ToList();

            for (var i = 1; i <= 12; i++)
            {
                if (!list.Any(t => t.Month == i))
                    list.Add(new PaymentStatistic { Month = i, Value = 0 });

                if (!list.Any(f => f.Month == i))
                    list.Add(new PaymentStatistic { Month = i, Value = 0 });

                if (!list.Any(f => f.Month == i))
                    list.Add(new PaymentStatistic { Month = i, Value = 0 });
            }

            return list.OrderBy(t => t.Month);
        }

        public bool IsPublicNameUnique(string publicName)
        {
            var expertRepository = new ExpertRepository(Db);
            var moderatorRepository = new ModeratorRepository(Db);

            return expertRepository.Find(e => e.PublicName == publicName) == null &&
                   moderatorRepository.Find(m => m.PublicName == publicName) == null;
        }
    }
}
