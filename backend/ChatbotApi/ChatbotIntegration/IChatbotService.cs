using System.Threading.Tasks;

namespace ChatbotIntegration
{
    public interface IChatbotService
    {
        /// <summary>
        /// Wysyła zapytanie do modelu AI i zwraca odpowiedź.
        /// </summary>
        /// <param name="message">Wiadomość użytkownika.</param>
        /// <returns>Odpowiedź AI.</returns>
        Task<string> GetResponseAsync(string message);
    }
}
