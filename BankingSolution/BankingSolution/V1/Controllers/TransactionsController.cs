using Asp.Versioning;
using AutoMapper;
using BankingSolution.Contract.Rest.V1.Models;
using BankingSolution.Contract.Rest.V1.Transport.Requests;
using BankingSolution.Contract.Rest.V1.Transport.Responses;
using BankingSolution.Infrustructure;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{v:apiVersion}/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(IMapper mapper, ITransactionsService transactionsService)
        {
            _mapper = mapper;
            _transactionsService = transactionsService;
        }

        /// <summary>
        /// Returns list of bank transactions made by specifed account
        /// </summary>
        /// <param name="request"><see cref="GetBankTransactionListByAccountIdRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="GetBankTransactionListByAccountIdResponse"/></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetBankTransactionListByAccountIdResponse> GetBankTransactionListByAccountIdAsync(
            [FromQuery]GetBankTransactionListByAccountIdRequest request, CancellationToken cancellationToken = default) =>
            new GetBankTransactionListByAccountIdResponse
            {
                Transactions = (await _transactionsService.GetBankTransactionsByAccountIdAsync(request.AccountId, cancellationToken))
                    .Select(t => _mapper.Map<BankTransaction>(t))
            };

        /// <summary>
        /// Adds deposit to specified account.
        /// </summary>
        /// <param name="request"><see cref="MakeDepositFundsToAccountRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        [HttpPut("deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task MakeDepositFundsToAccountAsync(
            [FromBody]MakeDepositFundsToAccountRequest request, CancellationToken cancellationToken = default) =>
            await _transactionsService.MakeDepositFundsToAccountAsync(request.Amount, request.AccountId, cancellationToken);

        /// <summary>
        /// Makes withdraw from the accoutn.
        /// </summary>
        /// <param name="request"><see cref="MakeWithdrawFundsRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        [HttpPut("withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task MakeWithdrawFundsFromAcountAsync(
            [FromBody] MakeWithdrawFundsRequest request, CancellationToken cancellationToken = default) =>
            await _transactionsService.MakeWithdrawFundsFromAcountAsync(request.Amount, request.AccountId, cancellationToken);

        /// <summary>
        /// Transfer funds between 2 accounts
        /// </summary>
        /// <param name="request"><see cref="TransferFundsBetweenAccountsRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        [HttpPost("transfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task TransferFundsBetweenAccountsAsync(
            [FromBody] TransferFundsBetweenAccountsRequest request, CancellationToken cancellationToken = default) =>
            await _transactionsService.TransferFundsBetweenAccountsAsync(request.Amount, request.SourceAccountId, request.DestinationAccountId, cancellationToken);
    }
}
