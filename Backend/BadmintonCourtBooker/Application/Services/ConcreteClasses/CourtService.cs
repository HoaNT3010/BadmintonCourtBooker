using Application.Services.Interfaces;
using AutoMapper;
using Infrastructure.Data.UnitOfWork;

namespace Application.Services.ConcreteClasses
{
    public class CourtService : ICourtService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CourtService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
