using AutoMapper;
using ExaminationSystem.Api.DTO.Question;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.Services.ChoiceService;
using ExaminationSystem.Api.Services.QuestionService;
using ExaminationSystem.Api.ViewModels.CreateQuestionViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Controllers;

public class QuestionsController : BaseApiController
{
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Question> _questionsRepository;
    private readonly IChoiceService _choiceService;

    public QuestionsController(IQuestionService questionService,
        IMapper mapper,
        IGenericRepository<Question> _questionsRepository,
        IChoiceService choiceService)
    {
        _questionService = questionService;
        _mapper = mapper;
        this._questionsRepository = _questionsRepository;
        _choiceService = choiceService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<QuestionToReturnDto>> GetAllQuestion()
    {
        var questions = _questionsRepository.GetAll();
        var questionToReturnDto = _mapper.ProjectTo<QuestionToReturnDto>(questions);

        return Ok(questionToReturnDto); 
    }

    [HttpGet("{id}")]
    public ActionResult<QuestionToReturnDto> GetQuestionById(int id)
    {
        var question = _questionsRepository.Get(x => x.Id == id);
        var questionToReturnDto = _mapper.ProjectTo<QuestionToReturnDto>(question).FirstOrDefault();

        return Ok(questionToReturnDto); 
    }

    [HttpPost]
    public ActionResult<QuestionToReturnDto> AddQuestion(CreateQuestionViewModel viewModel)
    {
        var createQuestionDto = _mapper.Map<CreateQuestionDto>(viewModel);
        var question = _questionService.AddQuestion(createQuestionDto);
        var questionToReturnDto = _mapper.Map<QuestionToReturnDto>(question);
        return Ok(questionToReturnDto);
    }

    [HttpPut("{id}")]
    public ActionResult<QuestionToReturnDto> UpdateQuestion(int id, CreateQuestionViewModel viewModel)
    {
        var questionfromdb = _questionsRepository.GetByID(id);


        if (questionfromdb != null) 
        {
            var question = _mapper.Map<Question>(viewModel);

            question.Id = id;
            
            _choiceService.Removechoice(question.Id);
            question = _questionsRepository.Update(question);
            
            _questionsRepository.SaveChanges();
            var questionToReturnDto = _mapper.Map<QuestionToReturnDto>(question);
            return Ok(questionToReturnDto);
        }
        
        return NotFound();
    }

    [HttpDelete]
    public ActionResult DeleteQuestion(int id) 
    {
        _questionsRepository.Delete(id);
        _questionsRepository.SaveChanges();
        return Ok();
    }
}
