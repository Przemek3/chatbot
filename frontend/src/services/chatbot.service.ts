import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserMessageDto } from '../dtos/out/user-message';
import { UserReactionDto } from '../dtos/out/user-reaction';
import { BotMessageDto } from '../dtos/out/bot-message';

@Injectable({
  providedIn: 'root'  // Dzięki temu serwis jest dostępny w całej aplikacji
})
export class ChatbotService {
  private apiUrl = 'https://localhost:32769/messages/'; // ✅ Nowy endpoint

  constructor(private http: HttpClient) { }

  sendUserMessage(conversationId: string | undefined, text: string): Observable<any> {
    const body: UserMessageDto = {
      conversationId: conversationId,
      text: text
    };

    return this.http.post<any>(this.apiUrl + 'user', body);
  }

  sendReaction(answerId: string, reaction: number): Observable<any> {
    const body: UserReactionDto = {
      answerId: answerId,
      reaction: reaction
    };

    return this.http.post<any>(this.apiUrl + 'reaction', body);
  }

  sendBotMessage(conversationId: string, questionId: string, text: string): Observable<any> {
    const body: BotMessageDto = {
      conversationId: conversationId,
      questionId: questionId,
      text: text
    };

    return this.http.post<any>(this.apiUrl + 'chat', body);
  }
}
