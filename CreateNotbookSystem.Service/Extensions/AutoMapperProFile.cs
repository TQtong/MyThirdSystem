using AutoMapper;
using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Service.Context;

namespace CreateNotbookSystem.Service.Extensions
{
    public class AutoMapperProFile : MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<Backlog, BacklogDto>().ReverseMap();
            CreateMap<Memo, MemoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
