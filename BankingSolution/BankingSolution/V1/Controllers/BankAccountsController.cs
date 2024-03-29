﻿using Asp.Versioning;
using AutoMapper;
using BankingSolution.Contract.Rest.V1.Models;
using BankingSolution.Contract.Rest.V1.Transport.Requests;
using BankingSolution.Contract.Rest.V1.Transport.Responses;
using BankingSolution.Domain.IntermediateModels;
using BankingSolution.Infrustructure;
using Microsoft.AspNetCore.Mvc;

namespace BankingSolution.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{v:apiVersion}/[controller]")]
    public class BankAccountsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBankAccountService _bankAccountService;

        public BankAccountsController(IMapper mapper, IBankAccountService bankAccountService)
        {
            _mapper = mapper;
            _bankAccountService = bankAccountService;
        }

        /// <summary>
        /// Create new bank account.
        /// </summary>
        /// <param name="request"><see cref="CreateBankAccountRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CreateBankAccountResponse> CreateBankAccountAsync([FromBody] CreateBankAccountRequest request, CancellationToken cancellationToken = default) =>
           new CreateBankAccountResponse
           {
               AccountId = await _bankAccountService.CreateBankAccountAsync(_mapper.Map<BankAccountCreationDetails>(request), cancellationToken)
           };

        /// <summary>
        /// Returns list with all created bank accounts
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>List of <see cref="BankAccount"/></returns>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetBankAccountsResponse> GetBankAccountAsync(CancellationToken cancellationToken = default) => new GetBankAccountsResponse()
        {
            BankAccounts = (await _bankAccountService.GetBankAccountListAsync(cancellationToken))
            .Select(a => _mapper.Map<BankAccount>(a))
        };

        /// <summary>
        /// Returns bank account by specified ID
        /// </summary>
        /// <param name="request"><see cref="GetBankAccountRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="GetBankAccountResponse"/></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetBankAccountResponse> GetBankAccountByIdAsync([FromRoute]GetBankAccountRequest request, CancellationToken cancellationToken = default) =>
            new GetBankAccountResponse()
            {
                Account = _mapper.Map<BankAccount>(await _bankAccountService.GetBankAccountByIdAsync(request.Id, cancellationToken)),
            };
    }
}
