# SharpGrip AuthorizationChecker [![NuGet](https://img.shields.io/nuget/v/SharpGrip.AuthorizationChecker)](https://www.nuget.org/packages/SharpGrip.AuthorizationChecker)

## Builds
[![AuthorizationChecker [Build]](https://github.com/SharpGrip/AuthorizationChecker/actions/workflows/Build.yaml/badge.svg)](https://github.com/SharpGrip/AuthorizationChecker/actions/workflows/Build.yaml)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SharpGrip_AuthorizationChecker&metric=alert_status)](https://sonarcloud.io/summary/overall?id=SharpGrip_AuthorizationChecker) \
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=SharpGrip_AuthorizationChecker&metric=sqale_rating)](https://sonarcloud.io/summary/overall?id=SharpGrip_AuthorizationChecker) \
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=SharpGrip_AuthorizationChecker&metric=reliability_rating)](https://sonarcloud.io/summary/overall?id=SharpGrip_AuthorizationChecker) \
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=SharpGrip_AuthorizationChecker&metric=security_rating)](https://sonarcloud.io/summary/overall?id=SharpGrip_AuthorizationChecker) \
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=SharpGrip_AuthorizationChecker&metric=coverage)](https://sonarcloud.io/summary/overall?id=SharpGrip_AuthorizationChecker)

## Introduction
SharpGrip AuthorizationChecker is a security system that provides an easy and effective way to check access on objects.

## Installation
Reference NuGet package `SharpGrip.AuthorizationChecker` (https://www.nuget.org/packages/SharpGrip.AuthorizationChecker).

Add the AuthorizationChecker security system to your service collection via the extension method.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthorizationChecker();
}
```

## Configuration

### Properties
| Property                       | Default value                        | Description                                                                                                                                            |
| ------------------------------ | ------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| AccessDecisionStrategy         | `AccessDecisionStrategy.Affirmative` | The access decision strategy used to determine if a given mode is allowed on a given subject.                                                          |
| UnanimousVoteAllowIfAllAbstain | `false`                              | Configures the access result outcome on `AccessDecisionStrategy.Unanimous` access decision strategies if all the subject's voters abstain from voting. |

### Access decision strategies
| Strategy                             | Description                                                                         |
| ------------------------------------ | ----------------------------------------------------------------------------------- |
| `AccessDecisionStrategy.Affirmative` | Grants access as soon as there is one `IVoter` instance granting access.            |
| `AccessDecisionStrategy.Majority`    | Grants access if there are more `IVoter` instances granting access than denying it. |
| `AccessDecisionStrategy.Unanimous`   | Grants access if there are no `IVoter` instances denying access.                    |

### Via the `services.AddAuthorizationChecker()` extension method
```
using SharpGrip.AuthorizationChecker.Extensions;
using SharpGrip.AuthorizationChecker.Options;

public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthorizationChecker(options =>
    {
        options.AccessDecisionStrategy = AccessDecisionStrategy.Majority;
        options.UnanimousVoteAllowIfAllAbstain = true;
    });
}
```

### Via the `services.Configure()` method
```
using SharpGrip.AuthorizationChecker.Extensions;
using SharpGrip.AuthorizationChecker.Options;

public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthorizationChecker();

    services.Configure<AuthorizationCheckerOptions>(options =>
    {
        options.AccessDecisionStrategy = AccessDecisionStrategy.Majority;
        options.UnanimousVoteAllowIfAllAbstain = true;
    });
}
```

## Usage

### Adding voters
Create a new class and implement either the `IVoter<TSubject>` or the `IVoter<TSubject, TUser>` interface.

#### `IVoter<TSubject>`
```
public class CarVoter : IVoter<Car>
{
    /// <summary>
    /// Determines if this <see cref="IVoter{TSubject}"/> instance will vote on the mode and subject.
    /// </summary>
    /// <param name="mode">The mode.</param>
    /// <param name="subject">The subject.</param>
    /// <returns>True if this <see cref="IVoter{TSubject}"/> instance will vote, False otherwise.</returns>
    public bool WillVote(string mode, Car subject)
    {
        return true;
    }

    /// <summary>
    /// Votes on the mode and subject.
    /// </summary>
    /// <param name="mode">The mode.</param>
    /// <param name="subject">The subject.</param>
    /// <returns>True if this <see cref="IVoter{TSubject}"/> instance allows access, False otherwise.</returns>
    public bool Vote(string mode, Car subject)
    {
        return mode switch
        {
            "create" => false,
            "read" => true,
            "update" => false,
            "delete" => true,
            _ => false
        };
    }
}
```

#### `IVoter<TSubject, TUser>`
```
public class CarVoter : IVoter<Car, User>
{
    /// <summary>
    /// Determines if this <see cref="IVoter{TSubject, TUser}"/> instance will vote on the mode and subject.
    /// </summary>
    /// <param name="mode">The mode.</param>
    /// <param name="subject">The subject.</param>
    /// <param name="user">The user.</param>
    /// <returns>True if this <see cref="IVoter{TSubject, TUser}"/> instance will vote, False otherwise.</returns>
    public bool WillVote(string mode, Car subject, User user)
    {
        return true;
    }

    /// <summary>
    /// Votes on the mode and subject.
    /// </summary>
    /// <param name="mode">The mode.</param>
    /// <param name="subject">The subject.</param>
    /// <param name="user">The user.</param>
    /// <returns>True if this <see cref="IVoter{TSubject, TUser}"/> instance allows access, False otherwise.</returns>
    public bool Vote(string mode, Car subject, User user)
    {
        return mode switch
        {
            "create" => true,
            "read" => false,
            "update" => true,
            "delete" => false,
            _ => true
        };
    }
}
```

### Checking access
Inject the `IAuthorizationChecker` service and call one of the methods below:

#### Checking access directly
```
Car car = new Car();
User user = new User();

// Resolves to `IVoter<TSubject>` voter instances using the configured (or default) access decision strategy.
bool result = authorizationChecker.IsAllowed("edit", car);

// Resolves to `IVoter<TSubject, TUser>` voter instances using the configured (or default) access decision strategy.
bool result = authorizationChecker.IsAllowed("edit", car, user);

// Resolves to `IVoter<TSubject>` voter instances using the provided (`AccessDecisionStrategy.Unanimous`) access decision strategy.
bool result = authorizationChecker.IsAllowed("edit", car, AccessDecisionStrategy.Unanimous);

// Resolves to `IVoter<TSubject, TUser>` voter instances using the provided (`AccessDecisionStrategy.Unanimous`) access decision strategy.
bool result = authorizationChecker.IsAllowed("edit", car, user, AccessDecisionStrategy.Unanimous);
```

#### Retrieving the authorization result and checking access
```
Car car = new Car();
User user = new User();

// Resolves to `IVoter<TSubject>` voter instances using the configured (or default) access decision strategy.
IAuthorizationResult<Car> authorizationResult = authorizationChecker.GetAuthorizationResult("edit", car);
bool result = authorizationResult.IsAllowed();

// Resolves to `IVoter<TSubject, TUser>` voter instances using the configured (or default) access decision strategy.
IAuthorizationResult<Car> authorizationResult = authorizationChecker.GetAuthorizationResult("edit", car, user);
bool result = authorizationResult.IsAllowed();

// Resolves to `IVoter<TSubject>` voter instances using the provided (`AccessDecisionStrategy.Unanimous`) access decision strategy.
IAuthorizationResult<Car> authorizationResult = authorizationChecker.GetAuthorizationResult("edit", car, AccessDecisionStrategy.Unanimous);
bool result = authorizationResult.IsAllowed();

// Resolves to `IVoter<TSubject, TUser>` voter instances using the provided (`AccessDecisionStrategy.Unanimous`) access decision strategy.
IAuthorizationResult<Car> authorizationResult = authorizationChecker.GetAuthorizationResult("edit", car, user, AccessDecisionStrategy.Unanimous);
bool result = authorizationResult.IsAllowed();
```