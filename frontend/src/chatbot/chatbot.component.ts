import { Component, OnInit } from '@angular/core';
import { ChatbotService } from '../services/chatbot.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { ChatHistoryNode } from '../models/chat-history';
import { BotMessageSavedResponse } from '../dtos/in/bot-message-saved-response';
import { ReactionButtons } from "../reaction-buttons/reaction-buttons.component";

@Component({
  selector: 'app-chatbot',
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.scss'],
  imports: [
    MatCardModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    FormsModule,
    ReactionButtons
],
  standalone: true
})
export class ChatbotComponent {
  userMessage: string = '';
  isGenerating: boolean = false;
  conversationId: string | undefined;
  chatHistory: Array<ChatHistoryNode> = [];
  index = 0;

  constructor(private chatbotService: ChatbotService) { }

  sendUserMessage(): void {
    if (this.userMessage.trim()) {
      const messageToSend = this.userMessage;


      // Dodaj wiadomość użytkownika do historii
      this.chatHistory.push((new ChatHistoryNode()).addUserMessageText(messageToSend));
 
      // Wyczyść pole wejściowe
      this.userMessage = '';

      this.chatbotService.sendUserMessage(this.conversationId, messageToSend).subscribe({
        next: (response) => {
          console.log('Odpowiedź z serwera:', response);
          if(this.conversationId == undefined)
          {
            this.conversationId = response.conversationId;
          }
          this.chatHistory[this.chatHistory.length - 1].userMessage!.messageId = response.userMessageId;
          this.simulateTypingEffect(response.responseText || 'Brak odpowiedzi');
        },
        error: (error) => {
          console.error('Błąd:', error);
        }
      });
    }
  }

  simulateTypingEffect(fullMessage: string): void {
    let currentMessage = '';
    this.index = 0;
    this.isGenerating = true;
    
    const interval = setInterval(() => {
      if (this.index < fullMessage.length) {
        currentMessage += fullMessage[this.index];
        if (this.chatHistory.length > 0 && this.chatHistory[this.chatHistory.length - 1].botMessage != null) {
          this.chatHistory[this.chatHistory.length - 1].addBotMessageText(currentMessage);
        } else {
          this.chatHistory[this.chatHistory.length - 1].addBotMessageInitial();
        }
        this.index++;
      } else {
        clearInterval(interval);
        this.chatHistory[this.chatHistory.length - 1].setHideReactions(false);
        this.chatbotService.sendBotMessage(
          this.conversationId ?? '3fa85f64-5717-4562-b3fc-2c963f66afa6', 
          this.chatHistory[this.chatHistory.length - 1].userMessage!.messageId ?? '3fa85f64-5717-4562-b3fc-2c963f66afa6', 
          this.chatHistory[this.chatHistory.length - 1].botMessage!.text!).subscribe({
            next: (response: BotMessageSavedResponse) => {
              this.chatHistory[this.chatHistory.length - 1].botMessage!.messageId = response.botMessageId;
            },
            error: (error) => {
              console.error('Błąd:', error);
            }
          });
        this.isGenerating = false;
      }
    }, 5); // Czas między literami (możesz dostosować)
  }

  stopGenerating() : void {
    this.index = 100000;;
    this.isGenerating = false;
  }
}
