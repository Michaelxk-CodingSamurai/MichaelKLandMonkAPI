using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;
using Entities.Extensions;

namespace LandMonkServer.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase

    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public PropertyController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}/unit")]
        public IActionResult GetPropertyWithDetails(int id) //use int not GUID
        {
            try
            {
                var property = _repository.Property.GetPropertyWithDetails(id);

                if (property.IsEmptyObject())
                {
                    _logger.LogError($"Property with id {id} was not found");
                    return NotFound();
                }

                _logger.LogInfo($"Returned property with id {id}.");

                return Ok(property);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPropertyWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllProperty()

        {
            try
            {
                var properties = _repository.Property.GetAllProperty();

                _logger.LogInfo($"Returned all properties from database");

                return Ok(properties);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProperty action:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "PropertyById")]
        public IActionResult GetPropertyById(int id)

        {
            try
            {
                var properties = _repository.Property.GetPropertyById(id);

                if (properties == null)
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned property with id: {id}");
                    return Ok(properties);

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPropertyById action:{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateProperty([FromBody]Property property)
        {
            try
            {
                if (property.IsObjectNull())
                {
                    _logger.LogError("Property object sent from client is null.");
                    return BadRequest("Property object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid property object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Property.CreateProperty(property);

                return CreatedAtRoute("PropertyById", new { id = property.Id }, property);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProperty action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, [FromBody]Property property)
        {
            try
            {
                if (property.IsObjectNull())
                {
                    _logger.LogError("Property object sent from client is null.");
                    return BadRequest("Property object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid property object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbProperty = _repository.Property.GetPropertyById(id);
                if (dbProperty.IsEmptyObject())
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Property.UpdateProperty(dbProperty, property);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProperty action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProperty(int id)
        {
            try
            {
                var property = _repository.Property.GetPropertyById(id);
                if (property.IsEmptyObject())
                {
                    _logger.LogError($"Property with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Unit.UnitsByProperty(id).Any())
                {
                    _logger.LogError($"Cannot delete property with id: {id}. It has related units. Delete those units first.");
                    return BadRequest("Cannot delete property. It has related units. Delete those units first.");

                }
                _repository.Property.DeleteProperty(property);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProperty action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}