using SistemaHotel.Models;

namespace SistemaHotel.Services;

public interface IClienteService
{
    IEnumerable<Cliente> BuscarClientes(string busqueda);
}