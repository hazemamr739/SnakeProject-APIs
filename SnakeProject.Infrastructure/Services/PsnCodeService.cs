namespace SnakeProject.Application.Services;

public class PsnCodeService(IPsnCodeRepository _repository) : IPsnCodeService
{
    public async Task<IEnumerable<PsnCodeResponse>> GetAllPsnCodesAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(p => new PsnCodeResponse(p.Id, p.Code, p.IsUsed));
    }

    public async Task<PsnCode> GetPsnCodeByIdAsync(int id)
    {
        var psnCode = await _repository.FindAsync(id);
        return psnCode ?? throw new KeyNotFoundException($"PsnCode with ID {id} not found.");
    }

    public async Task<PsnCode> AddPsnCodeAsync(PsnCodeRequest codeRequest, CancellationToken cancellationToken = default)
    {
        var existingCode = await _repository.GetByCodeAsync(codeRequest.Code, cancellationToken);
        if (existingCode is not null)
            throw new InvalidOperationException($"PsnCode with code '{codeRequest.Code}' already exists.");

        var psnCode = codeRequest.Adapt<PsnCode>();
        _repository.Add(psnCode);
        await _repository.SaveChangesAsync(cancellationToken);
        return psnCode;
    }
}
