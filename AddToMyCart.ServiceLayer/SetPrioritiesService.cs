using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.Repositories;
using AddToMyCart.DomainModels;
using AddToMyCart.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace AddToMyCart.ServiceLayer
{
    public interface ISetPrioritiesService
    {
        List<SetPriorityViewModel> GetAllPriorities();
    }

    public class SetPrioritiesService : ISetPrioritiesService
    {
        ISetPrioritiesRepository sr;

        public SetPrioritiesService()
        {
            sr = new SetPrioritiesRepository();
        }

        public List<SetPriorityViewModel> GetAllPriorities()
        {
            List<SetPriority> Priorities = sr.GetAllPriorities();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<SetPriority, SetPriorityViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<SetPriorityViewModel> pvm = mapper.Map<List<SetPriority>, List<SetPriorityViewModel>>(Priorities);
            return pvm;
        }
    }
}
