using Framework.Api.Clients;

namespace Framework.Api.ClientsLists;

public class ClientsList
{
    private PersonClient? _person;
    public PersonClient Person => _person ??= new PersonClient();

    private BookClient? _book;
    public BookClient Book => _book ??= new BookClient();

    private static ClientsList? _instance;
    public static ClientsList Get() => _instance ??= new ClientsList();

    private ClientsList()
    {
    }
}