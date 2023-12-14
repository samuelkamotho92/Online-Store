using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Dtos;
using Online_Store.Models;
using Online_Store.Services.IService;
using System.Net;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUser _userService;
        private readonly ResponseDto _responseDto;
        private readonly IJWT _jwtService;
        public AuthController(IMapper mapper,IUser user,IJWT jwt) { 
        
            _mapper = mapper;
            _userService = user;
            _responseDto = new ResponseDto();
            _jwtService = jwt;
        }
        //GetRegistered user
        [HttpGet("{email}")]
        public async Task<ActionResult<ResponseDto>> GetUser(string email)
        {
            try
            {
                User userone = await _userService.GetUserByEmail(email);
                _responseDto.message = "Success";
                _responseDto.Result = userone;
                _responseDto.StatusCode = HttpStatusCode.OK;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.message = $"{ex.InnerException}";
                Console.WriteLine(ex.InnerException);
                return BadRequest(_responseDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddUser(AddUserDto userDto)
        {
            try
            {
                var userval =  await   _userService.GetUserByEmail(userDto.email);
                if (userval != null)
                {
                    _responseDto.message = $"user exists with the {userval.email} enter another user";
                    _responseDto.StatusCode = HttpStatusCode.Forbidden;
                   return BadRequest(_responseDto);
                }
                //maping
                var  newuser =  _mapper.Map<User>(userDto);
                newuser.password = BCrypt.Net.BCrypt.HashPassword(newuser.password);

               string resp = await  _userService.RegisterUser(newuser);
                Console.WriteLine(resp);
                _responseDto.message = resp;
                _responseDto.StatusCode = HttpStatusCode.Created;
                _responseDto.Result = newuser;
                return Ok(_responseDto);    
            }
            catch (Exception e)
            {
                _responseDto.message = $"{e.InnerException}";
                return BadRequest(_responseDto);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> loginUser(LoginDto userDto)
        {
            try
            {
                var userval = await _userService.GetUserByEmail(userDto.email);
                if (userval == null)
                {
                    _responseDto.message = "user does not exist";
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_responseDto);
                }
                var correctPas = BCrypt.Net.BCrypt.Verify(userDto.password, userval.password);
                Console.WriteLine(correctPas);
                if (!correctPas)
                {
                    _responseDto.message = "Password does not match";
                    return BadRequest(_responseDto);
                }
              string resp = await  _userService.LoginUser();
                _responseDto.message = resp;
                _responseDto.Result = userval;
                _responseDto.token =  _jwtService.GenerateToken(userval);
                return Ok(_responseDto);
            }
            catch(Exception e)
            {
                _responseDto.message = $"Something went very wrong {e.InnerException}";
                return BadRequest(_responseDto);

            }
        }
    }
}
