using AmbiClean.Repositories;
using AmbiClean.Models;
using AmbiClean.Database;

class Program
{
    static void Main(string[] args)
    {
        var usuarioRepo = new UsuarioRepository();

        //MOSTRAR TODOS OK
        //List<Usuario> usuarios = usuarioRepo.BuscarTodos();

        //Console.WriteLine("Usuarios encontrados");

        //foreach (var usua in usuarios)
        //{
        //    Console.WriteLine($"ID: {usua.Id}");
        //    Console.WriteLine($"Nome: {usua.NomeCompleto}");
        //    Console.WriteLine($"Email: {usua.Email}");
        //    Console.WriteLine($"Telefone: {usua.Telefone}");
        //    Console.WriteLine($"Ativo: {(usua.Ativo ? "Sim" : "Não")}");
        //    Console.WriteLine($"Cargo ID: {usua.CargoId}");
        //    Console.WriteLine("-----------------------------");
        //}


        //INSERIR OK
        //var novoUsuario = new Usuario
        //{
        //    NomeCompleto = "Pedro",
        //    Email = "Pedro@email.com",
        //    Senha = "7855",
        //    TurnoId = 3,
        //    Telefone = "(11)98745-9999",
        //    Ativo = true,
        //    CargoId = 4
        //};

        //bool inserido = usuarioRepo.Inserir(novoUsuario);
        //Console.WriteLine($"Usuário inserido: {inserido}");

        //BUSCARPORID OK
        //var usuario = usuarioRepo.BuscarPorId(5);

        //if(usuario != null)
        //{
        //    Console.WriteLine($"Usuario {usuario.NomeCompleto} encontrado pelo id {usuario.Id}");
        //}
        //else
        //{
        //    Console.WriteLine("Usuario não encontrado");
        //}

        //ATUALIZAR OK
        //Usuario usuario = new Usuario
        //{
        //    Id = 2,
        //    NomeCompleto = "Carla da Silva"
        //};

        //usuarioRepo.Atualizar(usuario);

        //DELETAR OK
        //usuarioRepo.Excluir(1);


        Database.Instance.CloseConnection();
    }
}



