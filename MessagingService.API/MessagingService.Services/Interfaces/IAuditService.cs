namespace MessagingService.Services.Interfaces
{
    public interface IAuditService
    {
        void Subscribe(IUserService userService);
        void Subscribe(IMessageService messageService);

    }
}
