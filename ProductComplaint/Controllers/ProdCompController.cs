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

        //To get all complaints
        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            var allComplaints = dbContext.Products.ToList();
            
            return Ok(allComplaints);
        }

        //To get complaint by id
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

        //To add a complaint
        [HttpPost]
        public IActionResult AddComplaints(AddComplaintDto addComplaintDto)
        {

        //To check if all the values entered are not null and valid
            if (string.IsNullOrWhiteSpace(addComplaintDto.ProductID) || addComplaintDto.ProductID.Equals("string", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(addComplaintDto.CustName) || addComplaintDto.CustName.Equals("string", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(addComplaintDto.Email) || addComplaintDto.Email.Equals("string", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(addComplaintDto.ProblemDescription) || addComplaintDto.ProblemDescription.Equals("string", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(addComplaintDto.Status) || addComplaintDto.Status.Equals("string", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("All fields must be filled with valid values.");
            }

            //To check if the entered Status is valid
            var validStatuses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                 "Open",
                 "InProgress",
                 "Rejected",
                 "Accepted",
                 "Canceled"
            };

            if (!validStatuses.Contains(addComplaintDto.Status))
            {
                return BadRequest("Status must be one of the following: Open, InProgress, Rejected, Accepted, Canceled.");
            }

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

        //To update a complaint
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

        //To delete a complaint
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteComplaint(Guid id)
        {
            var prodcomp = dbContext.Products.Find(id);

            if (prodcomp == null)
            { 
                return NotFound();
            }

            if (prodcomp.Status.Equals("Canceled", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The complaint is already deleted or does not exist.");
            }

            //dbContext.Products.Remove(prodcomp);
            prodcomp.Status = "Canceled";
            dbContext.SaveChanges();
            
            return Ok();
        }
    }
}
