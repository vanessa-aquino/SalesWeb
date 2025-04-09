using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly SalesWebContext _context; // Representação do banco de dados

        public DepartmentsController(SalesWebContext context) // Esse construtor permite que o Controller acesse a tabela Department do banco de dados
        {
            _context = context;
        }

        // GET - Listar todos os departamentos:
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department.ToListAsync());
            // Busca todos os departamentos do banco de dados e retorna uma View com essa lista.
        }

        // GET - Ver detalhes de um departamento:
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Busca o departamento com o ID fornecido
            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id); 
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET - Formulário para criar um departamento:
        public IActionResult Create()
        {
            // Retorna uma view vazia, onde o usuário pode preencher os dados do novo departamento
            return View(); 
        }

        // POST: Criar um novo departamento:
        [HttpPost] // Esse método só responde a resquisições POST
        [ValidateAntiForgeryToken] // Protege contra ataques CSRF
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department) // Recebe os dados preenchidos pelo usuário na View de criação
        {
            if (ModelState.IsValid) // Verifica se são válidos
            {
                _context.Add(department); // Add o novo departamento ao banco de dados
                await _context.SaveChangesAsync(); // Salva as mudanças
                return RedirectToAction(nameof(Index)); // Redireciona para a listagem Index
            }
            return View(department); // Se os dados não forem válidos, retorna a View com erros
        }

        // GET - Carregar formulário para edição:
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id); // Busca o departamento no BD
            if (department == null)
            {
                return NotFound();
            }
            return View(department); // Se existir, exibe a view de edição
        }

        // POST: Atualizar um departamento:
        [HttpPost] // Garante que o método só seja achamado por requisições do tipo POST, ou seja, quando um formulário for enviado
        [ValidateAntiForgeryToken] // PROTAÇÃO CONTRA CSRF - Impede que isso aconteça exigindo um token de segurança em cada requisição POST. O Token é gerado automaticamente pelo ASP.NET
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department) // Recebe os dados preenchidos pelo usuário na VIew de criação
        {
            if (id != department.Id) // Verifica se o Id é válido
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Se os dados forem válidos, tenta atualizar o departamento
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Se houver erro de concorrência, verifica se o departamento ainda existe
            }
            return View(department); // Se tudo der certo, redireciona para a lista de departamentos
        }

        // GET - Carrega o formulário para edição:
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST - Atualizar um departamento:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
