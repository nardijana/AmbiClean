using System;
using System.Collections.Generic;
using AmbiClean.Models;
using AmbiClean.Repositories;

class Program
{
    static void Main(string[] args)
    {
        // instanciar todos os repositories
        var areaRepo = new AreaRepository();
        var atribRepo = new AtribuicaoUsuarioAreaRepository();
        var cargoRepo = new CargoRepository();
        var checklistRepo = new ChecklistRepository();
        var execRepo = new ExecucaoTarefaRepository();
        var itemRepo = new ItemChecklistRepository();
        var localRepo = new LocalRepository();
        var pontoRepo = new RegistroPontoRepository();
        var resultadoRepo = new ResultadoChecklistRepository();
        var tarefaRepo = new TarefaRepository();
        var turnoRepo = new TurnoRepository();
        var usuarioRepo = new UsuarioRepository();

        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("====== MENU PRINCIPAL ======");
            Console.WriteLine("1 - Area");
            Console.WriteLine("2 - AtribuicaoUsuarioArea");
            Console.WriteLine("3 - Cargo");
            Console.WriteLine("4 - Checklist");
            Console.WriteLine("5 - ExecucaoTarefa");
            Console.WriteLine("6 - ItemChecklist");
            Console.WriteLine("7 - Local");
            Console.WriteLine("8 - RegistroPonto");
            Console.WriteLine("9 - ResultadoChecklist");
            Console.WriteLine("10 - Tarefa");
            Console.WriteLine("11 - Turno");
            Console.WriteLine("12 - Usuario");
            Console.WriteLine("13 - Sair");
            Console.Write("Escolha uma opção: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: MenuArea(areaRepo); break;
                case 2: MenuAtribuicao(atribRepo); break;
                case 3: MenuCargo(cargoRepo); break;
                case 4: MenuChecklist(checklistRepo); break;
                case 5: MenuExecucao(execRepo); break;
                case 6: MenuItemChecklist(itemRepo); break;
                case 7: MenuLocal(localRepo); break;
                case 8: MenuRegistroPonto(pontoRepo); break;
                case 9: MenuResultadoChecklist(resultadoRepo); break;
                case 10: MenuTarefa(tarefaRepo); break;
                case 11: MenuTurno(turnoRepo); break;
                case 12: MenuUsuario(usuarioRepo); break;
                case 13:
                    Console.WriteLine("Encerrando...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Pressione ENTER.");
                    Console.ReadLine();
                    break;
            }

        } while (opcao != 13);

        // fechar conexão singleton (se necessário)
        try { AmbiClean.Database.Database.Instance.CloseConnection(); } catch { }
    }

    #region Helpers
    static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        int.TryParse(Console.ReadLine(), out int v);
        return v;
    }

    static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    static bool ReadBool(string prompt)
    {
        Console.Write(prompt);
        var s = Console.ReadLine();
        if (s == "1" || s?.ToLower() == "s" || s?.ToLower() == "sim") return true;
        return false;
    }

    static DateTime ReadDateTime(string prompt)
    {
        Console.Write(prompt);
        var s = Console.ReadLine();
        if (DateTime.TryParse(s, out DateTime dt)) return dt;
        return DateTime.Now;
    }

    static TimeSpan ReadTimeSpan(string prompt)
    {
        Console.Write(prompt);
        var s = Console.ReadLine();
        if (TimeSpan.TryParse(s, out TimeSpan ts)) return ts;
        return TimeSpan.Zero;
    }
    #endregion

    #region Menu Area
    static void MenuArea(AreaRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== AREA ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    Console.WriteLine("Áreas:");
                    foreach (var a in lista)
                    {
                        Console.WriteLine($"{a.AreaId} - {a.Nome} (LocalId: {a.LocalId}) - {a.Descricao}");
                    }
                    Console.ReadLine();
                    break;
                case 2:
                    var nova = new Area
                    {
                        LocalId = ReadInt("LocalId: "),
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: ")
                    };
                    Console.WriteLine(repo.Inserir(nova) ? "Inserida" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var id = ReadInt("Id: ");
                    var area = repo.BuscarPorId(id);
                    if (area == null) Console.WriteLine("Não encontrada");
                    else Console.WriteLine($"{area.AreaId} - {area.Nome} - {area.LocalId}");
                    Console.ReadLine();
                    break;
                case 4:
                    var upd = new Area
                    {
                        AreaId = ReadInt("Id: "),
                        LocalId = ReadInt("LocalId: "),
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: ")
                    };
                    repo.Atualizar(upd);
                    Console.WriteLine("Atualizada");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluída");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu AtribuicaoUsuarioArea
    static void MenuAtribuicao(AtribuicaoUsuarioAreaRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== ATRIBUICAO USUARIO AREA ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var x in lista)
                        Console.WriteLine($"{x.Id} - Usuario:{x.UsuarioId} Area:{x.AreaId} AtribuidoEm:{x.AtribuidoEm}");
                    Console.ReadLine();
                    break;
                case 2:
                    var nova = new AtribuicaoUsuarioArea
                    {
                        UsuarioId = ReadInt("UsuarioId: "),
                        AreaId = ReadInt("AreaId: "),
                        AtribuidoEm = ReadDateTime("AtribuidoEm (yyyy-MM-dd HH:mm) ou ENTER para agora: ")
                    };
                    if (nova.AtribuidoEm == DateTime.MinValue) nova.AtribuidoEm = DateTime.Now;
                    Console.WriteLine(repo.Inserir(nova) ? "Inserida" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var obj = repo.BuscarPorId(ReadInt("Id: "));
                    if (obj == null) Console.WriteLine("Não encontrada");
                    else Console.WriteLine($"{obj.Id} - U:{obj.UsuarioId} A:{obj.AreaId} Em:{obj.AtribuidoEm}");
                    Console.ReadLine();
                    break;
                case 4:
                    var upd = new AtribuicaoUsuarioArea
                    {
                        Id = ReadInt("Id: "),
                        UsuarioId = ReadInt("UsuarioId: "),
                        AreaId = ReadInt("AreaId: "),
                        AtribuidoEm = ReadDateTime("AtribuidoEm: ")
                    };
                    repo.Atualizar(upd);
                    Console.WriteLine("Atualizada");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluída");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu Cargo
    static void MenuCargo(CargoRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== CARGO ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var c in lista)
                        Console.WriteLine($"{c.Id} - {c.Nome} - {c.Descricao}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new Cargo
                    {
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var cfound = repo.BuscarPorId(ReadInt("Id: "));
                    if (cfound == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{cfound.Id} - {cfound.Nome}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new Cargo
                    {
                        Id = ReadInt("Id: "),
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu Checklist
    static void MenuChecklist(ChecklistRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== CHECKLIST ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var c in lista)
                        Console.WriteLine($"{c.Id} - {c.Nome} - {c.Descricao} - Ativo:{c.Ativo}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new Checklist
                    {
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: "),
                        Ativo = ReadBool("Ativo? (1/0): ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var cf = repo.BuscarPorId(ReadInt("Id: "));
                    if (cf == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{cf.Id} - {cf.Nome} - Ativo:{cf.Ativo}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new Checklist
                    {
                        Id = ReadInt("Id: "),
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: "),
                        Ativo = ReadBool("Ativo? (1/0): ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu ExecucaoTarefa
    static void MenuExecucao(ExecucaoTarefaRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== EXECUCAO TAREFA ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var e in lista)
                        Console.WriteLine($"{e.Id} - Tarefa:{e.TarefaId} Usuario:{e.UsuarioId} Status:{e.Status}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new ExecucaoTarefa
                    {
                        TarefaId = ReadInt("TarefaId: "),
                        UsuarioId = ReadInt("UsuarioId: "),
                        AgendadoInicio = ReadDateTime("AgendadoInicio (yyyy-MM-dd HH:mm): "),
                        AgendadoFim = ReadDateTime("AgendadoFim (yyyy-MM-dd HH:mm): "),
                        Inicio = ReadDateTime("Inicio (yyyy-MM-dd HH:mm) ou ENTER para agora: "),
                        Fim = ReadDateTime("Fim (yyyy-MM-dd HH:mm) ou ENTER para agora: "),
                        Status = ReadString("Status: "),
                        Observacoes = ReadString("Observacoes: "),
                        CriadoEm = DateTime.Now
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserida" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var eobj = repo.BuscarPorId(ReadInt("Id: "));
                    if (eobj == null) Console.WriteLine("Não encontrada");
                    else Console.WriteLine($"{eobj.Id} - Status:{eobj.Status} - Inicio:{eobj.Inicio}");
                    Console.ReadLine();
                    break;
                case 4:
                    var upd = new ExecucaoTarefa
                    {
                        Id = ReadInt("Id: "),
                        TarefaId = ReadInt("TarefaId: "),
                        UsuarioId = ReadInt("UsuarioId: "),
                        AgendadoInicio = ReadDateTime("AgendadoInicio: "),
                        AgendadoFim = ReadDateTime("AgendadoFim: "),
                        Inicio = ReadDateTime("Inicio: "),
                        Fim = ReadDateTime("Fim: "),
                        Status = ReadString("Status: "),
                        Observacoes = ReadString("Observacoes: "),
                        CriadoEm = ReadDateTime("CriadoEm: ")
                    };
                    repo.Atualizar(upd);
                    Console.WriteLine("Atualizada");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluída");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu ItemChecklist
    static void MenuItemChecklist(ItemChecklistRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== ITEM CHECKLIST ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var i in lista)
                        Console.WriteLine($"{i.Id} - Checklist:{i.ChecklistId} Pos:{i.Posicao} - {i.Descricao}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new ItemChecklist
                    {
                        ChecklistId = ReadInt("ChecklistId: "),
                        Descricao = ReadString("Descricao: "),
                        Posicao = ReadInt("Posicao: ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var item = repo.BuscarPorId(ReadInt("Id: "));
                    if (item == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{item.Id} - {item.Descricao}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new ItemChecklist
                    {
                        Id = ReadInt("Id: "),
                        ChecklistId = ReadInt("ChecklistId: "),
                        Descricao = ReadString("Descricao: "),
                        Posicao = ReadInt("Posicao: ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu Local
    static void MenuLocal(LocalRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== LOCAL ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var l in lista)
                        Console.WriteLine($"{l.Id} - {l.Nome} - {l.Endereco} - Ativo:{l.Ativo}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new Local
                    {
                        Nome = ReadString("Nome: "),
                        Endereco = ReadString("Endereco: "),
                        Ativo = ReadBool("Ativo? (1/0): ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var obj = repo.BuscarPorId(ReadInt("Id: "));
                    if (obj == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{obj.Id} - {obj.Nome} - {obj.Endereco}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new Local
                    {
                        Id = ReadInt("Id: "),
                        Nome = ReadString("Nome: "),
                        Endereco = ReadString("Endereco: "),
                        Ativo = ReadBool("Ativo? (1/0): ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu RegistroPonto
    static void MenuRegistroPonto(RegistroPontoRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO PONTO ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var p in lista)
                        Console.WriteLine($"{p.Id} - {p.TipoRegistro} - Ref:{p.ReferenciaId} - {p.RegistradoEm} - {p.Dispositivo}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new RegistroPonto
                    {
                        TipoRegistro = ReadString("TipoRegistro: "),
                        ReferenciaId = ReadInt("ReferenciaId: "),
                        RegistradoEm = ReadDateTime("RegistradoEm (yyyy-MM-dd HH:mm) ou ENTER para agora: "),
                        Dispositivo = ReadString("Dispositivo: ")
                    };
                    if (novo.RegistradoEm == DateTime.MinValue) novo.RegistradoEm = DateTime.Now;
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var obj = repo.BuscarPorId(ReadInt("Id: "));
                    if (obj == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{obj.Id} - {obj.TipoRegistro} - {obj.RegistradoEm}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new RegistroPonto
                    {
                        Id = ReadInt("Id: "),
                        TipoRegistro = ReadString("TipoRegistro: "),
                        ReferenciaId = ReadInt("ReferenciaId: "),
                        RegistradoEm = ReadDateTime("RegistradoEm: "),
                        Dispositivo = ReadString("Dispositivo: ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu ResultadoChecklist
    static void MenuResultadoChecklist(ResultadoChecklistRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== RESULTADO CHECKLIST ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var r in lista)
                        Console.WriteLine($"{r.Id} - Exec:{r.ExecucaoId} - Checklist:{r.ChecklistId} - Marcado:{r.Marcado} - {r.MarcadoEm}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new ResultadoChecklist
                    {
                        ExecucaoId = ReadInt("ExecucaoId: "),
                        ChecklistId = ReadInt("ChecklistId: "),
                        Marcado = ReadBool("Marcado? (1/0): "),
                        Comentario = ReadString("Comentario: "),
                        MarcadoEm = ReadDateTime("MarcadoEm (yyyy-MM-dd HH:mm) ou ENTER para agora: ")
                    };
                    if (novo.MarcadoEm == DateTime.MinValue) novo.MarcadoEm = DateTime.Now;
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var robj = repo.BuscarPorId(ReadInt("Id: "));
                    if (robj == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{robj.Id} - Exec:{robj.ExecucaoId} - Marcado:{robj.Marcado}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new ResultadoChecklist
                    {
                        Id = ReadInt("Id: "),
                        ExecucaoId = ReadInt("ExecucaoId: "),
                        ChecklistId = ReadInt("ChecklistId: "),
                        Marcado = ReadBool("Marcado? (1/0): "),
                        Comentario = ReadString("Comentario: "),
                        MarcadoEm = ReadDateTime("MarcadoEm: ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu Tarefa
    static void MenuTarefa(TarefaRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== TAREFA ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var t in lista)
                        Console.WriteLine($"{t.Id} - {t.Nome} - Area:{t.AreaId} - Checklist:{t.ChecklistId} - Min:{t.MinutosEstimados} - Ativo:{t.Ativo}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new Tarefa
                    {
                        AreaId = ReadInt("AreaId: "),
                        ChecklistId = ReadInt("ChecklistId: "),
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: "),
                        MinutosEstimados = ReadInt("MinutosEstimados: "),
                        Ativo = ReadBool("Ativo? (1/0): ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var tobj = repo.BuscarPorId(ReadInt("Id: "));
                    if (tobj == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{tobj.Id} - {tobj.Nome} - Area:{tobj.AreaId}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new Tarefa
                    {
                        Id = ReadInt("Id: "),
                        AreaId = ReadInt("AreaId: "),
                        ChecklistId = ReadInt("ChecklistId: "),
                        Nome = ReadString("Nome: "),
                        Descricao = ReadString("Descricao: "),
                        MinutosEstimados = ReadInt("MinutosEstimados: "),
                        Ativo = ReadBool("Ativo? (1/0): ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu Turno
    static void MenuTurno(TurnoRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== TURNO ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var t in lista)
                        Console.WriteLine($"{t.Id} - {t.Nome} - {t.HoraInicio} -> {t.HoraFim}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new Turno
                    {
                        Nome = ReadString("Nome: "),
                        HoraInicio = ReadTimeSpan("HoraInicio (HH:mm): "),
                        HoraFim = ReadTimeSpan("HoraFim (HH:mm): "),
                        Descricao = ReadString("Descricao: ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var tfound = repo.BuscarPorId(ReadInt("Id: "));
                    if (tfound == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{tfound.Id} - {tfound.Nome} - {tfound.HoraInicio}-{tfound.HoraFim}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new Turno
                    {
                        Id = ReadInt("Id: "),
                        Nome = ReadString("Nome: "),
                        HoraInicio = ReadTimeSpan("HoraInicio (HH:mm): "),
                        HoraFim = ReadTimeSpan("HoraFim (HH:mm): "),
                        Descricao = ReadString("Descricao: ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion

    #region Menu Usuario
    static void MenuUsuario(UsuarioRepository repo)
    {
        int op;
        do
        {
            Console.Clear();
            Console.WriteLine("=== USUARIO ===");
            Console.WriteLine("1 - Listar todos");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar por ID");
            Console.WriteLine("4 - Atualizar");
            Console.WriteLine("5 - Excluir");
            Console.WriteLine("6 - Voltar");
            op = ReadInt("Escolha: ");

            switch (op)
            {
                case 1:
                    var lista = repo.BuscarTodos();
                    foreach (var u in lista)
                        Console.WriteLine($"{u.Id} - {u.NomeCompleto} - {u.Email} - Turno:{u.TurnoId} - Cargo:{u.CargoId} - Ativo:{u.Ativo}");
                    Console.ReadLine();
                    break;
                case 2:
                    var novo = new Usuario
                    {
                        NomeCompleto = ReadString("NomeCompleto: "),
                        Email = ReadString("Email: "),
                        Senha = ReadString("Senha: "),
                        TurnoId = ReadInt("TurnoId: "),
                        Telefone = ReadString("Telefone: "),
                        Ativo = ReadBool("Ativo? (1/0): "),
                        CargoId = ReadInt("CargoId: ")
                    };
                    Console.WriteLine(repo.Inserir(novo) ? "Inserido" : "Erro");
                    Console.ReadLine();
                    break;
                case 3:
                    var uobj = repo.BuscarPorId(ReadInt("Id: "));
                    if (uobj == null) Console.WriteLine("Não encontrado");
                    else Console.WriteLine($"{uobj.Id} - {uobj.NomeCompleto} - {uobj.Email}");
                    Console.ReadLine();
                    break;
                case 4:
                    var up = new Usuario
                    {
                        Id = ReadInt("Id: "),
                        NomeCompleto = ReadString("NomeCompleto: "),
                        Email = ReadString("Email: "),
                        Senha = ReadString("Senha: "),
                        TurnoId = ReadInt("TurnoId: "),
                        Telefone = ReadString("Telefone: "),
                        Ativo = ReadBool("Ativo? (1/0): "),
                        CargoId = ReadInt("CargoId: ")
                    };
                    repo.Atualizar(up);
                    Console.WriteLine("Atualizado");
                    Console.ReadLine();
                    break;
                case 5:
                    repo.Excluir(ReadInt("Id a excluir: "));
                    Console.WriteLine("Excluído");
                    Console.ReadLine();
                    break;
            }
        } while (op != 6);
    }
    #endregion
}