import { Component, Input } from "@angular/core";
import { ChatbotService } from "../services/chatbot.service";
import { ChatHistoryNode } from "../models/chat-history";

@Component({
  selector: 'reaction-buttons',
  templateUrl: './reaction-buttons.component.html',
  styleUrls: ['./reaction-buttons.component.scss'],
  imports: [
  ],
  standalone: true
})
export class ReactionButtons {

  @Input() chatHistoryNode: ChatHistoryNode = new ChatHistoryNode();
  constructor(private chatbotService: ChatbotService) { }
    
  selectReaction(reaction: string): void {
    if(this.chatHistoryNode.botMessage != undefined && this.chatHistoryNode.botMessage.messageId != undefined)
    {
        var reactionNumber = 0; // 0 - brak reakcji, 1 - like, 2 - dislike
        this.chatHistoryNode.botMessage.selectedReaction = this.chatHistoryNode.botMessage.selectedReaction === reaction ? null : reaction;
        switch (this.chatHistoryNode.botMessage.selectedReaction) {
        case null:
          reactionNumber = 0;
          break;
        case 'like':
          reactionNumber = 1;
          break;
        case 'dislike':
          reactionNumber = 2;
          break;
        }
        this.chatbotService.sendReaction(this.chatHistoryNode.botMessage.messageId, reactionNumber).subscribe({
          next: (response) => {
            console.log('Odpowiedź z serwera:', response);
          },
          error: (error) => {
            console.error('Błąd:', error);
          }
        });
    }
  }
}