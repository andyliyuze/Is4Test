using AutoMapper;
using IdentityServer4.Models;
using Is4.Common.CustomAttributes;
using Is4.Domain.Repostitory;
using Is4.Service.Shared.DTO.ApiResource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiResource = IdentityServer4.EntityFramework.Entities.ApiResource;

namespace Is4.Service.Shared
{
    public class ApiResourceService : IApiResourceService
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IMapper _mapper;
        private const string SharedSecret = "SharedSecret";
        public ApiResourceService(IApiResourceRepository apiResourceRepository, IMapper mapper)
        {
            _apiResourceRepository = apiResourceRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<bool>> Create(CreateApiResourceInput createApiResourceInput)
        {
            createApiResourceInput.Secrets.ForEach(a => HashApiSharedSecret(a));
            var entity = _mapper.Map<ApiResource>(createApiResourceInput);

            await _apiResourceRepository.Create(entity);
            return new ResponseBase<bool>() { Result = true };
        }

        /// <summary>
        /// Hash加密
        /// </summary>
        /// <param name="apiSecretInput"></param>
        private void HashApiSharedSecret(CreateApiSecretInput apiSecretInput)
        {
            if (apiSecretInput.Type != SharedSecret) return;

            if (apiSecretInput.HashType == (int)HashType.Sha256)
            {
                apiSecretInput.Value = apiSecretInput.Value.Sha256();
            }
            else if (apiSecretInput.HashType == (int)HashType.Sha512)
            {
                apiSecretInput.Value = apiSecretInput.Value.Sha512();
            }
        }

        [UnitOfWork(false)]
        public async Task<ResponseBase<PaginatedList<ApiResourceOutput>>> GetList(int pageIndex, int pageSize)
        {
            var response = new ResponseBase<PaginatedList<ApiResourceOutput>>();
            if (pageIndex <= 0)
            {
                response.Message = "index必须大于等于1";
                return response;
            }
            var result = await _apiResourceRepository.Query().Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            var output = _mapper.Map<IList<ApiResourceOutput>>(result);
            var count = await _apiResourceRepository.Query().CountAsync();
            var paginatedList = new PaginatedList<ApiResourceOutput>(output, count, pageIndex, pageSize);

            response.Result = paginatedList;

            return response;
        }
    }
}
