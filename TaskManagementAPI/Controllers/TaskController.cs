using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using Task = TaskManagementAPI.Models.Task;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskDbContext _context;

    public TaskController(TaskDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            // Log exception if needed
            return StatusCode(500, new { Message = "An error occurred while fetching tasks.", Details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound(new { Message = $"Task with ID {id} not found." });

            return Ok(task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while fetching the task.", Details = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(Task task)
    {
        try
        {
            task.DueDate = DateTime.Parse(task.DueDate.ToString("yyyy-MM-dd"));
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { Message = "An error occurred while creating the task.", Details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, Task updatedTask)
    {
        try
        {
            if (id != updatedTask.Id) return BadRequest(new { Message = "Task ID mismatch." });

            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, new { Message = "A concurrency error occurred while updating the task.", Details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound(new { Message = $"Task with ID {id} not found." });

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while deleting the task.", Details = ex.Message });
        }
    }
}
