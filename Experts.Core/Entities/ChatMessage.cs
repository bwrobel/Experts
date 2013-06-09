using System;

namespace Experts.Core.Entities
{
    public class ChatMessage : IEntity
    {
        public int Id { get; set; }

        public virtual Chat Chat { get; set; }

        public string Text { get; set; }

        public virtual User Author { get; set; }

        public string AuthorEmail { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// Kontekst zapytania (URL, itp z którego user zaczął pisać) - przydatne np. przy szybkiej pomocy w wypełnianiu formularzy
        /// </summary>
        public string Context { get; set; }
    }

    public enum ChatModeratorStatus
    {
        /// <summary>
        /// Aktywny - przed chwilą był na czacie
        /// </summary>
        Active,

        /// <summary>
        /// Nie było go od jakiegoś czasu na czacie ale jest online
        /// </summary>
        Away,

        /// <summary>
        /// Nie wykazuje żadnej aktywności od jakiegoś czasu
        /// </summary>
        Offline
    }
}
