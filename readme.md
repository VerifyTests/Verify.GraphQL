# Verify.GraphQL

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.GraphQL.svg)](https://www.nuget.org/packages/Verify.GraphQL/)

Adds [Verify](https://github.com/VerifyTests/Verify) support to verify GraphQL.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->

**See [Milestones](../../milestones?state=closed) for release notes.**


## NuGet

 * https://nuget.org/packages/Verify.GraphQL


## Usage

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyGraphQL.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Result with data

<!-- snippet: ExecutionResultWithData -->
<a id='snippet-ExecutionResultWithData'></a>
```cs
[Fact]
public Task ExecutionResultWithData()
{
    var result = new ExecutionResult
    {
        Executed = true,
        Data = new Dictionary<string, object?>
        {
            {
                "user", new Dictionary<string, object?>
                {
                    { "name", "John" },
                    { "age", 30 }
                }
            }
        }
    };
    return Verify(result);
}
```
<sup><a href='/src/Tests/Tests.cs#L5-L27' title='Snippet source file'>snippet source</a> | <a href='#snippet-ExecutionResultWithData' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.ExecutionResultWithData.verified.txt -->
<a id='snippet-Tests.ExecutionResultWithData.verified.txt'></a>
```txt
{
  Data: {
    user: {
      age: 30,
      name: John
    }
  }
}
```
<sup><a href='/src/Tests/Tests.ExecutionResultWithData.verified.txt#L1-L8' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ExecutionResultWithData.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Result with errors

<!-- snippet: ExecutionResultWithErrors -->
<a id='snippet-ExecutionResultWithErrors'></a>
```cs
[Fact]
public Task ExecutionResultWithErrors()
{
    var error = new ExecutionError("Some error")
    {
        Path = ["query", "user"]
    };
    error.AddLocation(new(1, 5));

    var result = new ExecutionResult
    {
        Executed = true,
        Errors = [error]
    };
    return Verify(result);
}
```
<sup><a href='/src/Tests/Tests.cs#L29-L48' title='Snippet source file'>snippet source</a> | <a href='#snippet-ExecutionResultWithErrors' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.ExecutionResultWithErrors.verified.txt -->
<a id='snippet-Tests.ExecutionResultWithErrors.verified.txt'></a>
```txt
{
  Errors: [
    {
      Message: Some error,
      Locations: [
        {
          Line: 1,
          Column: 5
        }
      ],
      Path: [
        query,
        user
      ]
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.ExecutionResultWithErrors.verified.txt#L1-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ExecutionResultWithErrors.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Result with extensions

<!-- snippet: ExecutionResultWithExtensions -->
<a id='snippet-ExecutionResultWithExtensions'></a>
```cs
[Fact]
public Task ExecutionResultWithExtensions()
{
    var result = new ExecutionResult
    {
        Executed = true,
        Data = new Dictionary<string, object?>
        {
            { "hello", "world" }
        },
        Extensions = new()
        {
            { "tracing", "some-trace-data" }
        }
    };
    return Verify(result);
}
```
<sup><a href='/src/Tests/Tests.cs#L50-L70' title='Snippet source file'>snippet source</a> | <a href='#snippet-ExecutionResultWithExtensions' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.ExecutionResultWithExtensions.verified.txt -->
<a id='snippet-Tests.ExecutionResultWithExtensions.verified.txt'></a>
```txt
{
  Data: {
    hello: world
  },
  Extensions: {
    tracing: some-trace-data
  }
}
```
<sup><a href='/src/Tests/Tests.ExecutionResultWithExtensions.verified.txt#L1-L8' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ExecutionResultWithExtensions.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Full result

<!-- snippet: ExecutionResultFull -->
<a id='snippet-ExecutionResultFull'></a>
```cs
[Fact]
public Task ExecutionResultFull()
{
    var error = new ExecutionError("Some error")
    {
        Path = ["query", "user"]
    };
    error.AddLocation(new(1, 5));

    var result = new ExecutionResult
    {
        Executed = true,
        Data = new Dictionary<string, object?>
        {
            { "hello", "world" }
        },
        Errors = [error],
        Extensions = new()
        {
            { "tracing", "some-trace-data" }
        }
    };
    return Verify(result);
}
```
<sup><a href='/src/Tests/Tests.cs#L72-L99' title='Snippet source file'>snippet source</a> | <a href='#snippet-ExecutionResultFull' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.ExecutionResultFull.verified.txt -->
<a id='snippet-Tests.ExecutionResultFull.verified.txt'></a>
```txt
{
  Errors: [
    {
      Message: Some error,
      Locations: [
        {
          Line: 1,
          Column: 5
        }
      ],
      Path: [
        query,
        user
      ]
    }
  ],
  Data: {
    hello: world
  },
  Extensions: {
    tracing: some-trace-data
  }
}
```
<sup><a href='/src/Tests/Tests.ExecutionResultFull.verified.txt#L1-L23' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ExecutionResultFull.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Not executed result

<!-- snippet: ExecutionResultNotExecuted -->
<a id='snippet-ExecutionResultNotExecuted'></a>
```cs
[Fact]
public Task ExecutionResultNotExecuted()
{
    var result = new ExecutionResult
    {
        Executed = false,
        Data = new Dictionary<string, object?>
        {
            { "hello", "world" }
        }
    };
    return Verify(result);
}
```
<sup><a href='/src/Tests/Tests.cs#L101-L117' title='Snippet source file'>snippet source</a> | <a href='#snippet-ExecutionResultNotExecuted' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.ExecutionResultNotExecuted.verified.txt -->
<a id='snippet-Tests.ExecutionResultNotExecuted.verified.txt'></a>
```txt
{}
```
<sup><a href='/src/Tests/Tests.ExecutionResultNotExecuted.verified.txt#L1-L1' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ExecutionResultNotExecuted.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### GraphQL request

<!-- snippet: GraphQLRequest -->
<a id='snippet-GraphQLRequest'></a>
```cs
[Fact]
public Task GraphQLRequest()
{
    var request = new GraphQLRequest
    {
        Query = "{ hero { name } }",
        OperationName = "HeroQuery",
        Variables = new Inputs(new Dictionary<string, object?>
        {
            { "id", "1" }
        }),
        Extensions = new Inputs(new Dictionary<string, object?>
        {
            { "tracing", true }
        })
    };
    return Verify(request);
}
```
<sup><a href='/src/Tests/Tests.cs#L119-L140' title='Snippet source file'>snippet source</a> | <a href='#snippet-GraphQLRequest' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.GraphQLRequest.verified.txt -->
<a id='snippet-Tests.GraphQLRequest.verified.txt'></a>
```txt
{
  Query: { hero { name } },
  OperationName: HeroQuery,
  Variables: {
    id: 1
  },
  Extensions: {
    tracing: true
  }
}
```
<sup><a href='/src/Tests/Tests.GraphQLRequest.verified.txt#L1-L10' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.GraphQLRequest.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Operation message

<!-- snippet: OperationMessage -->
<a id='snippet-OperationMessage'></a>
```cs
[Fact]
public Task OperationMessage()
{
    var message = new OperationMessage
    {
        Type = "connection_init",
        Id = "1",
        Payload = new Dictionary<string, object?>
        {
            { "query", "{ hero { name } }" }
        }
    };
    return Verify(message);
}
```
<sup><a href='/src/Tests/Tests.cs#L156-L173' title='Snippet source file'>snippet source</a> | <a href='#snippet-OperationMessage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.OperationMessage.verified.txt -->
<a id='snippet-Tests.OperationMessage.verified.txt'></a>
```txt
{
  Type: connection_init,
  Id: 1,
  Payload: {
    query: { hero { name } }
  }
}
```
<sup><a href='/src/Tests/Tests.OperationMessage.verified.txt#L1-L7' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.OperationMessage.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
