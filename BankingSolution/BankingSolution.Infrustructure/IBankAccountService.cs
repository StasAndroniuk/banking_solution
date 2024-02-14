using BankingSolution.Domain;
using BankingSolution.Domain.IntermediateModels;

namespace BankingSolution.Infrustructure
{
    public interface IBankAccountService
    {
        /// <summary>
        /// Creates new bank accoutn with provided details. use existing account owner if it was found.
        /// </summary>
        /// <param name="details">Details for bank account.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task<uint> CreateBankAccountAsync(BankAccountCreationDetails details, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list with all existing bank accoutns.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>List of bank accounts <see cref="BankAccount"/></returns>
        Task<IEnumerable<BankAccount>> GetBankAccountListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns specific bank account  by id
        /// </summary>
        /// <param name="id">Id of bank account.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Entity of <see cref="BankAccount"/></returns>
        Task<BankAccount> GetBankAccountByIdAsync(uint id, CancellationToken cancellationToken = default);
    }
}
