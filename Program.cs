using Microsoft.EntityFrameworkCore;
using ProcessoLabSystem.Data;
using ProcessoLabSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SISTEMA UMBRELLA SYSTEM ===");

        using (var db = new ContextoBancoDeDados())
        {
            db.Database.EnsureCreated(); //Cria um arquivo .db caso n�o tenha.
        }

        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("\nMenu Principal:");
            Console.WriteLine("1 - Clientes");
            Console.WriteLine("2 - Fornecedores");
            Console.WriteLine("3 - Produtos");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            var opcao = Console.ReadLine();

            //Op��es que permitem que o usu�rio escolha o Submenu solicitado.
            if (opcao == "1") SubmenuClientes();
            else if (opcao == "2") SubmenuFornecedores();
            else if (opcao == "3") SubmenuProdutos();
            else if (opcao == "0") sair = true;
            else Console.WriteLine("Op��o inv�lida.");
        }
    }

    static bool ValidarCampo<T>(string valor, string propriedade, bool validarFormatoEspecifico = true)
    {
        var property = typeof(T).GetProperty(propriedade);
        if (property == null) return false;

        // Verifica se o input do usu�rio � v�lido ou n�o
        var requiredAttr = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
        if (requiredAttr != null && string.IsNullOrEmpty(valor))
        {
            Console.ForegroundColor = ConsoleColor.Red; //Faz com que a ErrorMessage apare�a com a cor vermelha no terminal.
            Console.WriteLine(requiredAttr.ErrorMessage);
            Console.ResetColor();
            return false;
        }

        // Verifica se o Input do usu�rio preenche os requis�tos solic�tados.
        if (validarFormatoEspecifico)
        {
            if (propriedade == "Email" && !valor.Contains("@")) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Email inv�lido. Deve conter o padr�o: usuario@exemplo.com ");
                Console.ResetColor();
                return false;
            }

            if (propriedade == "CNPJ" && !Regex.IsMatch(valor, @"^\d{14}$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CNPJ deve conter 14 d�gitos num�ricos.");
                Console.ResetColor();
                return false;
            }

            if (propriedade == "Celular" && !Regex.IsMatch(valor, @"^\(\d{2}\) \d{5}-\d{4}$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Formato de celular inv�lido. Use (XX) XXXXX-XXXX");
                Console.ResetColor();
                return false;
            }
        }

        // Valida o limite m�nimo e m�ximo de caracteres que o usu�rio pode escrever.
        var stringLengthAttr = (StringLengthAttribute)property.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault();
        if (stringLengthAttr != null && (valor.Length < stringLengthAttr.MinimumLength || valor.Length > stringLengthAttr.MaximumLength))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(stringLengthAttr.ErrorMessage ?? $"{propriedade} deve ter entre {stringLengthAttr.MinimumLength} e {stringLengthAttr.MaximumLength} caracteres.");
            Console.ResetColor();
            return false;
        }

        return true;
    }

    static void SubmenuClientes()
    {
        using var db = new ContextoBancoDeDados();
        bool voltar = false;

        while (!voltar)
        {
            //Navega��o no Submenu caso o usu�rio escolha a op��o cliente.
            Console.WriteLine("\n--- MENU CLIENTES ---");
            Console.WriteLine("1 - Adicionar");
            Console.WriteLine("2 - Listar");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha: ");

            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                string nome;
                do
                {
                    Console.Write("Nome (m�nimo 3 caracteres): ");
                    nome = Console.ReadLine();
                } while (!ValidarCampo<Cliente>(nome, "Nome", false));

                string email;
                do
                {
                    Console.Write("Email (ex: usuario@exemplo.com): ");
                    email = Console.ReadLine();
                } while (!ValidarCampo<Cliente>(email, "Email"));

                string celular;
                do
                {
                    Console.Write("Celular (Formato: (XX) XXXXX-XXXX): ");
                    celular = Console.ReadLine();
                } while (!ValidarCampo<Cliente>(celular, "Celular"));

                db.Clientes.Add(new Cliente { Nome = nome, Email = email, Celular = celular });
                db.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cliente cadastrado.");
                Console.ResetColor();
            }
            else if (opcao == "2")
            {
                var clientes = db.Clientes.ToList();
                foreach (var c in clientes)
                    Console.WriteLine($"ID: {c.ID} | Nome: {c.Nome} | Email: {c.Email} | Celular: {c.Celular}");
            }
            else if (opcao == "0") voltar = true;
            else Console.WriteLine("Op��o inv�lida.");
        }
    }

    static void SubmenuFornecedores()
    {
        using var db = new ContextoBancoDeDados();
        bool voltar = false;

        while (!voltar)
        {
            //Navega��o no Submenu caso o usu�rio escolha a op��o Fornecedor.
            Console.WriteLine("\n--- MENU FORNECEDORES ---");
            Console.WriteLine("1 - Adicionar");
            Console.WriteLine("2 - Listar");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha: ");

            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                string nome;
                do
                {
                    Console.Write("Nome (m�nimo 3 caracteres): ");
                    nome = Console.ReadLine();
                } while (!ValidarCampo<Fornecedor>(nome, "Nome", false));

                string cnpj;
                do
                {
                    Console.Write("CNPJ (14 d�gitos, apenas n�meros): ");
                    cnpj = Console.ReadLine();
                } while (!ValidarCampo<Fornecedor>(cnpj, "CNPJ"));

                string celular;
                do
                {
                    Console.Write("Celular (Formato: (XX) XXXXX-XXXX): ");
                    celular = Console.ReadLine();
                } while (!ValidarCampo<Fornecedor>(celular, "Celular"));

                db.Fornecedores.Add(new Fornecedor { Nome = nome, CNPJ = cnpj, Celular = celular });
                db.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Fornecedor cadastrado.");
                Console.ResetColor();
            }
            else if (opcao == "2")
            {
                var fornecedores = db.Fornecedores.ToList();
                foreach (var f in fornecedores)
                    Console.WriteLine($"ID: {f.ID} | Nome: {f.Nome} | CNPJ: {f.CNPJ} | Celular: {f.Celular}");
            }
            else if (opcao == "0") voltar = true;
            else Console.WriteLine("Op��o inv�lida.");
        }
    }

    static void SubmenuProdutos()
    {
        using var db = new ContextoBancoDeDados();
        bool voltar = false;

        while (!voltar)
        {
            //Navega��o no Submenu caso o usu�rio escolha a op��o Produtos.
            Console.WriteLine("\n--- MENU PRODUTOS ---");
            Console.WriteLine("1 - Adicionar");
            Console.WriteLine("2 - Listar");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha: ");

            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                string nome;
                do
                {
                    Console.Write("Nome (m�nimo 3 caracteres): ");
                    nome = Console.ReadLine();
                } while (!ValidarCampo<Produto>(nome, "Nome", false));

                decimal preco;
                while (true)
                {
                    Console.Write("Pre�o (maior que 0, ex: 10): ");
                    if (decimal.TryParse(Console.ReadLine(), out preco) && preco > 0)
                        break;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Pre�o inv�lido. Deve ser um valor maior que zero.");
                    Console.ResetColor();
                }

                int quantidade;
                while (true)
                {
                    Console.Write("Quantidade (n�o pode ser negativa): ");
                    if (int.TryParse(Console.ReadLine(), out quantidade) && quantidade >= 0)
                        break;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Quantidade inv�lida. N�o pode ser negativa.");
                    Console.ResetColor();
                }

                db.Produtos.Add(new Produto { Nome = nome, Preco = preco, Quantidade = quantidade });
                db.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Produto cadastrado.");
                Console.ResetColor();
            }
            else if (opcao == "2")
            {
                var produtos = db.Produtos.ToList();
                foreach (var p in produtos)
                    Console.WriteLine($"ID: {p.ID} | Nome: {p.Nome} | Pre�o: {p.Preco:C} | Estoque: {p.Quantidade}");
            }
            else if (opcao == "0") voltar = true;
            else Console.WriteLine("Op��o inv�lida.");
        }
    }
}