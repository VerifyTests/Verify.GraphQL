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

    [Test]
    public async Task ExecutionResultFromExecution()
    {
        var schema = new Schema
        {
            Query = new TestQuery()
        };
        var result = await new DocumentExecuter()
            .ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = "{ hero { name age } }";
            });
        await Verify(result);
    }

    class TestQuery : ObjectGraphType
    {
        public TestQuery() =>
            Field<TestHeroType>("hero")
                .Resolve(_ => new TestHero("Luke", 30));
    }

    record TestHero(string Name, int Age);

    class TestHeroType : ObjectGraphType<TestHero>
    {
        public TestHeroType()
        {
            Field(_ => _.Name);
            Field(_ => _.Age);
        }
    }

    #region Location

    [Test]
    public Task Location() =>
        Verify(new GraphQLParser.Location(1, 5));

    #endregion

    #region ObjectNode

    [Test]
    public Task ObjectNode()
    {
        var root = new RootExecutionNode(new ObjectGraphType(), null);
        var nameField = new FieldType
        {
            Name = "name"
        };
        var ageField = new FieldType
        {
            Name = "age"
        };
        root.SubFields =
        [
            new ValueExecutionNode(
                root, new StringGraphType(), null!, nameField, null)
            {
                Result = "Luke"
            },
            new ValueExecutionNode(
                root, new IntGraphType(), null!, ageField, null)
            {
                Result = 30
            }
        ];
        return Verify(root);
    }

    #endregion

    #region ArrayNode

    [Test]
    public Task ArrayNode()
    {
        var listType = new ListGraphType(new StringGraphType());
        var root = new RootExecutionNode(new ObjectGraphType(), null);
        var field = new FieldType
        {
            Name = "items",
            ResolvedType = listType
        };
        var node = new ArrayExecutionNode(
            root, listType, null!, field, null);
        node.Items =
        [
            new ValueExecutionNode(
                node, new StringGraphType(), null!, field, 0)
            {
                Result = "one"
            },
            new ValueExecutionNode(
                node, new StringGraphType(), null!, field, 1)
            {
                Result = "two"
            },
            new ValueExecutionNode(
                node, new StringGraphType(), null!, field, 2)
            {
                Result = "three"
            }
        ];
        return Verify(node);
    }

    #endregion

    #region ValueNode

    [Test]
    public Task ValueNode()
    {
        var root = new RootExecutionNode(new ObjectGraphType(), null);
        var field = new FieldType
        {
            Name = "name"
        };
        var node = new ValueExecutionNode(
            root, new StringGraphType(), null!, field, null)
        {
            Result = "hello"
        };
        return Verify(node);
    }

    #endregion

    #region NullNode

    [Test]
    public Task NullNode()
    {
        var root = new RootExecutionNode(new ObjectGraphType(), null);
        var field = new FieldType
        {
            Name = "value"
        };
        var node = new NullExecutionNode(
            root, new StringGraphType(), null!, field, null);
        return Verify(node);
    }

    #endregion
}
