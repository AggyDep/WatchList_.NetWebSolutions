using API.DTOs.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<ActorDTO>> GetActors();
        Task<ActorDTO> GetActor(int id);
        Task<ActorPostDTO> PostActor(ActorPostDTO actorPostDTO);
        Task<ActorPutDTO> PutActor(int id, ActorPutDTO actorPutDTO);
        Task<ActorDeleteDTO> DeleteActor(int id);
    }
}
