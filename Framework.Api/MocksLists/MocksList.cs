using Mocks;

namespace Framework.Api.MocksLists;

public class MocksList : IDisposable
{
    private readonly PersonMock _person = new();


    private static MocksList? _instance;
    public static MocksList Get() => _instance ??= new MocksList();

    private MocksList()
    {
    }


    public void Run()
    {
        _person.RunServer();
        
        _person.SetMocks();
    }

    public void Dispose()
    {
        _person.StopServer();
    }
}