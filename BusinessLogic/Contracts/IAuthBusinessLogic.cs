﻿
using Domain.Entities;

namespace BusinessLogic.Contracts;
public interface IAuthBusinessLogic
{
    Task Authenticate(User user);
    Task<string> Login(Credentials credentials);
}
