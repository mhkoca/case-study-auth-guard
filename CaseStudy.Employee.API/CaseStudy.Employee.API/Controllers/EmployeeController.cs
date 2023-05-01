using AutoMapper;
using CaseStudy.Employee.API.Models;
using CaseStudy.Employee.API.Models.DTOs;
using CaseStudy.Employee.API.Models.Entities;
using CaseStudy.Employee.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.Employee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            try
            {
                var employee = await _repository.Get(x => x.Id == id && x.IsActive && !x.IsDeleted).FirstOrDefaultAsync();

                if (employee == null)
                    return NotFound();

                var result = _mapper.Map<EmployeeDTO>(employee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                return new StatusCodeResult(500);
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            try
            {
                var employee = await _repository.List(x => x.IsActive && !x.IsDeleted).ToListAsync();

                if (employee == null)
                    return NotFound();

                var result = _mapper.Map<List<EmployeeDTO>>(employee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Create(EmployeeCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var modelToCreate = _mapper.Map<EmployeeEntity>(model);
                var insertResult = await _repository.InsertAsync(modelToCreate);

                if (insertResult)
                {
                    var result = _mapper.Map<EmployeeDTO>(modelToCreate);

                    return Ok(result);
                }

                _logger.Log(LogLevel.Error, "The record could not be inserted");

                return new StatusCodeResult(500);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                return new StatusCodeResult(500);
            }
        }


        [HttpPut]
        public async Task<ActionResult<bool>> Update(EmployeeUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var employee = await _repository.Get(x => x.Id == model.Id && x.IsActive && !x.IsDeleted).FirstOrDefaultAsync();

                if (employee == null)
                    return BadRequest();

                employee.FirstName = model.FirstName ?? employee.FirstName;
                employee.LastName = model.LastName ?? employee.LastName;
                employee.Email = model.Email ?? employee.Email;
                employee.Phone = model.Phone ?? employee.Phone;

                var updateResult = await _repository.UpdateAsync(employee);

                return Ok(updateResult);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {

                var employee = await _repository.Get(x => x.Id == id && x.IsActive && !x.IsDeleted).FirstOrDefaultAsync();

                if (employee == null)
                    return BadRequest();

                var deleteResult = await _repository.DeleteAsync(employee);

                return Ok(deleteResult);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);

                return new StatusCodeResult(500);
            }
        }
    }
}