import { Injectable } from "@angular/core";
import { BotMessage } from "./bot-message";
import { UserMessage } from "./users-message";
import { ChatbotService } from "../services/chatbot.service";

export class ChatHistoryNode
{ 
    constructor() { }
    public botMessage: BotMessage | undefined;
    public userMessage: UserMessage | undefined;

    addUserMessage(userMessage: UserMessage): void // jesli kiedys bedzie potrzebne pobieranie od razu calego chatu
    {
        this.userMessage = userMessage;
    }

    addUserMessageText(text: string): ChatHistoryNode
    {
        this.userMessage = new UserMessage();
        this.userMessage.text = text;
        return this;
    }
    
    addUserMessageId(text: string): void
    {
        this.userMessage = new UserMessage();
        this.userMessage.text = text;
    }

    addBotMessageInitial(): void // jesli kiedys bedzie potrzebne pobieranie od razu calego chatu
    {
        this.botMessage = new BotMessage();
    }

    addBotMessageText(text: string): ChatHistoryNode
    {
        if(this.botMessage == undefined)
        {
            this.botMessage = new BotMessage();
        }
        this.botMessage.text = text;
        return this;
    }
    setHideReactions(hideReactions: boolean): void
    {
        if(this.botMessage == undefined)
        {
            this.botMessage = new BotMessage();
        }
        this.botMessage.hideReactions = hideReactions;
    }
}