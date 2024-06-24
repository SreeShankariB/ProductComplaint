namespace ProductComplaint.Models
{
    public class UpdateComplaintDto
    {
        public required string CustName { get; set; }

        public required string Email { get; set; }

        public required string ProblemDescription { get; set; }

        public required string Status { get; set; }
    }
}
