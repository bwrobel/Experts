using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;

namespace Experts.Core.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ThreadIssue> ThreadIssues { get; set; }
        public DbSet<PriceProposal> PriceProposals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<CategoryAttributeValue> CategoryAttributeValues { get; set; }
        public DbSet<CategoryAttributeOption> CategoryAttributeOptions { get; set; }
        public DbSet<ExpertCategoryAttributeValues> ExpertCategoryAttributeValues { get; set; }
        public DbSet<SEOKeyword> SEOKeyword { get; set; }        
        public DbSet<QueuedEmail> QueuedEmails { get; set; }
        public DbSet<Event> Events { get; set;  }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Provision> Provisions { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<AdCampaignLandingPage> AdCampaignLandingPages { get; set; }

        public DataContext()
            : base("dataConnection")
        {
        }

        public void Load<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property)
            where TEntity : class, IEntity
            where TProperty : class, IEntity
        {
            Entry(entity).Reference(property).Load();
            Dispose();
        }

        public void Load<TEntity, TElement>(TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> property)
            where TEntity : class, IEntity
            where TElement : class, IEntity
        {
            Entry(entity).Collection(property).Load();
        }

        public void Attach<T>(T entity)
            where T : class, IEntity
        {
            if (!Set<T>().Any(e => e.Id == entity.Id))
                Set<T>().Attach(entity);
        }

        public void Attach<T>(ICollection<T> collection)
            where T : class, IEntity
        {
            var set = Set<T>();
            foreach (var item in collection.Where(item => item.Id != 0))
                set.Attach(item);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EmailDataConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Expert)
                .WithRequired(e => e.User);

            modelBuilder.Entity<CategoryAttributeValue>()
                .HasMany(c => c.SelectedOptions)
                .WithMany();

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Attributes)
                .WithMany();

            modelBuilder.Entity<CategoryAttribute>()
                .HasMany(c => c.ChildAttributes)
                .WithMany();

            modelBuilder.Entity<CategoryAttribute>()
                .HasMany(c => c.Options)
                .WithOptional(o => o.Attribute);

            modelBuilder.Entity<CategoryAttribute>()
                .HasMany(c => c.ParentOptions)
                .WithMany();

            modelBuilder.Entity<Thread>()
                .HasMany(t => t.CategoryAttributes)
                .WithOptional();

            modelBuilder.Entity<ExpertCategoryAttributeValues>()
                .HasMany(ecav => ecav.CategoryAttributes)
                .WithOptional();

            modelBuilder.Entity<Expert>()
                .HasMany(t => t.CategoryAttributes)
                .WithOptional();


            modelBuilder.Entity<User>()
                .HasOptional(u => u.Partner)
                .WithRequired(p => p.User);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Moderator)
                .WithRequired(m => m.User);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithOptional(c => c.Owner);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Consultant)
                .WithOptionalPrincipal(m => m.User);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages);

        }
    }

    public class EmailDataConfiguration : EntityTypeConfiguration<EmailData>
    {
    }

    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
    }
}
