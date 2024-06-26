﻿using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PedroMoreira.Application.Common.Abstractions.Authentication;
using PedroMoreira.Application.Common.Repository;
using PedroMoreira.Domain.ErrorMessages;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
    {
        private readonly PasswordHasher<Member> _hasher;
        private readonly IMemberRepository _memberRepository;
        private readonly IAuthTokenGeneratorService _authToken;

        public LoginCommandHandler(
            PasswordHasher<Member> hasher,
            IMemberRepository memberRepository,
            IAuthTokenGeneratorService authToken,
            IValidator<LoginCommand> validator)
        {
            _hasher = hasher;
            _memberRepository = memberRepository;
            _authToken = authToken;
        }
        public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetMemberByName(request.Username);

            if(user is null)
                return Errors.Authentication.InvalidCredentials;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            
            if (result.HasFlag(PasswordVerificationResult.Failed))
                return Errors.Authentication.InvalidCredentials;

            //Get User Roles Based on User and application
            var roles = user.ProjectSecurity
                                                    .FirstOrDefault(a => a.Project.Key == request.AppKey)
                                                    ?.Roles
                                                            .ToList();

            // Generate Token
            // TODO: Need to ajust the list of roles.
            var token = await _authToken.GetAuthToken(user, roles);

            return new AuthenticationResponse()
            {
                Token = token.Token,
                Expire = token.TokenExpire,
                Refresh = token.RefreshToken.Token,
                RefreshExpire = token.RefreshToken.Expire
            };
        }
    }
}
