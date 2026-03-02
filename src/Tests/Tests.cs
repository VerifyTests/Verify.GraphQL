public class Tests
{
    #region ExecutionResultWithData

    [Test]
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
                        {
                            "name", "John"
                        },
                        {
                            "age", 30
                        }
                    }
                }
            }
        };
        return Verify(result);
    }

    #endregion

    #region ExecutionResultWithErrors

    [Test]
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

    #endregion

    #region ExecutionResultWithExtensions

    [Test]
    public Task ExecutionResultWithExtensions()
    {
        var result = new ExecutionResult
        {
            Executed = true,
            Data = new Dictionary<string, object?>
            {
                {
                    "hello", "world"
                }
            },
            Extensions = new()
            {
                {
                    "tracing", "some-trace-data"
                }
            }
        };
        return Verify(result);
    }

    #endregion

    #region ExecutionResultFull

    [Test]
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
                {
                    "hello", "world"
                }
            },
            Errors = [error],
            Extensions = new()
            {
                {
                    "tracing", "some-trace-data"
                }
            }
        };
        return Verify(result);
    }

    #endregion

    #region ExecutionResultNotExecuted

    [Test]
    public Task ExecutionResultNotExecuted()
    {
        var result = new ExecutionResult
        {
            Executed = false,
            Data = new Dictionary<string, object?>
            {
                {
                    "hello", "world"
                }
            }
        };
        return Verify(result);
    }

    #endregion

    #region GraphQLRequest

    [Test]
    public Task GraphQLRequest()
    {
        var request = new GraphQLRequest
        {
            Query = "{ hero { name } }",
            OperationName = "HeroQuery",
            Variables = new(
                new Dictionary<string, object?>
                {
                    {
                        "id", "1"
                    }
                }),
            Extensions = new(
                new Dictionary<string, object?>
                {
                    {
                        "tracing", true
                    }
                })
        };
        return Verify(request);
    }

    #endregion

    #region GraphQLRequestMinimal

    [Test]
    public Task GraphQLRequestMinimal()
    {
        var request = new GraphQLRequest
        {
            Query = "{ hero { name } }"
        };
        return Verify(request);
    }

    #endregion

    #region OperationMessage

    [Test]
    public Task OperationMessage()
    {
        var message = new OperationMessage
        {
            Type = "connection_init",
            Id = "1",
            Payload = new Dictionary<string, object?>
            {
                {
                    "query", "{ hero { name } }"
                }
            }
        };
        return Verify(message);
    }

    #endregion

    #region OperationMessageMinimal

    [Test]
    public Task OperationMessageMinimal()
    {
        var message = new OperationMessage
        {
            Type = "connection_init"
        };
        return Verify(message);
    }

    #endregion

    #region Location

    [Test]
    public Task Location() =>
        Verify(new GraphQLParser.Location(1, 5));

    #endregion
}