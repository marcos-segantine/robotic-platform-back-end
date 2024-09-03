using Robotic.Application.Interfaces;

public class GetAllStudentsQueryHandler
{
    private readonly IStudentRepository _repository;

    public GetAllStudentsQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<StudentDTO> Handle(GetAllStudentsQuery query)
    {
        var students = _repository.GetAll(query.School);
        var studentsDTO = students.Select(student => new StudentDTO(student.Name, student.School, student.Schooling, student.PhotoPath));

        return studentsDTO;
    }
}