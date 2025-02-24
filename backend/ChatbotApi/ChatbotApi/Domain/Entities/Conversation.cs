﻿namespace ChatbotApi.Domain.Entities
{
    // Stworzyłem encję dla ewentualnej rozbudowy. W razie potrzeby pobierania starych konwersacji.
    // To jest jeszcze do skończenia
    public class Conversation
    {
        // Unikalny identyfikator rozmowy.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Tytuł lub nazwa rozmowy, np. wybrana przez użytkownika lub generowana automatycznie.
        public string? Title { get; set; }

        // Data i godzina rozpoczęcia rozmowy.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Data i godzina ostatniej aktywności w rozmowie (np. wysłania wiadomości).
        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;

        // Status rozmowy (np. aktywna, zakończona, archiwalna) – przydatne przy filtrowaniu.
        public string? Status { get; set; }

        // Nawigacyjna właściwość do powiązanych wiadomości.
        public ICollection<Question> Messages { get; set; } = new List<Question>();
    }
}
