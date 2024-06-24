using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductComplaint.Data;
using ProductComplaint.Models;
using ProductComplaint.Models.Entities;

namespace ProductComplaint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdCompController : ControllerBase
    {
        private readonly ProductComplaintDbContext dbContext;

        public ProdCompController(ProductComplaintDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            var allComplaints = dbContext.Products.ToList();
            
            return Ok(allComplaints);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetComplaintById(Guid id)
        {
            var prodcomp = dbContext.Products.Find(id);

            if (prodcomp == null)
            {
                return NotFound();
            }
            return Ok(prodcomp);

        }

        [HttpPost]
        public IActionResult AddComplaints(AddComplaintDto addComplaintDto)
        {
            var productcompEntity = new ProductComp()
            {
                ProductID = addComplaintDto.ProductID,
                CustName = addComplaintDto.CustName,
                Email = addComplaintDto.Email,
                DateofComp = addComplaintDto.DateofComp,
                ProblemDescription = addComplaintDto.ProblemDescription,
                Status = addComplaintDto.Status
            };


            dbContext.Products.Add(productcompEntity);
            dbContext.SaveChanges();

            return Ok(productcompEntity);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateComplaints(Guid id, UpdateComplaintDto updateComplaintDto)
        {
            var prodcomp = dbContext.Products.Find(id);

            if (prodcomp == null) 
            { 
                return NotFound(); 
            }

            if (!(prodcomp.Status.Equals("Open", StringComparison.OrdinalIgnoreCase) ||
           prodcomp.Status.Equals("InProgress", StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Complaints can only be updated if they are in the 'Open' or 'InProgress' status.");
            }

            // Update only the provided fields, keep existing values for others
            
            if (!string.IsNullOrWhiteSpace(updateComplaintDto.CustName) && updateComplaintDto.CustName != "string")
            {
                prodcomp.CustName = updateComplaintDto.CustName;
            }

            if (!string.IsNullOrWhiteSpace(updateComplaintDto.Email) && updateComplaintDto.Email != "string")
            {
                prodcomp.Email = updateComplaintDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(updateComplaintDto.ProblemDescription) && updateComplaintDto.ProblemDescription != "string")
            {
                prodcomp.ProblemDescription = updateComplaintDto.ProblemDescription;
            }

            if (!string.IsNullOrWhiteSpace(updateComplaintDto.Status) && updateComplaintDto.Status != "string")
            {
                prodcomp.Status = updateComplaintDto.Status;
            }

            dbContext.SaveChanges();

            return Ok(prodcomp);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteComplaint(Guid id)
        {
            var prodcomp = dbContext.Products.Find(id);

            if (prodcomp == null)
            { 
                return NotFound();
            }

            //dbContext.Products.Remove(prodcomp);
            prodcomp.Status = "Canceled";
            dbContext.SaveChanges();
            
            return Ok();
        }
    }
}
