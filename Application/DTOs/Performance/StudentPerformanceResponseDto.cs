using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Performance
{
    public class StudentPerformanceResponseDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int TotalAttempts { get; set; }
        public int CompletedAttempts { get; set; }
        public decimal AverageScore { get; set; }
        public decimal AveragePercentage { get; set; }
    }
}
