export class BotMessage {
    messageId: string | undefined;
    text: string | undefined;
    public hideReactions: boolean = true;
    public selectedReaction: string | null = null;
}