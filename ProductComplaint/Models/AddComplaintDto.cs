namespace ProductComplaint.Models
{
    public class AddComplaintDto
    {
        public required string ProductID { get; set; }

        public required string CustName { get; set; }

        public required string Email { get; set; }

        public DateTime DateofComp { get; set; }

        public required string ProblemDescription { get; set; }

        public required string Status { get; set; }
    }
}
