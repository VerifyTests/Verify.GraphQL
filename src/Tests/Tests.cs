public class Tests
{
    #region ExecutionResultWithData

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

    #endregion

    #region ExecutionResultWithErrors

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

    #endregion

    #region ExecutionResultWithExtensions

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

    #endregion

    #region ExecutionResultFull

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

    #endregion

    #region ExecutionResultNotExecuted

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

    #endregion
}
