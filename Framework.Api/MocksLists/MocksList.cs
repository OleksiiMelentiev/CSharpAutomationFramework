using Mocks;

namespace Framework.Api.MocksLists;

public class MocksList : IDisposable
{
    private PersonMock? _person;
    public PersonMock Person => _person ??= new PersonMock();

    private BookMock? _book;
    public BookMock Book => _book ??= new BookMock();

    private static MocksList? _instance;
    public static MocksList Get() => _instance ??= new MocksList();

    private MocksList()
    {
    }

    public void Run()
    {
        Person.RunServer();
        Book.RunServer();
        
        Person.SetMocks();
        Book.SetMocks();
    }

    public void Dispose()
    {
        Person.StopServer();
        Book.StopServer();
    }
}