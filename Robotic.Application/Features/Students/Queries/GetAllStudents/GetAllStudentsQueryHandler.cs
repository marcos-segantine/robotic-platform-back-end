using Robotic.Application.Interfaces;

public class GetAllStudentsQueryHandler
{
    private readonly IStudentRepository _repository;

    public GetAllStudentsQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudentDTO>> Handle(GetAllStudentsQuery query)
    {
        var students = await _repository.GetAll(query.School);
        var studentsDTO = students.Select(student => new StudentDTO(
                student.Name,
                student.School,
                student.Schooling,
                student.PhotoPath,
                student.Points,
                student.Certificates,
                student.ScheduleClass
                ));

        return studentsDTO;
    }
}