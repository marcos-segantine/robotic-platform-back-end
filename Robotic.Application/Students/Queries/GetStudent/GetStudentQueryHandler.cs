using Robotic.Application.Interfaces;

namespace Robotic.Application.Students.Queries.GetStudent;

public class GetStudentQueryHandler
{
    private readonly IStudentRepository _repository;

    public GetStudentQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentDTO> Handle(GetStudentQuery query)
    {
        var student = await _repository.GetById(query.Id);
        var studentDTO = new StudentDTO(student.Name, student.School, student.Schooling, student.PhotoPath);

        return studentDTO;
    }
}