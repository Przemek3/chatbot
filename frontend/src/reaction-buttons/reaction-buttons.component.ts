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
        this.chatHistoryNode.botMessage.selectedReaction = this.chatHistoryNode.botMessage.selectedReaction === reaction ? null : reaction;
        switch (this.chatHistoryNode.botMessage.selectedReaction) {
        case null:
          this.chatbotService.sendReaction(this.chatHistoryNode.botMessage.messageId,0);
          break;
        case 'like':
          this.chatbotService.sendReaction(this.chatHistoryNode.botMessage.messageId,1);
          break;
        case 'dislike':
          this.chatbotService.sendReaction(this.chatHistoryNode.botMessage.messageId,2);
          break;
        default:
      }
    }
}
}