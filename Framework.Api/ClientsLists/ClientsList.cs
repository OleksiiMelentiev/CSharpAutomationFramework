using Framework.Api.Clients;

namespace Framework.Api.ClientsLists;

public class ClientsList
{
    private PersonClient? _person;
    public PersonClient Person => _person ??= new PersonClient();


    private static ClientsList? _instance;
    public static ClientsList Get() => _instance ??= new ClientsList();

    private ClientsList()
    {
    }
}