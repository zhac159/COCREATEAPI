using Application.DTOs.PrepareDTOs;

namespace Application.Interfaces;

public interface IPrepareService {
    PrepareUploadResponseDTO Upload(List<PrepareUploadDTO> prepareUploadDTO);
}
