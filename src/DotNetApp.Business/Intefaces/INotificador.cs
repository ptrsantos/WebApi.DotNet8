using DotNetApp.Business.Notificacoes;

namespace DotNetApp.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}