using AuthApi.Entities;

namespace AuthApi.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email); // Busca o usuario pelo email pra verficiar a senha
    Task<bool> ExistsByEmailAsync(string email); //Antes de criar um usuario, valida se o email ja esta em uso
    Task AddAsync(User user); // Adiciona o novo usuario no contexto do EF Core, sem salvar no banco ainda
    Task SaveChangesAsync(); // Confirma as mudancas pendentes no bnanco de uma vez, foi separado do Add pra poder agrupar varias operacoes pendentes numa unica operacao.
}
