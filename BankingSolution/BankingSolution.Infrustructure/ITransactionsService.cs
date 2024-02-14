using BankingSolution.Domain;

namespace BankingSolution.Infrustructure
{
    /// <summary>
    /// Interface used to do financial transactions with bank accounts
    /// </summary>
    public interface ITransactionsService
    {
        /// <summary>
        /// Makes deposit to speficed bank account.
        /// </summary>
        /// <param name="amount">Funds to deposit</param>
        /// <param name="destinationAccountId">Id of bank account where need to make a deposit</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task MakeDepositFundsToAccountAsync(decimal amount, uint destinationAccountId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Makes Withdraw from specific bank account
        /// </summary>
        /// <param name="amount">Funds to withdraw</param>
        /// <param name="sourceAccountId">Id of bank account to withdrow</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task MakeWithdrawFundsFromAcountAsync(decimal amount, uint sourceAccountId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Transfer funds between different accounts.
        /// </summary>
        /// <param name="amount">Funds to transfer</param>
        /// <param name="sourceAccountIban">Iban of source bank account</param>
        /// <param name="destinationAccountIban">Iban of destination bank account</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task TransferFundsBetweenAccountsAsync(decimal amount, uint sourceAccountId, uint destinationAccountId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns list of bank transactions done with specific bank account
        /// </summary>
        /// <param name="id">Id of source bank account</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>List of <see cref="BankTransaction"/></returns>
        Task<IEnumerable<BankTransaction>> GetBankTransactionsByAccountIdAsync(uint id, CancellationToken cancellationToken = default);
    }
}
