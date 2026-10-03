namespace Application.DTOs.Quizzes
{
    public class QuizRequest
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? MaxAttempts { get; set; }

        public List<Guid> QuestionIds { get; set; } = new();
    }

    public class QuizResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int? MaxAttempts { get; set; }

        public List<Guid> QuestionIds { get; set; } = new();
    }
}
